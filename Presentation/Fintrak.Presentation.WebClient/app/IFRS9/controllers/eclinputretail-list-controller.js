/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ECLInputRetailController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        ECLInputRetailController]);

    function ECLInputRetailController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'eclinputretail-list-view';
        vm.viewName = 'eclinputretail';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

       
        vm.eclinputretails = [];

      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/eclinputretail/availableeclinputretails/', null,
                   function (result) {
                       vm.eclinputretails = result.data;
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
                if ($('#eclinputretailTable1').length > 0) {
                    var exportTable = $('#eclinputretailTable').DataTable({
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


        vm.loadbyrefno = function () {
      
            vm.viewModelHelper.apiGet('api/eclinputretail/geteclinputretailsbyrefno/' + vm.eclinputretails.account_number, null,
                               function (result) {
                                   vm.eclinputretails = result.data;
                                   vm.eclinputretails.account_number = result.data[0].account_number;
                               },
                                       function (result) {
                                           toastr.error(result, 'Fintrak Error');
                                       }, null);
        }
        vm.loadDefault = function () {
        
            vm.viewModelHelper.apiGet('api/eclinputretail/availableeclinputretails/', null,
                               function (result) {
                                   vm.eclinputretails = result.data;
                                  
                               },
                                       function (result) {
                                           toastr.error(result, 'Fintrak Error');
                                       }, null);
        }


        initialize(); 
    }
}());
