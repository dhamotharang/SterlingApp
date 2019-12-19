/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RoleListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator', 'roleSearchService',
                        RoleListController]);

    function RoleListController($scope,$state, viewModelHelper, validator,roleSearchService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'role-list-view';
        vm.viewName = 'Roles';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.roles = [];

        vm.roleType = 1;

        vm.roleTypes = [
           { Id: 1, Name: 'Application' },
            { Id: 2, Name: 'Report' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){
            vm.roleType = roleSearchService.getSearchModel().roleType;
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/role/getrolebytype/' + vm.roleType, null,
                   function (result) {
                       vm.roles = result.data;
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
                if ($('#roleTable').length > 0) {
                    var exportTable = $('#roleTable').DataTable({
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

        vm.load = function () {
            roleSearchService.setSearchModel({ roleType: vm.roleType })
            vm.viewModelHelper.apiGet('api/role/getrolebytype/' + vm.roleType, null,
                  function (result) {
                      vm.roles = result.data;
                      //
                  },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        initialize(); 
    }
}());
