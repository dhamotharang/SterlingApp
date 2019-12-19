/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanPryMoratoriumEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        LoanPryMoratoriumEditController]);

    function LoanPryMoratoriumEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Loan Primary Data';
        vm.view = 'loanprymoratorium-edit-view';
        vm.viewName = 'Loan Primary Moratorium Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.loanPryMoratorium = {};
       // vm.scheduleTypes = [];
              
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

     //   vm.openedRunDate = false;
       
       
        var loanprymoratoriumRules = [];

        var setupRules = function () {
          
            loanprymoratoriumRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));
      
            loanprymoratoriumRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "AccountNo is required" }
            }));

            loanprymoratoriumRules.push(new validator.PropertyRule("ValueDate", {
                notZero: { message: "ValueDate is required" }
            }));

            loanprymoratoriumRules.push(new validator.PropertyRule("MaturityDate", {
                notZero: { message: "MaturityDate is required" }
            }));
            
            loanprymoratoriumRules.push(new validator.PropertyRule("Amount", {
                required: { message: "Amount is required" }
            }));

            loanprymoratoriumRules.push(new validator.PropertyRule("Rate", {
                notZero: { message: "Rate is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

               if ($stateParams.loanPryMoratoriumId !== 0)
                                
                {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/loanprymoratorium/getloanprymoratorium/' + $stateParams.loanPryMoratoriumId, null,
                   function (result) {
                       vm.loanPryMoratorium = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.loanPryMoratorium = { RefNo: '', AccountNo: '', ValueDate: '', MaturityDate: '',  Rate: '', Active: true };
            }
        }

        var intializeLookUp = function () {
          //  getScheduleTypes();
            }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.loanPryMoratorium, loanprymoratoriumRules);
            vm.viewModelHelper.modelIsValid = vm.loanPryMoratorium.isValid;
            vm.viewModelHelper.modelErrors = vm.loanPryMoratorium.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/loanprymoratorium/updateloanprymoratorium', vm.loanPryMoratorium,
               function (result) {
                   
                   $state.go('ifrs-loanprymoratoriumdata-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.loanPryMoratorium.errors;

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
                vm.viewModelHelper.apiPost('api/loanprymoratorium/deleteloanprymoratorium', vm.loanPryMoratorium.LoanPryMoratoriumId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-loanprymoratoriumdata-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-loanprymoratoriumdata-list');
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

        setupRules();
        initialize(); 
    }
}());
