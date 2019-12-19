/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RatioCaptionMappingListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        RatioCaptionMappingListController]);

    function RatioCaptionMappingListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'ratiocaptionmapping-list-view';
        vm.viewName = 'MPR Ratio Caption Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ratioCaptionMappings = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/ratiocaptionmapping/availableratioCaptionMappings', null,
                   function (result) {
                       vm.ratioCaptionMappings = result.data;
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
                if ($('#ratioCaptionMappingTable').length > 0) {
                    var exportTable = $('#ratioCaptionMappingTable').DataTable({
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
