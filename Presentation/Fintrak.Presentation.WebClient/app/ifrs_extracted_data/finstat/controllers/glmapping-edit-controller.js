
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("GLMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        GLMappingEditController]);

    function GLMappingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'glmapping-edit-view';
        vm.viewName = 'GL Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.glMapping = {};


        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.checked = false;
        vm.Status = 0;


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

        var glmappingRules = [];

        var setupRules = function () {

            glmappingRules.push(new validator.PropertyRule("GLCode", {
                required: { message: "GLCode is required" }
            }));

            glmappingRules.push(new validator.PropertyRule("GLDescription", {
                required: { message: "Description is required" }
            }));

            glmappingRules.push(new validator.PropertyRule("CaptionCode", {
                required: { message: "MainCaption is required" }
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
                    vm.viewModelHelper.apiGet('api/glmapping/getglmapping/' + $stateParams.glmappingId, null,
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
                    vm.glMapping = { GLCode: '', GLDescription: '', MainCaption: '', SubCaption: '', SubPosition: 1, Active: true };
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


            validator.ValidateModel(vm.glMapping, glmappingRules);
            vm.viewModelHelper.modelIsValid = vm.glMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.glMapping.errors;

            vm.Indata = { 'GlMapping': vm.glMapping, 'Status': vm.Status };

            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/glmapping/updateglmapping', vm.Indata,

               function (result) {

                   $state.go('finstat-glmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }

            else {
                vm.viewModelHelper.modelErrors = vm.glMapping.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.checkme = function (event) {


            if (event.target.checked) {
                vm.Status = 1;
                alert('True');
            } else {
                vm.Status = 0;
                alert('false');
            }
            
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/glmapping/deleteglmapping', vm.glMapping.GLMappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-glmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('finstat-glmapping-list');
        };

        vm.onMainCaptionChanged = function (captionCode) {
            getSubCaptionbyCaptionCode(captionCode);
        }

        vm.onSubCaptionChanged = function (caption) {
            //vm.glMapping.SubCaption = caption.Name;
            getSubCaption1byCaptionCode(caption)
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

        var getMainCaptions = function () {
            vm.viewModelHelper.apiGet('api/registry/getmainCaptions', null,
                 function (result) {
                     vm.mainCaptions = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load main captions.', 'Fintrak');
                 }, null);
        }

        var getSubCaptions = function () {
            vm.viewModelHelper.apiGet('api/glmapping/getsubcaptions/0/0', null,
                 function (result) {
                     vm.subCaptions = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions.', 'Fintrak');
                 }, null);
        }
        //'api/glmapping/getsubcaptions/' + '/' + '5' + '/' + captionCode
        //var getSubCaption1s = function () {
        //    vm.viewModelHelper.apiGet('api/glmapping/getsubcaptions/1/0', null,
        //         function (result) {
        //             vm.subCaption1s = result.data;
        //         },
        //         function (result) {
        //             toastr.error('Fail to load sub captions level 1.', 'Fintrak');
        //         }, null);
        //}

        var getSubCaption1s = function (caption) {
            vm.viewModelHelper.apiGet('api/glmapping/getsubsubcaptions/' + caption, null,
                 function (result) {
                     vm.subCaption1s = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 1.', 'Fintrak');
                 }, null);
        }

        var getSubCaption2s = function () {
            vm.viewModelHelper.apiGet('api/glmapping/getsubcaptions/2/0', null,
                 function (result) {
                     vm.subCaption2s = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 2.', 'Fintrak');
                 }, null);
        }

        var getSubCaption3s = function () {
            vm.viewModelHelper.apiGet('api/glmapping/getsubcaptions/3/0', null,
                 function (result) {
                     vm.subCaption3s = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 3.', 'Fintrak');
                 }, null);
        }

        var getSubCaption4s = function () {
            vm.viewModelHelper.apiGet('api/glmapping/getsubcaptions/4/0', null,
                 function (result) {
                     vm.subCaption4s = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 4.', 'Fintrak');
                 }, null);
        }

        //+ vm.startDate.toDateString() + '/' + vm.endDate.toDateString(), null,
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


        var getSubCaption1byCaptionCode = function (caption) {
            vm.viewModelHelper.apiGet('api/glmapping/getsubsubcaptions/' + caption, null,
                 function (result) {
                     vm.subCaption1s = result.data;
                 },
                 function (result) {
                     vm.subCaption1s = [];
                     toastr.error('Fail to load SubCaption.', 'Fintrak');

                 }, null);
        }



        setupRules();
        initialize();
    }
}());

