/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ConsolidatedLoansEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ConsolidatedLoansEditController]);

    function ConsolidatedLoansEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'consolidatedloans-edit-view';
        vm.viewName = 'ConsolidatedLoans';
        vm.status = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.consoliDATEDLoans = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var consolidatedloansRules = [];

        var setupRules = function () {

            consolidatedloansRules.push(new validator.PropertyRule("ID", {
                required: { message: "ID is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("AcctNo", {
                required: { message: "AcctNo is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("Classification", {
                required: { message: "Classification is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("ProductName", {
                required: { message: "ProductName is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("ValueDate", {
                required: { message: "ValueDate is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("MaturityDate", {
                required: { message: "MaturityDate is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("Sector", {
                required: { message: "Sector is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("SubSector", {
                required: { message: "SubSector is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("ProductType", {
                required: { message: "ProductType is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("Amount", {
                required: { message: "Amount is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("CurrentBalance", {
                required: { message: "CurrentBalance is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("AdjustedBalances", {
                required: { message: "AdjustedBalances is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("LoanType", {
                required: { message: "LoanType is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("Rate", {
                required: { message: "Rate is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("HC1", {
                required: { message: "HC1 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("HC2", {
                required: { message: "HC2 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("PAY1", {
                required: { message: "PAY1 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("OB1", {
                required: { message: "OB1 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("RR1", {
                required: { message: "RR1 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("PAY2", {
                required: { message: "PAY2 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("OB2", {
                required: { message: "OB2 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("RR2", {
                required: { message: "RR2 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("PAY3", {
                required: { message: "PAY3 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("OB3", {
                required: { message: "OB3 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("RR3", {
                required: { message: "RR3 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("PAY4", {
                required: { message: "PAY4 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("OB4", {
                required: { message: "OB4 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("RR4", {
                required: { message: "RR4 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("PAY5", {
                required: { message: "PAY5 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("OB5", {
                required: { message: "OB5 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("RR5", {
                required: { message: "RR5 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("PAY6", {
                required: { message: "PAY6 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("OB6", {
                required: { message: "OB6 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("RR6", {
                required: { message: "RR6 is required" }
            }));

            consolidatedloansRules.push(new validator.PropertyRule("RunDate", {
                required: { message: "RunDate is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/consolidatedloans/getconsolidatedloans/' + $stateParams.ID, null,
                   function (result) {
                       vm.consoliDATEDLoans = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.consoliDATEDLoans = { ID: '', AcctNo: '', Classification: '', ProductName: '', ValueDate: '', MaturityDate: '', Sector: '', SubSector: '', ProductType: '', Amount: '', CurrentBalance: '', AdjustedBalances: '', LoanType: '', Rate: '', HC1: '', HC2: '', PAY1: '', OB1: '', RR1: '', PAY2: '', OB2: '', RR2: '', PAY3: '', OB3: '', RR3: '', PAY4: '', OB4: '', RR4: '', PAY5: '', OB5: '', RR5: '', PAY6: '', OB6: '', RR6: '', RunDate: ''};
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.consoliDATEDLoans, consolidatedloansRules);
            vm.viewModelHelper.modelIsValid = vm.consoliDATEDLoans.isValid;
            vm.viewModelHelper.modelErrors = vm.consoliDATEDLoans.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/consolidatedloans/updateconsolidatedloans', vm.consoliDATEDLoans,
               function (result) {
                   
                   $state.go('ifrs9-consolidatedloans-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.consoliDATEDLoans.errors;

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
                vm.viewModelHelper.apiPost('api/consolidatedloans/deleteconsolidatedloans', vm.consoliDATEDLoans.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-consolidatedloans-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-consolidatedloans-list');
        };

        
        setupRules();
        initialize(); 
    }
}());
