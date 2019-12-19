/**
 * Created by Tosin on 8/12/2019.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsProjectedCummDefaultFrqListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IfrsProjectedCummDefaultFrqListController]);

    function IfrsProjectedCummDefaultFrqListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrsprojecteddefaultfrq-list-view';
        vm.viewName = 'Projected Cumulative Default';
        vm.defaultCount = 2000;
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifrsprojectedcummdefault = [];
        vm.init = false;
        vm.showInstruction = true;
       
        var exportTable;
        var tabID = 'projcummdefault';
        vm.instruction = 'Only top ' + vm.defaultcount + ' records loaded.';

        var initialize = function(){
            
            if (vm.init === false) {
                vm.loaddefault();
                  
            }
        }

        var InitialView = function (tableID) {
            InitialGrid(tableID);
        }

        var InitialGrid = function (tableID) {
          tabID = '#' + tableID
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
            }, 50);
        }
        
        vm.loaddefault = function () {
            vm.viewModelHelper.apiGet('api/ifrsprojectedcummdefault/getallifrsprojectedcummdefaults/' + vm.defaultCount,  null,
                function (result) {
                    vm.ifrsprojectedcummdefault = result.data;
                    InitialView('projcummdefault');
                    vm.searchParam = '';
                    if (vm.init === true) {
                        exportTable.destroy();
                    } else vm.init = true;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        vm.loadSectors = function () {
            vm.viewModelHelper.apiGet('api/ifrsprojectedcummdefault/getifrsprojectedcummdefault',  null,
                function (result) {
                    vm.ifrsprojectedcummdefault = result.data;
                    InitialView('ifrsprojectedcummdefaultTable');

                    if (vm.init === true) {
                    exportTable.destroy();
                    }
                    else vm.init = true
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        vm.exportAllData = function () {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                vm.loading = false;
            }
            xhr.open('GET', 'api/ifrsprojectedcummdefault/getallifrsprojectedcummdefaults/0', true);
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
