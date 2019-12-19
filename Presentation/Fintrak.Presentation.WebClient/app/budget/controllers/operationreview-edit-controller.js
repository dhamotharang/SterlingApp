/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OperationReviewEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OperationReviewEditController]);

    function OperationReviewEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'operationreview-edit-view';
        vm.viewName = 'Operation Review';

        vm.operationReview = {};

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.statuses = [
            { Code: 1, Name: 'Draft' },
             { Code: 2, Name: 'Open' },
              { Code: 3, Name: 'Close' }
        ];

        var operationReviewRules = [];

        var setupRules = function () {
            operationReviewRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            operationReviewRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            operationReviewRules.push(new validator.PropertyRule("OperationCode", {
                required: { message: "Operation is required" }
            }));
        }

        var initialize = function () {
            getOperations();
            
            if (vm.init === false) {
                if ($stateParams.operationreviewId !== 0) {
                    vm.viewModelHelper.apiGet('api/operationreview/getoperationreview/' + $stateParams.operationreviewId, null,
                   function (result) {
                       vm.operationReview = result.data;
                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.operationReview = {Code:'', Name: '', OperationCode: $stateParams.operationcode, Status: 1, Active: true };
            }
        }

        var initialView = function () {
         
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.operationReview, operationReviewRules);
            vm.viewModelHelper.modelIsValid = vm.operationReview.isValid;
            vm.viewModelHelper.modelErrors = vm.operationReview.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/operationreview/updateoperationreview', vm.operationReview,
               function (result) {
                   
                   $state.go('budget-operation-edit', { operationId: $stateParams.operationId });
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.operationReview.errors;

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
                vm.viewModelHelper.apiPost('api/operationreview/deleteoperationreview', vm.operationReview.OperationReviewId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('budget-operation-edit', { operationId: $stateParams.operationId });
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('budget-operation-edit', { operationId: $stateParams.operationId });
        };

        var getOperations = function () {
            vm.viewModelHelper.apiGet('api/operation/availableoperations', null,
                 function (result) {
                     vm.operations = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
