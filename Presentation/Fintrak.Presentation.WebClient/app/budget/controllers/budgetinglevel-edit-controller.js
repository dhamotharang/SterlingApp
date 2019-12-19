/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BudgetingLevelEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        BudgetingLevelEditController]);

    function BudgetingLevelEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'budgetinglevel-edit-view';
        vm.viewName = 'Budgeting Level';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.budgetingLevel = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
       
        var budgetingLevelRules = [];

        vm.centres = [
           { Code: 1, Name: 'Cost Centre' },
            { Code: 2, Name: 'Profit Centre' }
        ];

        var setupRules = function () {

            budgetingLevelRules.push(new validator.PropertyRule("ModuleCode", {
                required: { message: "Module is required" }
            }));

            budgetingLevelRules.push(new validator.PropertyRule("DefinitionCode", {
                required: { message: "Definition is required" }
            }));

            budgetingLevelRules.push(new validator.PropertyRule("Center", {
                required: { message: "Center is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initializeLookUp();

                if ($stateParams.budgetingLevelId !== 0) {
                    vm.showPeriod = true;
                    vm.viewModelHelper.apiGet('api/budgetingLevel/getbudgetingLevel/' + $stateParams.budgetingLevelId, null,
                   function (result) {
                       vm.budgetingLevel = result.data;
                    
                       getOperationReviews(vm.budgetingLevel.Year);

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.budgetingLevel = { ModuleCode:'', DefinitionCode: '', Centre: 1,ReviewCode:'',Year:'', Active: true };
            }
        }

        var initialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#budgetingLevelReviewTable').length > 0) {
                    var exportTable = $('#budgetingLevelReviewTable').DataTable({
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

        var initializeLookUp = function(){
            getModules();
            getDefinitions();
            getOperations();
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.budgetingLevel, budgetingLevelRules);
            vm.viewModelHelper.modelIsValid = vm.budgetingLevel.isValid;
            vm.viewModelHelper.modelErrors = vm.budgetingLevel.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/budgetingLevel/updatebudgetingLevel', vm.budgetingLevel,
               function (result) {
                   
                   $state.go('budget-budgetinglevel-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.budgetingLevel.errors;

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
                vm.viewModelHelper.apiPost('api/budgetingLevel/deletebudgetingLevel', vm.budgetingLevel.BudgetingLevelId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-budgetinglevel-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('budget-budgetinglevel-list');
        };

        vm.operationChanged = function (operation) {
            getOperationReviews(operation);
        }

        var getModules = function () {
            vm.viewModelHelper.apiGet('api/budgetmodule/availablemodules', null,
                 function (result) {
                     vm.modules = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getDefinitions = function () {
            vm.viewModelHelper.apiGet('api/budget/teamdefinition/availableteamDefinitions', null,
                 function (result) {
                     vm.definitions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getOperations = function () {
            vm.viewModelHelper.apiGet('api/operation/availableoperations', null,
                 function (result) {
                     vm.operations = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getOperationReviews = function (operation) {
            vm.viewModelHelper.apiGet('api/operationreview/getoperationreviewbyoperation/' + operation, null,
                 function (result) {
                     vm.operationReviews = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }
       
        setupRules();
        initialize(); 
    }
}());
