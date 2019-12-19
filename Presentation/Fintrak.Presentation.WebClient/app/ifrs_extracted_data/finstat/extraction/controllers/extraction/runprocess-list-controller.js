/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RunProcessListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator', 'processService',
                        RunProcessListController]);

    function RunProcessListController($scope, $state, viewModelHelper, validator, processService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'runprocess-list-view';
        vm.viewName = 'Run Processes';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.templates = [];
        vm.processJobs = [];
        vm.processTriggers = [];

        vm.solutions = [];
        vm.clearHistory = [];
        vm.restartService = [];
        vm.servicestatus = '';
        vm.rservice = [];
        var serviceName = 'ProcessInstallerService';

        vm.solutionId = 1;
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.startDate = new Date();
        vm.endDate = new Date();
        vm.runTime = new Date();
        vm.solution = 0;

        vm.autoRefresh = 'Enable Refresh';

        var initialize = function () {

            if (vm.init === false) {
                loadSolutions();

                vm.loadProcesses();
                vm.getServiceStatus();
                vm.viewModelHelper.apiGet('api/runprocess/getcurrentjobs', null,
                   function (result) {
                       vm.processJobs = result.data;
                       InitialView();
                       vm.init === true;
                       toastr.success('Current jobs loaded successfully.', 'Fintrak');
                   },
                 function (result) {
                     toastr.error('Fail to load current jobs.', 'Fintrak');
                 }, null);
            }
        }

        var InitialView = function () {
            processService.setStatus(false);
            if (processService.getStatus()) {
                vm.autoRefresh = 'Disable Refresh';
                processService.startTimer(vm.loadJobs, 3000);
            }

            else {
                vm.autoRefresh = 'Enable Refresh';
                processService.stopTimer();
            }

            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#processTable').length > 0) {
                    var exportTable = $('#processTable').DataTable({
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

        vm.openStartDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedStartDate = true;
        }

        vm.openEndDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedEndDate = true;
        }

        vm.openRunTime = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunTime = true;
        }

        vm.runProcesses = function () {

            var processIds = [];

            angular.forEach(vm.templates, function (process) {
                if (process.CanRun) {
                    processIds.push(process.ProcessId);
                }
            });

            //Test 
            vm.viewModelHelper.modelIsValid = true;
            vm.viewModelHelper.modelErrors = [];

            vm.viewModelHelper.apiPost('api/runprocess/checkprocess', processIds,
              function (result) {

                  if (result.data === '"Ok"') {
                      //start process
                      //create job
                      var job = { Code: '', Status: 5, RunTime: vm.runTime };

                      vm.viewModelHelper.apiPost('api/runprocess/updateprocessjob', job,
                        function (result) {
                            job = result.data;

                            vm.viewModelHelper.apiPost('api/runprocess/startprocess/' + job.ProcessJobId + '/' + vm.runTime.toDateString(), processIds,
                                function (result) {
                                    vm.processJobs = result.data;
                                    vm.code = '';
                                    vm.processTriggers = [];
                                    toastr.success('Job started successfully.', 'Fintrak');
                                },
                                 function (result) {
                                     toastr.error('Fail to start job.', 'Fintrak');
                                 }, null);
                            toastr.success('Job updated successfully.', 'Fintrak');
                        },
                     function (result) {
                         toastr.error('Fail to update job.', 'Fintrak');
                     }, null);
                  }
                  else {
                      vm.viewModelHelper.modelIsValid = false;
                      vm.viewModelHelper.modelErrors.push('Unable to start job execution:');
                      vm.viewModelHelper.modelErrors.push(result.data);

                      var errorList = '';

                      angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                          errorList += error + '<br>';
                      });
                      toastr.error(errorList, 'Fintrak');
                  }
              },
              function (result) {
                  toastr.error('Fail to run job.', 'Fintrak');
              }, null);

          //  check();
        }

        vm.cancelProcesses = function () {
            vm.viewModelHelper.apiGet('api/runprocess/cancelprocessjob/' + vm.code, null,
                          function (result) {
                              vm.processJobs = result.data;
                              vm.code = '';
                              vm.processTriggers = [];
                              toastr.success('Job canceled.', 'Fintrak');
                          },
                          function (result) {
                              vm.code = '';
                              vm.processTriggers = [];
                              toastr.error('Fail to cancel job.', 'Fintrak');
                          }, null);
        }

        vm.loadJobs = function () {
            vm.viewModelHelper.apiGet('api/runprocess/getjobs', null,
                         function (result) {
                             vm.processJobs = result.data;
                             vm.code = '';
                             vm.processTriggers = [];
                             toastr.success('Job loaded successfully.', 'Fintrak');
                         },
                         function (result) {
                             vm.code = '';
                             vm.processTriggers = [];
                             toastr.error('Fail to load jobs.', 'Fintrak');
                         }, null);
        }

        vm.loadActiveJobs = function () {
            vm.viewModelHelper.apiGet('api/runprocess/getcurrentjobs', null,
                         function (result) {
                             vm.processJobs = result.data;
                             vm.code = '';
                             vm.processTriggers = [];
                             toastr.success('Current job loaded successfully.', 'Fintrak');
                         },
                         function (result) {
                             vm.code = '';
                             vm.processTriggers = [];
                             toastr.error('Fail to load current jobs.', 'Fintrak');
                         }, null);
        }

        vm.loadProcessTriggers = function (jobCode) {
            vm.viewModelHelper.apiGet('api/runprocess/getprocesstriggers/' + jobCode, null,
                         function (result) {
                             vm.processTriggers = result.data;
                             toastr.success('Triggers loaded successfully.', 'Fintrak');
                         },
                         function (result) {
                             vm.processTriggers = [];
                             toastr.error('Fail to load triggers.', 'Fintrak');
                         }, null);
        }

        var loadSolutions = function () {
            vm.viewModelHelper.apiGet('api/solution/availablesolutions', null,
                 function (result) {
                     vm.solutions = result.data;
                     vm.solution = result.data[0];

                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        vm.onJobRowChanged = function (job) {
            vm.code = job.Code;
            vm.loadProcessTriggers(job.Code);
        }

        vm.onTriggerRowChanged = function (trigger) {
            vm.remark = trigger.Remark;
            check();
        }

        var check = function () {
            if (vm.remark.includes("however"))
            {
                alert('Process successfully executed,however cannot proceed with the next Package');
            }
          
        }

        vm.loadProcesses = function () {
            vm.viewModelHelper.apiGet('api/runprocess/getrunprocesses/' + vm.solution, null,
                   function (result) {
                       vm.templates = result.data;
                       vm.solutionId = vm.solution;
                       vm.init === true;
                       //alert("Ok");
                   },
                   function (result) {
                       toastr.error('Fail to load process templates.', 'Fintrak');
                   }, null);
        }

        vm.getServiceStatus = function () {
            vm.viewModelHelper.apiGet('api/runprocess/getservicestatus/' + serviceName, null,
                   function (result) {
                       vm.servicestatus = result.data;

                       vm.init === true;
                       //alert("Ok");
                   },
                   function (result) {
                       toastr.error('Fail to get service status.', 'Fintrak');
                   }, null);
        }
        vm.setAutoRefresh = function () {
            var enableAutoRefresh = processService.getStatus();

            if (!enableAutoRefresh) {
                vm.autoRefresh = 'Disable Refresh';
                processService.startTimer(vm.loadJobs, 30000);
            }

            else {
                vm.autoRefresh = 'Enable Refresh';
                processService.stopTimer();
            }

        }
        vm.checkAll = function () {

            if (checkall.checked === true) {
                angular.forEach(vm.templates, function (process) {
                    process.CanRun = true;

                });
            }
            else {
                angular.forEach(vm.templates, function (process) {
                    process.CanRun = false;

                });

            }
        }
        vm.restartService = function () {
            vm.viewModelHelper.apiPost('api/runprocess/restartservice/' + serviceName, null,
          function (result) {
                             vm.rservice = result.data;
                             vm.getServiceStatus();
                             toastr.success('Service restarted successfully.', 'Fintrak');
                         },
                         function (result) {

                             toastr.error('Service failed to start.', 'Fintrak');
                         }, null);
        }

        vm.clearProcessHistory = function () {

            alert("This operation can be best Performed on the Application Server");

            if (vm.solutionId !== 1) {

                vm.viewModelHelper.apiGet('api/runprocess/clearprocesshistory/' + vm.solutionId, null,

                       function (result) {
                           reloadJobs();
                           toastr.error('Process history successfully cleared', 'Fintrak');
                       },
                       function (result) {
                           toastr.error('Fail to clear process history', 'Fintrak');
                          // vm.clearHistory = '';
                       }, null);
            }
            else if (vm.solutionId === 1) {

            }
            else
           {
               alert("Kindly select solution to perform this operation");
           }
        }

        var reloadJobs = function () {
            vm.viewModelHelper.apiGet('api/runprocess/getjobs', null,
                         function (result) {
                             vm.processJobs = result.data;
                             vm.code = '';
                             vm.processTriggers = [];
                             toastr.success('Job loaded successfully.', 'Fintrak');
                         },
                         function (result) {
                             vm.code = '';
                             vm.processTriggers = [];
                             toastr.error('Fail to load jobs.', 'Fintrak');
                         }, null);
        }
        initialize();
    }
}());
