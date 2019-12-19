/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanDetailListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        LoanDetailListController]);

    function LoanDetailListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LoanDetail_Data';
        vm.view = 'loandetail-list-view';
        vm.viewName = 'Loan Detail Data';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.defaultCount = 500;
        
        vm.loanDetail = [];
        vm.serachParam = '';
        vm.init = false;
        vm.showInstruction = true;
        vm.instruction = 'Only top ' + vm.defaultCount + ' records loaded. Use the main search fuctionality to find a specific record by by RefNo or Account No.';
        var exportTable;

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/rawloandetail/availablerawloandetail/' +vm.defaultCount, null,
                   function (result) {
                       vm.loanDetail = result.data;
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
                if ($('#loanDetailTable').length > 0) {
                    exportTable = $('#loanDetailTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        searching: false,
                        scrollX: true,
                        //"deferRender": true,
                        //sDom: "T<'clearfix'>" +
                        //"<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                        //"t" +
                        //"<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            }, 50);
        }

        vm.loadLoanDetailBySearch = function () {

            if (vm.serachParam === '') {
                toastr.warning('Please input a RefNo or Account No', 'Empty Search')
                return
            }
            else {

            vm.viewModelHelper.apiGet('api/rawloandetail/getloandetailbysearch/' + vm.serachParam, null,
                function (result) {
                    vm.loanDetail = result.data;
                    InitialView();
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

        vm.exportAllData = function () {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                vm.loading = false;
            }
            xhr.open('GET', 'api/rawloandetail/availablerawloandetail/0' , true);
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
