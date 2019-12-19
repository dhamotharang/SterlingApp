/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IFRS9DashboardController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IFRS9DashboardController]);

    function IFRS9DashboardController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'CREDIT PRINTOUT';
        vm.view = 'dashboard-list-view';
        vm.viewName = 'Dashboard';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.myDataSource = [];
        $scope.portfolioSource = {};
        $scope.sectorialSource = {};
        $scope.bucketSource = {};
       // var bucketSource = {};
        vm.dts = [];
        vm.ifrs9dashboards = [];
        vm.portfolioexposure = {};
        vm.portfolioexposurechart=[];
        vm.sectorialexposure = [];
        vm.bucketexposure = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                intializeLookUp();

                InitialView();
            }
        }

        var InitialView = function () {

            InitialGrid();

        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#portfolioexposureTable2').length > 0) {
                    var exportTable = $('#portfolioexposureTable2').DataTable({
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
        }
        var intializeLookUp = function () {
            getPortfolioExposure();
            getSectorialExposure();
            getBucketExposure();
            //portfolioexposurechart();
           // getdataSource2();
           // getdataSource3();
        }
       
        var getPortfolioExposure = function () {

            vm.viewModelHelper.apiGet('api/ifrs9dashboard/portfolioexposure', null,
                function (result) {
                    vm.portfolioexposure = result.data.PortfolioExposure;
                    vm.portfolioexposurechart = result.data.PortfolioExposureChart;
                    portfolioexposurechart();
                },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
        }

        var portfolioexposurechart = function () {
            var jdata;
            jdata = JSON.parse(vm.portfolioexposurechart); //
            $scope.portfolioSource = {
                chart: {
                    caption: "Portfolio Exposure",
                    subCaption: "Risk Distribution by Portfolio",
                    showPercentageValues: "1",
                    numberPrefix: "N",
                    theme: "ocean","caption": "Portfolio Exposure",
                    "subCaption": "Risk Category",
                    "paletteColors": "#0075c2,#1aaf5d,#f2c500,#f45b00,#8e0000",
                    "bgColor": "#ffffff",
                    "showBorder": "0",
                    "use3DLighting": "0",
                    "showShadow": "0",
                    "enableSmartLabels": "0",
                    "startingAngle": "0",
                    "showPercentValues": "1",
                    "showPercentInTooltip": "0",
                    "decimals": "1",
                    "captionFontSize": "14",
                    "subcaptionFontSize": "14",
                    "subcaptionFontBold": "0",
                    "toolTipColor": "#ffffff",
                    "toolTipBorderThickness": "0",
                    "toolTipBgColor": "#000000",
                    "toolTipBgAlpha": "80",
                    "toolTipBorderRadius": "2",
                    "toolTipPadding": "5",
                    "showHoverEffect": "1",
                    "showLegend": "1",
                    "legendBgColor": "#ffffff",
                    "legendBorderAlpha": "0",
                    "legendShadow": "0",
                    "legendItemFontSize": "10",
                    "legendItemFontColor": "#666666",
                    "useDataPlotColorForLabels": "1"
                },
                data: jdata
            };
        }

        var getSectorialExposure = function () {
            vm.viewModelHelper.apiGet('api/ifrs9dashboard/sectorialexposure', null,
                function (result) {
                    vm.sectorialexposure = result.data.SectorialExposure;
                    vm.sectorialexposurechart = result.data.SectorialExposureChart;
                    sectorialexposurechart();
                },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
        }

        var getBucketExposure = function () {
            vm.viewModelHelper.apiGet('api/ifrs9dashboard/bucketexposure', null,
                function (result) {
                    vm.bucketexposure = result.data.BucketExposure;
                    vm.bucketexposurechart = result.data.BucketExposureChart;
                    bucketexposurechart();
                },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
        }

        var sectorialexposurechart = function () {
            var jdata2;
            jdata2 = JSON.parse(vm.sectorialexposurechart); //
            $scope.sectorialSource = {
                chart: {
                    caption: "Sectorial Exposure",
                    subCaption: "Risk Distribution by Sector",
                    showPercentageValues: true,
                    numberPrefix: "N",
                    theme: "ocean",
                    "autoscale": "0",
                    "scaleimages": "1",
                    "origw": "400",
                    "origh": "300",
                    "labelDisplay": "rotate"
                },
                data: jdata2
            };
        }

        var bucketexposurechart = function () {
            var jdata3;
            jdata3 = JSON.parse(vm.bucketexposurechart); //
            $scope.bucketSource = {
                chart: {
                                caption: "Risk Distribution by Stage",                    
                                numberPrefix: "N",
                              //  theme: "ocean",
                                startingangle: "310",
                                decimals: "0",
                                "defaultcenterlabel": "Risk Distribution by Stage",
                                "centerlabel": "Risk Distribution by Stage $label: $value",
                                "theme": "fint",
                                showPercentageValues:true
                            },
                data: jdata3
            };
        }
        
        //var getdataSource3 = function () {

        //              $scope.bucketSource = {
        //        chart: {
        //            caption: "Risk Distribution by Stage",                    
        //            numberPrefix: "N",
        //          //  theme: "ocean",
        //            startingangle: "310",
        //            decimals: "0",
        //            "defaultcenterlabel": "Risk Distribution by Stage",
        //            "centerlabel": "Risk Distribution by Stage $label: $value",
        //            "theme": "fint",
        //            showPercentageValues:true
        //        },
        //        data: [{
        //            label: "PERFORMING",
        //            value: "16787817"
        //        },
        //        {
        //            label: "UNDER PERFORMING",
        //            value: "26959849.5"
        //        },
        //        {
        //            label: "NON PERFORMING",
        //            value: "23633486",
                   
        //        }]
        //    };

        //}

        initialize();
    }
}());
