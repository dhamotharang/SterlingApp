/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("NonProductRateEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        NonProductRateEditController]);

    function NonProductRateEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Balancesheet';
        vm.view = 'nonproductrate-edit-view';
        vm.viewName = 'Non Product Rate';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.nonProductRate = {};

        vm.nonProducts = [];
        vm.runDate = {};
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var nonproductrateRules = [];

        var setupRules = function () {
          
            nonproductrateRules.push(new validator.PropertyRule("NonProductCode", {
                required: { message: "Non Product is required" }
            }));

            nonproductrateRules.push(new validator.PropertyRule("ProductCode", {
                required: { message: "Product is required" }
            }));

            nonproductrateRules.push(new validator.PropertyRule("CaptionCode", {
                required: { message: "Caption is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.nonproductrateId !== 0 && $stateParams.nonproductrateId !== "0" ) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/nonproductrate/getnonproductrate/' + $stateParams.nonproductrateId, null,
                   function (result) {
                       vm.nonProductRate = result.data;
                       
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.nonProductRate = { ProductCode: '', Period: vm.runDate.Period, Year: vm.runDate.Year, Rate: 0, Active: true };

                getRunDate();
            }
        }

        var intializeLookUp = function () {
            getNonProducts();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.nonProductRate, nonproductrateRules);
            vm.viewModelHelper.modelIsValid = vm.nonProductRate.isValid;
            vm.viewModelHelper.modelErrors = vm.nonProductRate.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/nonproductrate/updatenonproductrate', vm.nonProductRate,
               function (result) {
                   
                   $state.go('mpr-nonproductrate-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.nonProductRate.errors;

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
                vm.viewModelHelper.apiPost('api/nonproductrate/deletenonproductrate', vm.nonProductRate.NonProductRateId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-nonproductrate-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-nonproductrate-list');
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

        var getRunDate = function () {
            vm.viewModelHelper.apiGet('api/solutionrundate/getsolutionrundatebysolution/FIN_MPR', null,
                 function (result) {
                     vm.runDate = result.data;
                     vm.nonProductRate.Period = vm.runDate.Month;
                     vm.nonProductRate.Year = vm.runDate.Year;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
