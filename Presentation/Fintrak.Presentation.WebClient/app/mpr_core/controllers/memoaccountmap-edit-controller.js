/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MemoAccountMapEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MemoAccountMapEditController]);

    function MemoAccountMapEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'memoaccountmap-edit-view';
        vm.viewName = 'Memo Accounts';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.memoAccountMap = {};
        vm.memos = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var memoaccountmapRules = [];

        var setupRules = function () {

            memoaccountmapRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            memoaccountmapRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "Account No is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.memoaccountmapId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/memoaccountmap/getmemoaccountmap/' + $stateParams.memoaccountmapId, null,
                   function (result) {
                       vm.memoAccountMap = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.memoAccountMap = { Code:'',AccountNo:'', Active: true };
            }
        }

        var intializeLookUp = function () {
            getMemos(1);
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.memoAccountMap, memoaccountmapRules);
            vm.viewModelHelper.modelIsValid = vm.memoAccountMap.isValid;
            vm.viewModelHelper.modelErrors = vm.memoAccountMap.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/memoaccountmap/updatememoaccountmap', vm.memoAccountMap,
               function (result) {
                   
                   $state.go('mpr-memoaccountmap-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.memoAccountMap.errors;

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
                vm.viewModelHelper.apiPost('api/memoaccountmap/deletememoaccountmap', vm.memoAccountMap.MemoAccountMapId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-memoaccountmap-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-memoaccountmap-list');
        };

        var getMemos = function () {
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
