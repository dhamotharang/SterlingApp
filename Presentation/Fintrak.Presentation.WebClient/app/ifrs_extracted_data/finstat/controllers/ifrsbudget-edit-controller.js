/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IFRSBudgetEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IFRSBudgetEditController]);

    function IFRSBudgetEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Finstat Core';
        vm.view = 'ifrsbudget-edit-view';
        vm.viewName = 'Finstat Budget';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.finstatbudget = {};

        vm.mainCaptions = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

      
        var finstatbudgetRules = [];

        var setupRules = function () {
          
            finstatbudgetRules.push(new validator.PropertyRule("CaptionName", {
                required: { message: "Caption Name is required" }
            }));

            finstatbudgetRules.push(new validator.PropertyRule("ReportDate", {
                required: { message: "Report Date is required" }
            }));

            finstatbudgetRules.push(new validator.PropertyRule("StretchBudget", {
                required: { message: "Stretch Budget is required" }
            }));

            finstatbudgetRules.push(new validator.PropertyRule("BoardBudget", {
                notZero: { message: "Board Budget is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.ifrsbudgetId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ifrsbudget/getifrsbudget/' + $stateParams.ifrsbudgetId, null,
                   function (result) {
                       vm.finstatbudget = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.finstatbudget = { Caption: '', FinType: '',FinSubType:'', Position: 1,Class: 1, Active: true };
            }
        }

        var intializeLookUp = function () {
            getMainCaptions();
        }

        var initialView = function () {
            
        }
        
        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.finstatbudget, finstatbudgetRules);
            vm.viewModelHelper.modelIsValid = vm.finstatbudget.isValid;
            vm.viewModelHelper.modelErrors = vm.finstatbudget.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/ifrsbudget/updateifrsBudget', vm.finstatbudget,
               function (result) {
                   
                   $state.go('finstat-budget-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.finstatbudget.errors;

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
                vm.viewModelHelper.apiPost('api/ifrsbudget/deleteifrsBudget', vm.finstatbudget.IFRSBudgetId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-budget-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('finstat-budget-list');
        };

        //vm.onFinTypeChanged = function (finType) {
        //    getFinSubTypes(finType);
        //}

        var getMainCaptions = function () {
            vm.viewModelHelper.apiGet('api/registry/getmainCaptions', null,
                 function (result) {
                     vm.mainCaptions = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load main captions.', 'Fintrak');
                 }, null);
        }
        
        //var getCaptions = function () {
        //    vm.viewModelHelper.apiGet('api/ifrsbudget/availableifrsBudgets', null,
        //         function (result) {
        //             vm.parents = result.data;
        //         },
        //         function (result) {
        //             toastr.error('Unable to load parents', 'Fintrak');
        //         }, null);
        //}
        setupRules();
        initialize(); 
    }
}());
