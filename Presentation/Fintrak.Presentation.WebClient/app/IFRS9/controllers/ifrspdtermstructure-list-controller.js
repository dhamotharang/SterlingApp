/**
 * Created by Tosin on 8/12/2019.
 */
(function () {
  "use strict";
  angular
    .module("fintrak")
    .controller("IfrsPdTermStructureListController",
      ['$scope', '$state', 'viewModelHelper', 'validator',
        IfrsPdTermStructureListController]);

  function IfrsPdTermStructureListController($scope, $state, viewModelHelper, validator) {
    var vm = this;
    vm.viewModelHelper = viewModelHelper;
    vm.parentController = $scope.$parent;

    vm.module = 'IFRS9';
    vm.view = 'IfrsPdTermStructure-list-view';
    vm.viewName = 'S&P PD Term Structure';

    vm.viewModelHelper.modelIsValid = true;
    vm.viewModelHelper.modelErrors = [];

    vm.IfrsPdTermStructure = [];

    vm.init = false;
    vm.showInstruction = true;
    //vm.instruction = 'Create and maintain any parameter or constant you wish to use for backend processes';

    var initialize = function () {

      if (vm.init === false) {
          vm.viewModelHelper.apiGet('api/IfrsPdTermStructure/getIfrsPdTermStructures', null,
          function (result) {
            vm.IfrsPdTermStructure = result.data;
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
        if ($('#IfrsPdTermStructureTable').length > 0) {
          var exportTable = $('#IfrsPdTermStructureTable').DataTable({
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




    vm.loadIfrsPdTermStructure = function () {
      vm.viewModelHelper.apiGet('api/IfrsPdTermStructure/getIfrsPdTermStructures', null,
        function (result) {
          vm.IfrsPdTermStructure = result.data;
          InitialView('IfrsPdTermStructureTable');

          if (vm.init === true) {
            exportTable.destroy();
          }
          else vm.init = true
        },
        function (result) {
          toastr.error(result.data, 'Fintrak');
        }, null);
    }






    ///

    initialize();
  }
}());
