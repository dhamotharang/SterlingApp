/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MacroEconomicsNPLListController",
                    ['$scope', '$state','$window', 'viewModelHelper', 'validator',
                        MacroEconomicsNPLListController]);

    function MacroEconomicsNPLListController($scope,$state,$window, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'macroeconomicsnpl-list-view';
        vm.viewName = 'MacroEconomics NPL';
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.macroeconomicsnpl = [];
       
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.scenario = '';

        vm.scenarios = [
        { Id: 1, Name: 'Best' },
        { Id: 2, Name: 'Optimistic' },
        { Id: 3, Name: 'Downturn' }
        ];

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/macroeconomicsnpl/availablemacroeconomicsnpls', null,
                   function (result) {
                       vm.macroeconomicsnpl = result.data;
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
          //  InitialGrid2();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#macroeconomicsnplTable').length > 0) {
                    var exportTable = $('#macroeconomicsnplTable').DataTable({
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


        vm.loadScenario = function () {
            vm.viewModelHelper.apiGet('api/macroeconomicsnpl/getmacroeconomicsnplbyscenario/' + vm.scenerio, null,
                   function (result) {
                       vm.macroeconomicsnpl = result.data;
                       vm.init === true;
                       //alert("Ok");
                   },
                   function (result) {
                       toastr.error('Fail to load macroeconomics npl scenarios.', 'Fintrak');
                   }, null);
        }
        initialize(); 
    }
}());
