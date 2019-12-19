/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("GLExceptionEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        GLExceptionEditController]);

    function GLExceptionEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'glexception-edit-view';
        vm.viewName = 'GL Exception';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.glException = {};
        vm.companies = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var glexceptionRules = [];

        var setupRules = function () {

            glexceptionRules.push(new validator.PropertyRule("GLAccount", {
                required: { message: "GL Account is required" }
            }));

            glexceptionRules.push(new validator.PropertyRule("CompanyCode", {
                required: { message: "Company Name is required" }
            }));
                      
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.glexceptionId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/glexception/getglexception/' + $stateParams.glexceptionId, null,
                   function (result) {
                       vm.glException = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.glException = { GLAccount: '', CompanyCode: '', Active: true };
            }
        }

        var intializeLookUp = function () {
          getCompanies();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.glException, glexceptionRules);
            vm.viewModelHelper.modelIsValid = vm.glException.isValid;
            vm.viewModelHelper.modelErrors = vm.glException.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/glexception/updateglexception', vm.glException,
               function (result) {
                   
                   $state.go('mpr-glexception-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.glException.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }
        // vm.derivedCaption.DerivedCaptionId,
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/glexception/deleteglexception', vm.glException.glExceptionId,//vm.glException.glexceptionId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-glexception-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-glexception-list');
        };

        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                 function (result) {
                     vm.companies = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }
        setupRules();
        initialize();
    }
}());
