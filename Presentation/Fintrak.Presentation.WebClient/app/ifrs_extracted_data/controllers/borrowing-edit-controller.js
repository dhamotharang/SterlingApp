/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BorrowingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        BorrowingEditController]);

    function BorrowingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Borrowings Primary Data';
        vm.view = 'borrowing-edit-view';
        vm.viewName = 'Borrowings Primary Data Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.borrowing = {};       
              
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
       
        var borrowingRules = [];

        var setupRules = function () {
          
            borrowingRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));
      
            borrowingRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "AccountNo is required" }
            }));

            borrowingRules.push(new validator.PropertyRule("ValueDate", {
                notZero: { message: "ValueDate is required" }
            }));

            borrowingRules.push(new validator.PropertyRule("MaturityDate", {
                notZero: { message: "MaturityDate is required" }
            }));
            
            borrowingRules.push(new validator.PropertyRule("Amount", {
                required: { message: "Amount is required" }
            }));

            borrowingRules.push(new validator.PropertyRule("Rate", {
                notZero: { message: "Rate is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

                if ($stateParams.borrowingId !== 0)
                {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/borrowing/getborrowing/' + $stateParams.borrowingId, null,
                   function (result) {
                       vm.borrowing = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.borrowing = { RefNo: '', AccountNo: '', ValueDate: '', MaturityDate: '',  Rate: '', Active: true };
            }
        }

        var intializeLookUp = function () {
          }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.borrowing, borrowingRules);
            vm.viewModelHelper.modelIsValid = vm.borrowing.isValid;
            vm.viewModelHelper.modelErrors = vm.borrowing.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/borrowing/updateborrowing', vm.borrowing,
               function (result) {
                   
                   $state.go('ifrs-borrowingdata-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.borrowing.errors;

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
                vm.viewModelHelper.apiPost('api/borrowing/deleteborrowing', vm.borrowing.BorrowingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-borrowingdata-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-borrowingdata-list');
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

        setupRules();
        initialize(); 
    }
}());
