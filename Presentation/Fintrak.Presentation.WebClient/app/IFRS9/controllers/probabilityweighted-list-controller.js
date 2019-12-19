/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ProbabilityWeightedListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        ProbabilityWeightedListController]);

    function ProbabilityWeightedListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'probabilityweighted-list-view';
        vm.viewName = 'Probability Weigthing for ECL Scenario';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.selectedinstrumentType = 'Investment';
        vm.probabilityWeighted = [];

        vm.instrumentTypes = [
          { Id: 1, Name: 'Investment' },
          { Id: 2, Name: 'Corporate' },
          { Id: 3, Name: 'OBE' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/probabilityweighted/availableprobabilityweighteds', null,
                   function (result) {
                       vm.probabilityWeighted = result.data;
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
                if ($('#probabilityweightedTable').length > 0) {
                    var exportTable = $('#probabilityweightedTable').DataTable({
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


        vm.loadProbabilityWeighted = function () {
            vm.probabilityWeighted = [];
            vm.init = false;

            vm.viewModelHelper.apiGet('api/probabilityweighted/getprobabilityweightedbyinstrumenttype/' + vm.selectedinstrumentType, null,
                               function (result) {
                                   vm.probabilityWeighted = result.data;
                                   InitialView();
                                   vm.init === true;
                               },
                                       function (result) {
                                           toastr.error(result, 'Fintrak Error');
                                       }, null);
        }

        initialize(); 
    }
}());
