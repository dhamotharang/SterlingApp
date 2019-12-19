/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MonthlyDiscountFactorBondListController",
                    ['$scope', '$state','$window', 'viewModelHelper', 'validator',
                        MonthlyDiscountFactorBondListController]);

    function MonthlyDiscountFactorBondListController($scope,$state,$window, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'monthlydiscountfactorbond-list-view';
        vm.viewName = 'Bonds Monthly Discount Factor';
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.monthlydiscountfactorbond = [];
        vm.refNo = '';
       
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/monthlydiscountfactorbond/getmonthlydiscountfactorbonds', null,
                   function (result) {
                       vm.monthlydiscountfactorbond = result.data;
                       InitialView();
                       vm.init = true;
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
                if ($('#monthlydiscountfactorbondTable').length > 0) {
                    exportTable = $('#monthlydiscountfactorbondTable').DataTable({
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


        vm.loadMonthlyDiscountFactorBond = function () {
            vm.monthlydiscountfactorbond = [];

            if (vm.refNo === "") {
                alert("Search cannot be empty")
                return
            };

            vm.viewModelHelper.apiGet('api/monthlydiscountfactorbond/getmonthlydiscountfactorbondbyrefno/' + vm.refNo, null,
                    function (result) {
                                 
                        vm.monthlydiscountfactorbond = result.data;
                        InitialView();
                        exportTable.destroy();
                    },
                        function (result) {
                            toastr.error(result, 'Fintrak Error');
                        }, null);
        }


        vm.refreshMonthlyDiscountFactorBond = function () {
            vm.init = false;
            initialize();
            exportTable.destroy();
        };


        initialize(); 
    }
}());
