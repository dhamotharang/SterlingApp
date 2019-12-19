/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("AccountMISListController",
                    ['$scope','$window', '$state', 'viewModelHelper', 'validator',
                        AccountMISListController]);

    function AccountMISListController($scope, $window,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'accountmis-list-view';
        vm.viewName = ' Account MIS';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.accountMISs = [];
        vm.selectedId = '';
        $scope.selection = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/accountMIS/availableaccountMISs', null,
                   function (result) {
                       vm.accountMISs = result.data;
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
                if ($('#accountMISTable').length > 0) {
                    var exportTable = $('#accountMISTable').DataTable({
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

        // toggle selection for a given fruit by name
        $scope.toggleSelection = function toggleSelection(AccountMISId) {
            var idx = $scope.selection.indexOf(AccountMISId);


            // is currently selected
            if (idx > -1) {
                $scope.selection.splice(idx, 1);
                vm.selectedId = $scope.selection.join(', ');
                //  alert(vm.selectedId)
            }

                // is newly selected
            else {
                $scope.selection.push(AccountMISId);
                vm.selectedId = $scope.selection.join(', ');
                //  alert(vm.selectedId)
            }
        };

        vm.delete = function () {

            var deleteFlag = $window.confirm(' Are you sure you want to delete selected Row(s)');
            if (deleteFlag) {
                var url = '';
                url = 'api/accountmis/deleteselectedlist/' + vm.selectedId,
                vm.viewModelHelper.apiPost(url, null,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-accountmis-list');
                  vm.refreshPage();
              },
              function (result) {
                  toastr.error('Fail to delete item.', 'Fintrak');
              }, null);
            }
        }

        vm.refreshPage = function () {
            vm.viewModelHelper.apiGet('api/accountMIS/availableaccountMISs', null,
                function (result) {
                    vm.accountMISs = result.data;
                },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
        }

        initialize(); 
    }
}());
