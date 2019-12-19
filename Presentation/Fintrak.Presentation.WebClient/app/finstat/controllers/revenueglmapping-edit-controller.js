/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RevenueGLMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        RevenueGLMappingEditController]);

    function RevenueGLMappingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'revenueglmapping-edit-view';
        vm.viewName = 'Revenue Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.glMapping = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.companies = [];
        vm.captions = [];
        vm.subCaptions = [];
        vm.subCaption1s = [];
        vm.subCaption2s = [];
        vm.subCaption3s = [];
        vm.subCaption4s = [];

        vm.options = [
        'Blue',
        'Red',
        'Pink',
        'Purple',
        'Green'];

        var glmappingRules = [];

        var setupRules = function () {
          
            glmappingRules.push(new validator.PropertyRule("GLCode", {
                required: { message: "GLCode is required" }
            }));

            glmappingRules.push(new validator.PropertyRule("GLDescription", {
                required: { message: "Description is required" }
            }));

            glmappingRules.push(new validator.PropertyRule("Caption", {
                required: { message: "Caption is required" }
            }));

            glmappingRules.push(new validator.PropertyRule("SubCaption", {
                required: { message: "SubCaption is required" }
            }));

            glmappingRules.push(new validator.PropertyRule("SubPosition", {
                notZero: { message: "Sub position is required" }
            }));

            glmappingRules.push(new validator.PropertyRule("CompanyCode", {
                required: { message: "Company is required" }
            }));

            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.glmappingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/revenueglmapping/getglmapping/' + $stateParams.glmappingId, null,
                   function (result) {
                       vm.glMapping = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.glMapping = { GLCode: '',GLDescription:'',Caption:'',SubCaption:'',SubPosition: 1, Active: true };
            }
        }

        var intializeLookUp = function () {
            getCompanies();
            getCaptions();
            getSubCaptions();
            getSubCaption1s();
            getSubCaption2s();
            getSubCaption3s();
            getSubCaption4s();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.glMapping, glmappingRules);
            vm.viewModelHelper.modelIsValid = vm.glMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.glMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/revenueglmapping/updateglmapping', vm.glMapping,
               function (result) {
                   
                   $state.go('finstat-revenueglmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.glMapping.errors;

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
                vm.viewModelHelper.apiPost('api/revenueglmapping/deleteglmapping', vm.glMapping.GLMappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-revenueglmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('finstat-revenueglmapping-list');
        };

        vm.onCaptionChanged = function (caption) {
            vm.glMapping.Caption = caption.Name;
        }

        vm.onSubCaptionChanged = function (caption) {
            vm.glMapping.SubCaption = caption.Name;
        }

        vm.onSubCaption1Changed = function (caption) {
            vm.glMapping.SubCaption1 = caption.Name;
        }

        vm.onSubCaption2Changed = function (caption) {
            vm.glMapping.SubCaption2 = caption.Name;
        }

        vm.onSubCaption3Changed = function (caption) {
            vm.glMapping.SubCaption3 = caption.Name;
        }

        vm.onSubCaption4Changed = function (caption) {
            vm.glMapping.SubCaption4 = caption.Name;
        }

        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                 function (result) {
                     vm.companies = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getCaptions = function () {
            vm.viewModelHelper.apiGet('api/revenueglmapping/getsubcaptions/0', null,
                 function (result) {
                     vm.captions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getSubCaptions = function () {
            vm.viewModelHelper.apiGet('api/revenueglmapping/getsubcaptions/1', null,
                 function (result) {
                     vm.subCaptions = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions.', 'Fintrak');
                 }, null);
        }

        var getSubCaption1s = function () {
            vm.viewModelHelper.apiGet('api/revenueglmapping/getsubcaptions/2', null,
                 function (result) {
                     vm.subCaption1s = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 2.', 'Fintrak');
                 }, null);
        }

        var getSubCaption2s = function () {
            vm.viewModelHelper.apiGet('api/revenueglmapping/getsubcaptions/3', null,
                 function (result) {
                     vm.subCaption2s = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 3.', 'Fintrak');
                 }, null);
        }

        var getSubCaption3s = function () {
            vm.viewModelHelper.apiGet('api/revenueglmapping/getsubcaptions/4', null,
                 function (result) {
                     vm.subCaption3s = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 4.', 'Fintrak');
                 }, null);
        }

        var getSubCaption4s = function () {
            vm.viewModelHelper.apiGet('api/revenueglmapping/getsubcaptions/5', null,
                 function (result) {
                     vm.subCaption4s = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 5.', 'Fintrak');
                 }, null);
        }

       
        setupRules();
        initialize(); 
    }
}());
