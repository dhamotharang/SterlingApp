/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MarginalCCFStrEditController",
            ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                MarginalCCFStrEditController]); // 

    function MarginalCCFStrEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {

        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MarginalCCFStr Primary Data';
        vm.view = 'marginalccfstr-edit-view';
        vm.viewName = 'MarginalCCFStr Primary Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.marginalCCFstr = {};
        vm.scheduleTypes = [];
              
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.obeType = [
            { value: 'FBG', name: 'FBG' },
            { value: 'PBG', name: 'PBG' },
            { value: 'Self Liquidating LC', name: 'Self Liquidating LC' }
        ];
        var marginalccfstrRules = [];

        var setupRules = function () {

            marginalccfstrRules.push(new validator.PropertyRule("seq", {
                required: { message: "seq is required" }
            }));

            marginalccfstrRules.push(new validator.PropertyRule("OBEType", {
                required: { message: "OBEType is required" }
            }));

            marginalccfstrRules.push(new validator.PropertyRule("MonthlyCCF", {
                notZero: { message: "MonthlyCCF is required" }
            }));

            marginalccfstrRules.push(new validator.PropertyRule("MarginalCCF", {
                notZero: { message: "MarginalCCF is required" }
            }));
            
        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

                if ($stateParams.Id !== 0)
                {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/marginalccfstr/getmarginalccfstr/' + $stateParams.Id, null,

                   function (result) {
                       vm.marginalCCFstr = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);

               }
               else
                    vm.marginalCCFstr = {
                        seq: '',
                        OBEType: '',
                        MonthlyCCF: '',
                        MarginalCCF: '',
                        Active: true
                    };
            }
        }

        var intializeLookUp = function () {
            getScheduleTypes();
            }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.marginalCCFstr, marginalccfstrRules);
            vm.viewModelHelper.modelIsValid = vm.marginalCCFstr.isValid;
            vm.viewModelHelper.modelErrors = vm.marginalCCFstr.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/marginalccfstr/updatemarginalccfstr', vm.marginalCCFstr,
               function (result) {
                   
                   $state.go('ifrs-marginalccfstr-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.marginalCCFstr.errors;

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
                vm.viewModelHelper.apiPost('api/marginalccfstr/deletemarginalccfstr', vm.marginalCCFstr.Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-marginalccfstr-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-marginalccfstr-list');
        };

        vm.openRunDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate = true;
        }
        vm.openRunDate2 = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate2 = true;
        }
        vm.openRunDate3 = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate3 = true;
        }

        var getScheduleTypes = function () {
            vm.viewModelHelper.apiGet('api/scheduletype/availablescheduletypes', null,
                 function (result) {
                     vm.scheduleTypes = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }


}());
