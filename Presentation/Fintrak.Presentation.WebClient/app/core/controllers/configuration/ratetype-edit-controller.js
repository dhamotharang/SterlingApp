/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RateTypeEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        RateTypeEditController]);

    function RateTypeEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'ratetype-edit-view';
        vm.viewName = 'Rate Type';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.rateType = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var rateTypeRules = [];

        var setupRules = function () {
          
            rateTypeRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initialLookUp();

                if ($stateParams.ratetypeId !== 0) {
                    vm.showPeriod = true;
                    vm.viewModelHelper.apiGet('api/rateType/getrateType/' + $stateParams.ratetypeId, null,
                   function (result) {
                       vm.rateType = result.data;

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.rateType = { Name: '', Active: true };
            }
        }

        var initialLookUp = function () {
            
        }

        var initialView = function () {
  
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.rateType, rateTypeRules);
            vm.viewModelHelper.modelIsValid = vm.rateType.isValid;
            vm.viewModelHelper.modelErrors = vm.rateType.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/rateType/updaterateType', vm.rateType,
               function (result) {
                   
                   $state.go('core-ratetype-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.rateType.errors;

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
                vm.viewModelHelper.apiPost('api/rateType/deleterateType', vm.rateType.RateTypeId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-ratetype-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-ratetype-list');
        };

        setupRules();
        initialize(); 
    }
}());
