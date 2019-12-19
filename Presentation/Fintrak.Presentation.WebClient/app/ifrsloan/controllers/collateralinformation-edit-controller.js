/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CollateralInformationEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CollateralInformationEditController]);

    function CollateralInformationEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'collateralinformation-edit-view';
        vm.viewName = 'Collateral Information';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.collateralInformations = {};
        vm.collateralCategories = [];
        vm.collateralType = [];

       
   

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var collateralinformationRules = [];

        var setupRules = function () {

            collateralinformationRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));

            collateralinformationRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "AccountNo is required" }
            }));

             collateralinformationRules.push(new validator.PropertyRule("Amount", {
                required: { message: "Amount is required" }
            }));

            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

                if ($stateParams.collateralinformationId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/collateralinformation/getcollateralinformation/' + $stateParams.collateralinformationId, null,
                   function (result) {
                       vm.collateralInformations = result.data;
                       //getCCDefintion();
                       //initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.collateralInformations = { RefNo: '', AccountNo: '', Category: '', Type: '', CustomerName: '', Amount: 0, Active: true };
            }
        }

        var intializeLookUp = function () {
            getCollateralCategories();
            getAllCollateralTypes();
   
        }
       

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.collateralInformations, collateralinformationRules);
            vm.viewModelHelper.modelIsValid = vm.collateralInformations.isValid;
            vm.viewModelHelper.modelErrors = vm.collateralInformations.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/collateralinformation/updatecollateralinformation', vm.collateralInformations,
               function (result) {
                   
                   $state.go('ifrsloan-collateralinformation-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.collateralInformations.errors;

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
                vm.viewModelHelper.apiPost('api/collateralinformation/deletecollateralinformation', vm.collateralInformations.CollateralInformationId,//vm.collateralInformation.collateralinformationId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrsloan-collateralinformation-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrsloan-collateralinformation-list');
        };

        vm.onCollateralCategoryChanged = function (categoryCode) {
            getTypesByCatCode(categoryCode);
        }

        var getCollateralCategories = function () {
            vm.viewModelHelper.apiGet('api/collateralcategory/availablecollateralcategories/', null,
                   function (result) {
                       vm.collateralCategories = result.data;
                      // vm.collateralTypes = result.data.CollateralType;
                      // initialView();
                       vm.init === true;
                       toastr.success('Collateral Categories data loaded, ready for modifiation.', 'Fintrak');
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
        }
        var getAllCollateralTypes = function () {
            vm.viewModelHelper.apiGet('api/collateraltype/availablecollateralTypes/', null,
                   function (result) {
                       vm.collateralType = result.data;
                       vm.init === true;
                       toastr.success('Collateral Types data loaded, ready for modifiation.', 'Fintrak');
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
        }

        var getTypesByCatCode = function (categoryCode) {
            vm.viewModelHelper.apiGet('api/collateralType/getcollateralTypebyCode/' + categoryCode, null,
                 function (result) {
                     vm.collateralType = result.data;
                 },
                 function (result) {
                     vm.collateralType = [];
                     toastr.error(result.data, 'Fintrak');

                 }, null);
        }
        setupRules();
        initialize();
    }
}());
