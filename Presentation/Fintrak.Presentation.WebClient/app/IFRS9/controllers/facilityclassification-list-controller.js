/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FacilityClassificationListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        FacilityClassificationListController]);

    function FacilityClassificationListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'facilityclassification-list-view';
        vm.viewName = 'Facility Classification';
        vm.defaultcount = 30;
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.facilityclassification = [];

        vm.init = false;
        vm.showInstruction = true;
        vm.searchParam = '';
        vm.instruction = 'Only top ' + vm.defaultcount + ' records loaded. Use the main search fuctionality to find a specific record by Ref No or Account No.';
        vm.pageFlag = '';
        var exportTable;
        var tabID = '';

        var initialize = function() {

            if (vm.init === false) {
                vm.loadLoans();
            }
        };



        var InitialView = function(tableID) {
            InitialGrid(tableID);
        };

        var InitialGrid = function(tableID) {
            tabID = '#' + tableID;
            setTimeout(function() {
                    // data export
                    if ($(tabID).length > 0) {
                        exportTable = $(tabID).DataTable({
                            "lengthMenu": [[50, 50, 100, -1], [50, 50, 100, "All"]],
                            sDom: "T<'clearfix'>" +
                                "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                                "t" +
                                "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                            "tableTools": {
                                "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                            }
                        });
                    }
                },
                50);
        };

        vm.loadLoans = function () {
            vm.viewModelHelper.apiGet('api/facilityclassification/availablefacilityclassifications/' + vm.defaultcount + '/' + 'LD',
                null,
                function (result) {
                    vm.facilityclassification = result.data;
                    InitialView('ldTable');
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

        vm.loadOBE = function() {
            vm.viewModelHelper.apiGet('api/facilityclassification/availablefacilityclassifications/' + vm.defaultcount + '/' + 'OB',
                null,
                function(result) {
                    vm.facilityclassification = result.data;
                    InitialView('obTable');
                    vm.pageFlag = 2;
                    vm.searchParam = '';

                    exportTable.destroy();
                },
                function(result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

      

        vm.loadInv = function () {
            vm.viewModelHelper.apiGet('api/facilityclassification/availablefacilityclassifications/' + vm.defaultcount + '/' + 'IN',
                null,
                function (result) {
                    vm.facilityclassification = result.data;
                    InitialView('inTable');
                    vm.pageFlag = 2;
                    vm.searchParam = '';

                    exportTable.destroy();
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

        vm.loadBySearch = function (type, tableID) {

            if (vm.serachParam === '') {
                toastr.warning('Please input a RefNo', 'Empty Search')
                return
            }

            else {

                vm.viewModelHelper.apiGet('api/facilityclassification/availablefacilityclassificationsbysearch/' + type + '/' + vm.searchParam, null,
                    function (result) {
                        vm.facilityclassification = result.data;
                        InitialView(tableID);
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
}());
