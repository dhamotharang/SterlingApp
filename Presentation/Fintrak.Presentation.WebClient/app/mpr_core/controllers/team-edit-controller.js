/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        TeamEditController]);

    function TeamEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'team-edit-view';
        vm.viewName = 'Team';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.team = {};
        vm.teamClassificationMaps = [];

        vm.teamDefinitions = [];
        vm.teams = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.showChildren = false;
        vm.canUseStaffId = false;

        var teamRules = [];
        vm.moduleOwners = [
           { Id: 1, Name: 'Generic' },
           { Id: 2, Name: 'MPR' },
            { Id: 3, Name: 'Budget' }
        ];

        vm.periods = [
         { Id: 1 },
         { Id: 2 },
         { Id: 3 },
         { Id: 4 },
         { Id: 5 },
         { Id: 6 },
         { Id: 7 },
         { Id: 8 },
         { Id: 9 },
         { Id: 10 },
         { Id: 11 },
         { Id: 12 }
        ];

        var setupRules = function () {

            teamRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            teamRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            teamRules.push(new validator.PropertyRule("DefinitionCode", {
                required: { message: "Team definition is required" }
            }));

            teamRules.push(new validator.PropertyRule("ModuleOwnerType", {
                required: { message: "Module Owner is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.teamId !== 0) {
                    vm.viewModelHelper.apiGet('api/team/getteamwithchildren/' + $stateParams.teamId, null,
                   function (result) {
                       vm.team = result.data.Team;
                       vm.teamClassificationMaps = result.data.Classifications;
                       vm.teamDefinition = result.data.Definition;

                       if (vm.teamDefinition.CanClassified)
                           vm.showChildren = true;

                       if (vm.teamDefinition.CanUseStaffId)
                           vm.canUseStaffId = true;

                       getTeams(vm.team.DefinitionCode);

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.team = { Name: '', Code: '', DefinitionCode: '', StaffId: '', Active: true, Period: 0 };
            }
        }

        var intializeLookUp = function () {
            getTeamDefinitions();
            getStaffs();
        }

        var initialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#teamClassificationMapTable').length > 0) {
                    var exportTable = $('#teamClassificationMapTable').DataTable({
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
            validator.ValidateModel(vm.team, teamRules);
            vm.viewModelHelper.modelIsValid = vm.team.isValid;
            vm.viewModelHelper.modelErrors = vm.team.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/team/updateteam', vm.team,
               function (result) {

                   $state.go('mpr-team-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.team.errors;

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
                vm.viewModelHelper.apiPost('api/team/deleteteam', vm.team.TeamId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-team-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-team-list');
        };

        vm.onTeamDefinitionChanged = function (definition) {
            getTeams(definition);
        }

        var getTeamDefinitions = function () {
            vm.viewModelHelper.apiGet('api/teamdefinition/availableteamdefinitions', null,
                 function (result) {
                     vm.teamDefinitions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getStaffs = function () {
            vm.viewModelHelper.apiGet('api/staffs/availablestaff', null,
                 function (result) {
                     vm.staffs = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getTeams = function (defintion) {
            vm.viewModelHelper.apiGet('api/team/getparentteams/' + defintion, null,
                 function (result) {
                     vm.teams = result.data;
                 },
                 function (result) {
                     vm.teams = [];
                     toastr.error(result.data, 'Fintrak');

                 }, null);
        }

        setupRules();
        initialize();
    }
}());
