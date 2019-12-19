/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("DatabaseEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        DatabaseEditController]);

    function DatabaseEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'database-edit-view';
        vm.viewName = 'Database';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.database = {};
        vm.companies = [];
        vm.databaseRates = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var databaseRules = [];

        var setupRules = function () {
          
            databaseRules.push(new validator.PropertyRule("DatabaseName", {
                required: { message: "DatabaseName is required" }
            }));

            databaseRules.push(new validator.PropertyRule("ServerName", {
                required: { message: "ServerName is required" }
            }));

            databaseRules.push(new validator.PropertyRule("UserName", {
                required: { message: "UserName is required" }
            }));

            databaseRules.push(new validator.PropertyRule("Password", {
                required: { message: "Password is required" }
            }));

            databaseRules.push(new validator.PropertyRule("IntegratedSecurity", {
                required: { message: "IntegratedSecurity is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.databaseId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/database/getdatabase/' + $stateParams.databaseId, null,
                   function (result) {
                       vm.database = result.data;

                       initialView();
                       vm.init = true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.database = { DatabaseName: '', ServerName: '', UserName: '', Password: '', IntegratedSecurity: '',SolutionId:0, Active: true };
            }
        }


        var intializeLookUp = function () {
            getCompanies();
            getSolutions();
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.database, databaseRules);
            vm.viewModelHelper.modelIsValid = vm.database.isValid;
            vm.viewModelHelper.modelErrors = vm.database.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/database/updatedatabase', vm.database,
               function (result) {
                   
                   $state.go('core-general-edit');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.database.errors;

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
                vm.viewModelHelper.apiPost('api/database/deletedatabase', vm.database.DatabaseId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-general-edit');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-general-edit');
        };

        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                 function (result) {
                     vm.companies = result.data;
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
