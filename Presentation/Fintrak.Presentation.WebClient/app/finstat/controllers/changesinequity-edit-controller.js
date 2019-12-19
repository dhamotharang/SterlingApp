/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ChangesInEquityEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ChangesInEquityEditController]);

    function ChangesInEquityEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Finstat Core';
        vm.view = 'changesinequity-edit-view';
        vm.viewName = 'Changes In Equity';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.changesInEquity = {};

        //vm.mainCaptions = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

      
        var changesInEquityRules = [];

        var setupRules = function () {
          
            changesInEquityRules.push(new validator.PropertyRule("Rundate", {
                required: { message: "Rundate is required" }
            }));

            changesInEquityRules.push(new validator.PropertyRule("Caption", {
                required: { message: "Caption is required" }
            }));

            changesInEquityRules.push(new validator.PropertyRule("ShareCapital", {
                required: { message: "Share Capital is required" }
            }));

            changesInEquityRules.push(new validator.PropertyRule("SharePremium", {
                required: { message: "Share Premium is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.changesinequityId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/changesinequity/getchangesinequity/' + $stateParams.changesinequityId, null,
                   function (result) {
                       vm.changesInEquity = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.changesInEquity = { Rundate: '', Caption: '',ShareCapital:'', SharePremium: '',PropertyRevaluationReserve: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            //getMainCaptions();
        }

        var initialView = function () {
            
        }
        
        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.changesInEquity, changesInEquityRules);
            vm.viewModelHelper.modelIsValid = vm.changesInEquity.isValid;
            vm.viewModelHelper.modelErrors = vm.changesInEquity.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/changesinequity/updatechangesinequity', vm.changesInEquity,
               function (result) {
                   
                   $state.go('finstat-changesinequity-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.changesInEquity.errors;

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
                vm.viewModelHelper.apiPost('api/changesinequity/deletechangesinequity', vm.changesInEquity.ChangesInEquityId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-changesinequity-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('finstat-changesinequity-list');
        };

        //vm.onFinTypeChanged = function (finType) {
        //    getFinSubTypes(finType);
        //}

        //var getMainCaptions = function () {
        //    vm.viewModelHelper.apiGet('api/registry/getmainCaptions', null,
        //         function (result) {
        //             vm.mainCaptions = result.data;
        //         },
        //         function (result) {
        //             toastr.error('Fail to load main captions.', 'Fintrak');
        //         }, null);
       // }
        
        //var getCaptions = function () {
        //    vm.viewModelHelper.apiGet('api/changesinequity/availableifrsBudgets', null,
        //         function (result) {
        //             vm.parents = result.data;
        //         },
        //         function (result) {
        //             toastr.error('Unable to load parents', 'Fintrak');
        //         }, null);
        //}
        setupRules();
        initialize(); 
    }
}());
