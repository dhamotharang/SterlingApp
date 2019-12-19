/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("PLIncomeReportAdjustmentListController",
                    ['$rootScope', '$scope', '$state', 'viewModelHelper', 'validator', 'UploadService',
                        PLIncomeReportAdjustmentListController]);

    function PLIncomeReportAdjustmentListController($rootScope, $scope, $state, viewModelHelper, validator, UploadService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'plincomereportadjustment-list-view';
        vm.viewName = 'PL Incomereport Adjustment';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.plIncomereportAdjustments = [];


        vm.selectedsearchType = '';
        vm.searchValue = '';
        vm.number = 0;

        vm.searchTypes = [
            { Id: 1, Name: 'Caption' },
            { Id: 2, Name: 'GLCode' },
            { Id: 3, Name: 'Narrative' },
            { Id: 4, Name: 'Team' },
            { Id: 5, Name: 'AccountOfficer' },
            { Id: 6, Name: 'RelatedAccount' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.csv = {
            uploadCode: 'MPR017',
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
            //if (vm.adjustmentType === 1)
            vm.csv.uploadCode = 'MPR017';
            //else
            //    vm.csv.uploadCode = 'IFRS011';

            UploadService.runUpload(vm.csv);
        }

        var onUploadCompleted = function () {
            toastr.success('Data upload completed.', 'Fintrak');
            vm.load();
        }

        //var initializeupload = function () {

        //    if (vm.init === false) {
        //        $rootScope.$on('uploadCompleted', onUploadCompleted);
        //        vm.load();
        //        InitialView();
        //        vm.init === true;
        //    }
        //}

        var initialize = function () {
            intializeLookUp();

            if (vm.init === false) {
                $rootScope.$on('uploadCompleted', onUploadCompleted);
                vm.viewModelHelper.apiGet('api/plincomereportadjustment/availableplIncomeReportAdjustment', null,
                   function (result) {
                       vm.plIncomeReportAdjustment = result.data;
                       //vm.load = result.data
                       InitialView();
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        vm.load = function () {

            vm.viewModelHelper.apiGet('api/plincomereportadjustment/availableplIncomeReportAdjustment', null,
               function (result) {
                   vm.plIncomeReportAdjustment = result.data;
                   toastr.success('Adjustments loaded successfully.', 'Fintrak');
               },
               function (result) {
                   toastr.error('Fail to load adjustments', 'Fintrak Error');
               }, null);
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#plIncomereportadjustmentTable').length > 0) {
                    var exportTable = $('#plIncomereportadjustmentTable').DataTable({
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

        vm.loadPLAdjustments = function (initialized) {

            vm.viewModelHelper.apiGet('api/plincomereportadjustment/getplincomereportadjustments/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
                  function (result) {

                      vm.plIncomeReportAdjustment = result.data;

                      if (vm.init) {
                          InitialView();
                          vm.init === true;
                      }


                  },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }


        vm.loadPLAdjustmentsbycode = function (initialized) {

            vm.viewModelHelper.apiGet('api/plincomereportadjustment/getplIncomeReportAdjustment/' + vm.Code, null,
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
            vm.viewModelHelper.apiGet('api/plincomereportadjustment/getCodebyUser', null,
                 function (result) {
                     vm.codebyUser = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }


        vm.deletemPLadjustment = function () {
            var params = { Code: vm.Code, userName: vm.userName };
            vm.viewModelHelper.apiPost('api/plincomereportadjustment/deleteplIncomeReportAdjustmentbycode', params,
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


///**
// * Created by Deb on 8/20/2014.
// */
//(function () {
//    "use strict";
//    angular
//        .module("fintrak")
//        .controller("PLIncomeReportAdjustmentListController",
//                    ['$scope', '$state', 'viewModelHelper', 'validator',
//                        PLIncomeReportAdjustmentListController]);

//    function PLIncomeReportAdjustmentListController($scope, $state, viewModelHelper, validator) {
//        var vm = this;
//        vm.viewModelHelper = viewModelHelper;
//        vm.parentController = $scope.$parent;

//        vm.module = 'MPR PL';
//        vm.view = 'plincomereportadjustment-list-view';
//        vm.viewName = 'PL Income Report Adjustment';

//        vm.viewModelHelper.modelIsValid = true;
//        vm.viewModelHelper.modelErrors = [];

//        vm.plIncomeReportAdjustment = [];

//        vm.init = false;
//        vm.showInstruction = false;
//        vm.instruction = '';

//        vm.csv = {
//            uploadCode: 'MPR010',
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
//            if (vm.adjustmentType === 1)
//                vm.csv.uploadCode = 'MPR010';
//            else
//                vm.csv.uploadCode = 'MPR011';

//            UploadService.runUpload(vm.csv);
//        }

//        var onUploadCompleted = function () {
//            toastr.success('Data upload completed.', 'Fintrak');
//            vm.load();
//        }


//        var initialize = function () {

//            if (vm.init === false) {
//                $rootScope.$on('uploadCompleted', onUploadCompleted);
//                vm.viewModelHelper.apiGet('api/plincomereportadjustment/availableplIncomeReportAdjustment', null,
//                   function (result) {
//                       vm.mprBalanceSheetAdjustments = result.data;
//                       InitialView();
//                       vm.init === true;
//                       
//                   },
//                 function (result) {
//                     toastr.error(result.data, 'Fintrak');
//                 }, null);
//            }
//        }

//    }

//    var InitialView = function () {
//        InitialGrid();
//    }

//    var InitialGrid = function () {
//        setTimeout(function () {

//            // data export
//            if ($('#glExceptionTable').length > 0) {
//                var exportTable = $('#glExceptionTable').DataTable({
//                    "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
//                    sDom: "T<'clearfix'>" +
//                        "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
//                        "t" +
//                        "<'row'<'col-sm-6'i><'col-sm-6'p>>",
//                    "tableTools": {
//                        "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
//                    }
//                });
//            }
//        }, 50);
//    }

//    initialize(); 
//}
//}());
