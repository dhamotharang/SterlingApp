/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("InstrumentTypeEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        InstrumentTypeEditController]);

    function InstrumentTypeEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'instrumenttype-edit-view';
        vm.viewName = 'Instrument Type';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.instrumentType = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.instruments = [
            { Id: 1, Name: 'Bonds' },
            { Id: 2, Name: 'TBills' },
            { Id: 3, Name: 'Loans' },
            { Id: 4, Name: 'Equity' },
            { Id: 5, Name: 'FinancialLiability' }
        ];
        vm.instrumentTypes = [];

        var instrumenttypeRules = [];

        var setupRules = function () {
          
            instrumenttypeRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            instrumenttypeRules.push(new validator.PropertyRule("Instrument", {
                notZero: { message: "Instrument is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.instrumenttypeId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/instrumenttype/getinstrumenttype/' + $stateParams.instrumenttypeId, null,
                   function (result) {
                       vm.instrumentType = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.instrumentType = { Name:'',Instrument:0, Active: true };
            }
        }

        var intializeLookUp = function () {
            getInstrumentTypes();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.instrumentType, instrumenttypeRules);
            vm.viewModelHelper.modelIsValid = vm.instrumentType.isValid;
            vm.viewModelHelper.modelErrors = vm.instrumentType.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/instrumenttype/updateinstrumenttype', vm.instrumentType,
               function (result) {
                   
                   $state.go('finstat-instrumenttype-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.instrumentType.errors;

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
                vm.viewModelHelper.apiPost('api/instrumenttype/deleteinstrumenttype', vm.instrumentType.InstrumentTypeId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-instrumenttype-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('finstat-instrumenttype-list');
        };

        var getInstrumentTypes = function () {
            vm.viewModelHelper.apiGet('api/instrumenttype/availableinstrumenttypes', null,
                 function (result) {
                     vm.instrumentTypes = result.data;
                     initialView();
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Unable to load instrument types', 'Fintrak');
                 }, null);
        }
       
        setupRules();
        initialize(); 
    }
}());
