/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .factory("closedPeriodSearchService",
                    ['$rootScope', 'viewModelHelper',
                        closedPeriodSearchService]);

    function closedPeriodSearchService($rootScope, viewModelHelper) {

        var searchModel = { solutionId: 0, year:0 };

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
