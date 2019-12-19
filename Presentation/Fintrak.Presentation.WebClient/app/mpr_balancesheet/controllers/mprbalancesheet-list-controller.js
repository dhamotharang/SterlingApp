


/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MPRBalanceSheetListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator', 'Excel', '$timeout',
                        MPRBalanceSheetListController]);

    function MPRBalanceSheetListController($scope, $state, viewModelHelper, validator, Excel, $timeout) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;
        //ffff
        vm.module = 'Core';
        vm.view = 'mprbalancesheet-list-view';
        vm.viewName = 'BalanaceSheet ';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.mprBalanceSheets = [];
        vm.distinctRunDate = [];
        //vm.distinctToDate = [];

        vm.selectedsearchType = 'All';
        vm.searchValue = 'All';
        vm.number = 0;
        vm.RunDate = 'None'//new Date();
        //vm.ToDate = 'None'//new Date();

        vm.searchTypes = [
           { Id: 0, Name: 'All' },
           { Id: 1, Name: 'Caption' },
           { Id: 2, Name: 'AccountNo' },
           { Id: 3, Name: 'AccountName' },
           { Id: 4, Name: 'Team' },
           { Id: 5, Name: 'AccountOfficer' }
        ];


        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {
            intializeLookUp();
             //intializeLookUp1();
             if (vm.init === false) {    
                vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number, null,
                //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
                   function (result) {
                       vm.mprBalanceSheets = result.data                     
                       toastr.success('BalanceSheets data loaded.', 'Fintrak');
                       InitialView();
                        vm.init === true;
                   },
                   function (result) {
                       toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                   }, null);
               
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#mprBalanceSheetTable1').length > 0) {
                    var exportTable = $('#mprBalanceSheetTable').DataTable({
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

         vm.loadBalanceSheets = function () {
             //vm.viewModelHelper.apiGet('api/mprbalancesheet/balancesheet/' + vm.RunDate.substr(0, 10) , null,
             vm.viewModelHelper.apiGet('api/mprbalancesheet/getbalancesheet/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number + '/' + vm.RunDate.substr(0, 10), null,
                        function (result) {
                            vm.mprBalanceSheets = result.data;
                        },
                       function (result) {
                           toastr.error(result, 'Fintrak Error');
                       }, null);
          }

        
        //vm.loadBalanceSheets = function () {

        //    if (vm.selectedsearchType === 'All' && vm.searchValue === 'All') {
        //        vm.viewModelHelper.apiGet('api/mprbalancesheet/getbalancesheet/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number + '/' + vm.RunDate.substr(0, 10) + '/' + vm.ToDate.substr(0, 10), null,
        //        function (result) {

        //            vm.mprBalanceSheets = result.data;
                   
        //            //InitialView();
        //            if (!vm.init) {
                       
                        
        //            }
        //        },
        //      function (result) {
        //          toastr.error(result.data, 'Fintrak');
        //      }, null);
        //    }
        //    else {
        //        vm.viewModelHelper.apiGet('api/mprbalancesheet/getbalancesheet/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number + '/' + vm.RunDate.substr(0, 10) + '/' + vm.ToDate.substr(0, 10), null,
        //       function (result) {

        //           vm.mprBalanceSheets = result.data;
        //           //vm.JSONToCSVConvertor(vm.mprBalanceSheets, "Balance Sheet Report", true);
        //           if (!vm.init) {
        //               //InitialView();
        //               vm.init = true;
        //           }
        //       },
        //     function (result) {
        //         toastr.error(result.data, 'Fintrak');
        //     }, null);
        //    }

            

        //}


        var intializeLookUp = function () {
            distinctRunDate();
            //distinctToDate();
            
        }     

          var distinctRunDate = function () {
              vm.viewModelHelper.apiGet('api/mprbalancesheet/getDate', null,
                   function (result) {
                       //var dateAsString = $filter('date')(result.data.RunDate, "MM/dd/yyyy") ;
                       vm.distinctRunDate = result.data;
                       //vm.distinctToDate = result.data;
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
          }

         //var intializeLookUp1 = function () {
         //   distinctToDate();
            
         // }
         // var distinctToDate = function () {
         //     vm.viewModelHelper.apiGet('api/mprbalancesheet/getTodate', null,
         //          function (result) {
         //              vm.distinctToDate = result.data;
         //          },
         //          function (result) {
         //              toastr.error(result.data, 'Fintrak');
         //          }, null);
         // }

       

        vm.exportToExcel = function (tableId) { // ex: '#my-table'
            //var exportHref = Excel.tableToExcel(tableId, 'Balance Sheet Budget');
            //$timeout(function () { location.href = exportHref; }, 100); // trigger download

            $(tableId).tableExport({ type: 'excel', escape: 'true' });
        }

       

        initialize(); 
    }
}());
