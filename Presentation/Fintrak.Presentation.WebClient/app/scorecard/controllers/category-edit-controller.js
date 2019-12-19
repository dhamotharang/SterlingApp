/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CategoryEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CategoryEditController]);

    function CategoryEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Scorecard';
        vm.view = 'category-edit-view';
        vm.viewName = 'Perspective';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.category = {};

        vm.parents = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var categoryRules = [];

        var setupRules = function () {
          
            categoryRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            categoryRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.categoryId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/category/getcategory/' + $stateParams.categoryId, null,
                   function (result) {
                       vm.category = result.data;

                       initialView();
                       vm.init = true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.category = { Code: '', Name: '',Active: true };
            }
        }


        var intializeLookUp = function () {
            getParents();
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.category, categoryRules);
            vm.viewModelHelper.modelIsValid = vm.category.isValid;
            vm.viewModelHelper.modelErrors = vm.category.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/category/updatecategory', vm.category,
               function (result) {
                   
                   $state.go('scd-category-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.category.errors;

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
                vm.viewModelHelper.apiPost('api/category/deletecategory', vm.category.CategoryId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('scd-category-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('scd-category-list');
        };

        var getParents = function () {
            vm.viewModelHelper.apiGet('api/category/availablecategory', null,
                 function (result) {
                     vm.parents = result.data;
                     vm.init === true;

                 },
                 function (result) {

                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
