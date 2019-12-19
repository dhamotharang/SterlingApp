/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsPdSeriesByRatingListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IfrsPdSeriesByRatingListController]);

    function IfrsPdSeriesByRatingListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrspdseriesbyrating-list-view';
        vm.viewName = 'PD Series By Rating';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.ifrspdseriesbyratings = [];
        vm.ratings = [];
        vm.disabled = false;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var exportTable;

        var loadAvailableRatings = function () {
            vm.viewModelHelper.apiGet('api/ifrspdseriesbyrating/availableRatings', null,
                   function (result) {
                       vm.ratings = result.data;
                       InitialView();
                       vm.init = true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var initialize = function(){

            if (vm.init === false) {
                loadAvailableRatings();
            }
        }

        vm.getIfrsPdSeriesByRating = function (Rating) {
            Rating = Rating.replace('+', 'p');
            vm.disabled = true;
            if (Rating) {
                vm.viewModelHelper.apiGet('api/ifrspdseriesbyrating/getIfrsPdSeriesByRating/' + Rating, null,
                   function (result) {
                       vm.ifrsPdSeriesByRatings = result.data;
                       InitialView();
                       exportTable.destroy();
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
                if ($('#ifrsPdSeriesByRatingTable').length > 0) {
                    exportTable = $('#ifrsPdSeriesByRatingTable').DataTable({
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

