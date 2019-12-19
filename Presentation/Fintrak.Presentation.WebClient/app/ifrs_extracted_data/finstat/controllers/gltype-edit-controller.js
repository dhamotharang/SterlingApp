/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("GLTypeEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        GLTypeEditController]);

    function GLTypeEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'gltype-edit-view';
        vm.viewName = 'GL Type';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.glType = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var gltypeRules = [];

        var setupRules = function () {
          
            
            gltypeRules.push(new validator.PropertyRule("Name", {
                required: { message: "GL Type is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.gltypeId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/gltype/getgltype/' + $stateParams.gltypeId, null,
                   function (result) {
                       vm.glType = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.glType = { Name:'', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.glType, gltypeRules);
            vm.viewModelHelper.modelIsValid = vm.glType.isValid;
            vm.viewModelHelper.modelErrors = vm.glType.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/gltype/updategltype', vm.glType,
               function (result) {
                   
                   $state.go('finstat-gltype-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.glType.errors;

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
                vm.viewModelHelper.apiPost('api/gltype/deletegltype', vm.glType.GLTypeId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-gltype-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('finstat-gltype-list');
        };

        setupRules();
        initialize(); 
    }
}());
