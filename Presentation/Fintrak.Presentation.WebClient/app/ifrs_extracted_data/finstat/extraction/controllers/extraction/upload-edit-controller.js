/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("UploadEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        UploadEditController]);

    function UploadEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'upload-edit-view';
        vm.viewName = 'Upload';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.upload = {};
        vm.uploadroles = [];
        
        vm.solutions = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.showChildren = false;

        var uploadRules = [];

        var setupRules = function () {
          
            uploadRules.push(new validator.PropertyRule("Title", {
                required: { message: "Title is required" }
            }));

            uploadRules.push(new validator.PropertyRule("SolutionId", {
                notZero: { message: "Solution is required" }
            }));

            uploadRules.push(new validator.PropertyRule("Action", {
                required: { message: "Action is required" }
            }));

            uploadRules.push(new validator.PropertyRule("Position", {
                notZero: { message: "Position is required" }
            }));

            uploadRules.push(new validator.PropertyRule("Template", {
                required: { message: "Template is required" }
            }));
           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.uploadId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/upload/getuploadwithchildren/' + $stateParams.uploadId, null,
                   function (result) {
                       vm.upload = result.data.Upload;
                       vm.uploadRoles = result.data.UploadRoles;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.upload = { Title: '',SolutionId: 0,Action:'',TruncateAction:'',Template:'',Position:0, Active: true };
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
                if ($('#uploadRoleTable').length > 0) {
                    var exportTable = $('#uploadRoleTable').DataTable({
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
            validator.ValidateModel(vm.upload, uploadRules);
            vm.viewModelHelper.modelIsValid = vm.upload.isValid;
            vm.viewModelHelper.modelErrors = vm.upload.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/upload/updateupload', vm.upload,
               function (result) {
                   
                   $state.go('core-upload-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.upload.errors;

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
                vm.viewModelHelper.apiPost('api/upload/deleteupload', vm.upload.UploadId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-upload-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-upload-list');
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
