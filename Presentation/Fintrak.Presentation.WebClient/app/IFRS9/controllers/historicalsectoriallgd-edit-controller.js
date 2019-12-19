/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("HistoricalSectorialLGDEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        HistoricalSectorialLGDEditController]);

    function HistoricalSectorialLGDEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'historicalsectoriallgd-edit-view';
        vm.viewName = 'Historical LGD Computation';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.PrevctlrStatus = true;
        vm.showpdbtn = true;
        vm.showlgdbtn = false;
        vm.historicalSectorialLGD = {};
        vm.pdstatus = false;
        vm.lgdstatus = false;       
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.status =  false;

        var historicalsectoriallgdRules = [];
        vm.distinctYear = [];
        vm.distinctPeriod = [];
        vm.CurYears = 'None';
        vm.CurPeriods = 'None';
        vm.PrevYears = 'None';
        vm.PrevPeriods = 'None';

        var setupRules = function () {

            historicalsectoriallgdRules.push(new validator.PropertyRule("SectorCode", {
                required: { message: "Sector is required" }
            }));

            historicalsectoriallgdRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            historicalsectoriallgdRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.historicalsectoriallgdId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/historicalsectoriallgd/gethistoricalsectoriallgd/' + $stateParams.historicalsectoriallgdId, null,
                   function (result) {
                       vm.historicalSectorialLGD = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                  //  alert(vm.PrevctlrStatus);
                    vm.historicalSectorialLGD = { SectorCode: '', Year: '', Period: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getYears()
            getPeriods()

        }

        var initialView = function () {

        }
        //vm.computepd = function () {
        //    var computeFlag = $window.confirm(' Are you sure you want to compute LGD for the selected Periods');
        //    if (computeFlag) {
        //        var params = {computationType:1, curYear: vm.CurYears, curPeriod: vm.CurPeriods, prevYear: vm.PrevYears, prevPeriod: vm.PrevPeriods };
        //        vm.viewModelHelper.apiPost('api/historicalsectoriallgd/computepd', params,
        //                  function (result) {
        //                      toastr.success('LGD successfully Computed.', 'Fintrak');
        //                      $state.go('ifrs9-historicalsectoriallgd-list');
        //                  },
        //                 function (result) {
        //                     toastr.error(result.data, 'Fintrak Error');
        //                 }, null);
        //    }
        //}
        vm.computelgd = function () {
            var computeFlag = $window.confirm(' Are you sure you want to compute LGD for the selected Period');
            if (computeFlag) {
                var params = { computationType: 2, curYear: vm.CurYears, curPeriod: vm.CurPeriods, prevYear: 0, prevPeriod: 0 };
                vm.viewModelHelper.apiPost('api/historicalsectoriallgd/computelgd', params,
                          function (result) {
                              toastr.success('LGD successfully Computed.', 'Fintrak');
                              $state.go('ifrs9-historicalsectoriallgd-list');
                          },
                         function (result) {
                             toastr.error(result.data, 'Fintrak Error');
                         }, null);
            }
        }

        vm.pd = function (stt) {
            vm.clearpd();
            if (stt === true) {

                vm.PrevctlrStatus === true;
                vm.showpdbtn = true;
                vm.showlgdbtn = false;
          
            }

            else {

            
            }
              
        }
        vm.lgd = function (stt) {
            vm.clearlgd();
            if (stt !== true) {
                vm.PrevctlrStatus === false;
                vm.showpdbtn = false;
                vm.showlgdbtn = true;
            }
          

        }
       
        vm.clearlgd = function () {
            vm.PrevctlrStatus = false;
            vm.showpdbtn = false;
            vm.showlgdbtn = true;
           // alert(vm.PrevctlrStatus)
        }
        vm.clearpd = function () {
            vm.PrevctlrStatus = true;
            vm.showpdbtn = true;
            vm.showlgdbtn = false;
           // alert(vm.PrevctlrStatus)
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/historicalsectoriallgd/deletehistoricalsectoriallgd', vm.historicalSectorialLGD.HistoricalSectorialLGDId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-historicalsectoriallgd-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrs9-historicalsectoriallgd-list');
        };

        var getYears = function () {
            vm.viewModelHelper.apiGet('api/historicalsectoriallgd/getyears', null,
                 function (result) {
                     vm.distinctCurYear = result.data;
                     vm.distinctPrevYear = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }
        var getPeriods = function () {
            vm.viewModelHelper.apiGet('api/historicalsectoriallgd/getperiods', null,
                 function (result) {
                     vm.distinctCurPeriod = result.data;
                     vm.distinctPrevPeriod = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize();
    }
}());
