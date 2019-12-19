/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsEquityUnqoutedEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IfrsEquityUnqoutedEditController]);

    function IfrsEquityUnqoutedEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrsequityunqouted-edit-view';
        vm.viewName = 'Equity Unqouted';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifrsEquityUnqouted = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ifrsequityunqoutedRules = [];

        vm.sectors = [];


        var setupRules = function () {
          
            ifrsequityunqoutedRules.push(new validator.PropertyRule("Stock_code", {
                required: { message: "Stock Code is required" }
            }));

            ifrsequityunqoutedRules.push(new validator.PropertyRule("Stock_description", {
                required: { message: "Stock Description is required" }
            }));

            ifrsequityunqoutedRules.push(new validator.PropertyRule("Units", {
                required: { message: "Units is required" }
            }));

            ifrsequityunqoutedRules.push(new validator.PropertyRule("EPS", {
                required: { message: "EPS is required" }
            }));


            ifrsequityunqoutedRules.push(new validator.PropertyRule("Book_value", {
                required: { message: "Book Value is required" }
            }));

            ifrsequityunqoutedRules.push(new validator.PropertyRule("Cash_flow", {
                required: { message: "Cash Flow is required" }
            }));

            ifrsequityunqoutedRules.push(new validator.PropertyRule("Sales", {
                required: { message: "Sales is required" }
            }));

            ifrsequityunqoutedRules.push(new validator.PropertyRule("Sector_code", {
                required: { message: "Sector Code is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.ifrsequityunqoutedId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ifrsequityunqouted/getifrsequityunqouted/' + $stateParams.ifrsequityunqoutedId, null,
                   function (result) {
                       vm.ifrsEquityUnqouted = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.ifrsEquityUnqouted = { Stock_code: '', Stock_description: '', Units: 0, EPS: 0,Book_value:0,Cash_flow:0,Sales:0,Sector_code:0,  Active: true };
            }
        }

        var intializeLookUp = function () {
            getSectors()
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ifrsEquityUnqouted, ifrsequityunqoutedRules);
            vm.viewModelHelper.modelIsValid = vm.ifrsEquityUnqouted.isValid;
            vm.viewModelHelper.modelErrors = vm.ifrsEquityUnqouted.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/ifrsequityunqouted/updateifrsequityunqouted', vm.ifrsEquityUnqouted,
               function (result) {
                   
                   $state.go('ifrs9-ifrsequityunqouted-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.ifrsEquityUnqouted.errors;

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
                vm.viewModelHelper.apiPost('api/ifrsequityunqouted/deleteifrsequityunqouted', vm.ifrsEquityUnqouted.IfrsEquityUnqoutedId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-ifrsequityunqouted-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-ifrsequityunqouted-list');
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
