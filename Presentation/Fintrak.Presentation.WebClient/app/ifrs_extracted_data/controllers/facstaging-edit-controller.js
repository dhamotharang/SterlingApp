/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FacStagingEditController",
            ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                FacStagingEditController]);

    function FacStagingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS Core';
        vm.view = 'facstaging-edit-view';
        vm.viewName = 'Homogeneous Classification Group';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.showfields = false;
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.facstagingRules = [];

        var setupRules = function () {

            vm.facstagingRules.push(new validator.PropertyRule("HC!",
                {
                    required: { message: "HC1 is required" }
                }));

            vm.facstagingRules.push(new validator.PropertyRule("HC2",
                {
                    required: { message: "HC2 is required" }
                }));

        };

        var initialize = function () {
            if (vm.init === false) {

                if ($stateParams.Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/facstaging/getfacstaging/' + $stateParams.Id, null,
                        function (result) {
                            vm.facstaging = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.facstaging = { HC1: '', HC2: '', Active: true };
            }
        }



        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.facstaging, vm.facstagingRules);
            vm.viewModelHelper.modelIsValid = vm.facstaging.isValid;
            vm.viewModelHelper.modelErrors = vm.facstaging.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/facstaging/updatefacstaging', vm.facstaging,
                    function (result) {

                        $state.go('ifrs-facstaging-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.facstaging.errors;

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
                vm.viewModelHelper.apiPost('api/facstaging/deletefacstaging', vm.facstaging.ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('ifrs-facstaging-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }



        vm.cancel = function () {
            $state.go('ifrs-facstaging-list');
        };


        setupRules();
        initialize();



    }
}());
