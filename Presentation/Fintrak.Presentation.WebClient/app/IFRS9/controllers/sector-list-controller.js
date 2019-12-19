/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SectorListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        SectorListController]);

    function SectorListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'sector-list-view';
        vm.viewName = 'Sectors and Mappings';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.selectedsource = 'CBN';
        vm.sectors = [];
        vm.sectormappings = [];

        vm.sources = [
          { Id: 'CBN', Name: 'CBN Sectors' },
          { Id: 'CCF', Name: 'Bank CCF Sectors' },
          { Id: 'LGD', Name: 'Bank LGD Sectors' }
        ];

        //vm.sources = [
        //  { Id: 1, Name: 'CBN' },
        //  { Id: 2, Name: 'CCF' },
        //  { Id: 3, Name: 'LGD' }
        //];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;
        var tabID = '';

        var initialize = function(){
            
            if (vm.init === false) {
                vm.loadSectors();
                    //vm.init === true;
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


        //vm.loadSectors = function () {
        //    vm.viewModelHelper.apiGet('api/sector/getsectorsbysource/' + vm.selectedsource, null,
        //                       function (result) {
        //                           vm.sectors = result.data;
        //                           //InitialView();
        //                           //vm.init === true;
        //                       },
        //                               function (result) {
        //                                   toastr.error(result, 'Fintrak Error');
        //                               }, null);
        //}

        vm.loadMapping = function () {
            vm.viewModelHelper.apiGet('api/sectormapping/availablesectormappings', null,
                function (result) {
                    vm.sectormappings = result.data;
                    InitialView('sectormappingTable');

                    exportTable.destroy();
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        vm.loadSectors = function () {
            vm.viewModelHelper.apiGet('api/sector/getsectorsbysource/' + vm.selectedsource, null,
                function (result) {
                    vm.sectors = result.data;
                    InitialView('sectorTable');

                    if (vm.init === true) {
                    exportTable.destroy();
                    }
                    else vm.init = true
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        initialize(); 
    }
}());
