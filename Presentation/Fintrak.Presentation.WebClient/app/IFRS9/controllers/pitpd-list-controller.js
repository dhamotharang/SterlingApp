/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("PiTPDListController",
                    ['$scope', '$state','$window', 'viewModelHelper', 'validator',
                        PiTPDListController]);

    function PiTPDListController($scope,$state,$window, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'pitpd-list-view';
        vm.viewName = 'Computed PiT PD';
        var stt;
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        //vm. pdtblstatus = true;
        //vm.lgdtblstatus = false;
        vm.pitPD = [];
       
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/pitpd/availablepitPDs', null,
                   function (result) {
                       vm.pitPD = result.data;
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
          //  InitialGrid2();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#pitPDTable').length > 0) {
                    var exportTable = $('#pitPDTable').DataTable({
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
        vm.regresspd = function () {
            var regressFlag = $window.confirm(' Are you sure you want to Regress PD for the Active Period');
            if (regressFlag) {
               
                vm.viewModelHelper.apiPost('api/pitpd/regresspd', null,
                          function (result) {
                              toastr.success('PD regression Completed Successfully.', 'Fintrak');
                             refreshTable();
                             alert('PD regression Completed Successfully');
                          },
                         function (result) {
                             toastr.error(result.data, 'Fintrak Error');
                         }, null);
            }
        }

        var refreshTable = function () {
                vm.viewModelHelper.apiGet('api/pitpd/availablepitPDs', null,
                   function (result) {
                       vm.pitPD = result.data;
                       //InitialView();
                       //vm.init === true;


                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
           
        }
        initialize(); 
    }
}());
