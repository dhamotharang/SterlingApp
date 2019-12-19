/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanClassificationSICRSignFlagListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator', LoanClassificationSICRSignFlagListController]);

    function LoanClassificationSICRSignFlagListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LoanClassificationSICRSignFlag_Data';
        vm.view = 'loansignificantflag-list-view';
        vm.viewName = 'Loans Significant Flag';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

 
        vm.loanSignificantflag = [];
        vm.init = false;
        vm.serachParam = '';
        vm.defaultCount = 5000;
        vm.showInstruction = true;
        vm.obeType = [
            { value: 'FBG', name: 'FBG' },
            { value: 'PBG', name: 'PBG' },
            { value: 'Self Liquidating LC', name: 'Self Liquidating LC' }
        ];
      //  vm.instruction = 'Only top ' + vm.defaultCount + ' records loaded. Use the main search fuctionality to find a specific record by RefNo or Account No.';


        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/loanSignificantflag/availableloanSignificantflag/' + vm.defaultCount, null,
                    function (result) {
                        vm.loanSignificantflag = result.data;
                        InitialView();
                        vm.init === true;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        var InitialView = function () {
            InitialGrid();
        };

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#loanSignificantflagTable').length > 0) {
                    var exportTable = $('#loanSignificantflagTable').DataTable({
                        "lengthMenu": [[20, 50, 100, 200, -1], [20, 50, 100, 200, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" + "RC" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        },
                        "aoColumnDefs": [
                            //{ "bVisible": false, "aTargets": [0] }
                        ],
                        "colVis": {
                            buttonText: 'Show / Hide Columns',
                            restore: "Restore",
                            showAll: "Show all"
                        }
                    });
                }
            }, 20);
        };

        vm.loadLoanClassificationSICRSignFlagBySearch = function () {

            if (vm.searchParam === '') {
                toastr.warning('Please input a ProductType ', 'Empty Search')  // or Account No
                return
            }
            else {

                //alert(vm.searchParam);

                vm.viewModelHelper.apiGet('api/loanSignificantflag/getloansignificantflagbysearch/' + vm.searchParam, null,
                    function (result) {
                        vm.loanSignificantflag = result.data;
                        // InitialView();                      
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }

        };   

        vm.refresh = function () {
            vm.init = false;
            vm.serachParam = '';
            vm.viewModelHelper.apiGet('api/loanSignificantflag/availableloansignificantflag/' + vm.defaultCount, null,
                function (result) {
                    vm.loanSignificantflag = result.data;
                    //  InitialView();
                    //  vm.init === true;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };


        initialize();
    }
}());
