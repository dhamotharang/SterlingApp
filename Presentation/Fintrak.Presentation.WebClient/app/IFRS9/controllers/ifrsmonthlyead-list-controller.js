/**
 * Created by Tosin on 8/12/2019.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsMonthlyEADListController",
        ['$scope', '$state', 'viewModelHelper', 'validator',
            IfrsMonthlyEADListController]);

    function IfrsMonthlyEADListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrsmonthlyead-list-view';
        vm.viewName = 'Loan Monthly EAD';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.defaultcount = 5000;
        vm.IfrsbondsMonthlyEAD = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;
        var tabID = 'loanEADTable';
        vm.showInstruction = true;
        vm.searchParam = '';
        vm.instruction = 'Only top ' + vm.defaultcount + ' records loaded. Use the main search fuctionality to find a specific record by Ref No or Account No.';

        var initialize = function () {

            if (vm.init === false) {
                vm.loadEADData();
                //vm.init === true;
            }
        }

        vm.loadEADData = function () {
            vm.viewModelHelper.apiGet('api/ifrsmonthlyead/getallifrsmonthlyead/' + vm.defaultcount,
                null,
                function (result) {
                    vm.ifrsmonthlyead = result.data;
                    InitialView('loanEADTable');
                    vm.searchParam = '';
                    if (vm.init === true) {
                        exportTable.destroy();
                    } else vm.init = true;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

        var InitialView = function (tableID) {
            InitialGrid(tableID);
        }

        var InitialGrid = function (tableID) {
            tabID = '#' + tableID
            setTimeout(function () {
                // data export
                if ($(tabID).length > 0) {
                    exportTable = $(tabID).DataTable({
                        "lengthMenu": [[50, 75, 100, -1], [50, 75, 100, "All"]],
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
        vm.loadBySearch = function () {

            if (vm.searchParam === '') {
                toastr.warning('Please input a RefNo', 'Empty Search')
                return
            }

            else {

                vm.viewModelHelper.apiGet('api/ifrsmonthlyead/getallifrsmonthlyeadbysearch/' + vm.searchParam, null,
                    function (result) {
                        vm.ifrsmonthlyead = result.data;
                        InitialView('loanEADTable');
                        exportTable.destroy();
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }

        }
        vm.refresh = function () {
            vm.init = false;
            vm.serachParam = '';
            initialize();
            exportTable.destroy();
        }
        initialize();
    }
    
}
    ());