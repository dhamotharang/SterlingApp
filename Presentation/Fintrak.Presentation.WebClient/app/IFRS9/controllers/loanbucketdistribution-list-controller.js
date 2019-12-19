/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanBucketDistributionListController",
                    ['$scope', '$state','$window', 'viewModelHelper', 'validator',
                        LoanBucketDistributionListController]);

    function LoanBucketDistributionListController($scope,$state,$window, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'loanbucketdistribution-list-view';
        vm.viewName = 'Loan Bucket Distribution';
        var stt;
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        //vm. pdtblstatus = true;
        //vm.lgdtblstatus = false;
        vm.loanBucketDistribution = [];
       
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/loanbucketdistribution/availabledistribution', null,
                   function (result) {
                       vm.loanBucketDistribution = result.data;
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
          //  InitialGrid2();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#loaBucketDistributionTable').length > 0) {
                    var exportTable = $('#loaBucketDistributionTable').DataTable({
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

        vm.pddistribution = function () {
            var regressFlag = $window.confirm(' Are you sure you want to Distribute Loan Using PD');
            if (regressFlag) {

                vm.viewModelHelper.apiPost('api/loanbucketdistribution/pddistribution', null,
                          function (result) {
                              toastr.success('Loan Distribution by Past Due Successfully Done.', 'Fintrak');
                              refreshTable();
                              alert('Loan Distribution by Past Due Successfully Done');
                          },
                         function (result) {
                             toastr.error(result.data, 'Fintrak Error');
                         }, null);
            }
        }

        vm.pastduedistribution = function () {
            var regressFlag = $window.confirm(' Are you sure you want to Distribute Loan Using Past Due Days');
            if (regressFlag) {

                vm.viewModelHelper.apiPost('api/loanbucketdistribution/pastduedaysdistribution', null,
                          function (result) {
                              toastr.success('Loan Distribution by Past Due Successfully Done.', 'Fintrak');
                              refreshTable();
                              alert('Loan Distribution by Past Due Successfully Done');
                          },
                         function (result) {
                             toastr.error(result.data, 'Fintrak Error');
                         }, null);
            }
        }

        var refreshTable = function () {
            vm.viewModelHelper.apiGet('api/loanbucketdistribution/availabledistribution', null,
               function (result) {
                   vm.loanBucketDistribution = result.data;
                   //InitialView();
                   //vm.init === true;


               },
             function (result) {
                 toastr.error(result.data, 'Fintrak');
             }, null);

        }
    
        initialize(); 
    }
}());
