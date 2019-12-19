var appMainModule = angular.module('appMain');

// this is a child controller of RegistrationViewModel
appMainModule.controller("RegistrationController", function ($scope, $http, $location, viewModelHelper, validator) {

    viewModelHelper.modelIsValid = true;
    viewModelHelper.modelErrors = [];

    $scope.company = {};

    var companyRules = [];
    var setupRules = function () {

        companyRules.push(new validator.PropertyRule("Code", {
            required: { message: "Company Code is required" }
        }));

        companyRules.push(new validator.PropertyRule("Name", {
            required: { message: "Company Name is required" }
        }));

        companyRules.push(new validator.PropertyRule("Address", {
            required: { message: "Company Address is required" }
        }));

        companyRules.push(new validator.PropertyRule("Country", {
            required: { message: "Country  is required" }
        }));

        companyRules.push(new validator.PropertyRule("Phone", {
            required: { message: "Company Phone is required" }
        }));

        companyRules.push(new validator.PropertyRule("Email", {
            required: { message: "Company Email is required" }
        }));

        companyRules.push(new validator.PropertyRule("ContactName", {
            required: { message: "Contact Name is required" }
        }));

        companyRules.push(new validator.PropertyRule("ContactPhone", {
            required: { message: "Contact Phone is required" }
        }));

        companyRules.push(new validator.PropertyRule("ContactEmail", {
            required: { message: "Contact Email is required" }
        }));
    }

    var initializeView = function () {
        $scope.company = { Id: 0, Code: '', Name: '', Address: '', Country: '', Phone: '', Email: '', Website: '', ContactName: '', ContactPhone: '', ContactEmail: '', Message: '', IsDemo: false, IsActivated: false, Date: new Date(), Active: false };
    }

    $scope.register = function () {
        //Validate
        validator.ValidateModel($scope.company, companyRules);

        viewModelHelper.apiPost('api/Companies', $scope.company,
            function (result) {
                alert('Registration successful');
            },
            function (result) {
                alert('Registration fail');
            }, null);
    }

    setupRules();
    initializeView();
});

