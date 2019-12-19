/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanInterestRateEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        LoanInterestRateEditController]);

    function LoanInterestRateEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS';
        vm.view = 'loaninterestrate-edit-view';
        vm.viewName = 'Floating Loan Interest Rates';
        vm.status='false';
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.loanInterestRate = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var loanInterestRateRules = [];


        var setupRules = function () {
          
            loanInterestRateRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));
          
            loanInterestRateRules.push(new validator.PropertyRule("CustomerName", {
                required: { message: "Customer Name is required" }
            }));

            loanInterestRateRules.push(new validator.PropertyRule("Date_PMT", {
                required: { message: "Date PMT is required" }
            }));

            loanInterestRateRules.push(new validator.PropertyRule("FPD_Date", {
                required: { message: "FPD Date is required" }
            }));

            loanInterestRateRules.push(new validator.PropertyRule("Repayment_Freq", {
                required: { message: "Repayment Frequency is required" }
            }));

            loanInterestRateRules.push(new validator.PropertyRule("Rate", {
                required: { message: "Rate is required" }
            }));


        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.LoanInterestRate_Id !== 0) {
                    vm.showChildren = true;
                    vm.status='true';
                    vm.viewModelHelper.apiGet('api/loaninterestrate/getloaninterestrate/' + $stateParams.LoanInterestRate_Id, null,
                   function (result) {
                       vm.loanInterestRate = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.loanInterestRate = { RefNo: '', CustomerName: '', Date_PMT: '', FPD_Date: '', Repayment_Freq: 1, Rate: 0, Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.loanInterestRate, loanInterestRateRules);
            vm.viewModelHelper.modelIsValid = vm.loanInterestRate.isValid;
            vm.viewModelHelper.modelErrors = vm.loanInterestRate.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/loaninterestrate/updateloaninterestrate', vm.loanInterestRate,
               function (result) {
                   
                   $state.go('ifrs-loaninterestrate-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.loanInterestRate.errors;

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
                vm.viewModelHelper.apiPost('api/loaninterestrate/deleteloaninterestrate', vm.loanInterestRate.LoanInterestRate_Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-loaninterestrate-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-loaninterestrate-list');
        };


        setupRules();
        initialize(); 
    }
}());
