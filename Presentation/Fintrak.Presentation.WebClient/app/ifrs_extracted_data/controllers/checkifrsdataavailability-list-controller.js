/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CheckifrsDataAvailabilityController",
        ['$scope', '$window','$state', 'viewModelHelper', 'validator',
                        CheckifrsDataAvailabilityController]);

    function CheckifrsDataAvailabilityController($scope, $state, $window, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'CheckifrsDataAvailability';
        vm.view = 'checkifrsdataavailability-list-view';
        vm.viewName = 'IFRS 9 Staging Data | Check Data Availability';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.startDate = new Date();
        vm.endDate = new Date();        
        vm.ifrsCkDataAv = [];
        vm.distinctMaturityDate = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.rundate = new Date();
        ////////////////////////////////////////////// Rundate functions...

        vm.usrDateField = '';
        vm.openedRunDate = false;

        vm.openRunDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate = true;
        }

        ///////////////////////////////////////

        vm.instruction = '';
        var exportTable;
        var tabID = '';

        var initialize = function(){
            //load lookups
            //intializeLookUp();

            //distinctMaturityDate(1);
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/checkifrsdataavailability/availabledatacheck', null,
                   function (result) {
                       vm.CkData = result.data;
                       InitialView('ifrsCDataAvTable');
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


        vm.openStartDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedStartDate = true;
        }

        vm.openEndDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedEndDate = true;
        }

        vm.rundatacheck = function () {
            if (vm.rundate === '') {
                toastr.warning('Please select a RunDate', 'Empty RunDate');
                return
            }
           var params = { Date: vm.runDate };
           vm.viewModelHelper.apiPost('api/checkifrsdataavailability/checkstagingbyrundate', params,
                function (result) {
                    //$state.go('api/checkdataavailability/availabledatacheck');
                    vm.loadcheckdata();
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak Error');
                }, null);
        }

        vm.loadcheckdata = function () {
            vm.viewModelHelper.apiGet('api/checkifrsdataavailability/availabledatacheck', null,
                function (result) {
                    vm.CkData = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }
        vm.openRunDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate = true;
        }

          initialize();
    }
}());
