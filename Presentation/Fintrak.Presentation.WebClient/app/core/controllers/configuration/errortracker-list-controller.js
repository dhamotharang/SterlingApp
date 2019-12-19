/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ErrorTrackerListController",
                    ['$window', '$scope', '$state', 'viewModelHelper', 'validator',
                        ErrorTrackerListController]);

    function ErrorTrackerListController($window, $scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'errorTracker-list-view';
        vm.viewName = 'ErrorTracker';
        vm.ErrorTrackerId = '';
        vm.errorData = {}

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.errorTrackers = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.delete = function (index) {

            var ErrorTrackerId = vm.errorTrackers[index].ErrorTrackerId;

            var deleteFlag = $window.confirm(' Are you sure you want to delete this current record');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/errortracker/deleteerrortracker', ErrorTrackerId,
              function (result) {
                  vm.viewModelHelper.apiGet('api/errortracker/availableerrortracker', null,
                   function (result) {
                       vm.errorTrackers = result.data;
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
                  toastr.success('Selected item deleted.', 'Fintrak');
                  //$state.go('core-client-list');
              },
              function (result) {
                  vm.viewModelHelper.apiGet('api/errortracker/availableerrortracker', null,
                   function (result) {
                       vm.errorTrackers = result.data;
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.deleteallrecord = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete all record');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/errortracker/deleteallerrortracker', 1,
              function (result) {
                  vm.viewModelHelper.apiGet('api/errortracker/availableerrortracker', null,
                   function (result) {
                       vm.errorTrackers = result.data;
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
                  toastr.success('Selected item deleted.', 'Fintrak');
                  //$state.go('core-client-list');
              },
              function (result) {
                  vm.viewModelHelper.apiGet('api/errortracker/availableerrortracker', null,
                   function (result) {
                       vm.errorTrackers = result.data;
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }


        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/errortracker/availableerrortracker', null,
                   function (result) {
                       vm.errorTrackers = result.data;
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
                if ($('#errorTrackerTable').length > 0) {
                    var exportTable = $('#errorTrackerTable').DataTable({
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
