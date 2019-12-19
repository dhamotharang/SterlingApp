/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ActivityEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ActivityEditController]);

    function ActivityEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'activity-edit-view';
        vm.viewName = 'Activity';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.activity = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var activityRules = [];

        var setupRules = function () {
          
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initialLookUp();

                if ($stateParams.activityId !== 0) {
                    vm.showPeriod = true;
                    vm.viewModelHelper.apiGet('api/activity/getactivity/' + $stateParams.activityId, null,
                   function (result) {
                       vm.activity = result.data;

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
              
            }
        }

        var initialLookUp = function () {
            
        }

        var initialView = function () {
  
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/activity/deleteactivity', vm.activity.ActivityId,
              function (result) {
                  
                  $state.go('core-activity-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-activity-list');
        };

        setupRules();
        initialize(); 
    }
}());
