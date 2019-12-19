/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsCorporatePdSeriesListController",
            ['$scope', '$state', 'viewModelHelper', 'validator',
                IfrsCorporatePdSeriesListController]);

    function IfrsCorporatePdSeriesListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrscorporatepdseries-list-view';
        vm.viewName = 'Corporate Pd Series';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifrsCorporatePdSeries = [];
        vm.queryOptions = {};
        vm.currentPage;
        vm.pageSize;
        vm.disabled = false;
        vm.search = false;
        vm.category = '';

        vm.init = false;
        vm.showInstruction = false;
        vm.loading = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet(
                    'api/ifrscorporatepdseries/getAllIfrsCorporatePdSeries?CurrentPage=&init=true',
                    null,
                    function (result) {
                        vm.ifrsCorporatePdSeries = result.data.Results;
                        vm.queryOptions = result.data.QueryOptions;
                        vm.currentPage = result.data.QueryOptions.CurrentPage;
                        vm.pageSize = result.data.QueryOptions.PageSize;
                        if (vm.queryOptions.FilterOption == '1') vm.queryOptions.FilterOption = '';
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

        vm.goToPage = function (dest) {
            if (dest === 'next') {
                vm.queryOptions.CurrentPage = vm.queryOptions.CurrentPage + 1;
            } else if (dest === 'prev') {
                vm.queryOptions.CurrentPage = vm.queryOptions.CurrentPage - 1;
            } else {
                vm.queryOptions.CurrentPage = parseInt(vm.queryOptions.CurrentPage);
            }
            vm.viewModelHelper.apiGet(
                urlHelper(),
                null,
                function (result) {
                    vm.ifrsCorporatePdSeries = result.data.Results;
                    vm.queryOptions = result.data.QueryOptions;
                    vm.currentPage = result.data.QueryOptions.CurrentPage;
                    if (vm.queryOptions.FilterOption == '1') vm.queryOptions.FilterOption = '';
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null
            );
        }


        vm.searchBy = function (searchField, fieldType) {

            if (!vm.queryOptions.FilterOption) {
                alert("Search cannot be empty")
                return
            };
            vm.queryOptions.init = 1;
            vm.queryOptions.FilterField = searchField;
            vm.queryOptions.FilterFieldType = fieldType;

            vm.viewModelHelper.apiGet(
                urlHelper(),
                null,
                function (result) {
                    vm.ifrsCorporatePdSeries = result.data.Results;
                    vm.queryOptions = result.data.QueryOptions;
                    vm.search = true;
                    vm.pageSize = vm.queryOptions.PageSize;
                    vm.currentPage = vm.queryOptions.CurrentPage;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null
            );
        }

        vm.reloadPage = function () {
            vm.search = false;
            vm.init = false;
            initialize();
        };

        vm.resizePage = function () {
            if (vm.queryOptions.PageSize > 200) {
                vm.queryOptions.PageSize = 200;
            } else if (vm.queryOptions.PageSize < 20) {
                vm.queryOptions.PageSize = 20;
            }

            vm.queryOptions.init = 1;

            vm.viewModelHelper.apiGet(
                urlHelper(),
                null,
                function (result) {
                    vm.ifrsCorporatePdSeries = result.data.Results;
                    vm.queryOptions = result.data.QueryOptions;
                    vm.pageSize = vm.queryOptions.PageSize;
                    vm.currentPage = vm.queryOptions.CurrentPage;
                    if (vm.queryOptions.FilterOption == '1') vm.queryOptions.FilterOption = '';
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null
            );
        };



        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#ifrsCorporatePdSeriesTable1').length > 0) {
                    var exportTable = $('#ifrsCorporatePdSeriesTable').DataTable({
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

        var urlHelper = function () {
            var urlFilterOption = '';
            if (vm.queryOptions.FilterOption) urlFilterOption = '&FilterOption=' + vm.queryOptions.FilterOption;
            return 'api/ifrscorporatepdseries/getAllIfrsCorporatePdSeries?CurrentPage=' + vm.queryOptions.CurrentPage
                + '&PageSize=' + vm.queryOptions.PageSize
                + '&SortField=' + vm.queryOptions.SortField
                + '&SortOrder=' + vm.queryOptions.SortOrder
                + '&TotalRecords=' + vm.queryOptions.TotalRecords
                + '&TotalPages=' + vm.queryOptions.TotalPages
                + '&FilterField=' + vm.queryOptions.FilterField
                + urlFilterOption
                + '&FilterFieldType=' + vm.queryOptions.FilterFieldType
                + '&init=' + vm.queryOptions.init
        }

        vm.exportAllData = function () {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                vm.loading = false;
            }
            xhr.open('GET', 'api/ifrscorporatepdseries/exportAllIfrsCorporatePdSeries', true);
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
