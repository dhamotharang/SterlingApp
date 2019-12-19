/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IFRSBondEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IFRSBondEditController]);

    function IFRSBondEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS Bond Data';
        vm.view = 'bonddata-edit-view';
        vm.viewName = 'Bond Data Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifrsBond = {};
        vm.companies = [];
        vm.collateralType = [];
        vm.ratings = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.classification = '';
        vm.category = '';
        vm.openedRunDate = false;
        vm.classifications = [
            { Id: 1, Name: 'Amortised Cost' },
            { Id: 2, Name: 'FVPL' },
            { Id: 3, Name: 'FVOCI' }
        ];
        vm.stages = [
        { Id: 1, Name: 'Stage 1' },
        { Id: 2, Name: 'Stage 2' },
        { Id: 3, Name: 'Stage 3' }
        ];
      vm.category = [
      { Id: 1, Name: 'FBND' },
      { Id: 2, Name: 'CBND' },
      { Id: 3, Name: 'SBND' }
     // { Id: 3, Name: 'CORPORATE' },
     // { Id: 3, Name: 'OTHERS' }
        ];
       
        var bonddataRules = [];

        var setupRules = function () {
          
            bonddataRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));

            //bonddataRules.push(new validator.PropertyRule("Currency", {
            //    required: { message: "Currency is required" }
            //}));

            bonddataRules.push(new validator.PropertyRule("Classification", {
                required: { message: "Classification is required" }
            }));

            bonddataRules.push(new validator.PropertyRule("ValueDate", {
                notZero: { message: "ValueDate is required" }
            }));

            bonddataRules.push(new validator.PropertyRule("MaturityDate", {
                notZero: { message: "MaturityDate is required" }
            }));


            //
            bonddataRules.push(new validator.PropertyRule("CleanPrice", {
                required: { message: "CleanPrice is required" }
            }));

            bonddataRules.push(new validator.PropertyRule("FaceValue", {
                required: { message: "FaceValue is required" }
            }));

            bonddataRules.push(new validator.PropertyRule("CouponRate", {
                notZero: { message: "CouponRate is required" }
            }));

            //bonddataRules.push(new validator.PropertyRule("CompanyCode", {
            //    notZero: { message: "CompanyCode is required" }
            //}));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.bondId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ifrsbond/getbonddata/' + $stateParams.bondId, null,
                   function (result) {
                       vm.ifrsBond = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.ifrsBond = { RefNo: '', Currency: '', Classification: '', ValueDate: '', MaturityDate: '', CleanPrice: '', FaceValue: '', CompanyCode: '', CouponRate: '', CollateralValue: '', CollateralType: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getCompanies();
            getAllCollateralTypes();
            getRatings();
            }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ifrsBond, bonddataRules);
            vm.viewModelHelper.modelIsValid = vm.ifrsBond.isValid;
            vm.viewModelHelper.modelErrors = vm.ifrsBond.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/ifrsbond/updateifrsbond', vm.ifrsBond,
               function (result) {
                   
                   $state.go('ifrs-bonddata-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.ifrsBond.errors;

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
                vm.viewModelHelper.apiPost('api/ifrsbond/deletebond', vm.ifrsBond.BondId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-bonddata-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-bonddata-list');
        };

   
        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                 function (result) {
                     vm.companies = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Unable to load companies', 'Fintrak');
                 }, null);
        }
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
