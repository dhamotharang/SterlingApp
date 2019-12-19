/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("KPIClassificationEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        KPIClassificationEditController]);

    function KPIClassificationEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Scorecard';
        vm.view = 'kpiclassification-edit-view';
        vm.viewName = 'KPI Collection To Matrix Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.classification = {};

        vm.kpis = [];
        vm.classifications = [];
        vm.categories = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var classificationRules = [];

        var setupRules = function () {
          
            classificationRules.push(new validator.PropertyRule("Period", {
                notZero: { message: "Period is required" }
            }));

            classificationRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            classificationRules.push(new validator.PropertyRule("KPICode", {
                required: { message: "KPI Code is required" }
            }));

            classificationRules.push(new validator.PropertyRule("TeamClassificationCode", {
                required: { message: "Team Classification Code is required" }
            }));

             classificationRules.push(new validator.PropertyRule("CategoryCode", {
                required: { message: "Category is required" }
            }));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.classificationId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/scdkpiclassification/getclassification/' + $stateParams.classificationId, null,
                   function (result) {
                       vm.classification = result.data;

                       initialView();
                       vm.init = true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.classification = { Period: 0, Year: '',KPICode:'',TeamClassificationCode:'', CategoryCode:'' ,Active: true };
            }
        }


        var intializeLookUp = function () {
            getKPIs();
            getClassifications();
            getCategories();
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.classification, classificationRules);
            vm.viewModelHelper.modelIsValid = vm.classification.isValid;
            vm.viewModelHelper.modelErrors = vm.classification.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/scdkpiclassification/updateclassification', vm.classification,
               function (result) {
                   
                   $state.go('scd-classification-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.classification.errors;

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
                vm.viewModelHelper.apiPost('api/scdkpiclassification/deleteclassification', vm.classification.ClassificationId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('scd-classification-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('scd-classification-list');
        };

        var getKPIs = function () {
            vm.viewModelHelper.apiGet('api/scdkpi/availablekpi', null,
                 function (result) {
                     vm.kpis = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Fail to load metrics.', 'Fintrak');
                 }, null);
        }

        var getClassifications = function () {
            vm.viewModelHelper.apiGet('api/scdteamclassification/availableteamclassification', null,
                 function (result) {
                     vm.classifications = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Fail to load team classification.', 'Fintrak');
                 }, null);
        }

         var getCategories = function () {
            vm.viewModelHelper.apiGet('api/category/availablecategory', null,
                 function (result) {
                     vm.categories = result.data;
                   
                 },
                 function (result) {
                     toastr.error('Fail to load kpi categories.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
