/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("PostingDetailContractsListController",
        ['$scope', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        PostingDetailContractsListController]);

    function PostingDetailContractsListController($scope, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS Core';
        vm.view = 'postingdetailcontracts-list-view';
        vm.viewName = 'Posting Details by Contracts';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.postingDetailContracts = [];
        vm.Filter = $stateParams.filter;
        vm.count = 0;

        vm.distinctFilters = [];
        vm.selectedFilters = [{ name: $stateParams.filter }];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        $('.dropdown-menu').find('input').click(function (e) {
            e.stopPropagation();
        });

        $('.dropdown-menu').find('button').click(function (e) {
            e.stopPropagation();
        });

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/postingdetailcontracts/getpostingdetailcontracts?filter=' + vm.Filter , null,
                   function (result) {
                       vm.PostingDetailContracts = result.data;                       

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
                if ($('#PostingDetailContractTable').length > 0) {
                    vm.exportTable = $('#PostingDetailContractTable').DataTable({
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

        var getFilters = function () {
            vm.viewModelHelper.apiGet('api/postingdetailcontracts/getfilters/' + vm.count, null,
                function (result) {
                    vm.distinctFilters = result.data;
                },
                function (result) {
                    toastr.error('Loan Schedule.', 'Fintrak');
                }, null);
        }
        vm.onSelectList = function (itemName) {
            vm.selectedFilters[0] = { name: itemName };
        }
        vm.onClearSelected = function () {
            vm.selectedFilters[0] = { name: '' };
        }
        vm.load = function () {
            vm.Filter = '';
            vm.selectedFilters.forEach(function (e, index) {
                if (index != (vm.selectedFilters.length)) {
                    vm.Filter += e.name + ' ';
                };
            });
            if (vm.Filter === '') {
                toastr.warning('Please input a Filter', 'Empty Reference Number')
                return
            } else {

                vm.viewModelHelper.apiGet('api/postingdetailcontracts/getpostingdetailcontracts?filter=' + vm.Filter , null,
                    function (result) {
                        vm.PostingDetailContracts = result.data;
                        vm.exportTable.destroy();
                        InitialView();

                        toastr.success('Data for the selected filter loaded.', 'Fintrak');
                    },
                    function (result) {
                        toastr.error('Failed to load data for the selected Filter.', 'Fintrak');
                    }, null);
            }
        }


        vm.exportAllData = function (split) {
            split = split ? split : '';
            vm.Filter = '';
            vm.selectedFilters.forEach(function (e, index) {
                if (index != (vm.selectedFilters.length)) {
                    vm.Filter += e.name + ' ';
                };
            });
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                vm.loading = false;
            }
            var Date = (!vm.Date || vm.Date === '') ? '' : vm.Date.toLocaleDateString('sq-AL');
            xhr.open('GET', 'api/postingdetailcontracts/getpostingdetailcontracts?filter=' + split + 'ExportData ' + vm.Filter, true);
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

        getFilters();
        initialize(); 
    }
}());
