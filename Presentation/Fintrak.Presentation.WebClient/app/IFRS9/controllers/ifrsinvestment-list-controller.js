/**
 * Created by Tosin on 8/12/2019.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ifrsinvestmentListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        ifrsinvestmentListController]);

    function ifrsinvestmentListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrsinvestment-list-view';
        vm.viewName = 'ifrsinvestment';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];


        vm.ifrsinvestment = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;
        var tabID = 'InvestmentTable';

        var initialize = function(){
            
            if (vm.init === false) {
                vm.loadAll();
                  
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
        vm.loadAll = function () {
            vm.viewModelHelper.apiGet('api/ifrsinvestment/getallifrsinvestments' , null,
                function (result) {
                    vm.ifrsinvestment = result.data;
                    InitialView('InvestmentTable');

                    if (vm.init === true) {
                    exportTable.destroy();
                    }
                    else vm.init = true
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        initialize(); 
    }
}());
