/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("EuroBondSpreadEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        EuroBondSpreadEditController]);

    function EuroBondSpreadEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'eurobondspread-edit-view';
        vm.viewName = 'EuroBondSpread';
        vm.status = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.euroBondSpread = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ifrs9eurobondspreadRules = [];
       
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

        vm.openRunDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate = true;
        }

        var eurobondspreadRules = function () {

            eurobondspreadRules.push(new validator.PropertyRule("ConDate", {
                required: { message: "ConDate is required" }
            }));

            eurobondspreadRules.push(new validator.PropertyRule("ZeroRate", {
                required: { message: "ZeroRate is required" }
            }));

            eurobondspreadRules.push(new validator.PropertyRule("Classification", {
                required: { message: "Classification is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.eurobondspreadId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/eurobondspread/geteurobondspread/' + $stateParams.eurobondspreadId, null,
                   function (result) {
                       vm.euroBondSpread = result.data;
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
                    vm.euroBondSpread = { ConDate: '', ZeroRate: 0, Classification: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.euroBondSpread, ifrs9eurobondspreadRules);
            vm.viewModelHelper.modelIsValid = vm.euroBondSpread.isValid;
            vm.viewModelHelper.modelErrors = vm.euroBondSpread.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/eurobondspread/updateeurobondspread', vm.euroBondSpread,
               function (result) {
                   
                   $state.go('ifrs9-eurobondspread-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.euroBondSpread.errors;

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
                vm.viewModelHelper.apiPost('api/eurobondspread/deleteeurobondspread', vm.euroBondSpread.EuroBondSpreadId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-eurobondspread-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-eurobondspread-list');
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
        //eurobondspreadRules();
        initialize(); 
    }
}());
