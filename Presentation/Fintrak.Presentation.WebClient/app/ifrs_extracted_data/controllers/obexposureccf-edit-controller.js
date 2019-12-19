/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OBExposureCCFEditController",
            ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                OBExposureCCFEditController]);

    function OBExposureCCFEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS Core';
        vm.view = 'obexposure-edit-view';
        vm.viewName = 'Historical Off Balance Sheet Exposures (BG, LC & Overdraft)';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.showfields = true;
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.openedRunDate = false;
        vm.productTypes = [];
        vm.subTypes = [];
        vm.sectors = [];

        vm.OBEMappedTo = [
            { value: 'PBB', name: 'PBG' },
            { value: 'FBG', name: 'FBG' },
            { value: 'Self Liquidating', name: 'Self Liquidating' },
            { value: 'N/A', name: 'N/A' },
        ];

        vm.obexposureccfRules = [];

        var setupRules = function () {

            vm.obexposureccfRules.push(new validator.PropertyRule("RefNo",
                {
                    required: { message: "RefNo is required" }
                }));

            vm.obexposureccfRules.push(new validator.PropertyRule("Amount",
                {
                    required: { message: "Amount is required" }
                }));

        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                vm.type = $stateParams.flag === 1 ? 'BG' : $stateParams.flag === 2 ? 'LC' : 'OD';

                if ($stateParams.obeId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/obexposureccf/getobexposureccf/' + $stateParams.obeId, null,
                        function (result) {
                            vm.obexposureccf = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.obexposureccf = { Caption: '', FinType: '', FinSubType: '', Position: 1, Class: 1, flag: $stateParams.flag, Active: true };
            }
        }



        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.obexposureccf, vm.obexposureccfRules);
            vm.viewModelHelper.modelIsValid = vm.obexposureccf.isValid;
            vm.viewModelHelper.modelErrors = vm.obexposureccf.errors;
            if (vm.viewModelHelper.modelIsValid) {
                vm.obexposure.flag = $stateParams.flag;
                vm.viewModelHelper.apiPost('api/obexposureccf/updateobexposureccf', vm.obexposureccf,
                    function (result) {

                        $state.go('ifrs-obexposureccf-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.obexposureccf.errors;

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
                vm.viewModelHelper.apiPost('api/obexposureccf/deleteobexposureccf', vm.obexposureccf.obeId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('ifrs-obexposureccf-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }

        var intializeLookUp = function () {
            getProductTypes();
            //getsubTypes('nill');
            // getsectors();
        };



        vm.cancel = function () {
            $state.go('ifrs-obexposureccf-list');
        };

        vm.openDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedDate = true;
        }
        vm.openDate2 = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedDate2 = true;
        }

        var getProductTypes = function () {
            vm.viewModelHelper.apiGet('api/obexposureccf/getHC1',
                null,
                function (result) {
                    vm.productTypes = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

        vm.onProductTypeChanged = function (productType) {
            //  alert(productType);
            getsubTypes(productType);

        };

        vm.onMappedOBETypeChanged = function () {

            $stateParams.flag = vm.obexposureccf.Mapped_OBE_Type === 'Self Liquidating' ? 2 : vm.obexposureccf.Mapped_OBE_Type === 'N/A' ? 3 : 1
            vm.type = $stateParams.flag === 1 ? 'BG' : $stateParams.flag === 2 ? 'LC' : 'OD';

        };

        var getsubTypes = function (productType) {
            vm.viewModelHelper.apiGet('api/obexposureccf/getHC2ByHC1/' + productType,
                null,
                function (result) {
                    vm.subTypes = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };


        setupRules();
        initialize();



    }
}());
