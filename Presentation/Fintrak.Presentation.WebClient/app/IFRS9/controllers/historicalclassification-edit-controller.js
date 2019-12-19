/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("HistoricalClassificationEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        HistoricalClassificationEditController]);

    function HistoricalClassificationEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'historicalclassification-edit-view';
        vm.viewName = 'Historical Classification';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.historicalClassification = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var historicalclassificationRules = [];
        vm.sectors = [];

        vm.collateralTypes = [
           { Id: 1, Name: 'LAND' },
           { Id: 2, Name: 'BUILDING' },
           { Id: 3, Name: 'PLANT & EQUIPMENT' },
           { Id: 4, Name: 'STOCKS' },
        ];


        vm.classifications = [
            { Id: 1, Name: 'NONPERFORMING' },
            { Id: 2, Name: 'PERFORMING' },
        ];

        vm.subclassifications = [
            { Id: 1, Name: 'Doubtful'},
            { Id: 2, Name: 'Lost'},
            { Id: 3, Name: 'Standard'},
            { Id: 4, Name: 'Sub-Standard'},
        ];


        var setupRules = function () {
          
            historicalclassificationRules.push(new validator.PropertyRule("CustomerNo", {
                required: { message: "CustomerNo is required" }
            }));

            historicalclassificationRules.push(new validator.PropertyRule("CustomerName", {
                required: { message: "CustomerName is required" }
            }));

            historicalclassificationRules.push(new validator.PropertyRule("SectorIndustry", {
                required: { message: "Sector is required" }
            }));

            historicalclassificationRules.push(new validator.PropertyRule("Classification", {
                required: { message: "Classification is required" }
            }));

            historicalclassificationRules.push(new validator.PropertyRule("SubClassification", {
                required: { message: "SubClassification is required" }
            }));

            historicalclassificationRules.push(new validator.PropertyRule("OutstandingBal", {
                required: { message: "Outstanding Balance is required" }
            }));

            historicalclassificationRules.push(new validator.PropertyRule("RecoverableAmt", {
                required: { message: "Recoverable Amount is required" }
            }));

            historicalclassificationRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));

            historicalclassificationRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.historicalclassificationId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/historicalclassification/gethistoricalclassification/' + $stateParams.historicalclassificationId, null,
                   function (result) {
                       vm.historicalClassification = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.historicalClassification = { CustomerNo: '', CustomerName: '', SectorIndustry: '', Classification: '', SubClassification: '', OutstandingBal: 0, RecoverableAmt: 0, Period: 0, Year: 0, Active: true };
            }
        }

        var intializeLookUp = function () {
            getSectors();
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.historicalClassification, historicalclassificationRules);
            vm.viewModelHelper.modelIsValid = vm.historicalClassification.isValid;
            vm.viewModelHelper.modelErrors = vm.historicalClassification.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/historicalclassification/updatehistoricalclassification', vm.historicalClassification,
               function (result) {
                   
                   $state.go('ifrs9-historicalclassification-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.historicalClassification.errors;

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
                vm.viewModelHelper.apiPost('api/historicalclassification/deletehistoricalclassification', vm.historicalClassification.HistoricalClassificationId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-historicalclassification-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-historicalclassification-list');
        };


        var getSectors = function () {
            vm.viewModelHelper.apiGet('api/sector/availablesectors', null,
                 function (result) {
                     vm.sectors = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
