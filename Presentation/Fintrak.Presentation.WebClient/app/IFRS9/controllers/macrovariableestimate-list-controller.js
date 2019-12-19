/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MacrovariableEstimateListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        MacrovariableEstimateListController]);

    function MacrovariableEstimateListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'macrovariableestimate-list-view';
        vm.viewName = 'Macrovariable Estimates';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.selectedcategory = 'Corporate';
        vm.macrovariableEstimate = [];

        vm.categories = [
          { Id: 1, Name: 'Retail' },
          { Id: 2, Name: 'Corporate' },
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/macrovariableestimate/getmacrovariableestimatebycategory/C', null,
                   function (result) {
                       vm.macrovariableEstimate = result.data;
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
                if ($('#macrovariableestimateTable1').length > 0) {
                    var exportTable = $('#macrovariableestimateTable').DataTable({
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


        vm.loadMacrovariableEstimate = function () {
            vm.macrovariableEstimate = [];
            //vm.init = false;
            var searchParam = (vm.selectedcategory == 'Corporate') ? 'C' : 'R';

            vm.viewModelHelper.apiGet('api/macrovariableestimate/getmacrovariableestimatebycategory/' + searchParam, null,
                               function (result) {
                                   vm.macrovariableEstimate = result.data;
                                   //InitialView();
                                   //vm.init === true;
                               },
                                       function (result) {
                                           toastr.error(result, 'Fintrak Error');
                                       }, null);
        }

        initialize(); 
    }
}());
