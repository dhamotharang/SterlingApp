/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("InternalRatingBasedEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        InternalRatingBasedEditController]);

    function InternalRatingBasedEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'internalratingbased-edit-view';
        vm.viewName = 'Internal Rating Based';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.internalRatingBased = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var internalratingbasedRules = [];

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
          
            internalratingbasedRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            internalratingbasedRules.push(new validator.PropertyRule("PD", {
                required: { message: "PD is required" }
            }));

            internalratingbasedRules.push(new validator.PropertyRule("PD_LowerBoundary", {
                required: { message: "PD_LowerBoundary is required" }
            }));

            internalratingbasedRules.push(new validator.PropertyRule("PD_UpperBoundary", {
                required: { message: "PD_UpperBoundary is required" }
            }));

            internalratingbasedRules.push(new validator.PropertyRule("Description", {
                required: { message: "Description is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.internalratingbasedId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/internalratingbased/getinternalratingbased/' + $stateParams.internalratingbasedId, null,
                   function (result) {
                       vm.internalRatingBased = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.internalRatingBased = { Code: '', PD: 0, PD_LowerBoundary: 0, PD_UpperBoundary: 0, Description: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.internalRatingBased, internalratingbasedRules);
            vm.viewModelHelper.modelIsValid = vm.internalRatingBased.isValid;
            vm.viewModelHelper.modelErrors = vm.internalRatingBased.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/internalratingbased/updateinternalratingbased', vm.internalRatingBased,
               function (result) {
                   
                   $state.go('ifrs9-internalratingbased-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.internalRatingBased.errors;

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
                vm.viewModelHelper.apiPost('api/internalratingbased/deleteinternalratingbased', vm.internalRatingBased.InternalRatingBasedId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-internalratingbased-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-internalratingbased-list');
        };

        setupRules();
        initialize(); 
    }
}());
