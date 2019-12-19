/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ExtractionEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ExtractionEditController]);

    function ExtractionEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'extraction-edit-view';
        vm.viewName = 'Extraction';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.extraction = {};
        vm.extractionroles = [];
        
        vm.solutions = [];

        vm.runTypes = [
            { Id: 1, Name: 'Package' },
            { Id: 2, Name: 'Project' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.showChildren = false;

        var extractionRules = [];

        var setupRules = function () {
          
            extractionRules.push(new validator.PropertyRule("Title", {
                required: { message: "Title is required" }
            }));

            extractionRules.push(new validator.PropertyRule("PackageName", {
                required: { message: "Package Name is required" }
            }));

            extractionRules.push(new validator.PropertyRule("PackagePath", {
                required: { message: "Package Path is required" }
            }));

            extractionRules.push(new validator.PropertyRule("ProcedureName", {
                required: { message: "Procedure Name is required" }
            }));

            extractionRules.push(new validator.PropertyRule("ScriptText", {
                required: { message: "Script Text is required" }
            }));

            extractionRules.push(new validator.PropertyRule("SolutionId", {
                notZero: { message: "Solution is required" }
            }));
           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.extractionId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/extraction/getextractionwithchildren/' + $stateParams.extractionId, null,
                   function (result) {
                       vm.extraction = result.data.Extraction;
                       vm.extractionRoles = result.data.ExtractionRoles;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.extraction = { Title: '', RunType: 1, PackageName: '', PackagePath: 'None', ProcedureName: 'None', ScriptText: 'None', SolutionId: 0, Active: true };
            }
        }

        var intializeLookUp = function () {
            getSolutions();
        }

        var initialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#extractionRoleTable').length > 0) {
                    var exportTable = $('#extractionRoleTable').DataTable({
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
            validator.ValidateModel(vm.extraction, extractionRules);
            vm.viewModelHelper.modelIsValid = vm.extraction.isValid;
            vm.viewModelHelper.modelErrors = vm.extraction.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/extraction/updateextraction', vm.extraction,
               function (result) {
                   
                   $state.go('core-extraction-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.extraction.errors;

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
                vm.viewModelHelper.apiPost('api/extraction/deleteextraction', vm.extraction.ExtractionId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-extraction-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-extraction-list');
        };

        var getSolutions = function () {
            vm.viewModelHelper.apiGet('api/solution/availablesolutions', null,
                 function (result) {
                     vm.solutions = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }
       
        setupRules();
        initialize(); 
    }
}());
