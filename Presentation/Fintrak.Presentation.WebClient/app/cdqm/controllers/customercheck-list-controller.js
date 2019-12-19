/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CustomerCheckListController",
                    ['$scope', '$window', '$state', 'viewModelHelper', 'validator', 'customerCheckSearchService',
                        CustomerCheckListController]);

    function CustomerCheckListController($scope, $window, $state, viewModelHelper, validator,customerCheckSearchService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'CDQM';
        vm.view = 'customercheck-list-view';
        vm.viewName = 'Customer Checks';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.customers = [];
       
        vm.selectedGroupId = '';

        vm.group = 1;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){
            if (vm.init === false) {
                initializeLookUp();
                vm.selectedGroupId = customerCheckSearchService.getSearchModel().groupId;
                vm.loadCustomers(true);
            }
            
        }

        var initializeLookUp = function () {
            getGroupIDs();
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#group1Table').length > 0) {
                    var exportTable = $('#group1Table').DataTable({
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

                if ($('#group2Table').length > 0) {
                    var exportTable = $('#group2Table').DataTable({
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

                if ($('#group3Table').length > 0) {
                    var exportTable = $('#group3Table').DataTable({
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

                if ($('#group4Table').length > 0) {
                    var exportTable = $('#group4Table').DataTable({
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

                if ($('#group5Table').length > 0) {
                    var exportTable = $('#group5Table').DataTable({
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
                if ($('#group6Table').length > 0) {
                    var exportTable = $('#group6Table').DataTable({
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
                if ($('#group7Table').length > 0) {
                    var exportTable = $('#group7Table').DataTable({
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
                if ($('#group8Table').length > 0) {
                    var exportTable = $('#group8Table').DataTable({
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
            customerCheckSearchService.setSearchModel({ groupId: vm.selectedGroupId })

            if (vm.selectedGroupId === '' || vm.selectedGroupId === undefined || vm.selectedGroupId === null)
            {
                vm.customers = [];
                return;
            }

            vm.viewModelHelper.apiGet('api/cdqmcustomercheck/getcdqmcustomerpersistent/' + vm.selectedGroupId, null,
                  function (result) {

                      vm.customers = result.data;
 
                      if (initialized)
                      {
                          InitialView();
                          vm.init === true;
                      }
                      
                      
                  },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        var getGroupIDs = function () {
            vm.viewModelHelper.apiGet('api/cdqmcustomercheck/getcustomergroupids', null,
                 function (result) {
                     vm.groupIds = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load customer group ids.', 'Fintrak');
                 }, null);
        }

        vm.onGroupSelected = function (group) {
            vm.group = group;
        }

        vm.goldStatus = function (golden) {
            if (golden)
                return 'Golden';
            else
                return 'Not Golden';
        }

        vm.updateCustomer = function (customer) {
            var confirmFlag = $window.confirm(' Are you sure you want to perform this operation.');

            if (confirmFlag) {
                vm.viewModelHelper.apiPost('api/cdqmcustomercheck/updatecustomer', customer,
                  function (result) {
                      vm.customers = result.data;
                      toastr.success('Customer duplicate data update successfully.', 'Fintrak');
                  },
                  function (result) {
                      toastr.error('Fail to update customer duplicate data.', 'Fintrak');
                  }, null);
            }
        }


        initialize(); 
    }
}());
