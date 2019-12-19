/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FairValueBasisMapEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        FairValueBasisMapEditController]);

    function FairValueBasisMapEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'fairvaluebasismap-edit-view';
        vm.viewName = 'Fair Value Basis';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.fairValueBasisMap = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.instruments = [
            { Id: 1, Name: 'Bonds' },
            { Id: 2, Name: 'TBills' },
            { Id: 3, Name: 'Loans' },
            { Id: 4, Name: 'Equity' },
            { Id: 5, Name: 'FinancialLiability' }
        ];

        vm.classifications = [
          { Id: 1, Name: 'HTM' },
          { Id: 2, Name: 'HFT' },
          { Id: 3, Name: 'AFS' }
        ];
       
        var fairvaluebasismapRules = [];

        var setupRules = function () {
          
            fairvaluebasismapRules.push(new validator.PropertyRule("Classification", {
                notZero: { message: "Classification is required" }
            }));

            fairvaluebasismapRules.push(new validator.PropertyRule("Instrument", {
                notZero: { message: "Instrument is required" }
            }));

            fairvaluebasismapRules.push(new validator.PropertyRule("BasisLevel", {
                notZero: { message: "Basis is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.fairvaluebasismapId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/fairvaluebasismap/getfairvaluebasismap/' + $stateParams.fairvaluebasismapId, null,
                   function (result) {
                       vm.fairValueBasisMap = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.fairValueBasisMap = { Classification: 1, BasisLevel: 1, InstrumentType: 1, Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.fairValueBasisMap, fairvaluebasismapRules);
            vm.viewModelHelper.modelIsValid = vm.fairValueBasisMap.isValid;
            vm.viewModelHelper.modelErrors = vm.fairValueBasisMap.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/fairvaluebasismap/updatefairvaluebasismap', vm.fairValueBasisMap,
               function (result) {
                   
                   $state.go('ifrsfi-fairvaluebasismap-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.fairValueBasisMap.errors;

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
                vm.viewModelHelper.apiPost('api/fairvaluebasismap/deletefairvaluebasismap', vm.fairValueBasisMap.FairValueBasisMapId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrsfi-fairvaluebasismap-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrsfi-fairvaluebasismap-list');
        };

        setupRules();
        initialize(); 
    }
}());
