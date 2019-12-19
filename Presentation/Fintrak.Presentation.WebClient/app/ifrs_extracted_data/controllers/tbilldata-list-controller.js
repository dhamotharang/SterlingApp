/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IFRSTbillListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IFRSTbillListController]);

    function IFRSTbillListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_Tbill_Data';
        vm.view = 'tbilldata-list-view';
        vm.viewName = 'Treasury Bills | Commercial Papers Data';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.classification = '';
        vm.MaturityDate = '';
        

           vm.classifications = [
          { Id: 1, Name: 'Amortised Cost' },
          { Id: 2, Name: 'FVPL' },
          { Id: 3, Name: 'FVOCI' }
        ];
        
        vm.ifrsTbills = [];
        vm.distinctMaturityDate = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;
        var tabID = '';

        var initialize = function(){
            //load lookups
            //intializeLookUp();

            distinctMaturityDate(1);
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/ifrstbill/getlistbytype/' + 1, null,
                   function (result) {
                       vm.ifrsTbills = result.data;
                       InitialView('ifrsTbillTable');
                       vm.init === true;
                                         },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        var InitialView = function (tableID) {
            InitialGrid(tableID);
        }

        var InitialGrid = function (tableID) {
            tabID = '#' + tableID
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
            }, 50);
        }

        vm.getClassification = function (Type) {
            if (vm.classification === '') {
                toastr.warning('Please provide a classification', 'Empty Classification');
                return
            };

              vm.viewModelHelper.apiGet('api/ifrstbill/getifrstbill/' + vm.classification + '/' + Type, null,
                        function (result) {
                            vm.ifrsTbills = result.data;
                            if (Type === 1) {
                                InitialView('ifrsTbillTable');
                            }
                            else if (Type === 2) {
                                InitialView('ifrsComPaperTable');
                            }

                            exportTable.destroy();
                        },
                       function (result) {
                           toastr.error(result, 'Fintrak Error');
                       }, null);
          }

        vm.getTbillbyMatDate = function (Type) {
            if (vm.MaturityDate === '') {
                toastr.warning('Please provide a Maturity Date', 'Empty Maturity Date');
                return
            };

            vm.viewModelHelper.apiGet('api/ifrstbill/gettbillbymatdate/' + vm.MaturityDate.substr(0, 10) + '/' + Type, null,
                        function (result) {
                            vm.ifrsTbills = result.data;
                            if (Type === 1) {
                                InitialView('ifrsTbillTable');
                            }
                            else if (Type === 2) {
                                InitialView('ifrsComPaperTable');
                            }

                            exportTable.destroy();
                        },
                       function (result) {
                           toastr.error(result, 'Fintrak Error');
                       }, null);
          }
          var intializeLookUp = function () {
              distinctMaturityDate();
            
          }

          var distinctMaturityDate = function (Type) {
              vm.viewModelHelper.apiGet('api/ifrstbill/getMaturityDate/' + Type, null,
                   function (result) {
                       vm.distinctMaturityDate = result.data;
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
          }
          vm.updateTbillsbyMatDate = function () {
              if (vm.MaturityDate === '') {
                  toastr.warning('Please provide a Maturity Date first', 'Empty Maturity Date');
                  return
              }
              else if (vm.ifrsTbills.CurrentMarketYield === '') {
                  toastr.warning('Please provide Current Market Yield', 'Empty Current Market Yield');
                  return
              }

              var params = { Date: vm.MaturityDate, Amount: vm.ifrsTbills.CurrentMarketYield };
              vm.viewModelHelper.apiPost('api/ifrstbill/updatetbillbymatdate', params,
                        function (result) {
                            $state.go('ifrs-tbills-list');
                            vm.getUpdatedMarketYieldData();
                        },
                       function (result) {
                           toastr.error(result.data, 'Fintrak Error');
                       }, null);
          }
        
          vm.getUpdatedMarketYieldData = function () {
              vm.viewModelHelper.apiGet('api/ifrstbill/availableifrstbill', null,
                  function (result) {
                      vm.ifrsTbills = result.data;
                  },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
          }

          vm.loadtBillscomPapers = function (Type) {
              vm.viewModelHelper.apiGet('api/ifrstbill/getlistbytype/' + Type, null,
                  function (result) {
                      vm.ifrsTbills = result.data;
                      if (Type === 1) {
                          distinctMaturityDate(1);
                          InitialView('ifrsTbillTable');
                      }
                      else if (Type === 2) {
                          distinctMaturityDate(2);
                          InitialView('ifrsComPaperTable');
                      }

                      exportTable.destroy();
                  },
                  function (result) {
                      toastr.error(result.data, 'Fintrak');
                  }, null);
          }

          initialize();
    }
}());
