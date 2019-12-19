/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("HistoricalSectorRatingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        HistoricalSectorRatingEditController]);

    function HistoricalSectorRatingEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'historicalsectorrating-edit-view';
        vm.viewName = 'Historical Sector Rating';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.historicalSectorRating = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var historicalsectorratingRules = [];

        vm.sectors = [];

        //vm.RunDate = new Date();

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
          
            historicalsectorratingRules.push(new validator.PropertyRule("Sector", {
                required: { message: "Sector is required" }
            }));

            historicalsectorratingRules.push(new validator.PropertyRule("Rating", {
                required: { message: "Rating is required" }
            }));

            historicalsectorratingRules.push(new validator.PropertyRule("RunDate", {
                required: { message: "RunDate is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.historicalsectorratingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/historicalsectorrating/gethistoricalsectorrating/' + $stateParams.historicalsectorratingId, null,
                   function (result) {
                       vm.historicalSectorRating = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.historicalSectorRating = { Sector: '', Rating: '', RunDate: '',  Active: true };
            }
        }

        var intializeLookUp = function () {
            getSectors()
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.historicalSectorRating, historicalsectorratingRules);
            vm.viewModelHelper.modelIsValid = vm.historicalSectorRating.isValid;
            vm.viewModelHelper.modelErrors = vm.historicalSectorRating.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/historicalsectorrating/updatehistoricalsectorrating', vm.historicalSectorRating,
               function (result) {
                   
                   $state.go('ifrs9-historicalsectorrating-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.historicalSectorRating.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
                toastr.error(errorList, 'Fintrak');
            }
                
        }


        vm.openRunDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate = true;
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/historicalsectorrating/deletehistoricalsectorrating', vm.historicalSectorRating.HistoricalSectorRatingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-historicalsectorrating-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-historicalsectorrating-list');
        };

        var getSectors = function () {
            vm.viewModelHelper.apiGet('api/sector/availablesectors', null,
                 function (result) {
                     vm.sectors = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
