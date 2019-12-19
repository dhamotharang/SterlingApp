/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MemoProductMapEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MemoProductMapEditController]);

    function MemoProductMapEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'memoproductmap-edit-view';
        vm.viewName = 'Memo Products';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.memoProductMap = {};
        vm.memos = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var memoproductmapRules = [];

        var setupRules = function () {

            memoproductmapRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            memoproductmapRules.push(new validator.PropertyRule("ProductCode", {
                required: { message: "Product No is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.memoproductmapId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/memoproductmap/getmemoproductmaps/' + $stateParams.memoproductmapId, null,
                   function (result) {
                       vm.memoProductMap = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.memoProductMap = { Code:'',ProductCode:'', Active: true };
            }
        }

        var intializeLookUp = function () {
            getMemos(1);
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.memoProductMap, memoproductmapRules);
            vm.viewModelHelper.modelIsValid = vm.memoProductMap.isValid;
            vm.viewModelHelper.modelErrors = vm.memoProductMap.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/memoproductmap/updatememoproductmaps', vm.memoProductMap,
               function (result) {
                   
                   $state.go('mpr-memoproductmap-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.memoProductMap.errors;

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
                vm.viewModelHelper.apiPost('api/memoproductmap/deletememoproductmaps', vm.memoProductMap.MemoProductMapId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-memoproductmap-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-memoproductmap-list');
        };

        var getMemos = function() {
            vm.viewModelHelper.apiGet('api/memounit/availablememounits/', null,
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
