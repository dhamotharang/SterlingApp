/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .factory("roleSearchService",
                    ['$rootScope', 'viewModelHelper',
                        roleSearchService]);

    function roleSearchService($rootScope, viewModelHelper) {

        var searchModel = { roleType: 1 };

        var getSearchModel = function () {
            return searchModel;
        }

        var setSearchModel = function (model) {
            searchModel = model;
        }

        return {
            getSearchModel: getSearchModel,
            setSearchModel: setSearchModel
        }
    }
}());
