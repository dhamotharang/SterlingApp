/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("AdjustmentListController",
                    ['$rootScope', '$scope', '$state', 'viewModelHelper', 'validator', 'UploadService',
                        AdjustmentListController]);

    function AdjustmentListController($rootScope, $scope, $state, viewModelHelper, validator, UploadService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'adjustment-list-view';
        vm.viewName = 'GL Adjustments';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.glAdjustments = [];

        vm.adjustmentType = 1;
        vm.adjustmentCode = '';

        vm.adjustmentTypes = [
            { Id: 1, Name: 'GAAP' },
            { Id: 2, Name: 'IFRS' }
        ];

        vm.reportTypes = [
           { Id: 1, Name: 'IAS39' },
           { Id: 2, Name: 'IFRS9' }
        ];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.csv = {
            uploadCode: 'IFRS010',
            content: null,
            header: true,
            headerVisible: false,
            separator: ',',
            separatorVisible: false,
            result: null,
            encoding: 'ISO-8859-1',
            encodingVisible: true,
        };

        vm.importData = function ()
        {
            if (vm.adjustmentType === 1)
                vm.csv.uploadCode = 'IFRS010';
            else
                vm.csv.uploadCode = 'IFRS011';

            UploadService.runUpload(vm.csv);
        }

        var onUploadCompleted = function () {
            toastr.success('Data upload completed.', 'Fintrak');
            vm.load();
        }

        var initialize = function(){

            if (vm.init === false) {
                $rootScope.$on('uploadCompleted', onUploadCompleted);
                vm.load();
                InitialView();
                vm.init === true;
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
               
                // data export
                if ($('#glAdjustmentTable').length > 0) {
                    var exportTable = $('#glAdjustmentTable').DataTable({
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

        vm.load = function () {
           
            vm.viewModelHelper.apiGet('api/gladjustment/getgladjustments/' + vm.adjustmentType, null,
               function (result) {
                   vm.glAdjustments = result.data;
                   toastr.success('Adjustments loaded successfully.', 'Fintrak');
               },
               function (result) {
                   toastr.error('Fail to load adjustments', 'Fintrak Error');
               }, null);
        }

        vm.reverseAdjustment = function () {

            vm.viewModelHelper.apiPost('api/gladjustment/reversegladjustmentbycode/' + vm.adjustmentType + '/' + vm.adjustmentCode, null,
               function (result) {
                   toastr.success('Adjustment reversal successful.', 'Fintrak');
                   vm.load();
               },
               function (result) {
                   toastr.error('Fail to reversed adjustment', 'Fintrak Error');
               }, null);
        }

        vm.purgeAdjustment = function () {

            vm.viewModelHelper.apiPost('api/gladjustment/purgegladjustmentbycode/' + vm.adjustmentType, null,
               function (result) {
                   toastr.success('Purging of adjustments successful.', 'Fintrak');
                   vm.load();
               },
               function (result) {
                   toastr.error('Fail to purge adjustments', 'Fintrak Error');
               }, null);
        }

        initialize(); 
    }
}());
