/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("GLMappingListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        GLMappingListController]);

    function GLMappingListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'glmapping-list-view';
        vm.viewName = 'GL Mappings';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.defaultcount = 200;
        vm.glMappings = [];

        vm.init = false;
        vm.showInstruction = true;
        vm.instruction = '';
        vm.pageFlag = '';
        var exportTable;
        var tabID;
        vm.searchParam = '';
        vm.instruction = 'Only top ' + vm.defaultcount + ' records loaded. Use the main search fuctionality to find a specific record by GL Code';

        var initialize = function () {

            if (vm.init === false) {
                vm.loadRegistriesMapping();
            }
        };

        var InitialView = function (tableID) {
            InitialGrid(tableID);
        };

        var InitialGrid = function (tableID) {
            tabID = '#' + tableID;
            setTimeout(function () {
                    // data export
                    if ($(tabID).length > 0) {
                        exportTable = $(tabID).DataTable({
                            "lengthMenu": [[50, 100, -1], [50, 100, "All"]],
                            sDom: "T<'clearfix'>" +
                                "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                                "t" +
                                "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                            "tableTools": {
                                "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                            }
                        });
                    }
                },
                50);
        };

        vm.loadOthMapping = function () {
            vm.viewModelHelper.apiGet('api/glMapping/availableglMappings/' + 3 + '/' + vm.defaultcount,
                null,
                function (result) {
                    vm.glMappings = result.data;
                    InitialView('othGlMappingTable');

                    if (vm.init === true) {
                        exportTable.destroy();
                    } else vm.init = true;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

        vm.loadMgtMapping = function () {
            vm.viewModelHelper.apiGet('api/glMapping/availableglMappings/' + 2 + '/' + vm.defaultcount,
                null,
                function (result) {
                    vm.glMappings = result.data;
                    InitialView('mgtGlMappingTable');
                    //vm.pageFlag = 2;

                    if (vm.init === true) {
                        exportTable.destroy();
                    } else vm.init = true;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

        vm.loadRegistriesMapping = function () {
            vm.viewModelHelper.apiGet('api/glMapping/availableglMappings/' + 1 + '/' + vm.defaultcount,
                null,
                function (result) {
                    vm.glMappings = result.data;
                    InitialView('registryGlMappingTable');

                    if (vm.init === true) {
                        exportTable.destroy();
                    } else vm.init = true;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        };

        vm.flipFlagToDescription = function (flag) {

            if (flag === 1) {
                return 'Conventional';
            } else if (flag === 2) {
                return 'Interim';
            } else if (flag === 3) {
                return 'NIB';
            }
        };

        vm.loadMappingsBySearch = function (flag, tableID) {

            vm.viewModelHelper.apiGet('api/glMapping/availableglmappingsbysearch/' + flag + '/' + vm.searchParam,
                null,
                function (result) {
                    vm.glMappings = result.data;
                    InitialView(tableID);

                    if (vm.init === true) {
                        exportTable.destroy();
                    } else vm.init = true;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                },
                null);
        }

        vm.exportAllData_1 = function () {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                vm.loading = false;
            }
            xhr.open('GET', 'api/glMapping/availableglMappings/1/0' , true);
            xhr.responseType = 'arraybuffer';
            xhr.onload = function () {
                if (this.status === 200) {
                    var filename = "";
                    var disposition = xhr.getResponseHeader('Content-Disposition');
                    if (disposition && disposition.indexOf('attachment') !== -1) {
                        var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                        var matches = filenameRegex.exec(disposition);
                        if (matches != null && matches[1]) filename = matches[1].replace(/['"]/g, '');
                    }
                    var type = xhr.getResponseHeader('Content-Type');

                    var blob = typeof File === 'function'
                        ? new File([this.response], filename, { type: type })
                        : new Blob([this.response], { type: type });
                    if (typeof window.navigator.msSaveBlob !== 'undefined') {
                        // IE workaround for "HTML7007: One or more blob URLs were revoked by closing the blob for which they were created. These URLs will no longer resolve as the data backing the URL has been freed."
                        window.navigator.msSaveBlob(blob, filename);
                    } else {
                        var URL = window.URL || window.webkitURL;
                        var downloadUrl = URL.createObjectURL(blob);

                        if (filename) {
                            // use HTML5 a[download] attribute to specify filename
                            var a = document.createElement("a");
                            // safari doesn't support this yet
                            if (typeof a.download === 'undefined') {
                                window.location = downloadUrl;
                            } else {
                                a.href = downloadUrl;
                                a.download = filename;
                                document.body.appendChild(a);
                                a.click();
                            }
                        } else {
                            window.location = downloadUrl;
                        }

                        setTimeout(function () { URL.revokeObjectURL(downloadUrl); }, 100); // cleanup
                    }
                }
            };
            xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
            vm.loading = true;
            xhr.send()
        }

        vm.exportAllData_2 = function () {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                vm.loading = false;
            }
            xhr.open('GET', 'api/glMapping/availableglMappings/2/0', true);
            xhr.responseType = 'arraybuffer';
            xhr.onload = function () {
                if (this.status === 200) {
                    var filename = "";
                    var disposition = xhr.getResponseHeader('Content-Disposition');
                    if (disposition && disposition.indexOf('attachment') !== -1) {
                        var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                        var matches = filenameRegex.exec(disposition);
                        if (matches != null && matches[1]) filename = matches[1].replace(/['"]/g, '');
                    }
                    var type = xhr.getResponseHeader('Content-Type');

                    var blob = typeof File === 'function'
                        ? new File([this.response], filename, { type: type })
                        : new Blob([this.response], { type: type });
                    if (typeof window.navigator.msSaveBlob !== 'undefined') {
                        // IE workaround for "HTML7007: One or more blob URLs were revoked by closing the blob for which they were created. These URLs will no longer resolve as the data backing the URL has been freed."
                        window.navigator.msSaveBlob(blob, filename);
                    } else {
                        var URL = window.URL || window.webkitURL;
                        var downloadUrl = URL.createObjectURL(blob);

                        if (filename) {
                            // use HTML5 a[download] attribute to specify filename
                            var a = document.createElement("a");
                            // safari doesn't support this yet
                            if (typeof a.download === 'undefined') {
                                window.location = downloadUrl;
                            } else {
                                a.href = downloadUrl;
                                a.download = filename;
                                document.body.appendChild(a);
                                a.click();
                            }
                        } else {
                            window.location = downloadUrl;
                        }

                        setTimeout(function () { URL.revokeObjectURL(downloadUrl); }, 100); // cleanup
                    }
                }
            };
            xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
            vm.loading = true;
            xhr.send()
        }


        vm.exportAllData_3 = function () {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                vm.loading = false;
            }
            xhr.open('GET', 'api/glMapping/availableglMappings/3/0', true);
            xhr.responseType = 'arraybuffer';
            xhr.onload = function () {
                if (this.status === 200) {
                    var filename = "";
                    var disposition = xhr.getResponseHeader('Content-Disposition');
                    if (disposition && disposition.indexOf('attachment') !== -1) {
                        var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                        var matches = filenameRegex.exec(disposition);
                        if (matches != null && matches[1]) filename = matches[1].replace(/['"]/g, '');
                    }
                    var type = xhr.getResponseHeader('Content-Type');

                    var blob = typeof File === 'function'
                        ? new File([this.response], filename, { type: type })
                        : new Blob([this.response], { type: type });
                    if (typeof window.navigator.msSaveBlob !== 'undefined') {
                        // IE workaround for "HTML7007: One or more blob URLs were revoked by closing the blob for which they were created. These URLs will no longer resolve as the data backing the URL has been freed."
                        window.navigator.msSaveBlob(blob, filename);
                    } else {
                        var URL = window.URL || window.webkitURL;
                        var downloadUrl = URL.createObjectURL(blob);

                        if (filename) {
                            // use HTML5 a[download] attribute to specify filename
                            var a = document.createElement("a");
                            // safari doesn't support this yet
                            if (typeof a.download === 'undefined') {
                                window.location = downloadUrl;
                            } else {
                                a.href = downloadUrl;
                                a.download = filename;
                                document.body.appendChild(a);
                                a.click();
                            }
                        } else {
                            window.location = downloadUrl;
                        }

                        setTimeout(function () { URL.revokeObjectURL(downloadUrl); }, 100); // cleanup
                    }
                }
            };
            xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
            vm.loading = true;
            xhr.send()
        }

        initialize(); 
    }
}());
