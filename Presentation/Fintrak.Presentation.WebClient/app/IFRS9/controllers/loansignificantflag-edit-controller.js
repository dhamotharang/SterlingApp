/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanSignificantFlagEditController",
            ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                LoanSignificantFlagEditController]); // 

    function LoanSignificantFlagEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {

        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'LoanSignificantFlag Primary Data';
        vm.view = 'loansignificantflag-edit-view';
        vm.viewName = 'LoanSignificantFlag Primary Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.loanSignificantflag = {};
        vm.prodType = 'nill';
        vm.productTypes = [];      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.Flag = [
            { value: 'Primary', name: 'Primary' },
            { value: 'Secondary', name: 'Secondary' },
            { value: 'Tetiary', name: 'Tertiary' }
        ];
       
        var loansignificantflagRules = [];

        var setupRules = function () {

            loansignificantflagRules.push(new validator.PropertyRule("LoanClassificationId", {
                required: { message: "LoanClassificationId is required" }
            }));

            loansignificantflagRules.push(new validator.PropertyRule("ProductType", {
                required: { message: "Product Type is required" }
            }));

            loansignificantflagRules.push(new validator.PropertyRule("SubType", {
                notZero: { message: "SubType is required" }
            }));

            loansignificantflagRules.push(new validator.PropertyRule("SICR_Flag", {
                notZero: { message: "SICR_Flag is required" }
            }));

            loansignificantflagRules.push(new validator.PropertyRule("SignificantNo", {
                required: { message: "SignificantNo is required" }
            }));

        };


        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

                if ($stateParams.Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/loansignificantflag/getloansignificantflag/' + $stateParams.Id, null,

                        function (result) {
                            vm.loanSignificantflag = result.data;
                            initialView();

                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);

                } else {

                    vm.loanSignificantflag = {
                        Id : '',
                        ProductType : '',
                        SubType : '',
                        SICR_Flag : '',
                        SignificantNo : '',
                        Active : true
                    };

                }


            }
        }

        var intializeLookUp = function () {
            getProductTypes();
            getsubTypes();
            }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.loanSignificantflag, loansignificantflagRules);
            vm.viewModelHelper.modelIsValid = vm.loanSignificantflag.isValid;
            vm.viewModelHelper.modelErrors = vm.loanSignificantflag.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/loansignificantflag/updateloansignificantflag', vm.loanSignificantflag,
               function (result) {                   
                   $state.go('ifrs-loansignificantflag-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.loanSignificantflag.errors;

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
                vm.viewModelHelper.apiPost('api/loansignificantflag/deleteloansignificantflag', vm.loanSignificantflag.Id,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('ifrs-loansignificantflag-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('ifrs-loansignificantflag-list');
        };

        //vm.openRunDate = function ($event) {
        //    $event.preventDefault();
        //    $event.stopPropagation();
        //    vm.openedRunDate = true;
        //};

        //vm.openRunDate2 = function ($event) {
        //    $event.preventDefault();
        //    $event.stopPropagation();
        //    vm.openedRunDate2 = true;
        //};

        //vm.openRunDate3 = function ($event) {
        //    $event.preventDefault();
        //    $event.stopPropagation();
        //    vm.openedRunDate3 = true;
        //};

        var getProductTypes = function () {
            vm.viewModelHelper.apiGet('api/obexposure/getproductTypes',
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
            getSubTypebyProductType(productType);
        };

        var getsubTypes = function () {
            vm.viewModelHelper.apiGet('api/obexposure/getsubtypebyproducttype/' + vm.prodType,
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
