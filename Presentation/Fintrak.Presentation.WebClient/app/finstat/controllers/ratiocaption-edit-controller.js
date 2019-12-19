/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RatioCaptionEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        RatioCaptionEditController]);

    function RatioCaptionEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Finstat';
        vm.view = 'ratiocaption-edit-view';
        vm.viewName = 'RatioCaption';
        vm.status = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ratiocaptions = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ratiocaptionRules = [];

        var setupRules = function () {

            ratiocaptionRules.push(new validator.PropertyRule("RatioCategory", {
                required: { message: "RatioCategory is required" }
            }));

            ratiocaptionRules.push(new validator.PropertyRule("RatioCaptionOpt", {
                required: { message: "RatioCaptionOpt is required" }
            }));

            ratiocaptionRules.push(new validator.PropertyRule("Position", {
                required: { message: "Position is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.RatioCaptionID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ratiocaption/getratiocaption/' + $stateParams.RatioCaptionID, null,
                   function (result) {
                       vm.ratiocaptions = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.ratiocaptions = {
                        RatioCategory: '',
                        RatioCaptionOpt: '',
                        Position: '',
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
            validator.ValidateModel(vm.ratiocaptions, ratiocaptionRules);
            vm.viewModelHelper.modelIsValid = vm.ratiocaptions.isValid;
            vm.viewModelHelper.modelErrors = vm.ratiocaptions.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/ratiocaption/updateratiocaption', vm.ratiocaptions,
               function (result) {                   
                   $state.go('finstat-ratiocaption-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.ratiocaptions.errors;

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
                vm.viewModelHelper.apiPost('api/ratiocaption/deleteratiocaption', vm.ratiocaptions.RatioCaptionID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-ratiocaption-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('finstat-ratiocaption-list');
        };

        
        setupRules();
        initialize();

    }
}());
