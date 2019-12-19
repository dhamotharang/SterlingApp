/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CashFlowEditController",
            ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                CashFlowEditController]); // 

    function CashFlowEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {

        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Cashflow Primary Data';
        vm.view = 'cashflow-edit-view';
        vm.viewName = 'Cashflow Primary Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.cashFlow = {};
        vm.scheduleTypes = [];
              
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

     //   vm.openedRunDate = false;
       
       
        var cashflowRules = [];

        var setupRules = function () {

            cashflowRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));

            cashflowRules.push(new validator.PropertyRule("datepmt", {
                required: { message: "Date PMT is required" }
            }));

            cashflowRules.push(new validator.PropertyRule("amt_prin_pay", {
                notZero: { message: "Amt_prin_pay is required" }
            }));

            cashflowRules.push(new validator.PropertyRule("amt_int_pay", {
                notZero: { message: "Amt_Int_pay is required" }
            }));

            cashflowRules.push(new validator.PropertyRule("amt_fee_pay", {
                required: { message: "Amt_Fee_pay is required" }
            }));

        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

                if ($stateParams.CashflowId !== 0)
                {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/cashflow/getcashflow/' + $stateParams.CashflowId, null,

                   function (result) {
                       vm.cashFlow = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);

               }
               else
                    vm.cashFlow = {
                        RefNo: '',
                        datepmt: '',
                        amt_prin_pay: '',
                        amt_int_pay: '',
                        amt_fee_pay: '', 
                        Active: true
                    };
            }
        }

        var intializeLookUp = function () {
            getScheduleTypes();
            }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.cashFlow, cashflowRules);
            vm.viewModelHelper.modelIsValid = vm.cashFlow.isValid;
            vm.viewModelHelper.modelErrors = vm.cashFlow.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/cashflow/updatecashflow', vm.cashFlow,
               function (result) {
                   
                   $state.go('ifrs-cashflow-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.cashFlow.errors;

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
                vm.viewModelHelper.apiPost('api/cashflow/deletecashflow', vm.cashFlow.CashflowId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-cashflow-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-cashflow-list');
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
        vm.openRunDate3 = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate3 = true;
        }

        var getScheduleTypes = function () {
            vm.viewModelHelper.apiGet('api/scheduletype/availablescheduletypes', null,
                 function (result) {
                     vm.scheduleTypes = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }


}());
