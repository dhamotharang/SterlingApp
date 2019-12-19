/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .factory("extractionService",
                    ['$interval',
                        extractionService]);

    function extractionService($interval) {

        var timer = null;
        var enableAutoRefresh = false;

        var getStatus = function () {
            return enableAutoRefresh;
        }

        var setStatus = function (status) {
            enableAutoRefresh = status;
        }

        var startTimer = function (callback, duration) {
            enableAutoRefresh = true;
            timer = $interval(callback, duration);
        }

        var stopTimer = function () {
            enableAutoRefresh = false;
            if (angular.isDefined(timer)) {
                $interval.cancel(timer);
                timer = undefined;
            }
        }

        return {
            getStatus: getStatus,
            setStatus: setStatus,
            startTimer: startTimer,
            stopTimer: stopTimer
        }
    }
}());
