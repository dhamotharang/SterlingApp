/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsLgdScenarioByInstrumentListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IfrsLgdScenarioByInstrumentListController]);

    function IfrsLgdScenarioByInstrumentListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrslgdscenariobyinstrument-list-view';
        vm.viewName = 'LGD Scenario By Instrument';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.ifrsLgdScenarioByInstrument = [];
        vm.disabled = false;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/ifrslgdscenariobyinstrument/getAllIfrsLgdScenarioByInstruments', null,
                   function (result) {
                       vm.ifrsLgdScenarioByInstruments = result.data;
                       InitialView();
                       vm.init = true;
                       
                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        /*vm.getIfrsLgdScenarioByInstrument = function (type) {
            vm.disabled = true;
            if (type) {
                vm.viewModelHelper.apiGet('api/ifrsLgdScenarioByInstrument/getIfrsLgdScenarioByInstrument/' + type, null,
                   function (result) {
                       vm.ifrsLgdScenarioByInstruments = result.data;
                       InitialView();

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }*/

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#ifrsLgdScenarioByInstrumentTable').length > 0) {
                    var exportTable = $('#ifrsLgdScenarioByInstrumentTable').DataTable({
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

        initialize(); 
    }
}());
