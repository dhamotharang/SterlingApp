"use strict";

var commonModule = angular.module('fintrakCommon', ['ngRoute', 'ui.bootstrap']);

var App = angular.module('fintrak', ['ngRoute', 'ui.bootstrap', 'ui.router', 'oc.lazyLoad', 'ngResource', 'isteven-multi-select',
    'fintrakCommon', 'colorpicker.module', 'ngCsvImport', 'ngLoadingSpinner', 'ks.ngScrollRepeat', 'treeGrid',
    'ngIdleTimer', 'ngSanitize', 'ngCsv', 'ngJsonExportExcel', 'ngTable', 'ngInputDate', 'ng-fusioncharts', 'textAngular'])
;
//, 'moment-picker', 'textAngular'
App.config(function ($provide) {
    $provide.decorator("$exceptionHandler",
        ["$delegate",
            function ($delegate) {
                return function (exception, cause) {
                    exception.message = "Please contact the Help Desk! \n Message: " +
                        exception.message;
                    $delegate(exception, cause);
                    alert(exception.message);
                };
            }]);
});

//Http Intercpetor to check auth failures for xhr requests
App.config(['$httpProvider',
    function ($httpProvider) {
        $httpProvider.interceptors.push('httpErrorResponseInterceptor');
    }
]);
App.config(function ($stateProvider, $urlRouterProvider) {
    //
    // For any unmatched url, redirect to /state1
    //var rootUrl = '/ClientPortal/';
    var rootUrl = '';


    $urlRouterProvider.otherwise("/core-userprofile-list");
    //
    // Now set up the states
    $stateProvider

        .state('core-module-list', {
            url: "/core-module-list-view",
            templateUrl: rootUrl + 'app/core/views/module/module-list-view.html',
            controller: 'ModuleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-apps-list', {
            url: "/core-apps-list-view",
            templateUrl: rootUrl + 'app/core/views/module/apps-list-view.html',
            controller: 'AppListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-general-edit', {
            url: "/core-general-edit-view",
            templateUrl: rootUrl + 'app/core/views/configuration/general-edit-view.html',
            controller: 'GeneralEditController as vm'



        }).state('core-changepassword-edit', {
            url: "/core-changepassword-edit-view",
            templateUrl: rootUrl + 'app/core/views/account/changepassword-edit-view.html',
            controller: 'AccountChangePasswordController as vm'

        }).state('core-fiscalyear-list', {
            url: "/core-fiscalyear-list-view",
            templateUrl: rootUrl + 'app/core/views/configuration/fiscalyear-list-view.html',
            controller: 'FiscalYearListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.tableTools.min.js',
                            rootUrl + 'app/assets/js/plugins/datatable/dataTables.bootstrap.js']
                    });
                }]
            }
        }).state('core-fiscalyear-edit', {
            url: "/core-fiscalyear-edit-view/:fiscalyearId",
            templateUrl: rootUrl + 'app/core/views/configuration/fiscalyear-edit-view.html',
            controller: 'FiscalYearEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']

                    });
                }]
            }
        }).state('core-fiscalperiod-edit', {
            url: "/core-fiscalperiod-edit-view/:fiscalyearId?fiscalperiodId",
            templateUrl: rootUrl + 'app/core/views/configuration/fiscalperiod-edit-view.html',
            controller: 'FiscalPeriodEditController as vm'
        }).state('core-financialtype-list', {
            url: "/core-financialtype-list",
            templateUrl: rootUrl + 'app/core/views/configuration/financialtype-list-view.html',
            controller: 'FinancialTypeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-financialtype-edit', {
            url: "/core-financialtype-edit/:financialtypeId",
            templateUrl: rootUrl + 'app/core/views/configuration/financialtype-edit-view.html',
            controller: 'FinancialTypeEditController as vm'

        }).state('core-gldefinition-list', {
            url: "/core-gldefinition-list",
            templateUrl: rootUrl + 'app/core/views/configuration/gldefinition-list-view.html',
            controller: 'GLDefinitionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-gldefinition-edit', {
            url: "/core-gldefinition-edit/:gldefinitionId",
            templateUrl: rootUrl + 'app/core/views/configuration/gldefinition-edit-view.html',
            controller: 'GLDefinitionEditController as vm'

        }).state('core-chartofaccount-list', {
            url: "/core-chartofaccount-list",
            templateUrl: rootUrl + 'app/core/views/configuration/chartofaccount-list-view.html',
            controller: 'ChartOfAccountListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-chartofaccount-edit', {
            url: "/core-chartofaccount-edit/:chartofaccountId",
            templateUrl: rootUrl + 'app/core/views/configuration/chartofaccount-edit-view.html',
            controller: 'ChartOfAccountEditController as vm'
        }).state('core-currency-list', {
            url: "/core-currency-list-view",
            templateUrl: rootUrl + 'app/core/views/configuration/currency-list-view.html',
            controller: 'CurrencyListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-audittrail-list', {
            url: "/core-audittrail-list-view",
            templateUrl: rootUrl + 'app/core/views/configuration/audittrail-list-view.html',
            controller: 'AuditTrailListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-currency-edit', {
            url: "/core-currency-edit-view/:currencyId",
            templateUrl: rootUrl + 'app/core/views/configuration/currency-edit-view.html',
            controller: 'CurrencyEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

            //
        }).state('core-reportstatus-list', {
            url: "/core-reportstatus-list-view",
            templateUrl: rootUrl + 'app/core/views/configuration/reportstatus-list-view.html',
            controller: 'ReportStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('core-reportstatus-edit', {
            url: "/core-reportstatus-edit-view/:statusId",
            templateUrl: rootUrl + 'app/core/views/configuration/reportstatus-edit-view.html',
            controller: 'ReportStatusEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
            //
        }).state('core-currencyrate-edit', {
            url: "/core-currencyrate-edit-view/:currencyId?currencyrateId",
            templateUrl: rootUrl + 'app/core/views/configuration/currencyrate-edit-view.html',
            controller: 'CurrencyRateEditController as vm'
        }).state('core-producttype-list', {
            url: "/core-producttype-list",
            templateUrl: rootUrl + 'app/core/views/configuration/producttype-list-view.html',
            controller: 'ProductTypeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-producttype-edit', {
            url: "/core-producttype-edit/:producttypeId",
            templateUrl: rootUrl + 'app/core/views/configuration/producttype-edit-view.html',
            controller: 'ProductTypeEditController as vm'
        }).state('core-product-list', {
            url: "/core-product-list-view",
            templateUrl: rootUrl + 'app/core/views/configuration/product-list-view.html',
            controller: 'ProductListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-product-edit', {
            url: "/core-product-edit-view/:productId?code?name",
            templateUrl: rootUrl + 'app/core/views/configuration/product-edit-view.html',
            controller: 'ProductEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-producttypemapping-edit', {
            url: "/core-producttypemapping-edit-view/:productId?productcode?producttypemappingId",
            templateUrl: rootUrl + 'app/core/views/configuration/producttypemapping-edit-view.html',
            controller: 'ProductTypeMappingEditController as vm'
        }).state('core-role-list', {
            url: "/core-role-list",
            templateUrl: rootUrl + 'app/core/views/account/role-list-view.html',
            controller: 'RoleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-role-edit', {
            url: "/core-role-edit-view/:roleId",
            templateUrl: rootUrl + 'app/core/views/account/role-edit-view.html',
            controller: 'RoleEditController as vm'
        }).state('core-menurole-list', {
            url: "/core-menurole-list",
            templateUrl: rootUrl + 'app/core/views/accessibility/menurole-list-view.html',
            controller: 'MenuRoleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-company-list', {
            url: "/core-company-list",
            templateUrl: rootUrl + 'app/core/views/configuration/company-list-view.html',
            controller: 'CompanyListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-company-edit', {
            url: "/core-company-edit-view/:companyId",
            templateUrl: rootUrl + 'app/core/views/configuration/company-edit-view.html',
            controller: 'CompanyEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-branch-edit', {
            url: "/core-branch-edit-view/:companyId?branchId",
            templateUrl: rootUrl + 'app/core/views/configuration/branch-edit-view.html',
            controller: 'BranchEditController as vm'
        }).state('core-companymodule-list', {
            url: "/core-companymodule-list",
            templateUrl: rootUrl + 'app/core/views/configuration/companymodule-list-view.html',
            controller: 'CompanyModuleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-companymodule-edit', {
            url: "/core-companymodule-edit-view/:companymoduleId",
            templateUrl: rootUrl + 'app/core/views/configuration/companymodule-edit-view.html',
            controller: 'CompanyModuleEditController as vm'
        }).state('core-usersetup-list', {
            url: "/core-usersetup-list",
            templateUrl: rootUrl + 'app/core/views/account/usersetup-list-view.html',
            controller: 'UserSetupListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-usersetup-edit', {
            url: "/core-usersetup-edit/:usersetupId",
            templateUrl: rootUrl + 'app/core/views/account/usersetup-edit-view.html',
            controller: 'UserSetupEditController as vm'
        }).state('core-usermanager-list', {
            url: "/core-usermanager-list",
            templateUrl: rootUrl + 'app/core/views/account/usermanager-list-view.html',
            controller: 'UserManagerListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-usermanager-edit', {
            url: "/core-usermanager-edit/:usersetupId",
            templateUrl: rootUrl + 'app/core/views/account/usermanager-edit-view.html',
            controller: 'UserManagerEditController as vm'
        }).state('core-userprofile-list', {
            url: "/core-userprofile-list",
            templateUrl: rootUrl + 'app/core/views/account/userprofile-list-view.html',
            controller: 'UserProfileListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('core-errortracker-list', {
            url: "/core-errortracker-list",
            templateUrl: rootUrl + 'app/core/views/configuration/errortracker-list-view.html',
            controller: 'ErrorTrackerListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('core-activity-list', {
            url: "/core-activity-list",
            templateUrl: rootUrl + 'app/core/views/configuration/activity-list-view.html',
            controller: 'ActivityListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-activity-edit', {
            url: "/core-activity-edit-view/:activityId",
            templateUrl: rootUrl + 'app/core/views/configuration/activity-edit-view.html',
            controller: 'ActivityEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-packagesetup-edit', {
            url: "/core-packagesetup-edit-view",
            templateUrl: rootUrl + 'app/extraction/views/extraction/packagesetup-edit-view.html',
            controller: 'PackageSetupEditController as vm'
        }).state('core-solutionrundate-list', {
            url: "/core-solutionrundate-list",
            templateUrl: rootUrl + 'app/extraction/views/extraction/solutionrundate-list-view.html',
            controller: 'SolutionRunDateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-solutionrundate-edit', {
            url: "/core-solutionrundate-edit/:solutionrundateId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/solutionrundate-edit-view.html',
            controller: 'SolutionRunDateEditController as vm'
        }).state('core-closedperiod-list', {
            url: "/core-closedperiod-list",
            templateUrl: rootUrl + 'app/extraction/views/extraction/closedperiod-list-view.html',
            controller: 'ClosedPeriodListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-closedperiod-edit', {
            url: "/core-closedperiod-edit/:closedperiodId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/closedperiod-edit-view.html',
            controller: 'ClosedPeriodEditController as vm'

            //
        }).state('core-openclosedperiod-list', {
            url: "/core-openclosedperiod-list",
            templateUrl: rootUrl + 'app/extraction/views/extraction/openclosedperiod-list-view.html',
            controller: 'OpenClosedPeriodListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-openclosedperiod-edit', {
            url: "/core-openclosedperiod-edit/:closedperiodId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/openclosedperiod-edit-view.html',
            controller: 'OpenClosedPeriodEditController as vm'
            //




        }).state('core-closedperiodtemplate-list', {
            url: "/core-closedperiodtemplate-list",
            templateUrl: rootUrl + 'app/extraction/views/extraction/closedperiodtemplate-list-view.html',
            controller: 'ClosedPeriodTemplateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-closedperiodtemplate-edit', {
            url: "/core-closedperiodtemplate-edit/:closedperiodtemplateId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/closedperiodtemplate-edit-view.html',
            controller: 'ClosedPeriodTemplateEditController as vm'
        }).state('core-extraction-list', {
            url: "/core-extraction-list-view",
            templateUrl: rootUrl + 'app/extraction/views/extraction/extraction-list-view.html',
            controller: 'ExtractionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-extraction-edit', {
            url: "/core-extraction-edit-view/:extractionId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/extraction-edit-view.html',
            controller: 'ExtractionEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-extractionrole-edit', {
            url: "/core-extractionrole-edit-view/:extractionId?extractionroleId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/extractionrole-edit-view.html',
            controller: 'ExtractionRoleEditController as vm'
        }).state('core-runextraction-list', {
            url: "/core-runextraction-list-view",
            templateUrl: rootUrl + 'app/extraction/views/extraction/runextraction-list-view.html',
            controller: 'RunExtractionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-process-list', {
            url: "/core-process-list-view",
            templateUrl: rootUrl + 'app/extraction/views/extraction/process-list-view.html',
            controller: 'ProcessListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-process-edit', {
            url: "/core-process-edit-view/:processId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/process-edit-view.html',
            controller: 'ProcessEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-processrole-edit', {
            url: "/core-processrole-edit-view/:processId?processroleId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/processrole-edit-view.html',
            controller: 'ProcessRoleEditController as vm'
        }).state('core-runprocess-list', {
            url: "/core-runprocess-list-view",
            templateUrl: rootUrl + 'app/extraction/views/extraction/runprocess-list-view.html',
            controller: 'RunProcessListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-upload-list', {
            url: "/core-upload-list-view",
            templateUrl: rootUrl + 'app/extraction/views/extraction/upload-list-view.html',
            controller: 'UploadListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-upload-edit', {
            url: "/core-upload-edit-view/:uploadId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/upload-edit-view.html',
            controller: 'UploadEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-uploadrole-edit', {
            url: "/core-uploadrole-edit-view/:uploadId?uploadroleId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/uploadrole-edit-view.html',
            controller: 'UploadRoleEditController as vm'
        }).state('core-runupload-list', {
            url: "/core-runupload-list-view",
            templateUrl: rootUrl + 'app/extraction/views/extraction/runupload-list-view.html',
            controller: 'RunUploadListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('core-client-list', {
            url: "/core-client-list",
            templateUrl: rootUrl + 'app/core/views/configuration/client-list-view.html',
            controller: 'ClientListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-client-edit', {
            url: "/core-client-edit/:clientId",
            templateUrl: rootUrl + 'app/core/views/configuration/client-edit-view.html',
            controller: 'ClientEditController as vm'



        }).state('core-database-list', {
            url: "/core-database-list",
            templateUrl: rootUrl + 'app/core/views/configuration/database-list-view.html',
            controller: 'DatabaseListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-database-edit', {
            url: "/core-database-edit/:databaseId",
            templateUrl: rootUrl + 'app/core/views/configuration/database-edit-view.html',
            controller: 'DatabaseEditController as vm'



        }).state('core-defaultuser-list', {
            url: "/core-defaultuser-list",
            templateUrl: rootUrl + 'app/core/views/configuration/defaultuser-list-view.html',
            controller: 'DefaultuserListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-defaultuser-edit', {
            url: "/core-defaultuser-edit/:defaultuserId",
            templateUrl: rootUrl + 'app/core/views/configuration/defaultuser-edit-view.html',
            controller: 'DefaultuserEditController as vm'



        }).state('core-companysecurity-list', {
            url: "/core-companysecurity-list",
            templateUrl: rootUrl + 'app/core/views/configuration/companysecurity-list-view.html',
            controller: 'CompanySecurityListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-companysecurity-edit', {
            url: "/core-companysecurity-edit/:companysecurityId",
            templateUrl: rootUrl + 'app/core/views/configuration/companysecurity-edit-view.html',
            controller: 'CompanySecurityEditController as vm'


            //}).state('core-usersession-list', {
            //    url: "/core-usersession-list",
            //    templateUrl: rootUrl + 'app/core/views/configuration/usersession-list-view.html',
            //    controller: 'UserSessionListController as vm',
            //    resolve: {
            //        loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
            //            return $ocLazyLoad.load({
            //                files: [
            //                      rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
            //                       rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
            //                       rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
            //            });
            //        }]
            //    }
            //}).state('core-usersession-edit', {
            //    url: "/core-usersession-edit/:usersessionId",
            //    templateUrl: rootUrl + 'app/core/views/configuration/usersession-edit-view.html',
            //    controller: 'UserSessionEditController as vm'



        }).state('core-staffs-list', {
            url: "/core-staffs-list",
            templateUrl: rootUrl + 'app/core/views/configuration/staff-list-view.html',
            controller: 'StaffListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-staff-edit', {
            url: "/core-staff-edit/:staffId",
            templateUrl: rootUrl + 'app/core/views/configuration/staff-edit-view.html',
            controller: 'StaffEditController as vm'




        }).state('finstat-registry-list', {
            url: "/finstat-registry-list",
            templateUrl: rootUrl + 'app/finstat/views/registry-list-view.html',
            controller: 'RegistryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-registry-edit', {
            url: "/finstat-registry-edit/:registryId?flag",
            templateUrl: rootUrl + 'app/finstat/views/registry-edit-view.html',
            controller: 'RegistryEditController as vm'


        }).state('finstat-revacctregistry-list', {
            url: "/finstat-revacctregistry-list",
            templateUrl: rootUrl + 'app/finstat/views/revacctregistry-list-view.html',
            controller: 'RevacctRegistryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-revacctregistry-edit', {
            url: "/finstat-revacctregistry-edit/:revenueId",
            templateUrl: rootUrl + 'app/finstat/views/revacctregistry-edit-view.html',
            controller: 'RevacctRegistryEditController as vm'


        }).state('finstat-qualitativenote-list', {
            url: "/finstat-qualitativenote-list",
            templateUrl: rootUrl + 'app/finstat/views/qualitativenote-list-view.html',
            controller: 'QualitativeNoteListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-qualitativenote-edit', {
            url: "/finstat-qualitativenote-edit/:qualitativeNoteId",
            templateUrl: rootUrl + 'app/finstat/views/qualitativenote-edit-view.html',
            controller: 'QualitativeNoteEditController as vm'

        }).state('finstat-interimqualitativenote-list', {
            url: "/finstat-interimqualitativenote-list",
            templateUrl: rootUrl + 'app/finstat/views/interimqualitativenote-list-view.html',
            controller: 'InterimQualitativeNoteListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-interimqualitativenote-edit', {
            url: "/finstat-interimqualitativenote-edit/:interimqualitativeNoteId",
            templateUrl: rootUrl + 'app/finstat/views/interimqualitativenote-edit-view.html',
            controller: 'InterimQualitativeNoteEditController as vm'




        }).state('finstat-budget-list', {
            url: "/finstat-budget-list",
            templateUrl: rootUrl + 'app/finstat/views/ifrsbudget-list-view.html',
            controller: 'IFRSBudgetListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-budget-edit', {
            url: "/finstat-budget-edit/:ifrsbudgetId",
            templateUrl: rootUrl + 'app/finstat/views/ifrsbudget-edit-view.html',
            controller: 'IFRSBudgetEditController as vm'

        }).state('finstat-derivedcaption-list', {
            url: "/finstat-derivedcaption-list",
            templateUrl: rootUrl + 'app/finstat/views/derivedcaption-list-view.html',
            controller: 'DerivedCaptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-derivedcaption-edit', {
            url: "/finstat-derivedcaption-edit/:derivedcaptionId",
            templateUrl: rootUrl + 'app/finstat/views/derivedcaption-edit-view.html',
            controller: 'DerivedCaptionEditController as vm'
        }).state('finstat-glmapping-list', {
            url: "/finstat-glmapping-list",
            templateUrl: rootUrl + 'app/finstat/views/glmapping-list-view.html',
            controller: 'GLMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-glmapping-edit', {
            url: "/finstat-glmapping-edit/:glmappingId?flag",
            templateUrl: rootUrl + 'app/finstat/views/glmapping-edit-view.html',
            controller: 'GLMappingEditController as vm'

        }).state('finstat-glmappingmgt-list', {
            url: "/finstat-glmappingmgt-list",
            templateUrl: rootUrl + 'app/finstat/views/glmappingmgt-list-view.html',
            controller: 'GLMappingMgtListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-glmappingmgt-edit', {
            url: "/finstat-glmappingmgt-edit/:glmappingmgtId",
            templateUrl: rootUrl + 'app/finstat/views/glmappingmgt-edit-view.html',
            controller: 'GLMappingMgtEditController as vm'

        }).state('finstat-unmappedgl-list', {
            url: "/finstat-unmappedgl-list",
            templateUrl: rootUrl + 'app/finstat/views/unmappedgl-list-view.html',
            controller: 'UnMappedGLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-unmappedgl-edit', {
            url: "/finstat-unmappedgl-edit/:glcode?gldescription",
            templateUrl: rootUrl + 'app/finstat/views/unmappedgl-edit-view.html',
            controller: 'UnMappedGLEditController as vm'
        }).state('finstat-gltype-list', {
            url: "/finstat-gltype-list",
            templateUrl: rootUrl + 'app/finstat/views/gltype-list-view.html',
            controller: 'GLTypeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-gltype-edit', {
            url: "/finstat-gltype-edit/:gltypeId",
            templateUrl: rootUrl + 'app/finstat/views/gltype-edit-view.html',
            controller: 'GLTypeEditController as vm'
        }).state('finstat-instrumenttype-list', {
            url: "/finstat-instrumenttype-list",
            templateUrl: rootUrl + 'app/finstat/views/instrumenttype-list-view.html',
            controller: 'InstrumentTypeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-instrumenttype-edit', {
            url: "/finstat-instrumenttype-edit/:instrumenttypeId",
            templateUrl: rootUrl + 'app/finstat/views/instrumenttype-edit-view.html',
            controller: 'InstrumentTypeEditController as vm'
        }).state('finstat-instrumenttypeglmap-list', {
            url: "/finstat-instrumenttypeglmap-list",
            templateUrl: rootUrl + 'app/finstat/views/instrumenttypeglmap-list-view.html',
            controller: 'InstrumentTypeGLMapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-instrumenttypeglmap-edit', {
            url: "/finstat-instrumenttypeglmap-edit/:instrumenttypeglmapId",
            templateUrl: rootUrl + 'app/finstat/views/instrumenttypeglmap-edit-view.html',
            controller: 'InstrumentTypeGLMapEditController as vm'
        }).state('finstat-autopostingtemplate-list', {
            url: "/finstat-autopostingtemplate-list",
            templateUrl: rootUrl + 'app/finstat/views/autopostingtemplate-list-view.html',
            controller: 'AutoPostingTemplateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-autopostingtemplate-edit', {
            url: "/finstat-autopostingtemplate-edit/:autopostingtemplateId",
            templateUrl: rootUrl + 'app/finstat/views/autopostingtemplate-edit-view.html',
            controller: 'AutoPostingTemplateEditController as vm'
        }).state('finstat-trialbalance-list', {
            url: "/finstat-trialbalance-list",
            templateUrl: rootUrl + 'app/finstat/views/trialbalance-list-view.html',
            controller: 'TrialBalanceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('finstat-trialbalanceconsolidated-list', {
            url: "/finstat-trialbalanceconsolidated-list",
            templateUrl: rootUrl + 'app/finstat/views/trialbalanceconsolidated-list-view.html',
            controller: 'ConsolidatedTrialBalanceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('finstat-adjustment-list', {
            url: "/finstat-adjustment-list",
            templateUrl: rootUrl + 'app/finstat/views/adjustment-list-view.html',
            controller: 'AdjustmentListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js'
                        ]
                    });
                }]
            }

        }).state('finstat-adjustment-edit', {
            url: "/finstat-adjustment-edit/:gladjustmentId?adjustmentType?reportType",
            templateUrl: rootUrl + 'app/finstat/views/adjustment-edit-view.html',
            controller: 'AdjustmentEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js'
                        ]
                    });
                }]
            }

        }).state('finstat-adjustment-edit2', {
            url: "/finstat-adjustment-edit/:gladjustmentId",
            templateUrl: rootUrl + 'app/finstat/views/adjustment-edit-view.html',
            controller: 'AdjustmentEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js'
                        ]
                    });
                }]
            }


        }).state('ifrs-reportpackviewer-list', {
            url: "/ifrs-reportpackviewer-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/ifrsreportpackviewer-list-view.html',
            controller: 'IFRSReportPackViewerListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js'
                        ]
                    });
                }]
            }

        }).state('finstat-transactiondetail-list', {
            url: "/finstat-transactiondetail-list",
            templateUrl: rootUrl + 'app/finstat/views/transactiondetail-list-view.html',
            controller: 'TransactionDetailListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-postingdetail-list', {
            url: "/finstat-postingdetail-list",
            templateUrl: rootUrl + 'app/finstat/views/postingdetail-list-view.html',
            controller: 'PostingDetailListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-postingdetailcontracts-list', {
            url: "/finstat-postingdetailcontracts-list/:filter",
            templateUrl: rootUrl + 'app/finstat/views/postingdetailcontracts-list-view.html',
            controller: 'PostingDetailContractsListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('finstat-revenueglmapping-list', {
            url: "/finstat-revenueglmapping-list",
            templateUrl: rootUrl + 'app/finstat/views/revenueglmapping-list-view.html',
            controller: 'RevenueGLMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-revenueglmapping-edit', {
            url: "/finstat-revenueglmapping-edit/:glmappingId",
            templateUrl: rootUrl + 'app/finstat/views/revenueglmapping-edit-view.html',
            controller: 'RevenueGLMappingEditController as vm'

        }).state('finstat-changesinequity-list', {
            url: "/finstat-changesinequity-list",
            templateUrl: rootUrl + 'app/finstat/views/changesinequity-list-view.html',
            controller: 'ChangesInEquityListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-changesinequity-edit', {
            url: "/finstat-changesinequity-edit/:changesinequityId",
            templateUrl: rootUrl + 'app/finstat/views/changesinequity-edit-view.html',
            controller: 'ChangesInEquityEditController as vm'

        }).state('finstat-ratiodetail-list', {
            url: "/finstat-ratiodetail-list",
            templateUrl: rootUrl + 'app/finstat/views/ratiodetail-list-view.html',
            controller: 'RatioDetailListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('finstat-ratiodetail-edit', {
            url: "/finstat-ratiodetail-edit/:RatioID",
            templateUrl: rootUrl + 'app/finstat/views/ratiodetail-edit-view.html',
            controller: 'RatioDetailEditController as vm'



        }).state('finstat-ratiocaption-list', {
            url: "/finstat-ratiocaption-list",
            templateUrl: rootUrl + 'app/finstat/views/ratiocaption-list-view.html',
            controller: 'RatioCaptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('finstat-ratiocaption-edit', {
            url: "/finstat-ratiocaption-edit/:RatioCaptionID",
            templateUrl: rootUrl + 'app/finstat/views/ratiocaption-edit-view.html',
            controller: 'RatioCaptionEditController as vm'

        }).state('finstat-calendar-list', {
            url: "/finstat-calendar-list",
            templateUrl: rootUrl + 'app/finstat/views/calendar-list-view.html',
            controller: 'CalendarListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('finstat-calendar-edit', {
            url: "/finstat-calendar-edit/:CalId",
            templateUrl: rootUrl + 'app/finstat/views/calendar-edit-view.html',
            controller: 'CalendarEditController as vm'

        }).state('finstat-glaarchive-list', {
            url: "/finstat-glaarchive-list",
            templateUrl: rootUrl + 'app/finstat/views/glaarchive-list-view.html',
            controller: 'GLAArchiveListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('Ifrsloansinfo-list', {
            url: "/Ifrsloansinfo-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsloansinfo-list-view.html',
            controller: 'IfrsLoansInfoListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('Ifrspdprojection-list', {
            url: "/Ifrspdprojection-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrspdprojection-list-view.html',
            controller: 'IfrsPDProjectionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }




        }).state('Ifrslgdprojections-list', {
            url: "/Ifrslgdprojections-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrslgdprojections-list-view.html',
            controller: 'IfrsLgdProjectionsListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }



        }).state('finstat-glaarchive-edit', {
            url: "/finstat-glaarchive-edit/:InstrumentID",
            templateUrl: rootUrl + 'app/finstat/views/glaarchive-edit-view.html',
            controller: 'GLAArchiveEditController as vm'


        }).state('ifrsloan-loansetup-list', {
            url: "/ifrsloan-loansetup-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/loansetup-list-view.html',
            controller: 'LoanSetupListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-loansetup-edit', {
            url: "/ifrsloan-loansetup-edit/:loansetupId",
            templateUrl: rootUrl + 'app/ifrsloan/views/loansetup-edit-view.html',
            controller: 'LoanSetupEditController as vm'
        }).state('ifrsloan-scheduletype-list', {
            url: "/ifrsloan-scheduletype-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/scheduletype-list-view.html',
            controller: 'ScheduleTypeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-scheduletype-edit', {
            url: "/ifrsloan-scheduletype-edit/:scheduletypeId",
            templateUrl: rootUrl + 'app/ifrsloan/views/scheduletype-edit-view.html',
            controller: 'ScheduleTypeEditController as vm'
        }).state('ifrsloan-product-list', {
            url: "/ifrsloan-product-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/product-list-view.html',
            controller: 'IFRSProductListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-product-edit', {
            url: "/ifrsloan-product-edit/:productId?code",
            templateUrl: rootUrl + 'app/ifrsloan/views/product-edit-view.html',
            controller: 'IFRSProductEditController as vm'
        }).state('ifrsloan-creditriskrating-list', {
            url: "/ifrsloan-creditriskrating-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/creditriskrating-list-view.html',
            controller: 'CreditRiskRatingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-creditriskrating-edit', {
            url: "/ifrsloan-creditriskrating-edit/:creditriskratingId",
            templateUrl: rootUrl + 'app/ifrsloan/views/creditriskrating-edit-view.html',
            controller: 'CreditRiskRatingEditController as vm'
        }).state('ifrsloan-collateralcategory-list', {
            url: "/ifrsloan-collateralcategory-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/collateralcategory-list-view.html',
            controller: 'CollateralCategoryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-collateralcategory-edit', {
            url: "/ifrsloan-collateralcategory-edit/:collateralcategoryId",
            templateUrl: rootUrl + 'app/ifrsloan/views/collateralcategory-edit-view.html',
            controller: 'CollateralCategoryEditController as vm'
        }).state('ifrsloan-collateraltype-edit', {
            url: "/ifrsloan-collateraltype-edit/:collateralcategoryId?categorycode?collateraltypeId",
            templateUrl: rootUrl + 'app/ifrsloan/views/collateraltype-edit-view.html',
            controller: 'CollateralTypeEditController as vm'
        }).state('ifrsloan-collateralinformation-list', {
            url: "/ifrsloan-collateralinformation-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/collateralinformation-list-view.html',
            controller: 'CollateralInformationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-collateralinformation-edit', {
            url: "/ifrsloan-collateralinformation-edit/:collateralinformationId",
            templateUrl: rootUrl + 'app/ifrsloan/views/collateralinformation-edit-view.html',
            controller: 'CollateralInformationEditController as vm'
        }).state('ifrsloan-collateralrealizationperiod-list', {
            url: "/ifrsloan-collateralrealizationperiod-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/collateralrealizationperiod-list-view.html',
            controller: 'CollateralRealizationPeriodListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-collateralrealizationperiod-edit', {
            url: "/ifrsloan-collateralrealizationperiod-edit/:collateralrealizationperiodId",
            templateUrl: rootUrl + 'app/ifrsloan/views/collateralrealizationperiod-edit-view.html',
            controller: 'CollateralRealizationPeriodEditController as vm'
        }).state('ifrsloan-watchlistedloan-list', {
            url: "/ifrsloan-watchlistedloan-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/watchlistedloan-list-view.html',
            controller: 'WatchListedLoanListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-watchlistedloan-edit', {
            url: "/ifrsloan-watchlistedloan-edit/:watchlistedloanId",
            templateUrl: rootUrl + 'app/ifrsloan/views/watchlistedloan-edit-view.html',
            controller: 'WatchListedLoanEditController as vm'

        }).state('ifrsloan-individualschedule-list', {
            url: "/ifrsloan-individualschedule-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/individualschedule-list-view.html',
            controller: 'IndividualScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-individualschedule-edit', {
            url: "/ifrsloan-individualschedule-edit/:individualscheduleId",
            templateUrl: rootUrl + 'app/ifrsloan/views/individualschedule-edit-view.html',
            controller: 'IndividualScheduleEditController as vm'

        }).state('ifrsloan-individualimpairment-list', {
            url: "/ifrsloan-individualimpairment-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/individualimpairment-list-view.html',
            controller: 'IndividualImpairmentListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-individualimpairment-edit', {
            url: "/ifrsloan-individualimpairment-edit/:individualimpairmentId",
            templateUrl: rootUrl + 'app/ifrsloan/views/individualimpairment-edit-view.html',
            controller: 'IndividualImpairmentEditController as vm'

        }).state('ifrsloan-impairmentoverride-list', {
            url: "/ifrsloan-impairmentoverride-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/impairmentoverride-list-view.html',
            controller: 'ImpairmentOverrideListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('finstat-ledgerdetailsummary-list', {
            url: "/finstat-ledgerdetailsummary-list",
            templateUrl: rootUrl + 'app/finstat/views/ledgerdetailsummary-list-view.html',
            controller: 'LedgerDetailSummaryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrsloan-bondsummary-list', {
            url: "/ifrsloan-bondsummary-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/bondsummary-list-view.html',
            controller: 'BondSummaryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs9-sandprating-list', {
            url: "/ifrs9-sandprating-list",
            templateUrl: rootUrl + 'app/IFRS9/views/sandprating-list-view.html',
            controller: 'SandPRatingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-sandprating-edit', {
            url: "/ifrs9-sandprating-edit/:SandPRating_Id",
            templateUrl: rootUrl + 'app/IFRS9/views/sandprating-edit-view.html',
            controller: 'SandPRatingEditController as vm'


        }).state('ifrs9-macroeconomicsvariablescenario-list', {
            url: "/ifrs9-macroeconomicsvariablescenario-list",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomicsvariablescenario-list-view.html',
            controller: 'MacroeconomicsVariableScenarioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-macroeconomicsvariablescenario-edit', {
            url: "/ifrs9-macroeconomicsvariablescenario-edit/:MacroeconomicsId",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomicsvariablescenario-edit-view.html',
            controller: 'MacroeconomicsVariableScenarioEditController as vm'


        }).state('ifrs9-lgdinputfactor-list', {
            url: "/ifrs9-lgdinputfactor-list",
            templateUrl: rootUrl + 'app/IFRS9/views/lgdinputfactor-list-view.html',
            controller: 'LgdInputFactorListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-lgdinputfactor-edit', {
            url: "/ifrs9-lgdinputfactor-edit/:LgdInputFactorId",
            templateUrl: rootUrl + 'app/IFRS9/views/lgdinputfactor-edit-view.html',
            controller: 'LgdInputFactorEditController as vm'


        }).state('ifrs9-regressionoutput-list', {
            url: "/ifrs9-regressionoutput-list",
            templateUrl: rootUrl + 'app/IFRS9/views/regressionoutput-list-view.html',
            controller: 'RegressionOutputListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrsloan-impairmentoverride-edit', {
            url: "/ifrsloan-impairmentoverride-edit/:impairmentoverrideId",
            templateUrl: rootUrl + 'app/ifrsloan/views/impairmentoverride-edit-view.html',
            controller: 'ImpairmentOverrideEditController as vm'
        }).state('ifrsfi-fairvaluebasismap-list', {
            url: "/ifrsfi-fairvaluebasismap-list",
            templateUrl: rootUrl + 'app/ifrsfi/views/fairvaluebasismap-list-view.html',
            controller: 'FairValueBasisMapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsfi-fairvaluebasismap-edit', {
            url: "/ifrsfi-fairvaluebasismap-edit/:fairvaluebasismapId",
            templateUrl: rootUrl + 'app/ifrsfi/views/fairvaluebasismap-edit-view.html',
            controller: 'FairValueBasisMapEditController as vm'
        }).state('ifrsfi-fairvaluebasisexemption-list', {
            url: "/ifrsfi-fairvaluebasisexemption-list",
            templateUrl: rootUrl + 'app/ifrsfi/views/fairvaluebasisexemption-list-view.html',
            controller: 'FairValueBasisExemptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsfi-fairvaluebasisexemption-edit', {
            url: "/ifrsfi-fairvaluebasisexemption-edit/:fairvaluebasisexemptionId",
            templateUrl: rootUrl + 'app/ifrsfi/views/fairvaluebasisexemption-edit-view.html',
            controller: 'FairValueBasisExemptionEditController as vm'
            //DataView

        }).state('ifrs-bondconsolidateddata-list', {
            url: "/ifrs-bondconsolidateddata-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/bondconsolidateddata-list-view.html',
            controller: 'BondConsolidatedDataListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-bondconsolidatedothers-list', {
            url: "/ifrs-bondconsolidatedothers-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/bondconsolidateddataothers-list-view.html',
            controller: 'BondConsolidatedDataOthersListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-tbillconsolidateddata-list', {
            url: "/ifrs-tbillconsolidateddata-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/tbillconsolidateddata-list-view.html',
            controller: 'TbillConsolidatedDataListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-loanconsolidateddata-list', {
            url: "/ifrs-loanconsolidateddata-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/loanconsolidateddata-list-view.html',
            controller: 'LoanConsolidatedDataListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loanconsolidateddatafsdh-list', {
            url: "/ifrs-loanconsolidateddatafsdh-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/loanconsolidateddatafsdh-list-view.html',
            controller: 'LoanConsolidatedDataFSDHListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-bondperiodicschedule-list', {
            url: "/ifrs-bondperiodicschedule-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/bondperiodicschedule-list-view.html',
            controller: 'BondPeriodicScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-bondamortization-list', {
            url: "/ifrs-bondamortization-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/bondcomputation-list-view.html',
            controller: 'BondComputationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-zerocouponbond-list', {
            url: "/ifrs-zerocouponbond-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/bondcomputationresultzero-list-view.html',
            controller: 'BondComputationResultZeroListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-loanperiodicschedule-list', {
            url: "/ifrs-loanperiodicschedule-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/loanperiodicschedule-list-view.html',
            controller: 'LoanPeriodicScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-borrowingperiodicschedule-list', {
            url: "/ifrs-borrowingperiodicschedule-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/borrowingperiodicschedule-list-view.html',
            controller: 'BorrowingPeriodicScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-unmappedproduct-list', {
            url: "/ifrs-unmappedproduct-list",

            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/unmappedifrsproduct-list-view.html',
            controller: 'UnMappedIFRSProductListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


            /////////    -  check availability extracted data... 

        }).state('ifrs-checkdataavailability-list', {
            url: "/ifrs-checkdataavailability-list",

            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/checkdataavailability-list-view.html',
            controller: 'CheckDataAvailabilityController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

            /////////    -  check availability extracted data... 


        }).state('ifrs-loandailyschedule-list', {
            url: "/ifrs-loandailyschedule-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/loanschedule-list-view.html',
            controller: 'LoanScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-borrowingdailyschedule-list', {
            url: "/ifrs-borrowingdailyschedule-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/borrowingschedule-list-view.html',
            controller: 'BorrowingScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loanimpairmentresult-list', {
            url: "/ifrs-loanimpairmentresult-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/loansimpairmentresult-list-view.html',
            controller: 'LoansImpairmentResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-treasurybillscomputation-list', {
            url: "/ifrs-treasurybillscomputation-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/tbillscomputationresult-list-view.html',
            controller: 'TBillsComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-equitystock-list', {
            url: "/ifrs-equitystock-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/equitystockcomputationresult-list-view.html',
            controller: 'EquityStockComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-bonddata-list', {
            url: "/ifrs-bonddata-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/bonddata-list-view.html',
            controller: 'IFRSBondListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-bonddata-edit', {
            url: "/ifrs-bonddata-edit/:bondId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/bonddata-edit-view.html',
            controller: 'IFRSBondEditController as vm'


        }).state('ifrs-offbalancesheetexposure-list', {
            url: "/ifrs-offbalancesheetexposure-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/offbalancesheetexposure-list-view.html',
            controller: 'OffBalanceSheetExposureListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-offbalancesheetexposure-edit', {
            url: "/ifrs-offbalancesheetexposure-edit/:ObeId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/offbalancesheetexposure-edit-view.html',
            controller: 'OffBalanceSheetExposureEditController as vm'


        }).state('ifrs-tbills-list', {
            url: "/ifrs-tbills-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/tbilldata-list-view.html',
            controller: 'IFRSTbillListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-tbills-edit', {
            url: "/ifrs-tbills-edit/:tbillId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/tbilldata-edit-view.html',
            controller: 'IFRSTbillEditController as vm'



        }).state('ifrs-loandetail-list', {
            url: "/ifrs-loandetail-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loandetail-list-view.html',
            controller: 'LoanDetailListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loandetail-edit', {
            url: "/ifrs-loandetail-edit/:loanDetailId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loandetail-edit-view.html',
            controller: 'LoanDetailEditController as vm'

        }).state('ifrs-loanprydata-list', {
            url: "/ifrs-loanprydata-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loanpry-list-view.html',
            controller: 'LoanPryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-hcclassification-list', {
            url: "/ifrs-hcclassification-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/hcclassification-list-view.html',
            controller: 'HCClassificationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-hcclassification-edit', {
            url: "/ifrs-hcclassification-edit/:Id",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/hcclassification-edit-view.html',
            controller: 'HCClassificationEditController as vm'


        }).state('ifrs-obexposure-list', {
            url: "/ifrs-obexposure-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/obexposure-list-view.html',
            controller: 'OBExposureListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-obexposure-edit', {
            url: "/ifrs-obexposure-edit/:flag/:obeId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/obexposure-edit-view.html',
            controller: 'OBExposureEditController as vm'

        }).state('ifrs-obexposureccf-list', {
            url: "/ifrs-obexposureccf-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/obexposureccf-list-view.html',
            controller: 'OBExposureCCFListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-obexposureccf-edit', {
            url: "/ifrs-obexposureccf-edit/:flag/:obeId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/obexposureccf-edit-view.html',
            controller: 'OBExposureCCFEditController as vm'

        }).state('ifrs-collateraldetails-list', {
            url: "/ifrs-collateraldetails-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/collateraldetails-list-view.html',
            controller: 'CollateralDetailsListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-collateraldetails-edit', {
            url: "/ifrs-collateraldetails-edit/:Id",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/collateraldetails-edit-view.html',
            controller: 'CollateralDetailsEditController as vm'

        }).state('ifrs-facclassconsolidated-list', {
            url: "/ifrs-facclassconsolidated-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/facclassconsolidated-list-view.html',
            controller: 'FacClassConsolidatedListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-facclassconsolidated-edit', {
            url: "/ifrs-facclassconsolidated-edit/:Id",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/facclassconsolidated-edit-view.html',
            controller: 'FacClassConsolidatedEditController as vm'

        }).state('ifrs-facrating-list', {
            url: "/ifrs-facrating-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/facrating-list-view.html',
            controller: 'FacRatingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-facrating-edit', {
            url: "/ifrs-facrating-edit/:Id",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/facrating-edit-view.html',
            controller: 'FacRatingEditController as vm'

        }).state('ifrs-facstaging-list', {
            url: "/ifrs-facstaging-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/facstaging-list-view.html',
            controller: 'FacStagingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-facstaging-edit', {
            url: "/ifrs-facstaging-edit/:Id",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/facstaging-edit-view.html',
            controller: 'FacStagingEditController as vm'

        }).state('ifrs-facobestaging-list', {
            url: "/ifrs-facobestaging-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/facobestaging-list-view.html',
            controller: 'FacOBEStagingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-facobestaging-edit', {
            url: "/ifrs-facobestaging-edit/:Id",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/facobestaging-edit-view.html',
            controller: 'FacOBEStagingEditController as vm'

        }).state('ifrs-loandetailedinfo-list', {
            url: "/ifrs-loandetailedinfo-list/:refno",
            templateUrl: rootUrl + 'app/IFRSDataView/views/loandetailedinfo-list-view.html',
            controller: 'LoanDetailedInfoListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
            
        }).state('ifrs-cashflow-list', {
            url: "/ifrs-cashflow-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/cashflow-list-view.html',
            controller: 'CashFlowListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-cashflow-edit', {
            url: "/ifrs-cashflow-edit/:CashflowId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/cashflow-edit-view.html',
            controller: 'CashFlowEditController as vm'

        }).state('ifrs-ifrscustomer-edit', {
            url: "/ifrs-ifrscustomer-edit/:ifrsCustomerId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/ifrscustomer-edit-view.html',
            controller: 'IfrsCustomerEditController as vm'

        }).state('ifrs-facilitystaging-list', {
            url: "/ifrs-facilitystaging-list",
            templateUrl: rootUrl + 'app/IFRS9/views/facilitystaging-list-view.html',
            controller: 'FacilityStagingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-facilityclassification-list', {
            url: "/ifrs-facilityclassification-list",
            templateUrl: rootUrl + 'app/IFRS9/views/facilityclassification-list-view.html',
            controller: 'FacilityClassificationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs-facilityclassification-edit', {
            url: "/ifrs-facilityclassification-edit/:classId",
            templateUrl: rootUrl + 'app/IFRS9/views/facilityclassification-edit-view.html',
            controller: 'FacilityClassificationEditController as vm'

        }).state('ifrs-sicrparameters-list', {
            url: "/ifrs-sicrparameters-list",
            templateUrl: rootUrl + 'app/IFRS9/views/sicrparameters-list-view.html',
            controller: 'SICRParametersListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs-sicrparameters-edit', {
            url: "/ifrs-sicrparameters-edit/:ID",
            templateUrl: rootUrl + 'app/IFRS9/views/sicrparameters-edit-view.html',
            controller: 'SICRParametersEditController as vm'


        }).state('ifrs-obeeclcomputation-list', {
            url: "/ifrs-obeeclcomputation-list",
            templateUrl: rootUrl + 'app/IFRS9/views/obeeclcomputation-list-view.html',
            controller: 'ObeEclComputationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs-loanseclcomputationresult-list', {
            url: "/ifrs-loanseclcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/loanseclcomputationresult-list-view.html',
            controller: 'LoansECLComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loansignificantflag-list', {
            url: "/ifrs-loansignificantflag-list",
            templateUrl: rootUrl + 'app/IFRS9/views/loansignificantflag-list-view.html',
            controller: 'LoanSignificantFlagListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loansignificantflag-edit', {
            url: "/ifrs-loansignificantflag-edit/:Id",
            templateUrl: rootUrl + 'app/IFRS9/views/loansignificantflag-edit-view.html',
            controller: 'LoanSignificantFlagEditController as vm'

        }).state('ifrs-marginalccfstr-list', {
            url: "/ifrs-marginalccfstr-list",
            templateUrl: rootUrl + 'app/IFRS9/views/marginalccfstr-list-view.html',
            controller: 'MarginalCCFStrListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-bondlgd-list', {
            url: "/ifrs-bondlgd-list",

            templateUrl: rootUrl + 'app/IFRS9/views/ifrsbondlgd-list-view.html',
            controller: 'IfrsBondLGDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-ifrsinvestment-list', {
            url: "/ifrs-ifrsinvestment-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsinvestment-list-view.html',
            controller: 'ifrsinvestmentListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-ifrsinvestment-edit', {
            url: "/ifrs-ifrsinvestment-edit/:Id",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsinvestment-edit-view.html',
            controller: 'IfrsInvestmentEditController as vm'

        }).state('ifrs-marginalpddstrlb-list', {
            url: "/ifrs-marginalpddstrlb-list",
            templateUrl: rootUrl + 'app/IFRS9/views/marginalpddstrlb-list-view.html',
            controller: 'MarginalPddSTRLBListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-bondseclcomputationresult-list', {
            url: "/ifrs9-bondseclcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/bondseclcomputationresult-list-view.html',
            controller: 'BondsECLComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-monthlyobeeadstrlb-list', {
            url: "/ifrs9-monthlyobeeadstrlb-list",
            templateUrl: rootUrl + 'app/IFRS9/views/monthlyobeeadstrlb-list-view.html',
            controller: 'MonthlyObeEadSTRLBListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-odeclcomputationresult-list', {
            url: "/ifrs-odeclcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/odeclcomputationresult-list-view.html',
            controller: 'ODEclComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs-marginalpdobedistr-list', {
            url: "/ifrs-marginalpdobedistr-list",
            templateUrl: rootUrl + 'app/IFRS9/views/marginalpdobedistr-list-view.html',
            controller: 'MarginalPdObeDistrListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-marginalpdoddistr-list', {
            url: "/('ifrs-marginalpdoddistr-list",
            templateUrl: rootUrl + 'app/IFRS9/views/marginalpdoddistr-list-view.html',
            controller: 'MarginalPdODDistrListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-lgdcomptresult-list', {
            url: "/ifrs-lgdcomptresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/lgdcomptresult-list-view.html',
            controller: 'LGDComptResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-lgdcomptresult-edit', {
            url: "/ifrs-lgdcomptresult-edit/:Id",
            templateUrl: rootUrl + 'app/IFRS9/views/lgdcomptresult-edit-view.html',
            controller: 'LGDComptResultEditController as vm'

        }).state('ifrs-obelgdcomptresult-list', {
            url: "/ifrs-obelgdcomptresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/obelgdcomptresult-list-view.html',
            controller: 'ObeLGDComptResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-obelgdcomptresult-edit', {
            url: "/ifrs-obelgdcomptresult-edit/:Id",
            templateUrl: rootUrl + 'app/IFRS9/views/obelgdcomptresult-edit-view.html',
            controller: 'ObeLGDComptResultEditController as vm'

        }).state('ifrsbondsmonthlyead-list', {
            url: "/ifrsbondsmonthlyead-list",

            templateUrl: rootUrl + 'app/IFRS9/views/ifrsbondsmonthlyead-list-view.html',
            controller: 'IfrsBondsMonthlyEADListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrsmonthlyead-list', {
            url: "/ifrsmonthlyead-list",

            templateUrl: rootUrl + 'app/IFRS9/views/ifrsmonthlyead-list-view.html',
            controller: 'IfrsMonthlyEADListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loanclassificationsicrsignflag-list', {
            url: "/ifrs-loanclassificationsicrsignflag-list",

            templateUrl: rootUrl + 'app/IFRS9/views/loanclassificationsicrsignflag-list-view.html',
            controller: 'LoanClassificationSICRSignFlagListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-loanclassificationsicrsignflag-edit', {
            url: "/ifrs-loanclassificationsicrsignflag-edit/:Id",
            templateUrl: rootUrl + 'app/IFRS9/views/loanclassificationsicrsignflag-edit-view.html',
            controller: 'LoanClassificationSICRSignFlagEditController as vm'


        }).state('ifrs9-loaneclresult-list', {
            url: "/ifrs9-loaneclresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/loaneclresult-list-view.html',
            controller: 'LoanECLResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs9-obeeclresult-list', {
            url: "/ifrs9-obeeclresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/obeeclresult-list-view.html',
            controller: 'ObeECLResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs9-bondseclresult-list', {
            url: "/ifrs9-bondseclresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/bondseclresult-list-view.html',
            controller: 'BondsECLResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-odeclresult-list', {
            url: "/ifrs9-odeclresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/odeclresult-list-view.html',
            controller: 'OdECLResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-collateralgrowthrate-list', {
            url: "/ifrs9-collateralgrowthrate-list",
            templateUrl: rootUrl + 'app/IFRS9/views/collateralgrowthrate-list-view.html',
            controller: 'CollateralGrowthRateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-cummulativedefaultamt-list', {
            url: "/ifrs9-cummulativedefaultamt-list",
            templateUrl: rootUrl + 'app/IFRS9/views/cummulativedefaultamt-list-view.html',
            controller: 'CummulativeDefaultAmtListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-cummulativelifetimepd-list', {
            url: "/ifrs9-cummulativelifetimepd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/cummulativelifetimepd-list-view.html',
            controller: 'CummulativeLifetimePdListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-collateralrecamtstaging-list', {
            url: "/ifrs9-collateralrecamtstaging-list",
            templateUrl: rootUrl + 'app/IFRS9/views/collateralrecamtstaging-list-view.html',
            controller: 'CollateralRecAmtStagingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs9-historicaldefaultfreq-list', {
            url: "/ifrs9-historicaldefaultfreq-list",
            templateUrl: rootUrl + 'app/IFRS9/views/historicaldefaultfreq-list-view.html',
            controller: 'HistoricalDefaultFreqListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-cummulativepd-list', {
            url: "/ifrs9-cummulativepd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/cummulativepd-list-view.html',
            controller: 'CummulativePDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-marginalccfpivotstrlb-list', {
            url: "/ifrs9-marginalccfpivotstrlb-list",
            templateUrl: rootUrl + 'app/IFRS9/views/marginalccfpivotstrlb-list-view.html',
            controller: 'MarginalCCFPivotSTRLBListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs9-ccfanalysisoverdraftstrlb-list', {
            url: "/ifrs9-ccfanalysisoverdraftstrlb-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ccfanalysisoverdraftstrlb-list-view.html',
            controller: 'CcfAnalysisOverDraftSTRLBListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrsmarginalpdbyscenerio-list', {
            url: "/ifrsmarginalpdbyscenerio-list",

            templateUrl: rootUrl + 'app/IFRS9/views/ifrsmarginalpdbyscenerio-list-view.html',
            controller: 'IfrsMarginalPDByScenerioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrspdtermstructure-list', {
            url: "/ifrspdtermstructure-list",

            templateUrl: rootUrl + 'app/IFRS9/views/ifrspdtermstructure-list-view.html',
            controller: 'IfrsPdTermStructureListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrspdtermstructure-edit', {
            url: "/ifrspdtermstructure-edit/:Id",
            templateUrl: rootUrl + 'app/ifrs9/views/ifrspdtermstructure-edit-view.html',
            controller: 'IfrsPdTermStructureEditController as vm'


        }).state('ifrs9-consolidatedloans-list', {
            url: "/ifrs9-consolidatedloans-list",
            templateUrl: rootUrl + 'app/IFRS9/views/consolidatedloans-list-view.html',
            controller: 'ConsolidatedLoansListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrsmonthlyforwardpdmacrovar-list', {
            url: "/ifrsmonthlyforwardpdmacrovar-list",

            templateUrl: rootUrl + 'app/IFRS9/views/ifrsmonthlyforwardpd-list-view.html',
            controller: 'ifrsmonthlyforwardpdmacrovarListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsprojectedcummdefault-list', {
            url: "/ifrsprojectedcummdefault-list",

            templateUrl: rootUrl + 'app/IFRS9/views/ifrsprojecteddefaultfrq-list-view.html',
            controller: 'IfrsProjectedCummDefaultFrqListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-investmentmarginalpd-list', {
            url: "/ifrs9-investmentmarginalpd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/investmentmarginalpd-list-view.html',
            controller: 'InvestmentMarginalPdListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-overdraftmonthlyead-list', {
            url: "/ifrs9-overdraftmonthlyead-list",
            templateUrl: rootUrl + 'app/IFRS9/views/overdraftmonthlyead-list-view.html',
            controller: 'OverdraftMonthlyEADListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-odlgdcompt-list', {
            url: "/ifrs9-odlgdcompt-list",
            templateUrl: rootUrl + 'app/IFRS9/views/overdraftlgdcomputation-list-view.html',
            controller: 'OverdraftLGDComputationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs-ifrscustomer-list', {
            url: "/ifrs-ifrscustomer-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/ifrscustomer-list-view.html',
            controller: 'IfrsCustomerListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-ifrscustomeraccount-edit', {
            url: "/ifrs-ifrscustomeraccount-edit/:ifrsCustomerAccountId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/ifrscustomeraccount-edit-view.html',
            controller: 'IfrsCustomerAccountEditController as vm'


        }).state('ifrs-ifrscustomeraccount-list', {
            url: "/ifrs-ifrscustomeraccount-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/ifrscustomeraccount-list-view.html',
            controller: 'IfrsCustomerAccountListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loanprydata-edit', {
            url: "/ifrs-loanprydata-edit/:pryId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loanpry-edit-view.html',
            controller: 'LoanPryEditController as vm'



        }).state('ifrs-borrowingdata-list', {
            url: "/ifrs-borrowingdata-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/borrowing-list-view.html',
            controller: 'BorrowingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-borrowingdata-edit', {
            url: "/ifrs-borrowingdata-edit/:borrowingId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/borrowing-edit-view.html',
            controller: 'BorrowingEditController as vm'




        }).state('ifrs-loanprymoratoriumdata-list', {
            url: "/ifrs-loanprymoratoriumdata-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loanprymoratorium-list-view.html',
            controller: 'LoanPryMoratoriumListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loanprymoratoriumdata-edit', {
            url: "/ifrs-loanprymoratoriumdata-edit/:loanPryMoratoriumId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loanprymoratorium-edit-view.html',
            controller: 'LoanPryMoratoriumEditController as vm'


        }).state('ifrs-integralfee-list', {
            url: "/ifrs-integralfee-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/integralfee-list-view.html',
            controller: 'IntegralFeeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-integralfee-edit', {
            url: "/ifrs-integralfee-edit/:integralFeeId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/integralfee-edit-view.html',
            controller: 'IntegralFeeEditController as vm'

        }).state('ifrs-placement-list', {
            url: "/ifrs-placement-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/placement-list-view.html',
            controller: 'PlacementListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-placement-edit', {
            url: "/ifrs-placement-edit/:Placement_Id",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/placement-edit-view.html',
            controller: 'PlacementEditController as vm'

        }).state('ifrs-loaninterestrate-list', {
            url: "/ifrs-loaninterestrate-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loaninterestrate-list-view.html',
            controller: 'LoanInterestRateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-checkifrsdataavailability-list', {
            url: "/ifrs-checkifrsdataavailability-list",

            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/checkifrsdataavailability-list-view.html',
            controller: 'CheckifrsDataAvailabilityController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loaninterestrate-edit', {
            url: "/ifrs-loaninterestrate-edit/:LoanInterestRate_Id",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loaninterestrate-edit-view.html',
            controller: 'LoanInterestRateEditController as vm'

        }).state('mpr-teamdefinition-list', {
            url: "/mpr-teamdefinition-list",
            templateUrl: rootUrl + 'app/mpr_core/views/teamdefinition-list-view.html',
            controller: 'TeamDefinitionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('mpr-teamdefinition-edit', {
            url: "/mpr-teamdefinition-edit/:teamdefinitionId",
            templateUrl: rootUrl + 'app/mpr_core/views/teamdefinition-edit-view.html',
            controller: 'TeamDefinitionEditController as vm'
        }).state('mpr-teamclassificationtype-list', {
            url: "/mpr-teamclassificationtype-list",
            templateUrl: rootUrl + 'app/mpr_core/views/teamclassificationtype-list-view.html',
            controller: 'TeamClassificationTypeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('mpr-captionmapping-list', {
            url: "/mpr-captionmapping-list",
            templateUrl: rootUrl + 'app/mpr_core/views/captionmapping-list-view.html',
            controller: 'CaptionMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-captionmapping-edit', {
            url: "/mpr-captionmapping-edit/:captionmappingId",
            templateUrl: rootUrl + 'app/mpr_core/views/captionmapping-edit-view.html',
            controller: 'CaptionMappingEditController as vm'

        }).state('mpr-bsinotherinformation-list', {
            url: "/mpr-bsinotherinformation-list",
            templateUrl: rootUrl + 'app/mpr_core/views/bsinotherinformation-list-view.html',
            controller: 'BSINOtherInformationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-bsinotherinformation-edit', {
            url: "/mpr-bsinotherinformation-edit/:bsinotherinformationId",
            templateUrl: rootUrl + 'app/mpr_core/views/bsinotherinformation-edit-view.html',
            controller: 'BSINOtherInformationEditController as vm'

        }).state('mpr-bsinotherinformationtotalline-list', {
            url: "/mpr-bsinotherinformationtotalline-list",
            templateUrl: rootUrl + 'app/mpr_core/views/bsinotherinformationtotalline-list-view.html',
            controller: 'BSINOtherInformationTotalLineListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-bsinotherinformationtotalline-edit', {
            url: "/mpr-bsinotherinformationtotalline-edit/:bsinotherinformationtotallineId",
            templateUrl: rootUrl + 'app/mpr_core/views/bsinotherinformationtotalline-edit-view.html',
            controller: 'BSINOtherInformationTotalLineEditController as vm'

        }).state('mpr-abcratio-list', {
            url: "/mpr-abcratio-list",
            templateUrl: rootUrl + 'app/mpr_core/views/abcratio-list-view.html',
            controller: 'AbcRatioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-abcratio-edit', {
            url: "/mpr-abcratio-edit/:abcratioId",
            templateUrl: rootUrl + 'app/mpr_core/views/abcratio-edit-view.html',
            controller: 'AbcRatioEditController as vm'

        }).state('mpr-sbu-list', {
            url: "/mpr-sbu-list",
            templateUrl: rootUrl + 'app/mpr_core/views/sbu-list-view.html',
            controller: 'SbuListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-sbu-edit', {
            url: "/mpr-sbu-edit/:sbuId",
            templateUrl: rootUrl + 'app/mpr_core/views/sbu-edit-view.html',
            controller: 'SbuEditController as vm'

        }).state('mpr-sbutype-list', {
            url: "/mpr-sbutype-list",
            templateUrl: rootUrl + 'app/mpr_core/views/sbutype-list-view.html',
            controller: 'SbuTypeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-sbutype-edit', {
            url: "/mpr-sbutype-edit/:sbutypeId",
            templateUrl: rootUrl + 'app/mpr_core/views/sbutype-edit-view.html',
            controller: 'SbuTypeEditController as vm'

        }).state('mpr-servicese-list', {
            url: "/mpr-servicese-list",
            templateUrl: rootUrl + 'app/mpr_core/views/servicese-list-view.html',
            controller: 'ServiceseListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-servicese-edit', {
            url: "/mpr-servicese-edit/:servicesId",
            templateUrl: rootUrl + 'app/mpr_core/views/servicese-edit-view.html',
            controller: 'ServiceseEditController as vm'

        }).state('mpr-ratios-list', {
            url: "/mpr-ratios-list",
            templateUrl: rootUrl + 'app/mpr_core/views/ratios-list-view.html',
            controller: 'RatiosListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-ratios-edit', {
            url: "/mpr-ratios-edit/:ratiosId",
            templateUrl: rootUrl + 'app/mpr_core/views/ratios-edit-view.html',
            controller: 'RatiosEditController as vm'

        }).state('mpr-ratiocaptionmapping-list', {
            url: "/mpr-ratiocaptionmapping-list",
            templateUrl: rootUrl + 'app/mpr_core/views/ratiocaptionmapping-list-view.html',
            controller: 'RatioCaptionMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-ratiocaptionmapping-edit', {
            url: "/mpr-ratiocaptionmapping-edit/:ratiocaptionmappingId",
            templateUrl: rootUrl + 'app/mpr_core/views/ratiocaptionmapping-edit-view.html',
            controller: 'RatioCaptionMappingEditController as vm'

        }).state('mpr-teamclassificationtype-edit', {
            url: "/mpr-teamclassificationtype-edit/:teamclassificationtypeId",
            templateUrl: rootUrl + 'app/mpr_core/views/teamclassificationtype-edit-view.html',
            controller: 'TeamClassificationTypeEditController as vm'
        }).state('mpr-teamclassification-list', {
            url: "/mpr-teamclassification-list",
            templateUrl: rootUrl + 'app/mpr_core/views/teamclassification-list-view.html',
            controller: 'TeamClassificationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-teamclassification-edit', {
            url: "/mpr-teamclassification-edit/:teamclassificationId",
            templateUrl: rootUrl + 'app/mpr_core/views/teamclassification-edit-view.html',
            controller: 'TeamClassificationEditController as vm'
        }).state('mpr-team-list', {
            url: "/mpr-team-list",
            templateUrl: rootUrl + 'app/mpr_core/views/team-list-view.html',
            controller: 'TeamListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-team-edit', {
            url: "/mpr-team-edit/:teamId",
            templateUrl: rootUrl + 'app/mpr_core/views/team-edit-view.html',
            controller: 'TeamEditController as vm'
        }).state('mpr-usermis-list', {
            url: "/mpr-usermis-list",
            templateUrl: rootUrl + 'app/mpr_core/views/usermis-list-view.html',
            controller: 'UserMISListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-usermis-edit', {
            url: "/mpr-usermis-edit/:usermisId",
            templateUrl: rootUrl + 'app/mpr_core/views/usermis-edit-view.html',
            controller: 'UserMISEditController as vm'
        }).state('mpr-accounttransferprice-list', {
            url: "/mpr-accounttransferprice-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/accounttransferprice-list-view.html',
            controller: 'AccountTransferPriceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-accounttransferprice-edit', {
            url: "/mpr-accounttransferprice-edit/:accounttransferpriceId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/accounttransferprice-edit-view.html',
            controller: 'AccountTransferPriceEditController as vm'
        }).state('mpr-generaltransferprice-list', {
            url: "/mpr-generaltransferprice-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/generaltransferprice-list-view.html',
            controller: 'GeneralTransferPriceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-generaltransferprice-edit', {
            url: "/mpr-generaltransferprice-edit/:generaltransferpriceId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/generaltransferprice-edit-view.html',
            controller: 'GeneralTransferPriceEditController as vm'
        }).state('mpr-teamclassificationmap-edit', {
            url: "/mpr-teamclassificationmap-edit/:teamId?miscode?definitioncode?teamclassificationmapId",
            templateUrl: rootUrl + 'app/mpr_core/views/teamclassificationmap-edit-view.html',
            controller: 'TeamClassificationMapEditController as vm'
        }).state('mpr-accountofficerdetail-list', {
            url: "/mpr-accountofficerdetail-list",
            templateUrl: rootUrl + 'app/mpr_core/views/accountofficerdetail-list-view.html',
            controller: 'AccountOfficerDetailListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-accountofficerdetail-edit', {
            url: "/mpr-accountofficerdetail-edit/:accountofficerdetailId",
            templateUrl: rootUrl + 'app/mpr_core/views/accountofficerdetail-edit-view.html',
            controller: 'AccountOfficerDetailEditController as vm'
        }).state('mpr-branchdefaultmis-list', {
            url: "/mpr-branchdefaultmis-list",
            templateUrl: rootUrl + 'app/mpr_core/views/branchdefaultmis-list-view.html',
            controller: 'BranchDefaultMISListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-branchdefaultmis-edit', {
            url: "/mpr-branchdefaultmis-edit/:branchdefaultmisId",
            templateUrl: rootUrl + 'app/mpr_core/views/branchdefaultmis-edit-view.html',
            controller: 'BranchDefaultMISEditController as vm'
        }).state('mpr-misreplacement-list', {
            url: "/mpr-misreplacement-list",
            templateUrl: rootUrl + 'app/mpr_core/views/misreplacement-list-view.html',
            controller: 'MISReplacementListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-misreplacement-edit', {
            url: "/mpr-misreplacement-edit/:misreplacementId",
            templateUrl: rootUrl + 'app/mpr_core/views/misreplacement-edit-view.html',
            controller: 'MISReplacementEditController as vm'

        }).state('mpr-bsexemption-list', {
            url: "/mpr-bsexemption-list",
            templateUrl: rootUrl + 'app/mpr_core/views/bsexemption-list-view.html',
            controller: 'BSExemptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-bsexemption-edit', {
            url: "/mpr-bsexemption-edit/:bsexemptionId",
            templateUrl: rootUrl + 'app/mpr_core/views/bsexemption-edit-view.html',
            controller: 'BSExemptionEditController as vm'



        }).state('mpr-memoaccountmap-list', {
            url: "/mpr-memoaccountmap-list",
            templateUrl: rootUrl + 'app/mpr_core/views/memoaccountmap-list-view.html',
            controller: 'MemoAccountMapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-memoaccountmap-edit', {
            url: "/mpr-memoaccountmap-edit/:memoaccountmapId",
            templateUrl: rootUrl + 'app/mpr_core/views/memoaccountmap-edit-view.html',
            controller: 'MemoAccountMapEditController as vm'

        }).state('mpr-memounit-list', {
            url: "/mpr-memounit-list",
            templateUrl: rootUrl + 'app/mpr_core/views/memounit-list-view.html',
            controller: 'MemoUnitListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-memounit-edit', {
            url: "/mpr-memounit-edit/:memounitId",
            templateUrl: rootUrl + 'app/mpr_core/views/memounit-edit-view.html',
            controller: 'MemoUnitEditController as vm'

        }).state('mpr-memoglmap-list', {
            url: "/mpr-memoglmap-list",
            templateUrl: rootUrl + 'app/mpr_core/views/memoglmap-list-view.html',
            controller: 'MemoGLMapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-memoglmap-edit', {
            url: "/mpr-memoglmap-edit/:memounitId",
            templateUrl: rootUrl + 'app/mpr_core/views/memoglmap-edit-view.html',
            controller: 'MemoGLMapEditController as vm'

        }).state('mpr-memoproductmap-list', {
            url: "/mpr-memoproductmap-list",
            templateUrl: rootUrl + 'app/mpr_core/views/memoproductmap-list-view.html',
            controller: 'MemoProductMapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-memoproductmap-edit', {
            url: "/mpr-memoproductmap-edit/:memoproductmapId",
            templateUrl: rootUrl + 'app/mpr_core/views/memoproductmap-edit-view.html',
            controller: 'MemoProductMapEditController as vm'


        }).state('mpr-custaccount-list', {
            url: "/mpr-custaccount-list",
            templateUrl: rootUrl + 'app/mpr_core/views/custaccount-list-view.html',
            controller: 'CustAccountListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-bscaption-list', {
            url: "/mpr-bscaption-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/bscaption-list-view.html',
            controller: 'BSCaptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-bscaption-edit', {
            url: "/mpr-bscaption-edit/:bscaptionId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/bscaption-edit-view.html',
            controller: 'BSCaptionEditController as vm'
        }).state('mpr-mprproduct-list', {
            url: "/mpr-mprproduct-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/mprproduct-list-view.html',
            controller: 'MPRProductListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-mprproduct-edit', {
            url: "/mpr-mprproduct-edit/:mprproductId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/mprproduct-edit-view.html',
            controller: 'MPRProductEditController as vm'
        }).state('mpr-bsglmapping-list', {
            url: "/mpr-bsglmapping-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/bsglmapping-list-view.html',
            controller: 'BSGLMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-bsglmapping-edit', {
            url: "/mpr-bsglmapping-edit/:bsglmappingId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/bsglmapping-edit-view.html',
            controller: 'BSGLMappingEditController as vm'
        }).state('mpr-nonproductmap-list', {
            url: "/mpr-nonproductmap-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/nonproductmap-list-view.html',
            controller: 'NonProductMapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-nonproductmap-edit', {
            url: "/mpr-nonproductmap-edit/:nonproductmapId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/nonproductmap-edit-view.html',
            controller: 'NonProductMapEditController as vm'
        }).state('mpr-nonproductrate-list', {
            url: "/mpr-nonproductrate-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/nonproductrate-list-view.html',
            controller: 'NonProductRateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-nonproductrate-edit', {
            url: "/mpr-nonproductrate-edit/:nonproductrateId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/nonproductrate-edit-view.html',
            controller: 'NonProductRateEditController as vm'
        }).state('mpr-productmis-list', {
            url: "/mpr-productmis-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/productmis-list-view.html',
            controller: 'ProductMISListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-productmis-edit', {
            url: "/mpr-productmis-edit/:productmisId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/productmis-edit-view.html',
            controller: 'ProductMISEditController as vm'
        }).state('mpr-balancesheetthreshold-list', {
            url: "/mpr-balancesheetthreshold-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/balancesheetthreshold-list-view.html',
            controller: 'BalanceSheetThresholdListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-balancesheetthreshold-edit', {
            url: "/mpr-balancesheetthreshold-edit/:balancesheetthresholdId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/balancesheetthreshold-edit-view.html',
            controller: 'BalanceSheetThresholdEditController as vm'
        }).state('mpr-balancesheetbudget-list', {
            url: "/mpr-balancesheetbudget-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/balancesheetbudget-list-view.html',
            controller: 'BalanceSheetBudgetListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-balancesheetbudget-edit', {
            url: "/mpr-balancesheetbudget-edit/:budgetId?budgettype",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/balancesheetbudget-edit-view.html',
            controller: 'BalanceSheetBudgetEditController as vm'
        }).state('mpr-plbudget-list', {
            url: "/mpr-plbudget-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/plbudget-list-view.html',
            controller: 'PLBudgetListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-plbudget-edit', {
            url: "/mpr-plbudget-edit/:budgetId?budgettype",
            templateUrl: rootUrl + 'app/mpr_pl/views/plbudget-edit-view.html',
            controller: 'PLBudgetEditController as vm'

        }).state('mpr-processdata-list', {
            url: "/mpr-processdata-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/processdata-list-view.html',
            controller: 'ProcessDataListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-processdata-edit', {
            url: "/mpr-processdata-edit/:processdataId",
            templateUrl: rootUrl + 'app/mpr_pl/views/processdata-edit-view.html',
            controller: 'ProcessDataEditController as vm'



        }).state('mpr-managementtree-list', {
            url: "/mpr-managementtree-list",
            templateUrl: rootUrl + 'app/mpr_core/views/managementtree-list-view.html',
            controller: 'ManagementTreeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-managementtree-edit', {
            url: "/mpr-managementtree-edit/:managementtreeId",
            templateUrl: rootUrl + 'app/mpr_core/views/managementtree-edit-view.html',
            controller: 'ManagementTreeEditController as vm'
        }).state('mpr-accountmis-list', {
            url: "/mpr-accountmis-list",
            templateUrl: rootUrl + 'app/mpr_core/views/accountmis-list-view.html',
            controller: 'AccountMISListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-accountmis-edit', {
            url: "/mpr-accountmis-edit/:accountmisId",
            templateUrl: rootUrl + 'app/mpr_core/views/accountmis-edit-view.html',
            controller: 'AccountMISEditController as vm'

        }).state('mpr-mprsetup-edit', {
            url: "/mpr-mprsetup-edit/:accountmisId",
            templateUrl: rootUrl + 'app/mpr_core/views/mprsetup-edit-view.html',
            controller: 'MPRSetupEditController as vm'
        }).state('mpr-transferprice-list', {
            url: "/mpr-transferprice-list",
            templateUrl: rootUrl + 'app/mpr_core/views/transferprice-list-view.html',
            controller: 'TransferPriceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-transferprice-edit', {
            url: "/mpr-transferprice-edit/:transferpriceId",
            templateUrl: rootUrl + 'app/mpr_core/views/transferprice-edit-view.html',
            controller: 'TransferPriceEditController as vm'




        }).state('mpr-mprbalancesheet-list', {
            url: "/mpr-mprbalancesheet-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/mprbalancesheet-list-view.html',
            controller: 'MPRBalanceSheetListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-mprbalancesheet-edit', {
            url: "/mpr-mprbalancesheet-edit/:balancesheetId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/mprbalancesheet-edit-view.html',
            controller: 'MPRBalanceSheetEditController as vm'



        }).state('mpr-mprbalancesheetadjustment-list', {
            url: "/mpr-mprbalancesheetadjustment-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/mprbalancesheetadjustment-list-view.html',
            controller: 'MPRBalanceSheetAdjustmentListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-mprbalancesheetadjustment-edit', {
            url: "/mpr-mprbalancesheetadjustment-edit/:mprbalancesheetadjustmentId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/mprbalancesheetadjustment-edit-view.html',
            controller: 'MPRBalanceSheetAdjustmentEditController as vm'
        }).state('mpr-unmappedproduct-list', {
            url: "/mpr-unmappedproduct-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/unmappedproduct-list-view.html',
            controller: 'UnMappedProductListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-plcaption-list', {
            url: "/mpr-plcaption-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/plcaption-list-view.html',
            controller: 'PLCaptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-plcaption-edit', {
            url: "/mpr-plcaption-edit/:plcaptionId",
            templateUrl: rootUrl + 'app/mpr_pl/views/plcaption-edit-view.html',
            controller: 'PLCaptionEditController as vm'
        }).state('mpr-mprglmapping-list', {
            url: "/mpr-mprglmapping-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/mprglmapping-list-view.html',
            controller: 'MPRGLMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-mprglmapping-edit', {
            url: "/mpr-mprglmapping-edit/:mprglmappingId",
            templateUrl: rootUrl + 'app/mpr_pl/views/mprglmapping-edit-view.html',
            controller: 'MPRGLMappingEditController as vm'
        }).state('mpr-unmappedgl-list', {
            url: "/mpr-unmappedgl-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/unmappedgl-list-view.html',
            controller: 'UnMappedPLGLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-glreclassification-list', {
            url: "/mpr-glreclassification-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/glreclassification-list-view.html',
            controller: 'GLReclassificationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-glreclassification-edit', {
            url: "/mpr-glreclassification-edit/:glreclassificationId",
            templateUrl: rootUrl + 'app/mpr_pl/views/glreclassification-edit-view.html',
            controller: 'GLReclassificationEditController as vm'
        }).state('mpr-glexception-list', {
            url: "/mpr-glexception-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/glexception-list-view.html',
            controller: 'GLExceptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-glexception-edit', {
            url: "/mpr-glexception-edit/:glexceptionId",
            templateUrl: rootUrl + 'app/mpr_pl/views/glexception-edit-view.html',
            controller: 'GLExceptionEditController as vm'
        }).state('mpr-glmis-list', {
            url: "/mpr-glmis-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/glmis-list-view.html',
            controller: 'GLMISListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-glmis-edit', {
            url: "/mpr-glmis-edit/:glmisId",
            templateUrl: rootUrl + 'app/mpr_pl/views/glmis-edit-view.html',
            controller: 'GLMISEditController as vm'



        }).state('mpr-revenue-list', {
            url: "/mpr-revenue-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/revenue-list-view.html',
            controller: 'RevenueListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-revenue-edit', {
            url: "/mpr-revenue-edit/:revenueId",
            templateUrl: rootUrl + 'app/mpr_pl/views/revenue-edit-view.html',
            controller: 'RevenueEditController as vm'



        }).state('mpr-mprcommfee-list', {
            url: "/mpr-mprcommfee-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/mprcommfee-list-view.html',
            controller: 'MPRCommFeeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-mprcommfee-edit', {
            url: "/mpr-mprcommfee-edit/:CommFee_Id",
            templateUrl: rootUrl + 'app/mpr_pl/views/mprcommfee-edit-view.html',
            controller: 'MPRCommFeeEditController as vm'


        }).state('mpr-plincomereportadjustment-list', {
            url: "/mpr-plincomereportadjustment-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/plincomereportadjustment-list-view.html',
            controller: 'PLIncomeReportAdjustmentListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-plincomereportadjustment-edit', {
            url: "/mpr-plincomereportadjustment-edit/:plincomereportadjustmentId",
            templateUrl: rootUrl + 'app/mpr_pl/views/plincomereportadjustment-edit-view.html',
            controller: 'PLIncomeReportAdjustmentEditController as vm'

        }).state('mpr-costcentredefinition-list', {
            url: "/mpr-costcentredefinition-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/costcentredefinition-list-view.html',
            controller: 'CostCentreDefinitionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-costcentredefinition-edit', {
            url: "/mpr-costcentredefinition-edit/:ccdefinitionId",
            templateUrl: rootUrl + 'app/mpr_opex/views/costcentredefinition-edit-view.html',
            controller: 'CostCentreDefinitionEditController as vm'

        }).state('mpr-costcentre-list', {
            url: "/mpr-costcentre-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/costcentre-list-view.html',
            controller: 'CostCentreListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-costcentre-edit', {
            url: "/mpr-costcentre-edit/:costcentreId",
            templateUrl: rootUrl + 'app/mpr_opex/views/costcentre-edit-view.html',
            controller: 'CostCentreEditController as vm'

        }).state('mpr-expensebasis-list', {
            url: "/mpr-expensebasis-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/expensebasis-list-view.html',
            controller: 'ExpenseBasisListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-expensebasis-edit', {
            url: "/mpr-expensebasis-edit/:expensebasisId",
            templateUrl: rootUrl + 'app/mpr_opex/views/expensebasis-edit-view.html',
            controller: 'ExpenseBasisEditController as vm'

        }).state('mpr-expensemapping-list', {
            url: "/mpr-expensemapping-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/expensemapping-list-view.html',
            controller: 'ExpenseMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-expensemapping-edit', {
            url: "/mpr-expensemapping-edit/:expensemappingId",
            templateUrl: rootUrl + 'app/mpr_opex/views/expensemapping-edit-view.html',
            controller: 'ExpenseMappingEditController as vm'

        }).state('mpr-expenseproductmapping-list', {
            url: "/mpr-expenseproductmapping-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/expenseproductmapping-list-view.html',
            controller: 'ExpenseProductMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-expenseproductmapping-edit', {
            url: "/mpr-expenseproductmapping-edit/:expenseproductId",
            templateUrl: rootUrl + 'app/mpr_opex/views/expenseproductmapping-edit-view.html',
            controller: 'ExpenseProductMappingEditController as vm'

        }).state('mpr-expenseglmapping-list', {
            url: "/mpr-expenseglmapping-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/expenseglmapping-list-view.html',
            controller: 'ExpenseGLMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-expenseglmapping-edit', {
            url: "/mpr-expenseglmapping-edit/:expenseglId",
            templateUrl: rootUrl + 'app/mpr_opex/views/expenseglmapping-edit-view.html',
            controller: 'ExpenseGLMappingEditController as vm'

        }).state('mpr-expenserawbasis-list', {
            url: "/mpr-expenserawbasis-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/expenserawbasis-list-view.html',
            controller: 'ExpenseRawBasisListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-expenserawbasis-edit', {
            url: "/mpr-expenserawbasis-edit/:expenserawbasisId",
            templateUrl: rootUrl + 'app/mpr_opex/views/expenserawbasis-edit-view.html',
            controller: 'ExpenseRawBasisEditController as vm'

        }).state('mpr-opexrawexpense-list', {
            url: "/mpr-opexrawexpense-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexrawexpense-list-view.html',
            controller: 'OpexRawExpenseListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexrawexpense-edit', {
            url: "/mpr-opexrawexpense-edit/:expenserawbasisId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexrawexpense-edit-view.html',
            controller: 'OpexRawExpenseEditController as vm'

        }).state('mpr-staffcost-list', {
            url: "/mpr-staffcost-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/staffcost-list-view.html',
            controller: 'StaffCostListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-staffcost-edit', {
            url: "/mpr-staffcost-edit/:staffcostId",
            templateUrl: rootUrl + 'app/mpr_opex/views/staffcost-edit-view.html',
            controller: 'StaffCostEditController as vm'

        }).state('mpr-opexmanagementtree-list', {
            url: "/mpr-opexmanagementtree-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexmanagementtree-list-view.html',
            controller: 'OpexManagementTreeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexmanagementtree-edit', {
            url: "/mpr-opexmanagementtree-edit/:opexmgtTreeId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexmanagementtree-edit-view.html',
            controller: 'OpexManagementTreeEditController as vm'

        }).state('mpr-activitybase-list', {
            url: "/mpr-activitybase-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/activitybase-list-view.html',
            controller: 'ActivityBaseListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-activitybase-edit', {
            url: "/mpr-activitybase-edit/:activitybaseId",
            templateUrl: rootUrl + 'app/mpr_opex/views/activitybase-edit-view.html',
            controller: 'ActivityBaseEditController as vm'

        }).state('mpr-activitybaseratio-list', {
            url: "/mpr-activitybaseratio-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/activitybaseratio-list-view.html',
            controller: 'ActivityBaseRatioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-activitybaseratio-edit', {
            url: "/mpr-activitybaseratio-edit/:activitybaseratioId",
            templateUrl: rootUrl + 'app/mpr_opex/views/activitybaseratio-edit-view.html',
            controller: 'ActivityBaseRatioEditController as vm'


        }).state('mpr-fixedassetsharingratio-list', {
            url: "/mpr-fixedassetsharingratio-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/fixedassetsharingratio-list-view.html',
            controller: 'FixedAssetSharingRatioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-fixedassetsharingratio-edit', {
            url: "/mpr-fixedassetsharingratio-edit/:fixedAssetSharingRatioId",
            templateUrl: rootUrl + 'app/mpr_opex/views/fixedassetsharingratio-edit-view.html',
            controller: 'FixedAssetSharingRatioEditController as vm'



        }).state('mpr-incomecentralvaultschedule-list', {
            url: "/mpr-incomecentralvaultschedule-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomecentralvaultschedule-list-view.html',
            controller: 'IncomeCentralVaultScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-incomecentralvaultschedule-edit', {
            url: "/mpr-incomecentralvaultschedule-edit/:incomeCentralVaultScheduleId",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomecentralvaultschedule-edit-view.html',
            controller: 'IncomeCentralVaultScheduleEditController as vm'
            /////hhhhhhhhhhhhhhhhhh/////
        }).state('mpr-incomecashcentrecode-list', {
            url: "/mpr-incomecashcentrecode-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/incomecashcentrecode-list-view.html',
            controller: 'IncomeCashCentreCodeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-incomecashcentrecode-edit', {
            url: "/mpr-incomecashcentrecode-edit/:incomecashcentrecodeId",
            templateUrl: rootUrl + 'app/mpr_opex/views/incomecashcentrecode-edit-view.html',
            controller: 'IncomeCashCentreCodeEditController as vm'


        }).state('mpr-incomeneaglsbu-list', {
            url: "/mpr-incomeneaglsbu-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/incomeneaglsbu-list-view.html',
            controller: 'IncomeNEAGLSBUListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-incomeneaglsbu-edit', {
            url: "/mpr-incomeneaglsbu-edit/:incomeneaglsbuId",
            templateUrl: rootUrl + 'app/mpr_opex/views/incomeneaglsbu-edit-view.html',
            controller: 'IncomeNEAGLSBUEditController as vm'

        }).state('mpr-categorytransferprice-list', {
            url: "/mpr-categorytransferprice-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/categorytransferprice-list-view.html',
            controller: 'CategoryTransferPriceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-categorytransferprice-edit', {
            url: "/mpr-categorytransferprice-edit/:categorytransferpriceId",
            templateUrl: rootUrl + 'app/mpr_opex/views/categorytransferprice-edit-view.html',
            controller: 'CategoryTransferPriceEditController as vm'


        }).state('mpr-neabranchsbushares-list', {
            url: "/mpr-neabranchsbushares-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/neabranchsbushares-list-view.html',
            controller: 'NEABranchSBUSharesListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-neabranchsbushares-edit', {
            url: "/mpr-neabranchsbushares-edit/:neabranchsbusharesId",
            templateUrl: rootUrl + 'app/mpr_opex/views/neabranchsbushares-edit-view.html',
            controller: 'NEABranchSBUSharesEditController as vm'


        }).state('mpr-opexabcexemption-list', {
            url: "/mpr-opexabcexemption-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexabcexemption-list-view.html',
            controller: 'OpexAbcExemptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexabcexemption-edit', {
            url: "/mpr-opexabcexemption-edit/:opexabcexemptionId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexabcexemption-edit-view.html',
            controller: 'OpexAbcExemptionEditController as vm'


        }).state('mpr-neabranchsharingratio-list', {
            url: "/mpr-neabranchsharingratio-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/neabranchsharingratio-list-view.html',
            controller: 'NEABranchSharingRatioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-neabranchsharingratio-edit', {
            url: "/mpr-neabranchsharingratio-edit/:neabranchsharingratioId",
            templateUrl: rootUrl + 'app/mpr_opex/views/neabranchsharingratio-edit-view.html',
            controller: 'NEABranchSharingRatioEditController as vm'


        }).state('mpr-neasharingratio-list', {
            url: "/mpr-neasharingratio-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/neasharingratio-list-view.html',
            controller: 'NEASharingRatioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-neasharingratio-edit', {
            url: "/mpr-neasharingratio-edit/:neasharingratioId",
            templateUrl: rootUrl + 'app/mpr_opex/views/neasharingratio-edit-view.html',
            controller: 'NEASharingRatioEditController as vm'


        }).state('mpr-neasharingratiofcy-list', {
            url: "/mpr-neasharingratiofcy-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/neasharingratiofcy-list-view.html',
            controller: 'NEASharingRatioFcyListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-neasharingratiofcy-edit', {
            url: "/mpr-neasharingratiofcy-edit/:neasharingratiofcyId",
            templateUrl: rootUrl + 'app/mpr_opex/views/neasharingratiofcy-edit-view.html',
            controller: 'NEASharingRatioFcyEditController as vm'


        }).state('mpr-opexbranchmapping-list', {
            url: "/mpr-opexbranchmapping-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexbranchmapping-list-view.html',
            controller: 'OpexBranchMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexbranchmapping-edit', {
            url: "/mpr-opexbranchmapping-edit/:opexbranchmappingId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexbranchmapping-edit-view.html',
            controller: 'OpexBranchMappingEditController as vm'


        }).state('mpr-lowcostremap-list', {
            url: "/mpr-lowcostremap-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/lowcostremap-list-view.html',
            controller: 'LowCostRemapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-lowcostremap-edit', {
            url: "/mpr-lowcostremap-edit/:lowcostremapId",
            templateUrl: rootUrl + 'app/mpr_opex/views/lowcostremap-edit-view.html',
            controller: 'LowCostRemapEditController as vm'



        }).state('mpr-opexmisreplacement-list', {
            url: "/mpr-opexmisreplacement-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexmisreplacement-list-view.html',
            controller: 'OpexMISReplacementListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexmisreplacement-edit', {
            url: "/mpr-opexmisreplacement-edit/:opexmisreplacementId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexmisreplacement-edit-view.html',
            controller: 'OpexMISReplacementEditController as vm'
        }).state('mpr-opexbusinessrule-list', {
            url: "/mpr-opexbusinessrule-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexbusinessrule-list-view.html',
            controller: 'OpexBusinessRuleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexbusinessrule-edit', {
            url: "/mpr-opexbusinessrule-edit/:opexbusinessruleId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexbusinessrule-edit-view.html',
            controller: 'OpexBusinessRuleEditController as vm'
        }).state('opex-opexglmapping-list', {
            url: "/opex-opexglmapping-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexglmapping-list-view.html',
            controller: 'OpexGLMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('opex-opexglmapping-edit', {
            url: "/opex-opexglmapping-edit/:glmappingId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexglmapping-edit-view.html',
            controller: 'OpexGLMappingEditController as vm'
        }).state('opex-unmappedgl-list', {
            url: "/opex-unmappedgl-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/unmappedgl-list-view.html',
            controller: 'UnMappedOpexGLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }



        }).state('mpr-opexreport-list', {
            url: "/mpr-opexreport-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexreport-list-view.html',
            controller: 'OpexReportListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-opexchecklist-list', {
            url: "/mpr-opexchecklist-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/checklist-list-view.html',
            controller: 'CheckListListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexglbasis-list', {
            url: "/mpr-opexglbasis-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexglbasis-list-view.html',
            controller: 'OpexGLBasisListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexglbasis-edit', {
            url: "/mpr-opexglbasis-edit/:opexglbasisId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexglbasis-edit-view.html',
            controller: 'OpexGLBasisEditController as vm'

        }).state('mpr-opexbasismapping-list', {
            url: "/mpr-opexbasismapping-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexbasismapping-list-view.html',
            controller: 'OpexBasisMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexbasismapping-edit', {
            url: "/mpr-opexbasismapping-edit/:opexbasismappingId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexbasismapping-edit-view.html',
            controller: 'OpexBasisMappingEditController as vm'



        }).state('mpr-hoexemptionmiscode-list', {
            url: "/mpr-hoexemptionmiscode-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/hoexemptionmiscode-list-view.html',
            controller: 'HoExemptionMISCodeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-hoexemptionmiscode-edit', {
            url: "/mpr-hoexemptionmiscode-edit/:id",
            templateUrl: rootUrl + 'app/mpr_opex/views/hoexemptionmiscode-edit-view.html',
            controller: 'HoExemptionMISCodeEditController as vm'


        }).state('mpr-messagingsubscription-edit', {
            url: "/mpr-messagingsubscription-edit/:messagingSubscriptionId",
            templateUrl: rootUrl + 'app/mpr_core/views/messagingsubscription-edit-view.html',
            controller: 'MessagingSubscriptionEditController as vm'
        })
        //CDQM
        .state('cdqm-address-list', {
            url: "/cdqm-address-list",
            templateUrl: rootUrl + 'app/cdqm/views/address-list-view.html',
            controller: 'AddressListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-address-edit', {
            url: "/cdqm-address-edit/:addressId",
            templateUrl: rootUrl + 'app/cdqm/views/address-edit-view.html',
            controller: 'AddressEditController as vm'
        }).state('cdqm-country-list', {
            url: "/cdqm-country-list",
            templateUrl: rootUrl + 'app/cdqm/views/country-list-view.html',
            controller: 'CDQMCountryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-country-edit', {
            url: "/cdqm-country-edit/:countryId",
            templateUrl: rootUrl + 'app/cdqm/views/country-edit-view.html',
            controller: 'CDQMCountryEditController as vm'
        }).state('cdqm-gendergroup-list', {
            url: "/cdqm-gendergroup-list",
            templateUrl: rootUrl + 'app/cdqm/views/gendergroup-list-view.html',
            controller: 'GenderGroupListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-gendergroup-edit', {
            url: "/cdqm-gendergroup-edit/:gendergroupId",
            templateUrl: rootUrl + 'app/cdqm/views/gendergroup-edit-view.html',
            controller: 'GenderGroupEditController as vm'
        }).state('cdqm-merchant-list', {
            url: "/cdqm-merchant-list",
            templateUrl: rootUrl + 'app/cdqm/views/merchant-list-view.html',
            controller: 'MerchantListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-merchant-edit', {
            url: "/cdqm-merchant-edit/:merchantId",
            templateUrl: rootUrl + 'app/cdqm/views/merchant-edit-view.html',
            controller: 'MerchantEditController as vm'
        }).state('cdqm-title-list', {
            url: "/cdqm-title-list",
            templateUrl: rootUrl + 'app/cdqm/views/title-list-view.html',
            controller: 'TitleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-title-edit', {
            url: "/cdqm-title-edit/:titleId",
            templateUrl: rootUrl + 'app/cdqm/views/title-edit-view.html',
            controller: 'TitleEditController as vm'
        }).state('cdqm-etlconfiguration-list', {
            url: "/cdqm-etlconfiguration-list",
            templateUrl: rootUrl + 'app/cdqm/views/etlconfiguration-list-view.html',
            controller: 'ETLConfigurationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-etlconfiguration-edit', {
            url: "/cdqm-etlconfiguration-edit/:etlconfigurationId",
            templateUrl: rootUrl + 'app/cdqm/views/etlconfiguration-edit-view.html',
            controller: 'ETLConfigurationEditController as vm'
        }).state('cdqm-product-list', {
            url: "/cdqm-product-list",
            templateUrl: rootUrl + 'app/cdqm/views/product-list-view.html',
            controller: 'CDQMProductListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-product-edit', {
            url: "/cdqm-product-edit/:productId",
            templateUrl: rootUrl + 'app/cdqm/views/product-edit-view.html',
            controller: 'CDQMProductEditController as vm'
        }).state('cdqm-customermis-list', {
            url: "/cdqm-customermis-list",
            templateUrl: rootUrl + 'app/cdqm/views/customermis-list-view.html',
            controller: 'CustomerMISListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-customermis-edit', {
            url: "/cdqm-customermis-edit/:customermisId",
            templateUrl: rootUrl + 'app/cdqm/views/customermis-edit-view.html',
            controller: 'CustomerMISEditController as vm'
        }).state('cdqm-customercheck-list', {
            url: "/cdqm-customercheck-list",
            templateUrl: rootUrl + 'app/cdqm/views/customercheck-list-view.html',
            controller: 'CustomerCheckListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


            //IFRS9



            //Begin Victor Update

        })

        .state('ifrs9-eurobondspread-list', {
            url: "/ifrs9-eurobondspread-list",
            templateUrl: rootUrl + 'app/IFRS9/views/eurobondspread-list-view.html',
            controller: 'EuroBondSpreadListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-eurobondspread-edit', {
            url: "/ifrs9-eurobondspread-edit/:eurobondspreadId",
            templateUrl: rootUrl + 'app/IFRS9/views/eurobondspread-edit-view.html',
            controller: 'EuroBondSpreadEditController as vm'
        }).state('ifrs9-placementcomputationresult-list', {
            url: "/ifrs9-placementcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/placementcomputationresult-list-view.html',
            controller: 'PlacementComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        })
        .state('ifrs9-ifrspdseriesbyrating-list', {
            url: "/ifrs9-ifrspdseriesbyrating-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrspdseriesbyrating-list-view.html',
            controller: 'IfrsPdSeriesByRatingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        })
        .state('ifrs9-ifrsretailpdseries-list', {
            url: "/ifrs9-ifrsretailpdseries-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsretailpdseries-list-view.html',
            controller: 'IfrsRetailPdSeriesListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        })
        .state('ifrs9-ifrsretailpdseries-edit', {
            url: "/ifrs9-ifrsretailpdseries-edit/:pdSeriesId",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsretailpdseries-edit-view.html',
            controller: 'IfrsRetailPdSeriesEditController as vm'
        })
        .state('ifrs9-ifrslgdscenariobyinstrument-list', {
            url: "/ifrs9-ifrslgdscenariobyinstrument-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrslgdscenariobyinstrument-list-view.html',
            controller: 'IfrsLgdScenarioByInstrumentListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-ifrslgdscenariobyinstrument-edit', {
            url: "/ifrs9-ifrslgdscenariobyinstrument-edit/:instrumentId",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrslgdscenariobyinstrument-edit-view.html',
            controller: 'IfrsLgdScenarioByInstrumentEditController as vm'

        })
        .state('ifrs9-macrovariablerecoveryrates-list', {
            url: "/ifrs9-macrovariablerecoveryrates-list",
            templateUrl: rootUrl + 'app/IFRS9/views/macrovariablerecoveryrates-list-view.html',
            controller: 'macrovariablerecoveryratesListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-macrovariablerecoveryrates-edit', {
            url: "/ifrs9-macrovariablerecoveryrates-edit/:instrumentId",
            templateUrl: rootUrl + 'app/IFRS9/views/macrovariablerecoveryrates-edit-view.html',
            controller: 'macrovariablerecoveryratesEditController as vm'

        })
        .state('ifrs9-ifrscorporateecl-list', {
            url: "/ifrs9-ifrscorporateecl-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrscorporateecl-list-view.html',
            controller: 'IfrsCorporateEclListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-investmentothersecl-list', {
            url: "/ifrs9-investmentothersecl-list",
            templateUrl: rootUrl + 'app/IFRS9/views/investmentothersecl-list-view.html',
            controller: 'InvestmentOthersECLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-ifrssectorccf-list', {
            url: "/ifrs9-ifrssectorccf-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrssectorccf-list-view.html',
            controller: 'IfrsSectorCCFListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-ifrssectorccf-edit', {
            url: "/ifrs9-ifrssectorccf-edit/:SectorId",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrssectorccf-edit-view.html',
            controller: 'IfrsSectorCCFEditController as vm'

        }).state('ifrs9-lgdcomputationresult-list', {
            url: "/ifrs9-lgdcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/lgdcomputationresult-list-view.html',
            controller: 'LgdComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-lgdcomputationresultplacement-list', {
            url: "/ifrs9-lgdcomputationresultplacement-list",
            templateUrl: rootUrl + 'app/IFRS9/views/lgdcomputationresultplacement-list-view.html',
            controller: 'LgdComputationResultPlacementListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-localbondspread-list', {
            url: "/ifrs9-localbondspread-list",
            templateUrl: rootUrl + 'app/IFRS9/views/localbondspread-list-view.html',
            controller: 'LocalBondSpreadListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-localbondspread-edit', {
            url: "/ifrs9-localbondspread-edit/:localbondspreadId",
            templateUrl: rootUrl + 'app/IFRS9/views/localbondspread-edit-view.html',
            controller: 'LocalBondSpreadEditController as vm'
        }).state('ifrs9-marginalpddistribution-list', {
            url: "/ifrs9-marginalpddistribution-list",
            templateUrl: rootUrl + 'app/IFRS9/views/marginalpddistribution-list-view.html',
            controller: 'MarginalPDDistributionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-bondmarginalpddistribution-list', {
            url: "/ifrs9-bondmarginalpddistribution-list",
            templateUrl: rootUrl + 'app/IFRS9/views/bondmarginalpddistribution-list-view.html',
            controller: 'BondMarginalPDDistributionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-marginalpddistributionplacement-list', {
            url: "/ifrs9-marginalpddistributionplacement-list",
            templateUrl: rootUrl + 'app/IFRS9/views/marginalpddistributionplacement-list-view.html',
            controller: 'MarginalPDDistributionPlacementListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-eclcomputationresult-list', {
            url: "/ifrs9-eclcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/eclcomputationresult-list-view.html',
            controller: 'EclComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-bondeclcomputationresult-list', {
            url: "/ifrs9-bondeclcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/bondeclcomputationresult-list-view.html',
            controller: 'BondEclComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-tBilleclcomputationresult-list', {
            url: "/ifrs9-tBilleclcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/tBilleclcomputationresult-list-view.html',
            controller: 'TBillEclComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-placementeclcomputationresult-list', {
            url: "/ifrs9-placementeclcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/placementeclcomputationresult-list-view.html',
            controller: 'PlacementEclComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-lcbgeclcomputationresult-list', {
            url: "/ifrs9-lcbgeclcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/lcbgeclcomputationresult-list-view.html',
            controller: 'LcBgEclComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        })




        //End Victor Update


        .state('ifrs9-externalrating-list', {
            url: "/ifrs9-externalrating-list",
            templateUrl: rootUrl + 'app/IFRS9/views/externalrating-list-view.html',
            controller: 'ExternalRatingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-externalrating-edit', {
            url: "/ifrs9-externalrating-edit/:externalratingId",
            templateUrl: rootUrl + 'app/IFRS9/views/externalrating-edit-view.html',
            controller: 'ExternalRatingEditController as vm'

        }).state('ifrs9-historicalsectorrating-list', {
            url: "/ifrs9-historicalsectorrating-list",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalsectorrating-list-view.html',
            controller: 'HistoricalSectorRatingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-historicalsectorrating-edit', {
            url: "/ifrs9-historicalsectorrating-edit/:historicalsectorratingId",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalsectorrating-edit-view.html',
            controller: 'HistoricalSectorRatingEditController as vm'



        }).state('ifrs9-internalratingbased-list', {
            url: "/ifrs9-internalratingbased-list",
            templateUrl: rootUrl + 'app/IFRS9/views/internalratingbased-list-view.html',
            controller: 'InternalRatingBasedListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-internalratingbased-edit', {
            url: "/ifrs9-internalratingbased-edit/:internalratingbasedId",
            templateUrl: rootUrl + 'app/IFRS9/views/internalratingbased-edit-view.html',
            controller: 'InternalRatingBasedEditController as vm'

        }).state('ifrs9-macroeconomic-list', {
            url: "/ifrs9-macroeconomic-list",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomic-list-view.html',
            controller: 'MacroEconomicListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-macroeconomic-edit', {
            url: "/ifrs9-macroeconomic-edit/:macroeconomicId",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomic-edit-view.html',
            controller: 'MacroEconomicEditController as vm'



        }).state('ifrs9-computedforcastedpdlgd-list', {
            url: "/ifrs9-computedforcastedpdlgd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/computedforcastedpdlgd-list-view.html',
            controller: 'ComputedForcastedPDLGDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-computedforcastedpdlgdforeign-list', {
            url: "/ifrs9-computedforcastedpdlgdforeign-list",
            templateUrl: rootUrl + 'app/IFRS9/views/computedforcastedpdlgdforeign-list-view.html',
            controller: 'ComputedForcastedPDLGDForeignListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-macroeconomicsnpl-list', {
            url: "/ifrs9-macroeconomicsnpl-list",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomicsnpl-list-view.html',
            controller: 'MacroEconomicsNPLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-macroeconomicsnpl-edit', {
            url: "/ifrs9-macroeconomicsnpl-edit/:macroeconomicnplId",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomicsnpl-edit-view.html',
            controller: 'MacroEconomicsNPLEditController as vm'


        }).state('ifrs9-monthlydiscountfactor-list', {
            url: "/ifrs9-monthlydiscountfactor-list",
            templateUrl: rootUrl + 'app/IFRS9/views/monthlydiscountfactor-list-view.html',
            controller: 'MonthlyDiscountFactorListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs9-monthlydiscountfactorplacement-list', {
            url: "/ifrs9-monthlydiscountfactorplacement-list",
            templateUrl: rootUrl + 'app/IFRS9/views/monthlydiscountfactorplacement-list-view.html',
            controller: 'MonthlyDiscountFactorPlacementListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs9-monthlydiscountfactorbond-list', {
            url: "/ifrs9-monthlydiscountfactorbond-list",
            templateUrl: rootUrl + 'app/IFRS9/views/monthlydiscountfactorbond-list-view.html',
            controller: 'MonthlyDiscountFactorBondListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-ratingmapping-list', {
            url: "/ifrs9-ratingmapping-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ratingmapping-list-view.html',
            controller: 'RatingMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-ratingmapping-edit', {
            url: "/ifrs9-ratingmapping-edit/:ratingmappingId",
            templateUrl: rootUrl + 'app/IFRS9/views/ratingmapping-edit-view.html',
            controller: 'RatingMappingEditController as vm'


        }).state('ifrs9-transition-list', {
            url: "/ifrs9-transition-list",
            templateUrl: rootUrl + 'app/IFRS9/views/transition-list-view.html',
            controller: 'TransitionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-transition-edit', {
            url: "/ifrs9-transition-edit/:transitionId",
            templateUrl: rootUrl + 'app/IFRS9/views/transition-edit-view.html',
            controller: 'TransitionEditController as vm'

        }).state('ifrs9-sector-list', {
            url: "/ifrs9-sector-list",
            templateUrl: rootUrl + 'app/IFRS9/views/sector-list-view.html',
            controller: 'SectorListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-sector-edit', {
            url: "/ifrs9-sector-edit/:sectorId",
            templateUrl: rootUrl + 'app/IFRS9/views/sector-edit-view.html',
            controller: 'SectorEditController as vm'


        }).state('ifrs9-historicalclassification-list', {
            url: "/ifrs9-historicalclassification-list",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalclassification-list-view.html',
            controller: 'HistoricalClassificationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-historicalclassification-edit', {
            url: "/ifrs9-historicalclassification-edit/:historicalclassificationId",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalclassification-edit-view.html',
            controller: 'HistoricalClassificationEditController as vm'


        }).state('ifrs9-macroeconomichistorical-list', {
            url: "/ifrs9-macroeconomichistorical-list",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomichistorical-list-view.html',
            controller: 'MacroEconomicHistoricalListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-macroeconomichistorical-edit', {
            url: "/ifrs9-macroeconomichistorical-edit/:macroeconomichistoricalId",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomichistorical-edit-view.html',
            controller: 'MacroEconomicHistoricalEditController as vm'


        }).state('ifrs9-setup-list', {
            url: "/ifrs9-setup-list",
            templateUrl: rootUrl + 'app/IFRS9/views/setup-list-view.html',
            controller: 'SetUpListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-setup-edit', {
            url: "/ifrs9-setup-edit/:setupId",
            templateUrl: rootUrl + 'app/IFRS9/views/setup-edit-view.html',
            controller: 'SetUpEditController as vm'


        }).state('ifrs9-notchdifference-list', {
            url: "/ifrs9-notchdifference-list",
            templateUrl: rootUrl + 'app/IFRS9/views/notchdifference-list-view.html',
            controller: 'NotchDifferenceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-notchdifference-edit', {
            url: "/ifrs9-notchdifference-edit/:notchdifferenceId",
            templateUrl: rootUrl + 'app/IFRS9/views/notchdifference-edit-view.html',
            controller: 'NotchDifferenceEditController as vm'

        }).state('ifrs9-historicalsectorialpd-list', {
            url: "/ifrs9-historicalsectorialpd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalsectorialpd-list-view.html',
            controller: 'HistoricalSectorialPDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-historicalsectoriallgd-edit', {
            url: "/ifrs9-historicalsectoriallgd-edit/:historicalsectoriallgdId",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalsectoriallgd-edit-view.html',
            controller: 'HistoricalSectorialLGDEditController as vm'

        }).state('ifrs9-historicalsectoriallgd-list', {
            url: "/ifrs9-historicalsectoriallgd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalsectoriallgd-list-view.html',
            controller: 'HistoricalSectorialLGDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-historicalsectorialpd-edit', {
            url: "/ifrs9-historicalsectorialpd-edit/:historicalsectorialpdId",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalsectorialpd-edit-view.html',
            controller: 'HistoricalSectorialPDEditController as vm'



        }).state('ifrs9-sectorialregressedpd-list', {
            url: "/ifrs9-sectorialregressedpd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/sectorialregressedpd-list-view.html',
            controller: 'SectorialRegressedPDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs9-sectorialregressedlgd-list', {
            url: "/ifrs9-sectorialregressedlgd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/sectorialregressedlgd-list-view.html',
            controller: 'SectorialRegressedLGDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

            //}).state('ifrs9-computedforcastedpdlgd-list', {
            //    url: "/ifrs9-computedforcastedpdlgd-list",
            //    templateUrl: rootUrl + 'app/IFRS9/views/computedforcastedpdlgd-list-view.html',
            //    controller: 'ComputedForcastedPDLGDListController as vm',
            //    resolve: {
            //        loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
            //            return $ocLazyLoad.load({
            //                files: [
            //                      rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
            //                       rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
            //                       rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
            //            });
            //        }]
            //    }

        }).state('ifrs9-macroeconomicvariable-list', {
            url: "/ifrs9-macroeconomicvariable-list",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomicvariable-list-view.html',
            controller: 'MacroEconomicVariableListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-macroeconomicvariable-edit', {
            url: "/ifrs9-macroeconomicvariable-edit/:macroeconomicvariableId",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomicvariable-edit-view.html',
            controller: 'MacroEconomicVariableEditController as vm'


        }).state('ifrs9-sectorvariablemapping-list', {
            url: "/ifrs9-sectorvariablemapping-list",
            templateUrl: rootUrl + 'app/IFRS9/views/sectorvariablemapping-list-view.html',
            controller: 'SectorVariableMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-sectorvariablemapping-edit', {
            url: "/ifrs9-sectorvariablemapping-edit/:sectorvariablemappingId",
            templateUrl: rootUrl + 'app/IFRS9/views/sectorvariablemapping-edit-view.html',
            controller: 'SectorVariableMappingEditController as vm'

        }).state('ifrs9-dashboard-list', {
            url: "/ifrs9-dashboard-list-view",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrs9-dashboard-list-view.html',
            controller: 'IFRS9DashboardController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-pitpd-list', {
            url: "/ifrs9-pitpd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/pitpd-list-view.html',
            controller: 'PiTPDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-loanbucketdistribution-list', {
            url: "/ifrs9-loanbucketdistribution-list",
            templateUrl: rootUrl + 'app/IFRS9/views/loanbucketdistribution-list-view.html',
            controller: 'LoanBucketDistributionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-pitformular-list', {
            url: "/ifrs9-pitformular-list",
            templateUrl: rootUrl + 'app/IFRS9/views/pitformular-list-view.html',
            controller: 'PitFormularListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-macroeconomicvdisplay-list', {
            url: "/ifrs9-macroeconomicvdisplay-list",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomicvdisplay-list-view.html',
            controller: 'MacroeconomicVDisplayListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-lifetimepdclassification-list', {
            url: "/ifrs9-lifetimepdclassification-list",
            templateUrl: rootUrl + 'app/IFRS9/views/lifetimepdclassification-list-view.html',
            controller: 'LifeTimePDClassificationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-performingloan-list', {
            url: "/ifrs9-performingloan-list",
            templateUrl: rootUrl + 'app/IFRS9/views/performingloan-list-view.html',
            controller: 'PerformingLoanListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-underperformingloan-list', {
            url: "/ifrs9-underperformingloan-list",
            templateUrl: rootUrl + 'app/IFRS9/views/underperformingloan-list-view.html',
            controller: 'UnderPerformingLoanListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-nonperformingloan-list', {
            url: "/ifrs9-nonperformingloan-list",
            templateUrl: rootUrl + 'app/IFRS9/views/nonperformingloan-list-view.html',
            controller: 'NonPerformingLoanListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-summaryreport-list', {
            url: "/ifrs9-summaryreport-list",
            templateUrl: rootUrl + 'app/IFRS9/views/summaryreport-list-view.html',
            controller: 'SummaryReportController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-eclcalculationmodel-list', {
            url: "/ifrs9-eclcalculationmodel-list",
            templateUrl: rootUrl + 'app/IFRS9/views/eclcalculationmodel-list-view.html',
            controller: 'EclCalculationModelListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-ifrsequityunqouted-list', {
            url: "/ifrs9-ifrsequityunqouted-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsequityunqouted-list-view.html',
            controller: 'IfrsEquityUnqoutedListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-ifrsequityunqouted-edit', {
            url: "/ifrs9-ifrsequityunqouted-edit/:ifrsequityunqoutedId",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsequityunqouted-edit-view.html',
            controller: 'IfrsEquityUnqoutedEditController as vm'

        }).state('ifrs9-ifrsstocksmapping-list', {
            url: "/ifrs9-ifrsstocksmapping-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsstocksmapping-list-view.html',
            controller: 'IfrsStocksMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-ifrsstocksmapping-edit', {
            url: "/ifrs9-ifrsstocksmapping-edit/:ifrsstocksmappingId",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsstocksmapping-edit-view.html',
            controller: 'IfrsStocksMappingEditController as vm'


        }).state('ifrs9-ifrsstocksprimarydata-list', {
            url: "/ifrs9-ifrsstocksprimarydata-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsstocksprimarydata-list-view.html',
            controller: 'IfrsStocksPrimaryDataListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-ifrsstocksprimarydata-edit', {
            url: "/ifrs9-ifrsstocksprimarydata-edit/:ifrsstocksprimarydataId",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsstocksprimarydata-edit-view.html',
            controller: 'IfrsStocksPrimaryDataEditController as vm'
        }).state('ifrs9-reconciliation-list', {
            url: "/ifrs9-reconciliation-list",
            templateUrl: rootUrl + 'app/IFRS9/views/reconciliation-list-view.html',
            controller: 'ReconciliationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-forecastedmacroeconimcssensitivity-list', {
            url: "/ifrs9-forecastedmacroeconimcssensitivity-list",
            templateUrl: rootUrl + 'app/IFRS9/views/forecastedmacroeconimcssensitivity-list-view.html',
            controller: 'ForecastedMacroeconimcsSensitivityListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-forecastedmacroeconimcsscenario-list', {
            url: "/ifrs9-forecastedmacroeconimcsscenario-list",
            templateUrl: rootUrl + 'app/IFRS9/views/forecastedmacroeconimcsscenario-list-view.html',
            controller: 'ForecastedMacroeconimcsScenarioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-fairvaluationmodel-list', {
            url: "/ifrs9-fairvaluationmodel-list",
            templateUrl: rootUrl + 'app/IFRS9/views/fairvaluationmodel-list-view.html',
            controller: 'FairValuationModelListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-loanspreadscenario-list', {
            url: "/ifrs9-loanspreadscenario-list",
            templateUrl: rootUrl + 'app/IFRS9/views/loanspreadscenario-list-view.html',
            controller: 'LoanSpreadScenarioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-loanspreadsensitivity-list', {
            url: "/ifrs9-loanspreadsensitivity-list",
            templateUrl: rootUrl + 'app/IFRS9/views/loanspreadsensitivity-list-view.html',
            controller: 'LoanSpreadSensitivityListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-unquotedequityfairvalueresult-list', {
            url: "/ifrs9-unquotedequityfairvalueresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/unquotedequityfairvalueresult-list-view.html',
            controller: 'UnquotedEquityFairvalueResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-markovmatrix-list', {
            url: "/ifrs9-markovmatrix-list",
            templateUrl: rootUrl + 'app/IFRS9/views/markovmatrix-list-view.html',
            controller: 'MarkovMatrixListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-pitpdcomparism-list', {
            url: "/ifrs9-pitpdcomparism-list",
            templateUrl: rootUrl + 'app/IFRS9/views/pitpdcomparism-list-view.html',
            controller: 'PiTPDComparismListController as vm'

        }).state('ifrs9-eclcomparism-list', {
            url: "/ifrs9-eclcomparism-list",
            templateUrl: rootUrl + 'app/IFRS9/views/eclcomparism-list-view.html',
            controller: 'ECLComparismListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-ccfmodelling-list', {
            url: "/ifrs9-ccfmodelling-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ccfmodelling-list-view.html',
            controller: 'CCFModellingListController as vm'

        }).state('ifrs9-foreigneadexchangerate-list', {
            url: "/ifrs9-foreigneadexchangerate-list",
            templateUrl: rootUrl + 'app/IFRS9/views/foreigneadexchangerate-list-view .html',
            controller: 'ForeignEADExchangeRateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-foreigneadexchangerate-edit', {
            url: "/ifrs9-foreigneadexchangerate-edit-view/:foreigneadexchangerateId",
            templateUrl: rootUrl + 'app/IFRS9/views/foreigneadexchangerate-edit-view.html',
            controller: 'ForeignEADExchangeRateEditController as vm'

        }).state('ifrs9-impairmentresultretail-list', {
            url: "/ifrs9-impairmentresultretail-list",
            templateUrl: rootUrl + 'app/IFRS9/views/impairmentresultretail-list-view.html',
            controller: 'ImpairmentResultRetailListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-impairmentresultobe-list', {
            url: "/ifrs9-impairmentresultobe-list",
            templateUrl: rootUrl + 'app/IFRS9/views/impairmentresultobe-list-view.html',
            controller: 'ImpairmentResultOBEListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-impairmentcorporate-list', {
            url: "/ifrs9-impairmentcorporate-list",
            templateUrl: rootUrl + 'app/IFRS9/views/impairmentcorporate-list-view.html',
            controller: 'ImpairmentCorporateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-impairmentinvestment-list', {
            url: "/ifrs9-impairmentinvestment-list",
            templateUrl: rootUrl + 'app/IFRS9/views/impairmentinvestment-list-view.html',
            controller: 'ImpairmentInvestmentListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-ifrsfinalretailoutput-list', {
            url: "/ifrs9-ifrsfinalretailoutput-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsfinalretailoutput-list-view.html',
            controller: 'ifrsfinalretailoutputListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-ifrscustomerpd-list', {
            url: "/ifrs9-ifrscustomerpd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrscustomerpd-list-view.html',
            controller: 'ifrscustomerpdListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-ifrscustomerpd-edit', {
            url: "/ifrs9-ifrscustomerpd-edit/:customerPDId",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrscustomerpd-edit-view.html',
            controller: 'ifrscustomerpdEditController as vm'

        }).state('ifrs9-ifrscorporatepdseries-list', {
            url: "/ifrs9-ifrscorporatepdseries-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrscorporatepdseries-list-view.html',
            controller: 'IfrsCorporatePdSeriesListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-eclinputretail-list', {
            url: "/ifrs9-eclinputretail-list",
            templateUrl: rootUrl + 'app/IFRS9/views/eclinputretail-list-view.html',
            controller: 'ECLInputRetailController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-eclinputretail-edit', {
            url: "/ifrs9-eclinputretail-edit/:eclInputRetailId",
            templateUrl: rootUrl + 'app/IFRS9/views/eclinputretail-edit-view.html',
            controller: 'ECLInputRetailEditController as vm'

        }).state('ifrs9-collateralinput-list', {
            url: "/ifrs9-collateralinput-list",
            templateUrl: rootUrl + 'app/IFRS9/views/collateralinput-list-view.html',
            controller: 'CollateralInputListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-collateralinput-edit', {
            url: "/ifrs9-collateralinput-edit/:Collateral_Id",
            templateUrl: rootUrl + 'app/IFRS9/views/collateralinput-edit-view.html',
            controller: 'CollateralInputEditController as vm'



        }).state('ifrs9-postingglmapping-list', {
            url: "/ifrs9-postingglmapping-list",
            templateUrl: rootUrl + 'app/IFRS9/views/postingglmapping-list-view.html',
            controller: 'PostingGLMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-postingglmapping-edit', {
            url: "/ifrs9-postingglmapping-edit/:ID",
            templateUrl: rootUrl + 'app/IFRS9/views/postingglmapping-edit-view.html',
            controller: 'PostingGLMappingEditController as vm'



        }).state('ifrs9-assumption-list', {
            url: "/ifrs9-assumption-list",
            templateUrl: rootUrl + 'app/IFRS9/views/assumption-list-view.html',
            controller: 'AssumptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-assumption-edit', {
            url: "/ifrs9-assumption-edit/:InstrumentID",
            templateUrl: rootUrl + 'app/IFRS9/views/assumption-edit-view.html',
            controller: 'AssumptionEditController as vm'




        }).state('ifrs9-spcumulativepd-list', {
            url: "/ifrs9-spcumulativepd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/spcumulativepd-list-view.html',
            controller: 'SPCumulativePDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-spcumulativepd-edit', {
            url: "/ifrs9-spcumulativepd-edit/:SPCumulative_Id",
            templateUrl: rootUrl + 'app/IFRS9/views/spcumulativepd-edit-view.html',
            controller: 'SPCumulativePDEditController as vm'


        }).state('ifrs-depositrepaymentschedule-list', {
            url: "/ifrs-depositrepaymentschedule-list",

            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/depositrepaymentschedule-list-view.html',
            controller: 'DepositRepaymentScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-depositrepaymentschedule-edit', {
            url: "/ifrs-depositrepaymentschedule-edit/:depositRepayId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/depositrepaymentschedule-edit-view.html',
            controller: 'DepositRepaymentScheduleEditController as vm'

        }).state('ifrs-liabilityrepaymentschedule-list', {
            url: "/ifrs-liabilityrepaymentschedule-list",

            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/liabilityrepaymentschedule-list-view.html',
            controller: 'LiabilityRepaymentScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-liabilityrepaymentschedule-edit', {
            url: "/ifrs-liabilityrepaymentschedule-edit/:depositRepayId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/liabilityrepaymentschedule-edit-view.html',
            controller: 'LiabilityRepaymentScheduleEditController as vm'


        }).state('ifrs-loancommitmentcomputationresult-list', {
            url: "/ifrs-loancommitmentcomputationresult-list",

            templateUrl: rootUrl + 'app/IFRS9/views/loancommitmentcomputationresult-list-view.html',
            controller: 'LoanCommitmentComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs-loancommitment-list', {
            url: "/ifrs-loancommitment-list",

            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loancommitment-list-view.html',
            controller: 'LoanCommitmentListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loancommitment-edit', {
            url: "/ifrs-loancommitment-edit/:loanCommitmentId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loancommitment-edit-view.html',
            controller: 'LoanCommitmentEditController as vm'


        }).state('ifrs-inputdetail-list', {
            url: "/ifrs-inputdetail-list",

            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/inputdetail-list-view.html',
            controller: 'InputDetailListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-inputdetail-edit', {
            url: "/ifrs-inputdetail-edit/:inputDetailId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/inputdetail-edit-view.html',
            controller: 'InputDetailEditController as vm'


        }).state('ifrs-nseindex-list', {
            url: "/ifrs-nseindex-list",

            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/nseindex-list-view.html',
            controller: 'NseIndexListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-nseindex-edit', {
            url: "/ifrs-nseindex-edit/:nseIndexId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/nseindex-edit-view.html',
            controller: 'NseIndexEditController as vm'



            //IFRS9 END


            //Scorecard states
        }).state('scd-dashboard-graph', {
            url: "/scd-dashboard-graph",
            templateUrl: rootUrl + 'app/scorecard/views/scd-dashboard-graph-view.html',
            controller: 'ScorecardDashboardController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-analytic-graph', {
            url: "/scd-analytic-graph",
            templateUrl: rootUrl + 'app/scorecard/views/scd-analytic-graph-view.html',
            controller: 'ScorecardAnalyticController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-dataentry-list', {
            url: "/scd-dataentry-list",
            templateUrl: rootUrl + 'app/scorecard/views/dataentry-list-view.html',
            controller: 'KPIDataEntryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-dataentry-edit', {
            url: "/scd-dataentry-edit/:dataentryId",
            templateUrl: rootUrl + 'app/scorecard/views/dataentry-edit-view.html',
            controller: 'KPIDataEntryEditController as vm'
        }).state('scd-setup-list', {
            url: "/scd-setup-list",
            templateUrl: rootUrl + 'app/scorecard/views/setup-edit-view.html',
            controller: 'SCDSetupEditController as vm'
        }).state('scd-teamclassification-list', {
            url: "/scd-teamclassification-list",
            templateUrl: rootUrl + 'app/scorecard/views/teamclassification-list-view.html',
            controller: 'SCDTeamClassificationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-teamclassification-edit', {
            url: "/scd-teamclassification-edit/:teamclassificationId",
            templateUrl: rootUrl + 'app/scorecard/views/teamclassification-edit-view.html',
            controller: 'SCDTeamClassificationEditController as vm'
        }).state('scd-teammapping-list', {
            url: "/scd-teammapping-list",
            templateUrl: rootUrl + 'app/scorecard/views/teammap-list-view.html',
            controller: 'TeamMapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-teammap-edit', {
            url: "/scd-teammap-edit/:teammapId",
            templateUrl: rootUrl + 'app/scorecard/views/teammap-edit-view.html',
            controller: 'TeamMapEditController as vm'
        }).state('scd-category-list', {
            url: "/scd-category-list",
            templateUrl: rootUrl + 'app/scorecard/views/category-list-view.html',
            controller: 'CategoryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-category-edit', {
            url: "/scd-category-edit/:categoryId",
            templateUrl: rootUrl + 'app/scorecard/views/category-edit-view.html',
            controller: 'CategoryEditController as vm'
        }).state('scd-metric-list', {
            url: "/scd-metric-list",
            templateUrl: rootUrl + 'app/scorecard/views/metric-list-view.html',
            controller: 'KPIMetricListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-metric-edit', {
            url: "/scd-metric-edit/:metricId",
            templateUrl: rootUrl + 'app/scorecard/views/metric-edit-view.html',
            controller: 'KPIMetricEditController as vm'
        }).state('scd-classification-list', {
            url: "/scd-classification-list",
            templateUrl: rootUrl + 'app/scorecard/views/kpiclassification-list-view.html',
            controller: 'KPIClassificationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-kpiclassification-edit', {
            url: "/scd-kpiclassification-edit/:classificationId",
            templateUrl: rootUrl + 'app/scorecard/views/kpiclassification-edit-view.html',
            controller: 'KPIClassificationEditController as vm'
        }).state('scd-participant-list', {
            url: "/scd-participant-list",
            templateUrl: rootUrl + 'app/scorecard/views/participant-list-view.html',
            controller: 'KPIParticipantListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-participant-edit', {
            url: "/scd-participant-edit/:participantId",
            templateUrl: rootUrl + 'app/scorecard/views/participant-edit-view.html',
            controller: 'KPIParticipantEditController as vm'
        }).state('scd-threshold-list', {
            url: "/scd-threshold-list",
            templateUrl: rootUrl + 'app/scorecard/views/threshold-list-view.html',
            controller: 'KPIThresholdListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-threshold-edit', {
            url: "/scd-threshold-edit/:thresholdId",
            templateUrl: rootUrl + 'app/scorecard/views/threshold-edit-view.html',
            controller: 'KPIThresholdEditController as vm'
        }).state('scd-actualdata-list', {
            url: "/scd-actualdata-list",
            templateUrl: rootUrl + 'app/scorecard/views/actualdata-list-view.html',
            controller: 'SCDActualDataListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-actualdata-edit', {
            url: "/scd-actualdata-edit/:actualdataId",
            templateUrl: rootUrl + 'app/scorecard/views/actualdata-edit-view.html',
            controller: 'SCDActualDataEditController as vm'
        }).state('scd-actualmapping-list', {
            url: "/scd-actualmapping-list",
            templateUrl: rootUrl + 'app/scorecard/views/actualmapping-list-view.html',
            controller: 'KPIActualMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-actualmapping-edit', {
            url: "/scd-actualmapping-edit/:actualmappingId",
            templateUrl: rootUrl + 'app/scorecard/views/actualmapping-edit-view.html',
            controller: 'KPIActualMappingEditController as vm'
        }).state('scd-targetdata-list', {
            url: "/scd-targetdata-list",
            templateUrl: rootUrl + 'app/scorecard/views/targetdata-list-view.html',
            controller: 'SCDTargetDataListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-targetdata-edit', {
            url: "/scd-targetdata-edit/:targetdataId",
            templateUrl: rootUrl + 'app/scorecard/views/targetdata-edit-view.html',
            controller: 'SCDTargetDataEditController as vm'
        }).state('scd-targetmapping-list', {
            url: "/scd-targetmapping-list",
            templateUrl: rootUrl + 'app/scorecard/views/targetmapping-list-view.html',
            controller: 'KPITargetMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-targetmapping-edit', {
            url: "/scd-targetmapping-edit/:targetmappingId",
            templateUrl: rootUrl + 'app/scorecard/views/targetmapping-edit-view.html',
            controller: 'KPITargetMappingEditController as vm'
        }).state('scd-topperformingkpi-report', {
            url: "/scd-topperformingkpi-report",
            templateUrl: rootUrl + 'app/scorecard/views/topperformingkpi-report-view.html',
            controller: 'TopPerformingKPIController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-worstperformingkpi-report', {
            url: "/scd-worstperformingkpi-report",
            templateUrl: rootUrl + 'app/scorecard/views/scd-worstperformingkpi-report-view.html',
            controller: 'WorstPerformingKPIController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-multiplekpi-report', {
            url: "/scd-multiplekpi-report",
            templateUrl: rootUrl + 'app/scorecard/views/scd-multiplekpi-report-view.html',
            controller: 'MultipleKPIController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-kpiperformance-report', {
            url: "/scd-kpiperformance-report",
            templateUrl: rootUrl + 'app/scorecard/views/scd-kpiperformance-report-view.html',
            controller: 'KPIPerformanceController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-userkpi-report', {
            url: "/scd-userkpi-report",
            templateUrl: rootUrl + 'app/scorecard/views/scd-userkpi-report-view.html',
            controller: 'UserKPIController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-dataentrybykpi-report', {
            url: "/scd-dataentrybykpi-report",
            templateUrl: rootUrl + 'app/scorecard/views/scd-dataentrybykpi-report-view.html',
            controller: 'DataEntryByKPIController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-dataentrybyuser-report', {
            url: "/scd-dataentrybyuser-report",
            templateUrl: rootUrl + 'app/scorecard/views/scd-dataentrybyuser-report-view.html',
            controller: 'DataEntryByUserController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-alldataentry-report', {
            url: "/scd-alldataentry-report",
            templateUrl: rootUrl + 'app/scorecard/views/scd-alldataentry-report-view.html',
            controller: 'AllDataEntryController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('budget-operation-list', {
            url: "/budget-operation-list",
            templateUrl: rootUrl + 'app/budget/views/operation-list-view.html',
            controller: 'OperationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('budget-operation-edit', {
            url: "/budget-operation-edit/:operationId",
            templateUrl: rootUrl + 'app/budget/views/operation-edit-view.html',
            controller: 'OperationEditController as vm'

        }).state('budget-operationreview-edit', {
            url: "/budget-operationreview-edit/:operationId?operationcode?operationreviewId",
            templateUrl: rootUrl + 'app/budget/views/operationreview-edit-view.html',
            controller: 'OperationReviewEditController as vm'

        }).state('budget-budgetinglevel-list', {
            url: "/budget-budgetinglevel-list",
            templateUrl: rootUrl + 'app/budget/views/budgetinglevel-list-view.html',
            controller: 'BudgetingLevelListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('budget-budgetinglevel-edit', {
            url: "/budget-budgetinglevel-edit/:budgetingLevelId",
            templateUrl: rootUrl + 'app/budget/views/budgetinglevel-edit-view.html',
            controller: 'BudgetingLevelEditController as vm'

        }).state('budget-policylevel-list', {
            url: "/budget-policylevel-list",
            templateUrl: rootUrl + 'app/budget/views/policylevel-list-view.html',
            controller: 'PolicyLevelListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('budget-policylevel-edit', {
            url: "/budget-policylevel-edit/:policyLevelId",
            templateUrl: rootUrl + 'app/budget/views/policylevel-edit-view.html',
            controller: 'PolicyLevelEditController as vm'
        }).state('budget-modificationlevel-list', {
            url: "/budget-modificationlevel-list",
            templateUrl: rootUrl + 'app/budget/views/modificationlevel-list-view.html',
            controller: 'ModificationLevelListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('budget-modificationlevel-edit', {
            url: "/budget-modificationlevel-edit/:modificationLevelId",
            templateUrl: rootUrl + 'app/budget/views/modificationlevel-edit-view.html',
            controller: 'ModificationLevelEditController as vm'



        }).state('ifrs9-probabilityweighted-list', {
            url: "/ifrs9-probabilityweighted-list",
            templateUrl: rootUrl + 'app/IFRS9/views/probabilityweighted-list-view.html',
            controller: 'ProbabilityWeightedListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-probabilityweighted-edit', {
            url: "/ifrs9-probabilityweighted-edit/:ProbabilityWeighted_Id",
            templateUrl: rootUrl + 'app/IFRS9/views/probabilityweighted-edit-view.html',
            controller: 'ProbabilityWeightedEditController as vm'



        }).state('ifrs9-historicaldefaultedaccounts-list', {
            url: "/ifrs9-historicaldefaultedaccounts-list",
            templateUrl: rootUrl + 'app/IFRS9/views/historicaldefaultedaccounts-list-view.html',
            controller: 'HistoricalDefaultedAccountsListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-historicaldefaultedaccounts-edit', {
            url: "/ifrs9-historicaldefaultedaccounts-edit/:defaultedAccountId",
            templateUrl: rootUrl + 'app/IFRS9/views/historicaldefaultedaccounts-edit-view.html',
            controller: 'HistoricalDefaultedAccountsEditController as vm'


        }).state('ifrs9-macrovariableestimate-list', {
            url: "/ifrs9-macrovariableestimate-list",
            templateUrl: rootUrl + 'app/IFRS9/views/macrovariableestimate-list-view.html',
            controller: 'MacrovariableEstimateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-macrovariableestimate-edit', {
            url: "/ifrs9-macrovariableestimate-edit/:MacrovariableEstimate_Id",
            templateUrl: rootUrl + 'app/IFRS9/views/macrovariableestimate-edit-view.html',
            controller: 'MacrovariableEstimateEditController as vm'


        }).state('ifrs9-offbalancesheetecl-list', {
            url: "/ifrs9-offbalancesheetecl-list",
            templateUrl: rootUrl + 'app/IFRS9/views/offbalancesheetecl-list-view.html',
            controller: 'OffBalancesheetECLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs9-staffloanscomputationresult-list', {
            url: "/ifrs9-staffloanscomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/staffloanscomputationresult-list-view.html',
            controller: 'StaffLoansComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs9-sectormapping-list', {
            url: "/ifrs9-sectormapping-list",
            templateUrl: rootUrl + 'app/IFRS9/views/sectormapping-list-view.html',
            controller: 'SectorMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-sectormapping-edit', {
            url: "/ifrs9-sectormapping-edit/:SectorMapping_Id",
            templateUrl: rootUrl + 'app/IFRS9/views/sectormapping-edit-view.html',
            controller: 'SectorMappingEditController as vm'
        });

});

App.controller('AppController', function ($scope, $rootScope, $routeParams, $location, $http, viewModelHelper) {

    $scope.userProfile = {};

    $scope.showMessage = function (title, message, type, includeDialog) {
        if (type === 'success') {
            toastr.success(message, title);
        } else if (type === 'error') {
            toastr.error(message, title);
        } else if (type === 'warning') {
            toastr.warning(message, title);
        } else if (type === 'info') {
            toastr.info(message, title);
        }

        if (includeDialog)
            alert(message);
    }

    var loadProfile = function () {
        viewModelHelper.apiGet('api/account/getuserprofile', null,
            function (result) {
                $scope.userProfile = result.data;
            },
            function (result) {
                toastr.error('Fail to load user data', 'Fintrak');
            }, null);
    }

    loadProfile();
});

// Services attached to the commonModule will be available to all other Angular modules.
commonModule.factory('viewModelHelper', function ($http, $q) {
    return Fintrak.viewModelHelper($http, $q);
});

commonModule.factory('validator', function () {
    return valJs.validator();
});

(function (se) {
    var viewModelHelper = function ($http, $q) {

        var self = this;

        self.modelIsValid = true;
        self.modelErrors = [];
        self.isLoading = false;

        self.apiGet = function (uri, data, success, failure, always) {
            self.isLoading = true;
            self.modelIsValid = true;
            $http.get(Fintrak.rootPath + uri, data)
                .then(function (result) {
                    success(result);
                    if (always !== null)
                        always();
                    self.isLoading = false;
                }, function (result) {
                    if (failure === null) {
                        if (result.status !== 400)
                            self.modelErrors = [result.status + ':' + result.statusText + ' - ' + result.data.Message];
                        else
                            self.modelErrors = [result.data.Message];
                        self.modelIsValid = false;
                    }
                    else
                        failure(result);
                    if (always !== null)
                        always();
                    self.isLoading = false;
                });
        }

        self.apiPost = function (uri, data, success, failure, always) {
            self.isLoading = true;
            self.modelIsValid = true;
            $http.post(Fintrak.rootPath + uri, data)
                .then(function (result) {
                    success(result);
                    if (always !== null)
                        always();
                    self.isLoading = false;
                }, function (result) {
                    if (failure === null) {
                        if (result.status !== 400)
                            self.modelErrors = [result.status + ':' + result.statusText + ' - ' + result.data.Message];
                        else
                            self.modelErrors = [result.data.Message];
                        self.modelIsValid = false;
                    }
                    else
                        failure(result);
                    if (always !== null)
                        always();
                    self.isLoading = false;
                });
        }

        return this;
    }
    se.viewModelHelper = viewModelHelper;
}(window.Fintrak));

(function (se) {
    var mustEqual = function (value, other) {
        return value === other;
    }
    se.mustEqual = mustEqual;
}(window.Fintrak));

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
                    if (prop === propertyRule.PropertyName) {
                        var propertyRules = propertyRule.Rules;

                        var propertyRuleProps = Object.keys(propertyRules);
                        for (var k = 0; k < propertyRuleProps.length; k++) {
                            var propertyRuleProp = propertyRuleProps[k];
                            if (propertyRuleProp !== 'custom') {
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
                                if (result !== true) {
                                    errors.push(getMessage(prop, propertyRules.custom, 'Invalid value.'));
                                }
                            }
                        }
                    }
                }
            }

            model['errors'] = errors;
            model['isValid'] = (errors.length === 0);
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
                    return !(value.toString().trim() === '');
                },
                message: 'Value is required 2.'
            };
            rules['notZero'] = {
                validator: function (value, params) {
                    return !(value === 0);
                },
                message: 'Value is must be greater than zero.'
            };
            rules['mostBePercentage'] = {
                validator: function (value, params) {
                    return !(value < 0);
                },
                message: 'Value must be greater than or equal zero.'
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
                    return !(value.toString().trim().length < params);
                },
                message: 'Value does not meet minimum length.'
            };
            rules['pattern'] = {
                validator: function (value, params) {
                    var regExp = new RegExp(params);
                    return !(regExp.exec(value.toString().trim()) === null);
                },
                message: 'Value must match regular expression.'
            };
        }

        function isDate(sDate) {
            if (sDate === null)
                return false;

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

App.controller('NoneController', function ($scope, $routeParams) {

});

App.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);

App.directive('kpiFormular', ['$rootScope', function ($rootScope) {
    return {
        link: function (scope, element, attrs) {
            $rootScope.$on('updateKPIFormular', function (e, val) {
                var domElement = element[0];

                if (document.selection) {
                    domElement.focus();
                    var sel = document.selection.createRange();
                    sel.text = val;
                    domElement.focus();
                } else if (domElement.selectionStart || domElement.selectionStart === 0) {
                    var startPos = domElement.selectionStart;
                    var endPos = domElement.selectionEnd;
                    var scrollTop = domElement.scrollTop;
                    domElement.value = domElement.value.substring(0, startPos) + val + domElement.value.substring(endPos, domElement.value.length);
                    domElement.focus();
                    domElement.selectionStart = startPos + val.length;
                    domElement.selectionEnd = startPos + val.length;
                    domElement.scrollTop = scrollTop;
                } else {
                    domElement.value += val;
                    domElement.focus();
                }

            });
        }
    }
}])

App.directive('scoreFormular', ['$rootScope', function ($rootScope) {
    return {
        link: function (scope, element, attrs) {
            $rootScope.$on('updateScoreFormular', function (e, val) {
                var domElement = element[0];

                if (document.selection) {
                    domElement.focus();
                    var sel = document.selection.createRange();
                    sel.text = val;
                    domElement.focus();
                } else if (domElement.selectionStart || domElement.selectionStart === 0) {
                    var startPos = domElement.selectionStart;
                    var endPos = domElement.selectionEnd;
                    var scrollTop = domElement.scrollTop;
                    domElement.value = domElement.value.substring(0, startPos) + val + domElement.value.substring(endPos, domElement.value.length);
                    domElement.focus();
                    domElement.selectionStart = startPos + val.length;
                    domElement.selectionEnd = startPos + val.length;
                    domElement.scrollTop = scrollTop;
                } else {
                    domElement.value += val;
                    domElement.focus();
                }

            });
        }
    }
}])


App.directive('format', ['$filter', function ($filter) {
    return {
        require: '?ngModel',
        link: function (scope, elem, attrs, ctrl) {
            if (!ctrl) return;

            ctrl.$formatters.unshift(function (a) {
                return $filter(attrs.format)(ctrl.$modelValue)
            });

            elem.bind('blur', function (event) {
                var plainNumber = elem.val().replace(/[^\d|\-+|\.+]/g, '');
                elem.val($filter(attrs.format)(plainNumber));
            });
        }
    };
}]);

App.factory('httpErrorResponseInterceptor', ['$q', '$location',
    function ($q, $location) {
        return {
            response: function (responseData) {
                return responseData;
            },
            responseError: function error(response) {
                switch (response.status) {
                    case 401:
                        $location.path('/login');
                        break;
                    case 404:
                        $location.path('/404');
                        break;
                    default:
                        alert(response.data);
                        //$location.path('/error');
                }

                return $q.reject(response);
            }
        };
    }
]);

//App.factory("akFileUploaderService", ["$q", "$http",
//               function ($q, $http) {

//                   var getModelAsFormData = function (data) {
//                       var dataAsFormData = new FormData();
//                       angular.forEach(data, function (value, key) {
//                           dataAsFormData.append(key, value);
//                       });
//                       return dataAsFormData;
//                   };

//                   var saveModel = function (data, url) {
//                       var deferred = $q.defer();
//                       $http({
//                           url: url,
//                           method: "POST",
//                           data: getModelAsFormData(data),
//                           transformRequest: angular.identity,
//                           headers: { 'Content-Type': undefined }
//                       }).success(function (result) {
//                           deferred.resolve(result);
//                       }).error(function (result, status) {
//                           deferred.reject(status);
//                       });
//                       return deferred.promise;
//                   };

//                   return {
//                       saveModel: saveModel
//                   }
//               }]);

//App.directive("akFileModel", ["$parse",
//                function ($parse) {
//                    return {
//                        restrict: "A",
//                        link: function (scope, element, attrs) {
//                            var model = $parse(attrs.akFileModel);
//                            var modelSetter = model.assign;
//                            element.bind("change", function () {
//                                scope.$apply(function () {
//                                    modelSetter(scope, element[0].files[0]);
//                                });
//                            });
//                        }
//                    };
//                }]);

App.factory('Excel', function ($window) {
    var uri = 'data:application/vnd.ms-excel;base64,',
        template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
        base64 = function (s) { return $window.btoa(unescape(encodeURIComponent(s))); },
        format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    return {
        tableToExcel: function (tableId, worksheetName) {
            var table = $(tableId),
                ctx = { worksheet: worksheetName, table: table.html() },
                href = uri + base64(format(template, ctx));
            return href;
        }
    };
})

