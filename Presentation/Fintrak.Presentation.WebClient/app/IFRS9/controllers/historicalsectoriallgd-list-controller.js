/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("HistoricalSectorialLGDListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        HistoricalSectorialLGDListController]);

    function HistoricalSectorialLGDListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'historicalsectoriallgd-list-view';
        vm.viewName = 'Historical LGD Computation / View' 
        var stt;
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm. pdtblstatus = true;
        vm.lgdtblstatus = true;
        vm.historicalSectorialLGDs = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/historicalsectoriallgd/availablehistoricalSectorialLGDs', null,
                   function (result) {
                       vm.historicalSectorialLGDs = result.data;
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
                if ($('#historicalSectorialLGDTable').length > 0) {
                    var exportTable = $('#historicalSectorialLGDTable').DataTable({
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
  


        vm.pd = function (stt) {
            vm.clearpd();
            if (stt) {

                (vm.pdtblstatus =true);              
                (vm.lgdtblstatus = false);
               // alert('Hide LGD table and show LGD table');
            }

            else {
                vm.pdtblstatus === false;
                vm.lgdtblstatus === true;

            }

        }
        vm.lgd = function (stt) {
           vm.clearlgd();
            if (!stt) {
                (vm.pdtblstatus = false);
                (vm.lgdtblstatus = true);
              //  alert('Hide LGD table and show LGD table');
            }
            else {
                vm.pdtblstatus === true;
                vm.lgdtblstatus === false;

            }

        }

        vm.clearlgd = function () {
            vm.pdtblstatus === false;
            vm.lgdtblstatus === true;
          
          
        }
        vm.clearpd = function () {
            
            vm.pdtblstatus === false;
            vm.lgdtblstatus === false;
           
        }
        initialize(); 
    }
}());
