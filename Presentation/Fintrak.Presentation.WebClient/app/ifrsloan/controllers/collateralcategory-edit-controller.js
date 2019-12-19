/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CollateralCategoryEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CollateralCategoryEditController]);

    function CollateralCategoryEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'collateralcategory-edit-view';
        vm.viewName = 'Collateral Categories';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.collateralCategory = {};
        vm.collateralType = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.showCollateralType = false;

        var collateralCategoryRules = [];

        var setupRules = function () {
          
            collateralCategoryRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            collateralCategoryRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
              
                if ($stateParams.collateralcategoryId !== 0) {
                    vm.showPeriod = true;
                    vm.viewModelHelper.apiGet('api/collateralcategory/getcollateralcategorywithchildren/' + $stateParams.collateralcategoryId, null,
                   function (result) {
                       vm.collateralCategory = result.data.CollateralCategory;
                       vm.collateralTypes = result.data.CollateralType;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.collateralCategory = { Code: '', Name: '', Active: true };
            }
        }

        var initialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#collateralTypeTable').length > 0) {
                    var exportTable = $('#collateralTypeTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            }, 50);
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.collateralCategory, collateralCategoryRules);
            vm.viewModelHelper.modelIsValid = vm.collateralCategory.isValid;
            vm.viewModelHelper.modelErrors = vm.collateralCategory.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/collateralcategory/updatecollateralcategory', vm.collateralCategory,
               function (result) {
                   
                   $state.go('ifrsloan-collateralcategory-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.collateralCategory.errors;

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
                vm.viewModelHelper.apiPost('api/collateralCategory/deletecollateralCategory', vm.collateralCategory.CollateralCategoryId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrsloan-collateralcategory-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrsloan-collateralcategory-list');
        };

      
       
        setupRules();
        initialize(); 
    }
}());
