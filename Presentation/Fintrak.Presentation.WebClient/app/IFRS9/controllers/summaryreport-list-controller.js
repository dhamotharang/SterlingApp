/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SummaryReportController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        SummaryReportController]);

    function SummaryReportController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'summaryreport-list-view';
        vm.viewName = 'Summary Report';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.myDataSource = [];
        $scope.summaryReport = {};
        var bucketSource = {};
        vm.dts = [];
        vm.summaryreports = [];
        vm.summaryreport = {};
        vm.summaryreportchart = [];
        vm.bucketexposure = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                intializeLookUp();

                InitialView();
            }
        }

        var InitialView = function () {

            InitialGrid();

        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#summaryreportTable2').length > 0) {
                    var exportTable = $('#summaryreportTable2').DataTable({
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
        var intializeLookUp = function () {
            getSummaryReport();
            
        }
       
        var getSummaryReport = function () {

            vm.viewModelHelper.apiGet('api/ifrs9dashboard/summaryreport', null,
                function (result) {
                    vm.summaryreport = result.data.SummaryReport;
                    vm.summaryreportchart = result.data.SummaryReportChart;
                    summaryreportchart();
                },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
        }

        var summaryreportchart = function () {
            var jdata;
            jdata = JSON.parse(vm.summaryreportchart); //
            $scope.summaryReport = {
                chart: {
                    caption: "Summary Report",
                    subCaption: "Loan Assessment Report",
                    numberPrefix: "N",
                    theme: "ocean"
                },
                "categories": [
                {
                    "category": [
                        { "label": "Performing" },
                        { "label": "UnderPerforming" },
                        { "label": "NonPerforming" },
                    ]
                }
                ],
                // data: jdata
                "dataset": [
               {
                   "seriesname": "Individual Assessment",
                   "data": [
                       { "value": "2325309" },
                       { "value": "60351246" },
                       { "value": "6027456" },
                   ]
               },
               {
                   "seriesname": "Collective Assessment",
                   "data": [
                       { "value": "639043" },
                       { "value": "3525085" },
                       { "value": "6052508" },
                   ]
               }
                ]
            };
        }

     

        initialize();
    }
}());
