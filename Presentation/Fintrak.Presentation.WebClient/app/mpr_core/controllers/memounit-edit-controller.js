/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MemoUnitEditController", 
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MemoUnitEditController]);

    function MemoUnitEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'memounits-edit-view';
        vm.viewName = 'Memo Units';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.memoUnits = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var memounitsRules = [];

        var setupRules = function () {

            memounitsRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code No is required" }
            }));

            memounitsRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.memounitId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/memounit/getmemounit/' + $stateParams.memounitId, null,
                   function (result) {
                       vm.memoUnits = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.memoUnits = { GL_Code :'',Description:'',CompanyCode:'', Active: true };
            }
        }

        //var intializeLookUp = function () {
        //    getTeams(1);
        //}

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.memoUnits, memounitsRules);
            vm.viewModelHelper.modelIsValid = vm.memoUnits.isValid;
            vm.viewModelHelper.modelErrors = vm.memoUnits.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/memounit/updatememounit', vm.memoUnits,
               function (result) {
                   
                   $state.go('mpr-memounit-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.memoUnits.errors;

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
                vm.viewModelHelper.apiPost('api/memounit/deletememounit', vm.memoUnits.MemoUnitsId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-memounit-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-memounit-list');
        };

       

        setupRules();
        initialize(); 
    }
}());
