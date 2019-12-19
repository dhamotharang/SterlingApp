/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BranchEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        BranchEditController]);

    function BranchEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'branch-edit-view';
        vm.viewName = 'Branch';

        vm.branch = {};

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var branchRules = [];

        var setupRules = function () {
           
            branchRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            branchRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            branchRules.push(new validator.PropertyRule("Address", {
                required: { message: "Address is required" }
            }));

            branchRules.push(new validator.PropertyRule("CompanyId", {
                notZero: { message: "Company is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                initializeLookUp();

                if ($stateParams.branchId !== 0) {
                    vm.viewModelHelper.apiGet('api/branch/getbranch/' + $stateParams.branchId, null,
                   function (result) {
                       vm.branch = result.data;
                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.branch = { Code: '', Name: '',Address:'', CompanyId: $stateParams.companyId, Active: true };
            }
        }

        var initializeLookUp = function () {
                                                                              
        }

        var initialView = function () {
         
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.branch, branchRules);
            vm.viewModelHelper.modelIsValid = vm.branch.isValid;
            vm.viewModelHelper.modelErrors = vm.branch.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/branch/updatebranch', vm.branch,
               function (result) {
                   
                   $state.go('core-company-edit', { companyId: $stateParams.companyId });
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.branch.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm('Do');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/branch/deletebranch', vm.branch.BranchId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-company-edit', { companyId: $stateParams.companyId });
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('core-company-edit', { companyId: $stateParams.companyId });
        };

       
        setupRules();
        initialize(); 
    }
}());
