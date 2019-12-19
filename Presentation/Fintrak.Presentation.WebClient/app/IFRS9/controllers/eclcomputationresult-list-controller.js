/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("EclComputationResultListController",
            ['$scope', '$state', 'viewModelHelper', 'validator',
                EclComputationResultListController]);

    function EclComputationResultListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'eclcomputationresult-list-view';
        vm.viewName = 'EclComputationResult';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.eclComputationResults = [];
        vm.stage = 'All';

        vm.stages = [
            { Id: 'All', Name: 'All' },
            { Id: 1, Name: 'Performing' },
            { Id: 2, Name: 'Under-Performing' },
            { Id: 3, Name: 'Non-Performing' },
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = 'Only top ' + vm.defaultCount + ' records loaded. Use the main search fuctionality to find specific records by Customer Number or Contract Number. Leave search blank to export all records. ' +
            'The "Split then Export" button splits records into seperate excel files based on Contract Number.';
        var exportTable;

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/eclcomputationresult/availableeclComputationResults', null,
                    function (result) {
                        vm.eclComputationResults = result.data;
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
                if ($('#eclComputationResultTable').length > 0) {
                    exportTable = $('#eclComputationResultTable').DataTable({
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

        vm.loadByStage = function () {
            if (vm.stage === 'All') {

                vm.viewModelHelper.apiGet('api/eclcomputationresult/availableeclComputationResults', null,
                    function (result) {
                        vm.eclComputationResults = result.data;
                        InitialView();
                        exportTable.destroy();

                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {

                vm.viewModelHelper.apiGet('api/eclcomputationresult/availableeclComputationResultsByStage/' + vm.stage, null,
                    function (result) {
                        vm.eclComputationResults = result.data;
                        InitialView();
                        exportTable.destroy();
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }

        vm.exportAllData = function () {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                vm.loading = false;
            }
            xhr.open('GET', 'api/eclcomputationresult/availableeclComputationResults/0', true);
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
