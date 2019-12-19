/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MonthlyDiscountFactorListController",
                    ['$scope', '$state','$window', 'viewModelHelper', 'validator',
                        MonthlyDiscountFactorListController]);

    function MonthlyDiscountFactorListController($scope,$state,$window, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'monthlydiscountfactor-list-view';
        vm.viewName = 'Loans Monthly Discount Factor';
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.monthlydiscountfactor = [];
        vm.refNo = '';
       
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/monthlydiscountfactor/getmonthlydiscountfactors', null,
                   function (result) {
                       vm.monthlydiscountfactor = result.data;
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
                if ($('#monthlydiscountfactorTable').length > 0) {
                    exportTable = $('#monthlydiscountfactorTable').DataTable({
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


        vm.loadMonthlyDiscountFactor = function () {

            if (vm.refNo === "") {
                alert("Search cannot be empty")
                return
            };

            vm.viewModelHelper.apiGet('api/monthlydiscountfactor/getmonthlydiscountfactorbyrefno/' + vm.refNo, null,
                    function (result) {
                                 
                        vm.monthlydiscountfactor = result.data;
                        InitialView();
                        exportTable.destroy();
                    },
                        function (result) {
                            toastr.error(result, 'Fintrak Error');
                        }, null);
        }


        vm.refreshMonthlyDiscountFactor = function () {
            vm.init = false;
            initialize();
            exportTable.destoy();
        };


        initialize(); 
    }
}());
