/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FacRatingEditController",
            ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                FacRatingEditController]);

    function FacRatingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS Core';
        vm.view = 'facrating-edit-view';
        vm.viewName = 'Homogeneous Classification Group';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.showfields = false;
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.facratingRules = [];

        var setupRules = function () {

            vm.facratingRules.push(new validator.PropertyRule("HC!",
                {
                    required: { message: "HC1 is required" }
                }));

            vm.facratingRules.push(new validator.PropertyRule("HC2",
                {
                    required: { message: "HC2 is required" }
                }));

        };

        var initialize = function () {
            if (vm.init === false) {

                if ($stateParams.Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/facrating/getfacrating/' + $stateParams.Id, null,
                        function (result) {
                            vm.facrating = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.facrating = { HC1: '', HC2: '', Active: true };
            }
        }



        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.facrating, vm.facratingRules);
            vm.viewModelHelper.modelIsValid = vm.facrating.isValid;
            vm.viewModelHelper.modelErrors = vm.facrating.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/facrating/updatefacrating', vm.facrating,
                    function (result) {

                        $state.go('ifrs-facrating-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.facrating.errors;

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
                vm.viewModelHelper.apiPost('api/facrating/deletefacrating', vm.facrating.ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('ifrs-facrating-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }



        vm.cancel = function () {
            $state.go('ifrs-facrating-list');
        };


        setupRules();
        initialize();



    }
}());
