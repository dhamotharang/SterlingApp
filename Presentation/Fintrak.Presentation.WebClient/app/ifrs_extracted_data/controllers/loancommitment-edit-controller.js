/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanCommitmentEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        LoanCommitmentEditController]);

    function LoanCommitmentEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'LoanCommitment';
        vm.view = 'loancommitment-edit-view';
        vm.viewName = 'Loan Commitment';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.loanCommitment = {};
        vm.collateralType = [];
        vm.ratings = [];

        vm.stages = [
        { Id: 1, Name: 'Stage 1' },
        { Id: 2, Name: 'Stage 2' },
        { Id: 3, Name: 'Stage 3' }
        ];
              
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

     //   vm.openedRunDate = false;
       
       
        var loancommitmentRules = [];

        var setupRules = function () {
          
            loancommitmentRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));
      
            loancommitmentRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "AccountNo is required" }
            }));

            loancommitmentRules.push(new validator.PropertyRule("ValueDate", {
                notZero: { message: "ValueDate is required" }
            }));

            loancommitmentRules.push(new validator.PropertyRule("MaturityDate", {
                notZero: { message: "MaturityDate is required" }
            }));
            
            loancommitmentRules.push(new validator.PropertyRule("Amount", {
                required: { message: "Amount is required" }
            }));

            loancommitmentRules.push(new validator.PropertyRule("Rate", {
                notZero: { message: "Rate is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

               if ($stateParams.loanCommitmentId !== 0)
                {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/loanCommitment/getloanCommitment/' + $stateParams.loanCommitmentId, null,
                   function (result) {
                       vm.loanCommitment = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.loanCommitment = { RefNo: '', AccountNo: '', ValueDate: '', MaturityDate: '',  Rate: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getAllCollateralTypes();
            getRatings();
            }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.loanCommitment, loancommitmentRules);
            vm.viewModelHelper.modelIsValid = vm.loanCommitment.isValid;
            vm.viewModelHelper.modelErrors = vm.loanCommitment.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/loanCommitment/updateloanCommitment', vm.loanCommitment,
               function (result) {
                   
                   $state.go('ifrs-loancommitment-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.loanCommitment.errors;

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
                vm.viewModelHelper.apiPost('api/loanCommitment/deleteloanCommitment', vm.loanCommitment.LoanCommitmentId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-loancommitment-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-loancommitment-list');
        };

        var getAllCollateralTypes = function () {
            vm.viewModelHelper.apiGet('api/collateraltype/availablecollateralTypes/', null,
                   function (result) {
                       vm.collateralType = result.data;
                       vm.init === true;
                       //toastr.success('Collateral Types data loaded, ready for modifiation.', 'Fintrak');
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
        }

        var getRatings = function () {
            vm.viewModelHelper.apiGet('api/creditriskrating/availablecreditRiskRating', null,
                function (result) {
                    vm.ratings = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

       // vm.openRunDate = function ($event) {
        //    $event.preventDefault();
        //    $event.stopPropagation();
       //     vm.openedRunDate = true;
       // }
       // vm.openRunDate2 = function ($event) {
       //     $event.preventDefault();
        //    $event.stopPropagation();
        //    vm.openedRunDate2 = true;
       // }
        //vm.openRunDate3 = function ($event) {
        //    $event.preventDefault();
        //    $event.stopPropagation();
        //    vm.openedRunDate3 = true;
       // }

       // var getScheduleTypes = function () {
        //    vm.viewModelHelper.apiGet('api/scheduletype/availablescheduletypes', null,
        //         function (result) {
        //             vm.scheduleTypes = result.data;
        //         },
        //         function (result) {
        //             toastr.error(result.data, 'Fintrak');
        //         }, null);
       // }

        setupRules();
        initialize(); 
    }
}());
