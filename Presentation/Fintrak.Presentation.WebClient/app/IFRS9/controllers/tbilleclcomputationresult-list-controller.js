/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TBillEclComputationResultListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        TBillEclComputationResultListController]);

    function TBillEclComputationResultListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'tBilleclcomputationresult-list-view';
        vm.viewName = 'Treasury Bills | Commercial Papers ECL Computation Result';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.tBillEclComputationResults = [];
        vm.instrumentType = [
            { id: 1, Name: 'Treasury Bills' },
            { id: 2, Name: 'Commercial Papers' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;
        var tabID = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/tBilleclcomputationresult/availabletBillEclComputationResults/' + 1, null,
                    function (result) {
                        vm.tBillEclComputationResults = result.data;
                        InitialView('tBillEclComputationResultTable');
                        vm.init === true;

                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
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

        vm.loadtBillscomPapers = function (Type) {
            vm.viewModelHelper.apiGet('api/tBilleclcomputationresult/availabletBillEclComputationResults/' + Type, null,
                function (result) {
                    vm.tBillEclComputationResults = result.data;
                    if (Type === 1) {
                        InitialView('tBillEclComputationResultTable');
                    }
                    else if (Type === 2) {
                        InitialView('comPaperEclComputationResultTable');
                    }

                    exportTable.destroy();
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        initialize();
    }
}());
