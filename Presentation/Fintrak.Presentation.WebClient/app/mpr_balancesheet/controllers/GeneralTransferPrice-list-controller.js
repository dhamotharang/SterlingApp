/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("GeneralTransferPriceListController",
                    ['$scope', '$window', '$state', 'viewModelHelper', 'validator',
                        GeneralTransferPriceListController]);

    function GeneralTransferPriceListController($scope, $window,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Balancesheet';
        vm.view = 'generaltransferprice-list-view';
        vm.viewName = 'General Transfer Price';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.generalTransferPrices = [];

        vm.selectedId = '';
        $scope.selection = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/generalTransferPrice/generaltransferprice', null,
                   function (result) {
                       vm.generalTransferPrices = result.data;
                       InitialView();
                       vm.init === true;
                       //alert("Ok");
                   },
                   function (result) {
                       alert("Fail");
                   }, null);
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
               
                // data export
                if ($('#generalTransferPriceTable').length > 0) {
                    var exportTable = $('#generalTransferPriceTable').DataTable({
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
        $scope.toggleSelection = function toggleSelection(generaltransferpriceId) {
            var idx = $scope.selection.indexOf(generaltransferpriceId);


            // is currently selected
            if (idx > -1) {
                $scope.selection.splice(idx, 1);
                vm.selectedId = $scope.selection.join(', ');
                //  alert(vm.selectedId)
            }

                // is newly selected
            else {
                $scope.selection.push(generaltransferpriceId);
                vm.selectedId = $scope.selection.join(', ');
                //  alert(vm.selectedId)
            }
        };

        vm.delete = function () {
          
            var deleteFlag = $window.confirm(' Are you sure you want to delete selected Row(s)');
            if (deleteFlag) {
                var url = '';
                url = 'api/generalTransferPrice/deleteselectedlist/' + vm.selectedId,
                vm.viewModelHelper.apiPost(url, null,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-generaltransferprice-list');
                  vm.refreshPage();
              },
              function (result) {
                  toastr.error('Fail to delete item.', 'Fintrak');
              }, null);
            }
        }

        vm.refreshPage = function () {
            vm.viewModelHelper.apiGet('api/generalTransferPrice/generaltransferprice', null,
                function (result) {
                    vm.generalTransferPrices = result.data;
                },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
        }

        initialize(); 
    }
}());
