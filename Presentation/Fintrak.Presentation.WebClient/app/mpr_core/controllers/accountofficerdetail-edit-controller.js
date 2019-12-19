/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("AccountOfficerDetailEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        AccountOfficerDetailEditController]);

    function AccountOfficerDetailEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'accountofficerdetail-edit-view';
        vm.viewName = 'Classification';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.accountOfficerDetail = {};
        vm.teams = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var accountofficerdetailRules = [];

        var setupRules = function () {

            accountofficerdetailRules.push(new validator.PropertyRule("StaffID", {
                required: { message: "Staff ID is required" }
            }));

            accountofficerdetailRules.push(new validator.PropertyRule("MisCode", {
                required: { message: "Account Officer is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.accountofficerdetailId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/accountofficerdetail/getaccountofficerdetail/' + $stateParams.accountofficerdetailId, null,
                   function (result) {
                       vm.accountOfficerDetail = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.accountOfficerDetail = { AccountOfficerId:0,StaffID:'', Active: true };
            }
        }

        var intializeLookUp = function () {
            getTeams(1);
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.accountOfficerDetail, accountofficerdetailRules);
            vm.viewModelHelper.modelIsValid = vm.accountOfficerDetail.isValid;
            vm.viewModelHelper.modelErrors = vm.accountOfficerDetail.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/accountofficerdetail/updateaccountofficerdetail', vm.accountOfficerDetail,
               function (result) {
                   
                   $state.go('mpr-accountofficerdetail-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.accountOfficerDetail.errors;

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
                vm.viewModelHelper.apiPost('api/accountofficerdetail/deleteaccountofficerdetail', vm.accountOfficerDetail.AccountOfficerDetailId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-accountofficerdetail-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-accountofficerdetail-list');
        };

        var getTeams = function (level) {
            vm.viewModelHelper.apiGet('api/team/getteambylevel/' + level, null,
                 function (result) {
                     vm.teams = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
