/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        TeamListController]);

    function TeamListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'team-list-view';
        vm.viewName = 'Teams';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.teams = [];
        vm.searchValue = '';

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/team/availableteams', null,
                   function (result) {
                       vm.teams = result.data;
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
                if ($('#teamTable1').length > 0) {
                    var exportTable = $('#teamTable').DataTable({
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


        vm.loadTeams = function () {
            vm.teams = [];

            if (vm.searchValue === "") {
                alert("Search cannot be empty")
                return
            };

            vm.viewModelHelper.apiGet('api/team/getteamsbysearch/' + vm.searchValue, null,
                               function (result) {
                                 
                                   vm.teams = result.data;
                               },
                                       function (result) {
                                           toastr.error(result, 'Fintrak Error');
                                       }, null);

        }

        vm.refreshTeams = function () {
            initialize();
        };

        initialize();
    }
}());
