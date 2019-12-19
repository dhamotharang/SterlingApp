/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .factory("glService",
                    ['$rootScope','viewModelHelper',
                        glService]);

    function glService($rootScope,viewModelHelper) {
        var gls = [];
        var selectedGL = '';
   
        var initialize = function () {
            if (gls.length === 0) {
                viewModelHelper.apiGet('api/glmapping/availableglMappings', null,
               function (result) {
                   gls = result.data;
               },
               function (result) {
                   alert("Fail");
               }, null);
            }
        }

        function getGLs() {
            return gls;
        }

        function getSelectedGL() {
            return selectedGL;
        }

        function setSelectedGL(gl) {
            selectedGL = gl;
            $rootScope.$broadcast('selected-gl-changed');
        }


        initialize();

        return {
            getGLs: getGLs,
            getSelectedGL: getSelectedGL,
            setSelectedGL: setSelectedGL
        }
    }
}());
