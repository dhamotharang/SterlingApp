/**
 * Created by Dara on 23/03/2018.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("InvestmentOthersECLListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        InvestmentOthersECLListController]);

    function InvestmentOthersECLListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'investmentothersecl-list-view';
        vm.viewName = 'Investment and Others ECL';
        var exportTable;
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.refNo = '';
        vm.investmentothersecls = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/investmentothersecl/availableinvestmentothersecls', null,
                   function (result) {
                       vm.investmentothersecls = result.data;
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
                if ($('#investmentotherseclTable').length > 0) {
                    exportTable = $('#investmentotherseclTable').DataTable({
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
            }, 50);
        }


        vm.loadInvestmentOthersECLs = function () {

            vm.viewModelHelper.apiGet('api/investmentothersecl/getinvestmentotherseclsbyrefno/' + vm.refNo, null,
                               function (result) {
                                   vm.investmentothersecls = result.data;
                                   InitialView();
                                   exportTable = exportTable.destroy();
                               },
                                       function (result) {
                                           toastr.error(result, 'Fintrak Error');
                                       }, null);
        }

        vm.refresh = function () {
            vm.init = false;
            vm.refNo = '';
            exportTable = exportTable.destroy();
            initialize();
        }


        initialize(); 
    }
}());
