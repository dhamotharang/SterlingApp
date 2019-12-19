/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("AuditTrailListController",
            ['$scope', '$state', '$filter', 'viewModelHelper', 'validator',
                AuditTrailListController]);

    function AuditTrailListController($scope, $state, $filter, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'audittrail-list-view';
        vm.viewName = 'Audit Trail';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.auditTrails = [];

        vm.actionsPerformed = '';


        //vm.startDate = '08-27-2015' ;
        //vm.endDate = '08-28-2015';

        vm.startDate = new Date();
        vm.endDate = new Date();



        vm.actions = [
            { Id: 1, Name: 'Create' },
            { Id: 2, Name: 'Update' },
            { Id: 3, Name: 'Delete' },
            { Id: 4, Name: 'Extraction' }
        ];


        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;

        var initialize = function () {

            if (vm.init === false) {
                InitialView();
                vm.init = true;
            }
        };

        var InitialView = function () {
            InitialGrid();
        };

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#auditTrailTable').length > 0) {
                    exportTable = $('#auditTrailTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        searching: false,
                        scrollX: true,
                        sDom:
                            "T" +
                        //< 'clearfix' > " +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>"
                            +
                            //"RC" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                        //"aoColumnDefs": [
                        //    { "bVisible": false, "aTargets": [0] }
                        //],
                        //"colVis": {
                        //    buttonText: 'Show / Hide Columns',
                        //    restore: "Restore",
                        //    showAll: "Show all"
                        //}
                    });
                }
            }, 50);
        };


        vm.filterByAction = function () {

            //var startDate = $filter('date')(vm.startDate, 'MM-dd-yyyy'); 
            //var endDate = $filter('date')(vm.endDate, 'MM-dd-yyyy');

            //vm.viewModelHelper.apiGet('api/auditTrail/getaudittrailbyaction/' + vm.actionsPerformed + '/' + vm.startDate + '/' + vm.endDate, null,

            if (!vm.actionsPerformed) {
                toastr.info('Select an action performed to load', 'No Action Selected', {preventDuplicates: true});
                return;
            }

            vm.viewModelHelper.apiGet(
                'api/auditTrail/getaudittrailbyaction/' +
                vm.actionsPerformed +
                '/' +
                vm.startDate.toDateString() +
                '/' +
                vm.endDate.toDateString(),
                null,
                function(result) {

                    vm.auditTrails = result.data;

                    if (vm.auditTrails.length === 0) {
                        toastr.warning('No data exists for the date range and action selected', 'No audit history', { preventDuplicates: true });
                    }

                    if (vm.init) {
                        exportTable.destroy();
                        InitialView();
                        vm.init === true;
                    }
                },
                function(result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };


        initialize();

        vm.openStartDate = function($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedStartDate = true;
        };

        vm.openEndDate = function($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedEndDate = true;
        };
    }
}());
