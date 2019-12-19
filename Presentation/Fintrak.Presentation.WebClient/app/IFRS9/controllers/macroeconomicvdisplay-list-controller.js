/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MacroeconomicVDisplayListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        MacroeconomicVDisplayListController]);

    function MacroeconomicVDisplayListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'macroeconomicvdisplay-list-view';
        vm.viewName = 'Pivoted Macroeconomic Variables';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];        
        vm.macroeconomicVDisplays = [];
        vm.distinctFHYears = [];
        vm.reloadYears = [];
        vm.reloadgrid = [];
        var vtype = '2'
        vm.VType = '';
        vm.Years = 'None';
        vm.types = [
        { Id: 1, Name: 'Historical' },
        { Id: 2, Name: 'Forcasted' },
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            intializeLookUp();

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/macroeconomicvdisplay/availablemacroeconomicVDisplays', null,
                   function (result) {
                       vm.macroeconomicVDisplays = result.data;
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
                if ($('#macroEconomicVDisplayTable').length > 0) {
                    var exportTable = $('#macroEconomicVDisplayTable').DataTable({
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
            getYears(vtype);
        }

        vm.reloadYears = function (vType) {
            getYears(vType);
        }

        vm.reloadgrid = function () {
            getgriddata();
        }
        var getYears = function (vType) {
            vm.viewModelHelper.apiGet('api/macroeconomicvdisplay/getyears/' + vType, null,
                 function (result) {
                     vm.distinctFHYears = result.data;
                 },
                 function (result) {
                 //    vm.distinctFHYears = [];
                     toastr.error(result.data, 'Fintrak');

                 }, null);
        }
        var getgriddata = function () {
            vm.viewModelHelper.apiGet('api/macroeconomicvdisplay/getmacroevarbyyear/' + vm.Years, null,
                 function (result) {
                     vm.macroeconomicVDisplays = result.data;
                 },
                 function (result) {
                    // vm.macroeconomicVDisplays = [];
                     toastr.error(result.data, 'Fintrak');

                 }, null);
        }
       
        initialize(); 
    }
}());
