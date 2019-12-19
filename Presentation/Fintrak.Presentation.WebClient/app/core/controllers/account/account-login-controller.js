(function ()
{
    "use strict";
    angular
        .module("fintrak.extra")
        .controller("AccountLoginController",
                    ['$http', AccountLoginController]);

    function AccountLoginController($http, $location)
    {

        var vm = this;

        vm.init = false;
        vm.accountModel = new Fintrak.AccountLoginModel();
        vm.returnUrl = '';
        vm.firstlogon = 'OK';    
        //var message = 0;
        vm.enableDefaultCompany = false;

        var initialize = function () {
            if (vm.init === false) {

                $http.get(Fintrak.rootPath + 'api/general/getgeneral', null)
              .then(function (result) {
                  vm.enableDefaultCompany = result.data.EnableCompanyDefaultLogin;
                  vm.accountModel.CompanyCode = result.data.DefaultCompanyCode;
              }, function (result) {
                  toastr.error(result.data, 'Fintrak');
              });

                initializeView();
                vm.init === true;
            }
        }

        var initializeView = function () {
        }

        vm.login = function (loginId) {
            //login with user's credentials
            ConfirmFirstLogon(loginId);

            if (vm.firstlogon === "OK" && vm.accountModel.Password !== "@password")
            {
                $http.post(Fintrak.rootPath + 'api/account/login', vm.accountModel)
                  .then(function (result) {
                      window.location.href = Fintrak.rootPath;
                  }, function (result) {
                      alert('Unable to login: ' + result.data);
                      toastr.error('Unable to login: ' + result.data, 'Fintrak');
                  })
            }
           
            else if (vm.firstlogon === "OK" && vm.accountModel.Password === "@password")
                {
                    //alert('Please change password ' + result.data);
                    $http.post(Fintrak.rootPath + 'api/account/login', vm.accountModel)
                       .then(function (result) {
                           window.location.href = Fintrak.rootPath;
                       }, function (result) {
                           alert('Unable to login: ' + result.data);
                           toastr.error('Unable to login: ' + result.data, 'Fintrak');
                       });
                }
            else
            {
                alert('Unable to login: ' + result.data);
                toastr.error('Unable to login: ' + result.data, 'Fintrak');
            }
      
        }

        var ConfirmFirstLogon = function (loginId) {
            //var url = 'api/account/userexist/' + loginId
            //vm.viewModelHelper.apiGet(url, null,
            $http.get(Fintrak.rootPath + 'api/account/userexist/' + loginId)
            .then(function (result) {
                vm.firstlogon = result.statusText;
            },
            function (result) {
                alert('Unable to login: ' + result.statusText);
                vm.firstlogon = result.statusText;
            });
        }

        initialize();
    }
   
    
}());
