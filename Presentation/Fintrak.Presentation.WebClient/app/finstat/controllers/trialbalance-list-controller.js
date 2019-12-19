/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TrialBalanceListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        TrialBalanceListController]);

    function TrialBalanceListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;
        //ffff
        vm.module = 'Core';
        vm.view = 'trialbalance-list-view';
        vm.viewName = 'Trial Balance';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.trialBalances = [];
        vm.adjustmentType = 1;
        vm.totalBalance = {};
        vm.adjustmentTypes = [
            { Id: 1, Name: 'GAAP' },
            { Id: 2, Name: 'IFRS' }
        ];
        vm.selectedBranch = '';
        vm.branches = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;

        var initialize = function() {
            intializeLookUp();
            //if (vm.init === false) {

            //    var url = '';
            //    if (vm.adjustmentType === 2)
            //        url = 'api/trialBalance/getifrstrialbalancebybranch/' + vm.selectedBranch;
            //    else
            //        url = 'api/trialBalance/getgaptrialbalancebybranch/' + vm.selectedBranch;

            //    vm.viewModelHelper.apiGet(url,
            //        null,
            //        function(result) {
            //            vm.trialBalances = result.data.TrialBalance;
            //            vm.totalBalance = result.data.TranslatedBalance;

            //            toastr.success('Trial balance data loaded.', 'Fintrak');
            //            InitialView();
            //        },
            //        function(error) {
            //            toastr.error(error, 'Fail to load trial balance data.');
            //            return;
            //        },
            //        null);
            //    vm.init === true;
            //}
        };

        var intializeLookUp = function() {
            vm.viewModelHelper.apiGet('api/branch/availablebranches',
                null,
                (result) => {
                    vm.branches = result.data;
                    vm.selectedBranch = vm.branches[0].Code;

                    if (vm.init === false) {

                        var url = '';
                        if (vm.adjustmentType === 2)
                            url = 'api/trialBalance/getifrstrialbalancebybranch/' + vm.selectedBranch;
                        else
                            url = 'api/trialBalance/getgaptrialbalancebybranch/' + vm.selectedBranch;

                        vm.viewModelHelper.apiGet(url,
                            null,
                            function (result) {
                                vm.trialBalances = result.data.TrialBalance;
                                vm.totalBalance = result.data.TranslatedBalance;
                                vm.BSAutoBalance = result.data.BSAutoBalance;
                                vm.OBSAutoBalance = result.data.OBSAutoBalance;
                                vm.ifrsAutoBalance = result.data.IFRSAutoBalance;
                                toastr.success('Trial balance data loaded.', 'Fintrak');
                               // InitialView();
                            },
                            function (error) {
                                toastr.error(error, 'Fail to load trial balance data.');
                                return;
                            },
                            null);
                        vm.init === true;
                    }
                },
                (error) => {
                    toastr.error(error, 'Unable to fetch branches');
                },
                null);
        };

        var InitialView = function() {
            InitialGrid();
        };

        var InitialGrid = function() {
            setTimeout(function() {

                    // data export
                    if ($('#trialBalanceTable').length > 0) {
                        exportTable = $('#trialBalanceTable').DataTable({
                            "lengthMenu": [[20, 50, 100, -1], [20, 50, 100, "All"]],
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

        vm.load = function() {
            var url = '';
            if (vm.adjustmentType === 2)
                url = 'api/trialBalance/getifrstrialbalancebybranch/' + vm.selectedBranch;
            else
                url = 'api/trialBalance/getgaptrialbalancebybranch/' + vm.selectedBranch;

            vm.viewModelHelper.apiGet(url,
                null,
                function(result) {
                    //vm.trialBalances = result.data;
                    vm.trialBalances = result.data.TrialBalance;
                    vm.totalBalance = result.data.TranslatedBalance;
                    vm.BSAutoBalance = result.data.BSAutoBalance;
                    vm.OBSAutoBalance = result.data.OBSAutoBalance;
                    vm.ifrsAutoBalance = result.data.IFRSAutoBalance;
                    toastr.success('Trial balance data loaded.', 'Fintrak');
                   // InitialView();
                   // exportTable.destroy();
                },
                function(error) {
                    toastr.error(error, 'Fail to load trial balance data.');
                    return;
                },
                null);
        };

        initialize(); 
    }
}());
