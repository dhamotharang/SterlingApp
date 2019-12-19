/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MPRCommFeeListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        MPRCommFeeListController]);

    function MPRCommFeeListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'MPRCommFee-list-view';
        vm.viewName = 'MPR Fee And Commission';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.mprcommfee = [];


        vm.searchValue = '';

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

                         if (vm.init === false) {    
                             vm.viewModelHelper.apiGet('api/mprcommfee/getmprcommfees', null,
             
                   function (result) {
                       vm.mprcommfee = result.data                     
                       toastr.success('MPR Commision & Fee data loaded.', 'Fintrak');
                       InitialView();
                        vm.init === true;
                   },
                   function (result) {
                       toastr.error('Fail to load MPR Commision & Fee data.', 'Fintrak');
                   }, null);
               
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
               
                // data export
                if ($('#mprcommfeeTable1').length > 0) {
                    var exportTable = $('#mprcommfeeTable').DataTable({
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




        vm.loadMPRCommFees = function () {
            vm.mprcommfee = [];

            if (vm.searchValue === "") {
                alert("Search cannot be empty")
                return
            };

            vm.viewModelHelper.apiGet('api/mprcommfee/getmprcommfeebysearch/' + vm.searchValue, null,
                               function (result) {
                                 
                                   vm.mprcommfee = result.data;
                               },
                                       function (result) {
                                           toastr.error(result, 'Fintrak Error');
                                       }, null);
        }


        vm.refreshMPRCommFees = function () {
            initialize();
        };


         

         vm.exportToExcel = function (tableId) { // ex: '#my-table'
             //var exportHref = Excel.tableToExcel(tableId, 'Balance Sheet Budget');
             //$timeout(function () { location.href = exportHref; }, 100); // trigger download

             $(tableId).tableExport({ type: 'excel', escape: 'false' });
         }

        initialize(); 
    }
}());
