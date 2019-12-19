/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CustAccountListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        CustAccountListController]);

    function CustAccountListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'custaccount-list-view';
        vm.viewName = 'Customer Account';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.custAccounts = []

        vm.selectedsearchType = '';
        vm.searchValue = '';
         vm.number = 0;

        vm.searchTypes = [
            { Id: 1, Name: 'CustNo' },
            { Id: 2, Name: 'AccountNo' },
            { Id: 3, Name: 'AccountName' },
            { Id: 4, Name: 'Team' },
            { Id: 5, Name: 'AccountOfficer' }
        ];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {

      
                vm.viewModelHelper.apiGet('api/custaccount/availablecustaccount', null,       
                  //vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType  + '/' + vm.searchValue + '/' + vm.number, null,
                   function (result) {
                       vm.custAccounts = result.data;
                       InitialView();
                       //vm.loadCustomers(true);
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
                if ($('#custAccountTable').length > 0) {
                    var exportTable = $('#custAccountTable').DataTable({
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

        vm.loadCustomers = function (initialized) {

            vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType  + '/' + vm.searchValue + '/' + vm.number, null,
                  function (result) {

                      vm.custAccounts = result.data;
 
                      if (vm.init)
                      {
                          InitialView();
                          vm.init === true;
                      }

                      
                  },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        initialize(); 
    }
}());
