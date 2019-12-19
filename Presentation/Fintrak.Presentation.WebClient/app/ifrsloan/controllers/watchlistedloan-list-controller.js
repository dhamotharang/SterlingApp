/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("WatchListedLoanListController",
                    ['$rootScope', '$scope', '$state', 'viewModelHelper', 'validator', 'UploadService',
                        WatchListedLoanListController]);

    function WatchListedLoanListController($rootScope, $scope, $state, viewModelHelper, validator, UploadService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'watchlistedloan-list-view';
        vm.viewName = 'WatchListed Loans';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.watchListedLoans = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.csv = {
            uploadCode: 'IFRS003',
            content: null,
            header: true,
            headerVisible: false,
            separator: ',',
            separatorVisible: false,
            result: null,
            encoding: 'ISO-8859-1',
            encodingVisible: true,
        };

        vm.importData = function () {
            UploadService.runUpload(vm.csv);
        }

        var onUploadCompleted = function () {
            vm.viewModelHelper.apiGet('api/watchlistedloan/availablewatchlistedloans', null,
                  function (result) {
                      vm.watchListedLoans = result.data;
                    
                      
                  },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var initialize = function(){

            if (vm.init === false) {
                $rootScope.$on('uploadCompleted', onUploadCompleted);
                vm.viewModelHelper.apiGet('api/watchlistedloan/availablewatchlistedloans', null,
                   function (result) {
                       vm.watchListedLoans = result.data;
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
                if ($('#watchListedLoanTable').length > 0) {
                    var exportTable = $('#watchListedLoanTable').DataTable({
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
