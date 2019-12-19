/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RunUploadListController",
                    ['$scope', '$state', '$http', 'viewModelHelper', 'validator', 'ExcelService',
                        RunUploadListController]);

    function RunUploadListController($scope, $state, $http, viewModelHelper, validator, ExcelService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'runupload-list-view';
        vm.viewName = 'Run Uploads';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.uploadSelection = {};
        vm.uploadDatas = [];

        vm.uploads = [];
        vm.myFile = {};

        vm.showPreview = false;
        vm.showJSONPreview = true;
        vm.json_string = "";
        vm.selectedUploadItem = null;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                initializeUploads();
            }
        }

        var initializeUploads = function () {
            getUploads();
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                // Init
                //var spinner = $(".spinner").spinner();
                var table = $('#runuploadTable').dataTable({
                    "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]]
                });

                var tableTools = new $.fn.dataTable.TableTools(table, {
                    "sSwfPath": "../app/vendors/DataTables/extensions/TableTools/swf/copy_csv_xls_pdf.swf",
                    "buttons": [
                        "copy",
                        "csv",
                        "xls",
                        "pdf",
                        { "type": "print", "buttonText": "Print me!" }
                    ]
                });
                $(".DTTT_container").css("float", "right");

                
            }, 50);
        }

        $scope.fileChanged = function (files) {
            vm.isProcessing = true;
            vm.sheets = [];
            vm.excelFile = files[0];
            ExcelService.readFile(vm.excelFile, vm.showPreview, $scope.showJSONPreview).then(function (xlsxData) {
                vm.sheets = xlsxData.sheets;
                vm.isProcessing = false;
            });

            //var fd = new FormData();
            //fd.append('file', vm.excelFile);
            //vm.uploadSelection.Attachment = fd;
            //var uploadUrl = '~/api/runupload/uploaddata'
            //$http.post(uploadUrl, fd, {
            //    transformRequest: angular.identity,
            //    headers: { 'Content-Type': undefined }
            //})
            //.success(function () {
            //})
            //.error(function () {
            //});
        }

        vm.updateJSONString = function () {
            vm.json_string = JSON.stringify(vm.sheets[vm.selectedSheetName], null, 2);
        }

        vm.showPreviewChanged = function () {
            if (vm.showPreview) {
                vm.showJSONPreview = false;
                vm.isProcessing = true;
                ExcelService.readFile(vm.excelFile, vm.showPreview, vm.showJSONPreview).then(function (xlsxData) {
                    vm.sheets = xlsxData.sheets;
                    vm.isProcessing = false;
                });
            }
        }

        vm.submitUpload = function () {
            var fd = new FormData();
            fd.append('file', vm.myFile);
            var uploadUrl = Fintrak.rootPath + 'api/runupload/uploaddata'
            $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                //headers: { 'Content-Type': undefined }
            })
            .success(function () {
            })
            .error(function () {
            });
            //vm.uploadSelection.Attachment = vm.excelFile;
            //var data = getModelAsFormData(vm.uploadSelection);
            //vm.viewModelHelper.apiPost('api/runupload/uploaddata', vm.uploadSelection,
            //      function (result) {
            //          //vm.uploadResult = result.data;
            //      },
            //      function (result) {
            //          alert("Fail");
            //      }, null);
        };

        var getModelAsFormData = function (data) {
            var dataAsFormData = new FormData();
            angular.forEach(data, function (value, key) {
                dataAsFormData.append(key, value);
            });
            return dataAsFormData;
        };

        var getUploads = function () {
            vm.viewModelHelper.apiGet('api/upload/getuploads', null,
                   function (result) {
                       vm.uploads = result.data;
                   },
                   function (result) {
                       alert("Fail");
                   }, null);
        }

        initialize(); 
    }
}());
