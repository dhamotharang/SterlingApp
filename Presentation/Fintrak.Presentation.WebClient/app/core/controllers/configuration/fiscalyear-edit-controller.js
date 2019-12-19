/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FiscalYearEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        FiscalYearEditController]);

    function FiscalYearEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'fiscalYear-edit-view';
        vm.viewName = 'Fiscal Year';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.fiscalYear = {};
        vm.fiscalPeriod = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.showPeriod = false;

        var fiscalYearRules = [];

        vm.openedStartDate = false;
        vm.openedEndDate = false;

        var setupRules = function () {
          
            fiscalYearRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            fiscalYearRules.push(new validator.PropertyRule("StartDate", {
                mustBeDate: { message: "Please enter a valid date" }
            }));

            fiscalYearRules.push(new validator.PropertyRule("EndDate", {
                mustBeDate: { message: "Please enter a valid date" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
              
                if ($stateParams.fiscalyearId !== 0) {
                    vm.showPeriod = true;
                    vm.viewModelHelper.apiGet('api/fiscalYear/getfiscalyearwithperiods/' + $stateParams.fiscalyearId, null,
                   function (result) {
                       vm.fiscalYear = result.data.FiscalYear;
                       vm.fiscalPeriods = result.data.FiscalPeriods;
                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.fiscalYear = { Name: '', StartDate: new Date(),EndDate: new Date(), Closed: false, Active: true };
            }
        }

        var initialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#fiscalPeriodTable').length > 0) {
                    var exportTable = $('#fiscalPeriodTable').DataTable({
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

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.fiscalYear, fiscalYearRules);
            vm.viewModelHelper.modelIsValid = vm.fiscalYear.isValid;
            vm.viewModelHelper.modelErrors = vm.fiscalYear.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/fiscalYear/updatefiscalYear', vm.fiscalYear,
               function (result) {
                   
                   $state.go('core-fiscalyear-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.fiscalYear.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/fiscalYear/deletefiscalYear', vm.fiscalYear.FiscalYearId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-fiscalyear-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-fiscalyear-list');
        };

        vm.openStartDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedStartDate = true;
        }

        vm.openEndDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedEndDate = true;
        }
       
        setupRules();
        initialize(); 
    }
}());
