/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .factory("ExcelService",
                    ['$q','$rootScope',
                        ExcelService]);

    function ExcelService($q, $rootScope) {
        
        var service = function (data) {
            angular.extend(this, data);
        }

        service.readFile = function (file, readCells, toJSON) {
            var deferred = $q.defer();

            XLSXReader(file, readCells, toJSON, function (data) {
                $rootScope.$apply(function () {
                    deferred.resolve(data);
                });
            });

            return deferred.promise;
        }

        return service;
    }
}());
