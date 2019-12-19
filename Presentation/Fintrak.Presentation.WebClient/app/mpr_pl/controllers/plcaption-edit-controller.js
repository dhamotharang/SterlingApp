/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("PLCaptionEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        PLCaptionEditController]);

    function PLCaptionEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'plcaption-edit-view';
        vm.viewName = 'Captions';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.plCaption = {};

        vm.moduleOwners = [
           { Id: 1, Name: 'Generic' },
           { Id: 2, Name: 'MPR' },
            { Id: 3, Name: 'Budget' }
        ];

        vm.accountTypes = [
            { Id: 1, Name: 'View' },
            { Id: 2, Name: 'Asset' },
            { Id: 3, Name: 'Liability' },
            { Id: 4, Name: 'Income' },
            { Id: 5, Name: 'Expense' },
            { Id: 6, Name: 'ContigentAsset' },
            { Id: 7, Name: 'ContigentLiability' },
            { Id: 8, Name: 'Consolidation' }
        ];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.companies =[];
        //vm.accounttypes = [];
        vm.parents = [];

        var plcaptionRules = [];

        var setupRules = function () {
          
            plcaptionRules.push(new validator.PropertyRule("CaptionCode", {
                notZero: { message: "Code is required" }
            }));

            plcaptionRules.push(new validator.PropertyRule("CaptionName", {
                notZero: { message: "Name is required" }
            }));

            //plcaptionRules.push(new validator.PropertyRule("CompanyCode", {
            //    notZero: { message: "Company is required" }
            //}));

            plcaptionRules.push(new validator.PropertyRule("Position", {
                notZero: { message: "Position is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.plcaptionId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/plcaption/getplcaption/' + $stateParams.plcaptionId, null,
                   function (result) {
                       vm.plCaption = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.plCaption = { Code: '', Name: '', Position: 0, Color: '', AccountType: 0, CompanyCode: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getCompanies();
            //getAccounttypes();
            getCaptions();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.plCaption, plcaptionRules);
            vm.viewModelHelper.modelIsValid = vm.plCaption.isValid;
            vm.viewModelHelper.modelErrors = vm.plCaption.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/plcaption/updateplcaption', vm.plCaption,
               function (result) {
                   
                   $state.go('mpr-plcaption-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.plCaption.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/plcaption/deleteplcaption', $stateParams.plcaptionId,//vm.plCaption.plCaptionId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-plcaption-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-plcaption-list');
        };

        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                 function (result) {
                     vm.companies = result.data;
                     initialView();
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getCaptions = function () {
            vm.viewModelHelper.apiGet('api/plcaption/availableplcaptions', null,
                 function (result) {
                     vm.parents = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
