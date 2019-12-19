/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MPRCommFeeEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MPRCommFeeEditController]);

    function MPRCommFeeEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'mprcommfee-edit-view';
        vm.viewName = 'MPR Commission and Fees';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.mprcommfee = {};
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var mprcommfeeRules = [];

        var setupRules = function () {

            mprcommfeeRules.push(new validator.PropertyRule("Glcode", {
                required: { message: "Glcode is required" }
            }));

            mprcommfeeRules.push(new validator.PropertyRule("CustomerName", {
                required: { message: "CustomerName is required" }
            }));

            mprcommfeeRules.push(new validator.PropertyRule("AccountOfficerCode", {
                required: { message: "Account Officer Code is required" }
            }));

            mprcommfeeRules.push(new validator.PropertyRule("RelatedAccount", {
                required: { message: "Related Account is required" }
            }));
                      
        }

        var initialize = function () {
            if (vm.init === false) {

                if ($stateParams.CommFee_Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/mprcommfee/getmprcommfee/' + $stateParams.CommFee_Id, null,
                   function (result) {
                       vm.mprcommfee = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.mprcommfee = { Glcode: '', CustomerName: '', AccountOfficerCode: '', RelatedAccount: '', Active: true };
            }
        }


        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.mprcommfee, mprcommfeeRules);
            vm.viewModelHelper.modelIsValid = vm.mprcommfee.isValid;
            vm.viewModelHelper.modelErrors = vm.mprcommfee.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/mprcommfee/updatemprcommfee', vm.mprcommfee,
               function (result) {
                   
                   $state.go('mpr-mprcommfee-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.mprcommfee.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }
        // vm.derivedCaption.DerivedCaptionId,
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/mprcommfee/deletemprcommfee', vm.mprcommfee.CommFee_Id, //vm.mprcommfee.CommFee_Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-mprcommfee-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-mprcommfee-list');
        };

        
        setupRules();
        initialize();
    }
}());
