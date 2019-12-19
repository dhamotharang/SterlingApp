/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsStocksPrimaryDataEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IfrsStocksPrimaryDataEditController]);

    function IfrsStocksPrimaryDataEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrsstocksprimarydata-edit-view';
        vm.viewName = 'Stocks Primary Data';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifrsStocksPrimaryData = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ifrsstocksprimarydataRules = [];
        vm.sectors = [];

        var setupRules = function () {
          
            ifrsstocksprimarydataRules.push(new validator.PropertyRule("Stock_code", {
                required: { message: "Stock Code is required" }
            }));

            ifrsstocksprimarydataRules.push(new validator.PropertyRule("Company_name", {
                required: { message: "Company is required" }
            }));

            ifrsstocksprimarydataRules.push(new validator.PropertyRule("Current_stock_price", {
                required: { message: "Current Stock Price is required" }
            }));

            ifrsstocksprimarydataRules.push(new validator.PropertyRule("Share_volume", {
                required: { message: "Share Volume is required" }
            }));

            ifrsstocksprimarydataRules.push(new validator.PropertyRule("EPS", {
                required: { message: "EPS is required" }
            }));


            ifrsstocksprimarydataRules.push(new validator.PropertyRule("Book_value", {
                required: { message: "Book Value is required" }
            }));

            ifrsstocksprimarydataRules.push(new validator.PropertyRule("Cash_flow", {
                required: { message: "Cash Flow is required" }
            }));

            ifrsstocksprimarydataRules.push(new validator.PropertyRule("Sales", {
                required: { message: "Sales is required" }
            }));

            ifrsstocksprimarydataRules.push(new validator.PropertyRule("Sector_code", {
                required: { message: "Sector Code is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.ifrsstocksprimarydataId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ifrsstocksprimarydata/getifrsstocksprimarydata/' + $stateParams.ifrsstocksprimarydataId, null,
                   function (result) {
                       vm.ifrsStocksPrimaryData = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.ifrsStocksPrimaryData = { Stock_code: '', Stock_description: '', Current_stock_price: 0, Share_volume: 0, EPS: 0, Book_value: 0, Cash_flow: 0, Sales: 0, Sector_code: 0, Active: true };
            }
        }

        var intializeLookUp = function () {
            getSectors()
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ifrsStocksPrimaryData, ifrsstocksprimarydataRules);
            vm.viewModelHelper.modelIsValid = vm.ifrsStocksPrimaryData.isValid;
            vm.viewModelHelper.modelErrors = vm.ifrsStocksPrimaryData.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/ifrsstocksprimarydata/updateifrsstocksprimarydata', vm.ifrsStocksPrimaryData,
               function (result) {
                   
                   $state.go('ifrs9-ifrsstocksprimarydata-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.ifrsStocksPrimaryData.errors;

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
                vm.viewModelHelper.apiPost('api/ifrsstocksprimarydata/deleteifrsstocksprimarydata', vm.ifrsStocksPrimaryData.IfrsStocksPrimaryDataId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-ifrsstocksprimarydata-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-ifrsstocksprimarydata-list');
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
