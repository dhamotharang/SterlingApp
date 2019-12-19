/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IFRSTbillEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IFRSTbillEditController]);

    function IFRSTbillEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS Tbill Data';
        vm.view = 'tbilldata-edit-view';
        vm.viewName = 'IFRS Treasury Bill Data Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifrsTbill = {};

        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.classification = '';
        vm.openedRunDate = false;
        vm.classifications = [
            { Id: 1, Name: 'Amortised Cost' },
            { Id: 2, Name: 'FVPL' },
            { Id: 3, Name: 'FVOCI' }
        ];
       
        var tbilldataRules = [];

        var setupRules = function () {
          
            tbilldataRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));

            tbilldataRules.push(new validator.PropertyRule("Classification", {
                required: { message: "Classification is required" }
            }));

            tbilldataRules.push(new validator.PropertyRule("EffectiveDate", {
                notZero: { message: "EffectiveDate is required" }
            }));

            tbilldataRules.push(new validator.PropertyRule("MaturityDate", {
                notZero: { message: "MaturityDate is required" }
            }));

            tbilldataRules.push(new validator.PropertyRule("CleanPrice", {
                required: { message: "CleanPrice is required" }
            }));

            tbilldataRules.push(new validator.PropertyRule("FaceValue", {
                required: { message: "FaceValue is required" }
            }));

            tbilldataRules.push(new validator.PropertyRule("InterestRate", {
                notZero: { message: "InterestRate is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
              

                if ($stateParams.tbillId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ifrstbill/gettbilldata/' + $stateParams.tbillId, null,
                   function (result) {
                       vm.ifrsTbill = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.ifrsTbill = { RefNo: '',  Classification: '', EffectiveDate: '', MaturityDate: '', CleanPrice: '', FaceValue: '',  InterestRate: '', Active: true };
            }
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ifrsTbill, tbilldataRules);
            vm.viewModelHelper.modelIsValid = vm.ifrsTbill.isValid;
            vm.viewModelHelper.modelErrors = vm.ifrsTbill.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/ifrstbill/updateifrstbill', vm.ifrsTbill,
               function (result) {
                   
                   $state.go('ifrs-tbills-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.ifrsTbill.errors;

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
                vm.viewModelHelper.apiPost('api/ifrstbill/deleteifrstbill', vm.ifrsTbill.TbillId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-tbills-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-tbills-list');
        };

        vm.openRunDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate = true;
        }
        vm.openRunDate2 = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate2 = true;
        }
   
        setupRules();
        initialize(); 
    }
}());
