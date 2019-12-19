/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FacClassConsolidatedEditController",
            ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                FacClassConsolidatedEditController]);

    function FacClassConsolidatedEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS Core';
        vm.view = 'facclassconsolidated-edit-view';
        vm.viewName = 'Homogeneous Classification Group';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.showfields = false;
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.facclassconsolidatedRules = [];

        var setupRules = function () {

            vm.facclassconsolidatedRules.push(new validator.PropertyRule("HC!",
                {
                    required: { message: "HC1 is required" }
                }));

            vm.facclassconsolidatedRules.push(new validator.PropertyRule("HC2",
                {
                    required: { message: "HC2 is required" }
                }));

        };

        var initialize = function () {
            if (vm.init === false) {

                if ($stateParams.Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/facclassconsolidated/getfacclassconsolidated/' + $stateParams.Id, null,
                        function (result) {
                            vm.facclassconsolidated = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.facclassconsolidated = { HC1: '', HC2: '', Active: true };
            }
        }



        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.facclassconsolidated, vm.facclassconsolidatedRules);
            vm.viewModelHelper.modelIsValid = vm.facclassconsolidated.isValid;
            vm.viewModelHelper.modelErrors = vm.facclassconsolidated.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/facclassconsolidated/updatefacclassconsolidated', vm.facclassconsolidated,
                    function (result) {

                        $state.go('ifrs-facclassconsolidated-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.facclassconsolidated.errors;

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
                vm.viewModelHelper.apiPost('api/facclassconsolidated/deletefacclassconsolidated', vm.facclassconsolidated.ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('ifrs-facclassconsolidated-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }



        vm.cancel = function () {
            $state.go('ifrs-facclassconsolidated-list');
        };


        setupRules();
        initialize();



    }
}());
