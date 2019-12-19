/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .factory("customerCheckSearchService",
                    ['$rootScope', 'viewModelHelper',
                        customerCheckSearchService]);

    function customerCheckSearchService($rootScope, viewModelHelper) {

        var searchModel = { groupId: '' };

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
