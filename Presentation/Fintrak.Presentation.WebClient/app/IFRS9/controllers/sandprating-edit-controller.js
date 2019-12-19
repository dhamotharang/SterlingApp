/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SandPRatingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SandPRatingEditController]);

    function SandPRatingEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'sandprating-edit-view';
        vm.viewName = 'S and P Rating';
        vm.status = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.sandPRating = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var sandpratingRules = [];

        var setupRules = function () {

            sandpratingRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            sandpratingRules.push(new validator.PropertyRule("Rating", {
                required: { message: "Rating is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.SandPRating_Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/sandprating/getsandprating/' + $stateParams.SandPRating_Id, null,
                   function (result) {
                       vm.sandPRating = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.sandPRating = { Year: '', Rating: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.sandPRating, sandpratingRules);
            vm.viewModelHelper.modelIsValid = vm.sandPRating.isValid;
            vm.viewModelHelper.modelErrors = vm.sandPRating.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/sandprating/updatesandprating', vm.sandPRating,
               function (result) {
                   
                   $state.go('ifrs9-sandprating-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.sandPRating.errors;

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
                vm.viewModelHelper.apiPost('api/sandprating/deletesandprating', vm.sandPRating.SandPRating_Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-sandprating-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-sandprating-list');
        };

        
        setupRules();
        initialize(); 
    }
}());
