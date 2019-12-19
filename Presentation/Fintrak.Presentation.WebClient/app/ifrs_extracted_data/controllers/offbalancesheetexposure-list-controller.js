/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OffBalanceSheetExposureListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        OffBalanceSheetExposureListController]);

    function OffBalanceSheetExposureListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Off_BalanceSheet_Expsoure';
        vm.view = 'offbalancesheetexposure-list-view';
        vm.viewName = 'Off BalanceSheet Exposure';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.portfolio = '';
        
        

        vm.Portfolios = [
          { Id: 1, Name: 'CORPORATE' },
          { Id: 2, Name: 'Non-Corporate' }       
        ];
        
        vm.OffBalanceSheetExposures = [];
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){
            //load lookups
            intializeLookUp();
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/offbalancesheetexposure/availableoffbalancesheetexposure', null,
                   function (result) {
                       vm.OffBalanceSheetExposures = result.data;
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
                if ($('#OffBalanceSheetExposureTable').length > 0) {
                    var exportTable = $('#OffBalanceSheetExposureTable').DataTable({
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

          vm.getPortfolio = function () {
              vm.viewModelHelper.apiGet('api/offbalancesheetexposure/getoffbalancesheetexposure/' + vm.Portfolio, null,
                        function (result) {
                            vm.OffBalanceSheetExposures = result.data;
                                          },
                       function (result) {
                           toastr.error(result, 'Fintrak Error');
                       }, null);
          }

         
          var intializeLookUp = function () {
             // distinctMaturityDate();
            
          }

          
        
         

          initialize();
    }
}());
