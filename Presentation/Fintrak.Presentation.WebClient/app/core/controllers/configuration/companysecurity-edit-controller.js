/**Currency
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CompanySecurityEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CompanySecurityEditController]);

    function CompanySecurityEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'companysecurity-edit-view';
        vm.viewName = 'Company Security';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.companysecurity = {};
        vm.companies = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.showChildren = false;

        var companysecurityRules = [];

        var setupRules = function () {
          
            companysecurityRules.push(new validator.PropertyRule("CompanyCode", {
                required: { message: "Company Code is required" }
            }));

            companysecurityRules.push(new validator.PropertyRule("Root", {
                required: { message: "Root is required" }
            }));

            companysecurityRules.push(new validator.PropertyRule("Filter", {
                required: { message: "Filter is required" }
            }));

            companysecurityRules.push(new validator.PropertyRule("Attributes", {
                required: { message: "Attributes is required" }
            }));

            companysecurityRules.push(new validator.PropertyRule("Scope", {
                required: { message: "Scope is required" }
            }));

            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.companysecurityId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/companysecurity/getcompanysecurity/' + $stateParams.companysecurityId, null,
                   function (result) {
                       vm.companysecurity = result.data;
                        initialView();
                       vm.init = true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.companysecurity = { CompanyCode: '', Root: '', Filter: '', Attributes: '', Scope: '', Active: true };
            }
        }

        var initialView = function () {
            InitialGrid();
            intializeLookUp();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#companysecurityRateTable').length > 0) {
                    var exportTable = $('#companysecurityRateTable').DataTable({
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
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.companysecurity, companysecurityRules);
            vm.viewModelHelper.modelIsValid = vm.companysecurity.isValid;
            vm.viewModelHelper.modelErrors = vm.companysecurity.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/companysecurity/updatecompanysecurity', vm.companysecurity,
               function (result) {
                   
                   $state.go('core-companysecurity-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.companysecurity.errors;

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
                vm.viewModelHelper.apiPost('api/companysecurity/deletecompanysecurity', vm.companysecurity.CompanySecurityId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-companysecurity-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-companysecurity-list');
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

        setupRules();
        initialize(); 
    }
}());
