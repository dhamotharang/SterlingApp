/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanSetupListController",
                    ['$scope', '$window', '$state', 'viewModelHelper', 'validator',
                        LoanSetupListController]);
    //SetupListController
    function LoanSetupListController($scope, $window, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'loansetup-list-view';
        vm.viewName = 'Loan Setups';
        // selected fruits
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.selectedId = '';
        $scope.selection = [];

        vm.loanSetups = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/loansetup/availableloansetups', null,
                   function (result) {
                       vm.loanSetups = result.data;
                       InitialView();
                       vm.init === true;
                       toastr.success('Main data loaded successfully.', 'Fintrak');
                   },
                 function (result) {
                     toastr.error('Fail to load main data.', 'Fintrak');
                 }, null);
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#loanSetupTable').length > 0) {
                    var exportTable = $('#loanSetupTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "../app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            }, 50);
        }
        // toggle selection for a given selected row by id
        $scope.toggleSelection = function toggleSelection(LoanSetupId) {
            var idx = $scope.selection.indexOf(LoanSetupId);
            

            // is currently selected
            if (idx > -1) {
                $scope.selection.splice(idx, 1);
                vm.selectedId = $scope.selection.join(', ');
              //  alert(vm.selectedId)
            }

                // is newly selected
            else {
                $scope.selection.push(LoanSetupId);
                vm.selectedId = $scope.selection.join(', ');
              //  alert(vm.selectedId)
            }
        };

        vm.delete = function () {
           
            var deleteFlag = $window.confirm(' Are you sure you want to delete selected Row(s)');
            if (deleteFlag) {
                var url = '';
                url = 'api/loansetup/deleteloansetuplist/' + vm.selectedId,
                vm.viewModelHelper.apiPost(url,null,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrsloan-loansetup-list');
                  vm.refreshPage();
              },
              function (result) {
                  toastr.error('Fail to delete item.', 'Fintrak');
              }, null);
            }
        }
       
        vm.refreshPage = function () {
            vm.viewModelHelper.apiGet('api/loansetup/availableloansetups', null,
                function (result) {
                    vm.loanSetups = result.data;
                },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
        }
        
        initialize();
    }
}());
