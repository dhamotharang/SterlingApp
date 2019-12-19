/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("DepositRepaymentScheduleEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        DepositRepaymentScheduleEditController]);

    function DepositRepaymentScheduleEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Deposit Repayment Schedule';
        vm.view = 'depositrepaymentschedule-edit-view';
        vm.viewName = 'Deposit Repayment Schedule Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.DepositRepaymentSchedule = {};
        vm.companies = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

       
        var depositrepaymentscheduleRules = [];

        var setupRules = function () {
          
            depositrepaymentscheduleRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));

            //depositrepaymentscheduleRules.push(new validator.PropertyRule("Currency", {
            //    required: { message: "Currency is required" }
            //}));

            depositrepaymentscheduleRules.push(new validator.PropertyRule("PRINCIPAL_AMOUNT_DUE", {
                required: { message: "PRINCIPAL AMOUNT DUE is required" }
            }));

            depositrepaymentscheduleRules.push(new validator.PropertyRule("PRINCIPAL_PAID", {
                notZero: { message: "PRINCIPAL PAID is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.depositRepayId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/depositrepaymentschedule/getdepositrepaymentschedule/' + $stateParams.depositRepayId, null,
                   function (result) {
                       vm.depositrepaymentschedule = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.depositrepaymentschedule = { RefNo: '', Currency: '', Classification: '', ValueDate: '', MaturityDate: '', CleanPrice: '', FaceValue: '', CompanyCode: '', CouponRate: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getCompanies();
            }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.DepositRepaymentSchedule, depositrepaymentscheduleRules);
            vm.viewModelHelper.modelIsValid = vm.DepositRepaymentSchedule.isValid;
            vm.viewModelHelper.modelErrors = vm.DepositRepaymentSchedule.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/depositrepaymentschedule/updatedepositrepaymentschedule', vm.depositrepaymentschedule,
               function (result) {
                   
                   $state.go('ifrs-depositrepaymentschedule-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.DepositRepaymentSchedule.errors;

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
                vm.viewModelHelper.apiPost('api/DepositRepaymentSchedule/deletebond', vm.DepositRepaymentSchedule.BondId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-depositrepaymentschedule-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-depositrepaymentschedule-list');
        };

   
        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                 function (result) {
                     vm.companies = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Unable to load companies', 'Fintrak');
                 }, null);
        }
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
        setupRules();
        initialize(); 
    }
}());
