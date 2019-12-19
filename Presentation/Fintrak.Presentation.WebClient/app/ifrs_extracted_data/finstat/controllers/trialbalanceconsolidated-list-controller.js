/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ConsolidatedTrialBalanceListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        ConsolidatedTrialBalanceListController]);

    function ConsolidatedTrialBalanceListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;
        //ffff
        vm.module = 'Core';
        vm.view = 'consolidatedtrialbalance-list-view'; 
        vm.viewName = 'Consolidated Trial Balance';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.consolidatedTrialBalances = [];
        vm.adjustmentType = 2;
        vm.totalBalance = {};
        vm.adjustmentTypes = [
            { Id: 1, Name: 'GAAP' },
            { Id: 2, Name: 'IFRS' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
               
                var url = '';
                if (vm.adjustmentType === 2)
                    url = 'api/consolidatedtrialbalance/getconsolidatedtrialbalanceIFRS';
                else
                    url = 'api/consolidatedtrialbalance/getconsolidatedtrialbalanceGAAP';

                vm.viewModelHelper.apiGet(url, null,
                   function (result) {
                       vm.consolidatedTrialBalances = result.data;
                       //vm.totalBalance = result.data.TranslatedBalance;
                       
                       toastr.success('Consolidated Trial balance data loaded.', 'Fintrak');
                       InitialView();
                   },
                   function (result) {
                       toastr.error('Fail to load Consolidated trial balance data.', 'Fintrak');
                   }, null);
                vm.init === true;
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#consolidatedTrialBalanceTable').length > 0) {
                    var exportTable = $('#consolidatedTrialBalanceTable').DataTable({
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

        vm.load = function () {
            var url = '';
                       
            if (vm.adjustmentType === 2)
                url = 'api/consolidatedtrialbalance/getconsolidatedtrialbalanceIFRS';
            else
                url = 'api/consolidatedtrialbalance/getconsolidatedtrialbalanceGAAP';

            vm.viewModelHelper.apiGet(url, null,
               function (result) {
                   //vm.consolidatedTrialBalances = result.data;
                   vm.consolidatedTrialBalances = result.data;
                   //vm.totalBalance = result.data.TranslatedBalance;
                   toastr.success('Consolidated Trial balance data loaded.', 'Fintrak');
               },
               function (result) {
                   toastr.error('Fail to load Consolidated trial balance data.', 'Fintrak');
               }, null);
        }

        initialize(); 
    }
}());
