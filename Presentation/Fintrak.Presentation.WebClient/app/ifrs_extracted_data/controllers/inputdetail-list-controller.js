/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("InputDetailListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        InputDetailListController]);

    function InputDetailListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS';
        vm.view = 'inputdetail-list-view';
        vm.viewName = 'ECL Test Simmulation';
        vm.viewName2 = 'ECL Weighted Average Results';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.inputDetail = [];
        vm.eclWeightedAvg = [];
        vm.refNo = '';
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/inputdetail/availableinputdetails', null,
                   function (result) {
                       vm.inputDetail = result.data;
                       InitialView();
                       vm.init === true;
                        displayEclAvgs();
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
                if ($('#inputDetailTable1').length > 0) {
                    exportTable = $('#inputDetailTable').DataTable({
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

          vm.insertbyrefno = function () {
            if (vm.refNo === ''){
            window.alert('Please provide a Reference Number!');
                return;
             }
            else if (alphanumeric(vm.refNo) === false){
                //window.alert('Please provide a valid Reference Number of alphanumeric format');
                toastr.error('Please provide a valid Reference Number of alphanumeric format', 'Fintrak');
                return;
                   };

              vm.viewModelHelper.apiGet('api/inputdetail/insertbyrefno/' + vm.refNo, null,
                        function (result) {
                            if (result.data === 1) {
                              toastr.success('RefNo Successfully Loaded', 'Fintrak');
                              vm.init = false;
                              vm.refNo = '';
                              initialize();
                            }
                              else {
                                    toastr.error('RefNo Not Found', 'Fintrak');
                                }
                          },
                        function (result) {
                           toastr.error(result.data, 'fintrak error');
                       }, null);
                }

          vm.computeECL = function () {
              vm.viewModelHelper.apiGet('api/inputdetail/computeecl', null,
                        function (result) {
                            toastr.success('Average ECL Successfully Computed', 'Fintrak');
                            displayEclAvgs();
                          },
                       function (result) {
                           toastr.error(result.data, 'fintrak error');
                       }, null);
                }

          var displayEclAvgs = function () {
              vm.viewModelHelper.apiGet('api/inputdetail/availableeclweightedavgs', null,
                        function (result) {
                            vm.eclWeightedAvg = result.data;
                          },
                       function (result) {
                           toastr.error('fail to load EclWeightedAvg Result', 'fintrak error');
                       }, null);
                }

        vm.delete = function (inputDetailId) {
            var deleteFlag = window.confirm(' Are you sure you want to delete' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/inputdetail/deleteinputdetail', inputDetailId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                       initialize();
                       //exportTable = exportTable.destroy();
                  //$state.go('ifrs-inputdetail-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

            // Function to check letters and numbers
            function alphanumeric(inputtxt) { 
              var xters = /^[0-9a-zA-Z]+$/;
              if (xters.test(inputtxt)) {
                //alert('accepted');
                return true;
              } else {
                //alert('Please input a valid RefNo of alphanumeric format');
                return false;
              }
            }

        initialize(); 
    }
}());
