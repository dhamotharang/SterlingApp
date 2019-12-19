/**
 * Created by Tosin on 8/12/2019.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsBondsMonthlyEADListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IfrsBondsMonthlyEADListController]);

    function IfrsBondsMonthlyEADListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrsbondsmonthlyead-list-view';
        vm.viewName = 'Bonds Monthly EAD';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.defaultcount = 3000;
        vm.ifrsbondsmonthlyead = {};

        vm.showInstruction = true;
        vm.searchParam = '';
        vm.instruction = 'Only top ' + vm.defaultcount + ' records loaded. Use the main search fuctionality to find a specific record by Ref No or Account No.';

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;
        var tabID = '';

        var initialize = function(){
            
            if (vm.init === false) {
                vm.loadBoandData();
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
        vm.loadBySearch = function () {

            if (vm.searchParam === '') {
                toastr.warning('Please input a RefNo', 'Empty Search')
                return
            }

            else {

                vm.viewModelHelper.apiGet('api/ifrsbondsmonthlyead/getallifrsbondsmonthlyeadbysearch/' + vm.searchParam, null,
                    function (result) {
                        vm.ifrsbondsmonthlyead = result.data;
                        InitialView('bondEADTable');
                        exportTable.destroy();
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }

        }
        vm.loadBoandData = function () {
            vm.viewModelHelper.apiGet('api/ifrsbondsmonthlyead/getallifrsbondsmonthlyead/' + vm.defaultcount, null,
                function (result) {
                    vm.ifrsbondsmonthlyead = result.data;
                    InitialView('bondEADTable');
                    vm.searchParam = '';
                    if (vm.init === true) {
                    exportTable.destroy();
                    }
                    else vm.init = true
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        vm.refresh = function () {
            vm.init = false;
            vm.searchParam = '';
            initialize();
            exportTable.destroy();
        }

        initialize(); 
    }
}());
