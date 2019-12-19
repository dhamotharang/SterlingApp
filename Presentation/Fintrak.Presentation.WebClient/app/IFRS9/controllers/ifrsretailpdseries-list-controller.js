/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsRetailPdSeriesListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IfrsRetailPdSeriesListController]);

    function IfrsRetailPdSeriesListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrsretailpdseries-list-view';
        vm.viewName = 'Retail Pd Series';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.ifrsRetailPdSeries = [];
        vm.queryOptions = {};
        vm.currentPage;
        vm.pageSize;
        vm.disabled = false;
        vm.search = false;
        vm.category = '';

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet(
                    'api/ifrsretailpdseries/getAllIfrsRetailPdSeries?CurrentPage=&init=true',
                    null,
                    function (result) {
                        vm.ifrsRetailPdSeries = result.data.Results;
                        vm.queryOptions = result.data.QueryOptions;
                        vm.currentPage = result.data.QueryOptions.CurrentPage;
                        vm.pageSize = result.data.QueryOptions.PageSize;
                        if (vm.queryOptions.FilterOption === '1') vm.queryOptions.FilterOption = '';
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
                    vm.ifrsRetailPdSeries = result.data.Results;
                    vm.queryOptions = result.data.QueryOptions;
                    vm.currentPage = result.data.QueryOptions.CurrentPage;
                    if (vm.queryOptions.FilterOption === '1') vm.queryOptions.FilterOption = '';
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
                    vm.ifrsRetailPdSeries = result.data.Results;
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
                    vm.ifrsRetailPdSeries = result.data.Results;
                    vm.queryOptions = result.data.QueryOptions;
                    vm.pageSize = vm.queryOptions.PageSize;
                    vm.currentPage = vm.queryOptions.CurrentPage;
                    if (vm.queryOptions.FilterOption === '1') vm.queryOptions.FilterOption = '';
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
                if ($('#ifrsRetailPdSeriesTable1').length > 0) {
                    var exportTable = $('#ifrsRetailPdSeriesTable').DataTable({
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
            return 'api/ifrsretailpdseries/getAllIfrsRetailPdSeries?CurrentPage=' + vm.queryOptions.CurrentPage
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

        initialize(); 
    }
}());
