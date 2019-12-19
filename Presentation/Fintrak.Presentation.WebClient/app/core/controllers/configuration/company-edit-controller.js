/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CompanyEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CompanyEditController]);

    function CompanyEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'company-edit-view';
        vm.viewName = 'Company';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.company = {};
        vm.branches = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.showChildren = false;

        var companyRules = [];

        var setupRules = function () {
          
            companyRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            companyRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            companyRules.push(new validator.PropertyRule("Email", {
                required: { message: "Email is required" }
            }));

           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initialLookUp();

                if ($stateParams.companyId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/company/getcompanywithchildrens/' + $stateParams.companyId, null,
                   function (result) {
                       vm.company = result.data.Company;
                       vm.init === true;

                       vm.viewModelHelper.apiGet('api/branch/getbranchebycompany/' + $stateParams.companyId, null,
                       function (result) {
                           vm.branches = result.data;

                           initialView();
                       },
                       function (result) {
                           toastr.error(result.data, 'Fintrak');
                       }, null);
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.company = { Code:'', Name: '', Email: '', Active: true };
            }
        }

        var initialLookUp = function () {
            
        }

        var initialView = function () {
  
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.company, companyRules);
            vm.viewModelHelper.modelIsValid = vm.company.isValid;
            vm.viewModelHelper.modelErrors = vm.company.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/company/updatecompany', vm.company,
               function (result) {
                   
                   $state.go('core-general-edit');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.company.errors;

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
                vm.viewModelHelper.apiPost('api/company/deletecompany', vm.company.CompanyId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-general-edit');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-general-edit');
        };

        setupRules();
        initialize(); 
    }
}());
