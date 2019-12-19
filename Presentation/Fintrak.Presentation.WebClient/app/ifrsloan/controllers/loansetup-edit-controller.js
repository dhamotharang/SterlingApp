/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanSetupEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        LoanSetupEditController]);

    function LoanSetupEditController($scope,$window, $state,  $stateParams, viewModelHelper,validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'loansetup-edit-view';
        vm.viewName = 'Loan Setup';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.loanSetups = {};
       $scope.customStyle = {};
       vm.strength=''
       vm.status = true;
        vm.ratingtypes = [
            { Id: 1, Name: 'Individual' },
            { Id: 2, Name: 'Product' },
            { Id: 3, Name: 'Sector' }
        ];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var loansetupRules = [];

        var setupRules = function () {
          
            

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.loansetupId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/loansetup/getloansetup/' + $stateParams.loansetupId, null,
                   function (result) {
                       vm.loanSetups = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.loanSetups = { SignificantLoanMark: 0,RatingType:1, EPOption: true,EPDefault:1, Active: true };
            }
        }

   

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.loanSetups, loansetupRules);
            vm.viewModelHelper.modelIsValid = vm.loanSetups.isValid;
            vm.viewModelHelper.modelErrors = vm.loanSetups.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/loansetup/updateloansetup', vm.loanSetups,
               function (result) {
                   
                   $state.go('ifrsloan-loansetup-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.loanSetups.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
                toastr.error(errorList, 'Fintrak');
            }
                
        }

        vm.Delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/loansetup/deleteloansetup', vm.loanSetups.LoanSetupId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrsloan-loansetup-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.Cancel = function () {
            $state.go('ifrsloan-loansetup-list');
        };


        
        setupRules();
        initialize(); 
    }
}());
