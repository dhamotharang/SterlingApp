/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CurrencyEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CurrencyEditController]);

    function CurrencyEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'currency-edit-view';
        vm.viewName = 'Currency';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.currency = {};
        vm.currencyRates = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.showChildren = false;

        var currencyRules = [];

        var setupRules = function () {
          
            currencyRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            currencyRules.push(new validator.PropertyRule("Symbol", {
                required: { message: "Symbol is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
              
                if ($stateParams.currencyId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/currency/getcurrencywithchildrens/' + $stateParams.currencyId, null,
                   function (result) {
                       vm.currency = result.data.Currency;
                       vm.currencyRates = result.data.CurrencyRates;
                       initialView();
                       vm.init = true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.currency = { Name: '',Symbol: '', Active: true };
            }
        }

        var initialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#currencyRateTable').length > 0) {
                    var exportTable = $('#currencyRateTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            }, 50);
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.currency, currencyRules);
            vm.viewModelHelper.modelIsValid = vm.currency.isValid;
            vm.viewModelHelper.modelErrors = vm.currency.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/currency/updatecurrency', vm.currency,
               function (result) {
                   
                   $state.go('core-currency-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.currency.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/currency/deletecurrency', vm.currency.CurrencyId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-currency-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-currency-list');
        };

        setupRules();
        initialize(); 
    }
}());
