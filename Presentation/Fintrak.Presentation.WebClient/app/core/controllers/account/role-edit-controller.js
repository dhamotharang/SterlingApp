/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RoleEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        RoleEditController]);

    function RoleEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Extraction & Process';
        vm.view = 'role-edit-view';
        vm.viewName = 'Run Date';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.role = {};

        vm.solutions = [];
        vm.roleTypes = [
           { Id: 1, Name: 'Application' },
            { Id: 2, Name: 'Report' }
        ];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var roleRules = [];

        vm.openedRunDate = false;

        var setupRules = function () {
          
            roleRules.push(new validator.PropertyRule("SolutionId", {
                notZero: { message: "Solution is required" }
            }));

            roleRules.push(new validator.PropertyRule("Type", {
                notZero: { message: "Role type is required" }
            }));

            roleRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intialiazeLookUp();

                if ($stateParams.roleId > 0) {
                    vm.showPeriod = true;
                    vm.viewModelHelper.apiGet('api/role/getrole/' + $stateParams.roleId, null,
                   function (result) {
                       vm.role = result.data;

                       initialView();

                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.role = {Name:'', SolutionId: 0, Type: 1, Active: true };
            }
        }

        var intialiazeLookUp = function () {
            
            getSolutions();
        }

        var initialView = function () {
        
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.role, roleRules);
            vm.viewModelHelper.modelIsValid = vm.role.isValid;
            vm.viewModelHelper.modelErrors = vm.role.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/role/updaterole', vm.role,
               function (result) {
                   
                   $state.go('core-role-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.role.errors;

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
                vm.viewModelHelper.apiPost('api/role/deleterole', vm.role.RoleId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-role-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-role-list');
        };

        

        var getSolutions = function () {
            vm.viewModelHelper.apiGet('api/solution/availablesolutions', null,
                 function (result) {
                     vm.solutions = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }


        setupRules();
        initialize(); 
    }
}());
