/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsCustomerAccountListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IfrsCustomerAccountListController]);


    function IfrsCustomerAccountListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IfrsCustomerAccount';
        vm.view = 'ifrscustomeraccount-list-view';
        vm.viewName = 'Customer Data Information';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];


        vm.IfrsCustomerAccount = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/ifrscustomeraccount/availableifrscustomeraccount', null,
                   function (result) {
                       vm.IfrsCustomerAccount = result.data;
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
                if ($('#ifrsCustomerAccountTable').length > 0) {
                    var exportTable = $('#ifrsCustomerAccountTable').DataTable({
                        "lengthMenu": [[200, 1000, 50000, 1000000, -1], [200, 1000, 50000, 1000000, "All"]],
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
    }
}());
