/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoansECLComputationResultListController",
            ['$scope', '$state', 'viewModelHelper', 'validator', LoansECLComputationResultListController]);

    function LoansECLComputationResultListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LoansECLComputationResult_Data';
        vm.view = 'loanseclcomputationresult-list-view';
        vm.viewName = 'Detail ECL Result for Loans';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        var exportTable;
        var tabID = 'loansecLComputationresultTable';

        vm.loansecLComputationresult = [];
        vm.init = false;
        vm.searchParam = '';
        vm.defaultCount = 5000;
        vm.showInstruction = true;
        vm.instruction = 'Only top ' + vm.defaultCount + ' records loaded. Use the main search fuctionality to find specific records by CustID or Account No or leave blank to load all records. ' +
            'Begin your search with "split " before exporting to split records into seperate excel files based on Account No.';


        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/loansecLComputationresult/availableLoansECLComputationResult/' + vm.defaultCount, null,
                    function (result) {
                        vm.loansecLComputationresult = result.data;
                        InitialView('loansecLComputationresultTable');
                        vm.searchParam = '';
                        if (vm.init === true) {
                            exportTable.destroy();
                        } else vm.init = true;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        var InitialView = function (tableId) {
            InitialGrid(tableId);
        };

        var InitialGrid = function (tabID) {
            tabID = '#' + tabID;
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
            },
                50);
        };

        vm.loadLoansECLComputationResultBySearch = function () {

            if (vm.searchParam === '') {
                toastr.warning('Please input a RefNo ', 'Empty Search')  // or Account No
                return
            }
            else {

                vm.viewModelHelper.apiGet('api/loansecLComputationresult/getloanseclcomputationresultbysearch/' + vm.searchParam, null,
                    function (result) {
                        vm.loansecLComputationresult = result.data;
                        InitialView('loansecLComputationresultTable');
                        if (vm.init === true) {
                            exportTable.destroy();
                        } else vm.init = true;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }

        };

        vm.refresh = function () {
            vm.init = false;
            vm.searchParam = '';
            initialize();
            exportTable.destroy();
        }

        vm.exportAllData = function () {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                vm.loading = false;
            }
            if (!vm.searchParam || vm.searchParam === '') {
                xhr.open('GET', 'api/loansecLComputationresult/availableLoansECLComputationResult/0', true);
            } else if (vm.searchParam.substring(0, 5) === 'split' && (vm.searchParam.split('split')[1] === null || vm.searchParam.split('split')[1].match(/^ *$/) !== null)) {
                xhr.open('GET', 'api/loansecLComputationresult/availableLoansECLComputationResult/-1', true);
            } else {
                xhr.open('GET', 'api/loansecLComputationresult/getloanseclcomputationresultbysearch/ExportData ' + vm.searchParam, true);
            }
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
