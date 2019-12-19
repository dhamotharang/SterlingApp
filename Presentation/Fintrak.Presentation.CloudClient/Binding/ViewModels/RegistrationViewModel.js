
var registrationModule = angular.module('registration', ['common'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when(PI360.rootPath + 'home/registercompany', { templateUrl: PI360.rootPath + 'Templates/RegisterCompany.html', controller: 'RegisterCompanyViewModel' });
        $routeProvider.when(PI360.rootPath + 'home/finishregistration', { templateUrl: PI360.rootPath + 'Templates/FinishRegistration.html', controller: 'FinishRegistrationViewModel' });
        $routeProvider.otherwise({ redirectTo: PI360.rootPath + 'home/registercompany' });
        $locationProvider.html5Mode(true);
    });

registrationModule.factory("RegistrationService", function () {
    var companyRegistration = null;

    return {
        getCompanyRegistration: function () {
            if (companyRegistration === null)
                companyRegistration = { Id:0, Code: '', Name: '', Address: '', Country: '',Phone:'', Email: '',Website:'',ContactName:'',ContactPhone:'',ContactEmail:'', Date: new Date(), Active: false };

            return companyRegistration;
        },
        setCompanyRegistration: function (item) {
            companyRegistration = item;
            return companyRegistration;
        }
    };
});

registrationModule.controller("RegistrationViewModel", function ($scope, $window, viewModelHelper) {

    $scope.viewModelHelper = viewModelHelper;
  
    $scope.previous = function () {
        $window.history.back();
    }
});

// this is a child controller of RegistrationViewModel
registrationModule.controller("RegisterCompanyViewModel", function ($scope, $http, $location, viewModelHelper, validator, RegistrationService) {

    viewModelHelper.modelIsValid = true;
    viewModelHelper.modelErrors = [];
    
    $scope.promise = viewModelHelper.promise;

    $scope.companyRegistration = RegistrationService.getCompanyRegistration();
 
    var companyRegistrationModelRules = [];

    var initializeView = function () {
       
        if (!$scope.companyRegistration)
            $scope.companyRegistration = { Id: 0, Active: false };
    }

    var setupRules = function () {

        companyRegistrationModelRules.push(new validator.PropertyRule("Code", {
            required: { message: "Company Code is required" }
        }));
        
        companyRegistrationModelRules.push(new validator.PropertyRule("Name", {
            required: { message: "Company Name is required" }
        }));

        companyRegistrationModelRules.push(new validator.PropertyRule("Address", {
            required: { message: "Company Address is required" }
        }));

        companyRegistrationModelRules.push(new validator.PropertyRule("Country", {
            required: { message: "Country  is required" }
        }));

        companyRegistrationModelRules.push(new validator.PropertyRule("Phone", {
            required: { message: "Company Phone is required" }
        }));

        companyRegistrationModelRules.push(new validator.PropertyRule("Email", {
            required: { message: "Company Email is required" }
        }));

        companyRegistrationModelRules.push(new validator.PropertyRule("ContactName", {
            required: { message: "Contact Name is required" }
        }));

        companyRegistrationModelRules.push(new validator.PropertyRule("ContactPhone", {
            required: { message: "Contact Phone is required" }
        }));

        companyRegistrationModelRules.push(new validator.PropertyRule("ContactEmail", {
            required: { message: "Contact Email is required" }
        }));
    }

    $scope.newCompanyRegistration = function () {
        $location.path(PI360.rootPath + 'home/registercompany');
    }

    $scope.saveCompanyRegistration = function () {
        //Validate
        validator.ValidateModel($scope.companyRegistration, companyRegistrationModelRules);
        viewModelHelper.modelIsValid = $scope.companyRegistration.isValid;
        viewModelHelper.modelErrors = $scope.companyRegistration.errors;
        if (viewModelHelper.modelIsValid) {

            viewModelHelper.apiPost('api/company/registercompany', $scope.companyRegistration,
           function (result) {
               RegistrationService.setCompanyRegistration(result.data)
               $location.path(PI360.rootPath + 'home/finishregistration');
           });
        }
        else
            viewModelHelper.modelErrors = $scope.companyRegistration.errors;
    }

    setupRules();
    initializeView();
});

// this is a child controller of RegistrationViewModel
registrationModule.controller("FinishRegistrationViewModel", function ($scope, $http, $location, viewModelHelper, validator, RegistrationService) {

    $scope.companyRegistration = RegistrationService.getCompanyRegistration();

    var initializeView = function () {
      
    }

    $scope.newCompanyRegistration = function () {
        $location.path(PI360.rootPath + 'home/registercompany');
    }

    initializeView();
});