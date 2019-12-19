/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanDetailEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        LoanDetailEditController]);

    function LoanDetailEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Loan Detail Data';
        vm.view = 'loandetail-edit-view';
        vm.viewName = 'Loan Details Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.loanDetail = {};
        vm.scheduleTypes = [];
        vm.classification = '';
        vm.subClassification = '';
        vm.collateralType = [];
        //vm.collateralRecovCompute = {};
        vm.sectors = [];
        vm.ratings = [];
        vm.notes = '';
        vm.oldStage = '';
        vm.takeNote = false;
        vm.showPrompt = false;
              
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
       
        vm.classifications = [
            {id: 1, Name: 'PERFORMING'},
            { id: 2, Name: 'NON-PERFORMING' }
        ];

        vm.stages = [
           { id: 1, Name: 'Stage 1' },           
           { id: 2, Name: 'Stage 2' },
           { id: 3, Name: 'Stage 3' },
        ];
       
        vm.subClassifications = [
            { id: 1, Name: 'STANDARD' },
            { id: 2, Name: 'SUB-STANDARD' },
            { id: 3, Name: 'WATCHLIST' },
            { id: 4, Name: 'DOUBTFUL' },
            { id: 5, Name: 'LOST' },
        ];


        var loandetailRules = [];

        var setupRules = function () {
          
            loandetailRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));
      
            loandetailRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "AccountNo is required" }
            }));

            loandetailRules.push(new validator.PropertyRule("ValueDate", {
                notZero: { message: "Value Date is required" }
            }));

            loandetailRules.push(new validator.PropertyRule("MaturityDate", {
                notZero: { message: "Maturity Date is required" }
            }));
            
            loandetailRules.push(new validator.PropertyRule("Amount", {
                required: { message: "Amount is required" }
            }));

            loandetailRules.push(new validator.PropertyRule("Rate", {
                required: { message: "Rate is required" }
            }));

            //loandetailRules.push(new validator.PropertyRule("Rate", {
            //  notZero: { message: "Rate is required" }
            //}));
            
            loandetailRules.push(new validator.PropertyRule("Classification", {
                required: { message: "Classification is required" }
            }));
            
            loandetailRules.push(new validator.PropertyRule("SubClassification", {
                required: { message: "Sub Classification is required" }
            }));
            
            loandetailRules.push(new validator.PropertyRule("ProductCode", {
                required: { message: "Product Code is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

                if ($stateParams.loanDetailId !== 0)
                {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/rawloandetail/getrawloandetail/' + $stateParams.loanDetailId, null,
                   function (result) {
                       vm.loanDetail = result.data;
                       vm.oldStage = vm.loanDetail.Stage;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.loanDetail = { RefNo: '', AccountNo: '', ValueDate: '', MaturityDate: '',  Rate: '', Active: true,CollateralValue:0,ProductCategory:'LOANS' };
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
            validator.ValidateModel(vm.loanDetail, loandetailRules);
            vm.viewModelHelper.modelIsValid = vm.loanDetail.isValid;
            vm.viewModelHelper.modelErrors = vm.loanDetail.errors;
            if (vm.viewModelHelper.modelIsValid) {

                if (vm.notes === '' && vm.takeNote === true && vm.oldStage !== '' && vm.oldStage !== vm.loanDetail.Stage) {
                    toastr.warning('Please provide a reason for changing the loan stage', 'Empty Remark');
                    //alert("Please provide a reason for the change")
                    return
                };
                vm.viewModelHelper.apiPost('api/rawloandetail/updaterawloandetail', vm.loanDetail,
                    function (result) {
                   vm.updateloanclassificationotch();
                   $state.go('ifrs-loandetail-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.loanDetail.errors;

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
                vm.viewModelHelper.apiPost('api/rawloandetail/deleteloandetail', vm.loanDetail.LoanDetailId,
              function (result) {
                  deleteLoansDetailsNotch();
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-loandetail-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.updateloanclassificationotch = function () {
            if (vm.notes === '') {
                vm.notes = null
            } else {
            vm.notes = 'Previous Stage: ' + vm.oldStage/*.toString()*/ + '; Reason for change: ' + vm.notes
            }
            var params = { RefNo: vm.loanDetail.RefNo, Rating: vm.loanDetail.Sector, Stage: vm.loanDetail.Stage, Notes: vm.notes };
            vm.viewModelHelper.apiPost('api/rawloandetail/updateloanclassnotch', params,
                      function (result) {
                        //  $state.go('ifrs-loandetail-list');
                         // vm.getUpdatedMarketYieldData();
                      },
                     function (result) {
                         toastr.error(result.data, 'Fintrak Error');
                     }, null);
        }

        var getAllCollateralTypes = function () {
            vm.viewModelHelper.apiGet('api/collateraltype/availablecollateralTypes/', null,
                   function (result) {
                       vm.collateralType = result.data;
                       //vm.init === true;
                       //toastr.success('Collateral Types data loaded, ready for modifiation.', 'Fintrak');
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
        }

        var getSectors = function () {
            vm.viewModelHelper.apiGet('api/ifrssectorccf/getAllSectorsForCCF', null,
                   function (result) {
                       vm.sectors = result.data;
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
        }

        var deleteLoansDetailsNotch = function () {
                  vm.viewModelHelper.apiPost('api/rawloandetail/deleteloansdetailsnotch/' + vm.loanDetail.RefNo,
                   function (result) {
                       toastr.success('Loan RefNo deleted from Other tables', 'Fintrak');
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

        document.getElementById("collatVal")
            .addEventListener("keyup", function (event) {
                event.preventDefault();
                if (event.keyCode === 13) {
                    vm.getCollateralRecovAmt();
                }
            });

        vm.getCollateralRecovAmt = function () {

            vm.viewModelHelper.apiGet('api/rawloandetail/getcollateralrecovamt/' + vm.loanDetail.RefNo + '/' + vm.loanDetail.CollateralType + '/' + vm.loanDetail.CollateralValue, null,
                function (result) {
                    //vm.collateralRecovCompute = result.data;
                    vm.loanDetail.CollateralRecoverableAmt = result.data[0].CollateralRecovAmt;
                    vm.loanDetail.CollateralHaircut = result.data[0].Haircut;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        vm.cancel = function () {
            $state.go('ifrs-loandetail-list');
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
