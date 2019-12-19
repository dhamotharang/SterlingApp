/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("GLAArchiveListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        GLAArchiveListController]);

    function GLAArchiveListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'glaarchive-list-view';
        vm.viewName = 'GL Adjustment Archive';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.glaarchives = [];
        vm.selectedRundate = new Date();

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var exportTable;

        var initialize = function () {

            if (vm.init === false) {
                InitialView();
                vm.init === true;
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#glaarchivesTable').length > 0) {
                    exportTable = $('#glaarchivesTable').DataTable({
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

        vm.load = function () {
            vm.viewModelHelper.apiGet('api/glaarchive/gladjustmentarchivesbyrundate/' + vm.selectedRundate.toDateString(), null,
                function (result) {
                    vm.glaarchives = result.data;
                    InitialView();
                    exportTable.destroy();
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        vm.flipIndicator = function (indicator) {
            if (indicator === 1) {
                return 'Credit';
            } else {
                return 'Debit';
            }
        }

        initialize();
    }
}());
