/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BSINOtherInformationTotalLineListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        BSINOtherInformationTotalLineListController]);

    function BSINOtherInformationTotalLineListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'bsinotherinformationtotalline-list-view';
        vm.viewName = 'BS Other Information Total Line';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.bsINOtherInformationTotalLines = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/bsinotherinformationtotalline/availablebsinotherinformationtotallines', null,
                   function (result) {
                       vm.bsINOtherInformationTotalLines = result.data;
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
                if ($('#bsINOtherInformationTotalLineTable').length > 0) {
                    var exportTable = $('#bsINOtherInformationTotalLineTable').DataTable({
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
