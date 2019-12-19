/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ProbabilityWeightedEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ProbabilityWeightedEditController]);

    function ProbabilityWeightedEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'probabilityweighted-edit-view';
        vm.viewName = 'Probability Weigthing for ECL Scenario'

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.probabilityWeighted = {};
        vm.selectedOption = "";
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.instrumentTypes = [
          { Id: 1, Name: 'Loans' },
          { Id: 2, Name: 'Bonds' },
          { Id: 3, Name: 'Others' }
        ];

        var probabilityWeightedRules = [];

        var setupRules = function () {


            probabilityWeightedRules.push(new validator.PropertyRule("ProductType", {
                required: { message: "ProductType is required" }
            }));


            probabilityWeightedRules.push(new validator.PropertyRule("SubType", {
                required: { message: "SubType is required" }
            }));
          
            probabilityWeightedRules.push(new validator.PropertyRule("Optimistic", {
                required: { message: "Optimistic is required" }
            }));


            probabilityWeightedRules.push(new validator.PropertyRule("Best", {
                required: { message: "Best Estimate is required" }
            }));


            probabilityWeightedRules.push(new validator.PropertyRule("Downturn", {
                required: { message: "Downturn is required" }
            }));


            probabilityWeightedRules.push(new validator.PropertyRule("InstrumentType", {
                required: { message: "Instrument Type is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.ProbabilityWeighted_Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/probabilityweighted/getprobabilityweighted/' + $stateParams.ProbabilityWeighted_Id, null,
                   function (result) {
                       vm.probabilityWeighted = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.probabilityWeighted = {
                        ProductType: '',
                        SubType: '',
                        Optimistic: '',
                        Best: '',
                        Downturn: '',
                        InstrumentType: '',
                        Active: true
                    };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.probabilityWeighted, probabilityWeightedRules);
            vm.viewModelHelper.modelIsValid = vm.probabilityWeighted.isValid;
            vm.viewModelHelper.modelErrors = vm.probabilityWeighted.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/probabilityweighted/updateprobabilityweighted', vm.probabilityWeighted,
               function (result) {
                   
                   $state.go('ifrs9-probabilityweighted-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.probabilityWeighted.errors;

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
                vm.viewModelHelper.apiPost('api/probabilityweighted/deleteprobabilityweighted', vm.probabilityWeighted.ProbabilityWeighted_Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-probabilityweighted-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-probabilityweighted-list');
        };


        setupRules();
        initialize(); 
    }
}());
