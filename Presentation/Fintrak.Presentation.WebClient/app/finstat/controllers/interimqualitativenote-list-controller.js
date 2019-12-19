/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("InterimQualitativeNoteListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        InterimQualitativeNoteListController]);

    function InterimQualitativeNoteListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'qualitativenote-list-view';
        vm.viewName = 'Interim Qualitative Note';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.interimqualitativeNotes = [];
        var exportTable;
        var tabID = 'interimqualitativeNoteTable';
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/interimqualitativeNote/availableinterimqualitativenotes', null,
                   function (result) {
                       vm.interimqualitativeNotes = result.data;

                       InitialView('interimqualitativeNoteTable');

                       if (vm.init === true) {
                           exportTable.destroy();
                       } else vm.init = true;
                       
                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        vm.flipFlagToDescription = function (flag) {

            if (flag === 1) {
                return 'Conventional';
            } else if (flag === 2) {
                return 'Interim';
            } else if (flag === 3) {
                return 'NIB';
            }
        };

        var InitialView = function (tableID) {
            InitialGrid(tableID);
        };

        var InitialGrid = function (tableID) {
            tabID = '#' + tableID;
            setTimeout(function () {
                // data export
                if ($(tabID).length > 0) {
                    exportTable = $(tabID).DataTable({
                        "lengthMenu": [[10, 100, -1], [10, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            },
                50);
        };

        initialize(); 
    }
}());
