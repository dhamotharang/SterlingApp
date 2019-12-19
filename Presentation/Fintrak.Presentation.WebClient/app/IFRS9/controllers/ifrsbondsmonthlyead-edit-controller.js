/**
 * Created by Tosin on 8/12/2019.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsMonthlyEADController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IfrsMonthlyEADController]);

    function IfrsMonthlyEADController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrsmonthlyead-edit-view';
        vm.viewName = 'IfrsMonthlyEAD';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifrsmonthlyead = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
       // vm.instruments = [
           // 'Term Loans',
           // 'Corporate Bonds',
           // 'Off Balance-Sheet Item' 
        //];

        var ifrsmonthlyeadRules = [];

        //vm.agencies = [
        //    { Id: 1, Name: 'Fitch' },
        //    { Id: 2, Name: 'Moodys' },
        //    { Id: 3, Name: 'S & P' },
        //];

        //vm.categories = [
        //    { Id: 1, Name: 'Investment Grade' },
        //    { Id: 2, Name: 'Speculative Grade'},
        //];

        /* var setupRules = function () {

            ifrsmonthlyeadRules.push(new validator.PropertyRule("InstrumentType", {
                required: { message: "Instrument type is required" }
            }));

            ifrsmonthlyeadRules.push(new validator.PropertyRule("LGD_BEST", {
                required: { message: "LGD (BEST) is required" }
            }));

            ifrsmonthlyeadRules.push(new validator.PropertyRule("LGD_DOWNTURN", {
                required: { message: "LGD (Downturn) is required" }
            }));

            ifrsmonthlyeadRules.push(new validator.PropertyRule("LGD_OPTIMISTIC", {
                required: { message: "LGD (Optimistic) is required" }
            }));

        } */

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.instrumentId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ifrsmonthlyead/getIfrsLgdScenarioByInstrumentId/' + $stateParams.instrumentId, null,
                   function (result) {
                     vm.ifrsmonthlyead  = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                  vm.ifrsmonthlyead  = { InstrumentType: '', LGD_BEST: 0, LGD_DOWNTURN: 0, LGD_OPTIMISTIC: 0, Active: true };
            }
        }

        var intializeLookUp = function () {

        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
          validator.ValidateModel(vm.ifrsmonthlyead , ifrsmonthlyeadRules);
          vm.viewModelHelper.modelIsValid = vm.ifrsmonthlyead.isValid;
          vm.viewModelHelper.modelErrors = vm.ifrsmonthlyead.errors;
            if (vm.viewModelHelper.modelIsValid) {

              vm.viewModelHelper.apiPost('api/ifrsmonthlyead/updateifrsmonthlyead', vm.ifrsmonthlyead,
               function (result) {

                   $state.go('ifrs9-ifrsmonthlyead-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
              vm.viewModelHelper.modelErrors = vm.ifrsmonthlyead.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
              vm.viewModelHelper.apiPost('api/ifrsmonthlyead/deleteifrsmonthlyead', vm.ifrsmonthlyead.Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-ifrsmonthlyead-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrs9-ifrsmonthlyead-list');
        };

        setupRules();
        initialize();
    }
}());
