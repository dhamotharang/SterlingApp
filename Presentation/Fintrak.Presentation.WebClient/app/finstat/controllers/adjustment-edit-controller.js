/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("AdjustmentEditController",
        ['$rootScope',  '$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$modal', 'glService',
            AdjustmentEditController]);

    function AdjustmentEditController($rootScope,  $scope, $window, $state, $stateParams, viewModelHelper, validator, $modal, glService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'adjustment-edit-view';
        vm.viewName = 'GL Adjustment';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.glAdjustment = {};
        vm.glAdjustments = [];
        vm.currencyRate = {};
        vm.lcybal = 0;
        vm.trialBalance = 0;
        vm.adjustment = 0;
        vm.finalBalance = 0;
        vm.totalAdjustment = 0;
        var totalAdjustmentTemp = 0;
        vm.currencyRate = 1;
       // alert($filter.vm.lcybal);
        vm.postStatus = 'tomato';
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.adjustmentTypes = [
            { Id: 1, Name: 'GAAP' },
            { Id: 2, Name: 'IFRS' }
        ];

        vm.indicators = [
            { Id: 1, Name: 'Debit' },
            { Id: 2, Name: 'Credit' }
        ];

        vm.reportTypes = [
            { Id: 1, Name: 'IAS39' },
            { Id: 2, Name: 'IFRS9' }
        ];

        vm.firstCurrency = '';
        vm.firstbranch = '';
        vm.currencies = [];
        vm.companies = [];
        vm.account = [];
        vm.branches = [];

        vm.adjustmentType = 1;
        vm.reportType = 2;

        if ($stateParams.adjustmentType) {
            vm.adjustmentType = $stateParams.adjustmentType;
            //vm.reportType = $stateParams.reportType;
        };


        var gladjustmentRules = [];

        var setupRules = function () {

            gladjustmentRules.push(new validator.PropertyRule("GLCode", {
                required: { message: "GL is required" }
            }));

            gladjustmentRules.push(new validator.PropertyRule("Indicator", {
                notZero: { message: "Indicator is required" }
            }));

            gladjustmentRules.push(new validator.PropertyRule("AdjustmentType", {
                notZero: { message: "Adjustment Type is required" }
            }));

            gladjustmentRules.push(new validator.PropertyRule("Currency", {
                required: { message: "Currency is required" }
            }));

            gladjustmentRules.push(new validator.PropertyRule("Narration", {
                required: { message: "Narration is required" }
            }));


        };

        var initialize = function () {
            if (vm.init === false) {

                $rootScope.$on('selected-gl-changed', selectedGLChanged);

                //load lookups
                intializeLookUp();

                if ($stateParams.gladjustmentId !== 0 && $stateParams.gladjustmentId !== '0') {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/gladjustment/getglAdjustment/' + $stateParams.gladjustmentId, null,

                        function (result) {
                            vm.glAdjustment = result.data;

                            loadTrialBalance(vm.glAdjustment.GLCode);
                            vm.init === true;
                            loadNonPosted();
                            loadUnMappedGL();

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else if ($stateParams.gladjustmentId === 0 && $stateParams.gladjustmentId === '0') {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/gladjustment/getgladjustments/' + $stateParams.gladjustmentId + '/' + $stateParams.adjustmentType + '/' + $stateParams.reportType, null,
                        function (result) {
                            vm.glAdjustment = result.data;
                            loadTrialBalance(vm.glAdjustment.GLCode);
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.glAdjustment = { GLCode: '', Indicator: 1, AdjustmentType: parseInt($stateParams.adjustmentType), ReportType: parseInt($stateParams.reportType), CompanyCode: vm.firstbranch, Currency: vm.firstCurrency, Posted: false, Active: true };
                loadNonPosted();
                loadUnMappedGL();
            }
        };

        var intializeLookUp = function () {
            getBranches();
            //getAccounts();
            getCurrencies();
        };

        var initialView = function () {
            initialGrid();
        };

        var initialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#glAdjustmentTable').length > 0) {
                    var exportTable = $('#glAdjustmentTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                        "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                        "t" +
                        "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            }, 50);
        };

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.glAdjustment, gladjustmentRules);
            vm.viewModelHelper.modelIsValid = vm.glAdjustment.isValid;
            vm.viewModelHelper.modelErrors = vm.glAdjustment.errors;
            if (vm.viewModelHelper.modelIsValid) {
                var addt = vm.glAdjustment;
                addt.ExchangeRate = vm.currencyRate;
                addt.Amount = vm.lcybal;
                vm.viewModelHelper.apiPost('api/gladjustment/updategladjustment', vm.glAdjustment,
                    function (result) {

                        loadNonPosted();
                        vm.glAdjustment = { GLCode: '', Indicator: 1, AdjustmentType: parseInt($stateParams.adjustmentType), ReportType: parseInt($stateParams.reportType), Posted: false, Active: true };

                        //$state.go('finstat-adjustment-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.glAdjustment.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        };

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/gladjustment/deletegladjustment', vm.glAdjustment.GLAdjustmentId,
                    function (result) {
                        vm.glAdjustment = {};
                        toastr.success('Selected item deleted.', 'Fintrak');
                        loadNonPosted();
                        //$state.go('finstat-adjustment-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('finstat-adjustment-list');
        };

        vm.post = function () {
            if (Math.abs(vm.totalAdjustment < 10) ) {
                vm.viewModelHelper.apiPost('api/gladjustment/postadjustment/' + $stateParams.adjustmentType + '/' + $stateParams.reportType, null,
                    function (result) {
                        toastr.success('Adjustments posting successful', 'Fintrak');
                        $state.go('finstat-adjustment-list');
                    },
                    function (result) {
                        toastr.error('Fail to post adjustments.', 'Fintrak');
                    }, null);
            } else {
                toastr.error('Adjustment balanacing figure is not zero.', 'Fintrak');
            }
        };

        vm.onGLChanged = function (glCode) {
            loadTrialBalance(glCode);
            vm.glAdjustment.CompanyCode = glCode.substr(0, 3);
        };

        vm.onCCYAmountChanged = function (amount) {
            if (vm.glAdjustment.Indicator === 1) {
                vm.adjustment = (-1 * amount * vm.currencyRate);
                vm.lcybal = (amount * vm.currencyRate);
            } else {
                vm.adjustment = (amount * vm.currencyRate);
                vm.lcybal = vm.adjustment;
                vm.finalBalance = +vm.trialBalance + +vm.adjustment;
            }
        };

        vm.onAmountChanged = function (amount) {
            if (vm.glAdjustment.Indicator === 1) {
                vm.adjustment = (-1 * amount * vm.currencyRate);
                vm.lcybal = vm.adjustment;
            }
            else {
                vm.adjustment = (amount * vm.currencyRate);               
                vm.lcybal = vm.adjustment;
                vm.finalBalance = +vm.trialBalance + +vm.adjustment;
            };
        };

        vm.openGLDialog = function () {
            var options = {
                templateUrl: 'app/finstat/dialogs/gl-dialog-view.html',
                controller: 'GLDialogController',
                scope: $scope
            };

            $modal.open(options);

        };

        var selectedGLChanged = function () {

            vm.glAdjustment.GLCode = glService.getSelectedGL().GLCode;
            loadTrialBalance(vm.glAdjustment.GLCode);
        };

        var loadNonPosted = function () {
            vm.viewModelHelper.apiGet('api/gladjustment/getgladjustmentbystatus/' + $stateParams.adjustmentType + '/' + $stateParams.reportType + '/false', null,
                function (result) {
                    vm.glAdjustments = result.data;
                    vm.totalAdjustment = 0;

                    var branches = [];

                    angular.forEach(vm.glAdjustments, function (adjustment) {
                        var found = false;
                        angular.forEach(vm.branches, function (branch) {
                            if (adjustment.CompanyCode === branch)
                                found = true;
                        });

                        if (found !== true)
                            branches.push(adjustment.CompanyCode);
                    });

                    angular.forEach(branches, function (branch) {


                        //if (vm.totalAdjustment === 0)
                        if (totalAdjustmentTemp === 0) {
                            angular.forEach(vm.glAdjustments, function (adjustment) {
                                if (adjustment.CompanyCode === branch) {
                                    if (adjustment.Indicator === 1)
                                        totalAdjustmentTemp = +totalAdjustmentTemp + (-1 * adjustment.Amount);
                                    else
                                        totalAdjustmentTemp = +totalAdjustmentTemp + +adjustment.Amount;
                                }
                            });
                        }
                    });
                    vm.totalAdjustment = Math.round(totalAdjustmentTemp);
                    if (Math.abs(vm.totalAdjustment < 10 ))

                        vm.postStatus = 'greenyellow';
                    else
                        vm.postStatus = 'tomato';

                    if (vm.initGrid === false) {
                        initialView();
                        vm.initGrid = true;
                    }

                    // toastr.info('Non-posted list refreshed successfully.', 'Fintrak');
                },
                function (result) {
                    toastr.error('Unable to refresh non-posted list.', 'Fintrak');
                }, null);
        };

        var loadTrialBalance = function (glCode) {

            var url = 'api/trialBalance/getgaptrialbalancebygl/' + glCode + '/';
            if (vm.adjustmentType === 2)
                url = 'api/trialBalance/gettrialbalancebygl/' + glCode + '/';
            vm.viewModelHelper.apiGet(url, null,
                function (result) {

                    vm.trialBalance = 0;
                    angular.forEach(result.data, function (tb) {
                        vm.trialBalance = +vm.trialBalance + + tb.LCY_Balance;
                    });

                    //   toastr.success('GL trial balance figure loaded.', 'Fintrak');
                },
                function (result) {
                    toastr.error(result.data.message, 'Fintrak');
                }, null);
        };

        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                function (result) {
                    vm.companies = result.data;
                    vm.init === true;

                },
                function (result) {
                    toastr.error('Unable to load companies', 'Fintrak');
                }, null);
        };


        var loadUnMappedGL = function () {
            vm.viewModelHelper.apiGet('api/gladjustment/unmappedglcodes', null,
                function (result) {
                    vm.unmappedgl = result.data;
                    vm.init === true;

                },
                function (result) {
                    toastr.error('Unable to load Un-Mapped GLs', 'Fintrak');
                }, null);
        };

        var getBranches = function () {
            vm.viewModelHelper.apiGet('api/branch/availablebranches', null,
                function (result) {
                    vm.branches = result.data;

                    angular.forEach(vm.branches, function (company) {
                        vm.firstbranch = company.Code;
                        vm.glAdjustment.CompanyCode = company.Code;
                    });

                },
                function (result) {
                    toastr.error('Unable to load branches', 'Fintrak');
                }, null);
        };

        var getCurrencies = function () {
            vm.viewModelHelper.apiGet('api/currency/availablecurrencies', null,
                function (result) {
                    vm.currencies = result.data;
                    vm.init === true;

                    angular.forEach(vm.currencies, function (currency) {
                        vm.firstCurrency = currency.Symbol;
                        vm.glAdjustment.Currency = currency.Symbol;
                    });


                },
                function (result) {
                    toastr.error('Unable to load currencies', 'Fintrak');
                }, null);
        };

        vm.onCurrencyChanged = function () {
            vm.viewModelHelper.apiGet('api/currencyRate/getcurrencyRatebyDate/' + vm.glAdjustment.Currency, null,

                function (result) {
                    vm.currencyRate = result.data[0].Rate;  
                   // vm.adjustment = 0;
                    vm.lcybal = (Math.abs(vm.glAdjustment.CCY_Amount) * vm.currencyRate);
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        vm.IndicatorChanged = function () {
            vm.cur = 'NGN';
            vm.viewModelHelper.apiGet('api/currencyRate/getcurrencyRatebyDate/' + vm.cur, null,
                function (result) {
                    vm.currencyRate = result.data[0].Rate;                   
                        vm.adjustment = 0;
                        vm.lcybal = 0;
                    
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        var getAccounts = function () {
            vm.viewModelHelper.apiGet('api/glmapping/availableglMappings', null,
                function (result) {
                    vm.accounts = result.data;
                },
                function (result) {
                    toastr.error('Unable to load GLs', 'Fintrak');
                }, null);
        };


        vm.editNonPosted = function (GLAdjustmentId) {
            vm.viewModelHelper.apiGet('api/gladjustment/getglAdjustment/' + GLAdjustmentId, null,

                function (result) {
                    vm.glAdjustment = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };


        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/gladjustment/deletegladjustment', vm.glAdjustment.GLAdjustmentId,
                    function (result) {
                        vm.glAdjustment = {};
                        toastr.success('Selected item deleted.', 'Fintrak');
                        loadNonPosted();
                        //$state.go('finstat-adjustment-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }

        };
        setupRules();
        initialize();
    }
}());
