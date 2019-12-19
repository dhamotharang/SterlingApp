/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("UnMappedGLEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        UnMappedGLEditController]);

    function UnMappedGLEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'unmappedgl-edit-view';
        vm.viewName = 'Un-Mapped GL';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.unMappedGL = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.companies = [];
        vm.mainCaptions = [];
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

        var unmappedglRules = [];

        var setupRules = function () {
          
            unmappedglRules.push(new validator.PropertyRule("GLCode", {
                required: { message: "GLCode is required" }
            }));

            unmappedglRules.push(new validator.PropertyRule("GLDescription", {
                required: { message: "Description is required" }
            }));

            unmappedglRules.push(new validator.PropertyRule("CaptionCode", {
                required: { message: "MainCaption is required" }
            }));

            unmappedglRules.push(new validator.PropertyRule("SubCaption", {
                required: { message: "SubCaption is required" }
            }));

            unmappedglRules.push(new validator.PropertyRule("SubPosition", {
                notZero: { message: "Sub position is required" }
            }));

            unmappedglRules.push(new validator.PropertyRule("CompanyCode", {
                required: { message: "Company is required" }
            }));

            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.glcode !== '') {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/glmapping/getunmappedgl/' + $stateParams.glcode, null,
                   function (result) {
                       vm.unMappedGL = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                   vm.unMappedGL = { GLCode: '', GLDescription: '',GLParentCode:'', MainCaption: '', SubCaption: '', SubPosition: 1, Active: true };
                    
            }
        }
        var intializeLookUp = function () {
            getCompanies();
            getMainCaptions();
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
            validator.ValidateModel(vm.unMappedGL, unmappedglRules);
            vm.viewModelHelper.modelIsValid = vm.unMappedGL.isValid;
            vm.viewModelHelper.modelErrors = vm.unMappedGL.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/glmapping/updateglMapping', vm.unMappedGL,
               function (result) {
                   toastr.success('GL mapped successfully.', 'Fintrak');
                   $state.go('finstat-unmappedgl-list');
               },
               function (result) {
                   toastr.error('Fail to mapped GL.', 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.unMappedGL.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
                
        }

        vm.cancel = function () {
            $state.go('finstat-unmappedgl-list');
        };

 vm.onMainCaptionChanged = function (captionCode) {
            getSubCaptionbyCaptionCode(captionCode);
        }

        vm.onSubCaptionChanged = function (caption) {
            vm.unMappedGL.SubCaption = caption.Name;
        }

        vm.onSubCaption1Changed = function (caption) {
            vm.unMappedGL.SubCaption1 = caption.Name;
        }

        vm.onSubCaption2Changed = function (caption) {
            vm.unMappedGL.SubCaption2 = caption.Name;
        }

        vm.onSubCaption3Changed = function (caption) {
            vm.unMappedGL.SubCaption3 = caption.Name;
        }

        vm.onSubCaption4Changed = function (caption) {
            vm.unMappedGL.SubCaption4 = caption.Name;
        }

        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                 function (result) {
                     vm.companies = result.data;
                     toastr.success('Companies loaded successfully.', 'Fintrak');
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getMainCaptions = function () {
            vm.viewModelHelper.apiGet('api/registry/getmainCaptions', null,
                 function (result) {
                     vm.mainCaptions = result.data;
                     toastr.success('Main Captions loaded successfully.', 'Fintrak');
                 },
                 function (result) {
                     toastr.error('Fail to load main captions.', 'Fintrak');
                 }, null);
        }

        var getSubCaptions = function () {
            vm.viewModelHelper.apiGet('api/glmapping/getsubcaptions/0/0', null,
                 function (result) {
                     vm.subCaptions = result.data;
                     toastr.success('Sub Captions level 0 loaded successfully.', 'Fintrak');
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 0.', 'Fintrak');
                 }, null);
        }

        var getSubCaption1s = function () {
            vm.viewModelHelper.apiGet('api/glmapping/getsubcaptions/1/0', null,
                 function (result) {
                     vm.subCaption1s = result.data;
                     toastr.success('Sub Captions level 1 loaded successfully.', 'Fintrak');
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 1.', 'Fintrak');
                 }, null);
        }

        var getSubCaption2s = function () {
            vm.viewModelHelper.apiGet('api/glmapping/getsubcaptions/2/0', null,
                 function (result) {
                     vm.subCaption2s = result.data;
                     toastr.success('Sub Captions level 2 loaded successfully.', 'Fintrak');
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 2.', 'Fintrak');
                 }, null);
        }

        var getSubCaption3s = function () {
            vm.viewModelHelper.apiGet('api/glmapping/getsubcaptions/3/0', null,
                 function (result) {
                     vm.subCaption3s = result.data;
                     toastr.success('Sub Captions level 3 loaded successfully.', 'Fintrak');
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 3.', 'Fintrak');
                 }, null);
        }

        var getSubCaption4s = function () {
            vm.viewModelHelper.apiGet('api/glmapping/getsubcaptions/4/0', null,
                 function (result) {
                     vm.subCaption4s = result.data;
                     toastr.success('Sub Captions level 4 loaded successfully.', 'Fintrak');
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 4.', 'Fintrak');
                 }, null);
        }

 var getSubCaptionbyCaptionCode = function (captionCode) {
            vm.viewModelHelper.apiGet('api/glmapping/getsubcaptions/' + '/' + '5' + '/' + captionCode, null,
                 function (result) {
                     vm.subCaptions = result.data;
                 },
                 function (result) {
                     vm.subCaptions = [];
                     toastr.error('Fail to load SubCaption.', 'Fintrak');

                 }, null);
        }
           
        setupRules();
        initialize(); 
    }
}());
