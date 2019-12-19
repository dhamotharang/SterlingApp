/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FacOBEStagingEditController",
            ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                FacOBEStagingEditController]);

    function FacOBEStagingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS Core';
        vm.view = 'facobestaging-edit-view';
        vm.viewName = 'Homogeneous Classification Group';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.showfields = false;
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.facobestagingRules = [];

        var setupRules = function () {

            vm.facobestagingRules.push(new validator.PropertyRule("HC!",
                {
                    required: { message: "HC1 is required" }
                }));

            vm.facobestagingRules.push(new validator.PropertyRule("HC2",
                {
                    required: { message: "HC2 is required" }
                }));

        };

        var initialize = function () {
            if (vm.init === false) {

                if ($stateParams.Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/facobestaging/getfacobestaging/' + $stateParams.Id, null,
                        function (result) {
                            vm.facobestaging = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.facobestaging = { HC1: '', HC2: '', Active: true };
            }
        }



        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.facobestaging, vm.facobestagingRules);
            vm.viewModelHelper.modelIsValid = vm.facobestaging.isValid;
            vm.viewModelHelper.modelErrors = vm.facobestaging.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/facobestaging/updatefacobestaging', vm.facobestaging,
                    function (result) {

                        $state.go('ifrs-facobestaging-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.facobestaging.errors;

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
                vm.viewModelHelper.apiPost('api/facobestaging/deletefacobestaging', vm.facobestaging.ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('ifrs-facobestaging-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }



        vm.cancel = function () {
            $state.go('ifrs-facobestaging-list');
        };


        setupRules();
        initialize();



    }
}());
