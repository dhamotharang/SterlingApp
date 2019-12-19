/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CollateralDetailsEditController",
            ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                CollateralDetailsEditController]);

    function CollateralDetailsEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS Core';
        vm.view = 'collateraldetails-edit-view';
        vm.viewName = 'Collateral Information';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.showfields = false;
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.collateraldetailsRules = [];

        var setupRules = function () {

            vm.collateraldetailsRules.push(new validator.PropertyRule("HC!",
                {
                    required: { message: "HC1 is required" }
                }));

            vm.collateraldetailsRules.push(new validator.PropertyRule("HC2",
                {
                    required: { message: "HC2 is required" }
                }));

        };

        var initialize = function () {
            if (vm.init === false) {

                if ($stateParams.Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/collateraldetails/getcollateraldetails/' + $stateParams.Id, null,
                        function (result) {
                            vm.collateraldetails = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.collateraldetails = { HC1: '', HC2: '', Active: true };
            }
        }



        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.collateraldetails, vm.collateraldetailsRules);
            vm.viewModelHelper.modelIsValid = vm.collateraldetails.isValid;
            vm.viewModelHelper.modelErrors = vm.collateraldetails.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/collateraldetails/updatecollateraldetails', vm.collateraldetails,
                    function (result) {

                        $state.go('ifrs-collateraldetails-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.collateraldetails.errors;

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
                vm.viewModelHelper.apiPost('api/collateraldetails/deletecollateraldetails', vm.collateraldetails.ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('ifrs-collateraldetails-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }



        vm.cancel = function () {
            $state.go('ifrs-collateraldetails-list');
        };


        setupRules();
        initialize();



    }
}());
