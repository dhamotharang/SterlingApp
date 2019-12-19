/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("StaffCostEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        StaffCostEditController]);

    function StaffCostEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'staffcost-edit-view';
        vm.viewName = 'Staff Cost';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.staffCosts = {};
        vm.branches = [];
        vm.ccentres = [];

       
   

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var staffcostRules = [];

        var setupRules = function () {

            staffcostRules.push(new validator.PropertyRule("EmployeeCode", {
                required: { message: "EmployeeCode is required" }
            }));

            staffcostRules.push(new validator.PropertyRule("EmployeeName", {
                required: { message: "EmployeeName is required" }
            }));

            staffcostRules.push(new validator.PropertyRule("MISCode", {
                required: { message: "MISName is required" }
            }));

            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

                if ($stateParams.staffcostId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/staffcost/getstaffcost/' + $stateParams.staffcostId, null,
                   function (result) {
                       vm.staffCost = result.data;
                       //getCCDefintion();
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.staffCost = { EmployeeCode: '', EmployeeName: '', Level: '', Amount: 0, BranchCode: '', MISCode: '', Active: true };
            }
        }


        var intializeLookUp = function () {          
            getBranch();
            getCCentre();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.staffCost, staffcostRules);
            vm.viewModelHelper.modelIsValid = vm.staffCost.isValid;
            vm.viewModelHelper.modelErrors = vm.staffCost.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/staffcost/updatestaffcost', vm.staffCost,
               function (result) {
                   
                   $state.go('mpr-staffcost-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.staffCost.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }
        // vm.derivedCaption.DerivedCaptionId,
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/staffcost/deletestaffcost', vm.staffCost.StaffCostId,//vm.staffCost.staffcostId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-staffcost-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-staffcost-list');
        };

        var getBranch = function () {
            vm.viewModelHelper.apiGet('api/branch/availablebranches', null,
                 function (result) {
                     vm.branches = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }


        var getCCentre = function () {
            vm.viewModelHelper.apiGet('api/costcentre/availablecostCentres', null,
                 function (result) {
                     vm.ccentres = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load cost centres.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize();
    }
}());
