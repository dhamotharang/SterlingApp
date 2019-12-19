/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("NseIndexListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        NseIndexListController]);

    function NseIndexListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS';
        vm.view = 'nseindex-list-view';
        vm.viewName = 'NSE Index';
        vm.viewName2 = 'Computed Probability Weights';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.nseIndex = [];
        vm.probabilityWeight = [];
        vm.lOC = 95;
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/nseindex/availablenseindexs', null,
                   function (result) {
                       vm.nseIndex = result.data;
                       InitialView();
                       vm.init === true;
                        displayProbabilityWeights();
                          },
                     function (result) {
                         toastr.error(result.data, 'Fintrak');
                     }, null);
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function() {
            setTimeout(function() {

                    // data export
                    if ($('#nseIndexTable').length > 0) {
                        exportTable = $('#nseIndexTable').DataTable({
                            "lengthMenu": [[10, 20, 50, 50, 100, -1], [10, 20, 50, 50, 100, "All"]],
                            //sDom: "T<'clearfix'>" +
                            //    "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                            //    "RC" +
                            //    "t" +
                            //    "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                            "tableTools": {
                                "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                            }
                            //"aoColumnDefs": [
                            //     //{ "bVisible": false, "aTargets": [0] }
                            //]
                            //"colVis": {
                            //    buttonText: 'Show / Hide Columns',
                            //    restore: "Restore",
                            //    showAll: "Show all"
                            //}
                        });
                    }
                },
                50);
        };

        vm.ComputeProbabilityWeight = function() {
            if (vm.lOC <= 50) {
                vm.lOC = 50.01;
            } else if (vm.lOC >= 100) {
                vm.lOC = 99.99;
            }
            if (countDecimals(vm.lOC) > 2) {
                window.alert("Not more than two decimal places allowed");
                return;
            }
            vm.lOC1 = vm.lOC * 10000;
            vm.viewModelHelper.apiGet('api/nseindex/computeprobabilityweight/' + vm.lOC1,
                null,
                function(result) {
                    toastr.success('Probability Weights Successfully Computed', 'Fintrak');
                    displayProbabilityWeights();
                },
                function(result) {
                    toastr.error(result.data, 'fintrak error');
                },
                null);
        };

          var displayProbabilityWeights = function () {
              vm.viewModelHelper.apiGet('api/nseindex/availableprobabilityweights', null,
                        function (result) {
                            vm.probabilityWeight = result.data;
                       },
                       function (result) {
                           toastr.error('fail to load Probability Weight Result', 'fintrak error');
                       }, null);
          }

          var countDecimals = function (value) {
              if ((value % 1) !== 0)
                  return value.toString().split(".")[1].length;
              return 0;
          };

        initialize(); 
    }
}());
