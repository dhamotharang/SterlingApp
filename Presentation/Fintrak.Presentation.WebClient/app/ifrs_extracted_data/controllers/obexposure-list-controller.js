/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OBExposureListController",
            ['$scope', '$state', 'viewModelHelper', 'validator',
                OBExposureListController]);

    function OBExposureListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'obexposure-list-view';
        vm.viewName = 'Current Off-Balance Sheet Exposures';
        vm.defaultCount = 2000;
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.minPageSize = 10;
        vm.pageSize = vm.minPageSize;

        vm.init = false;
        vm.showInstruction = true;
        vm.searchParam = '';
        vm.instruction = 'Only top ' + vm.defaultCount + ' records loaded. Use the main search fuctionality to find a specific record by Reference No or Account No';
        vm.flag = '';
        var exportTable;
        var tabID = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.loadBG();
            }
        };



        var InitialView = function (tableID) {
            if (exportTable) exportTable.destroy();
            InitialGrid(tableID);
        };

        var InitialGrid = function (tableID) {
            tabID = '#' + tableID;
            setTimeout(function () {
                // data export
                if ($(tabID).length > 0) {
                    exportTable = $(tabID).DataTable({
                        searching: false, paging: false, info: false,
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" + "RC" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        },
                        "aoColumnDefs": [
                            //{ "bVisible": false, "aTargets": [0] }
                        ],
                        "colVis": {
                            buttonText: 'Show / Hide Columns',
                            restore: "Restore",
                            showAll: "Show all"
                        }
                    });
                }
            },
                50);
        };

        vm.loadLC = function () {
            vm.flag = 2
            vm.viewModelHelper.apiGet('api/obExposure/availableOBExposure/' + vm.flag + '/' + vm.defaultCount,
                null,
                function (result) {
                    vm.obexposuresTotal = result.data;
                    InitialView('lcTable');
                    vm.searchParam = '';
                    vm.totalRecords = vm.obexposuresTotal.length;
                    vm.totalPages = Math.ceil(vm.totalRecords / vm.pageSize);
                    vm.currentPage = vm.totalPages == 0 ? 0 : 1;
                    vm.lastRecordPrevPage = vm.totalPages == 0 ? -1 : 0;
                    vm.obexposures = vm.obexposuresTotal.slice(vm.lastRecordPrevPage,
                        Math.min(vm.obexposuresTotal.length, vm.currentPage * vm.pageSize));
                    vm.displayedRows = vm.totalPages == 0 ? 1 : vm.obexposures.length;

                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

        vm.loadBG = function () {
            vm.flag = 1;
            vm.viewModelHelper.apiGet('api/obExposure/availableOBExposure/' + vm.flag + '/' + vm.defaultCount,
                null,
                function (result) {
                    vm.obexposuresTotal = result.data;
                    InitialView('bgTable');
                    vm.searchParam = '';
                    vm.totalRecords = vm.obexposuresTotal.length;
                    vm.totalPages = Math.ceil(vm.totalRecords / vm.pageSize);
                    vm.currentPage = vm.totalPages == 0 ? 0 : 1;
                    vm.lastRecordPrevPage = vm.totalPages == 0 ? -1 : 0;
                    vm.obexposures = vm.obexposuresTotal.slice(vm.lastRecordPrevPage,
                        Math.min(vm.obexposuresTotal.length, vm.currentPage * vm.pageSize));
                    vm.displayedRows = vm.totalPages == 0 ? 1 : vm.obexposures.length;
                    if (vm.init === true) {
                    } else vm.init = true;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

        vm.loadOD = function () {
            vm.flag = 3
            vm.viewModelHelper.apiGet('api/obExposure/availableOBExposure/' + vm.flag + '/' + vm.defaultCount,
                null,
                function (result) {
                    vm.obexposuresTotal = result.data;
                    InitialView('odTable');
                    vm.searchParam = '';
                    vm.totalRecords = vm.obexposuresTotal.length;
                    vm.totalPages = Math.ceil(vm.totalRecords / vm.pageSize);
                    vm.currentPage = vm.totalPages == 0 ? 0 : 1;
                    vm.lastRecordPrevPage = vm.totalPages == 0 ? -1 : 0;
                    vm.obexposures = vm.obexposuresTotal.slice(vm.lastRecordPrevPage,
                        Math.min(vm.obexposuresTotal.length, vm.currentPage * vm.pageSize));
                    vm.displayedRows = vm.totalPages == 0 ? 1 : vm.obexposures.length;

                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

        vm.loadOBEBySearch = function (flag, tableID) {
            vm.flag = flag;
            if (vm.serachParam === '') {
                toastr.warning('Please input a RefNo', 'Empty Search')
                return
            }

            else {

                vm.viewModelHelper.apiGet('api/obExposure/availableOBExposureBySearch/' + flag + '/' + vm.searchParam.split('/').join('__'), null,
                    function (result) {
                        vm.obexposuresTotal = result.data;
                        vm.totalRecords = vm.obexposuresTotal.length;
                        vm.totalPages = Math.ceil(vm.totalRecords / vm.pageSize);
                        vm.currentPage = vm.totalPages == 0 ? 0 : 1;
                        vm.lastRecordPrevPage = vm.totalPages == 0 ? -1 : 0;
                        vm.obexposures = vm.obexposuresTotal.slice(vm.lastRecordPrevPage,
                            Math.min(vm.obexposuresTotal.length, vm.currentPage * vm.pageSize));
                        vm.displayedRows = vm.totalPages == 0 ? 1 : vm.obexposures.length;
                        InitialView(tableID);
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
        }

        vm.goToPage = function (dest) {
            if (dest === 'next') {
                vm.currentPage = parseInt(vm.currentPage) + 1;
            } else if (dest === 'prev') {
                vm.currentPage = parseInt(vm.currentPage) - 1;
            } else {
                vm.currentPage = parseInt(vm.currentPage);
            }
            vm.currentPage = vm.totalPages == 0 ? 0 : Math.max(1, Math.min(vm.totalPages, vm.currentPage));
            vm.lastRecordPrevPage = (vm.currentPage - 1) * vm.pageSize;
            vm.obexposures = vm.obexposuresTotal.slice(vm.lastRecordPrevPage,
                Math.min(vm.obexposuresTotal.length, vm.currentPage * vm.pageSize));
            vm.displayedRows = vm.obexposures.length;
        }

        vm.resizePage = function () {
            vm.pageSize = parseInt(vm.pageSize);
            if (vm.pageSize > 200) {
                vm.pageSize = 200;
            } else if (vm.pageSize < vm.minPageSize) {
                vm.pageSize = vm.minPageSize;
            }
            vm.totalPages = Math.ceil(vm.totalRecords / vm.pageSize);

            vm.currentPage = vm.totalPages == 0 ? 0 : 1;
            vm.obexposures = vm.obexposuresTotal.slice(0,
                Math.min(vm.obexposuresTotal.length, vm.currentPage * vm.pageSize));
            vm.displayedRows = vm.obexposures.length;

            InitialGrid(vm.flag == 1 ? 'bgTable' : vm.flag == 2 ? 'lcTable' : 'odTable');
        };


        vm.exportAllData = function () {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                vm.loading = false;
            }
            xhr.open('GET', 'api/obExposure/availableOBExposure/' + vm.flag + '/0', true);
            xhr.responseType = 'arraybuffer';
            xhr.onload = function () {
                if (this.status === 200) {
                    var filename = "";
                    var disposition = xhr.getResponseHeader('Content-Disposition');
                    if (disposition && disposition.indexOf('attachment') !== -1) {
                        var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                        var matches = filenameRegex.exec(disposition);
                        if (matches != null && matches[1]) filename = matches[1].replace(/['"]/g, '');
                    }
                    var type = xhr.getResponseHeader('Content-Type');

                    var blob = typeof File === 'function'
                        ? new File([this.response], filename, { type: type })
                        : new Blob([this.response], { type: type });
                    if (typeof window.navigator.msSaveBlob !== 'undefined') {
                        // IE workaround for "HTML7007: One or more blob URLs were revoked by closing the blob for which they were created. These URLs will no longer resolve as the data backing the URL has been freed."
                        window.navigator.msSaveBlob(blob, filename);
                    } else {
                        var URL = window.URL || window.webkitURL;
                        var downloadUrl = URL.createObjectURL(blob);

                        if (filename) {
                            // use HTML5 a[download] attribute to specify filename
                            var a = document.createElement("a");
                            // safari doesn't support this yet
                            if (typeof a.download === 'undefined') {
                                window.location = downloadUrl;
                            } else {
                                a.href = downloadUrl;
                                a.download = filename;
                                document.body.appendChild(a);
                                a.click();
                            }
                        } else {
                            window.location = downloadUrl;
                        }

                        setTimeout(function () { URL.revokeObjectURL(downloadUrl); }, 100); // cleanup
                    }
                }
            };
            xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
            vm.loading = true;
            xhr.send()
        }


        initialize();
    }
}());
