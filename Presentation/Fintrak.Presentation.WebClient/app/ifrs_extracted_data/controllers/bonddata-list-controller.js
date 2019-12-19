/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IFRSBondListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IFRSBondListController]);

    function IFRSBondListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_Bond_Data';
        vm.view = 'bonddata-list-view';
        vm.viewName = 'IFRS Bond Data';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.classification = '';
        vm.MaturityDate = 'None';

        vm.classifications = [
            { Id: 1, Name: 'Amortised Cost' },
            { Id: 2, Name: 'FVPL' },
            { Id: 3, Name: 'FVOCI' }
        ];
        
        vm.ifrsBonds = [];
        vm.distinctMaturityDate = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;

        var initialize = function () {
             intializeLookUp();

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/ifrsbond/availableifrsbond', null,
                   function (result) {
                       vm.ifrsBonds = result.data;
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
                if ($('#ifrsBondTable').length > 0) {
                     exportTable = $('#ifrsBondTable').DataTable({
                         "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                         scrollX: true,
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" + "RC" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        },
                        "aoColumnDefs": [
                             //{ "bVisible": false, "aTargets": [0] }
                        ],
                        "colVis": {
                            buttonText: 'Show / Hide Columns',
                            restore: "Restore",
                            showAll: "Show all"
                        }
                    });
                }
            }, 50);
        }

          vm.getClassification = function () {
              vm.viewModelHelper.apiGet('api/ifrsbond/getifrsbond/' + vm.classification, null,
                        function (result) {
                            vm.ifrsBonds = result.data;
                            InitialView();
                            exportTable.destroy();
                                          },
                       function (result) {
                           toastr.error('Fail to load Bond data', 'Fintrak Error');
                       }, null);
          }

        vm.getBondbyMatDate = function () {
              vm.viewModelHelper.apiGet('api/ifrsbond/getbondbymatdate/' + vm.MaturityDate.substr(0, 10), null,
                        function (result) {
                            vm.ifrsBonds = result.data;
                        },
                       function (result) {
                           toastr.error(result, 'Fintrak Error');
                       }, null);
        }

        vm.updateBondbyMatDate = function () {
            var params = { Date: vm.MaturityDate, Amount: vm.ifrsBond.CurrentMarketYield };
            vm.viewModelHelper.apiPost('api/ifrsbond/updatebondbymatdate', params,
                      function (result) {
                          $state.go('ifrs-bonddata-list');
                          vm.getUpdatedMarketYieldData();
                      },
                     function (result) {
                         toastr.error(result.data, 'Fintrak Error');                       
                     }, null);
        }
          var intializeLookUp = function () {
              distinctMaturityDate();
            
          }
          var distinctMaturityDate = function () {
              vm.viewModelHelper.apiGet('api/ifrsbond/getMaturityDate', null,
                   function (result) {
                       vm.distinctMaturityDate = result.data;
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
          }
          vm.getUpdatedMarketYieldData = function () {
              vm.viewModelHelper.apiGet('api/ifrsbond/availableifrsbond', null,
                  function (result) {
                      vm.ifrsBonds = result.data;
                  },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
          }
          vm.exportToExcel = function (tableId) { // ex: '#my-table'
              //var exportHref = Excel.tableToExcel(tableId, 'Balance Sheet Report');
              //$timeout(function () { location.href = exportHref; }, 100); // trigger download

              $(tableId).tableExport({ type: 'excel', escape: 'false' });
          }

        initialize(); 
    }
}());
