/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RunExtractionListController",
                    ['$scope', '$state', '$filter', 'viewModelHelper', 'validator', 'extractionService',
                        RunExtractionListController]);

    function RunExtractionListController($scope, $state, $filter, viewModelHelper, validator, extractionService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'runextraction-list-view';
        vm.viewName = 'Run Extractions';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.templates = [];
        vm.extractionJobs = [];
        vm.solutionId = 1;
        vm.extractionTriggers = [];
        vm.solutions = [];
        var serviceName = 'ExtractionInstallerService';
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.startDate = new Date();
        vm.endDate = new Date();
        vm.runTime = new Date(1970, 0, 1, 14, 57, 0);
        vm.solution = 0;

        vm.autoRefresh = 'Enable Refresh';

        var initialize = function () {

            if (vm.init === false) {

                loadSolutions();

                vm.loadExtraction();
                vm.getServiceStatus();
                vm.viewModelHelper.apiGet('api/runextraction/getcurrentjobs', null,
                   function (result) {
                       vm.extractionJobs = result.data;
                       InitialView();
                       vm.init === true;
                       toastr.success('Current jobs loaded succ essfully.', 'Fintrak');
                   },
                 function (result) {
                     toastr.error('Fail to load current jobs.', 'Fintrak');
                 }, null);

                //vm.viewModelHelper.apiGet('api/solutionrundate/getsolutionrundatebysolution/IFRS', null,
                vm.viewModelHelper.apiGet('api/solutionrundate/getsolutionrundatebyloginbydefault', null,
                  //function (result) {
                  function (result) {
                      var dateAsString = $filter('date')(new Date(), result.data);
                      vm.startDate = dateAsString
                      vm.endDate = dateAsString
                      //vm.startDate = new Date(result.data.RunDate)//dateAsString
                      // vm.endDate= new Date(result.data.RunDate)//dateAsString;
                  },
                function (result) {
                    toastr.error('Fail to load soution run dates.', 'Fintrak');
                }, null);
            }
        }

        var InitialView = function () {
            extractionService.setStatus(false);
            if (extractionService.getStatus()) {
                vm.autoRefresh = 'Disable Refresh';
                extractionService.startTimer(vm.loadJobs, 3000);
            }

            else {
                vm.autoRefresh = 'Enable Refresh';
                extractionService.stopTimer();
            }
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#extractionTable').length > 0) {
                    var exportTable = $('#extractionTable').DataTable({
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

        vm.runExtractions = function () {

            //angular.forEach(vm.templates, function (template) {
            //    angular.forEach(template.Extractions, function (extraction) {
            //        if (extraction.CanRun) {
            //            vm.viewModelHelper.apiPost('api/runextraction/runextraction/' + extraction.Extraction.ExtractionId + '/' + startDate + '/' + endDate + '/' + runTime, null,
            //              function (result) {
            //                  vm.extractionTriggers = result.data;
            //                  InitialGrid();
            //              },
            //              function (result) {

            //              }, null);
            //        }
            //    });
            //});

            var extractionIds = [];

            angular.forEach(vm.templates, function (extraction) {
                if (extraction.CanRun) {
                    extractionIds.push(extraction.ExtractionId);
                }
            });

            //Test 
            vm.viewModelHelper.modelIsValid = true;
            vm.viewModelHelper.modelErrors = [];

            vm.viewModelHelper.apiPost('api/runextraction/checkextraction/' + vm.startDate.toDateString() + '/' + vm.endDate.toDateString(), extractionIds,
              function (result) {

                  if (result.data === '"Ok"') {
                      //start extraction
                      //create job
                      var job = { Code: '', Status: 5, StartDate: vm.startDate.toDateString(), EndDate: vm.endDate.toDateString(), RunTime: vm.runTime };

                      vm.viewModelHelper.apiPost('api/runextraction/updateextractionjob', job,
                        function (result) {
                            job = result.data;
                            vm.runTime = new Date(1970, 0, 1, 14, 57, 0);
                            vm.viewModelHelper.apiPost('api/runextraction/startextraction/' + job.ExtractionJobId + '/' + vm.startDate.toDateString() + '/' + vm.endDate.toDateString() + '/' + vm.startDate.toDateString(), extractionIds,
                                function (result) {
                                    vm.extractionJobs = result.data;
                                    vm.code = '';
                                    vm.extractionTriggers = [];
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

            //check();
        }

        vm.cancelExtractions = function () {
            vm.viewModelHelper.apiGet('api/runextraction/cancelextractionjob/' + vm.code + '/' + vm.startDate.toDateString() + '/' + vm.endDate.toDateString(), null,
                          function (result) {
                              vm.extractionJobs = result.data;
                              vm.code = '';
                              vm.extractionTriggers = [];
                              toastr.success('Job canceled.', 'Fintrak');
                          },
                          function (result) {
                              vm.code = '';
                              vm.extractionTriggers = [];
                              toastr.error('Fail to cancel job.', 'Fintrak');
                          }, null);
        }

        vm.loadJobs = function () {
            vm.viewModelHelper.apiGet('api/runextraction/getjobs/' + vm.startDate.toDateString() + '/' + vm.endDate.toDateString(), null,
                         function (result) {
                             vm.extractionJobs = result.data;
                             vm.code = '';
                             vm.extractionTriggers = [];
                             toastr.success('Job loaded successfully.', 'Fintrak');
                         },
                         function (result) {
                             vm.code = '';
                             vm.extractionTriggers = [];
                             toastr.error('Fail to load jobs.', 'Fintrak');
                         }, null);
        }

        vm.loadActiveJobs = function () {
            vm.viewModelHelper.apiGet('api/runextraction/getcurrentjobs', null,
                         function (result) {
                             vm.extractionJobs = result.data;
                             vm.code = '';
                             vm.extractionTriggers = [];
                             toastr.success('Current job loaded successfully.', 'Fintrak');
                         },
                         function (result) {
                             vm.code = '';
                             vm.extractionTriggers = [];
                             toastr.error('Fail to load current jobs.', 'Fintrak');
                         }, null);
        }

        vm.loadExtractionTriggers = function (jobCode) {
            vm.viewModelHelper.apiGet('api/runextraction/getextractiontriggers/' + jobCode, null,
                         function (result) {
                             vm.extractionTriggers = result.data;
                             toastr.success('Triggers loaded successfully.', 'Fintrak');
                         },
                         function (result) {
                             vm.extractionTriggers = [];
                             toastr.error('Fail to load triggers.', 'Fintrak');
                         }, null);
        }

        vm.onJobRowChanged = function (job) {
            vm.code = job.Code;
            vm.loadExtractionTriggers(job.Code);
        }

        vm.onTriggerRowChanged = function (trigger) {
            vm.remark = trigger.Remark;

        }

        //var check = function (trigger) {
            
        //    vm.remark = trigger.Remark;
        //    if (vm.remark === "Package successfully executed,however cannot proceed with the next process")
        //    {
        //       alert = $window.confirm(' Extraction has stop');
        //    }



        //}


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

        vm.loadExtraction = function () {
            vm.viewModelHelper.apiGet('api/runextraction/getrunextractions/' + vm.solution, null,

                   function (result) {
                       vm.templates = result.data;
                       vm.solutionId = vm.solution;
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error('Fail to load extraction history.', 'Fintrak');
                   }, null);
        }


        vm.setAutoRefresh = function () {
            var enableAutoRefresh = extractionService.getStatus();

            if (!enableAutoRefresh) {
                vm.autoRefresh = 'Disable Refresh';
                extractionService.startTimer(vm.loadJobs, 30000);
            }

            else {
                vm.autoRefresh = 'Enable Refresh';
                extractionService.stopTimer();
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

        //vm.clearHistory = function () {

        //    if (vm.solution !== null)
        //    {

        //        vm.viewModelHelper.apiGet('api/runextraction/clearextractionhistory/' + vm.solution, null,

        //               function (result) {
        //                   vm.clearHistory = result.data;
        //               },
        //               function (result) {
        //                   toastr.error('Fail to clear extraction history', 'Fintrak');
        //               }, null);
        //    }
        //    else {
        //        alert("Kindly select solution to perform this operation");
        //    }
        //}

        vm.clearExtractionHistory = function () {

            if (vm.solutionId !== 1) {

                vm.viewModelHelper.apiGet('api/runextraction/clearextractionhistory/' + vm.solutionId, null,

                       function (result) {
                           reloadJobs();
                           toastr.error('Extraction history successfully cleared', 'Fintrak');
                       },
                       function (result) {
                           toastr.error('Fail to clear Extraction history', 'Fintrak');

                       }, null);
            }
            else if (vm.solutionId === 1) {

            }
            else {
                alert("Kindly select solution to perform this operation");
            }
        }

        var reloadJobs = function () {
            vm.viewModelHelper.apiGet('api/runextraction/getrunextractions/' + vm.solution, null,

                   function (result) {
                       vm.templates = result.data;
                       vm.solutionId = vm.solution;
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error('Fail to load extraction history.', 'Fintrak');
                   }, null);
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

        initialize();
    }
}());
