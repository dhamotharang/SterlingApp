/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("NseIndexEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        NseIndexEditController]);

    function NseIndexEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'NSEIndex';
        vm.view = 'nseindex-edit-view';
        vm.viewName = 'NSE Index Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.nseIndex = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

 
        var nseIndexRules = [];

        var setupRules = function () {
          
            nseIndexRules.push(new validator.PropertyRule("Date", {
                required: { message: "Date is required" }
            }));

            nseIndexRules.push(new validator.PropertyRule("NSEIndex", {
                required: { message: "NSEIndex is required" }
            }));

            nseIndexRules.push(new validator.PropertyRule("NSEIndex", {
                notZero: { message: "NSEIndex must not be zero" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.nseIndexId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/nseindex/getnseindex/' + $stateParams.nseIndexId, null,
                   function (result) {
                       vm.nseIndex = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.nseIndex = { Active: true };
            }
        }


        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.nseIndex, nseIndexRules);
            vm.viewModelHelper.modelIsValid = vm.nseIndex.isValid;
            vm.viewModelHelper.modelErrors = vm.nseIndex.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/nseindex/updatenseindex', vm.nseIndex,
               function (result) {
                   
                   $state.go('ifrs-nseindex-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.nseIndex.errors;

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
                vm.viewModelHelper.apiPost('api/nseindex/deletenseindex', vm.nseIndex.NseIndexId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-nseindex-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-nseindex-list');
        };
 

        setupRules();
        initialize(); 
    }
}());
