/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MPRProductListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        MPRProductListController]);

    function MPRProductListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'mprproduct-list-view';
        vm.viewName = 'Products';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.mprProducts = [];
        vm.mprProductCode = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var getmprProduct = function () {
            vm.viewModelHelper.apiGet('api/mprProduct/availablemprProducts/' + vm.mprProductCode.ProductCode, null,
                 function (result) {
                     vm.mprProducts = [];
                     vm.mprProducts = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load teams.', 'Fintrak');
                 }, null);
        }


        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/mprProduct/availablemprProducts', null,
                   function (result) {
                       vm.mprProducts = result.data;
                       InitialView();
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#mprProductTable').length > 0) {
                    var exportTable = $('#mprProductTable').DataTable({
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

        initialize();

        vm.searchByProductCode = function () {
            getmprProduct();
        }



    }
}());
