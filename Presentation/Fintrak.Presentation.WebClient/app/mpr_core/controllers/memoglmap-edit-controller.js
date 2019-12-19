/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MemoGLMapEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MemoGLMapEditController]);

    function MemoGLMapEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'memoglmap-edit-view';
        vm.viewName = 'Memo GLs';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.memoGLMap = {};
        vm.memos = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var memoglmapRules = [];

        var setupRules = function () {

            memoglmapRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            memoglmapRules.push(new validator.PropertyRule("GLNo", {
                required: { message: "GL No is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.memoglmapId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/memoglmap/getmemoglmap/' + $stateParams.memoglmapId, null,
                   function (result) {
                       vm.memoGLMap = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.memoGLMap = { Code:'',GLNo:'', Active: true };
            }
        }

        var intializeLookUp = function () {
            getMemos(1);
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.memoGLMap, memoglmapRules);
            vm.viewModelHelper.modelIsValid = vm.memoGLMap.isValid;
            vm.viewModelHelper.modelErrors = vm.memoGLMap.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/memoglmap/updatememoglmap', vm.memoGLMap,
               function (result) {
                   
                   $state.go('mpr-memoglmap-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.memoGLMap.errors;

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
                vm.viewModelHelper.apiPost('api/memoglmap/deletememoglmap', vm.memoGLMap.MemoGLMapId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-memoglmap-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-memoglmap-list');
        };

        var getMemos = function () {
            vm.viewModelHelper.apiGet('api/memounits/getmemounits/', null,
                 function (result) {
                     vm.memos = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load memos.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
