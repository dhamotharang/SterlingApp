/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TransferPriceEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        TransferPriceEditController]);

    function TransferPriceEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'transferprice-edit-view';
        vm.viewName = 'Transfer Prices';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.transferPrice = {};

        vm.products = [];
        vm.captions = [];

        vm.solutions = [];
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var transferpriceRules = [];

        var setupRules = function () {
          
            transferpriceRules.push(new validator.PropertyRule("ProductCode", {
                required: { message: "Product is required" }
            }));

            transferpriceRules.push(new validator.PropertyRule("CaptionCode", {
                required: { message: "Caption is required" }
            }));

            transferpriceRules.push(new validator.PropertyRule("DefinitionCode", {
                required: { message: "Definition is required" }
            }));

            transferpriceRules.push(new validator.PropertyRule("MisCode", {
                required: { message: "MisCode is required" }
            }));


               transferpriceRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            transferpriceRules.push(new validator.PropertyRule("Rate", {
                required: { message: "Rate is required" }
            }));

            transferpriceRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));

            transferpriceRules.push(new validator.PropertyRule("SolutionId", {
                required: { message: "Solution is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.transferpriceId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/transferprice/gettransferprice/' + $stateParams.transferpriceId, null,
                   function (result) {
                       vm.transferPrice = result.data;

                       getTeams(vm.transferPrice.TeamDefinitionCode);
                     

                       initialView();
                       vm.init === true;
                     
                   },
                   function (result) {
                     
                   }, null);
               }
               else
                    vm.transferPrice = { ProductCode: '', CaptionCode: '', DefinitionCode: '', MisCode: '',Rate: 1, Period: '',Year: '',SolutionId:1, Active: true };
            }
        }

        var intializeLookUp = function () {
            getProducts();
            getCaptions();
            getSolutions();
            getTeamDefinitions();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.transferPrice, transferpriceRules);
            vm.viewModelHelper.modelIsValid = vm.transferPrice.isValid;
            vm.viewModelHelper.modelErrors = vm.transferPrice.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/transferprice/updatetransferprice', vm.transferPrice,
               function (result) {
                 //
                   $state.go('mpr-transferprice-list');
               },
               function (result) {
                  
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.transferPrice.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                }); 
            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/transferprice/deletetransferprice', vm.transferPrice.TransferPriceId,
              function (result) {
                 //
                  $state.go('mpr-transferprice-list');
              },
              function (result) {
                 
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-transferprice-list');
        };


          var getSolutions = function () {
            vm.viewModelHelper.apiGet('api/solution/availablesolutions', null,
                 function (result) {
                     vm.solutions = result.data;
                     //vm.init === true;

                 },
                 function (result) {

                 }, null);
        }

        var getProducts = function () {
            vm.viewModelHelper.apiGet('api/product/availableproducts', null,
                 function (result) {
                     vm.products = result.data;
                 },
                 function (result) {

                 }, null);
        }

        var getCaptions = function () {
            vm.viewModelHelper.apiGet('api/bscaption/availablebscaptions', null,
                 function (result) {
                     vm.captions = result.data;
                 },
                 function (result) {

                 }, null);
        }

        vm.onTeamDefinitionChanged = function (definition) {
            getTeams(definition);
        }


        var getTeamDefinitions = function () {
            vm.viewModelHelper.apiGet('api/teamdefinition/availableteamdefinitions', null,
                 function (result) {
                     vm.teamDefinitions = result.data;
                 },
                 function (result) {

                 }, null);
        }

        var getTeams = function (definition) {
            vm.viewModelHelper.apiGet('api/team/getteambydefinition/' + definition, null,
                 function (result) {
                     vm.teams = result.data;
                 },
                 function (result) {

                 }, null);
        }


       


        setupRules();
        initialize(); 
    }
}());
