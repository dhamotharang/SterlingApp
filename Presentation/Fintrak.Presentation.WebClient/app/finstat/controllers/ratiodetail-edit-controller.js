/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RatioDetailEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        RatioDetailEditController]);

    function RatioDetailEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ratiodetail-edit-view';
        vm.viewName = 'RatioDetail';
        vm.status = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ratiodetails = {};
        vm.mainCaptions = [];
        vm.subCaptions = [];

        vm.ratiodetails.ReportType = 1;

        vm.captionTypes = [
            { value: 1, name: 'Registry' },
            { value: 2, name: 'Mgt Registry' },
            { value: 3, name: 'Other' }
        ];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ratiodetailRules = [];

        var setupRules = function () {

            ratiodetailRules.push(new validator.PropertyRule("RatioCaption", {
                required: { message: "RatioCaption is required" }
            }));

            ratiodetailRules.push(new validator.PropertyRule("ReportCaption", {
                required: { message: "ReportCaption is required" }
            }));

            ratiodetailRules.push(new validator.PropertyRule("ReportSubCaption", {
                required: { message: "ReportSubCaption is required" }
            }));

            ratiodetailRules.push(new validator.PropertyRule("ReportSubSubCaption", {
                required: { message: "ReportSubSubCaption is required" }
            }));

            ratiodetailRules.push(new validator.PropertyRule("DivisorType", {
                required: { message: "DivisorType is required" }
            }));
            ratiodetailRules.push(new validator.PropertyRule("Multiplier", {
                required: { message: "Multiplier is required" }
            }));

            ratiodetailRules.push(new validator.PropertyRule("PreviousType", {
                required: { message: "PreviousType is required" }
            }));

            ratiodetailRules.push(new validator.PropertyRule("Annualised", {
                required: { message: "Annualised is required" }
            }));

            ratiodetailRules.push(new validator.PropertyRule("ReportType", {
                required: { message: "ReportType is required" }
            }));

            ratiodetailRules.push(new validator.PropertyRule("Position", {
                required: { message: "Position is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.RatioID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ratiodetail/getratiodetail/' + $stateParams.RatioID, null,
                   function (result) {
                       vm.ratiodetails = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.ratiodetails = {
                        RatioCaption: '',
                        ReportType: '',
                        ReportCaption: '',
                        ReportSubCaption: '',
                        ReportSubSubCaption: '',
                        DivisorType: '',
                        Multiplier: 0,
                        PreviousType: '',
                        Position: 1,
                        Annualised: false,
                        Active: true
                    };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            getMainCaptions();
            getSubCaptionbyCaptionCode(vm.ratiodetails.CaptionCode);
            //getSubCaption1byCaptionCode(vm.glMapping.SubCaption);
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ratiodetails, ratiodetailRules);
            vm.viewModelHelper.modelIsValid = vm.ratiodetails.isValid;
            vm.viewModelHelper.modelErrors = vm.ratiodetails.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/ratiodetail/updateratiodetail', vm.ratiodetails,
               function (result) {
                   
                   $state.go('finstat-ratiodetail-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else{
                vm.viewModelHelper.modelErrors = vm.ratiodetails.errors;

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
                vm.viewModelHelper.apiPost('api/ratiodetail/deleteratiodetail', vm.ratiodetails.InstrumentID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-ratiodetail-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('finstat-ratiodetail-list');
        };

        vm.onCaptionTypeChanged = function () {
            getMainCaptions();
        };

        vm.onMainCaptionChanged = function (captionCode) {
            getSubCaptionbyCaptionCode(captionCode);
        };

        //vm.onSubCaptionChanged = function (caption) {
        //    //vm.glMapping.SubCaption = caption.Name;
        //    getSubCaption1byCaptionCode(caption);
        //};

        var getMainCaptions = function () {
            vm.viewModelHelper.apiGet('api/registry/getmainCaptions/' + vm.ratiodetails.ReportType,
                null,
                function (result) {
                    vm.mainCaptions = result.data;
                },
                function (result) {
                    toastr.error('Fail to load main captions.', 'Fintrak');
                },
                null);
        };

        var getSubCaptionbyCaptionCode = function (captionCode) {
            vm.viewModelHelper.apiGet('api/glmapping/getsubcaptions/' + '/' + '5' + '/' + captionCode,
                null,
                function (result) {
                    vm.subCaptions = result.data;
                },
                function (result) {
                    vm.subCaptions = [];
                    toastr.error('Fail to load SubCaption.', 'Fintrak');

                },
                null);
        };
        
        setupRules();
        initialize(); 
    }
}());
