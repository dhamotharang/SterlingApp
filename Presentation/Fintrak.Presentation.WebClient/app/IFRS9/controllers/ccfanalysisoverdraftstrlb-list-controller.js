/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CcfAnalysisOverDraftSTRLBListController",
            ['$scope', '$state', 'viewModelHelper', 'validator',
                CcfAnalysisOverDraftSTRLBListController]);

    function CcfAnalysisOverDraftSTRLBListController($scope, $state, viewModelHelper, validator) {

        var vm = this;

        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_CcfAnalysisOverDraftSTRLB_Data';
        vm.view = 'ccfanalysisoverdraftstrlb-list-view';
        vm.viewName = 'CCF Analysis for Overdraft Facilities';
        //Pagination Proper
        var tabID = 'ccfanalysisOVERDRAFTstrlbTable';
        var exportTable;
        //End
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];


        vm.ccfanalysisOVERDRAFTstrlb = [];
        vm.init = false;
        vm.searchParam = '';
        vm.defaultCount = 2000;
        vm.showInstruction = false;
        vm.instruction = 'Only top ' + vm.defaultCount + ' records loaded. Use the main search fuctionality to find a specific record by RefNo or Account No.';


        var initialize = function () {
            if (vm.init === false) {
                vm.loaddefault();
            }

        };
        var InitialView = function (tabID) {
            InitialGrid(tabID);
        }

        vm.loaddefault = function () {
            vm.viewModelHelper.apiGet('api/ccfanalysisOVERDRAFTstrlb/availableCcfAnalysisOverDraftSTRLBs/' + vm.defaultCount, null,
                function (result) {
                    vm.ccfanalysisOVERDRAFTstrlb = result.data;
                    InitialView('ccfanalysisOVERDRAFTstrlbTable');
                    vm.searchParam = '';
                    if (vm.init === true) {
                        exportTable.destroy();
                    } else vm.init = true;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }




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

        vm.loadCcfAnalysisOverDraftSTRLBBySearch = function () {

            if (vm.searchParam === '') {
                toastr.warning('Please input a RefNo ', 'Empty Search');
                return
            } else {

                vm.viewModelHelper.apiGet('api/ccfanalysisOVERDRAFTstrlb/getccfanalysisoverdraftstrlbbysearch/' + vm.searchParam, null,
                    function (result) {
                        vm.ccfanalysisOVERDRAFTstrlb = result.data;
                        InitialView('ccfanalysisOVERDRAFTstrlbTable');
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



        initialize();
    }
}());
