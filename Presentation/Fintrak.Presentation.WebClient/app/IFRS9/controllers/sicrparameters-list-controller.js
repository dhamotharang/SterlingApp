/**
 * Created by Deb on 8/20/2014.
 */
(function () {
  "use strict";
  angular
    .module("fintrak")
    .controller("SICRParametersListController",
      ['$scope', '$state', 'viewModelHelper', 'validator',
        SICRParametersListController]);

  function SICRParametersListController($scope, $state, viewModelHelper, validator) {
    var vm = this;
    vm.viewModelHelper = viewModelHelper;
    vm.parentController = $scope.$parent;

    vm.module = 'IFRS9';
    vm.view = 'sicrparameters-list-view';
    vm.viewName = 'Significant Increase in Credit Risk Parameters/Triggers';

    vm.viewModelHelper.modelIsValid = true;
    vm.viewModelHelper.modelErrors = [];

    vm.sicrparameters = [];

    vm.init = false;
    vm.showInstruction = true;
    vm.instruction = 'Create and maintain all bank parameter or trigger that drive SICR';

    var initialize = function () {

      if (vm.init === false) {
          vm.viewModelHelper.apiGet('api/sicrparameters/getsicrparameters', null,
          function (result) {
           vm.sicrparameters = result.data;
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
          if ($('#sicrTable').length > 0) {
              var exportTable = $('#sicrTable').DataTable({
            "lengthMenu": [[10, 50, 50, 100, -1], [10, 50, 50, 100, "All"]],
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

    vm.loadSICRParameters = function () {
      vm.viewModelHelper.apiGet('api/sicrparameters/getsicrparameters', null,
        function (result) {
          vm.sicrparameters = result.data;
          InitialView('sicrTable');

          if (vm.init === true) {
            exportTable.destroy();
          }
          else vm.init = true
        },
        function (result) {
          toastr.error(result.data, 'Fintrak');
        }, null);
    }     

    initialize();
  }
}());
