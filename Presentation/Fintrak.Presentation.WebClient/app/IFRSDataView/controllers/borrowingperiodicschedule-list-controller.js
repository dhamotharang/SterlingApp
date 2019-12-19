/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BorrowingPeriodicScheduleListController",
            ['$scope', '$state', '$window', 'viewModelHelper', 'validator',
                BorrowingPeriodicScheduleListController]);

    function BorrowingPeriodicScheduleListController($scope, $state, $window, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_Processed_Data';
        vm.view = 'borrowingperiodicschedule-list-view';
        vm.viewName = 'Borrowing Periodic Schedule';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.borrowingPeriodicSchedules = [];
        vm.distinctRefNos = [];
        vm.selectedRefNos = [];

        vm.RefNo = 'None';
        vm.init = false;
        vm.showInstruction = true;
        vm.instruction = 'Use the dropdown to search and select all desired contracts to load. ' +
            'To export all records, unselect all contracts. ' +
            'The "Split then Export" button splits records into seperate excel files based on Contract Number.';


        var initialize = function () {

            if (vm.init === false) {

                intializeLookUp();
                vm.viewModelHelper.apiGet('api/borrowingperiodicschedule/availableborrowingperiodicschedule', null,

                    function (result) {
                        //  vm.borrowingPeriodicSchedules = result.data;
                        InitialView();

                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);

                vm.init === true;
            }
        }

        var intializeLookUp = function () {
            getRefNos();
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#borrowingPeriodicScheduleTable').length > 0) {
                    var exportTable = $('#borrowingPeriodicScheduleTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" + "RC" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        },
                        "aoColumnDefs": [
                        ],
                        "colVis": {
                            buttonText: 'Show / Hide Columns',
                            restore: "Restore",
                            showAll: "Show all"
                        }
                    });
                }
            }, 50);
        }

        var getRefNos = function () {
            vm.viewModelHelper.apiGet('api/borrowingperiodicschedule/getrefnos', null,
                function (result) {
                    vm.distinctRefNos = result.data;
                },
                function (result) {
                    toastr.error('Borrowing Periodic Scedule.', 'Fintrak');
                }, null);
        }
        vm.load = function () {
            vm.RefNo = '';
            vm.selectedRefNos.forEach(function (e, index) {
                if (index != (vm.selectedRefNos.length)) {
                    vm.RefNo += e.name + ' ';
                };
            });
            if (vm.RefNo === '') {
                toastr.warning('Please input a RefNo', 'Empty Reference Number')
                return
            }
            else {

                vm.viewModelHelper.apiGet('api/borrowingperiodicschedule/getborrowingperiodicschedule/' + vm.RefNo, null,
                    function (result) {
                        vm.borrowingPeriodicSchedules = result.data;

                        toastr.success('Data for the selected RefNo loaded.', 'Fintrak');
                    },
                    function (result) {
                        toastr.error('Fail to load data for the selected RefNo.', 'Fintrak');
                    }, null);
            }
        }

        vm.exportAllData = function (split) {
            split = split ? split : '';
            vm.RefNo = '';
            vm.selectedRefNos.forEach(function (e, index) {
                if (index != (vm.selectedRefNos.length)) {
                    vm.RefNo += e.name + ' ';
                };
            });
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                vm.loading = false;
            }
            var Date = (!vm.Date || vm.Date === '') ? '' : vm.Date.toLocaleDateString('sq-AL');
            xhr.open('GET', 'api/borrowingperiodicschedule/getborrowingperiodicschedule/' + split + 'ExportData ' + vm.RefNo, true);
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
        vm.delete = function (RefNo) {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/borrowingperiodicschedule/deleteborrowingperiodicschedule/' + vm.RefNo, null,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('ifrs-borrowingperiodicschedule-list');
                        vm.RefreshTable();
                    },
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                    }, null);
            }
        }
        vm.RefreshTable = function () {
            vm.viewModelHelper.apiGet('api/borrowingperiodicschedule/getborrowingperiodicschedule/' + vm.RefNo, null,
                function (result) {
                    vm.borrowingPeriodicSchedules = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }


    }
}());
