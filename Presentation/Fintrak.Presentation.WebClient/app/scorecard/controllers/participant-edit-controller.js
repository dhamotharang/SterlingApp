/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("KPIParticipantEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        KPIParticipantEditController]);

    function KPIParticipantEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Scorecard';
        vm.view = 'participant-edit-view';
        vm.viewName = 'KPI Participant';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.participant = {};

        vm.kpis = [];
        vm.staffs = [];
        vm.statuses = [
            {Id:1 , Name:'Include'},
             { Id: 2, Name: 'Excempt' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var participantRules = [];

        var setupRules = function () {
          
            participantRules.push(new validator.PropertyRule("Period", {
                notZero: { message: "Period is required" }
            }));

            participantRules.push(new validator.PropertyRule("Status", {
                notZero: { message: "Status is required" }
            }));

            participantRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            participantRules.push(new validator.PropertyRule("KPICode", {
                required: { message: "KPI Code is required" }
            }));

            participantRules.push(new validator.PropertyRule("TeamClassificationCode", {
                required: { message: "Classification is required" }
            }));

            //participantRules.push(new validator.PropertyRule("StaffCode", {
            //    required: { message: "Staff Code is required" }
            //}));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.participantId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/participant/getparticipant/' + $stateParams.participantId, null,
                   function (result) {
                       vm.participant = result.data;

                       initialView();
                       vm.init = true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.participant = { Period: 0, Year: '',KPICode:'',StaffCode:'',Status: 1, Active: true };
            }
        }


        var intializeLookUp = function () {
            getKPIs();
            getClassifications();
            getStaffs();
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.participant, participantRules);
            vm.viewModelHelper.modelIsValid = vm.participant.isValid;
            vm.viewModelHelper.modelErrors = vm.participant.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/participant/updateparticipant', vm.participant,
               function (result) {
                   
                   $state.go('scd-participant-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.participant.errors;

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
                vm.viewModelHelper.apiPost('api/participant/deleteparticipant', vm.participant.ParticipantId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('scd-participant-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('scd-participant-list');
        };

        var getKPIs = function () {
            vm.viewModelHelper.apiGet('api/scdkpi/availablekpi', null,
                 function (result) {
                     vm.kpis = result.data;
                   
                 },
                 function (result) {
                     toastr.error('Fail to load metrics.', 'Fintrak');
                 }, null);
        }

        var getStaffs = function () {
            vm.viewModelHelper.apiGet('api/staff/availablestaffs', null,
                 function (result) {
                     vm.staffs = result.data;
                  
                 },
                 function (result) {
                     toastr.error('Fail to load staffs.', 'Fintrak');
                 }, null);
        }

        var getClassifications = function () {
            vm.viewModelHelper.apiGet('api/scdteamclassification/availableteamclassification', null,
                 function (result) {
                     vm.classifications = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Fail to load team classification.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
