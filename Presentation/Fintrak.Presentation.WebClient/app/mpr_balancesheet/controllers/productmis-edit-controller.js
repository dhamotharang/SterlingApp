/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ProductMISEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ProductMISEditController]);

    function ProductMISEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'productmis-edit-view';
        vm.viewName = 'Product';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.productMIS = {};

        vm.products = [];
        vm.captions = [];
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var productmisRules = [];

        var setupRules = function () {
          
            productmisRules.push(new validator.PropertyRule("ProductCode", {
                required: { message: "Product is required" }
            }));

            productmisRules.push(new validator.PropertyRule("CaptionCode", {
                required: { message: "Caption is required" }
            }));

            productmisRules.push(new validator.PropertyRule("TeamDefinitionCode", {
                required: { message: "Team Definition is required" }
            }));

            productmisRules.push(new validator.PropertyRule("TeamCode", {
                required: { message: "Team is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.productmisId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/productmis/getproductmis/' + $stateParams.productmisId, null,
                   function (result) {
                       vm.productMIS = result.data;

                       getTeams(vm.productMIS.TeamDefinitionCode);
                       getAccountOfficers(vm.productMIS.AccountOfficerDefinitionCode);

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.productMIS = { ProductCode: '', CaptionCode: '', TeamDefinitionCode: '', TeamCode: '',AccountOfficerDefinitionCode: '', AccountOfficerCode: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getProducts();
            getCaptions();
            getTeamDefinitions();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.productMIS, productmisRules);
            vm.viewModelHelper.modelIsValid = vm.productMIS.isValid;
            vm.viewModelHelper.modelErrors = vm.productMIS.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/productmis/updateproductmis', vm.productMIS,
               function (result) {
                   
                   $state.go('mpr-productmis-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.productMIS.errors;

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
                vm.viewModelHelper.apiPost('api/productmis/deleteproductmis', vm.productMIS.ProductMISId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-productmis-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-productmis-list');
        };

        var getProducts = function () {
            vm.viewModelHelper.apiGet('api/product/availableproducts', null,
                 function (result) {
                     vm.products = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getCaptions = function () {
            vm.viewModelHelper.apiGet('api/bscaption/availablebscaptions', null,
                 function (result) {
                     vm.captions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        vm.onTeamDefinitionChanged = function (definition) {
            getTeams(definition);
        }

        vm.onAccountOfficerDefinitionChanged = function (definition) {
            getAccountOfficers(definition);
        }

        var getTeamDefinitions = function () {
            vm.viewModelHelper.apiGet('api/teamdefinition/availableteamdefinitions', null,
                 function (result) {
                     vm.teamDefinitions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getTeams = function (definition) {
            vm.viewModelHelper.apiGet('api/team/getteambydefinition/' + definition, null,
                 function (result) {
                     vm.teams = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getAccountOfficers = function (definition) {
            vm.viewModelHelper.apiGet('api/team/getteambydefinition/' + definition, null,
                 function (result) {
                     vm.accountOfficers = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
