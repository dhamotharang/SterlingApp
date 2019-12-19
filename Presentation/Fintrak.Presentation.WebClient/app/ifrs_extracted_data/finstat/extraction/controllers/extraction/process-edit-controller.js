/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ProcessEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ProcessEditController]);

    function ProcessEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'process-edit-view';
        vm.viewName = 'Process';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.process = {};
        vm.processroles = [];
        
        vm.modules = [];

        vm.runTypes = [
           { Id: 1, Name: 'Package' },
           { Id: 2, Name: 'Project' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.showChildren = false;

        var processRules = [];

        var setupRules = function () {
          
            processRules.push(new validator.PropertyRule("Title", {
                required: { message: "Title is required" }
            }));

            processRules.push(new validator.PropertyRule("PackageName", {
                required: { message: "Package Name is required" }
            }));

            processRules.push(new validator.PropertyRule("PackagePath", {
                required: { message: "Package Path is required" }
            }));

            processRules.push(new validator.PropertyRule("ModuleId", {
                notZero: { message: "Module is required" }
            }));
           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.processId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/process/getprocesswithchildren/' + $stateParams.processId, null,
                   function (result) {
                       vm.process = result.data.Process;
                       vm.processRoles = result.data.ProcessRoles;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.process = { Title: '',RunType:1, PackageName: '',PackagePath: 'None',ModuleId: 0, Active: true };
            }
        }

        var intializeLookUp = function () {
            getModules();
        }

        var initialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
               
                // data export
                if ($('#processRoleTable').length > 0) {
                    var exportTable = $('#processRoleTable').DataTable({
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
            validator.ValidateModel(vm.process, processRules);
            vm.viewModelHelper.modelIsValid = vm.process.isValid;
            vm.viewModelHelper.modelErrors = vm.process.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/process/updateprocess', vm.process,
               function (result) {
                   
                   $state.go('core-process-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.process.errors;

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
                vm.viewModelHelper.apiPost('api/process/deleteprocess', vm.process.ProcessId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-process-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-process-list');
        };

        var getModules = function () {
            vm.viewModelHelper.apiGet('api/module/availablemodules', null,
                 function (result) {
                     vm.modules = result.data;
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
