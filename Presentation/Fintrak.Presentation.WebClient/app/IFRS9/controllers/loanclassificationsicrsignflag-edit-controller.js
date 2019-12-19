/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanClassificationSICRSignFlagEditController",
            ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                LoanClassificationSICRSignFlagEditController]); // 

    function LoanClassificationSICRSignFlagEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {

        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'LoanClassificationSICRSignFlag';
        vm.view = 'loanclassificationsicrsignflag-edit-view';
        vm.viewName = 'Classification SICR Significant Flag Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.loanclassificationSICRFlag = {};     
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.Flag = [
            { value: 'Primary', name: 'Primary' },
            { value: 'Secondary', name: 'Secondary' },
            { value: 'Tetiary', name: 'Tertiary' }
        ];
       
        var loanclassificationSICRFlagRules = [];

        var setupRules = function () {

            loanclassificationSICRFlagRules.push(new validator.PropertyRule("LoanClassificationId", {
                required: { message: "LoanClassificationId is required" }
            }));

            loanclassificationSICRFlagRules.push(new validator.PropertyRule("SICR_Flag", {
                notZero: { message: "SICR_Flag is required" }
            }));
        };


        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

                if ($stateParams.Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/loanclassificationSignFlag/getLoanClassificationSignFlag/' + $stateParams.Id, null,

                        function (result) {
                            vm.loanclassificationsicrsignflag = result.data;
                            initialView();

                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);

                } else {

                    vm.loanclassificationSICRFlag = {
                        Id : '',
                        ProductType : '',
                        SubType : '',
                        SICR_Flag : '',
                        SignificantNo : '',
                        Active : true
                    };

                }


            }
        }

        var intializeLookUp = function () {
            getGroupedClassification();
            getSICRParamter();
            }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.loanclassificationSICRFlag, loanclassificationSICRFlagRules);
            vm.viewModelHelper.modelIsValid = vm.loanclassificationSICRFlag.isValid;
            vm.viewModelHelper.modelErrors = vm.loanclassificationSICRFlag.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/loanclassificationSignFlag/updateLoanClassificationSignFlag', vm.loanclassificationsicrsignflag,
               function (result) {                   
                   $state.go('ifrs-loanclassificationsicrsignflag-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.loanclassificationSICRFlag.errors;

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
                vm.viewModelHelper.apiPost('api/loanclassificationSICRFlag/deleteloanclassificationSICRFlag', vm.loanclassificationSICRFlag.Id,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('ifrs-loanclassificationsicrsignflag-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('ifrs-loanclassificationsicrsignflag-list');
        };

        var getGroupedClassification = function () {
            vm.viewModelHelper.apiGet('api/loanclassificationSignFlag/getgroupedclassification',
                null,
                function (result) {
                    vm.grpClass = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };
        var getSICRParamter = function () {
            vm.viewModelHelper.apiGet('api/sicrparameters/getsicrparameters', null,
                function (result) {
                    vm.sicrparameters = result.data;
                  
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        setupRules();
        initialize(); 
    }


}());
