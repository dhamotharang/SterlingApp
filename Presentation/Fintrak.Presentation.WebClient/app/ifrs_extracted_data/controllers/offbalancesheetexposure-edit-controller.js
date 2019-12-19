/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OffBalanceSheetExposureEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OffBalanceSheetExposureEditController]);

    function OffBalanceSheetExposureEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Off Balancesheet Exposure';
        vm.view = 'offbalancesheetexposure-edit-view';
        vm.viewName = 'Off BalanceSheet Exposure Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.offBalanceSheetExposure = {};

        vm.collateralTypes = [];

        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.portfolio = '';


        vm.stages = [
        { Id: 1, Name: 'Stage 1' },
        { Id: 2, Name: 'Stage 2' },
        { Id: 3, Name: 'Stage 3' }
        ];

        vm.Portfolios = [
          { Id: 1, Name: 'CORPORATE' },
          { Id: 2, Name: 'Non-Corporate' }
        ];
       
        var offbalancesheetexposureRules = [];

        var setupRules = function () {
          
            offbalancesheetexposureRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));

            offbalancesheetexposureRules.push(new validator.PropertyRule("CUSTOMER_NAME", {
                required: { message: "CUSTOMER_NAME is required" }
            }));

            offbalancesheetexposureRules.push(new validator.PropertyRule("CollateralType", {
                required: { message: "CollateralType is required" }
            }));

            offbalancesheetexposureRules.push(new validator.PropertyRule("CollateralValue", {
                required: { message: "CollateralValue is required" }
            }));

            offbalancesheetexposureRules.push(new validator.PropertyRule("Staging", {
                required: { message: "Staging is required" }
            }));

            offbalancesheetexposureRules.push(new validator.PropertyRule("TRNX_DATE", {
                notZero: { message: "EffectiveDate is required" }
            }));

            offbalancesheetexposureRules.push(new validator.PropertyRule("MATURITY_DATE", {
                notZero: { message: "MaturityDate is required" }
            }));

            offbalancesheetexposureRules.push(new validator.PropertyRule("Amount_FCY", {
                required: { message: "Amount_FCY is required" }
            }));

            offbalancesheetexposureRules.push(new validator.PropertyRule("RATING", {
                required: { message: "RATING is required" }
            }));
           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.ObeId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/offbalancesheetexposure/gettoffbalancesheetexposure/' + $stateParams.ObeId, null,
                   function (result) {
                       vm.OffBalanceSheetExposures = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.OffBalanceSheetExposures = { RefNo: '', Portfolio: '', TRNX_DATE: '', CollateralValue: '', CollateralType: '', MATURITY_DATE: '', Amount_FCY: '', Amount_NGN: '', Active: true };
            }
        }

        var intializeLookUp = function () {

            getcollateralTypes();

        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.offBalanceSheetExposure, offbalancesheetexposureRules);
            vm.viewModelHelper.modelIsValid = vm.offBalanceSheetExposure.isValid;
            vm.viewModelHelper.modelErrors = vm.offBalanceSheetExposure.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/offbalancesheetexposure/updateoffbalancesheetexposure', vm.OffBalanceSheetExposures,
               function (result) {
                   
                   $state.go('ifrs-offbalancesheetexposure-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.offBalanceSheetExposure.errors;

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
                vm.viewModelHelper.apiPost('api/offbalancesheetexposure/deleteoffbalancesheetexposure', vm.offBalanceSheetExposure.ObeId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-offbalancesheetexposure-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-offbalancesheetexposure-list');
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
