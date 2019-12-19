/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OperationEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OperationEditController]);

    function OperationEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'operation-edit-view';
        vm.viewName = 'Operation';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.operation = {};
        vm.operationReviews = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.showPeriod = false;

        var operationRules = [];

        vm.statuses = [
           { Code: 1, Name: 'Draft' },
            { Code: 2, Name: 'Open' },
             { Code: 3, Name: 'Close' }
        ];

        var setupRules = function () {

            operationRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            operationRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
              
                if ($stateParams.operationId !== 0) {
                    vm.showPeriod = true;
                    vm.viewModelHelper.apiGet('api/operation/getoperationdetail/' + $stateParams.operationId, null,
                   function (result) {
                       vm.operation = result.data.Operation;
                       vm.operationReviews = result.data.OperationReview;
                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.operation = { Code:'', Name: '', Status: 1, Active: true };
            }
        }

        var initialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#operationReviewTable').length > 0) {
                    var exportTable = $('#operationReviewTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            }, 50);
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.operation, operationRules);
            vm.viewModelHelper.modelIsValid = vm.operation.isValid;
            vm.viewModelHelper.modelErrors = vm.operation.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/operation/updateoperation', vm.operation,
               function (result) {
                   
                   $state.go('budget-operation-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.operation.errors;

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
                vm.viewModelHelper.apiPost('api/operation/deleteoperation', vm.operation.OperationId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-operation-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('budget-operation-list');
        };
       
        setupRules();
        initialize(); 
    }
}());
