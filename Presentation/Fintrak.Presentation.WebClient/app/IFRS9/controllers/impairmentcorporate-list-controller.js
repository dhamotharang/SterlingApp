/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ImpairmentCorporateListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        ImpairmentCorporateListController]);

    function ImpairmentCorporateListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'impairmentcorporate-list-view';
        vm.viewName = 'Corporate | Retail Impairment';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.impairmentCorporates = [];
        vm.impairmentResultRetails = [];
        vm.SearchParam = '';
        var exportTable;
        var tabID = '';

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.loadCorporate();
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

        vm.loadRetail = function() {
            vm.viewModelHelper.apiGet('api/impairmentresultretail/availableimpairmentResultRetails', null,
                function (result) {
                    vm.impairmentResultRetails = result.data;
                    InitialView('impairmentResultRetailTable');

                    exportTable.destroy();
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        vm.loadCorporate = function () {
            vm.viewModelHelper.apiGet('api/impairmentcorporate/availableimpairmentCorporates', null,
                function (result) {
                    vm.impairmentCorporates = result.data;
                    InitialView('impairmentCorporateTable');

                    if (vm.init === true) {
                        exportTable.destroy();
                    }
                    else vm.init = true

                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        vm.reloadPage = function () {
            //vm.search = false;
            vm.init = false;
            vm.SearchParam = '';
            vm.loadRetail();
            //exportTable.destroy();
        };

        vm.getImpairmentResultRetail = function (SearchParam) {
            //vm.disabled = true;
            if (SearchParam.length > 3) {
                vm.viewModelHelper.apiGet('api/impairmentresultretail/getimpairmentResultRetailsBySearch/' + SearchParam, null,
                    function (result) {
                        vm.impairmentResultRetails = result.data;
                        InitialView('impairmentResultRetailTable');
                        exportTable.destroy();
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else if (SearchParam === '') {
                toastr.warning('Search cannot be empty','Empty Input')
            }
            else
                toastr.warning('Provide a minimum of 4 characters to search','Insufficient Characters');
        }

        initialize(); 
    }
}());
