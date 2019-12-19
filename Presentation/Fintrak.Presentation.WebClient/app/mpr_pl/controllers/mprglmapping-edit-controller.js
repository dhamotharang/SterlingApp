/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MPRGLMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MPRGLMappingEditController]);

    function MPRGLMappingEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR-PL';
        vm.view = 'mprglmapping-edit-view';
        vm.viewName = 'GL Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.mprglMapping = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.companies = [];      
        vm.captions = [];
        var mprglmappingRules = [];

        var setupRules = function () {
          
            mprglmappingRules.push(new validator.PropertyRule("GLCode", {
                required: { message: "GLCode is required" }
            }));
  
            mprglmappingRules.push(new validator.PropertyRule("CaptionCode", {
                required: { message: "Caption is required" }
            }));          

            mprglmappingRules.push(new validator.PropertyRule("CompanyCode", {
                required: { message: "Company is required" }
            }));

            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.mprglmappingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/mprglmapping/getmprglmapping/' + $stateParams.mprglmappingId, null,
                   function (result) {
                       vm.mprglMapping = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.mprglMapping = { GLCode: '',CaptionCode:'',CompanyCode: '', Active: true };
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
            validator.ValidateModel(vm.mprglMapping, mprglmappingRules);
            vm.viewModelHelper.modelIsValid = vm.mprglMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.mprglMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/mprglmapping/updatemprglmapping', vm.mprglMapping,
               function (result) {
                   
                   $state.go('mpr-mprglmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.mprglMapping.errors;

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
                vm.viewModelHelper.apiPost('api/mprglmapping/deletemprglmapping', vm.mprglMapping.MPRGLMappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-mprglmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-mprglmapping-list');
        };

        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                 function (result) {
                     vm.companies = result.data;
                     initialView();
                     vm.init === true;

                 },
                 function (result) {

                 }, null);
        }

        var getCaptions = function () {
            vm.viewModelHelper.apiGet('api/plcaption/availableplcaptions', null,
                 function (result) {
                     vm.captions = result.data;
                 },
                 function (result) {

                 }, null);
        }

       
        setupRules();
        initialize(); 
    }
}());
