/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("AutoPostingTemplateEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        AutoPostingTemplateEditController]);

    function AutoPostingTemplateEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'autopostingtemplate-edit-view';
        vm.viewName = 'Auto Posting Template';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.autoPostingTemplate = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var autopostingtemplateRules = [];

        var setupRules = function () {
          
            autopostingtemplateRules.push(new validator.PropertyRule("Title", {
                required: { message: "Title is required" }
            }));

            autopostingtemplateRules.push(new validator.PropertyRule("Action", {
                required: { message: "Action is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.autopostingtemplateId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/autopostingtemplate/getautopostingtemplate/' + $stateParams.autopostingtemplateId, null,
                   function (result) {
                       vm.autoPostingTemplate = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.autoPostingTemplate = { Title:'',Action:'', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.autoPostingTemplate, autopostingtemplateRules);
            vm.viewModelHelper.modelIsValid = vm.autoPostingTemplate.isValid;
            vm.viewModelHelper.modelErrors = vm.autoPostingTemplate.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/autopostingtemplate/updateautopostingtemplate', vm.autoPostingTemplate,
               function (result) {
                   
                   $state.go('finstat-autopostingtemplate-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.autoPostingTemplate.errors;

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
                vm.viewModelHelper.apiPost('api/autopostingtemplate/deleteautopostingtemplate', vm.autoPostingTemplate.AutoPostingTemplateId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-autopostingtemplate-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('finstat-autopostingtemplate-list');
        };

        setupRules();
        initialize(); 
    }
}());
