/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IFRSReportPackViewerListController",
                    ['$scope', '$window', '$sce', '$state', 'viewModelHelper', 'validator',
                        IFRSReportPackViewerListController]);
    //SetupListController
    function IFRSReportPackViewerListController($scope, $window,$sce, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'ifrsreportpackviewer-list-view';
        vm.viewName = 'IFRS Report Pack Viewer';
        // selected fruits
        vm.src = '';
        vm.srr = '',//$sce.trustAsResourceUrl('http://pi360.fintrakonline.com/ReportServer_FINTRAKSQL/Pages/ReportViewer.aspx?%2fIFRS_PI360%2f');
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];


        vm.ifrsreportpackviewer = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            intializeLookUp();
            vm.srr = $sce.trustAsResourceUrl('http://pi360.fintrakonline.com/ReportServer_FINTRAKSQL/Pages/ReportViewer.aspx?%2fIFRS_PI360%2f');
            
        }
        var intializeLookUp = function () {
            loadReportPacks();           
            loadRunDates();
        }
      var  loadReportPacks = function () {
            vm.viewModelHelper.apiGet('api/ifrsreportpackviewer/availableifrsreportpack', null,
                function (result) {
                    vm.ifrsreportpackviewer = result.data;
                },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
        }
        
     var loadRunDates = function () {
            vm.viewModelHelper.apiGet('api/ifrsreportpackviewer/availablerundate', null,
                function (result) {
                    vm.reportdate = result.data;
                },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
     }

     vm.loadSelectedReport = function () {
         var params = { ReportName: vm.ifrsreportpackviewer.ReportDescription, RunDate: vm.reportdate.RunDate };
         vm.viewModelHelper.apiPost('api/ifrsreportpackviewer/returnreporturl/',  params,
                   function (result) {
                       vm.src = result.data;
                       var str=vm.src
                       str = str.replace(/^"(.*)"$/, '$1');
                     //  alert(str);

                       vm.srr = $sce.trustAsResourceUrl(str);
                     //  alert(vm.srr);
                    $state.go('ifrs-reportpackviewer-list');
                   },
                  function (result) {
                      toastr.error(result.data, 'Fintrak Error');
                  }, null);
     }

        initialize();
    }
}());
