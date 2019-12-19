/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .directive("akFileModel",
                    ['$parse',
                        akFileModel]);

    function akFileModel($parse) {
        return {
            restrict: "A",
            link: function (scope, element, attrs) {
                var model = $parse(attrs.akFileModel);
                var modelSetter = model.assign;
                element.bind("change", function () {
                    scope.$apply(function () {
                        modelSetter(scope, element[0].files[0]);
                    });
                });
            }
        };

    }
}());
