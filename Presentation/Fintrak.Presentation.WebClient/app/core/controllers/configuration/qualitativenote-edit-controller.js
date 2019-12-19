/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("QualitativeNoteEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator','textAngular',
                        QualitativeNoteEditController]);

    function QualitativeNoteEditController($scope, $window, $state, $stateParams, viewModelHelper, validator, textAngular) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'qualitativenote-edit-view';
        vm.viewName = 'Qulitative Note';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.qualitativenote = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        $scope.htmlContent = '<h2>Try me!</h2><p>textAngular is a super cool WYSIWYG Text Editor directive for AngularJS</p><p><b>Features:</b></p><ol><li>Automatic Seamless Two-Way-Binding</li><li style="color: blue;">Super Easy <b>Theming</b> Options</li><li>Simple Editor Instance Creation</li><li>Safely Parses Html for Custom Toolbar Icons</li><li>Doesn&apos;t Use an iFrame</li><li>Works with Firefox, Chrome, and IE8+</li></ol><p><b>Code at GitHub:</b> <a href="https://github.com/fraywing/textAngular">Here</a> </p>';

       
        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initialLookUp();

                //vm.viewModelHelper.apiGet('api/qualitativenote/getqualitativenote', null,
                //  function (result) {
                //      vm.qualitativenote = result.data;

                //      if (vm.qualitativenote === 'null')
                //          vm.qualitativenote = { Host: '', Email: '', Password: '', Active: true };

                      initialView();
                //      vm.init === true;
                    
                //      //
                //  },
                //   function (result) {
                //       toastr.error(result.data, 'Fintrak');
                //   }, null);
            }
        }

        var initialLookUp = function () {
            
        }

        var initialView = function () {
  
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.qualitativenote, qualitativenoteRules);
            vm.viewModelHelper.modelIsValid = vm.qualitativenote.isValid;
            vm.viewModelHelper.modelErrors = vm.qualitativenote.errors;
            //if (vm.viewModelHelper.modelIsValid) {
             
            //    vm.viewModelHelper.apiPost('api/qualitativenote/updatequalitativenote', vm.qualitativenote,
            //   function (result) {
                   
            //   },
            //   function (result) {
            //       toastr.error(result.data, 'Fintrak');
            //   }, null);
            //}
            //else
            //{
            //    vm.viewModelHelper.modelErrors = vm.qualitativenote.errors;

            //    var errorList = '';

            //    angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
            //        errorList += error + '<br>';
            //    });

            //    toastr.error(errorList, 'Fintrak');
            //}
                
        }

        setupRules();
        initialize(); 
    }
}());

//app = angular.module('fintrak', ['textAngular']);
//console.log(app);
//app.controller('QualitativeNoteController', ['$scope', function ($scope) {
//    $scope.content = 'llll';
//    $scope.htmlContent = '<h2>Try me!</h2><p>textAngular is a super cool WYSIWYG Text Editor directive for AngularJS</p><p><b>Features:</b></p><ol><li>Automatic Seamless Two-Way-Binding</li><li style="color: blue;">Super Easy <b>Theming</b> Options</li><li>Simple Editor Instance Creation</li><li>Safely Parses Html for Custom Toolbar Icons</li><li>Doesn&apos;t Use an iFrame</li><li>Works with Firefox, Chrome, and IE8+</li></ol><p><b>Code at GitHub:</b> <a href="https://github.com/fraywing/textAngular">Here</a> </p>';
//}]);