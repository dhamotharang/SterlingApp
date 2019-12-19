/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LGDComptResultEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        LGDComptResultEditController]);

    function LGDComptResultEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'LGD Computation Result Data';
        vm.view = 'lgdcomptresult-edit-view';
        vm.viewName = 'LGD Computation Result Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.lgdComptResult = {};
        vm.scheduleTypes = [];
              
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

     //   vm.openedRunDate = false;


        var lgdcomptresultRules = [];

        var setupRules = function () {
          
            lgdcomptresultRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "AccountNo is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("Refno", {
                required: { message: "Refno is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("Producttype", {
                required: { message: "Producttype is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("SubType", {
                required: { message: "SubType is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("CustomerName", {
                required: { message: "CustomerName is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("Currency", {
                required: { message: "Currency is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("ExchangeRate", {
                required: { message: "ExchangeRate is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("date_pmt", {
                required: { message: "date_pmt is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("AmortizedCost_FCY", {
                required: { message: "AmortizedCost_FCY is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("AmortizedCost_LCY", {
                required: { message: "AmortizedCost_LCY is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("EIR", {
                required: { message: "EIR is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("Stage", {
                required: { message: "Stage is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("MaturityDate", {
                required: { message: "MaturityDate is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("TotalColRecAmt", {
                required: { message: "TotalColRecAmt is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("RecoveryRate", {
                required: { message: "RecoveryRate is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("UNSecuredRecovery", {
                required: { message: "UNSecuredRecovery is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("TotalRecoverableAmt", {
                required: { message: "TotalRecoverableAmt is required" }
            }));
          
            lgdcomptresultRules.push(new validator.PropertyRule("LGD", {
                required: { message: "LGD is required" }
            }));
     
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

                if ($stateParams.Id !== 0)
                {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/lgdcomptresult/getlgdcomptresult/' + $stateParams.Id, null,
                   function (result) {
                       vm.lgdComptResult = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.lgdComptResult = {
                        AccountNo: '',
                        Refno: '',
                        Producttype: '',
                        SubType: '',
                        CustomerName: '',
                        Currency: '',
                        ExchangeRate: '',
                        date_pmt: '',
                        AmortizedCost_FCY: '',
                        AmortizedCost_LCY: '',
                        EIR: '',
                        Stage: '',
                        MaturityDate: '',
                        TotalColRecAmt: '',
                        RecoveryRate: '',
                        UNSecuredRecovery: '',
                        TotalRecoverableAmt: '',
                        LGD: '',
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
            validator.ValidateModel(vm.lgdComptResult, lgdcomptresultRules);
            vm.viewModelHelper.modelIsValid = vm.lgdComptResult.isValid;
            vm.viewModelHelper.modelErrors = vm.lgdComptResult.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/lgdcomptresult/updatelgdcomptresult', vm.lgdComptResult,
               function (result) {
                   
                   $state.go('ifrs-lgdcomptresult-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.lgdComptResult.errors;

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
                vm.viewModelHelper.apiPost('api/lgdcomptresult/deletelgdcomptresult', vm.lgdComptResult.Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-lgdcomptresult-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-lgdcomptresult-list');
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
