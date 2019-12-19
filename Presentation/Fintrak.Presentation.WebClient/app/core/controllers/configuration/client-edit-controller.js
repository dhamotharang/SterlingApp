/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ClientEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ClientEditController]);

    function ClientEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'client-edit-view';
        vm.viewName = 'Client';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.client = {};
        //vm.companies = [];
        vm.clientRates = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var clientRules = [];

        var setupRules = function () {
          
            clientRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            clientRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.clientId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/client/getclient/' + $stateParams.clientId, null,
                   function (result) {
                       vm.client = result.data;

                       initialView();
                       vm.init = true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.client = { Code: '', Name: '',Active: true };
            }
        }


        var intializeLookUp = function () {
            //getCompanies();
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.client, clientRules);
            vm.viewModelHelper.modelIsValid = vm.client.isValid;
            vm.viewModelHelper.modelErrors = vm.client.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/client/updateclient', vm.client,
               function (result) {
                   
                   $state.go('core-client-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.client.errors;

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
                vm.viewModelHelper.apiPost('api/client/deleteclient', vm.client.ClientId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-client-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-client-list');
        };

        //var getCompanies = function () {
        //    vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
        //         function (result) {
        //             vm.companies = result.data;
        //             vm.init === true;

        //         },
        //         function (result) {

        //         }, null);
        //}

        setupRules();
        initialize(); 
    }
}());
