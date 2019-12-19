/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("DefaultuserEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        DefaultuserEditController]);

    function DefaultuserEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'defaultuser-edit-view';
        vm.viewName = 'Default User';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.defaultuser = {};
        vm.usersetups = [];
        vm.solutions = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var defaultuserRules = [];

        var setupRules = function () {
          
            defaultuserRules.push(new validator.PropertyRule("LoginID", {
                required: { message: "LoginID is required" }
            }));

            defaultuserRules.push(new validator.PropertyRule("SolutionId", {
                required: { message: "SolutionId is required" }
            }));           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.defaultuserId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/defaultuser/getdefaultUser/' + $stateParams.defaultuserId, null,
                   function (result) {
                       vm.defaultuser = result.data;

                       initialView();
                       vm.init = true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.defaultuser = { LoginID: '', SolutionId:0, Active: true };
            }
        }


        var intializeLookUp = function () {
            getUsersetups();
            getSolutions();
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.defaultuser, defaultuserRules);
            vm.viewModelHelper.modelIsValid = vm.defaultuser.isValid;
            vm.viewModelHelper.modelErrors = vm.defaultuser.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/defaultuser/updatedefaultuser', vm.defaultuser,
               function (result) {
                   
                   $state.go('core-defaultuser-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.defaultuser.errors;

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
                vm.viewModelHelper.apiPost('api/defaultuser/deletedefaultuser', vm.defaultuser.DefaultUserId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-defaultuser-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-defaultuser-list');
        };

        var getUsersetups = function () {
            vm.viewModelHelper.apiGet('api/account/getallaccount', null,
                 function (result) {
                     vm.usersetups = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getSolutions = function () {
            vm.viewModelHelper.apiGet('api/solution/availablesolutions', null,
                 function (result) {
                     vm.solutions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
