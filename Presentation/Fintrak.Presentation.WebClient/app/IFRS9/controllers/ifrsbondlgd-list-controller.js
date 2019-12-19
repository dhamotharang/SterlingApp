/**
 * Created by Tosin on 8/12/2019.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsBondLGDListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IfrsBondLGDListController]);

    function IfrsBondLGDListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrsbondlgd-list-view';
        vm.viewName = 'Bonds LGD Computation Result';
        vm.defaultcount = 5000;
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.searchParam = '';
      
         vm.ifrsbondlgd = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;
        var tabID = 'bondlgdTable';

        var initialize = function(){
            
            if (vm.init === false) {
                vm.loadData();
                   
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


        vm.loadSearch = function () {
          vm.viewModelHelper.apiGet('api/ifrsbondlgd/getRecordByRefNo/' +  vm.searchParam, null,
          function (result) {
            vm.ifrsbondlgd = result.data;
            InitialView('bondlgdTable');
            exportTable.destroy();
          },
          function (result) {
            toastr.error(result.data, 'Fintrak');
          }, null);
      }



      vm.loadData = function () {
          vm.viewModelHelper.apiGet('api/ifrsbondlgd/getallifrsbondlgd/' + vm.defaultcount , null,
          function (result) {
            vm.ifrsbondlgd = result.data;
            InitialView('bondlgdTable');
            vm.searchParam = '';
            if (vm.init === true) {
                exportTable.destroy();
            } else vm.init = true;
          },
          function (result) {
            toastr.error(result.data, 'Fintrak');
          }, null);
      }

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
            xhr.open('GET', 'api/ifrsbondlgd/getallifrsbondlgd/0', true);
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
