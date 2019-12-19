/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FacilityClassificationEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        FacilityClassificationEditController]);

    function FacilityClassificationEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS Core';
        vm.view = 'facilityclassification-edit-view';
        vm.viewName = 'Facility Classification(Loans, OBE & Investments)';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.productType = 'nill';
        vm.facilityclassification = {};
        vm.showfields = true;
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.openedRunDate = false;

        vm.productTypes = [];
        vm.subTypes = [];
        vm.sectors = [];
       
        var facilityclassificationRules = [];

        var setupRules = function() {

            facilityclassificationRules.push(new validator.PropertyRule("RefNo",
                {
                    required: { message: "RefNo is required" }
                }));

            facilityclassificationRules.push(new validator.PropertyRule("AccountNo",
                {
                    required: { message: "AccountNo is required" }
                }));

        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.classId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/facilityclassification/getfacilityclassification/' + $stateParams.classId, null,
                   function (result) {
                       vm.facilityclassification = result.data;

                       initialView();                      
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
                else
                    vm.facilityclassification = { Caption: '', FinType: '', FinSubType: '', Position: 1, Class: 1, flag: $stateParams.type, Active: true };
            }
        }

    

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.facilityclassification, facilityclassificationRules);
            vm.viewModelHelper.modelIsValid = vm.facilityclassification.isValid;
            vm.viewModelHelper.modelErrors = vm.facilityclassification.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/facilityclassification/updatefacilityclassification', vm.facilityclassification,
               function (result) {
                   
                   $state.go('ifrs-facilityclassification-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.facilityclassification.errors;

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
                vm.viewModelHelper.apiPost('api/facilityclassification/deletefacilityclassification', vm.facilityclassification.Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-facilityclassification-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        var intializeLookUp = function () {
            getProductTypes();
            getsubTypes();
           // getsectors();
        };



        vm.cancel = function () {
            $state.go('ifrs-facilityclassification-list');
        };


        var getProductTypes = function () {
            vm.viewModelHelper.apiGet('api/facilityclassification/getproductTypes',
                null,
                function (result) {
                    vm.productTypes = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

        vm.onProductTypeChanged = function (productType) {
            getSubTypebyProductType(productType);
        };

        var getsubTypes = function () {
            vm.viewModelHelper.apiGet('api/facilityclassification/getsubtypebyproducttype/' + vm.productType ,
                null,
                function (result) {
                    vm.subTypes = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };


        setupRules();
        initialize(); 



    }
}());
