var appMainModule = angular.module('appMain');

// this is a child controller of ControlCentreViewModel
appMainModule.controller("ControlCentreController", function ($scope, $http, $location,$window, viewModelHelper, validator) {

    viewModelHelper.modelIsValid = true;
    viewModelHelper.modelErrors = [];

    $scope.companies = [];

    $scope.demoCaption = 'Start Demo';
    $scope.activationCaption = 'Activate';
      
    var initializeView = function () {
        viewModelHelper.apiGet('api/Companies', null,
          function (result) {
              $scope.companies = result.data;
          });
    }

    $scope.demo = function (company) {
        var confirm = false;
        confirm = $window.confirm(' Are you sure you want to perform  this opration on this company.');

        if (confirm) {
            $http.put('api/Companies/1', company)
           .success(function (data, status, headers) {

               if (company.IsDemo) {
                   company.IsDemo = false;
                   company.DemoMode = 'Start Demo';
                   alert('Company demo deactivation successful.');
               } else {
                   company.IsDemo = true;
                   company.DemoMode = 'End Demo';
                   alert('Company demo activation successful.');
               }

           })
           .error(function (data, status, header, config) {

           });
        }
       
    }

    $scope.activation = function (company) {
        var confirm = false;
        confirm = $window.confirm(' Are you sure you want to perform  this opration on this company.');

        if (confirm) {
            $http.put('api/Companies/2', company)
           .success(function (data, status, headers) {

               if (company.IsActivate) {
                   company.IsActivate = false;
                   company.ActivationMode = 'Activate';
                   alert('Company deactivation successful.');
               } else {
                   company.IsActivate = true;
                   company.ActivationMode = 'Deactivate';
                   alert('Company activation successful.');
               }

           })
           .error(function (data, status, header, config) {

           });
        }

    }

    initializeView();
});

