/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("NonProductMapEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        NonProductMapEditController]);

    function NonProductMapEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Balancesheet';
        vm.view = 'nonproductmap-edit-view';
        vm.viewName = 'Non Product Map';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.nonProductMap = {};

        vm.nonProducts = [];
        vm.products = [];
        vm.captions = [];
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var nonproductmapRules = [];

        var setupRules = function () {
          
            nonproductmapRules.push(new validator.PropertyRule("NonProductCode", {
                required: { message: "Non Product is required" }
            }));

            //nonproductmapRules.push(new validator.PropertyRule("ProductCode", {
            //    required: { message: "Product is required" }
            //}));

            nonproductmapRules.push(new validator.PropertyRule("CaptionCode", {
                required: { message: "Caption is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.nonproductmapId !== 0 && $stateParams.nonproductmapId !== "0" ) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/nonproductmap/getnonproductmap/' + $stateParams.nonproductmapId, null,
                   function (result) {
                       vm.nonProductMap = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       vm.nonProductMap = { NonProductCode: '', ProductCode: '', CaptionCode: '', Active: true };
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.nonProductMap = {NonProductCode: '', ProductCode: '', CaptionCode: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getNonProducts();
            getProducts();
            getCaptions();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.nonProductMap, nonproductmapRules);
            vm.viewModelHelper.modelIsValid = vm.nonProductMap.isValid;
            vm.viewModelHelper.modelErrors = vm.nonProductMap.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/nonproductmap/updatenonproductmap', vm.nonProductMap,
               function (result) {
                   
                   $state.go('mpr-nonproductmap-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.nonProductMap.errors;

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
                vm.viewModelHelper.apiPost('api/nonproductmap/deletenonproductmap', vm.nonProductMap.NonProductMapId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-nonproductmap-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-nonproductmap-list');
        };

        var getNonProducts = function () {
            vm.viewModelHelper.apiGet('api/mprproduct/getnonproducts', null,
                 function (result) {
                     vm.nonProducts = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getProducts = function () {
            vm.viewModelHelper.apiGet('api/mprproduct/getrealproducts', null,
                 function (result) {
                     vm.products = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getCaptions = function () {
            vm.viewModelHelper.apiGet('api/bscaption/availablebscaptions', null,
                 function (result) {
                     vm.captions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
