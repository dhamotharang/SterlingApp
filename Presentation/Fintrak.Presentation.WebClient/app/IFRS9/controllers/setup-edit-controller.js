/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SetUpEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SetUpEditController]);

    function SetUpEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'setup-edit-view';
        vm.viewName = 'SetUp';
        vm.status = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.setUp = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ifrs9setupRules = [];
       



        vm.Types = [
            { Id: 1, Name: 'Number' },
            { Id: 2, Name: 'Text' },
            { Id: 3, Name: 'Date' },
        ];


        vm.groupBased = [
           { Id: 1, Name: 'Sector' },
           { Id: 2, Name: 'Loan Type' },
          
        ];
        vm.ltpdapproach = [
            { Id: 1, Name: 'Binomial Tree Approach' },
            { Id: 2, Name: 'Markov Chain Approach' },
        ];
        var setupRules = function () {
          
            //ifrs9setupRules.push(new validator.PropertyRule("Threshold", {
            //    required: { message: "Threshold is required" }
            //}));

            //ifrs9setupRules.push(new validator.PropertyRule("Deteroriation_Level", {
            //    required: { message: "Deteroriation Level is required" }
            //}));

            //ifrs9setupRules.push(new validator.PropertyRule("Classification_Type", {
            //    required: { message: "Classification Type is required" }
            //}));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.setupId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/setup/getsetup/' + $stateParams.setupId, null,
                   function (result) {
                       vm.setUp = result.data;
                       if (vm.setUp.Type == 'Number') {
                           vm.setUp.Value = parseFloat(vm.setUp.Value)
                       }
                       else if (vm.setUp.Type == 'Date') {
                           vm.setUp.Value = new Date(vm.setUp.Value)
                       }
                       var classid = result.data.Classification_Type;
                       if (classid === 1) {
                           vm.status = false;
                       }
                       else {
                           vm.status = true;
                       }
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.setUp = { Threshold: 0, Deteroriation_Level: 0, Classification_Type: 0, Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.setUp, ifrs9setupRules);
            vm.viewModelHelper.modelIsValid = vm.setUp.isValid;
            vm.viewModelHelper.modelErrors = vm.setUp.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/setup/updatesetup', vm.setUp,
               function (result) {
                   
                   $state.go('ifrs9-setup-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.setUp.errors;

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
                vm.viewModelHelper.apiPost('api/setup/deletesetup', vm.setUp.SetUpId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-setup-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-setup-list');
        };

        vm.makevisible = function (classid) {

            if (classid === 1) {
                vm.status = false;
            }
            else
            {
                vm.status = true;
            }

        }
        setupRules();
        initialize(); 
    }
}());
