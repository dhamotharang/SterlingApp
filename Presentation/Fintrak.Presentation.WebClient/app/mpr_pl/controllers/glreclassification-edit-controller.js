/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("GLReclassificationEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        GLReclassificationEditController]);

    function GLReclassificationEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'glreclassification-edit-view';
        vm.viewName = 'GL Reclassification';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.glReclassification = {};
        vm.companies = [];
        vm.captions = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var glreclassificationRules = [];

        var setupRules = function () {

            glreclassificationRules.push(new validator.PropertyRule("GLAccount", {
                required: { message: "GL Account is required" }
            }));

            glreclassificationRules.push(new validator.PropertyRule("CaptionCode", {
                required: { message: "Caption Name is required" }
            }));

            glreclassificationRules.push(new validator.PropertyRule("CompanyCode", {
                required: { message: "Company Name is required" }
            }));
                      
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.glreclassificationId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/glreclassification/getglreclassification/' + $stateParams.glreclassificationId, null,
                   function (result) {
                       vm.glReclassification = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.glReclassification = { GLAccount: '',CaptionCode: '' ,CompanyCode: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getCompanies();
            getCaptions();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.glReclassification, glreclassificationRules);
            vm.viewModelHelper.modelIsValid = vm.glReclassification.isValid;
            vm.viewModelHelper.modelErrors = vm.glReclassification.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/glreclassification/updateglreclassification', vm.glReclassification,
               function (result) {
                   
                   $state.go('mpr-glreclassification-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.glReclassification.errors;

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
                vm.viewModelHelper.apiPost('api/glreclassification/deleteglreclassification', vm.glReclassification.GLReclassificationId,//vm.glReclassification.glreclassificationId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-glreclassification-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-glreclassification-list');
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

        var getCaptions = function () {
            vm.viewModelHelper.apiGet('api/plcaption/availableplcaptions', null,
                 function (result) {
                     vm.captions = result.data;
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
