/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanDetailedInfoListController",
        ['$scope', '$state', '$stateParams', 'viewModelHelper', 'validator',
                LoanDetailedInfoListController]);

    function LoanDetailedInfoListController($scope, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_Processed_Data';
        vm.view = 'loandetailedinfo-list-view';
        vm.viewName = 'Loan information in detail';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.loandetailedinfo = {
            loanpry: [],
            loandetails: [],
            loanschedule: [],
            loaneclresult: []
        };

        vm.distinctRefNos = [];
        vm.count = 1000;
        vm.selectedRefNos = [{ name: $stateParams.refno }];

        vm.RefNo = $stateParams.refno;
        vm.Date = new Date();
        vm.init = false;
        vm.showInstruction = true;
        vm.instruction = 'Use the dropdown to search for the desired contract to load. ' +
            'Select a date to load the daily amortized cost schedule for 30 days prior and 30 days after. ';

        $('.dropdown-menu').find('input').click(function (e) {
            e.stopPropagation();
        });

        $('.dropdown-menu').find('button').click(function (e) {
            e.stopPropagation();
        });

        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            $($.fn.dataTable.tables(true)).DataTable()
                .columns.adjust();
        });

        var intializeLookUp = function () {
            getRefNos();
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#loanscheduleTable').length > 0) {
                    vm.exportTable1 = $('#loanscheduleTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        searching: true,
                        scrollX: true,
                        //sDom: "T<'clearfix'>" +
                        //    "<'row'<'col-sm-6'l><'col-sm-6'f>r>" + "RC" +
                        //    "t" +
                        //    "<'row'<'col-sm-6'i><'col-sm-6'p>>",
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

                // data export
                if ($('#loancashflowTable').length > 0) {
                    vm.exportTable2 = $('#loancashflowTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        searching: true,
                        scrollX: true,
                        //sDom: "T<'clearfix'>" +
                        //    "<'row'<'col-sm-6'l><'col-sm-6'f>r>" + "RC" +
                        //    "t" +
                        //    "<'row'<'col-sm-6'i><'col-sm-6'p>>",
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

                // data export
                if ($('#loanmonthlyeadTable').length > 0) {
                    vm.exportTable3 = $('#loanmonthlyeadTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        searching: true,
                        scrollX: true,
                        //sDom: "T<'clearfix'>" +
                        //    "<'row'<'col-sm-6'l><'col-sm-6'f>r>" + "RC" +
                        //    "t" +
                        //    "<'row'<'col-sm-6'i><'col-sm-6'p>>",
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

                // data export
                if ($('#loaneclcomputationresultTable').length > 0) {
                    vm.exportTable4 = $('#loaneclcomputationresultTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        searching: true,
                        scrollX: true,
                        //sDom: "T<'clearfix'>" +
                        //    "<'row'<'col-sm-6'l><'col-sm-6'f>r>" + "RC" +
                        //    "t" +
                        //    "<'row'<'col-sm-6'i><'col-sm-6'p>>",
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

            }, 50);
        }

        var initialize = function () {

            if (vm.init === false) {

                intializeLookUp();
                if (vm.RefNo !== '' && vm.RefNo !== undefined && vm.RefNo !== null) {
                    vm.viewModelHelper.apiGet('api/loandetailedinfo/getloandetailedinfo?refno=' + vm.RefNo + '&Date=' + vm.Date.toLocaleDateString('sq-AL'), null,
                        function (result) {
                            vm.loandetailedinfo = result.data;
                            vm.AmortAdj = vm.loandetailedinfo.loaneclresult.AmortizedCost - (vm.loandetailedinfo.loandetails.PrincipalOutstandingBal + vm.loandetailedinfo.loandetails.Interest_Receiv_Pay_UnEarn);
                            vm.loandetailedinfo.loaneclresult.EIR = vm.loandetailedinfo.loaneclresult.EIR * 100;
                            vm.loan = !(vm.loandetailedinfo.loandetails.AccountNo === vm.loandetailedinfo.loandetails.RefNo);

                            vm.init = true;
                            InitialView();
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
            }
        }

        var getRefNos = function () {
            vm.viewModelHelper.apiGet('api/loandetailedinfo/getrefnos/' + vm.count, null,
                function (result) {
                    vm.distinctRefNos = result.data;
                },
                function (result) {
                    toastr.error('Loan Schedule.', 'Fintrak');
                }, null);
        }
        vm.onSelectList = function (itemName) {
            vm.selectedRefNos[0] = { name: itemName };
        }
        vm.onClearSelected = function () {
            vm.selectedRefNos[0] = { name: '' };
        }
        vm.load = function () {
            vm.RefNo = '';
            vm.selectedRefNos.forEach(function (e, index) {
                if (index != (vm.selectedRefNos.length)) {
                    vm.RefNo += e.name + ' ';
                };
            });
            if (vm.RefNo === '') {
                toastr.warning('Please input a RefNo', 'Empty Reference Number')
                return
            }
            else if (!vm.Date || vm.Date === '') {
                vm.Date = new Date();
            }
            else {

                vm.viewModelHelper.apiGet('api/loandetailedinfo/getloandetailedinfo?refno=' + vm.RefNo + '&Date=' + vm.Date.toLocaleDateString('sq-AL'), null,
                    function (result) {
                        vm.loandetailedinfo = result.data;
                        vm.AmortAdj = vm.loandetailedinfo.loaneclresult.AmortizedCost - (vm.loandetailedinfo.loandetails.PrincipalOutstandingBal + vm.loandetailedinfo.loandetails.Interest_Receiv_Pay_UnEarn);
                        vm.loandetailedinfo.loaneclresult.EIR = vm.loandetailedinfo.loaneclresult.EIR * 100;
                        vm.loan = !(vm.loandetailedinfo.loandetails.AccountNo === vm.loandetailedinfo.loandetails.RefNo);
                        vm.exportTable1.destroy();
                        vm.exportTable2.destroy();
                        vm.exportTable3.destroy();
                        vm.exportTable4.destroy();
                        InitialView();

                        toastr.success('Data for the selected RefNo and Date Range loaded.', 'Fintrak');
                    },
                    function (result) {
                        toastr.error('Failed to load data for the selected RefNo and Date Range.', 'Fintrak');
                    }, null);
            }
        }

                

        initialize();
    }
}());
