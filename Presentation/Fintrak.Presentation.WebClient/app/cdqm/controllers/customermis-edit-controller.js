/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CustomerMISEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CustomerMISEditController]);

    function CustomerMISEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'CDQM';
        vm.view = 'customermis-edit-view';
        vm.viewName = 'Customer MIS';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.customerMIS = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var customerMISRules = [];

        var setupRules = function () {
          
            
            //customerMISRules.push(new validator.PropertyRule("StreetName", {
            //    required: { message: "Street Name is required" }
            //}));

          

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.customermisId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/cdqmcustomerMIS/getcdqmcustomerMIS/' + $stateParams.customermisId, null,
                   function (result) {
                       vm.customerMIS = result.data;

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.customerMIS = { Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.customerMIS, customerMISRules);
            vm.viewModelHelper.modelIsValid = vm.customerMIS.isValid;
            vm.viewModelHelper.modelErrors = vm.customerMIS.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/cdqmcustomerMIS/updatecdqmcustomerMIS', vm.customerMIS,
               function (result) {
                   
                   $state.go('cdqm-customermis-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.customerMIS.errors;

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
                vm.viewModelHelper.apiPost('api/cdqmcustomerMIS/deletecdqmcustomerMIS', vm.customerMIS.CustomerMISId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('cdqm-customermis-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('cdqm-customermis-list');
        };

        setupRules();
        initialize(); 
    }
}());
