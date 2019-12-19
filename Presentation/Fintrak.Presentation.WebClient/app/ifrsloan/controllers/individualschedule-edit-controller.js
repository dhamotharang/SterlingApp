/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IndividualScheduleEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IndividualScheduleEditController]);

    function IndividualScheduleEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'individualschedule-edit-view';
        vm.viewName = 'Individual Schedule';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.individualschedules = {};

        vm.distinctrefnos = [];
        vm.RefNo = 'None';

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.openedRunDate = false;
        
        var IndividualscheduleRules = [];

        var setupRules = function () {

            IndividualscheduleRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));

            //IndividualscheduleRules.push(new validator.PropertyRule("AccountNo", {
            //    required: { message: "AccountNo is required" }
            //}));

             IndividualscheduleRules.push(new validator.PropertyRule("Amount", {
                required: { message: "Amount is required" }
            }));

            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

               if ($stateParams.individualscheduleId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/individualschedule/getindividualschedule/' + $stateParams.individualscheduleId, null,
                   function (result) {
                       vm.individualschedules = result.data;
                       //getCCDefintion();
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                      
                   }, null);
                }
                else
                    vm.individualschedules = { RefNo: '', Amount: '0.0',IRR: '0.0', ValueDate: '', FirstRepaymentdate: '', PastDueAmount: '0.0', MaturityDate: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getdistinctrefnos();
      
        }


         var initialView = function () {
            
        }
        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.individualschedules, IndividualscheduleRules);
            vm.viewModelHelper.modelIsValid = vm.individualschedules.isValid;
            vm.viewModelHelper.modelErrors = vm.individualschedules.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/individualschedule/updateindividualschedule', vm.individualschedules,
               function (result) {
                   
                   $state.go('ifrsloan-individualschedule-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.individualschedules.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/individualschedule/deleteindividualschedule', vm.individualschedules.Id,//vm.collateralInformation.IndividualscheduleId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrsloan-individualschedule-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrsloan-individualschedule-list');
        };

        var getdistinctrefnos = function () {
            vm.viewModelHelper.apiGet('api/individualschedule/getdistinctrefnos/', null,
                   function (result) {
                       vm.distinctrefnos = result.data;
                       vm.init === true;
                       toastr.success('Loan RefNo data loaded, ready for modifiation.', 'Fintrak');
                   },
                   function (result) {
                       toastr.error('Fail to load Loan RefNo', 'Fintrak');
                   }, null);
        }

        vm.load = function () {
            var url = '';
            url = 'api/individualschedule/getindividualschedulebyrefno/' + vm.individualschedules.RefNo,
             //url = 'api/individualschedule/getindividualschedulebyrefno/' + vm.RefNo,

            vm.viewModelHelper.apiGet(url, null,
               function (result) {

                   vm.individualschedules.IRR = result.data[0].IRR;
                   vm.individualschedules.Amount = result.data[0].Amount;
                   vm.individualschedules.PastDueAmount = result.data[0].PastDueAmount;
                   vm.individualschedules.ValueDate = result.data[0].ValueDate;
                   vm.individualschedules.FirstRepaymentdate = result.data[0].FirstRepaymentdate;
                    vm.individualschedules.MaturityDate = result.data[0].MaturityDate;
                   //vm.individualschedules.Rundate = result.data[0].Rundate;

                   vm.init === true;
                  toastr.success('Data for the selected RefNo loaded.', 'Fintrak');
               },
               function (result) {
                   toastr.error('Fail to load data for the selected RefNo.', 'Fintrak');
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
