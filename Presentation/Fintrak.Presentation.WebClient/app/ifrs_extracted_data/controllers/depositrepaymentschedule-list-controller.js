/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("DepositRepaymentScheduleListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        DepositRepaymentScheduleListController]);

    function DepositRepaymentScheduleListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'DepositRepaymentSchedule';
        vm.view = 'depositrepaymentschedule-list-view';
        vm.viewName = 'Deposit Repayment Schedule';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.depositrepaymentschedule = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;
        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/depositrepaymentschedule/availabledepositrepaymentschedule', null,
                   function (result) {
                       vm.depositrepaymentschedule = result.data;
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
                if ($('#DepositRepaymentScheduleTable').length > 0) {
                    exportTable = $('#DepositRepaymentScheduleTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" + "RC" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
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

        vm.verify = function () {
            vm.viewModelHelper.apiPost('api/depositrepaymentschedule/getvariancedata', null,
                      function (result) {
                          vm.depositrepaymentschedule = result.data;
                          InitialView();
                          exportTable.destroy();
                      },
                     function (result) {
                         toastr.error(result.data, 'Fintrak Error');
                     }, null);
        }

        vm.LoadAll = function () {
            vm.init = false;
            initialize();
            exportTable.destroy();
        }

        initialize(); 
    }
}());
