using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Presentation.WebClient.Models;


namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/plincomereportadjustment")]
    [UsesDisposableService]
    public class PLIncomeReportAdjustmentApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PLIncomeReportAdjustmentApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        [HttpPost]
        [Route("updateplIncomeReportAdjustment")]
        public HttpResponseMessage UpdatePLIncomeReportAdjustment(HttpRequestMessage request, [FromBody]PLIncomeReportAdjustment plIncomeReportAdjustmentModel)
        {
            return GetHttpResponse(request, () =>
            {
                var plIncomeReportAdjustment = _MPRPLService.UpdatePLIncomeReportAdjustment(plIncomeReportAdjustmentModel);

                return request.CreateResponse<PLIncomeReportAdjustment>(HttpStatusCode.OK, plIncomeReportAdjustment);
            });
        }

        [HttpPost]
        [Route("deleteplIncomeReportAdjustment")]
        public HttpResponseMessage DeletePLIncomeReportAdjustment(HttpRequestMessage request, [FromBody]int plIncomeReportAdjustmentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                PLIncomeReportAdjustment plIncomeReportAdjustment = _MPRPLService.GetPLIncomeReportAdjustment(plIncomeReportAdjustmentId);

                if (plIncomeReportAdjustment != null)
                {
                    _MPRPLService.DeletePLIncomeReportAdjustment(plIncomeReportAdjustmentId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No plIncomeReportAdjustment found under that ID.");

                return response;
            });
        }



        [HttpGet]
        [Route("getplincomereportadjustment/{plIncomeReportAdjustmentId}")]
        public HttpResponseMessage GetPLIncomeReportAdjustment(HttpRequestMessage request, int plIncomeReportAdjustmentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                PLIncomeReportAdjustment plIncomeReportAdjustment = _MPRPLService.GetPLIncomeReportAdjustment(plIncomeReportAdjustmentId);

                // notice no need to create a seperate model object since PLIncomeReportAdjustment entity will do just fine
                response = request.CreateResponse<PLIncomeReportAdjustment>(HttpStatusCode.OK, plIncomeReportAdjustment);

                return response;
            });
        }

        [HttpGet]
        [Route("getplincomereportadjustments/{searchType}/{searchValue}/{number}")]
        public HttpResponseMessage GetBalanceSheets(HttpRequestMessage request, string searchType, string searchValue, int number)
        {
            return GetHttpResponse(request, () =>
            {
                PLIncomeReportAdjustment[] plIncomeReportAdjustment = _MPRPLService.GetPLIncomeReportAdjustments(searchType, searchValue, number);

                return request.CreateResponse<PLIncomeReportAdjustment[]>(HttpStatusCode.OK, plIncomeReportAdjustment);
            });
        }


        [HttpGet]
        [Route("availableplIncomeReportAdjustment")]
        public HttpResponseMessage GetAvailablePLIncomeReportAdjustment(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PLIncomeReportAdjustment[] plIncomeReportAdjustment = _MPRPLService.GetAllPLIncomeReportAdjustments();

                return request.CreateResponse<PLIncomeReportAdjustment[]>(HttpStatusCode.OK, plIncomeReportAdjustment);
            });
        }


        [HttpGet]
        [Route("getCodebyUser")]
        public HttpResponseMessage GetCodebyUsers(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                var userName = User.Identity.Name;
                PLIncomeReportAdjustment[] plIncomeReportAdjustment = _MPRPLService.GetCodebyUser(userName).ToArray();

                List<BalanceSheetAdjDateModel> code = new List<BalanceSheetAdjDateModel>();

                List<string> codes = null;

                codes = plIncomeReportAdjustment.Select(c => c.Code).Distinct().ToList();

                foreach (var c in codes)
                    code.Add(new BalanceSheetAdjDateModel()
                    {
                        Code = c
                    });
                return request.CreateResponse<BalanceSheetAdjDateModel[]>(HttpStatusCode.OK, code.ToArray());
            });
        }


        [HttpGet]
        [Route("getplIncomeReportAdjustment/{code}")]
        public HttpResponseMessage GetBalanceSheetAdjustmentByCode(HttpRequestMessage request, string code)
        {
            return GetHttpResponse(request, () =>
            {
                var userName = User.Identity.Name;
                PLIncomeReportAdjustment[] plIncomeReportAdjustment = _MPRPLService.GetBalanceSheetAdjustmentByCode(code, userName);

                return request.CreateResponse<PLIncomeReportAdjustment[]>(HttpStatusCode.OK, plIncomeReportAdjustment);
            });
        }


        [HttpPost]
        [Route("deleteplIncomeReportAdjustmentbycode")]
        public HttpResponseMessage DeleteMPRBalanceSheetAdjustment(HttpRequestMessage request, [FromBody] BalanceSheetAdjustment param)
        {
            return GetHttpResponse(request, () =>
            {
                var userName = User.Identity.Name;
                HttpResponseMessage response = null;

                _MPRPLService.DeleteMPRBalanceSheetAdjustment(param.code, userName);

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

    }
}
