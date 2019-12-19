/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("UserSetupEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        UserSetupEditController]);

    function UserSetupEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'usersetup-edit-view';
        vm.viewName = 'User Setup';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.userSetup = {};
        vm.userRoles = [];
        vm.userReportRoles = [];
        vm.userCompanies = [];
        vm.showRoles = true;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var userSetupRules = [];

        var setupRules = function () {
          
            userSetupRules.push(new validator.PropertyRule("LoginID", {
                required: { message: "LoginID is required 1" }
            }));

            userSetupRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required 1" }
            }));

           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initialLookUp();

                if ($stateParams.usersetupId !== 0) {
                    vm.showPeriod = true;
                    vm.viewModelHelper.apiGet('api/account/getaccountdetail/' + $stateParams.usersetupId, null,
                   function (result) {
                       vm.userSetup = result.data.UserSetup;
                       vm.userRoles = result.data.Roles;
                       vm.userReportRoles = result.data.ReportRoles;
                       vm.userCompanies = result.data.UserCompanies;

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                   vm.userSetup = { LoginID:'', Name: '', Email: '',IsApplicationUser:true,IsReportUser:true, Active: true };
            }
        }

        var initialLookUp = function () {
            
        }

        var initialView = function () {
  
        }


        vm.checkEmail = function () {

            var email = document.getElementById('remail');
            var filter = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

            if (!filter.test(email.value)) {
                var stat = false;
                alert('Please provide a valid email address');
                email.focus;
                return stat;
            }
            else {
                var stat = true;
                return stat;
            }

        }


        vm.save = function () {
            
            if (vm.checkEmail() == true) {

                //Validate
                validator.ValidateModel(vm.userSetup, userSetupRules);
                vm.viewModelHelper.modelIsValid = vm.userSetup.isValid;
                vm.viewModelHelper.modelErrors = vm.userSetup.errors;
                if (vm.viewModelHelper.modelIsValid) {

                    var userModel = { UserSetup: vm.userSetup, Roles: vm.userRoles, ReportRoles: vm.userReportRoles, UserCompanies: vm.userCompanies };

                    vm.viewModelHelper.apiPost('api/account/updateaccountdetail', userModel,
                   function (result) {

                       $state.go('core-usersetup-list');
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
            }
            else {
                vm.viewModelHelper.modelErrors = vm.userSetup.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
            
            
            }

            

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete this user?' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/account/deleteuserSetup', vm.userSetup.UserSetupId,
              function (result) {
                  toastr.success('Selected user deleted.', 'Fintrak');
                  $state.go('core-usersetup-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-usersetup-list');
        };

        //vm.resetpassword = function (loginId) {
        //    var resetpw = $window.confirm(' Are you sure you want to Reset Password for user : ' + loginId);

        //    if (resetpw) {
        //        vm.viewModelHelper.apiPost('api/account/passwordreset/', loginId,null
        //      function (result) {
        //          toastr.success('Password reset for user ' + loginId + 'Successful', 'Fintrak');
        //          $state.go('core-usersetup-list');
        //      },
        //      function (result) {
        //          toastr.error(result.data, 'Fintrak');
        //      }, null);
        //    }
        //}
        vm.resetpassword = function (loginId) {
            var resetpw = $window.confirm(' Are you sure you want to Reset Password for user : ' + loginId);
            if (resetpw)
            {
                var url = 'api/account/passwordreset/'+ loginId
                vm.viewModelHelper.apiPost(url ,null,
                function (result) {
                    toastr.success('Password reset for user ' + loginId + 'Successful', 'Fintrak');
                    alert('Password reset for user ' + loginId + ' Successful', 'Fintrak');
                    $state.go('core-usersetup-list');
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
            } 
        }

        setupRules();
        initialize(); 
    }
}());
