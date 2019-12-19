/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TransferPriceListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        TransferPriceListController]);

    function TransferPriceListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'transferprice-list-view';
        vm.viewName = 'Transfer Prices';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.transferPrices = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/transferPrice/availabletransferPrices', null,
                   function (result) {
                       vm.transferPrices = result.data;
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
                // Init
                //var spinner = $(".spinner").spinner();
                var table = $('#transferPriceTable').dataTable({
                    "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]]
                });

                var tableTools = new $.fn.dataTable.TableTools(table, {
                    "sSwfPath": "../app/vendors/DataTables/extensions/TableTools/swf/copy_csv_xls_pdf.swf",
                    "buttons": [
                        "copy",
                        "csv",
                        "xls",
                        "pdf",
                        { "type": "print", "buttonText": "Print me!" }
                    ]
                });
                $(".DTTT_container").css("float", "right");

                if ($('#demo-checkbox-radio').length <= 0) {
                    $('input[type="checkbox"]:not(".switch")').iCheck({
                        checkboxClass: 'icheckbox_minimal-grey',
                        increaseArea: '20%' // optional
                    });

                    $('input[type="radio"]:not(".switch")').iCheck({
                        radioClass: 'iradio_minimal-grey',
                        increaseArea: '20%' // optional
                    });
                }

                //BEGIN CHECKBOX TABLE
                $('.checkall').on('ifChecked ifUnchecked', function (event) {
                    if (event.type == 'ifChecked') {
                        $(this).closest('table').find('input[type=checkbox]').iCheck('check');
                    } else {
                        $(this).closest('table').find('input[type=checkbox]').iCheck('uncheck');
                    }
                });
                //END CHECKBOX TABLE
            }, 50);
        }

        initialize(); 
    }
}());
