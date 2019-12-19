/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("PLIncomeReportListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        PLIncomeReportListController]);

    function PLIncomeReportListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'plincomereport-list-view';
        vm.viewName = 'PL Income Report';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.plIncomeReports = [];

        vm.selectedsearchType = '';
        vm.searchValue = '';
        vm.number = 0;

        vm.searchTypes = [
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

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/plincomereport/availableplincomereport', null,
                   function (result) {
                       vm.plIncomeReports = result.data;
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
                if ($('#plIncomeReportTable').length > 0) {
                    var exportTable = $('#plIncomeReportTable').DataTable({
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

         vm.loadPLReports = function (initialized) {

            vm.viewModelHelper.apiGet('api/plincomereport/getplincomereport/' + vm.selectedsearchType  + '/' + vm.searchValue + '/' + vm.number, null,
                  function (result) {

                      vm.plIncomeReports = result.data;
 
                      if (vm.init)
                      {
                          InitialView();
                          vm.init === true;
                      }

                      
                  },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        initialize(); 
    }
}());
