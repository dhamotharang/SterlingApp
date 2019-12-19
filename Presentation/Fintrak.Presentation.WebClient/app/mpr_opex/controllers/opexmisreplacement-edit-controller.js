/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexMISReplacementEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OpexMISReplacementEditController]);

    function OpexMISReplacementEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'opexmisreplacement-edit-view';
        vm.viewName = 'OPEX MIS Replacement';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.opexmisReplacement = {};
        vm.costCentres = [];
      
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var opexmisreplacementRules = [];

        var setupRules = function () {

            opexmisreplacementRules.push(new validator.PropertyRule("OldMISCode", {
                required: { message: "Old MIS code is required" }
            }));

            //opexmisreplacementRules.push(new validator.PropertyRule("TeamDefinitionId", {
            //    notZero: { message: "Team definition is required" }
            //}));

           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.opexmisreplacementId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/opexmisreplacement/getopexmisreplacement/' + $stateParams.opexmisreplacementId, null,
                   function (result) {
                       vm.opexmisReplacement = result.data;

                     //  getTeams(vm.misReplacement.DefinitionCode);

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.opexmisReplacement = { OldMIScode:'',MISCode:'', Active: true };
            }
        }

        var intializeLookUp = function () {
            getCostCentres();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.opexmisReplacement, opexmisreplacementRules);
            vm.viewModelHelper.modelIsValid = vm.opexmisReplacement.isValid;
            vm.viewModelHelper.modelErrors = vm.opexmisReplacement.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/opexmisreplacement/updateopexmisreplacement', vm.opexmisReplacement,
               function (result) {
                   
                   $state.go('mpr-opexmisreplacement-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.opexmisReplacement.errors;

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
                vm.viewModelHelper.apiPost('api/opexmisreplacement/deleteopexmisreplacement', vm.opexmisReplacement.OpexMISReplacementId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-opexmisreplacement-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-opexmisreplacement-list');
        };

        var getCostCentres = function () {
            vm.viewModelHelper.apiGet('api/costcentre/availablecostcentres', null,
                 function (result) {
                     vm.costCentres = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load cost centres.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
