/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CollateralRealizationPeriodEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CollateralRealizationPeriodEditController]);

    function CollateralRealizationPeriodEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'collateralrealizationperiod-edit-view';
        vm.viewName = 'Other LGD Parameters';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.collateralRealizationPeriod = {};

        vm.collateralTypes = [];
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var collateralrealizationperiodRules = [];

        var setupRules = function () {
          
            collateralrealizationperiodRules.push(new validator.PropertyRule("TypeCode", {
                required: { message: "Collateral Type is required" }
            }));

            collateralrealizationperiodRules.push(new validator.PropertyRule("Duration", {
                required: { message: "Duration(in Months) is required" }
            }));
            collateralrealizationperiodRules.push(new validator.PropertyRule("HairCut", {
                required: { message: "Hair Cut is required" }
            }));
            collateralrealizationperiodRules.push(new validator.PropertyRule("AvgRecoveryCost", {
                required: { message: "Avg. Recovery Cost is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.collateralrealizationperiodId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/collateralrealizationperiod/getcollateralrealizationperiod/' + $stateParams.collateralrealizationperiodId, null,
                   function (result) {
                       vm.collateralRealizationPeriod = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.collateralRealizationPeriod = { ProductCode: '', collateralTypeCode: '', MarketRate: '', PastDueRate: '',  Active: true };
            }
        }

        var intializeLookUp = function () {
        //getProducts();
           // getLoanProducts();
        getcollateralTypes();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.collateralRealizationPeriod, collateralrealizationperiodRules);
            vm.viewModelHelper.modelIsValid = vm.collateralRealizationPeriod.isValid;
            vm.viewModelHelper.modelErrors = vm.collateralRealizationPeriod.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/collateralrealizationperiod/updatecollateralrealizationperiod', vm.collateralRealizationPeriod,
               function (result) {
                   
                   $state.go('ifrsloan-collateralrealizationperiod-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.collateralRealizationPeriod.errors;

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
                vm.viewModelHelper.apiPost('api/collateralrealizationperiod/deletecollateralrealizationperiod', vm.collateralRealizationPeriod.CollateralRealizationPeriodId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrsloan-collateralrealizationperiod-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrsloan-collateralrealizationperiod-list');
        };

        var getcollateralTypes = function () {
            vm.viewModelHelper.apiGet('api/collateralType/availablecollateralTypes', null,
                 function (result) {
                     vm.collateralTypes = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load collateral types.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
