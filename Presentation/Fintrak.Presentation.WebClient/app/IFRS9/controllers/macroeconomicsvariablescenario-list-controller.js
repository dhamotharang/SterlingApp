/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MacroeconomicsVariableScenarioListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        MacroeconomicsVariableScenarioListController]);

    function MacroeconomicsVariableScenarioListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'macroeconomicsvariablescenario-list-view';
        vm.viewName = 'Macroeconomics Variable Scenario';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.macroeconomicsVariableScenarios = [];
        vm.type = 1;

        vm.types = [
          { Id: 1, Name: 'MacroeconomicsVariable' },
          { Id: 2, Name: 'NPL-MacroeconomicsVariable' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/macroeconomicsvariablescenario/availablemacroeconomicsbyflag/' + vm.type, null,
                   function (result) {
                       vm.macroeconomicsVariableScenarios = result.data;
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
                if ($('#macroeconomicsVariableScenariosTable').length > 0) {
                    var exportTable = $('#macroeconomicsVariableScenariosTable').DataTable({
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
        vm.loadChange = function () {
        
            vm.viewModelHelper.apiGet('api/macroeconomicsvariablescenario/availablemacroeconomicsbyflag/' + vm.type, null,
                               function (result) {
                                   vm.macroeconomicsVariableScenarios = result.data;
                                  
                               },
                                       function (result) {
                                           toastr.error(result, 'Fintrak Error');
                                       }, null);
        }

        initialize();
    }
}());
