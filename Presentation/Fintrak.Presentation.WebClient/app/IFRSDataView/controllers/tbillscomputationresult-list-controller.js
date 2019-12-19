/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TBillsComputationResultListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        TBillsComputationResultListController]);

    function TBillsComputationResultListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_Processed_Data';
        vm.view = 'tbillscomputationresult-list-view';
        vm.viewName = 'TBills Computation Results';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

           vm.classification = '';

           vm.classifications = [
          { Id: 1, Name: 'HTM' },
          { Id: 2, Name: 'HFT' },
          { Id: 3, Name: 'AFS' }
        ];
        
        vm.tBillsComputationResults = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/tbillscomputationresult/availabletbillscomputationresult', null,
                   function (result) {
                       vm.tBillsComputationResults = result.data;
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
                if ($('#tBillsComputationResultTable').length > 0) {
                    var exportTable = $('#tBillsComputationResultTable').DataTable({
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

          vm.getClassification = function () {
                    vm.viewModelHelper.apiGet('api/tbillscomputationresult/gettbillscomputationresult/' + vm.classification, null,
                        function (result) {
                            vm.tBillsComputationResults = result.data;
                           
                            toastr.success('Equity loaded successfully.', 'Fintrak');
                        },
                       function (result) {
                           toastr.error('Fail to load Equity', 'Fintrak Error');
                       }, null);
                }

              vm.exportToExcel = function (tableId) { 
            $(tableId).tableExport({ type: 'excel', escape: 'false' });
        }

        initialize(); 
    }
}());
