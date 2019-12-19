/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ImpairmentOverrideListController",
                    ['$rootScope', '$scope', '$state', 'viewModelHelper', 'validator', 'UploadService',
                        ImpairmentOverrideListController]);

    function ImpairmentOverrideListController($rootScope, $scope, $state, viewModelHelper, validator, UploadService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'impairmentoverride-list-view';
        vm.viewName = 'Impairment Override';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.impairmentOverrides = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.csv = {
            uploadCode: 'IFRS004',
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
            vm.viewModelHelper.apiGet('api/impairmentOverride/availableimpairmentoverrides', null,
                   function (result) {
                       vm.impairmentOverrides = result.data;
                       
                   },
                   function (result) {
                       alert("Fail");
                   }, null);
        }

        var initialize = function(){

            if (vm.init === false) {
                $rootScope.$on('uploadCompleted', onUploadCompleted);
                vm.viewModelHelper.apiGet('api/impairmentOverride/availableimpairmentoverrides', null,
                   function (result) {
                       vm.impairmentOverrides = result.data;
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
                if ($('#impairmentOverrideTable').length > 0) {
                    var exportTable = $('#impairmentOverrideTable').DataTable({
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
