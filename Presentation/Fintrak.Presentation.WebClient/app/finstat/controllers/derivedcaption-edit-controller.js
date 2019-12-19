/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("DerivedCaptionEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        DerivedCaptionEditController]);

    function DerivedCaptionEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'derivedcaption-edit-view';
        vm.viewName = 'Derived Caption';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.derivedCaption = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.captions = [];

        var derivedcaptionRules = [];

        var setupRules = function () {
          
            derivedcaptionRules.push(new validator.PropertyRule("Caption", {
                required: { message: "Caption is required" }
            }));

            derivedcaptionRules.push(new validator.PropertyRule("DependencyCaption", {
                required: { message: "Dependency Caption is required" }
            }));

             derivedcaptionRules.push(new validator.PropertyRule("Factor", {
                 mostBePercentage: { message: "Factor must be in percentage" }
            }));
            

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.derivedcaptionId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/derivedcaption/getderivedcaption/' + $stateParams.derivedcaptionId, null,
                   function (result) {
                       vm.derivedCaption = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.derivedCaption = { Caption: '', DependencyCaption: '', Factor: 0, Active: true };
            }
        }

        var intializeLookUp = function () {
            getCaptions();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.derivedCaption, derivedcaptionRules);
            vm.viewModelHelper.modelIsValid = vm.derivedCaption.isValid;
            vm.viewModelHelper.modelErrors = vm.derivedCaption.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/derivedcaption/updatederivedcaption', vm.derivedCaption,
               function (result) {
                   
                   $state.go('finstat-derivedcaption-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.derivedCaption.errors;

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
                vm.viewModelHelper.apiPost('api/derivedcaption/deletederivedcaption', vm.derivedCaption.DerivedCaptionId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-derivedcaption-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('finstat-derivedcaption-list');
        };

        var getCaptions = function () {
            vm.viewModelHelper.apiGet('api/registry/getmaincaptions', null,
                 function (result) {
                     vm.captions = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load main captions.', 'Fintrak');
                 }, null);
        }
       
        setupRules();
        initialize(); 
    }
}());
