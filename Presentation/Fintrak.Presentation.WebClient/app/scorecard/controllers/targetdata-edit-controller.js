/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SCDTargetDataEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SCDTargetDataEditController]);

    function SCDTargetDataEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Scorecard';
        vm.view = 'targetdata-edit-view';
        vm.viewName = 'Target Data';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.target = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var targetRules = [];

        var setupRules = function () {
          
            targetRules.push(new validator.PropertyRule("Caption", {
                required: { message: "Caption is required" }
            }));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups  targetId
                intializeLookUp();
                if ($stateParams.targetdataId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/target/gettarget/' + $stateParams.targetdataId, null,
                   function (result) {
                       vm.target = result.data;

                       initialView();
                       vm.init = true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.target = { Caption: '', Amount: 0,Date:new Date(), Period:1, Year: '',Active: true };
            }
        }


        var intializeLookUp = function () {
          
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.target, targetRules);
            vm.viewModelHelper.modelIsValid = vm.target.isValid;
            vm.viewModelHelper.modelErrors = vm.target.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/target/updatetarget', vm.target,
               function (result) {
                   
                   $state.go('scd-targetdata-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.target.errors;

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
                vm.viewModelHelper.apiPost('api/target/deletetarget', vm.target.TargetId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('scd-targetdata-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('scd-targetdata-list');
        };

        vm.openDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedDate = true;
        }

        setupRules();
        initialize(); 
    }
}());
