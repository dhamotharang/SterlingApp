/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("UnquotedEquityFairvalueResultListController",
                    ['$scope', '$state','$window', 'viewModelHelper', 'validator',
                        UnquotedEquityFairvalueResultListController]);

    function UnquotedEquityFairvalueResultListController($scope,$state,$window, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'unquotedequityfairvalueresult-list-view';
        vm.viewName = 'Equity FairValue Result';
        var stt;
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        //vm. pdtblstatus = true;
        //vm.lgdtblstatus = false;
        vm.unquotedEquityFairvalueResult = [];
       
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/unquotedequityfairvalueresult/availableunquotedEquityFairvalueResults', null,
                   function (result) {
                       vm.unquotedEquityFairvalueResult = result.data;
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
                if ($('#UnquotedEquityFairvalueResultTable').length > 0) {
                    var exportTable = $('#UnquotedEquityFairvalueResultTable').DataTable({
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

    
        initialize(); 
    }
}());
