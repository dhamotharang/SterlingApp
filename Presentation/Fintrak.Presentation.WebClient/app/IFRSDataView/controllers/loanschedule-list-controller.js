/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanScheduleListController",
            ['$scope', '$state', 'viewModelHelper', 'validator',
                LoanScheduleListController]);

    function LoanScheduleListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_Processed_Data';
        vm.view = 'loanschedule-list-view';
        vm.viewName = 'Loan Schedules';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.loanSchedules = [];

        vm.distinctRefNos = [];
        vm.selectedRefNos = [];

        vm.RefNo = '';
        vm.Date = '';
        vm.init = false;
        vm.showInstruction = true;
        vm.instruction = 'Use the dropdown to search and select all desired contracts to load. ' +
            'Select a date to load the schedule for 30 days prior and 30 days after. ' +
            'To export all contracts, unselect all contracts and to export the schedule from effective date to maturity, clear date. ' +
            'The "Split then Export" button splits records into seperate excel files based on Contract Number.';
        var exportTable;

        var initialize = function () {

            if (vm.init === false) {

                intializeLookUp();
                //vm.viewModelHelper.apiGet('api/loanschedule/getloanschedule/' + vm.RefNo, null,
                //   function (result) {
                //       vm.loanSchedules = result.data;
                InitialView();
                vm.init === true;

                //   },
                // function (result) {
                //     toastr.error(result.data, 'Fintrak');
                // }, null);
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
                if ($('#loanScheduleTable').length > 0) {
                    exportTable = $('#loanScheduleTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        searching: false,
                        scrollX: true,
                        //sDom: "T<'clearfix'>" +
                        //    "<'row'<'col-sm-6'l><'col-sm-6'f>r>" + "RC" +
                        //    "t" +
                        //    "<'row'<'col-sm-6'i><'col-sm-6'p>>",
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
            }, 50);
        }

        var getRefNos = function () {
            vm.viewModelHelper.apiGet('api/loanschedule/getrefnos', null,
                function (result) {
                    vm.distinctRefNos = result.data;
                },
                function (result) {
                    toastr.error('Loan  Scedule.', 'Fintrak');
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
            else if (!vm.Date || vm.Date === '') {
                toastr.warning('Please provide a date', 'Empty Date')
                return
            }
            else {

                vm.viewModelHelper.apiGet('api/loanschedule/getschedulerange?RefNo=' + vm.RefNo + '&Date=' + vm.Date.toLocaleDateString('sq-AL'), null,
                    function (result) {
                        vm.loanSchedules = result.data;
                        InitialView();
                        exportTable.destroy();

                        toastr.success('Data for the selected RefNo and Date Range loaded.', 'Fintrak');
                    },
                    function (result) {
                        toastr.error('Fail to load data for the selected RefNo and Date Range.', 'Fintrak');
                    }, null);
            }
        }

        //vm.exportToExcel = function (tableId) { 
        //    $(tableId).tableExport({ type: 'excel', escape: 'false' });
        //}


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
            xhr.open('GET', 'api/loanschedule/getschedulerange?RefNo=' + split + 'ExportData ' + vm.RefNo + '&Date=' + Date, true);
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
