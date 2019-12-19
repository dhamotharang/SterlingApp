/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TitleEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        TitleEditController]);

    function TitleEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'CDQM';
        vm.view = 'title-edit-view';
        vm.viewName = 'Title';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.title = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var titleRules = [];

        var setupRules = function () {
          
            
            //titleRules.push(new validator.PropertyRule("StreetName", {
            //    required: { message: "Street Name is required" }
            //}));

          

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.titleId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/cdqmtitle/getcdqmtitle/' + $stateParams.titleId, null,
                   function (result) {
                       vm.title = result.data;

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.title = { Valid: '', Invalid: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.title, titleRules);
            vm.viewModelHelper.modelIsValid = vm.title.isValid;
            vm.viewModelHelper.modelErrors = vm.title.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/cdqmtitle/updatecdqmtitle', vm.title,
               function (result) {
                   
                   $state.go('cdqm-title-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.title.errors;

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
                vm.viewModelHelper.apiPost('api/cdqmtitle/deletecdqmtitle', vm.title.TitleId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('cdqm-title-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('cdqm-title-list');
        };

        setupRules();
        initialize(); 
    }
}());
