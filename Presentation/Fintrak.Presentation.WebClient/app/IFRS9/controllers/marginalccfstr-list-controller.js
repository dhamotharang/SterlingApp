/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MarginalCCFStrListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator', MarginalCCFStrListController]);

    function MarginalCCFStrListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_MarginalCCFStr_Data';
        vm.view = 'marginalccfstr-list-view';
        vm.viewName = 'Marginal CCF';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

 
        vm.marginalCCFstr = [];
        vm.init = false;
        vm.filterParam = '';

        vm.defaultCount = 5000;
        vm.showInstruction = true;
        var exportTable;
        var tabID = 'marginalCCFstrTable';
       // vm.instruction = 'Only top ' + vm.defaultCount + ' records loaded. Use the main search fuctionality to find a specific record by RefNo or Account No.';
        vm.obeType = [
            { value: 'FBG', name: 'FBG' },
            //{ value: 'OBE', name: 'OBE' },
            { value: 'PBG', name: 'PBG' },
            { value: 'Self Liquidating LC', name: 'Self Liquidating LC' }
        ];

        var initialize = function () {

            if (vm.init === false) {
                vm.loadMarginalCCF();              
            }
        };

        vm.loadMarginalCCF = function () {
            vm.viewModelHelper.apiGet('api/marginalCCFstr/availableMarginalCCFStr/' + vm.defaultCount, null,
                function (result) {
                    vm.marginalCCFstr = result.data;
                    InitialView('marginalCCFstrTable');
                    vm.searchParam = '';
                    if (vm.init === true) {
                        exportTable.destroy();
                    } else vm.init = true;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        var InitialView = function (tableID) {
            InitialGrid(tableID);
        };
        var InitialGrid = function (tableID) {
            tabID = '#' + tableID;
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
            },
                50);
        };
        vm.loadMarginalCCFStrBySearch = function () {

            if (vm.serachParam === '') {
                toastr.warning('Please input a RefNo ', 'Empty Search')  // or Account No
                return
            }
            else {

                vm.viewModelHelper.apiGet('api/marginalCCFstr/getmarginalccfstrbysearch/' + vm.serachParam, null,
                    function (result) {
                        vm.marginalCCFstr = result.data;                      
                        InitialView('marginalCCFstrTable');                       
                        exportTable.destroy();                       
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }

        };   

        vm.filterMarginalCCFStr = function () {
            //alert(vm.filterParam);
            vm.viewModelHelper.apiGet('api/marginalCCFstr/getmarginalccfstrbysearch/' + vm.filterParam, null,
            function (result) {
                vm.marginalCCFstr = result.data;
                InitialView('marginalCCFstrTable');     
                exportTable.destroy();
            },
            function (result) {
                toastr.error(result.data, 'Fintrak');
            }, null);
        }


        vm.refresh = function () {
            vm.init = false;
            vm.filterParam = '';
            initialize();
            exportTable.destroy();
          };


        initialize();
    }
}());
