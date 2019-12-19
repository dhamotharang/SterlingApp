/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CollateralTypeEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CollateralTypeEditController]);

    function CollateralTypeEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'collateraltype-edit-view';
        vm.viewName = 'Collateral Types';

        vm.collateralType = {};

       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var collateralTypeRules = [];

        var setupRules = function () {
            collateralTypeRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            collateralTypeRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));
            collateralTypeRules.push(new validator.PropertyRule("CategoryCode", {
                required: { message: "Category is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                if ($stateParams.collateraltypeId !== 0) {
                    vm.viewModelHelper.apiGet('api/collateraltype/getcollateraltype/' + $stateParams.collateraltypeId, null,
                   function (result) {
                       vm.collateralType = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.collateralType = { Code: '', Name: '', CategoryCode: $stateParams.categorycode, Active: true };
            }
        }

        var initialView = function () {
         
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.collateralType, collateralTypeRules);
            vm.viewModelHelper.modelIsValid = vm.collateralType.isValid;
            vm.viewModelHelper.modelErrors = vm.collateralType.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/collateraltype/updatecollateraltype', vm.collateralType,
               function (result) {
                   
                   $state.go('ifrsloan-collateralcategory-edit', { collateralcategoryId: $stateParams.collateralcategoryId });
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.collateralType.errors;

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
                vm.viewModelHelper.apiPost('api/collateraltype/deletecollateraltype', vm.collateralType.CollateralTypeId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrsloan-collateralcategory-edit', { collateralcategoryId: $stateParams.collateralcategoryId });
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrsloan-collateralcategory-edit', { collateralcategoryId: $stateParams.collateralcategoryId });
        };

        setupRules();
        initialize(); 
    }
}());
