
var commonModule = angular.module('common', ['ngRoute', 'ngAnimate', 'ui.bootstrap']);

// Non-SPA views will use Angular controllers created on the appMainModule.
var appMainModule = angular.module('appMain', ['common']);

// SPA-views will attach to their own module and use their own data-ng-app and nested controllers.
// Each MVC-delivered top-spa-level view will link its needed JS files.

// Services attached to the commonModule will be available to all other Angular modules.

commonModule.factory('viewModelHelper', function ($http, $q) {
    return PI360.viewModelHelper($http, $q);
});

commonModule.factory('validator', function () {
    return valJs.validator();
});

(function (cr) {
    var viewModelHelper = function ($http, $q) {
        
        var self = this;

        self.modelIsValid = true;
        self.modelErrors = [];
        self.isLoading = false;
        self.promise = null;

        self.apiGet = function (uri, data, success, failure, always) {
            self.isLoading = true;
            self.modelIsValid = true;
            self.promise = $http.get('http://localhost:5153/' + uri, data)
                .then(function (result) {
                    success(result);
                    if (always != null)
                        always();
                    self.isLoading = false;
                }, function (result) {
                    if (failure == null) {
                        if (result.status != 400)
                            self.modelErrors = [result.status + ':' + result.statusText + ' - ' + result.data.Message];
                        else
                            self.modelErrors = [result.data.Message];
                        self.modelIsValid = false;
                    }
                    else
                        failure(result);
                    if (always != null)
                        always();
                    self.isLoading = false;
                });
        }

        self.apiPost = function (uri, data, success, failure, always) {
            self.isLoading = true;
            self.modelIsValid = true;
            self.promise = $http.post('http://localhost:5153/' + uri, data)
                .then(function (result) {
                    success(result);
                    if (always != null)
                        always();
                    self.isLoading = false;
                }, function (result) {
                    if (failure == null) {
                        if (result.status != 400)
                            self.modelErrors = [result.status + ':' + result.statusText + ' - ' + result.data.Message];
                        else
                            self.modelErrors = [result.data.Message];
                        self.modelIsValid = false;
                    }
                    else
                        failure(result);
                    if (always != null)
                        always();
                    isLoading = false;
                });
        }

        return this;
    }
    cr.viewModelHelper = viewModelHelper;
}(window.PI360));

(function (cr) {
    var mustEqual = function (value, other) {
        return value == other;
    }
    cr.mustEqual = mustEqual;
}(window.PI360));

// ***************** validation *****************

window.valJs = {};

(function (val) {
    var validator = function () {

        var self = this;

        self.PropertyRule = function (propertyName, rules) {
            var self = this;
            self.PropertyName = propertyName;
            self.Rules = rules;
        };

        self.ValidateModel = function (model, allPropertyRules) {
            var errors = [];
            var props = Object.keys(model);
            for (var i = 0; i < props.length; i++) {
                var prop = props[i];
                for (var j = 0; j < allPropertyRules.length; j++) {
                    var propertyRule = allPropertyRules[j];
                    if (prop == propertyRule.PropertyName) {
                        var propertyRules = propertyRule.Rules;

                        var propertyRuleProps = Object.keys(propertyRules);
                        for (var k = 0; k < propertyRuleProps.length; k++)
                        {
                            var propertyRuleProp = propertyRuleProps[k];
                            if (propertyRuleProp != 'custom') {
                                var rule = rules[propertyRuleProp];
                                var params = null;
                                if (propertyRules[propertyRuleProp].hasOwnProperty('params'))
                                    params = propertyRules[propertyRuleProp].params;
                                var validationResult = rule.validator(model[prop], params);
                                if (!validationResult) {
                                    errors.push(getMessage(prop, propertyRuleProp, rule.message));
                                }
                            }
                            else {
                                var validator = propertyRules.custom.validator;
                                var value = null;
                                if (propertyRules.custom.hasOwnProperty('params')) {
                                    value = propertyRules.custom.params;
                                }
                                var result = validator(model[prop], value());
                                if (result != true) {
                                    errors.push(getMessage(prop, propertyRules.custom, 'Invalid value.'));
                                }
                            }
                        }
                    }
                }
            }

            model['errors'] = errors;
            model['isValid'] = (errors.length == 0);
        }

        var getMessage = function (prop, rule, defaultMessage) {
            var message = '';
            if (rule.hasOwnProperty('message'))
                message = rule.message;
            else
                message = prop + ': ' + defaultMessage;
            return message;
        }

        var rules = [];

        var setupRules = function () {

            rules['required'] = {
                validator: function (value, params) {
                    if(typeof(value)  === "undefined") 
                        return false;
                    else
                    return !(value.toString().trim() === '');
                },
                message: 'Value is required.'
            };
            rules['notZero'] = {
                validator: function (value, params) {
                    return !(value === 0);
                },
                message: 'Value is must be greater than zero.'
            };
            rules['mustBeDate'] = {
                validator: function (value, params) {
                    return (isDate(value));
                },
                message: 'Value is must be a valid date.'
            };
            rules['mustBeNumeric'] = {
                validator: function (value, params) {
                    return (isNumber(value));
                },
                message: 'Value is must be a valid number.'
            };
            rules['minLength'] = {
                validator: function (value, params) {
                    if (typeof (value) === "undefined")
                        return false;
                    else
                    return !(value.toString().trim().length < params);
                },
                message: 'Value does not meet minimum length.'
            };
            rules['pattern'] = {
                validator: function (value, params) {
                    var regExp = new RegExp(params);

                    if (typeof (value) === "undefined")
                        return false;
                    else
                    return !(regExp.exec(value.trim()) === null);
                },
                message: 'Value must match regular expression.'
            };
        }

        function isDate(sDate) {
            var scratch = new Date(sDate);
            if (scratch.toString() === 'NaN' || scratch.toString() === 'Invalid Date') {
                return false;
            } else {
                return true;
            }
        }

        function isNumber(n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        }

        setupRules();

        return this;
    }
    val.validator = validator;
}(window.valJs));
