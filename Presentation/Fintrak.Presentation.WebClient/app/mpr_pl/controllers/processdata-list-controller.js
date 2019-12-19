/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ProcessDataListController",
                    ['$rootScope','$scope', '$state', 'viewModelHelper', 'validator','UploadService',
                        ProcessDataListController]);

    function ProcessDataListController($rootScope,$scope, $state, viewModelHelper, validator,UploadService)  {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'processdata-list-view';
        vm.viewName = 'Transaction Data';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.processData = [];


        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


         vm.csv = {
            uploadCode: 'MPR055',
            content: null,
            header: true,
            headerVisible: false,
            separator: ',',
            separatorVisible: false,
            result: null,
            encoding: 'ISO-8859-1',
            encodingVisible: true,
            //truncate: true,
            //postUploadAction: true,
         };


        //    vm.csv1 = {
        //    uploadCode1: 'MPR056',
        //    content1: null,
        //    header1: true,
        //    headerVisible1: false,
        //    separator1: ',',
        //    separatorVisible1: false,
        //    result1: null,
        //    encoding1: 'ISO-8859-1',
        //    encodingVisible1: true,
        //};

        vm.importData = function () {
      
            vm.csv.uploadCode = 'MPR055';
            UploadService.runUpload(vm.csv);
        }

        // vm.importData1 = function () {
      
        //    vm.csv1.uploadCode1 = 'MPR056';
        //    UploadService.runUpload1(vm.csv1);
        //}

         var onUploadCompleted = function () {
              vm.viewModelHelper.apiGet('api/processdata/availableprocessData', null,
                  function (result) {
                      vm.processData = result.data;
                  },
                  function (result) {
                      alert("Fail");
                  }, null);
            toastr.success('Data upload completed.', 'Fintrak');
            //vm.load();
        }

        // var onUploadCompleted1 = function () {
        //    toastr.success('Data upload completed.', 'Fintrak');
        //    //vm.load();
        //}


        var initialize = function(){

            if (vm.init === false) {
                 $rootScope.$on('uploadCompleted', onUploadCompleted);
                vm.viewModelHelper.apiGet('api/processdata/availableprocessData', null,
                   function (result) {
                       vm.processData = result.data;
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
                if ($('#processDataTable').length > 0) {
                    var exportTable = $('#processDataTable').DataTable({
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
