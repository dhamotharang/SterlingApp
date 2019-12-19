/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("PostingGLMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        PostingGLMappingEditController]);

    function PostingGLMappingEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'postingglmapping-edit-view';
        vm.viewName = 'PostingGLMapping';
        vm.status = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.postingglmappings = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var postingglmappingRules = [];

        var setupRules = function () {

            postingglmapping.push(new validator.PropertyRule("BS_GL", {
                required: { message: "BS_GL  is required" }
            }));
            postingglmapping.push(new validator.PropertyRule("PL_GL", {
                required: { message: "PL_GL  is required" }
            }));
            postingglmapping.push(new validator.PropertyRule("BS_Description", {
                required: { message: "BS_Description  is required" }
            }));
            postingglmapping.push(new validator.PropertyRule("PL_Description", {
                required: { message: "PL_Description  is required" }
            }));
            postingglmapping.push(new validator.PropertyRule("ProductCategory", {
                required: { message: "ProductCategory  is required" }
            }));
            postingglmapping.push(new validator.PropertyRule("Classification", {
                required: { message: "Classification  is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/postingglmapping/getpostingglmapping/' + $stateParams.ID, null,
                   function (result) {
                       vm.postingglmappings = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.postingglmappings = {
                        BS_GL: '',
                        PL_GL: '',
                        BS_Description: '',
                        PL_Description: '',
                        ProductCategory: '',
                        Classification: '',
                        Active: true
                    };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.postingglmappings, postingglmappingRules);
            vm.viewModelHelper.modelIsValid = vm.postingglmappings.isValid;
            vm.viewModelHelper.modelErrors = vm.postingglmappings.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/postingglmapping/updatepostingglmapping', vm.postingglmappings,
               function (result) {
                   
                   $state.go('ifrs9-postingglmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.postingglmappings.errors;

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
                vm.viewModelHelper.apiPost('api/postingglmapping/deletepostingglmapping', vm.postingglmappings.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-postingglmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-postingglmapping-list');
        };

        
        setupRules();
        initialize(); 
    }
}());
