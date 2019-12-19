/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CollateralRealizationPeriodListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        CollateralRealizationPeriodListController]);

    function CollateralRealizationPeriodListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'ifrs-collateralrealizationperiod-list-view';
        vm.viewName = 'LGD Related Setups';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.collateralRealizationPeriods = [];
        vm.collateralCategories = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;
        var tabID = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.loadCollatReal();
            }
        }

        var InitialView = function (tableID) {
            InitialGrid(tableID);
        }

        var InitialGrid = function (tableID) {
            tabID = '#' + tableID
            setTimeout(function () {
                // data export
                if ($(tabID).length > 0) {
                    exportTable = $(tabID).DataTable({
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

        vm.loadCategories = function () {
            vm.viewModelHelper.apiGet('api/collateralcategory/availablecollateralcategories', null,
                function (result) {
                    vm.collateralCategories = result.data;
                    InitialView('collateralCategoryTable');

                    exportTable.destroy();
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        vm.loadCollatReal = function () {
            vm.viewModelHelper.apiGet('api/collateralRealizationPeriod/availablecollateralrealizationperiods', null,
                function (result) {
                    vm.collateralRealizationPeriods = result.data;
                    InitialView('collateralRealizationPeriodTable');

                    if (vm.init === true) {
                        exportTable.destroy();
                    }
                    else vm.init = true
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        initialize(); 
    }
}());
