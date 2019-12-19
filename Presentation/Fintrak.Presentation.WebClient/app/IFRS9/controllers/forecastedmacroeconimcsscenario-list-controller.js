/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ForecastedMacroeconimcsScenarioListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        ForecastedMacroeconimcsScenarioListController]);

    function ForecastedMacroeconimcsScenarioListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'forecastedmacroeconimcsscenario-list-view';
        vm.viewName = 'Scenario Forecasted Macro Economic Variable';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.forecastedMacroeconimcsScenarios = [];

        vm.types = 1

        vm.Value=[]
        vm.sector = [];
        vm.microeconomic = [];
        vm.year = [];
        vm.sectors = [];
        vm.microeconomics = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            intializeLookUp();
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/forecastedmacroeconimcsscenario/availableforecastedMacroeconimcsScenarios', null,
                   function (result) {
                       vm.forecastedMacroeconimcsScenarios = result.data;
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

        var intializeLookUp = function () {
            getSectors()
            getMicroeconomics()
            distinctYears()
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#forecastedMacroeconimcsScenarioTable').length > 0) {
                    var exportTable = $('#forecastedMacroeconimcsScenarioTable').DataTable({
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


        var getSectors = function () {
            vm.viewModelHelper.apiGet('api/sector/availablesectors', null,
                 function (result) {
                     vm.sectors = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }


        var getMicroeconomics = function () {
            vm.viewModelHelper.apiGet('api/macroeconomicvariable/availablemacroEconomicVariables', null,
                 function (result) {
                     vm.microeconomics = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var distinctYears = function () {
            vm.viewModelHelper.apiGet('api/macroeconomicvdisplay/getyears/2', null,
                 function (result) {
                     vm.distinctYears = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }
        vm.Save = function () {
            var params = { sector: vm.sector, microeconomic: vm.microeconomic, year: vm.year, types: 1, values: vm.Value };
            vm.viewModelHelper.apiPost('api/forecastedmacroeconimcsscenario/InsertScenarioData', params,
                      function (result) {
                          vm.getUpdatedScenarioData();
                      },
                     function (result) {
                         toastr.error(result.data, 'Fintrak Error');
                     }, null);
        }


        vm.Compute = function () {
            vm.viewModelHelper.apiPost('api/forecastedmacroeconimcsscenario/Compute',
                      function (result) {
                          $state.go('ifrs9-externalrating-list');
                      },
                     function (result) {
                         toastr.error(result.data, 'Fintrak Error');
                     }, null);
        }

        vm.getUpdatedScenarioData = function () {
            vm.viewModelHelper.apiGet('api/forecastedmacroeconimcsscenario/availableforecastedMacroeconimcsScenarios', null,
                function (result) {
                    vm.forecastedMacroeconimcsScenarios = result.data;
                },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
        }

        initialize(); 
    }
}());
