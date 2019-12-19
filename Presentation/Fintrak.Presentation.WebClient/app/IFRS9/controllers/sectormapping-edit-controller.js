/**
 * Created by Dara on 23/03/2018.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SectorMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SectorMappingEditController]);

    function SectorMappingEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'sectormapping-edit-view';
        vm.viewName = 'Sector Mapping'

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.sectormapping = {};
        vm.cbnsectors = [];
        vm.lgdsectors = [];
        vm.ccfsectors = [];
        vm.cbn = 'CBN';
        vm.lgd = 'LGD';
        vm.ccf = 'CCF';
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        //vm.sources = [
        //  { Id: 'CBN', Name: 'CBN SectorMappings' },
        //  { Id: 'CCF', Name: 'Bank CCF SectorMappings' },
        //  { Id: 'LGD', Name: 'Bank LGD SectorMappings' }
        //];

        var sectorRules = [];

        var setupRules = function () {
          
            sectorRules.push(new validator.PropertyRule("CBNSector", {
                required: { message: "CBNSector is required" }
            }));


            sectorRules.push(new validator.PropertyRule("LGDSectorMapping", {
                required: { message: "LGDSectorMapping is required" }
            }));


            sectorRules.push(new validator.PropertyRule("CCFMapping", {
                required: { message: "CCFMapping is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.SectorMapping_Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/sectormapping/getsectormapping/' + $stateParams.SectorMapping_Id, null,
                   function (result) {
                       vm.sectormapping = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.sector = { CBNSector: '', LGDSectorMapping: '', CCFMapping: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            loadSectors()
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.sectormapping, sectorRules);
            vm.viewModelHelper.modelIsValid = vm.sectormapping.isValid;
            vm.viewModelHelper.modelErrors = vm.sectormapping.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/sectormapping/updatesectormapping', vm.sectormapping,
               function (result) {
                   
                   $state.go('ifrs9-sector-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.sectormapping.errors;

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
                vm.viewModelHelper.apiPost('api/sectormapping/deletesectormapping', vm.sectormapping.SectorMapping_Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-sector-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-sector-list');
        };

        var loadSectors = function () {

            vm.viewModelHelper.apiGet('api/sector/getsectorsbysource/' + vm.cbn, null,
                               function (result) {
                                   vm.cbnsectors = result.data;

                               vm.viewModelHelper.apiGet('api/sector/getsectorsbysource/' + vm.lgd, null,
                               function (result) {
                                   vm.lgdsectors = result.data;

                                           vm.viewModelHelper.apiGet('api/sector/getsectorsbysource/' + vm.ccf, null,
                                           function (result) {
                                               vm.ccfsectors = result.data;
                                           },
                                                   function (result) {
                                                       toastr.error(result, 'Fintrak Error');
                                                   }, null);

                               },
                                       function (result) {
                                           toastr.error(result, 'Fintrak Error');
                                       }, null);

                    },
                            function (result) {
                                toastr.error(result, 'Fintrak Error');
                            }, null);
            
            
        }

        setupRules();
        initialize();



    }
}());
