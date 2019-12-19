/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("GLDialogController",
                    ['$scope', 'viewModelHelper', 'glService',
                        GLDialogController]);

    function GLDialogController($scope, viewModelHelper, glService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        $scope.parentController = $scope.$parent;

        $scope.gls = [];
        $scope.selectedGL = {};

        var initialize = function () {
            $scope.gls = glService.getGLs();
            //vm.viewModelHelper.apiGet('api/glmapping/availableglMappings', null,
            //   function (result) {
            //       $scope.gls = result.data;
            //       InitialView();
            //   },
            //   function (result) {
            //       alert("Fail");
            //   }, null);

            InitialView();
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                // Init
                //var spinner = $(".spinner").spinner();
                var table = $('#glDialogTable').dataTable({
                    "lengthMenu": [[10, 20, 50, 100, -1], [10, 20, 50, 100, "All"]]
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

                if ($('#demo-checkbox-radio').length <= 0) {
                    $('input[type="checkbox"]:not(".switch")').iCheck({
                        checkboxClass: 'icheckbox_minimal-grey',
                        increaseArea: '20%' // optional
                    });

                    $('input[type="radio"]:not(".switch")').iCheck({
                        radioClass: 'iradio_minimal-grey',
                        increaseArea: '20%' // optional
                    });
                }

                //BEGIN CHECKBOX TABLE
                $('.checkall').on('ifChecked ifUnchecked', function (event) {
                    if (event.type == 'ifChecked') {
                        $(this).closest('table').find('input[type=checkbox]').iCheck('check');
                    } else {
                        $(this).closest('table').find('input[type=checkbox]').iCheck('uncheck');
                    }
                });
                //END CHECKBOX TABLE
            }, 50);
        }

        $scope.select = function (gl) {
            $scope.selectedGL = gl;
            glService.setSelectedGL(gl);
            $scope.$close();
        }

        $scope.accept = function () {
            $scope.selectedGL = gl;
            glService.setSelectedGL(gl);
            $scope.$close();
        }

        initialize(); 
    }
}());
