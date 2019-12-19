


/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BalanceSheetBudgetListController",
                    ['$scope','$window', '$state', 'viewModelHelper', 'validator', 'Excel', '$timeout',
                        BalanceSheetBudgetListController]);

    function BalanceSheetBudgetListController($scope,$window, $state, viewModelHelper, validator, Excel, $timeout) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;
        //ffff
        vm.module = 'Core';
        vm.view = 'balancesheetbudget-list-view';
        vm.viewName = 'Balanace Sheet Budgets';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.selectedId = '';
        $scope.selection = [];

        vm.budgets = [];
        vm.budgetType = 1;
        vm.selectedYear = '2015';
        vm.searchValue = '';

        vm.budgetTypes = [
            { Id: 1, Name: 'Team Budget' },
            { Id: 2, Name: 'Officer  Budget' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                getYears();
                var url = '';
                if (vm.budgetType === 2)
                    url = 'api/balancesheetbudgetofficer/getbalancesheetbudgetofficers/' + vm.selectedYear;
                else
                    url = 'api/balancesheetbudget/getbalancesheetbudgets/' + vm.selectedYear;

                vm.viewModelHelper.apiGet(url, null,
                   function (result) {
                       vm.budgets = result.data;
                      
                       toastr.success('Budget data loaded.', 'Fintrak');
                       InitialView();
                   },
                   function (result) {
                       toastr.error('Fail to load budget data.', 'Fintrak');
                   }, null);
                vm.init === true;
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#budgetTable1').length > 0) {
                    var exportTable = $('#budgetTable').DataTable({
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
            var url = '';
            if (vm.budgetType === 2)
                url = 'api/balancesheetbudgetofficer/getbalancesheetbudgetofficers/' + vm.selectedYear;
            else
                url = 'api/balancesheetbudget/getbalancesheetbudgets/' + vm.selectedYear;

            vm.viewModelHelper.apiGet(url, null,
               function (result) {

                   vm.budgets = result.data;

                   toastr.success('Budget data loaded.', 'Fintrak');
               },
               function (result) {
                   toastr.error('Fail to load budget data.', 'Fintrak');
               }, null);
        }

        var getYears = function () {
            vm.viewModelHelper.apiGet('api/fiscalYear/getyears', null,
                 function (result) {
                     vm.years = result.data;
                     //InitialView();
                 },
                 function (result) {
                     toastr.error('Fail to load years.', 'Fintrak');
                 }, null);
        }

        vm.exportToExcel = function (tableId) { // ex: '#my-table'
            //var exportHref = Excel.tableToExcel(tableId, 'Balance Sheet Budget');
            //$timeout(function () { location.href = exportHref; }, 100); // trigger download

            $(tableId).tableExport({ type: 'excel', escape: 'false' });
        }

        vm.getSearch = function () {
             var url = '';
            if (vm.budgetType === 2)
                    url = 'api/balancesheetbudgetofficer/getbalancesheetbudgetofficerssearch/' + vm.searchValue;
                else
                    url = 'api/balancesheetbudget/getbalancesheetbudgetsearch/' +  vm.searchValue;
              vm.viewModelHelper.apiGet(url, null,
                        function (result) {
                            vm.budgets = result.data;
                        },
                       function (result) {
                           toastr.error(result, 'Fintrak Error');
                       }, null);
        }


        // toggle selection for a given selected row by id
        $scope.toggleSelection = function toggleSelection(BalancesheetBudgetId) {
            var idx = $scope.selection.indexOf(BalancesheetBudgetId);


            // is currently selected
            if (idx > -1) {
                $scope.selection.splice(idx, 1);
                vm.selectedId = $scope.selection.join(', ');
                //  alert(vm.selectedId)
            }

                // is newly selected
            else {
                $scope.selection.push(BalancesheetBudgetId);
                vm.selectedId = $scope.selection.join(', ');
                //  alert(vm.selectedId)
            }
        };

        vm.delete = function () {

            var deleteFlag = $window.confirm(' Are you sure you want to delete selected Row(s)');
            if (deleteFlag) {
                var url = '';
                url = 'api/balancesheetbudget/deleteselectedlist/' + vm.selectedId,
                vm.viewModelHelper.apiPost(url, null,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-balancesheetbudget-list');
                  vm.refreshPage();
              },
              function (result) {
                  toastr.error('Fail to delete item.', 'Fintrak');
              }, null);
            }
        }

        vm.refreshPage = function () {
            getYears();
            var url = '';
            if (vm.budgetType === 2)
                url = 'api/balancesheetbudgetofficer/getbalancesheetbudgetofficers/' + vm.selectedYear;
            else
                url = 'api/balancesheetbudget/getbalancesheetbudgets/' + vm.selectedYear;

            vm.viewModelHelper.apiGet(url, null,
               function (result) {
                   vm.budgets = result.data;

                   toastr.success('Budget data loaded.', 'Fintrak');
                   InitialView();
               },
               function (result) {
                   toastr.error('Fail to load budget data.', 'Fintrak');
               }, null);
        }
        initialize(); 
    }
}());
