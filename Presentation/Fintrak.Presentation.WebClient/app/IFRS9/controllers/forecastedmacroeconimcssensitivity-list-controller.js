/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ForecastedMacroeconimcsSensitivityListController",
                    ['$scope', '$state','$location','$window', 'viewModelHelper', 'validator',
                        ForecastedMacroeconimcsSensitivityListController]);

    function ForecastedMacroeconimcsSensitivityListController($scope, $state, $location, $window,viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'forecastedmacroeconimcssensitivity-list-view';
        vm.viewName = 'Sensitivity Forecasted Macro Economic Variable';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.forecastedMacroeconimcsSensitivitys = [];

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
                vm.viewModelHelper.apiGet('api/forecastedmacroeconimcssensitivity/availableforecastedMacroeconimcsSensitivitys', null,
                   function (result) {
                       vm.forecastedMacroeconimcsSensitivitys = result.data;
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
                if ($('#forecastedMacroeconimcsSensitivityTable').length > 0) {
                    var exportTable = $('#forecastedMacroeconimcsSensitivityTable').DataTable({
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
            var params = {microeconomic: vm.microeconomic, year: vm.year, types: 1, values:vm.Value };
            vm.viewModelHelper.apiPost('api/forecastedmacroeconimcssensitivity/InsertSensitivityData', params,
                      function (result) {
                          vm.getUpdatedSensitivityData();
                      },
                     function (result) {
                         toastr.error(result.data, 'Fintrak Error');
                     }, null);
        }

        //if (vm.accountModel.Password !== "@password") 
        //{
        //    $http.post(Fintrak.rootPath + 'api/account/login', vm.accountModel)
        //      .then(function (result) {
        //          window.location.href = Fintrak.rootPath;
        //      }, function (result) {
        //          alert('Unable to login: ' + result.data);
        //          toastr.error('Unable to login: ' + result.data, 'Fintrak');
        //      })
        //}


        vm.Compute = function () {
            vm.viewModelHelper.apiPost('api/forecastedmacroeconimcssensitivity/Compute',
                      function (result) {
                          ///window.location.href = Fintrak.rootPath + 'ifrs-tbills-list';
                          //  $state.go('ifrs9-nonperformingloan-list');
                          //window.location.href = '/app/IFRS9/views/pitpdcomparism-list-view';
                          alert('Sensitivity Analysis Successfully Completed');
                      },
                     function (result) {
                         alert('Sensitivity Analysis Successfully Completed');
                     }, null);
        }

        vm.getUpdatedSensitivityData = function () {
            vm.viewModelHelper.apiGet('api/forecastedmacroeconimcssensitivity/availableforecastedMacroeconimcsSensitivitys', null,
                function (result) {
                    vm.forecastedMacroeconimcsSensitivitys = result.data;
                },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
        }

        initialize(); 
    }
}());
