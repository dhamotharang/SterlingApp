/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SPCumulativePDEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SPCumulativePDEditController]);

    function SPCumulativePDEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'spcumulativepd-edit-view';
        vm.viewName = 'S&P Cummulative PD';
        vm.status = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.sPCumulativePD = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var spcumulativepdRules = [];

        var setupRules = function () {

            spcumulativepdRules.push(new validator.PropertyRule("Rating", {
                required: { message: "Rating is required" }
            }));

            spcumulativepdRules.push(new validator.PropertyRule("Years", {
                required: { message: "Years is required" }
            }));

            spcumulativepdRules.push(new validator.PropertyRule("Value", {
                required: { message: "Value is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.SPCumulative_Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/spcumulativepd/getspcumulativepd/' + $stateParams.SPCumulative_Id, null,
                   function (result) {
                       vm.sPCumulativePD = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.sPCumulativePD = { Year: '', Rating: '', Value: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.sPCumulativePD, spcumulativepdRules);
            vm.viewModelHelper.modelIsValid = vm.sPCumulativePD.isValid;
            vm.viewModelHelper.modelErrors = vm.sPCumulativePD.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/spcumulativepd/updatespcumulativepd', vm.sPCumulativePD,
               function (result) {
                   
                   $state.go('ifrs9-spcumulativepd-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.sPCumulativePD.errors;

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
                vm.viewModelHelper.apiPost('api/spcumulativepd/deletespcumulativepd', vm.sPCumulativePD.SPCumulative_Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-spcumulativepd-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-spcumulativepd-list');
        };

        
        setupRules();
        initialize(); 
    }
}());
