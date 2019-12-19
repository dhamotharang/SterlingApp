/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanInterestRateListController",
                    ['$scope', '$state','$window', 'viewModelHelper', 'validator',
                        LoanInterestRateListController]);

    function LoanInterestRateListController($scope,$state,$window, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS';
        vm.view = 'loaninterestrate-list-view';
        vm.viewName = 'Floating Loan Interest Rates';
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.loanInterestRate = [];
        vm.refNo = ''
       
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/loaninterestrate/availableloaninterestrates', null,
                   function (result) {
                       vm.loanInterestRate = result.data;
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
                if ($('#loanInterestRateTable').length > 0) {
                    var exportTable = $('#loanInterestRateTable').DataTable({
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


        //vm.loadLoanInterestRate = function () {
        //    vm.monthlydiscountfactor = [];

        //    if (vm.refNo === "") {
        //        alert("Search cannot be empty")
        //        return
        //    };

        //    vm.viewModelHelper.apiGet('api/monthlydiscountfactor/getmonthlydiscountfactorbyrefno/' + vm.refNo, null,
        //                       function (result) {
                                 
        //                           vm.monthlydiscountfactor = result.data;
        //                       },
        //                               function (result) {
        //                                   toastr.error(result, 'Fintrak Error');
        //                               }, null);
        //}


        //vm.refreshLoanInterestRate = function () {
        //    initialize();
        //};


        initialize(); 
    }
}());
