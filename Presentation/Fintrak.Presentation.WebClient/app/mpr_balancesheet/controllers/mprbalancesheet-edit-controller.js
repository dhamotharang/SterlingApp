/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MPRBalanceSheetEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MPRBalanceSheetEditController]);

    function MPRBalanceSheetEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Balancesheet';
        vm.view = 'mprbalancesheet-edit-view';
        vm.viewName = 'BalanceSheet';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.mprBalanceSheet = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.category = [
         { Id: 2, Name: 'Asset' },
         { Id: 3, Name: 'Liability' }
        ];

        var mprbalancesheetRules = [];

        vm.teams = [];
         vm.accountofficers = [];

        var setupRules = function () {

            mprbalancesheetRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "AccountNo Code is required" }
            }));
            mprbalancesheetRules.push(new validator.PropertyRule("AccountName", {
                required: { message: "AccountName is required" }
            }));
            mprbalancesheetRules.push(new validator.PropertyRule("TeamCode", {
                required: { message: "Team is required" }
            }));
            mprbalancesheetRules.push(new validator.PropertyRule("AccountOfficerCode", {
                required: { message: "Account Officer is required" }
            }));
            mprbalancesheetRules.push(new validator.PropertyRule("ActualBal", {
                required: { message: "Actual Balance is required" }
            }));
            mprbalancesheetRules.push(new validator.PropertyRule("AverageBal", {
                required: { message: "Average Balance is required" }
            }));
        }



        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                 intializeLookUp();

                if ($stateParams.mprbalancesheetId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/mprbalancesheet/getmprbalancesheet/' + $stateParams.balancesheetId, null,
                   function (result) {
                       vm.mprBalanceSheet = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {

                   }, null);
                }
                else
                    vm.mprBalanceSheet = { AccountNo: '', AccountName:'', TeamCode: '', AccountOfficerCode: '', ActualBal: '', AverageBal: '', Active: true };
            }
        }

        var initialView = function () {

        }

        var intializeLookUp = function () {
            getTeams();
            getAccountOfficers();
           
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.mprBalanceSheet, mprbalancesheetRules);
            vm.viewModelHelper.modelIsValid = vm.mprBalanceSheet.isValid;
            vm.viewModelHelper.modelErrors = vm.mprBalanceSheet.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/mprbalancesheet/updatemprbalancesheet', vm.mprBalanceSheet,
               function (result) {
                   //
                   $state.go('mpr-mprbalancesheet-list');
               },
               function (result) {

               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.mprBalanceSheet.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
            }

        }
        // vm.derivedCaption.DerivedCaptionId,
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/mprbalancesheet/deletemprbalancesheet', vm.mprBalanceSheet.BalanceSheetId,//vm.mprBalanceSheet.mprbalancesheetId,
              function (result) {
                  //
                  $state.go('mpr-mprbalancesheet-list');
              },
              function (result) {

              }, null);
            }
        }

        var getTeams = function () {
            vm.viewModelHelper.apiGet('api/team/getteambylevel/2', null,
                 function (result) {
                     vm.teams = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub Team.', 'Fintrak');
                 }, null);
        }

        var getAccountOfficers = function () {
            vm.viewModelHelper.apiGet('api/team/getteambylevel/1', null,
                 function (result) {
                     vm.accountofficers = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load sub AccountOfficer.', 'Fintrak');
                 }, null);
        }

        vm.cancel = function () {
            $state.go('mpr-mprbalancesheet-list');
        };


        //  vm.onAccountOfficerCodeChanged = function (caption) {
        //    vm.mprBalanceSheet.AccountOfficerCode = caption.Name;
        //}


        setupRules();
        initialize();
    }
}());
