/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ProcessDataEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ProcessDataEditController]);

    function ProcessDataEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'processdata-edit-view';
        vm.viewName = 'Process Data';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.processData = {};
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var processDataRules = [];

        var setupRules = function () {

            processDataRules.push(new validator.PropertyRule("MISCode", {
                required: { message: "MISCode is required" }
            }));

            //processDataRules.push(new validator.PropertyRule("AccountOfficerCode", {
            //    required: { message: "Account Officer Code is required" }
            //}));
    
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.processDataId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/processdata/getprocessData/' + $stateParams.processdataId, null,
                   function (result) {
                       vm.processData = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    //vm.processData = { AccountOfficerCode: '', MISCode: '', Active: true };
                    vm.processData = { MISCode: '', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.processData, processDataRules);
            vm.viewModelHelper.modelIsValid = vm.processData.isValid;
            vm.viewModelHelper.modelErrors = vm.processData.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/processdata/updateprocessData', vm.processData,
               function (result) {
                   
                   $state.go('mpr-processdata-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.processData.errors;

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
                vm.viewModelHelper.apiPost('api/processdata/deleteprocessData', vm.processData.ProcessDataId,//vm.processData.processDataId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-processdata-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-processdata-list');
        };

        setupRules();
        initialize();
    }
}());
