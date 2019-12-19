/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ifrscustomerpdEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IfrsCustomerPdEditController
                    ]
        );

    function IfrsCustomerPdEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrscustomerpd-edit-view';
        vm.viewName = 'Customer Pd';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifrsCustomerPd = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.ratings = [
            '1',
            '2+',
            '2',
            '2-',
            '3+',
            '3',
            '3-',
            '4',
            '5',
            '6',
            '7',
            '8' 
        ];
        vm.spRatings = [
            'A',
            'B',
            'BB',
            'CCC',
            'D'
        ];

        var ifrsCustomerPdRules = [];

        var setupRules = function () {

            ifrsCustomerPdRules.push(new validator.PropertyRule("CustomerId", {
                required: { message: "Customer ID is required" }
            }));

            ifrsCustomerPdRules.push(new validator.PropertyRule("Rating", {
                required: { message: "Rating is required" }
            }));

            ifrsCustomerPdRules.push(new validator.PropertyRule("SP", {
                required: { message: "S&P Rating is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.customerPDId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ifrscustomerpd/getIfrsCustomerPD/' + $stateParams.customerPDId, null,
                   function (result) {
                       vm.ifrsCustomerPd = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.ifrsCustomerPd = { refno: 0, ClassificationStage: 0, LGD_BEST: 0, LGD_DOWNTURN: 0, LGD_OPTIMISTIC: 0, Month: 0, Category: '', Active: true };
            }
        }

        var intializeLookUp = function () {

        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ifrsCustomerPd, ifrsCustomerPdRules);
            vm.viewModelHelper.modelIsValid = vm.ifrsCustomerPd.isValid;
            vm.viewModelHelper.modelErrors = vm.ifrsCustomerPd.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/ifrscustomerpd/updateIfrsCustomerPD', vm.ifrsCustomerPd,
               function (result) {

                   $state.go('ifrs9-ifrscustomerpd-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.ifrsCustomerPd.errors;

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
                vm.viewModelHelper.apiPost('api/ifrscustomerpd/deleteIfrsCustomerPD', vm.ifrsCustomerPd.CustomerPDId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-ifrscustomerpd-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrs9-ifrscustomerpd-list');
        };

        setupRules();
        initialize();
    }
}());
