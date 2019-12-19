/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("GLDefinitionEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        GLDefinitionEditController]);

    function GLDefinitionEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'gldefinition-edit-view';
        vm.viewName = 'GL Definitions';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.glDefinition = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var gldefinitionRules = [];

        var setupRules = function () {

            gldefinitionRules.push(new validator.PropertyRule("GL_Code", {
                required: { message: "GL Code No is required" }
            }));

            gldefinitionRules.push(new validator.PropertyRule("Description", {
                required: { message: "Description is required" }
            }));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.gldefinitionId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/gldefinition/getgldefinition/' + $stateParams.gldefinitionId, null,
                   function (result) {
                       vm.glDefinition = result.data;

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.glDefinition = { GL_Code :'',Description:'', Active: true };
            }
        }

        //var intializeLookUp = function () {
        //    getTeams(1);
        //}

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.glDefinition, gldefinitionRules);
            vm.viewModelHelper.modelIsValid = vm.glDefinition.isValid;
            vm.viewModelHelper.modelErrors = vm.glDefinition.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/gldefinition/updategldefinition', vm.glDefinition,
               function (result) {
                   
                   $state.go('mpr-gldefinition-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.glDefinition.errors;

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
                vm.viewModelHelper.apiPost('api/gldefinition/deletegldefinition', vm.glDefinition.GLDefinitionId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-gldefinition-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-gldefinition-list');
        };

       

        setupRules();
        initialize(); 
    }
}());
