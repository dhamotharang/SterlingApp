/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BSCaptionEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        BSCaptionEditController]);

    function BSCaptionEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'bscaption-edit-view';
        vm.viewName = 'Caption';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.bsCaption = {};

        vm.categories = [
            { Id: 2, Name: 'Asset' },
            { Id: 3, Name: 'Liability' }
        ];

        vm.currencyTypes = [
           { Id: 1, Name: 'LCY' },
           { Id: 2, Name: 'FCY' }
        ];

        vm.moduleOwners = [
           { Id: 1, Name: 'Generic' },
           { Id: 2, Name: 'MPR' },
            { Id: 3, Name: 'Budget' }
        ];

        vm.balanceSheetTypes = [
           { Id: 1, Name: 'ON' },
           { Id: 2, Name: 'OFF' }
        ];

        vm.captions = [];
        vm.plcaptions = [];
        vm.companies = [];
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.CaptionId = '';

        var bscaptionRules = [];

        var setupRules = function () {
          
            bscaptionRules.push(new validator.PropertyRule("CaptionCode", {
                required: { message: "Code is required" }
            }));
            
            bscaptionRules.push(new validator.PropertyRule("CaptionName", {
                required: { message: "Name is required" }
            }));

            bscaptionRules.push(new validator.PropertyRule("Position", {
                notZero: { message: "Position cannot be zero" }
            }));

            bscaptionRules.push(new validator.PropertyRule("Category", {
                notZero: { message: "Category is required" }
            }));

            bscaptionRules.push(new validator.PropertyRule("CurrencyType", {
                notZero: { message: "Currency Type is required" }
            }));

            bscaptionRules.push(new validator.PropertyRule("ModuleOwnerType", {
                notZero: { message: "Module Owner is required" }
            }));

            bscaptionRules.push(new validator.PropertyRule("BalanceSheetType", {
                notZero: { message: "Balance Sheet Type is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.bscaptionId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/bscaption/getbscaption/' + $stateParams.bscaptionId, null,
                   function (result) {
                       vm.bsCaption = result.data;

                       vm.CaptionId = vm.bsCaption.CaptionId;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.bsCaption = { CaptionCode:'',CaptionName:'',Position:0,Category:0,CurrencyType:0,BalanceSheetType:0,CompanyCode:'', Active: true };
            }
        }

        var intializeLookUp = function () {
            getCompanies();
            getCaptions();
             getPLCaptions();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.bsCaption, bscaptionRules);
            vm.viewModelHelper.modelIsValid = vm.bsCaption.isValid;
            vm.viewModelHelper.modelErrors = vm.bsCaption.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/bscaption/updatebscaption', vm.bsCaption,
               function (result) {
                   
                   $state.go('mpr-bscaption-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.bsCaption.errors;

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
                vm.bsCaption.BSCaptionId = vm.CaptionId;
                vm.viewModelHelper.apiPost('api/bscaption/deletebscaption', vm.bsCaption.BSCaptionId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-bscaption-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-bscaption-list');
        };

        var getCaptions = function () {
            vm.viewModelHelper.apiGet('api/bscaption/availablebscaptions', null,
                 function (result) {
                     vm.captions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getPLCaptions = function () {
            vm.viewModelHelper.apiGet('api/plCaption/availableplCaptions', null,
                 function (result) {
                     vm.plcaptions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                 function (result) {
                     vm.companies = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
