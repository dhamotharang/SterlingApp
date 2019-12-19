/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanPryEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        LoanPryEditController]);

    function LoanPryEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Loan Primary Data';
        vm.view = 'loanpry-edit-view';
        vm.viewName = 'Loan Primary Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.loanPry = {};
        vm.scheduleTypes = [];
              
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

     //   vm.openedRunDate = false;
       
       
        var loanpryRules = [];

        var setupRules = function () {
          
            loanpryRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));
      
            loanpryRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "AccountNo is required" }
            }));

            loanpryRules.push(new validator.PropertyRule("ValueDate", {
                notZero: { message: "ValueDate is required" }
            }));

            loanpryRules.push(new validator.PropertyRule("MaturityDate", {
                notZero: { message: "MaturityDate is required" }
            }));
            
            loanpryRules.push(new validator.PropertyRule("Amount", {
                required: { message: "Amount is required" }
            }));

            loanpryRules.push(new validator.PropertyRule("Rate", {
                notZero: { message: "Rate is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

                if ($stateParams.pryId !== 0)
                {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/loanpry/getloanpry/' + $stateParams.pryId, null,
                   function (result) {
                       vm.loanPry = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.loanPry = { RefNo: '', AccountNo: '', ValueDate: '', MaturityDate: '',  Rate: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getScheduleTypes();
            }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.loanPry, loanpryRules);
            vm.viewModelHelper.modelIsValid = vm.loanPry.isValid;
            vm.viewModelHelper.modelErrors = vm.loanPry.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/loanpry/updateloanpry', vm.loanPry,
               function (result) {
                   
                   $state.go('ifrs-loanprydata-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.loanPry.errors;

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
                vm.viewModelHelper.apiPost('api/loanpry/deleteloanpry', vm.loanPry.PryId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-loanprydata-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-loanprydata-list');
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
