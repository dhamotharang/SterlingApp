/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ifrsfinalretailoutputListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IfrsFinalRetailOutputListController]);

    function IfrsFinalRetailOutputListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrsfinalretailoutput-list-view';
        vm.viewName = 'Final Retail ECL Output';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.ifrsFinalRetailOutputs = [];
        vm.disabled = false;
        vm.search;
        var exportTable;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet(
                    'api/ifrsfinalretailoutput/getAllIfrsFinalRetailOutputs',
                    null,
                    function (result) {
                        vm.ifrsFinalRetailOutputs = result.data;
                        InitialView();
                        vm.init = true;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    },
                    null
                );
            }
        }

        vm.getIfrsFinalRetailOutput = function (accountNo) {
            vm.disabled = true;
            if (accountNo && accountNo.length > 2) {
                vm.viewModelHelper.apiGet('api/ifrsfinalretailoutput/getIfrsFinalRetailOutputByAccountNo/' + accountNo, null,
                   function (result) {
                       vm.ifrsFinalRetailOutputs = result.data;
                       InitialView();
                       exportTable.destroy();
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
                if ($('#ifrsFinalRetailOutputTable').length > 0) {
                    exportTable = $('#ifrsFinalRetailOutputTable').DataTable({
                        "lengthMenu": [[20, 50, 100, -1], [20, 50, 100, "All"]],
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

        initialize();

        vm.reloadPage = function () {
            vm.search = false;
            vm.init = false;
            vm.accountNo = null;
            initialize();
            exportTable.destroy();
        };

        vm.exportAllData = function () {
            var xhr = new XMLHttpRequest();
            xhr.open('GET', 'api/ifrsfinalretailoutput/getAllIfrsFinalRetailOutputs', true);
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
            xhr.send()
        }
    }
}());
