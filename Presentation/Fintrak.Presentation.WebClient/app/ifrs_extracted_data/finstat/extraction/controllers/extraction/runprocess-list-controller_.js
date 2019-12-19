/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RunProcessListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        RunProcessListController]);

    function RunProcessListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'runprocess-list-view';
        vm.viewName = 'Run Processes';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.templates = [];
        //vm.processs = [];
        vm.processTriggers = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.startDate = new Date();
        vm.endDate = new Date();
        vm.runTime = new Date();

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/runprocess/getrunprocesses', null,
                   function (result) {
                       vm.templates = result.data;
                       //vm.processs = result.data.Processs;
                      
                       vm.init === true;
                       //alert("Ok");
                   },
                   function (result) {
                       alert("Fail");
                   }, null);

                vm.viewModelHelper.apiGet('api/runprocess/getcurrentprocesstriggers', null,
                   function (result) {
                       vm.processTriggers = result.data;
                       //vm.processs = result.data.Processs;
                       InitialView();
                       vm.init === true;
                       //alert("Ok");
                   },
                   function (result) {
                       alert("Fail");
                   }, null);
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                // Init
                //var spinner = $(".spinner").spinner();
                var table = $('#processTable').dataTable({
                    "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]]
                });

                var tableTools = new $.fn.dataTable.TableTools(table, {
                    "sSwfPath": "../app/vendors/DataTables/extensions/TableTools/swf/copy_csv_xls_pdf.swf",
                    "buttons": [
                        "copy",
                        "csv",
                        "xls",
                        "pdf",
                        { "type": "print", "buttonText": "Print me!" }
                    ]
                });
                $(".DTTT_container").css("float", "right");

                
            }, 50);
        }

        vm.openRunTime = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunTime = true;
        }



        vm.runProcesss = function () {
          
            //angular.forEach(vm.templates, function (template) {
            //    angular.forEach(template.Processs, function (process) {
            //        if (process.CanRun) {
            //            vm.viewModelHelper.apiPost('api/runprocess/runprocess/' + process.Process.ProcessId + '/' + startDate + '/' + endDate + '/' + runTime, null,
            //              function (result) {
            //                  vm.processTriggers = result.data;
            //                  InitialGrid();
            //              },
            //              function (result) {

            //              }, null);
            //        }
            //    });
            //});

           
            angular.forEach(vm.templates, function (process) {
                if (process.CanRun) {

                    //Test 
                    vm.viewModelHelper.modelIsValid = true;
                    vm.viewModelHelper.modelErrors = [];

                    vm.viewModelHelper.apiGet('api/runprocess/checkprocess/' + process.ProcessId , null,
                      function (result) {
                         
                          if (result.data === '"Ok"') {
                              //Insert
                              vm.viewModelHelper.apiGet('api/runprocess/startprocess/' + process.ProcessId + '/' + vm.runTime.toDateString(), null,
                                function (result) {
                                    vm.processTriggers = result.data;
                                },
                                function (result) {
                                    alert('Error');
                                }, null);
                          }
                          else
                          {
                              vm.viewModelHelper.modelIsValid = false;
                              vm.viewModelHelper.modelErrors.push(result.data + '<br>');
                          }  
                      },
                      function (result) {
                          alert('Error');
                      }, null);
                    
                }
            });
        }

        vm.cancelProcesss = function () {
            vm.viewModelHelper.apiGet('api/runprocess/cancelprocesses/' + vm.code, null,
                          function (result) {
                              vm.processTriggers = result.data;
                              
                          },
                          function (result) {
                              alert("Fail");
                          }, null);
        }

        vm.loadProcessTriggers = function () {
            vm.viewModelHelper.apiGet('api/runprocess/getprocesstriggers', null,
                         function (result) {
                             vm.processTriggers = result.data;
                             
                         },
                         function (result) {
                             alert("Fail");
                         }, null);
        }

        vm.onRowChanged = function (process) {
            vm.remark = process.Remark;
            vm.code = process.Code;
        }

        initialize(); 
    }
}());
