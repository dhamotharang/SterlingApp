
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("GLMappingMgtEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        GLMappingMgtEditController]);

    function GLMappingMgtEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'glmappingmgt-edit-view';
        vm.viewName = 'Management GL Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.glMappingMgt = {};


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

        var glmappingmgtRules = [];

        var setupRules = function () {

            glmappingmgtRules.push(new validator.PropertyRule("GLCode", {
                required: { message: "GLCode is required" }
            }));

            glmappingmgtRules.push(new validator.PropertyRule("GLDescription", {
                required: { message: "Description is required" }
            }));

            glmappingmgtRules.push(new validator.PropertyRule("CaptionCode", {
                required: { message: "MainCaption is required" }
            }));

            glmappingmgtRules.push(new validator.PropertyRule("SubCaption", {
                required: { message: "SubCaption is required" }
            }));

            //glmappingmgtRules.push(new validator.PropertyRule("SubPosition", {
            //    notZero: { message: "Sub position is required" }
            //}));

            glmappingmgtRules.push(new validator.PropertyRule("CompanyCode", {
                required: { message: "Company is required" }
            }));


        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.glmappingmgtId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/glmappingmgt/getglMapping/' + $stateParams.glmappingmgtId, null,
                   function (result) {
                       vm.glMappingMgt = result.data;
                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.glMappingMgt = { GLCode: '', GLDescription: '', MainCaption: '', SubCaption: '', SubPosition: 1, Active: true };
            }
        }

        var intializeLookUp = function () {
            getCompanies();
            getMainCaptions();
            getSubCaptions();
            getSubCaption1s();
         //   getSubCaption2s();
         //   getSubCaption3s();
         //   getSubCaption4s();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate


            validator.ValidateModel(vm.glMappingMgt, glmappingmgtRules);
            vm.viewModelHelper.modelIsValid = vm.glMappingMgt.isValid;
            vm.viewModelHelper.modelErrors = vm.glMappingMgt.errors;

            vm.Indata = { 'GLMappingMgt': vm.glMappingMgt, 'Status': vm.Status };

            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/glmappingmgt/updateglmapping', vm.Indata,

               function (result) {

                   $state.go('finstat-glmappingmgt-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }

            else {
                vm.viewModelHelper.modelErrors = vm.glMappingMgt.errors;

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
               // alert('True');
            } else {
                vm.Status = 0;
              //  alert('false');
            }
            
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/glmappingmgt/deleteglmapping', vm.glMappingMgt.GLMappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-glmappingmgt-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('finstat-glmappingmgt-list');
        };

        vm.onMainCaptionChanged = function (captionCode) {
            getSubCaptionbyCaptionCode(captionCode);
        }

        vm.onSubCaptionChanged = function (caption) {
            //vm.glMappingMgt.SubCaption = caption.Name;
            getSubCaption1byCaptionCode(caption)
        }

        vm.onSubCaption1Changed = function (caption) {
            vm.glMappingMgt.SubCaption1 = caption.Name;
        }

        vm.onSubCaption2Changed = function (caption) {
            vm.glMappingMgt.SubCaption2 = caption.Name;
        }

        vm.onSubCaption3Changed = function (caption) {
            vm.glMappingMgt.SubCaption3 = caption.Name;
        }

        vm.onSubCaption4Changed = function (caption) {
            vm.glMappingMgt.SubCaption4 = caption.Name;
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
            vm.viewModelHelper.apiGet('api/registry/getmainCaptions/' + 2, null,
                 function (result) {
                     vm.mainCaptions = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load main captions.', 'Fintrak');
                 }, null);
        }

        var getSubCaptions = function () {
            vm.viewModelHelper.apiGet('api/glmappingmgt/getsubcaptions/0/0', null,
                 function (result) {
                     vm.subCaptions = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions.', 'Fintrak');
                 }, null);
        }
        //'api/glmappingmgt/getsubcaptions/' + '/' + '5' + '/' + captionCode
        //var getSubCaption1s = function () {
        //    vm.viewModelHelper.apiGet('api/glmappingmgt/getsubcaptions/1/0', null,
        //         function (result) {
        //             vm.subCaption1s = result.data;
        //         },
        //         function (result) {
        //             toastr.error('Fail to load sub captions level 1.', 'Fintrak');
        //         }, null);
        //}

        var getSubCaption1s = function (caption) {
            vm.viewModelHelper.apiGet('api/glmappingmgt/getsubsubcaptions/' + caption, null,
                 function (result) {
                     vm.subCaption1s = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 1.', 'Fintrak');
                 }, null);
        }

        var getSubCaption2s = function () {
            vm.viewModelHelper.apiGet('api/glmappingmgt/getsubcaptions/2/0', null,
                 function (result) {
                     vm.subCaption2s = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 2.', 'Fintrak');
                 }, null);
        }

        var getSubCaption3s = function () {
            vm.viewModelHelper.apiGet('api/glmappingmgt/getsubcaptions/3/0', null,
                 function (result) {
                     vm.subCaption3s = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 3.', 'Fintrak');
                 }, null);
        }

        var getSubCaption4s = function () {
            vm.viewModelHelper.apiGet('api/glmappingmgt/getsubcaptions/4/0', null,
                 function (result) {
                     vm.subCaption4s = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub captions level 4.', 'Fintrak');
                 }, null);
        }

        //+ vm.startDate.toDateString() + '/' + vm.endDate.toDateString(), null,
        var getSubCaptionbyCaptionCode = function (captionCode) {
            vm.viewModelHelper.apiGet('api/glmappingmgt/getsubcaptions/' + '/' + '5' + '/' + captionCode, null,
                 function (result) {
                     vm.subCaptions = result.data;
                 },
                 function (result) {
                     vm.subCaptions = [];
                     toastr.error('Fail to load SubCaption.', 'Fintrak');

                 }, null);
        }


        var getSubCaption1byCaptionCode = function (caption) {
            vm.viewModelHelper.apiGet('api/glmappingmgt/getsubsubcaptions/' + caption, null,
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

