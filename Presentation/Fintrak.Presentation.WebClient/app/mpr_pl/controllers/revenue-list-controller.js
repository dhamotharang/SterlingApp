/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RevenueListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        RevenueListController]);

    function RevenueListController($scope, $state, viewModelHelper, validator)  {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'revenue-list-view';
        vm.viewName = 'Fee And Commission';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.revenue = [];
        vm.distinctRunDate = [];
        vm.distinctToDate = [];



        vm.selectedsearchType = 'All';
        vm.searchValue = 'All';
        vm.number = 0;
        vm.RunDate = 'None'//new Date();
        vm.ToDate = 'None'//new Date();


        vm.searchTypes = [
            { Id: 0, Name: 'All' },
            { Id: 1, Name: 'Caption' },
            { Id: 2, Name: 'GLCode' },
            { Id: 3, Name: 'Narrative' },
            { Id: 4, Name: 'Team' },
            { Id: 5, Name: 'AccountOfficer' },
            { Id: 6, Name: 'RelatedAccount' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

             intializeLookUp();
             intializeLookUp1();
                         if (vm.init === false) {    
                vm.viewModelHelper.apiGet('api/revenue/availablerevenue/'  + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number , null,
             
                   function (result) {
                       vm.revenue = result.data                     
                       toastr.success('Revenue data loaded.', 'Fintrak');
                       InitialView();
                        vm.init === true;
                   },
                   function (result) {
                       toastr.error('Fail to load Revenue data.', 'Fintrak');
                   }, null);
               
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
               
                // data export
                if ($('#revenueTable').length > 0) {
                    var exportTable = $('#revenueTable').DataTable({
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


         vm.loadPLReports = function () {
             //vm.viewModelHelper.apiGet('api/mprbalancesheet/balancesheet/' + vm.RunDate.substr(0, 10) , null,
             vm.viewModelHelper.apiGet('api/revenue/getrevenue/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number + '/' + vm.RunDate.substr(0, 10) + '/' + vm.ToDate.substr(0, 10), null,
                        function (result) {
                            vm.revenue = result.data;
                        },
                       function (result) {
                           toastr.error(result, 'Fintrak Error');
                       }, null);
          }



         var intializeLookUp = function () {
            distinctRunDate();
            //distinctToDate();
            
        }     

          var distinctRunDate = function () {
              vm.viewModelHelper.apiGet('api/revenue/getDate', null,
                   function (result) {
                       //var dateAsString = $filter('date')(result.data.RunDate, "MM/dd/yyyy") ;
                       vm.distinctRunDate = result.data;
                       //vm.distinctToDate = result.data;
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
          }

         var intializeLookUp1 = function () {
            distinctToDate();
            
          }
          var distinctToDate = function () {
              vm.viewModelHelper.apiGet('api/revenue/getTodate', null,
                   function (result) {
                       vm.distinctToDate = result.data;
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
          }

       

         //vm.loadPLReports = function (initialized) {

         //    if (vm.selectedsearchType === 'All' && vm.searchValue === 'All') {

         //        vm.viewModelHelper.apiGet('api/revenue/availablerevenue/' + vm.number, null,
         //          function (result) {

         //              vm.revenue = result.data;

         //              if (vm.init) {
         //                  InitialView();
         //                  vm.init === true;
         //              }
         //          },
         //        function (result) {
         //            toastr.error(result.data, 'Fintrak');
         //        }, null);

         //    } else{
         //        vm.viewModelHelper.apiGet('api/revenue/getrevenue/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
         //         function (result) {

         //             vm.revenue = result.data;

         //             if (vm.init) {
         //                 InitialView();
         //                 vm.init === true;
         //             }


         //         },
         //       function (result) {
         //           toastr.error(result.data, 'Fintrak');
         //       }, null);
         //    }

            
         //}

         //vm.getHeader = function () {
         //    return
         //    ['RevenueId', 'TransId', 'TransDate', 'Indicator', 'AccountTitle', 'GLDescription', 'GLAccount', 'GLCode', 'Narrative', 'BranchCode', 'TeamCode', 'AccountOfficerCode', 'Caption', 'RelatedAccount', 'Amount_LCY', 'RunDate', 'CompanyCode', 'Active',
         //        '1','2','3','4','5','6','7','8'
         //    ];
         //};

         vm.exportToExcel = function (tableId) { // ex: '#my-table'
             //var exportHref = Excel.tableToExcel(tableId, 'Balance Sheet Budget');
             //$timeout(function () { location.href = exportHref; }, 100); // trigger download

             $(tableId).tableExport({ type: 'excel', escape: 'false' });
         }

        initialize(); 
    }
}());
