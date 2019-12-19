/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ProductEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ProductEditController]);

    function ProductEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'product-edit-view';
        vm.viewName = 'Product';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.product = {};
        vm.productTypeMappings = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.showChildren = false;

        var productRules = [];

        var setupRules = function () {

            productRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            productRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));


        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups

                if ($stateParams.productId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/product/getproductwithchildren/' + $stateParams.productId, null,
                   function (result) {
                       vm.product = result.data.Product;
                       vm.productTypeMappings = result.data.ProductTypeMappings;
                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    //  vm.product = { Code: '', Name: '', Active: true };
                    vm.product = { Active: true, Code: $stateParams.code, Name: $stateParams.name, IsSwitch: false };

            }
        }

        var initialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#productTypeMappingTable').length > 0) {
                    var exportTable = $('#productTypeMappingTable').DataTable({
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
            validator.ValidateModel(vm.product, productRules);
            vm.viewModelHelper.modelIsValid = vm.product.isValid;
            vm.viewModelHelper.modelErrors = vm.product.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/product/updateproduct', vm.product,
               function (result) {

                   $state.go('core-product-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.product.errors;

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
                vm.viewModelHelper.apiPost('api/product/deleteproduct', vm.product.ProductId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-product-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('core-product-list');
        };

        setupRules();
        initialize();
    }
}());
