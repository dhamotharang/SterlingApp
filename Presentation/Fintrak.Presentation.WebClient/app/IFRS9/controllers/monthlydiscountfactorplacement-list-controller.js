/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MonthlyDiscountFactorPlacementListController",
                    ['$scope', '$state','$window', 'viewModelHelper', 'validator',
                        MonthlyDiscountFactorPlacementListController]);

    function MonthlyDiscountFactorPlacementListController($scope,$state,$window, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'monthlydiscountfactorplacement-list-view';
        vm.viewName = 'Placements Monthly Discount Factor';
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.monthlydiscountfactorplacement = [];
        vm.refNo = '';
       
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/monthlydiscountfactorplacement/getmonthlydiscountfactorplacements', null,
                   function (result) {
                       vm.monthlydiscountfactorplacement = result.data;
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
                if ($('#monthlydiscountfactorplacementTable').length > 0) {
                    exportTable = $('#monthlydiscountfactorplacementTable').DataTable({
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


        vm.loadMonthlyDiscountFactorPlacement = function () {
            vm.monthlydiscountfactorplacement = [];

            if (vm.refNo === "") {
                alert("Search cannot be empty")
                return
            };

            vm.viewModelHelper.apiGet('api/monthlydiscountfactorplacement/getmonthlydiscountfactorplacementbyrefno/' + vm.refNo, null,
                    function (result) {
                                 
                        vm.monthlydiscountfactorplacement = result.data;
                        InitialView();
                        exportTable.destroy();
                    },
                        function (result) {
                            toastr.error(result, 'Fintrak Error');
                        }, null);
        }


        vm.refreshMonthlyDiscountFactorPlacement = function () {
            initialize();
        };


        initialize(); 
    }
}());
