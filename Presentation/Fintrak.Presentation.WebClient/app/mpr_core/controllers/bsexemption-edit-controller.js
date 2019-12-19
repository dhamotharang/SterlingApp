/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BSExemptionEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        BSExemptionEditController]);

    function BSExemptionEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'bsexemption-edit-view';
        vm.viewName = 'Classification';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.bsExemption = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var bsexemptionRules = [];

        var setupRules = function () {

            bsexemptionRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "Account No is required" }
            }));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.bsexemptionId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/bsexemption/getbsexemption/' + $stateParams.bsexemptionId, null,
                   function (result) {
                       vm.bsExemption = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.bsExemption = { AccountNo:'', Active: true };
            }
        }

        //var intializeLookUp = function () {
        //    getTeams(1);
        //}

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.bsExemption, bsexemptionRules);
            vm.viewModelHelper.modelIsValid = vm.bsExemption.isValid;
            vm.viewModelHelper.modelErrors = vm.bsExemption.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/bsexemption/updatebsExemption', vm.bsExemption,
               function (result) {
                   
                   $state.go('mpr-bsexemption-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.bsExemption.errors;

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
                vm.viewModelHelper.apiPost('api/bsexemption/deletebsexemption', vm.bsExemption.BSExemptionId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-bsexemption-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-bsexemption-list');
        };

       

        setupRules();
        initialize(); 
    }
}());
