/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MessagingSubscriptionEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MessagingSubscriptionEditController]);

    function MessagingSubscriptionEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'messagingsubscription-edit-view';
        vm.viewName = 'e-Messaging';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.messagingSubscription = {};
        vm.messagingSubscriptions = [];

        vm.recipients = [];
        vm.selectedId = '';
        //vm.Recipents=''
        //vm.Subjects=''
        //vm.eMessage=''
        //vm.recipients = 'User'
    
        $scope.selection = [];

        //vm.messagingSubscriptions = [
        //    {
        //        'Select': '',
        //        'Recipents': 'Infosys Technologies',
        //        'Subjects': 125000,
        //        'eMessage': 'Bangalore'
        //    },
        //            	{
        //            	    'Select': '',
        //            	    'Recipents': 'Cognizant Technologies',
        //            	    'Subjects': 100000,
        //            	    'eMessage': 'Bangalore'
        //            	},
	    //                	{
	    //                	    'Select': '',
	    //                	    'Recipents': 'Wipro',
	    //                	    'Subjects': 115000,
	    //                	    'eMessage': 'Bangalore'
	    //                	},
		//                    	{
		//                    	    'Select': '',
		//                    	    'Recipents': 'Tata Consultancy Services (TCS)',
		//                    	    'Subjects': 150000,
		//                    	    'eMessage': 'Bangalore'
		//                    	},
		//	                    	{
		//	                    	    'Select': '',
		//	                    	    'Recipents': 'HCL Technologies',
		//	                    	    'Subjects': 90000,
		//	                    	    'eMessage': 'Noida'
		//	                    	},

        //];


        //vm.addRow = function () {

        //    if (vm.Recipents !== '' && vm.Subjects !== '' && vm.eMessage !== '')
        //    {
        //        vm.recipients.push({ 'Recipents': vm.Recipents, 'Subjects': vm.Subjects, 'eMessage': vm.eMessage, 'FileType': vm.recipients.FileType, 'Select': vm.Select });
        //        vm.Select = '';
        //        vm.Recipents = '';
        //        vm.Subjects = '';
        //        vm.eMessage = '';
        //        vm.recipients.FileType = '';
        //    }
        //    else
        //    {
        //        vm.Select = '';
        //        vm.Recipents = '';
        //        vm.Subjects = '';
        //        vm.eMessage = '';
        //        vm.recipients.FileType = '';
        //    }
     
        //};

        //vm.removeRow = function (Recipents) {
        //    var index = -1;
        //    var comArr = eval(vm.messagingSubscriptions);
        //    for (var i = 0; i < comArr.length; i++) {
        //        if (comArr[i].Recipents === Recipents) {
        //            index = i;
        //            break;
        //        }
        //    }
        //    if (index === -1) {
        //        alert("Something gone wrong");
        //    }
        //    vm.messagingSubscriptions.splice(index, 1);
        //};

        vm.reportTypes = [];
        vm.rundates = [];
        vm.openDate = false;
        vm.Report = ''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.recipientType = '';
        vm.attachmentTypes = [
           { Id: 1, Name: 'MHTML' },
           { Id: 2, Name: 'PDF' },
           { Id: 3, Name: 'EXCEL' },
           { Id: 4, Name: 'WORD' }
        ];


        vm.recipientTypes = [
          { Id: 1, Name: 'Level' },
          { Id: 2, Name: 'User' }
        ];
        var messagingSubscriptionRules = [];

        var setupRules = function () {
          
           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initialLookUp();

                vm.viewModelHelper.apiGet('', null,
                  function (result) {
                      //vm.messagingSubscriptions = result.data[0];

                      if (vm.messagingSubscriptions === 'null')
                          //vm.messagingSubscriptions = {Recipents:'', Subjects: '' , eMessage: '',  Active: true };
                          vm.messagingSubscription = { Recipents: '', Subjects: '', eMessage: '', FileType: '', Select: '', Report: '', ReportID: vm.selectedId };
                          //      vm.Select = '';
                          //      vm.Recipents = '';
                          //       vm.Subjects = '';
                          //      vm.eMessage = '';
                          //      vm.recipients.FileType = '';
                          //      vm.Report = '';
                          //      vm.selectedId = '';


                    //var intializeLookUp = function () {
         
                    //    getTeamDefinitions();
                    //    getreportTypes();
                    //}

                      initialView();
                      vm.init === true;

                  },
                  function (result) {

                  }, null); 
            }
        }

        var initialLookUp = function () {
            getreportTypes();
            getrundates();
            
        }

        var initialView = function () {
  
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.messagingSubscription, messagingSubscriptionRules);
            vm.viewModelHelper.modelIsValid = vm.messagingSubscription.isValid;
            vm.viewModelHelper.modelErrors = vm.messagingSubscription.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.messagingSubscription.ReportID = vm.selectedId;
                vm.messagingSubscription.TriggeredBy = false;
                vm.viewModelHelper.apiPost('api/messagingSubscription/updatemessagingSubscription', vm.messagingSubscription,
               function (result) {
                   
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.messagingSubscription.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
                
        }

        vm.onRecipientTypeChanged = function () {
            vm.recipienttype();       
            $scope.selection = [];
        }

        //vm.OnrecipientsClick = function () {
        //    vm.addRow
        //}


        $scope.toggleSelection = function toggleSelection(RevenueId) {
            var idx = $scope.selection.indexOf(RevenueId);


            // is currently selected
            if (idx > -1) {
                $scope.selection.splice(idx, 1);
                vm.selectedId = $scope.selection.join(', ');
                //  alert(vm.selectedId)
            }

                // is newly selected
            else {
                $scope.selection.push(RevenueId);
                vm.selectedId = $scope.selection.join(', ');
                //alert(vm.selectedId)
            }
        };

        vm.recipienttype = function () {
            var recipients = vm.messagingSubscription.Recipents

            var url = 'api/messagingSubscription/getmessagingSubscriptionRecipients/' + recipients;
            vm.viewModelHelper.apiGet(url, null,
               function (result) {
                   vm.recipients = result.data;
                   toastr.success('Recipient loaded successfully.', 'Fintrak');
               },
               function (result) {
                   toastr.error('Fail to load Recipient', 'Fintrak Error');
               }, null);
        }


         var getreportTypes = function () {
             //var url = '';

             //if (vm.recipientType === 1)
             //    url = 'api/consolidatedtrialbalance/getconsolidatedtrialbalanceIFRS';
             //else
             //    url = 'api/consolidatedtrialbalance/getconsolidatedtrialbalanceGAAP';

             vm.viewModelHelper.apiGet('api/messagingsubscription/report', null,
             //vm.viewModelHelper.apiGet(url, null,
                 function (result) {
                     vm.reportTypes = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
         }


         var getrundates = function () {

             vm.viewModelHelper.apiGet('api/messagingsubscription/rundate', null,
                 function (result) {
                     vm.rundates = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
         }


         vm.date = function ($event) {
             $event.preventDefault();
             $event.stopPropagation();
             vm.openDate = true;
         }

        setupRules();
        initialize(); 
    }
}());
