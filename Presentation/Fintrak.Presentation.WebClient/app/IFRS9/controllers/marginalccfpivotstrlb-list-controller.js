/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MarginalCCFPivotSTRLBListController",
            ['$scope', '$state', 'viewModelHelper', 'validator',
                MarginalCCFPivotSTRLBListController]);

    function MarginalCCFPivotSTRLBListController($scope, $state, viewModelHelper, validator) {

        var vm = this;

        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_MarginalCCFPivotSTRLB_Data';
        vm.view = 'marginalccfpivotstrlb-list-view';
        vm.viewName = 'Marginal CCF Pivot STRLB Data';

        //Pagination Proper
        var tabID = 'marginalccfPIVOTstrlbTable';
        var exportTable;
        //End

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];


        vm.marginalccfPIVOTstrlb = [];
        vm.init = false;
        vm.searchParam = '';
        vm.defaultCount = 2000;
        vm.showInstruction = true;
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
            vm.viewModelHelper.apiGet('api/marginalccfPIVOTstrlb/availableMarginalCCFPivotSTRLBs/' + vm.defaultCount, null,
                function (result) {
                    vm.marginalccfPIVOTstrlb = result.data;
                    InitialView('marginalccfPIVOTstrlbTable');
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

        vm.loadMarginalCCFPivotSTRLBBySearch = function () {

            if (vm.searchParam === '') {
                toastr.warning('Please input a RefNo ', 'Empty Search');
                return
            } else {

                vm.viewModelHelper.apiGet('api/marginalccfPIVOTstrlb/getmarginalccfpivotstrlbbysearch/' + vm.searchParam, null,
                    function (result) {
                        vm.marginalccfPIVOTstrlb = result.data;
                        InitialView('marginalccfPIVOTstrlbTable');
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
