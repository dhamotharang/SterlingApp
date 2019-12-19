/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .factory("UploadService",
                    ['$rootScope', 'viewModelHelper',
                        UploadService]);

    function UploadService($rootScope,viewModelHelper) {

        var uploadResults = [];

        var runUpload = function (csv) {
            if (csv.content !== null) {
                uploadResults = [];
                var csvModel = { UploadId: csv.uploadCode, Content: csv.content, Header: csv.header, Separator: csv.separator };
                viewModelHelper.apiPost('api/runupload/uploadcsvbycode', csvModel,
                      function (result) {
                          uploadResults = result.data;
                          $rootScope.$broadcast('uploadCompleted');
                      },
                      function (result) {
                          alert(result.data);
                      }, null);
            } else {
                alert('Please select the file to upload.');
            }
        }

        var getUploadResult = function () {
            return uploadResults;
        }

        return {
            runUpload: runUpload,
            getUploadResult: getUploadResult
        }
    }
}());
