/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanPeriodicScheduleListController",
            ['$scope', '$state', '$window', 'viewModelHelper', 'validator',
                LoanPeriodicScheduleListController]);

    function LoanPeriodicScheduleListController($scope, $state, $window, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_Processed_Data';
        vm.view = 'loanperiodicschedule-list-view';
        vm.viewName = 'Loan Periodic Schedule';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.loanPeriodicSchedules = [];
        vm.distinctRefNos = [];
        vm.selectedRefNos = [];

        vm.RefNo = 'None';
        vm.init = false;
        vm.showInstruction = true;
        vm.instruction = 'Use the dropdown to search and select all desired contracts to load. ' +
            'To export all records, unselect all contracts. ' +
            'The "Split then Export" button splits records into seperate excel files based on Contract Number.';
        var exportTable;


        var initialize = function () {

            if (vm.init === false) {

                intializeLookUp();
                //  vm.viewModelHelper.apiGet('api/loanperiodicschedule/getloanperiodicschedule/' + vm.RefNo, null,
                //  vm.viewModelHelper.apiGet('api/loanperiodicschedule/availableloanperiodicschedule', null,

                //function (result) {
                //  vm.loanPeriodicSchedules = result.data;
                InitialView();
                vm.init === true;

                //  },
                //function (result) {
                //    toastr.error(result.data, 'Fintrak');
                //}, null);

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
                if ($('#loanPeriodicScheduleTable').length > 0) {
                    exportTable = $('#loanPeriodicScheduleTable').DataTable({
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
            vm.viewModelHelper.apiGet('api/loanperiodicschedule/getrefnos', null,
                function (result) {
                    vm.distinctRefNos = result.data;
                },
                function (result) {
                    toastr.error('Loan Periodic Scedule.', 'Fintrak');
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

                vm.viewModelHelper.apiGet('api/loanperiodicschedule/getloanperiodicschedule/' + vm.RefNo, null,
                    function (result) {
                        vm.loanPeriodicSchedules = result.data;

                        toastr.success('Data for the selected RefNo loaded.', 'Fintrak');
                    },
                    function (result) {
                        toastr.error('Fail to load data for the selected RefNo.', 'Fintrak');
                    }, null);
            }
        }

        vm.delete = function (RefNo) {
            var deleteFlag = $window.confirm(' Are you sure you want to delete the schedule for the selected RefNo ?');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/loanperiodicschedule/deleteloanperiodicschedule/' + vm.RefNo, null,
                    function (result) {
                        toastr.success('Selected RefNo deleted.', 'Fintrak');
                        alert('Selected RefNo deleted.');
                        $state.go('ifrs-loanperiodicschedule-list');
                        vm.RefreshTable();
                    },
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                    }, null);
            }
        }

        vm.RefreshTable = function () {
            vm.viewModelHelper.apiGet('api/loanperiodicschedule/getloanperiodicschedule/' + vm.RefNo, null,
                function (result) {
                    vm.loanPeriodicSchedules = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
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
            xhr.open('GET', 'api/loanperiodicschedule/getloanperiodicschedule/' + split + 'ExportData ' + vm.RefNo, true);
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
