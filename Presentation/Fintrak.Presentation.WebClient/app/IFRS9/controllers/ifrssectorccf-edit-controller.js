/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsSectorCCFEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IfrsSectorCCFEditController]);

    function IfrsSectorCCFEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrssectorccf-edit-view';
        vm.viewName = 'CCF by sector';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifrsSectorCCF = {};
        vm.sector = {};
        vm.sectors = [];
        vm.isEdit = false;
        //vm.cbnsectors = [];
        //vm.lgdsectors = [];
        //vm.ccfsectors = [];
        //vm.cbn = 'CBN';
        //vm.lgd = 'LGD';
        //vm.ccf = 'CCF';
        vm.type = 'CCF';
        vm.types = [
            { Id: 'CCF', Name: 'CCF' },
            { Id: 'LGD', Name: 'LGD' },
            { Id: 'CBN', Name: 'PD' },
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ifrsSectorCCFRules = [];

        vm.setSectorCode = function (ifrsSectorCCF) {
            vm.ifrsSectorCCF.code = ifrsSectorCCF.Code;
            vm.ifrsSectorCCF.sector = ifrsSectorCCF.Description;
        }

        var setupRules = function () {

            ifrsSectorCCFRules.push(new validator.PropertyRule("sector", {
                required: { message: "Sector is required" }
            }));

            //ifrsSectorCCFRules.push(new validator.PropertyRule("code", {
            //    required: { message: "Sector code is required" }
            //}));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups

                if ($stateParams.SectorId !== 0) {
                    vm.showChildren = true;
                    vm.isEdit = true;
                    vm.viewModelHelper.apiGet('api/ifrssectorccf/getIfrsSectorCCFById/' + $stateParams.SectorId, null,
                   function (result) {
                       vm.ifrsSectorCCF = result.data;
                       vm.type = vm.ifrsSectorCCF.type;
                       if (vm.ifrsSectorCCF.type === 'PD') {
                           vm.type = 'CBN';
                       }
                       intializeLookUp();

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    intializeLookUp();
                    vm.ifrsSectorCCF = { sector: '', /* code: '', ccf: 0,*/ Active: true };
            }
        }

        var intializeLookUp = function () {
            //vm.viewModelHelper.apiGet('api/ifrssectorccf/getAllSectorsForCCF', null,
            //       function (result) {
            //           vm.sectors = result.data;
            //       },
            //       function (result) {
            //           toastr.error(result.data, 'Fintrak');
            //       }, null);

            vm.loadSectors();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ifrsSectorCCF, ifrsSectorCCFRules);
            vm.viewModelHelper.modelIsValid = vm.ifrsSectorCCF.isValid;
            vm.viewModelHelper.modelErrors = vm.ifrsSectorCCF.errors;
            if (vm.viewModelHelper.modelIsValid) {
                vm.ifrsSectorCCF.type = vm.type
                if (vm.type === 'CBN') {
                    vm.ifrsSectorCCF.type = 'PD';
                }
                vm.viewModelHelper.apiPost('api/ifrssectorccf/updateIfrsSectorCCF', vm.ifrsSectorCCF,
               function (result) {

                   $state.go('ifrs9-ifrssectorccf-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.ifrsSectorCCF.errors;

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
                vm.viewModelHelper.apiPost('api/ifrssectorccf/deleteIfrsSectorCCF', vm.ifrsSectorCCF.SectorId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-ifrssectorccf-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrs9-ifrssectorccf-list');
        };

        vm.loadSectors = function () {

            vm.viewModelHelper.apiGet('api/sector/getsectorsbysource/' + vm.type, null,
                function (result) {
                    vm.sectors = result.data;
                },

                function (result) {
                    toastr.error(result, 'Fintrak Error');
                }, null);
        }

        //var loadLGDSectors = function () {

        //    vm.viewModelHelper.apiGet('api/sector/getsectorsbysource/' + vm.lgd, null,
        //        function (result) {
        //            vm.lgdsectors = result.data;
        //        },

        //        function (result) {
        //            toastr.error(result, 'Fintrak Error');
        //        }, null);
        //}

        //var loadPDSectors = function () {

        //    vm.viewModelHelper.apiGet('api/sector/getsectorsbysource/' + vm.cbn, null,
        //        function (result) {
        //            vm.cbnsectors = result.data;

        //        },
        //        function (result) {
        //            toastr.error(result, 'Fintrak Error');
        //        }, null);
        //}

        setupRules();
        initialize();
    }
}());
