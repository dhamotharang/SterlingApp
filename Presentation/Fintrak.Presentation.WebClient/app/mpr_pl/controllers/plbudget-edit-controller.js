/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("PLBudgetEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        PLBudgetEditController]);

    function PLBudgetEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'plbudget-edit-view';
        vm.viewName = 'PL Budget';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.plBudget = {};

        vm.nonProducts = [];
        vm.runDate = {};
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var plbudgetRules = [];

        var setupRules = function () {
          
           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.plbudgetId !== 0  ) {
                    vm.showChildren = true;

                    var url = '';
                    if ($stateParams.budgetType === 2)
                        url = 'api/revenuebudgetofficer/getrevenuebudgetofficer';
                    else
                        url = 'api/revenuebudget/getrevenuebudget';

                    vm.viewModelHelper.apiGet(url + '/' + $stateParams.budgetId, null,
                   function (result) {
                       vm.plBudget = result.data;
                       
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.plBudget = { CaptionName: '', MisCode: '', Year: '', CompanyCode: '', Mth1: 0, Mth2:0, Mth3:0, Mth4:0, Mth5:0, Mth6:0, Mth7:0, Mth8:0, Mth9:0, Mth10:0, Mth11:0, Mth12:0, Active: true };
            }
        }

        var intializeLookUp = function () {
          
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            var url = '';
            if ($stateParams.budgetType === 2)
                url = 'api/revenuebudgetofficer/updaterevenuebudgetofficer';
            else
                url = 'api/revenuebudget/updaterevenuebudget';

            //Validate
            validator.ValidateModel(vm.plBudget, plbudgetRules);
            vm.viewModelHelper.modelIsValid = vm.plBudget.isValid;
            vm.viewModelHelper.modelErrors = vm.plBudget.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost(url, vm.plBudget,
               function (result) {
                   
                   $state.go('mpr-plbudget-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.plBudget.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
                toastr.error(errorList, 'Fintrak');

            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );
            var url = '';
            if ($stateParams.budgetType === 2)
                url = 'api/revenuebudgetofficer/deleterevenuebudgetofficer';
            else
                url = 'api/revenuebudget/deleterevenuebudget';

            if (deleteFlag) {
                vm.viewModelHelper.apiPost(url, vm.plBudget.BudgetId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-plbudget-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-plbudget-list');
        };

        setupRules();
        initialize(); 
    }
}());
