/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CreditRiskRatingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CreditRiskRatingEditController]);

    function CreditRiskRatingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS LOANS';
        vm.view = 'creditriskrating-edit-view';
        vm.viewName = 'Credit Risk Rating';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.creditRiskRating = {};
        vm.companies = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var creditriskratingRules = [];
       

        var setupRules = function () {

            creditriskratingRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));
                      creditriskratingRules.push(new validator.PropertyRule("EP", {
                          required: { message: "Emergence Period is required" }
                      }));
                      creditriskratingRules.push(new validator.PropertyRule("LGD", {
                          required: { message: "Loss Given Default is required" }
                      }));
                      creditriskratingRules.push(new validator.PropertyRule("CompanyCode", {
                          required: { message: "Company Code is required" }
                      }));
                     
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
              intializeLookUp();

                if ($stateParams.creditriskratingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/creditriskrating/getcreditriskrating/' + $stateParams.creditriskratingId, null,
                   function (result) {
                       vm.creditRiskRating = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.creditRiskRating = { Code: '', EP: '', LGD: '', PD: '',Description: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getCompanies();
           }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.creditRiskRating, creditriskratingRules);
            vm.viewModelHelper.modelIsValid = vm.creditRiskRating.isValid;
            vm.viewModelHelper.modelErrors = vm.creditRiskRating.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/creditriskrating/updatecreditriskrating', vm.creditRiskRating,
               function (result) {
                   
                   $state.go('ifrsloan-creditriskrating-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.creditRiskRating.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
                toastr.error(errorList, 'Fintrak');
            }

        }
        // vm.derivedCaption.DerivedCaptionId,
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/creditriskrating/deletecreditriskrating', vm.creditRiskRating.CreditRiskRatingId,//vm.creditRiskRating.creditriskratingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrsloan-creditriskrating-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrsloan-creditriskrating-list');
        };

        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                 function (result) {
                     vm.companies = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }
        setupRules();
        initialize();
    }
}());
