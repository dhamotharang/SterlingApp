/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsStocksMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IfrsStocksMappingEditController]);

    function IfrsStocksMappingEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrsstocksmapping-edit-view';
        vm.viewName = 'Stocks Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifrsStocksMapping = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ifrsstocksmappingRules = [];
        vm.qouteds = [];
        vm.unqouteds = [];


        var setupRules = function () {
          
            ifrsstocksmappingRules.push(new validator.PropertyRule("Unqouted_stock_code", {
                required: { message: "Unqouted Stock Code is required" }
            }));

            ifrsstocksmappingRules.push(new validator.PropertyRule("Qouted_stock_code", {
                required: { message: "Qouted Stock Code is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.ifrsstocksmappingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ifrsstocksmapping/getifrsstocksmapping/' + $stateParams.ifrsstocksmappingId, null,
                   function (result) {
                       vm.ifrsStocksMapping = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.ifrsStocksMapping = { Unqouted_stock_code: '', Qouted_stock_code: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getQouteds()
            getUnQouteds()
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ifrsStocksMapping, ifrsstocksmappingRules);
            vm.viewModelHelper.modelIsValid = vm.ifrsStocksMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.ifrsStocksMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/ifrsstocksmapping/updateifrsstocksmapping', vm.ifrsStocksMapping,
               function (result) {
                   
                   $state.go('ifrs9-ifrsstocksmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.ifrsStocksMapping.errors;

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
                vm.viewModelHelper.apiPost('api/ifrsstocksmapping/deleteifrsstocksmapping', vm.ifrsStocksMapping.IfrsStocksMappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-ifrsstocksmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-ifrsstocksmapping-list');
        };



        var getQouteds = function () {
            vm.viewModelHelper.apiGet('api/ifrsstocksprimarydata/availableifrsStocksPrimaryDatas', null,
                 function (result) {
                     vm.qouteds = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }


        var getUnQouteds = function () {
            vm.viewModelHelper.apiGet('api/ifrsequityunqouted/availableifrsEquityUnqouteds', null,
                 function (result) {
                     vm.unqouteds = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
