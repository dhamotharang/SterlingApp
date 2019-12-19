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
        vm.companies = [];
        vm.consolidatedTrialBalances = [];
        vm.adjustmentType = 2;
        vm.totalBalance = {};
         
        vm.adjustmentTypes = [
            { Code: 1, Name: 'GAAP' },
            { Id: 2, Name: 'IFRS' }
        ];
   

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){
            intializeLookUp();
            if (vm.init === false) {
               
                var url = '';
                if (vm.adjustmentType === 2)
                    url = 'api/consolidatedtrialbalance/getconsolidatedtrialbalanceIFRS/' + vm.selectedCompany;
                else
                    url = 'api/consolidatedtrialbalance/getconsolidatedtrialbalanceGAAP/' + vm.selectedCompany;

                vm.viewModelHelper.apiGet(url, null,
                   function (result) {
                       vm.consolidatedTrialBalances = result.data.TrialBalance;
                       vm.totalBalance = result.data.TranslatedBalance;
                       vm.BSAutoBalance = result.data.BSAutoBalance;
                       vm.OBSAutoBalance = result.data.OBSAutoBalance;
                       vm.ifrsAutoBalance = result.data.IFRSAutoBalance;
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
        //var intializeLookUp = function () {
        //  // getCompanies();
        //    vm.viewModelHelper.apiGet('api/branch/availablebranches',
        //}
        //vm.load = function () {
        //    var url = '';
                       
        //    if (vm.adjustmentType === 2)
        //        url = 'api/consolidatedtrialbalance/getconsolidatedtrialbalanceIFRS';
        //    else
        //        url = 'api/consolidatedtrialbalance/getconsolidatedtrialbalanceGAAP';

        //    vm.viewModelHelper.apiGet(url, null,
        //       function (result) {
        //           //vm.consolidatedTrialBalances = result.data;
        //           vm.consolidatedTrialBalances = result.data.TrialBalance;
        //           vm.totalBalance = result.data.TranslatedBalance;
        //           vm.BSAutoBalance = result.data.BSAutoBalance;
        //           vm.OBSAutoBalance = result.data.OBSAutoBalance;
        //           vm.ifrsAutoBalance = result.data.IFRSAutoBalance;

        //           toastr.success('Consolidated Trial balance data loaded.', 'Fintrak');
        //       },
        //       function (result) {
        //           toastr.error('Fail to load Consolidated trial balance data.', 'Fintrak');
        //       }, null);
        //}
        var intializeLookUp = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies',
                null,
                (result) => {
                    vm.companies = result.data;
                    vm.selectedCompany = vm.companies[0].Code;

                    if (vm.init === false) {

                        var url = '';
                        if (vm.adjustmentType === 2)
                            url = 'api/consolidatedtrialbalance/getconsolidatedtrialbalanceIFRS/' + vm.selectedCompany;
                        else
                            url = 'api/consolidatedtrialbalance/getconsolidatedtrialbalanceGAAP/' + vm.selectedCompany;

                        vm.viewModelHelper.apiGet(url,
                            null,
                            function (result) {
                                vm.consolidatedTrialBalances = result.data.TrialBalance;
                                vm.totalBalance = result.data.TranslatedBalance;
                                vm.BSAutoBalance = result.data.BSAutoBalance;
                                vm.OBSAutoBalance = result.data.OBSAutoBalance;
                                vm.ifrsAutoBalance = result.data.IFRSAutoBalance;
                                toastr.success('Consolidated Trial balance data loaded.', 'Fintrak');
                               // InitialView();
                            },
                            function (error) {
                                toastr.error(error, 'Fail to load Consolidated trial balance data.');
                                return;
                            },
                            null);
                        vm.init === true;
                    }
                },
                (error) => {
                    toastr.error(error, 'Fail to load Consolidated trial balance data');
                },
                null);
        };
        var getCompanies = () => {
            vm.viewModelHelper.apiGet('api/company/availablecompanies',
                null,
                function (result) {
                    vm.companies = result.data;
                    vm.firstcoy = result.data[0].Code
                },
                function (error) {
                    toastr.error(error.data, 'Fintrak');
                },
                null);
        };
        
        vm.load = function () {
            var url = '';
            if (vm.adjustmentType === 2)
                url = 'api/consolidatedtrialbalance/getconsolidatedtrialbalanceIFRS/' + vm.selectedCompany;
            else
                url = 'api/consolidatedtrialbalance/getconsolidatedtrialbalanceGAAP/' + vm.selectedCompany;

            vm.viewModelHelper.apiGet(url,
                null,
                function (result) {
                    //vm.trialBalances = result.data;
                    vm.consolidatedTrialBalances = result.data.TrialBalance;
                    vm.totalBalance = result.data.TranslatedBalance;
                    vm.BSAutoBalance = result.data.BSAutoBalance;
                    vm.OBSAutoBalance = result.data.OBSAutoBalance;
                    vm.ifrsAutoBalance = result.data.IFRSAutoBalance;
                    toastr.success('Trial balance data loaded.', 'Fintrak');
                   // InitialView();
                    // exportTable.destroy();
                },
                function (error) {
                    toastr.error(error, 'Fail to load trial balance data.');
                    return;
                },
                null);
        };

        initialize(); 
    }
}());
