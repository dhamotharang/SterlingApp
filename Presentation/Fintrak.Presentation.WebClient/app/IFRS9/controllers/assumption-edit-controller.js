/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("AssumptionEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        AssumptionEditController]);

    function AssumptionEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'assumption-edit-view';
        vm.viewName = 'Assumption';
        vm.status = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.assumptions = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var assumptionRules = [];

        var setupRules = function () {

            assumptionRules.push(new validator.PropertyRule("Instrument", {
                required: { message: "Instrument is required" }
            }));

            assumptionRules.push(new validator.PropertyRule("Highest_Level_of_Speculative", {
                required: { message: "Highest_Level_of_Speculative is required" }
            }));

            assumptionRules.push(new validator.PropertyRule("Notches_for_sicr", {
                required: { message: "Notches_for_sicr is required" }
            }));

            assumptionRules.push(new validator.PropertyRule("Default_Rating", {
                required: { message: "Default_Rating is required" }
            }));

            assumptionRules.push(new validator.PropertyRule("Assumed_DefaultRating", {
                required: { message: "Assumed_DefaultRating is required" }
            }));

            assumptionRules.push(new validator.PropertyRule("Assumed_EIR", {
                required: { message: "Assumed_EIR is required" }
            }));

            assumptionRules.push(new validator.PropertyRule("Assumed_Tenor", {
                required: { message: "Assumed_Tenor is required" }
            }));

            assumptionRules.push(new validator.PropertyRule("Assumed_MaturityDate", {
                required: { message: "Assumed_MaturityDate is required" }
            }));

            assumptionRules.push(new validator.PropertyRule("Assumed_StartDate", {
                required: { message: "Assumed_StartDate is required" }
            }));

            assumptionRules.push(new validator.PropertyRule("Assumed_CCF_Guarantee", {
                required: { message: "Assumed_CCF_Guarantee is required" }
            }));

            assumptionRules.push(new validator.PropertyRule("Assumed_CCF_LCs", {
                required: { message: "Assumed_CCF_LCs is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.InstrumentID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/assumption/getassumption/' + $stateParams.InstrumentID, null,
                   function (result) {
                       vm.assumptions = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.assumptions = { Instrument: '', Highest_Level_of_Speculative: '', Notches_for_sicr: '', Default_Rating: '', Assumed_DefaultRating: '', Assumed_EIR: '', Assumed_Tenor: '', Assumed_MaturityDate: '', Assumed_StartDate: '', Assumed_CCF_Guarantee: '', Assumed_CCF_LCs: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.assumptions, assumptionRules);
            vm.viewModelHelper.modelIsValid = vm.assumptions.isValid;
            vm.viewModelHelper.modelErrors = vm.assumptions.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/assumption/updateassumption', vm.assumptions,
               function (result) {
                   
                   $state.go('ifrs9-assumption-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.assumptions.errors;

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
                vm.viewModelHelper.apiPost('api/assumption/deleteassumption', vm.assumptions.InstrumentID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-assumption-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-assumption-list');
        };

        
        setupRules();
        initialize(); 
    }
}());
