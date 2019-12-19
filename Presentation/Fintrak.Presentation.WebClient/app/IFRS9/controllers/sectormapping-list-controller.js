/**
 * Created by Dara on 23/03/2018.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SectorMappingListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        SectorMappingListController]);

    function SectorMappingListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'sectormapping-list-view';
        vm.viewName = 'Sector Mapping';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.selectedsource = 'CCF';
        vm.sectormappings = [];

        //vm.sources = [
        //  { Id: 'CBN', Name: 'CBN SectorMappings' },
        //  { Id: 'CCF', Name: 'Bank CCF SectorMappings' },
        //  { Id: 'LGD', Name: 'Bank LGD SectorMappings' }
        //];

        //vm.sources = [
        //  { Id: 1, Name: 'CBN' },
        //  { Id: 2, Name: 'CCF' },
        //  { Id: 3, Name: 'LGD' }
        //];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/sectormapping/availablesectormappings', null,
                   function (result) {
                       vm.sectormappings = result.data;
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
                if ($('#sectormappingTable').length > 0) {
                    var exportTable = $('#sectormappingTable').DataTable({
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


        //vm.loadSectorMappings = function () {
        //    vm.sectormappings = [];
        //    //vm.init = false;

        //    vm.viewModelHelper.apiGet('api/sectormapping/getsectormappingsbysource/' + vm.selectedsource, null,
        //                       function (result) {
        //                           vm.sectormappings = result.data;
        //                           //InitialView();
        //                           //vm.init === true;
        //                       },
        //                               function (result) {
        //                                   toastr.error(result, 'Fintrak Error');
        //                               }, null);
        //}

        initialize(); 
    }
}());
