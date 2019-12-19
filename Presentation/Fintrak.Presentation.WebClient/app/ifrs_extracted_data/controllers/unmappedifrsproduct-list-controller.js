/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("UnMappedIFRSProductListController",
                    ['$scope', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        UnMappedIFRSProductListController]);

    function UnMappedIFRSProductListController($scope, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_Processed_Data';
        vm.view = 'unmappedifrsproduct-list-view';
        vm.viewName = 'UnMapped IFRS Products';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.unMappedProducts = [];
        vm.product = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/unmappedproduct/availableunmappedproduct', null,
                   function (result) {
                       vm.unMappedProducts = result.data;
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
                if ($('#unMappedProductTable').length > 0) {
                    var exportTable = $('#unMappedProductTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
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
        vm.MapProduct = function (globalPrd,prdCode,prdName)
        {

            if (globalPrd === false)
               
            {
             $state.go('core-product-edit', { productId: 0,code:prdCode, name:prdName, context: 'core-product-edit' });
            }  
            else 
            {
                $state.go('ifrsloan-product-edit', { productId: 0, code: prdCode,  context: 'ifrsloan-product-edit' });
           
                
        }
        }
        initialize(); 
    }
}());
