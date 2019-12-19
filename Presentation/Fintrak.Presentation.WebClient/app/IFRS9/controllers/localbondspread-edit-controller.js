/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LocalBondSpreadEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        LocalBondSpreadEditController]);

    function LocalBondSpreadEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'localbondspread-edit-view';
        vm.viewName = 'LocalBondSpread';
        vm.status = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.localBondSpread = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ifrs9localbondspreadRules = [];
       
        vm.openRunDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate = true;
        }
        //vm.classifications = [
        //    { Id: 1, Name: 'Past Due Days' },
        //    { Id: 2, Name: 'Probability of Default' },
        //    { Id: 3, Name: 'Hybrid' },
        //];


        //vm.groupBased = [
        //   { Id: 1, Name: 'Sector' },
        //   { Id: 2, Name: 'Loan Type' },
          
        //];
        //vm.ltpdapproach = [
        //    { Id: 1, Name: 'Binomial Tree Approach' },
        //    { Id: 2, Name: 'Markov Chain Approach' },
        //];

        var localbondspreadRules = function () {

            localbondspreadRules.push(new validator.PropertyRule("ConDate", {
                required: { message: "ConDate is required" }
            }));

            localbondspreadRules.push(new validator.PropertyRule("ZeroRate", {
                required: { message: "ZeroRate is required" }
            }));

            localbondspreadRules.push(new validator.PropertyRule("Classification", {
                required: { message: "Classification is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.localbondspreadId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/localbondspread/getlocalbondspread/' + $stateParams.localbondspreadId, null,
                   function (result) {
                       vm.localBondSpread = result.data;
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
                    vm.localBondSpread = { ConDate: '', ZeroRate: 0, Classification: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.localBondSpread, ifrs9localbondspreadRules);
            vm.viewModelHelper.modelIsValid = vm.localBondSpread.isValid;
            vm.viewModelHelper.modelErrors = vm.localBondSpread.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/localbondspread/updatelocalbondspread', vm.localBondSpread,
               function (result) {
                   
                   $state.go('ifrs9-localbondspread-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.localBondSpread.errors;

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
                vm.viewModelHelper.apiPost('api/localbondspread/deletelocalbondspread', vm.localBondSpread.LocalBondSpreadId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-localbondspread-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-localbondspread-list');
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
        //localbondspreadRules();
        initialize(); 
    }
}());
