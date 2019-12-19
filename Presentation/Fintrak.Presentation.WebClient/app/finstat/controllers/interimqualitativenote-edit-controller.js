/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("InterimQualitativeNoteEditController",
            ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', InterimQualitativeNoteEditController]);
    //, 'textAngular'
    //, textAngular
    function InterimQualitativeNoteEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'qualitativenote-edit-view';
        vm.viewName = 'Interim Qualitative Note';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.interimqualitativeNote = {};
        vm.topnote = '';
        vm.bottomnote = '';
        vm.htmlContent = '';
        vm.htmlContentbottom = '';
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var exportTable;
        var tabID;
        vm.reportNames = [];
        vm.openedRunDate = false;
        var interimqualitativenoteRules = [];
     
        vm.captionTypes = [
            { value: 1, name: 'Conventional' },
            { value: 2, name: 'Interim' },
            { value: 3, name: 'NIB' }
        ];

        var setupRules = function () {

            interimqualitativenoteRules.push(new validator.PropertyRule("report", {
                required: { message: "Report is required" }
            }));

            interimqualitativenoteRules.push(new validator.PropertyRule("TopNote", {
                required: { message: "Top Note is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.interimqualitativeNoteId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/interimqualitativenote/getinterimqualitativenote/' + $stateParams.interimqualitativeNoteId, null,
                        function (result) {
                            vm.interimqualitativeNotes = result.data;
                            vm.htmlContent = vm.interimqualitativeNotes.TopNotes;
                            vm.htmlContentbottom = vm.interimqualitativeNotes.BottomNotes;
                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.interimqualitativeNote = { report: '', TopNotes: '', BottomNotes: '', Active: true };
            }
        }

        var intializeLookUp = function () {
          //  getReportNames();
        }

        var initialView = function () {

        }

        vm.save = function () {

            var ob_params = {               
                TopNote: vm.htmlContent,
                BottomNote: vm.htmlContentbottom,
                RunDate: vm.interimqualitativeNotes.RunDate,
                ReportType: vm.interimqualitativeNotes.ReportType,
                report: vm.interimqualitativeNotes.report
               
            };

            console.log(ob_params)

            validator.ValidateModel(ob_params, interimqualitativenoteRules);

            vm.viewModelHelper.modelIsValid = ob_params.isValid;
            vm.viewModelHelper.modelErrors = ob_params.errors;

            if (vm.viewModelHelper.modelIsValid) {

                console.log('working');

                vm.viewModelHelper.apiPost('api/interimqualitativenote/updateinterimqualitativenote', ob_params,
                    function (result) {
                        $state.go('finstat-interimqualitativenote-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);

            } else {

                vm.viewModelHelper.modelErrors = vm.interimqualitativeNote.errors;

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
                vm.viewModelHelper.apiPost('api/interimqualitativenote/deleteinterimqualitativenote', vm.interimqualitativeNotes.InterimQualitativeNoteId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('finstat-interimqualitativenote-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }

        vm.cancel = function (rawtophtml) {
            $state.go('finstat-interimqualitativenote-list');
        };
        
        vm.openRunDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate = true;
        }

        vm.onCaptionTypeChanged = function () {
            getreportnames();
        };

        var getreportnames = function () {
            vm.viewModelHelper.apiGet('api/interimqualitativenote/getinterimqualitativenotebyType/' + vm.interimqualitativeNotes.ReportType,
                null,
                function (result) {

                    vm.reportNames = result.data;
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
