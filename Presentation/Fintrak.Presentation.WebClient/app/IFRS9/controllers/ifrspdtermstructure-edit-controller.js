/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsPdTermStructureEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IfrsPdTermStructureEditController]);

    function IfrsPdTermStructureEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrspdtermstructure-edit-view';
        vm.viewName = 'S&P Pd Term Structure'

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

       
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ifrsPdTermStructureRules = [];

        var setupRules = function () {

            ifrsPdTermStructureRules.push(new validator.PropertyRule("Rating", {
                required: { message: "Rating required" }
            }));

            ifrsPdTermStructureRules.push(new validator.PropertyRule("_1", {
                required: { message: "PD for year 1 required" }
            }));

            ifrsPdTermStructureRules.push(new validator.PropertyRule("_2", {
                required: { message: "PD for year 2 required" }
            }));

            ifrsPdTermStructureRules.push(new validator.PropertyRule("_3", {
                required: { message: "PD for year 3 required" }
            }));

            ifrsPdTermStructureRules.push(new validator.PropertyRule("_4", {
                required: { message: "PD for year 4 required" }
            }));

            ifrsPdTermStructureRules.push(new validator.PropertyRule("_5", {
                required: { message: "PD for year 5 required" }
            }));

            ifrsPdTermStructureRules.push(new validator.PropertyRule("_6", {
                required: { message: "PD for year 6 required" }
            }));

            ifrsPdTermStructureRules.push(new validator.PropertyRule("_7", {
                required: { message: "PD for year 7 required" }
            }));

            ifrsPdTermStructureRules.push(new validator.PropertyRule("_8", {
                required: { message: "PD for year 8 required" }
            }));

            ifrsPdTermStructureRules.push(new validator.PropertyRule("_9", {
                required: { message: "PD for year 9 required" }
            }));

            ifrsPdTermStructureRules.push(new validator.PropertyRule("_10", {
                required: { message: "PD for year 10 required" }
            }));

            ifrsPdTermStructureRules.push(new validator.PropertyRule("_11", {
                required: { message: "PD for year 11 required" }
            }));

            ifrsPdTermStructureRules.push(new validator.PropertyRule("_12", {
                required: { message: "PD for year 12 required" }
            }));

            ifrsPdTermStructureRules.push(new validator.PropertyRule("_13", {
                required: { message: "PD for year 13 required" }
            }));

            ifrsPdTermStructureRules.push(new validator.PropertyRule("_14", {
                required: { message: "PD for year 14 required" }
            }));

            ifrsPdTermStructureRules.push(new validator.PropertyRule("_15", {
                required: { message: "PD for year 15 required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.Id !== 0) {
                    vm.showChildren = true;
                  vm.viewModelHelper.apiGet('api/IfrsPdTermStructure/getIfrsPdTermStructure/' + $stateParams.Id, null,
                   function (result) {
                       vm.IfrsPdTermStructure = result.data;

                      initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.ifrsPdTermStructureRules = { Source: '',  Description: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.IfrsPdTermStructure, ifrsPdTermStructureRules);
            vm.viewModelHelper.modelIsValid = vm.IfrsPdTermStructure.isValid;
            vm.viewModelHelper.modelErrors = vm.IfrsPdTermStructure.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
              vm.viewModelHelper.apiPost('api/IfrsPdTermStructure/updateIfrsPdTermStructure', vm.IfrsPdTermStructure,
               function (result) {
                   
                   $state.go('ifrspdtermstructure-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.IfrsPdTermStructure.errors;

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
              vm.viewModelHelper.apiPost('api/IfrsPdTermStructure/deleteIfrsPdTermStructure', vm.IfrsPdTermStructure.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrspdtermstructure-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrspdtermstructure-list');
        };

        setupRules();
        initialize(); 
    }
}());
