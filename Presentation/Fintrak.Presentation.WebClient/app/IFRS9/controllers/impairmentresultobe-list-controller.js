/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ImpairmentResultOBEListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        ImpairmentResultOBEListController]);

    function ImpairmentResultOBEListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'impairmentresultobe-list-view';
        vm.viewName = 'OBE Impairment Result';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.impairmentResultOBEs = [];
        vm.SearchParam = '';
        var exportTable;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/impairmentresultobe/availableimpairmentResultOBEs', null,
                   function (result) {
                       vm.impairmentResultOBEs = result.data;
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
                if ($('#impairmentResultOBETable').length > 0) {
                    exportTable = $('#impairmentResultOBETable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" + "RC" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        },
                        "aoColumnDefs": [
                             //{ "bVisible": false, "aTargets": [0] }
                        ],
                        "colVis": {
                            buttonText: 'Show / Hide Columns',
                            restore: "Restore",
                            showAll: "Show all"
                        }
                    });
                }
            }, 50);
        }

        initialize(); 

        vm.reloadPage = function () {
            //vm.search = false;
            vm.init = false;
            vm.SearchParam = null;
            initialize();
            exportTable.destroy();
        };
         

        vm.getImpairmentResultOBE = function (SearchParam) {
            //vm.disabled = true;
            if (SearchParam.length > 3) {
                vm.viewModelHelper.apiGet('api/impairmentresultobe/getimpairmentResultOBEsBySearch/' + SearchParam, null,
                   function (result) {
                       vm.impairmentResultOBEs = result.data;
                       InitialView();
                       exportTable.destroy();
                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
            else if (SearchParam === "") {
                alert("Search cannot be empty")
            }
            else
                alert("Provide a minimum of 4 characters to search");
        }
    }
}());
