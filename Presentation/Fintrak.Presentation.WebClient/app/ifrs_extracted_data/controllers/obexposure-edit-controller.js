/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OBExposureEditController",
            ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                OBExposureEditController]);

    function OBExposureEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS Core';
        vm.view = 'obexposure-edit-view';
        vm.viewName = 'Current Off Balance Sheet Exposures (BG, LC & Overdraft)';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.showfields = false;
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

        vm.obexposureRules = [];

        var setupRules = function () {

            vm.obexposureRules.push(new validator.PropertyRule("RefNo",
                {
                    required: { message: "RefNo is required" }
                }));

            vm.obexposureRules.push(new validator.PropertyRule("Amount",
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
                    vm.viewModelHelper.apiGet('api/obexposure/getobexposure/' + $stateParams.obeId, null,
                        function (result) {
                            vm.obexposure = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.obexposure = { Caption: '', FinType: '', FinSubType: '', Position: 1, Class: 1, flag: $stateParams.flag, Active: true };
            }
        }



        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.obexposure, vm.obexposureRules);
            vm.viewModelHelper.modelIsValid = vm.obexposure.isValid;
            vm.viewModelHelper.modelErrors = vm.obexposure.errors;
            if (vm.viewModelHelper.modelIsValid) {
                vm.obexposure.flag = $stateParams.flag;
                vm.viewModelHelper.apiPost('api/obexposure/updateobexposure', vm.obexposure,
                    function (result) {

                        $state.go('ifrs-obexposure-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.obexposure.errors;

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
                vm.viewModelHelper.apiPost('api/obexposure/deleteobexposure', vm.obexposure.obeId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('ifrs-obexposure-list');
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
            $state.go('ifrs-obexposure-list');
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
            vm.viewModelHelper.apiGet('api/obexposure/getHC1',
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

            $stateParams.flag = vm.obexposure.Mapped_OBE_Type === 'Self Liquidating' ? 2 : vm.obexposure.Mapped_OBE_Type === 'N/A' ? 3 : 1
            vm.type = $stateParams.flag === 1 ? 'BG' : $stateParams.flag === 2 ? 'LC' : 'OD';

        };

        var getsubTypes = function (productType) {
            vm.viewModelHelper.apiGet('api/obexposure/getHC2ByHC1/' + productType,
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
