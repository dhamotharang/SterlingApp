/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("InputDetailEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        InputDetailEditController]);

    function InputDetailEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'InputDetail';
        vm.view = 'inputdetail-edit-view';
        vm.viewName = 'ECL Simulation Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.inputDetail = {};
        vm.collateralType = [];
        vm.stages = [
            { Id: 1, Name: 'Stage 1' },
            { Id: 2, Name: 'Stage 2' },
            { Id: 3, Name: 'Stage 3' }
        ];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

 
        var inputDetailRules = [];

        var setupRules = function () {
          
            inputDetailRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));

            inputDetailRules.push(new validator.PropertyRule("Rating", {
                required: { message: "Rating is required" }
            }));

            //inputDetailRules.push(new validator.PropertyRule("CollateralValue", {
            //    notZero: { message: "CollateralValue is required" }
            //}));

            inputDetailRules.push(new validator.PropertyRule("Stage", {
                required: { message: "Stage is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.inputDetailId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/inputdetail/getinputdetail/' + $stateParams.inputDetailId, null,
                   function (result) {
                       vm.inputDetail = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.inputDetail = { RefNo: '', Rating: '', CollateralValue: '', Stage: '', Active: true };
            }
        }


        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.inputDetail, inputDetailRules);
            vm.viewModelHelper.modelIsValid = vm.inputDetail.isValid;
            vm.viewModelHelper.modelErrors = vm.inputDetail.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/inputdetail/updateinputdetail', vm.inputDetail,
               function (result) {
                   
                   $state.go('ifrs-inputdetail-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.inputDetail.errors;

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
                vm.viewModelHelper.apiPost('api/inputdetail/deleteinputdetail', vm.inputDetail.InputDetailId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-inputdetail-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-inputdetail-list');
        };

        var getAllCollateralTypes = function () {
            vm.viewModelHelper.apiGet('api/collateraltype/availablecollateralTypes/', null,
                function (result) {
                    vm.collateralType = result.data;
                    vm.init === true;
                    toastr.success('Collateral Types data loaded, ready for modifiation.', 'Fintrak');
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        var intializeLookUp = function () {
            getAllCollateralTypes();

        }
 

        setupRules();
        initialize(); 
    }
}());
