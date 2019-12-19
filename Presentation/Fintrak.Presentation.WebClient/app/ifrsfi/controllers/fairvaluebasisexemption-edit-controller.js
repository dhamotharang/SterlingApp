/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FairValueBasisExemptionEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        FairValueBasisExemptionEditController]);

    function FairValueBasisExemptionEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'fairvaluebasisexemption-edit-view';
        vm.viewName = 'Fair Value Basis Exemption';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.fairValueBasisExemption = {};
        
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

        //vm.classifications = [
        //  { Id: 1, Name: 'HTM' },
        //  { Id: 2, Name: 'HFT' },
        //  { Id: 3, Name: 'AFS' }
        //];
       
        //vm.Classifications = [
        //   { Id: 1, Name: 'HTM' },
        //   { Id: 2, Name: 'HFT' },
        //   { Id: 3, Name: 'AFS' }
        //];

        var fairvaluebasisexemptionRules = [];

        var setupRules = function () {
          
            fairvaluebasisexemptionRules.push(new validator.PropertyRule("RefNo", {
                notZero: { message: "RefNo is required" }
            }));

            fairvaluebasisexemptionRules.push(new validator.PropertyRule("Instrument", {
                notZero: { message: "Instrument is required" }
            }));

            fairvaluebasisexemptionRules.push(new validator.PropertyRule("BasisLevel", {
                notZero: { message: "Basis is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.fairvaluebasisexemptionId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/fairvaluebasisexemption/getfairvaluebasisexemption/' + $stateParams.fairvaluebasisexemptionId, null,
                   function (result) {
                       vm.fairValueBasisExemption = result.data;

                       initialView();
                       vm.init === true;
                     
                   },
                   function (result) {
                     
                   }, null);
               }
               else
                    vm.fairValueBasisExemption = { RefNo: '', BasisLevel: 1, InstrumentType: 1, Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.fairValueBasisExemption, fairvaluebasisexemptionRules);
            vm.viewModelHelper.modelIsValid = vm.fairValueBasisExemption.isValid;
            vm.viewModelHelper.modelErrors = vm.fairValueBasisExemption.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/fairvaluebasisexemption/updatefairvaluebasisexemption', vm.fairValueBasisExemption,
               function (result) {
                 //
                   $state.go('ifrsfi-fairvaluebasisexemption-list');
               },
               function (result) {
                  
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.fairValueBasisExemption.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                }); 
            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/fairvaluebasisexemption/deletefairvaluebasisexemption', vm.fairValueBasisExemption.FairValueBasisExemptionId,
              function (result) {
                 //
                  $state.go('ifrsfi-fairvaluebasisexemption-list');
              },
              function (result) {
                 
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrsfi-fairvaluebasisexemption-list');
        };

        setupRules();
        initialize(); 
    }
}());
