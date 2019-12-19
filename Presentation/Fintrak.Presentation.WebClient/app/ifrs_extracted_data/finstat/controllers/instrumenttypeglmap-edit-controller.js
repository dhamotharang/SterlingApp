/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("InstrumentTypeGLMapEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        InstrumentTypeGLMapEditController]);

    function InstrumentTypeGLMapEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Finstat';
        vm.view = 'instrumenttypeglmap-edit-view';
        vm.viewName = 'Instrument GL Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.instrumentTypeGLMap = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.instrumentTypes = [];
        vm.glTypes = [];
        vm.companies =[];
        vm.accounts = [];

        var instrumenttypeglmapRules = [];

        var setupRules = function () {
          
            instrumenttypeglmapRules.push(new validator.PropertyRule("InstrumentTypeId", {
                notZero: { message: "Instrument Type is required" }
            }));

            instrumenttypeglmapRules.push(new validator.PropertyRule("GLTypeId", {
                notZero: { message: "GL Type is required" }
            }));

            instrumenttypeglmapRules.push(new validator.PropertyRule("CompanyCode", {
                required: { message: "Company is required" }
            }));

            instrumenttypeglmapRules.push(new validator.PropertyRule("GLCode", {
                required: { message: "GL is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.instrumenttypeglmapId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/instrumenttypeglmap/getinstrumenttypeglmap/' + $stateParams.instrumenttypeglmapId, null,
                   function (result) {
                       vm.instrumentTypeGLMap = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.instrumentTypeGLMap = { InstrumentTypeId:0,GLTypeId: 0, GLCode: '',CompanyId:0, Active: true };
            }
        }

        var intializeLookUp = function () {
            getInstrumentTypes();
            getGLTypes();
            getAccounts();
            getCompanies();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.instrumentTypeGLMap, instrumenttypeglmapRules);
            vm.viewModelHelper.modelIsValid = vm.instrumentTypeGLMap.isValid;
            vm.viewModelHelper.modelErrors = vm.instrumentTypeGLMap.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/instrumenttypeglmap/updateinstrumenttypeglmap', vm.instrumentTypeGLMap,
               function (result) {
                   
                   $state.go('finstat-instrumenttypeglmap-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.instrumentTypeGLMap.errors;

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
                vm.viewModelHelper.apiPost('api/instrumenttypeglmap/deleteinstrumenttypeglmap', vm.instrumentTypeGLMap.InstrumentTypeMapId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-instrumenttypeglmap-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('finstat-instrumenttypeglmap-list');
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

        var getGLTypes = function () {
            vm.viewModelHelper.apiGet('api/gltype/availablegltypes', null,
                 function (result) {
                     vm.glTypes = result.data;
                     initialView();
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Unable to load gl types', 'Fintrak');
                 }, null);
        }

        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                 function (result) {
                     vm.companies = result.data;
                     initialView();
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Unable to load companies', 'Fintrak');
                 }, null);
        }

        var getAccounts = function () {
            vm.viewModelHelper.apiGet('api/glmapping/availableglMappings', null,
                 function (result) {
                     vm.accounts = result.data;
                 },
                 function (result) {
                     toastr.error('Unable to load GLs', 'Fintrak');
                 }, null);
        }
       
        setupRules();
        initialize(); 
    }
}());
