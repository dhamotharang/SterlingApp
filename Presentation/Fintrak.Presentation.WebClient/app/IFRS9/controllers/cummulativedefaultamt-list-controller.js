/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CummulativeDefaultAmtListController",
        ['$scope', '$state', 'viewModelHelper', 'validator',
            CummulativeDefaultAmtListController]);

    function CummulativeDefaultAmtListController($scope, $state, viewModelHelper, validator) {

        var vm = this;

        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_CummulativeDefaultAmt_Data';
        vm.view = 'cummulativedefaultamt-list-view';
        vm.viewName = 'Cumulative Default Amount Data';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

 
        vm.cummulativeDEFAULTamt = [];
        vm.init = false;
        vm.searchParam = '';
        vm.defaultCount = 5000;
        vm.showInstruction = true ;
        vm.instruction = 'Only top ' + vm.defaultCount + ' records loaded. Use the main search fuctionality to find a specific record by RefNo or Account No.';


        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/cummulativeDEFAULTamt/availableCummulativeDefaultAmts/' + vm.defaultCount, null,
                    function (result) {
                        vm.cummulativeDEFAULTamt = result.data;
                        InitialView();
                        vm.init === true;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        var InitialView = function () {
            InitialGrid();
        };

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#cummulativeDEFAULTamtTable').length > 0) {
                    var exportTable = $('#cummulativeDEFAULTamtTable').DataTable({
                        "lengthMenu": [[20, 50, 100, 200, -1], [20, 50, 100, 200, "All"]],
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
            }, 20);
        };

        vm.loadCummulativeDefaultAmtBySearch = function () {

            if (vm.searchParam === '') {
                toastr.warning('Please input a RefNo ', 'Empty Search');
                return
            }else{

                vm.viewModelHelper.apiGet('api/cummulativeDEFAULTamt/getcummulativedefaultamtbysearch/' + vm.searchParam, null,
                    function (result) {
                        vm.cummulativeDEFAULTamt = result.data;
                        // InitialView();                      
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        //};   

        vm.refresh = function () {
            vm.init = false;
            vm.searchParam = '';
            vm.viewModelHelper.apiGet('api/cummulativeDEFAULTamt/availablecummulativedefaultamts/' + vm.defaultCount, null,
                function (result) {
                    vm.cummulativeDEFAULTamt = result.data;
                    //  InitialView();
                    //  vm.init === true;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        vm.exportAllData = function () {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                vm.loading = false;
            }
            xhr.open('GET', 'api/cummulativeDEFAULTamt/availableCummulativeDefaultAmts/0', true);
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
