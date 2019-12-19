/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("UserProfileListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        UserProfileListController]);

    function UserProfileListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'userprofile-list-view';
        vm.viewName = 'User Profile';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.userProfile = {};
        vm.userRoles = [];
        vm.solutionRunDates = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                getUserProfile();
                getUserSolutionRunDates();
                getUserRoles();
               
            }
        }

   
        var InitialView = function () {
            //InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#userProfileTable').length > 0) {
                    var exportTable = $('#userProfileTable').DataTable({
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

        var getUserProfile = function () {
            vm.viewModelHelper.apiGet('api/account/getuserprofile', null,
                  function (result) {
                      vm.userProfile = result.data;

                      //updateaccount();

                      //toastr.success('User data loaded, ready for modifiation.', 'Fintrak');
                  },
                   function (result) {
                       toastr.error('Fail to load user data', 'Fintrak');
                   }, null);
        }

        var getUserSolutionRunDates = function () {
            vm.viewModelHelper.apiGet('api/solutionrundate/getsolutionrundatebylogin', null,
                  function (result) {
                      vm.solutionRunDates = result.data;
                     
                  },
                   function (result) {
                       toastr.error('Fail to load solution run dates', 'Fintrak');
                   }, null);
        }

        var getUserRoles = function () {
            vm.viewModelHelper.apiGet('api/userrole/getuserrolebylogin', null,
                  function (result) {
                      vm.userRoles = result.data;
                  },
                  function (result) {
                      toastr.error('Fail to load roles', 'Fintrak');
                  }, null);
        }

        var updateaccount = function () {
            vm.viewModelHelper.apiPost('api/account/updateaccount', vm.userProfile,
                  function (result) {
                  },
                   function (result) {
                       toastr.error('Fail to load user data', 'Fintrak');
                   }, null);
        }

        vm.uploadPhoto = function (input, control) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {

                    //Sets the Old Image to new New Image
                    //$('#photo-id').attr('src', e.target.result);
                    $('#' + control).attr('src', e.target.result);

                    //Create a canvas and draw image on Client Side to get the byte[] equivalent
                    var canvas = document.createElement("canvas");
                    var imageElement = document.createElement("img");

                    imageElement.setAttribute('src', e.target.result);
                    canvas.width = imageElement.width;
                    canvas.height = imageElement.height;
                    var context = canvas.getContext("2d");
                    context.drawImage(imageElement, 0, 0);
                    var base64Image = canvas.toDataURL("image/jpeg");

                    //Removes the Data Type Prefix 
                    //And set the view model to the new value
                    vm.userProfile.Photo = base64Image.replace(/data:image\/jpeg;base64,/g, '');

                    vm.viewModelHelper.apiPost('api/account/updateusersetupprofile', vm.userProfile,
                     function (result) {
                         
                     },
                     function (result) {
                         toastr.error(result.data, 'Fintrak');
                     }, null);
                }

                //Renders Image on Page
                reader.readAsDataURL(input.files[0]);
            }
        };

        initialize();
       
    }
}());
