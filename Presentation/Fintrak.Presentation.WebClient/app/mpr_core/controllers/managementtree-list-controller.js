/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ManagementTreeListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        ManagementTreeListController]);

    function ManagementTreeListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'managementtree-list-view';
        vm.viewName = ' Management Tree';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.managementTrees = [];
        vm.tree_data = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.viewType = 'list';

        //vm.tree_data = [
        //                                   {
        //                                       Name: "USA", Area: 9826675, Population: 318212000, TimeZone: "UTC -5 to -10",
        //                                       children: [
        //                                         {
        //                                             Name: "California", Area: 423970, Population: 38340000, TimeZone: "Pacific Time",
        //                                             children: [
        //                                                 { Name: "San Francisco", Area: 231, Population: 837442, TimeZone: "PST" },
        //                                                 { Name: "Los Angeles", Area: 503, Population: 3904657, TimeZone: "PST" }
        //                                             ]
        //                                         },
        //                                         {
        //                                             Name: "Illinois", Area: 57914, Population: 12882135, TimeZone: "Central Time Zone",
        //                                             children: [
        //                                                 { Name: "Chicago", Area: 234, Population: 2695598, TimeZone: "CST" }
        //                                             ]
        //                                         }
        //                                       ]
        //                                   },
        //                                { Name: "Texas", Area: 268581, Population: 26448193, TimeZone: "Mountain" }
        //];

        vm.col_defs = [
              { field: "Column1", displayName: 'Team Definition' },
              { field: "Column2", displayName: 'Team Code' },
              { field: "Column3", displayName: 'Officer Definition' },
              { field: "Column4", displayName: 'Officer Code' },
              { field: "Column5", displayName: 'Rate' },
              {
                  //field: "Column5",
                  //displayName: "Action",
                  //cellTemplate: "<a class='btn btn-default' data-ui-sref='mpr-managementtree-edit({managementtreeId: managementtreeId})'><i class='fa fa-align-left'></i></a>",
      cellTemplateScope: {
          click: function(data) {         // this works too: $scope.someMethod;
              alert('You Click');
          }
      }
      }
          ];

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/managementTree/availablemanagementTrees', null,
                   function (result) {
                       vm.managementTrees = result.data;
                       InitialView();
                       vm.init === true;
                       
                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);

                vm.viewModelHelper.apiGet('api/managementTree/getmanagementTrees', null,
                  function (result) {
                      vm.tree_data = result.data;

                      //angular.forEach(results, function (data) {
                      //    var root = { Column1: data.Column1, Column2: data.Column2, Column3: data.Column3, Column4: data.Column4, Column5: data.Column5, children: [] }

                      //    angular.forEach(data.children, function (child) {
                      //        var node = { Column1: child.Column1, Column2: child.Column2, Column3: child.Column3, Column4: child.Column4, Column5: child.Column5, children: [] }

                      //        root.children.push(node);
                      //    });

                      //    vm.tree_data.push(root);

                      //});

                      toastr.success('Main data tree loaded successfully.', 'Fintrak');
                  },
                function (result) {
                    toastr.error('Fail to load main data tree.', 'Fintrak');
                }, null);
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
               
                // data export
                if ($('#managementTreeTable').length > 0) {
                    var exportTable = $('#managementTreeTable').DataTable({
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

        vm.toggleView = function(view){
            vm.viewType = view;
        }

        initialize(); 
    }
}());
