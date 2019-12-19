/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("QualitativeNoteEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        QualitativeNoteEditController]);

    function QualitativeNoteEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'qualitativenote-edit-view';
        vm.viewName = 'Qualitative Note';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.qualitativeNote = {};
        vm.topnote='';
        vm.bottomnote = '';
        vm.htmlContent = '';
        vm.htmlContentbottom = '';
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.refnotes = [];
        vm.openedRunDate = false;
        var qualitativenoteRules = [];

        var setupRules = function () {
          
            qualitativenoteRules.push(new validator.PropertyRule("RefNote", {
                required: { message: "RefNote is required" }
            }));

            qualitativenoteRules.push(new validator.PropertyRule("TopNotes", {
                required: { message: "Top Noteis required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.qualitativeNoteId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/qualitativenote/getqualitativenote/' + $stateParams.qualitativeNoteId, null,
                   function (result) {
                       vm.qualitativeNotes = result.data;
                       vm.htmlContent = vm.qualitativeNotes.TopNotes;
                       vm.htmlContentbottom = vm.qualitativeNotes.BottomNotes;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.qualitativeNote = { RefNote: '', TopNotes: '', BottomNotes: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getRefNotes();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.qualitativeNote, qualitativenoteRules);
            vm.viewModelHelper.modelIsValid = vm.qualitativeNote.isValid;
            vm.viewModelHelper.modelErrors = vm.qualitativeNote.errors;
            if (vm.viewModelHelper.modelIsValid) {
                var params = { RefNote: vm.qualitativeNotes.RefNote, TopNote: vm.htmlContent, BottomNote: vm.htmlContentbottom, RunDate: vm.qualitativeNotes.RunDate };
                vm.viewModelHelper.apiPost('api/qualitativenote/updatequalitativenote', params,
               function (result) {
                   
                   $state.go('finstat-qualitativenote-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.qualitativeNote.errors;

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
                vm.viewModelHelper.apiPost('api/qualitativenote/deletequalitativenote', vm.qualitativeNotes.QualitativeNoteId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-qualitativenote-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function (rawtophtml) {
            $state.go('finstat-qualitativenote-list');
        };

        var getRefNotes = function () {
            vm.viewModelHelper.apiGet('api/registry/getdistinctrefnotes', null,
                 function (result) {
                     vm.refnotes = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load refnotes.', 'Fintrak');
                 }, null);
        }
       
        vm.openRunDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate = true;
        }

        setupRules();
        initialize(); 
    }
}());
