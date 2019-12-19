/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RatingMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        RatingMappingEditController]);

    function RatingMappingEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ratingmapping-edit-view';
        vm.viewName = 'Rating Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ratingMapping = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ratingmappingRules = [];
        vm.internalRatings = [];
        vm.externalRatings = [];


        var setupRules = function () {
          
            ratingmappingRules.push(new validator.PropertyRule("Credit_Risk_Id", {
                required: { message: "Credit Risk is required" }
            }));

            ratingmappingRules.push(new validator.PropertyRule("External_Rating_Id", {
                required: { message: "External Rating is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.ratingmappingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ratingmapping/getratingmapping/' + $stateParams.ratingmappingId, null,
                   function (result) {
                       vm.ratingMapping = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.ratingMapping = { Credit_Risk_Id: '', External_Rating_Id: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getInternalRatings()
            getExternalRatings()
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ratingMapping, ratingmappingRules);
            vm.viewModelHelper.modelIsValid = vm.ratingMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.ratingMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/ratingmapping/updateratingmapping', vm.ratingMapping,
               function (result) {
                   
                   $state.go('ifrs9-ratingmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.ratingMapping.errors;

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
                vm.viewModelHelper.apiPost('api/ratingmapping/deleteratingmapping', vm.ratingMapping.RatingMappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-ratingmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-ratingmapping-list');
        };



        var getInternalRatings = function () {
            vm.viewModelHelper.apiGet('api/internalratingbased/availableinternalRatingBaseds', null,
                 function (result) {
                     vm.internalRatings = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }


        var getExternalRatings = function () {
            vm.viewModelHelper.apiGet('api/externalrating/availableexternalRatings', null,
                 function (result) {
                     vm.externalRatings = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
