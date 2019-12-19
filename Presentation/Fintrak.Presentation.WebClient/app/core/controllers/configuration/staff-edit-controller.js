/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("StaffEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        StaffEditController]);

    function StaffEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'staff-edit-view';
        vm.viewName = 'Staff';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.staff = {};
        vm.companies = [];
        vm.staffRates = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var staffRules = [];

        var setupRules = function () {
          
            staffRules.push(new validator.PropertyRule("StaffCode", {
                required: { message: "StaffCode is required" }
            }));

            staffRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            staffRules.push(new validator.PropertyRule("Email", {
                required: { message: "Email is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.staffId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/staff/getstaff/' + $stateParams.staffId, null,
                   function (result) {
                       vm.staff = result.data;

                       initialView();
                       vm.init = true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.staff = { StaffCode: '', Name: '', Email: '', Phone: '', Active: true };
            }
        }


        var intializeLookUp = function () {
            //getCompanies();
            //getSolutions();
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.staff, staffRules);
            vm.viewModelHelper.modelIsValid = vm.staff.isValid;
            vm.viewModelHelper.modelErrors = vm.staff.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/staff/updatestaff', vm.staff,
               function (result) {
                   
                   $state.go('core-staffs-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.staff.errors;

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
                vm.viewModelHelper.apiPost('api/staff/deletestaff', vm.staff.StaffId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-staffs-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-staffs-list');
        };

        //var getCompanies = function () {
        //    vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
        //         function (result) {
        //             vm.companies = result.data;
        //         },
        //         function (result) {
        //             toastr.error(result.data, 'Fintrak');
        //         }, null);
        //}

        //var getSolutions = function () {
        //    vm.viewModelHelper.apiGet('api/solution/availablesolutions', null,
        //         function (result) {
        //             vm.solutions = result.data;
        //         },
        //         function (result) {
        //             toastr.error(result.data, 'Fintrak');
        //         }, null);
        //}

        setupRules();
        initialize(); 
    }
}());
