/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("EclCalculationModelEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        EclCalculationModelEditController]);

    function EclCalculationModelEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'sector-edit-view';
        vm.viewName = 'EclCalculationModel'

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.sector = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var sectorRules = [];

        //vm.agencies = [
        //    { Id: 1, Name: 'Fitch' },
        //    { Id: 2, Name: 'Moodys' },
        //    { Id: 3, Name: 'S & P' },
        //];

        //vm.categories = [
        //    { Id: 1, Name: 'Investment Grade' },
        //    { Id: 2, Name: 'Speculative Grade'},
        //];

        var setupRules = function () {
          
            sectorRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));


            sectorRules.push(new validator.PropertyRule("Description", {
                required: { message: "Description is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.sectorId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/sector/getsector/' + $stateParams.sectorId, null,
                   function (result) {
                       vm.sector = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.sector = { Code: '', Description: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.sector, sectorRules);
            vm.viewModelHelper.modelIsValid = vm.sector.isValid;
            vm.viewModelHelper.modelErrors = vm.sector.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/sector/updatesector', vm.sector,
               function (result) {
                   
                   $state.go('ifrs9-sector-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.sector.errors;

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
                vm.viewModelHelper.apiPost('api/sector/deletesector', vm.sector.EclCalculationModelId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-sector-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-sector-list');
        };




        setupRules();
        initialize(); 
    }
}());
