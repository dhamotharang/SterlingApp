/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("GeneralEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        GeneralEditController]);

    function GeneralEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'general-edit-view';
        vm.viewName = 'General';
        vm.viewName2 = 'Company and Database Setup';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.general = {};
        vm.companies = [];
        vm.database = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var generalRules = [];

        var setupRules = function () {
            generalRules.push(new validator.PropertyRule("Email", {
                required: { message: "Invalid email address" }
            }));
           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initialLookUp();

                vm.viewModelHelper.apiGet('api/general/getgeneral', null,
                  function (result) {
                      vm.general = result.data;

                      if (vm.general === 'null')
                          vm.general = { Host: '', Email: '', Password: '', Active: true };

                      initialView();
                      vm.init === true;
                      //
                  },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                  }, null);

                vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                    function (result) {
                        vm.companies = result.data;
                        InitialView();
                        vm.init === true;

                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }

        var initialLookUp = function () {
            
        }

        var initialView = function () {
  
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.general, generalRules);
            vm.viewModelHelper.modelIsValid = vm.general.isValid;
            vm.viewModelHelper.modelErrors = vm.general.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/general/updategeneral', vm.general,
               function (result) {
                   
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.general.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
                
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#companyTable').length > 0) {
                    var exportTable = $('#companyTable').DataTable({
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

        vm.loadDb = function () {

            vm.viewModelHelper.apiGet('api/database/availabledatabase', null,
            function (result) {
                vm.database = result.data;
                //InitialView();
                //vm.init === true;

            },
            function (result) {
                toastr.error(result.data, 'Fintrak');
            }, null);
        }
        

        setupRules();
        initialize(); 
    }
}());
