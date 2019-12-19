/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ServiceseEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ServiceseEditController]);

    function ServiceseEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'servicese-edit-view';
        vm.viewName = 'Services';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.servicese = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var serviceseRules = [];

        var setupRules = function () {

            serviceseRules.push(new validator.PropertyRule("ServicesCode", {
                required: { message: "ServicesCode is required" }
            }));

            serviceseRules.push(new validator.PropertyRule("Service", {
                required: { message: "Service is required" }
            }));

            serviceseRules.push(new validator.PropertyRule("Weight", {
                required: { message: "Weight is required" }
            }));

            serviceseRules.push(new validator.PropertyRule("ServiceType", {
                required: { message: "ServiceType is required" }
            }));

            serviceseRules.push(new validator.PropertyRule("ServiceCat", {
                required: { message: "ServiceCat is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.servicesId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/servicese/getservicese/' + $stateParams.servicesId, null,
                   function (result) {
                       vm.servicese = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.servicese = { ServicesCode: '', Service: '', Weight: '', ServiceType: '', ServiceCat: '', Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.servicese, serviceseRules);
            vm.viewModelHelper.modelIsValid = vm.servicese.isValid;
            vm.viewModelHelper.modelErrors = vm.servicese.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/servicese/updateservicese', vm.servicese,
               function (result) {

                   $state.go('mpr-servicese-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.servicese.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/servicese/deleteservicese', vm.servicese.ServicesId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-servicese-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-servicese-list');
        };

        setupRules();
        initialize();
    }
}());
