/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ExpenseMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ExpenseMappingEditController]);

    function ExpenseMappingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'expensemapping-edit-view';
        vm.viewName = 'Expense Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.expenseMappings = {};
        var selectedBasis = {};
       
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var expensemappingRules = [];

        var setupRules = function () {

            expensemappingRules.push(new validator.PropertyRule("BasisCode", {
                required: { message: "BasisCode is required" }
            }));

            //expensemappingRules.push(new validator.PropertyRule("ItemCode", {
            //    required: { message: "Code is required" }
            //}));

            expensemappingRules.push(new validator.PropertyRule("MISCode", {
                required: { message: "MisCode is required" }
            }));
    
            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

               if ($stateParams.expensemappingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/expensemapping/getexpensemapping/' + $stateParams.expensemappingId, null,
                   function (result) {
                       vm.expenseMapping = result.data;
                       
                       getItems(vm.expenseMapping.BasisCode);
                       vm.parentTeamChanged(vm.expenseMapping.ParentMISCode, vm.expenseMapping.BasisCode);
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.expenseMapping = { BasisCode: '', ItemCode: '',MisCode:'', Active: true };
            }
        }

        var intializeLookUp = function () {
            getBasis();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.expenseMapping, expensemappingRules);
            vm.viewModelHelper.modelIsValid = vm.expenseMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.expenseMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/expensemapping/updateexpensemapping', vm.expenseMapping,
               function (result) {
                   
                   $state.go('mpr-expensemapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.expenseMapping.errors;

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
                vm.viewModelHelper.apiPost('api/expensemapping/deleteexpensemapping', vm.expenseMapping.ExpenseMappingId,//vm.expenseMapping.expensemappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-expensemapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-expensemapping-list');
        };

        vm.onBasisChanged = function (basisCode) {
            getItems(basisCode);
        }

        var getBasis = function () {
            vm.viewModelHelper.apiGet('api/expensebasis/availableexpenseBasis', null,
                 function (result) {
                     vm.basis = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load expense basis.', 'Fintrak');
                 }, null);
        }

        var getItems = function (basisCode) {
            vm.items = [];
            vm.parentTeams = [];
            vm.isTeam = true;
            vm.isSelection = true;
            vm.isSelectionNotTeam = false;

            vm.viewModelHelper.apiGet('api/expensebasis/getexpenseBasisByCode/' + basisCode, null,
                 function (result) {
                     var b = result.data;
                     selectedBasis = b;

                     //load items
                     var url = '';
                     if (b.ItemType === 1 || b.ItemType === 2) {
                         vm.isTeam = false;

                         if (b.ItemType === 1) {
                             vm.isSelection = true;
                             vm.isSelectionNotTeam = true;
                             url = 'api/mprproduct/availablemprProducts';
                         }
                         else if (b.ItemType === 2)
                             vm.isSelection = false;
                         //    url = 'api/team/getteambydefinition/' + b.TeamDefinitionCode;

                         vm.viewModelHelper.apiGet(url, null,
                         function (result) {

                             angular.forEach(result.data, function (data) {
                                 if (b.ItemType === 1)
                                     vm.items.push({ Code: data.ProductCode, Name: data.ProductName });
                                 else if (b.ItemType === 3)
                                     vm.items.push({ Code: data.Code, Name: data.Name });

                             });
                         },
                         function (result) {
                             toastr.error('Fail to load items.', 'Fintrak');
                         }, null);
                     }

                     //load teams
                     vm.viewModelHelper.apiGet('api/team/getteambydefinition/' + b.TeamDefinitionCode, null,
                         function (result) {

                             vm.parentTeams = result.data;
                         },
                         function (result) {
                             toastr.error(result.data, 'Fintrak');
                         }, null);
                 },
                 function (result) {
                     toastr.error('Fail to load expense basis.', 'Fintrak');
                 }, null);
        }

        vm.parentTeamChanged = function (misCode,basisCode) {
            vm.viewModelHelper.apiGet('api/expensebasis/getexpenseBasisByCode/' + basisCode, null,
                function (result) {
                    var b = result.data;

                    vm.viewModelHelper.apiGet('api/team/getteambyparentdefinition/' + b.TeamDefinitionCode + '/' + misCode, null,
                        function (result) {

                            vm.teams = result.data;
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                },
                 function (result) {
                     toastr.error('Fail to load expense basis.', 'Fintrak');
                 }, null);
           
        }

        setupRules();
        initialize();
    }
}());
