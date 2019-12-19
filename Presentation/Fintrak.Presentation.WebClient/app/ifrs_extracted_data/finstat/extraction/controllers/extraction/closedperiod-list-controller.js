/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ClosedPeriodListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator', 'closedPeriodSearchService',
                        ClosedPeriodListController]);

    function ClosedPeriodListController($scope, $state, viewModelHelper, validator, closedPeriodSearchService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'closedperiod-list-view';
        vm.viewName = 'Open/Closed Periods';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.closedPeriods = [];
        vm.selectedSolutionId = 0;
        vm.selectedYear = 2015;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){
            vm.selectedSolutionId = closedPeriodSearchService.getSearchModel().solutionId;
            vm.selectedYear = closedPeriodSearchService.getSearchModel().year;

            if (vm.init === false) {
                getSolutions();
                getYears();
                vm.viewModelHelper.apiGet('api/closedperiod/getclosedperiodbylogin/' + vm.selectedSolutionId + '/' + vm.selectedYear, null,
                   function (result) {
                       vm.closedPeriods = result.data;
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
                if ($('#closedPeriodTable').length > 0) {
                    var exportTable = $('#closedPeriodTable').DataTable({
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
            closedPeriodSearchService.setSearchModel({ solutionId: vm.selectedSolutionId,year:vm.selectedYear })
            vm.viewModelHelper.apiGet('api/closedperiod/getclosedperiodbylogin/' + vm.selectedSolutionId + '/' + vm.selectedYear, null,
                   function (result) {
                       vm.closedPeriods = result.data;
                       
                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        vm.getStatus = function (status) {
            if (status)
                return 'Open';
            else
                return 'Closed';
        }

        vm.openDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedDate = true;
        }

        var getSolutions = function () {
            vm.viewModelHelper.apiGet('api/solution/availablesolutions', null,
                 function (result) {
                     vm.solutions = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getYears = function () {
            vm.viewModelHelper.apiGet('api/fiscalYear/getyears', null,
                 function (result) {
                     vm.years = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load years.', 'Fintrak');
                 }, null);
        }

        initialize(); 
    }
}());
