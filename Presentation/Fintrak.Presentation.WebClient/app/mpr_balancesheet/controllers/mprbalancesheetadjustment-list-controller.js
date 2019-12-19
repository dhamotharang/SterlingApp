///**
// * Created by Deb on 8/20/2014.
// */
//(function () 
//{
//    "use strict";
//    angular
//        .module("fintrak")
//        .controller("MPRBalanceSheetAdjustmentListController",
//                    ['$rootScope', '$scope', '$state', 'viewModelHelper', 'validator', 'UploadService',
//                        MPRBalanceSheetAdjustmentListController]);

//    function MPRBalanceSheetAdjustmentListController($rootScope,$scope, $state, viewModelHelper, validator, UploadService) {
//        var vm = this;
//        vm.viewModelHelper = viewModelHelper;
//        vm.parentController = $scope.$parent;

//        vm.module = 'MPR Core';
//        vm.view = 'mprbalancesheetadjustment-list-view';
//        vm.viewName = 'BalanceSheet Adjustment';


//        vm.viewModelHelper.modelIsValid = true;
//        vm.viewModelHelper.modelErrors = [];
        
//        vm.mprBalanceSheetAdjustments = [];
//        vm.codebyUser = [];

//          vm.selectedsearchType = '';
//        vm.searchValue = '';
//        vm.number = 0;
//        vm.userName = '';

//        vm.searchTypes = [
//            { Id: 1, Name: 'Product' },
//            { Id: 2, Name: 'AccountNo' },
//            { Id: 3, Name: 'AccountName' },
//            { Id: 4, Name: 'Team' },
//            { Id: 5, Name: 'AccountOfficer' }
//        ];

//        vm.init = false;
//        vm.showInstruction = false;
//        vm.instruction = '';

//        vm.csv = {
//            uploadCode: 'MPR018',
//            content: null,
//            header: true,
//            headerVisible: false,
//            separator: ',',
//            separatorVisible: false,
//            result: null,
//            encoding: 'ISO-8859-1',
//            encodingVisible: true,
//        };

//        vm.importData = function () {
      
//            vm.csv.uploadCode = 'MPR018';
//            UploadService.runUpload(vm.csv);
//        }

//        var onUploadCompleted = function () {
//            toastr.success('Data upload completed.', 'Fintrak');
//            vm.load();
//        }

//        var initialize = function () {

//            if (vm.init === false) {
//                $rootScope.$on('uploadCompleted', onUploadCompleted);
//                vm.viewModelHelper.apiGet('api/mprbalancesheetadjustment/availablemprbalancesheetadjustment', null,
//                function (result) {
//                    vm.mprBalanceSheetAdjustments = result.data;
//                    InitialView();
//                    vm.init === true;
                    
//                },
//                 function (result) {
//                     toastr.error(result.data, 'Fintrak');
//                 }, null);
//                //InitialView();
//                //vm.init === true;
//            }
//        }
//        vm.load = function () {

//            vm.viewModelHelper.apiGet('api/mprbalancesheetadjustment/availablemprbalancesheetadjustment', null,
//               function (result) {
//                   vm.mprBalanceSheetAdjustments = result.data;
//                   //  InitialView();
//                   // vm.init === false;
                   
//               },
//                 function (result) {
//                     toastr.error(result.data, 'Fintrak');
//                 }, null);
//        }
//            var InitialView = function () {
//                InitialGrid();
//            }

//            var InitialGrid = function () {
//                setTimeout(function () {

//                    // data export
//                    if ($('#mprBalanceSheetAdjustmentTable').length > 0) {
//                        var exportTable = $('#mprBalanceSheetAdjustmentTable').DataTable({
//                            "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
//                            sDom: "T<'clearfix'>" +
//                                "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
//                                "t" +
//                                "<'row'<'col-sm-6'i><'col-sm-6'p>>",
//                            "tableTools": {
//                                "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
//                            }
//                        });
//                    }
//                }, 50);
//            }
//          vm.loadBalanceSheetAdjustments = function (initialized) {

//            vm.viewModelHelper.apiGet('api/mprbalancesheetadjustment/getbalancesheetadjustment/' + vm.selectedsearchType  + '/' + vm.searchValue + '/' + vm.number, null,
//                  function (result) {

//                      vm.mprBalanceSheetAdjustments = result.data;
 
//                      if (vm.init)
//                      {
//                          InitialView();
//                          vm.init === true;
//                      }

                      
//                  },
//                function (result) {
//                    toastr.error(result.data, 'Fintrak');
//                }, null);
//        }

     
//            initialize();
//        }
//    }());
/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MPRBalanceSheetAdjustmentListController",
                    ['$rootScope', '$scope', '$state', 'viewModelHelper', 'validator', 'UploadService',
                        MPRBalanceSheetAdjustmentListController]);

    function MPRBalanceSheetAdjustmentListController($rootScope, $scope, $state, viewModelHelper, validator, UploadService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'mprbalancesheetadjustment-list-view';
        vm.viewName = 'BalanceSheet Adjustment';


        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.mprBalanceSheetAdjustments = [];
        vm.codebyUser = [];

        vm.selectedsearchType = '';
        vm.searchValue = '';
        vm.number = 0;
        vm.userName = '';

        vm.searchTypes = [
            { Id: 1, Name: 'Product' },
            { Id: 2, Name: 'AccountNo' },
            { Id: 3, Name: 'AccountName' },
            { Id: 4, Name: 'Team' },
            { Id: 5, Name: 'AccountOfficer' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.csv = {
            uploadCode: 'MPR018',
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

            vm.csv.uploadCode = 'MPR018';
            UploadService.runUpload(vm.csv);
        }

        var onUploadCompleted = function () {
            toastr.success('Data upload completed.', 'Fintrak');
            vm.load();
        }

        var initialize = function () {
            intializeLookUp();

            if (vm.init === false) {
                $rootScope.$on('uploadCompleted', onUploadCompleted);
                vm.viewModelHelper.apiGet('api/mprbalancesheetadjustment/availablemprbalancesheetadjustment', null,
                function (result) {
                    vm.mprBalanceSheetAdjustments = result.data;
                    InitialView();
                    vm.init === true;

                },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
                //InitialView();
                //vm.init === true;
            }
        }
        vm.load = function () {

            vm.viewModelHelper.apiGet('api/mprbalancesheetadjustment/availablemprbalancesheetadjustment', null,
               function (result) {
                   vm.mprBalanceSheetAdjustments = result.data;
                   //  InitialView();
                   // vm.init === false;

               },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }
        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#mprBalanceSheetAdjustmentTable').length > 0) {
                    var exportTable = $('#mprBalanceSheetAdjustmentTable').DataTable({
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
        vm.loadBalanceSheetAdjustments = function (initialized) {

            vm.viewModelHelper.apiGet('api/mprbalancesheetadjustment/getbalancesheetadjustment/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
                  function (result) {

                      vm.mprBalanceSheetAdjustments = result.data;

                      if (vm.init) {
                          InitialView();
                          vm.init === true;
                      }


                  },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        vm.loadBalanceSheetAdjustmentsbycode = function (initialized) {

            vm.viewModelHelper.apiGet('api/mprbalancesheetadjustment/getbalancesheetadjustment/' + vm.Code, null,
                  function (result) {

                      vm.mprBalanceSheetAdjustments = result.data;

                      if (vm.init) {
                          InitialView();
                          vm.init === true;
                      }


                  },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        var intializeLookUp = function () {
            getCodebyUser();

        }

        var getCodebyUser = function () {
            vm.viewModelHelper.apiGet('api/mprbalancesheetadjustment/getCodebyUser', null,
                 function (result) {
                     vm.codebyUser = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }


        vm.deletemprbalancesheetadjustment = function () {
            var params = { Code: vm.Code, userName: vm.userName };
            vm.viewModelHelper.apiPost('api/mprbalancesheetadjustment/deletebalancesheetadjustmentbycode', params,
                      function (result) {
                          toastr.success('Deleted Successfully.', 'Fintrak');
                          vm.load();
                          getCodebyUser();
                      },
                     function (result) {
                         toastr.error(result.data, 'Fintrak Error');
                     }, null);
        }


        initialize();
    }
}());
