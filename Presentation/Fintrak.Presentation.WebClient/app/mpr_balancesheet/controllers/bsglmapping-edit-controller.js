/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BSGLMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        BSGLMappingEditController]);

    function BSGLMappingEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'bsglmapping-edit-view';
        vm.viewName = 'Balancesheet GL Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.bsGLMapping = {};

        vm.products = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var bsglmappingRules = [];

        var setupRules = function () {
          
            bsglmappingRules.push(new validator.PropertyRule("ProductCode", {
                required: { message: "Product is required" }
            }));

            bsglmappingRules.push(new validator.PropertyRule("GLCodeGLCode", {
                required: { message: "Caption is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.bsglmappingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/bsglmapping/getbsglmapping/' + $stateParams.bsglmappingId, null,
                   function (result) {
                       vm.bsGLMapping = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.bsGLMapping = { ProductCode: '', GLCode: '',CompanyCode:'', Active: true };
            }
        }

        var intializeLookUp = function () {
            getProducts();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.bsGLMapping, bsglmappingRules);
            vm.viewModelHelper.modelIsValid = vm.bsGLMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.bsGLMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/bsglmapping/updatebsglmapping', vm.bsGLMapping,
               function (result) {
                   
                   $state.go('mpr-bsglmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.bsGLMapping.errors;

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
                vm.viewModelHelper.apiPost('api/bsglmapping/deletebsglmapping', vm.bsGLMapping.BSGLMappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-bsglmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-bsglmapping-list');
        };

        var getProducts = function () {
            vm.viewModelHelper.apiGet('api/product/availableproducts', null,
                 function (result) {
                     vm.products = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
