/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsSectorCCFListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IfrsSectorCCFListController]);

    function IfrsSectorCCFListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrssectorccf-list-view';
        vm.viewName = 'CCF | LGD | PD by sector';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.ifrsSectorCCF = [];
        vm.disabled = false;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;
        var tabID = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/ifrssectorccf/getAllIfrsSectorCCFs/' + 'CCF', null,
                   function (result) {
                       vm.ifrsSectorCCFs = result.data;
                       InitialView('ifrsSectorCCFTable');
                       vm.init === true;
                       
                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        var InitialView = function (tableID) {
            InitialGrid(tableID);
        }

        var InitialGrid = function (tableID) {
                tabID = '#' + tableID
            setTimeout(function () {
                // data export
                if ($(tabID).length > 0) {
                    exportTable = $(tabID).DataTable({
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

        //var InitialGrid2 = function () {
        //    setTimeout(function () {

        //        // data export
        //        if ($('#ifrsSectorCCFTable2').length > 0) {
        //            exportTable = $('#ifrsSectorCCFTable2').DataTable({
        //                "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
        //                sDom: "T<'clearfix'>" +
        //                "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
        //                "t" +
        //                "<'row'<'col-sm-6'i><'col-sm-6'p>>",
        //                "tableTools": {
        //                    "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
        //                }
        //            });
        //        }
        //    }, 50);
        //}

        //var InitialGrid3 = function () {
        //    setTimeout(function () {

        //        // data export
        //        if ($('#ifrsSectorCCFTable3').length > 0) {
        //            exportTable = $('#ifrsSectorCCFTable3').DataTable({
        //                "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
        //                sDom: "T<'clearfix'>" +
        //                "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
        //                "t" +
        //                "<'row'<'col-sm-6'i><'col-sm-6'p>>",
        //                "tableTools": {
        //                    "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
        //                }
        //            });
        //        }
        //    }, 50);
        //}

        vm.loadOthers = function (Type) {

            vm.viewModelHelper.apiGet('api/ifrssectorccf/getAllIfrsSectorCCFs/' + Type, null,
                function (result) {
                    vm.ifrsSectorCCFs = result.data;
                    //if (Type === 'CCF') {
                    //    InitialView('ifrsSectorCCFTable');
                    //}
                    //else if (Type === 'LGD') {
                    //    InitialView('ifrsSectorCCFTable2');
                    //}
                    //else if (Type === 'PD') {
                    //    InitialView('ifrsSectorCCFTable3');
                    //}
                    switch (Type) {
                        case 'LGD':
                            InitialView('ifrsSectorCCFTable2')
                            break;
                        case 'PD':
                            InitialView('ifrsSectorCCFTable3')
                            break;
                        default:
                            InitialView('ifrsSectorCCFTable');
                    }
                        exportTable.destroy();
                },

                function (result) {
                    toastr.error(result, 'Fintrak Error');
                }, null);
        }

        initialize(); 
    }
}());
