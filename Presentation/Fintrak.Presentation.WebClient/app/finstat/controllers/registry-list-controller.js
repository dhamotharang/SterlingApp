/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RegistryListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        RegistryListController]);

    function RegistryListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'registry-list-view';
        vm.viewName = 'Registry';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.registries = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.pageFlag = '';
        var exportTable;
        var tabID = '';

        var initialize = function() {

            if (vm.init === false) {
                vm.loadRegistries();
            }
        };

        var InitialView = function(tableID) {
            InitialGrid(tableID);
        };

        var InitialGrid = function(tableID) {
            tabID = '#' + tableID;
            setTimeout(function() {
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
                },
                50);
        };

        vm.loadOthRegistries = function () {
            vm.viewModelHelper.apiGet('api/registry/availableregistrys/' + 3,
                null,
                function (result) {
                    vm.registries = result.data;
                    InitialView('othregistryTable');

                    if (vm.init === true) {
                        exportTable.destroy();
                    } else vm.init = true;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

        vm.loadMgtRegistries = function() {
            vm.viewModelHelper.apiGet('api/registry/availableregistrys/' + 2,
                null,
                function(result) {
                    vm.registries = result.data;
                    InitialView('mgtregistryTable');
                    vm.pageFlag = 2;

                    exportTable.destroy();
                },
                function(result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

        vm.loadRegistries = function() {
            vm.viewModelHelper.apiGet('api/registry/availableregistrys/' + 1,
                null,
                function(result) {
                    vm.registries = result.data;
                    InitialView('registryTable');

                    if (vm.init === true) {
                        exportTable.destroy();
                    } else vm.init = true;
                },
                function(result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

        initialize(); 
    }
}());
