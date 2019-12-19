
/**Currency
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CompanyModuleEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CompanyModuleEditController]);

    function CompanyModuleEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'companymodule-edit-view';
        vm.viewName = 'Company Module';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.companyModule = {};

        vm.companies = [];
        vm.modules = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.showChildren = false;

        var companyModuleRules = [];

        var setupRules = function () {
          
            companyModuleRules.push(new validator.PropertyRule("CompanyCode", {
                required: { message: "Company Code is required" }
            }));

            companyModuleRules.push(new validator.PropertyRule("ModuleName", {
                required: { message: "Module is required" }
            }));

           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.companymoduleId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/companymodule/getcompanymodule/' + $stateParams.companymoduleId, null,
                   function (result) {
                       vm.companyModule = result.data;
                        initialView();
                       vm.init = true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.companyModule = { CompanyCode: '', ModuleName: '', Active: true };
            }
        }

        var initialView = function () {
            InitialGrid();
            intializeLookUp();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#companymoduleRateTable').length > 0) {
                    var exportTable = $('#companymoduleRateTable').DataTable({
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

        var intializeLookUp = function () {
            getCompanies();
            getModules();
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.companyModule, companyModuleRules);
            vm.viewModelHelper.modelIsValid = vm.companyModule.isValid;
            vm.viewModelHelper.modelErrors = vm.companyModule.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/companymodule/updatecompanymodule', vm.companyModule,
               function (result) {
                   
                   $state.go('core-companymodule-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.companyModule.errors;

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
                vm.viewModelHelper.apiPost('api/companymodule/deletecompanymodule', vm.companyModule.CompanyModuleId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-companymodule-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-companymodule-list');
        };

        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                 function (result) {
                     vm.companies = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getModules = function () {
            vm.viewModelHelper.apiGet('api/module/availablemodules', null,
                 function (result) {
                     vm.modules = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
