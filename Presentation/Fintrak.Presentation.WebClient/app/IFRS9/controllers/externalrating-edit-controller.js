/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ExternalRatingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ExternalRatingEditController]);

    function ExternalRatingEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'externalrating-edit-view';
        vm.viewName = 'External Rating';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.externalRating = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var externalratingRules = [];

        vm.agencies = [
            { Id: 1, Name: 'Fitch' },
            { Id: 2, Name: 'Moodys' },
            { Id: 3, Name: 'S & P' },
        ];

        vm.categories = [
            { Id: 1, Name: 'Investment Grade' },
            { Id: 2, Name: 'Speculative Grade'},
        ];

        var setupRules = function () {
          
            externalratingRules.push(new validator.PropertyRule("Agency", {
                required: { message: "Agency is required" }
            }));

            externalratingRules.push(new validator.PropertyRule("Rating", {
                required: { message: "Rating is required" }
            }));

            externalratingRules.push(new validator.PropertyRule("Category", {
                required: { message: "Category is required" }
            }));

            externalratingRules.push(new validator.PropertyRule("Description", {
                required: { message: "Description is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.externalratingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/externalrating/getexternalrating/' + $stateParams.externalratingId, null,
                   function (result) {
                       vm.externalRating = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.externalRating = { Agency: '', Rating: '', Category: '', Description: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.externalRating, externalratingRules);
            vm.viewModelHelper.modelIsValid = vm.externalRating.isValid;
            vm.viewModelHelper.modelErrors = vm.externalRating.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/externalrating/updateexternalrating', vm.externalRating,
               function (result) {
                   
                   $state.go('ifrs9-externalrating-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.externalRating.errors;

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
                vm.viewModelHelper.apiPost('api/externalrating/deleteexternalrating', vm.externalRating.ExternalRatingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-externalrating-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-externalrating-list');
        };

        setupRules();
        initialize(); 
    }
}());
