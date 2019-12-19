/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IndividualImpairmentEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IndividualImpairmentEditController]);

    function IndividualImpairmentEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'individualimpairment-edit-view';
        vm.viewName = 'Individual Impairment';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.individualImpairments = [];
        vm.RefNo = 'None';


        //vm.ratingtypes = [
        //    { Id: 1, Name: 'Individual' },
        //    { Id: 2, Name: 'Product' },
        //    { Id: 3, Name: 'Sector' }
        //];

        vm.referenceNumbers = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var individualimpairmentRules = [];


        var individualimpairmentRules = function () {

            individualimpairmentRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));

            individualimpairmentRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "AccountNo is required" }
            }));

            individualimpairmentRules.push(new validator.PropertyRule("ProductName", {
                required: { message: "ProductName is required" }
            }));

            individualimpairmentRules.push(new validator.PropertyRule("ValueDate", {
                required: { message: "ValueDate is required" }
            }));


            individualimpairmentRules.push(new validator.PropertyRule("MaturityDate", {
                required: { message: "MaturityDate is required" }
            }));
        }




        var setupRules = function () {

        }


        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.individualimpairmentId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/individualimpairment/getindividualimpairment/' + $stateParams.individualimpairmentId, null,
                   function (result) {
                       vm.individualImpairments = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.individualImpairments = { RefNo: '', AccountNo: '', ProductName: '', ValueDate: '', MaturityDate: '', Active: true };


                //vm.individualImpairments.AccountNo = result.data[0].AccountNo;
                //vm.individualImpairments.ProductName = result.data[0].ProductName;
                //vm.individualImpairments.ValueDate = result.data[0].ValueDate;
                //vm.individualImpairments.MaturityDate = result.data[0].MaturityDate;


            }
        }

        var intializeLookUp = function () {
            getreferenceNumbers();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.individualImpairments, individualimpairmentRules);
            vm.viewModelHelper.modelIsValid = vm.individualImpairments.isValid;
            vm.viewModelHelper.modelErrors = vm.individualImpairments.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/individualimpairment/updateindividualimpairment', vm.individualImpairments,
               function (result) {
                   
                   $state.go('ifrsloan-individualimpairment-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.individualImpairments.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/individualimpairment/deleteindividualimpairment', vm.individualImpairments.Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrsloan-individualimpairment-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrsloan-individualimpairment-list');
        };


        var getreferenceNumbers = function () {
            vm.viewModelHelper.apiGet('api/individualimpairment/availablereferenceNumbers', null,
                 function (result) {
                     vm.referenceNumbers = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Fail to load Reference Numbers.', 'Fintrak');
                 }, null);
        }

        vm.load = function () {
            var url = '';
            url = 'api/individualimpairment/getindividualimpairmentbyrefno/' + vm.individualImpairments.RefNo,

                   vm.viewModelHelper.apiGet(url, null,
                      function (result) {

                          vm.individualImpairments.AccountNo = result.data[0].AccountNo;
                          vm.individualImpairments.ProductName = result.data[0].ProductName;
                          vm.individualImpairments.ValueDate = result.data[0].ValueDate;
                          vm.individualImpairments.MaturityDate = result.data[0].MaturityDate;

                          toastr.success('Data for the selected RefNo loaded.', 'Fintrak');
                      },
                      function (result) {
                          toastr.error('Fail to load data for the selected RefNo.', 'Fintrak');
                      }, null);
        }

        //$scope.populate = function () {
        //    $scope.item.size.code = $scope.selectedItem.code
        //    // use $scope.selectedItem.code and $scope.selectedItem.name here
        //    // for other stuff ...
        //}

        setupRules();
        initialize();
    }
}());
