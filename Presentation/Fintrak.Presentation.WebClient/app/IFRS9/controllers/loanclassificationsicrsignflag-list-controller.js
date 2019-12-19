/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanClassificationSICRSignFlagListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        LoanClassificationSICRSignFlagListController]);

    function LoanClassificationSICRSignFlagListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'loanclassificationsicrsignflag-list-view';
        vm.viewName = 'Classification SICR Significant Flag';
        var exportTable;
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];      
        vm.loanclassificationsicrsignflag = [];
        vm.defaultcount = 1000;
        vm.instruction = 'Only top ' + vm.defaultcount + ' records loaded. Use the main search fuctionality to find a specific record by Reference No or Account No';

        vm.init = false;
        vm.showInstruction = true;
        

        var tabID = 'classSICRSignFlagTable';
        
        var initialize = function () {
            intializeLookUp();
            if (vm.init === false) {
                vm.default();
            }
        };

        var intializeLookUp = function () {
            getGroupedClassification()

        }

        var InitialView = function (tabID) {
            InitialGrid(tabID);
        };

        var InitialGrid = function (tabID) {
            tabID = '#' + tabID;
            setTimeout(function () {
                // data export
                if ($(tabID).length > 0) {
                    exportTable = $(tabID).DataTable({
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
            },
                50);
        };

        var InitialView = function (tableID) {
            InitialGrid(tableID);
        }

        vm.default = function () {
            vm.viewModelHelper.apiGet('api/loanclassificationSignFlag/availableLoanClassificationSignFlag/' + vm.defaultcount ,
                null,
                function (result) {
                    vm.loanclassificationsicrsignflag = result.data;
                    InitialView('classSICRSignFlagTable');
                    vm.searchParam = '';
                    if (vm.init === true) {
                        exportTable.destroy();
                    } else vm.init = true;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

        vm.refresh = function () {
            vm.init = false;
            vm.serachParam = '';
           initialize();
           exportTable.destroy();
        }
        var getGroupedClassification = function () {
            vm.viewModelHelper.apiGet('api/loanclassificationSignFlag/getgroupedclassification',
                null,
                function (result) {
                    vm.grpClass = result.data;          
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

        vm.load = function () {
            getgrpClassbyId(vm.Value);
          
        };

        var getgrpClassbyId = function () {
            vm.viewModelHelper.apiGet('api/loanclassificationSignFlag/getLoanClassSICRFlagByLoanClassId/' + vm.Value,
                null,
                function (result) {
                    vm.loanclassificationsicrsignflag = result.data;
                    InitialView('classSICRSignFlagTable');
                    if (vm.init === true) {
                       exportTable.destroy();
                    } else vm.init = true;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };


        initialize();
    }
}());
