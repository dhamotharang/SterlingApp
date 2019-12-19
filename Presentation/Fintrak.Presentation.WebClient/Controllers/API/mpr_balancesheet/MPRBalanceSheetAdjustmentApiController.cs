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
    [RoutePrefix("api/mprbalancesheetadjustment")]
    [UsesDisposableService]
    public class MPRBalanceSheetAdjustmentApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MPRBalanceSheetAdjustmentApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

       
        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }
        [HttpPost]
        [Route("updatemprbalanceSheetAdjustment")]
        public HttpResponseMessage UpdateMPRBalanceSheetAdjustment(HttpRequestMessage request, [FromBody]MPRBalanceSheetAdjustment mprbalanceSheetAdjustmentModel)
        {
            return GetHttpResponse(request, () =>
            {
                var mprbalanceSheetAdjustment = _MPRBSService.UpdateBalanceSheetAdjustment(mprbalanceSheetAdjustmentModel);

                return request.CreateResponse<MPRBalanceSheetAdjustment>(HttpStatusCode.OK, mprbalanceSheetAdjustment);
            });
        }



        [HttpGet]
        [Route("availablemprbalancesheetadjustment")]
        public HttpResponseMessage GetAvailableMPRBalanceSheetAdjustments(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MPRBalanceSheetAdjustment[] mprbalancesheetadjustment = _MPRBSService.GetAllBalanceSheetAdjustments();

                return request.CreateResponse<MPRBalanceSheetAdjustment[]>(HttpStatusCode.OK, mprbalancesheetadjustment);
            });
        }
        [HttpPost]
        [Route("deletemprbalancesheetadjustment")]
        public HttpResponseMessage DeleteBalanceSheetThreshold(HttpRequestMessage request, [FromBody]int mprbalancesheetadjustmentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MPRBalanceSheetAdjustment mprbalancesheetadjustment = _MPRBSService.GetBalanceSheetAdjustment(mprbalancesheetadjustmentId);

                if (mprbalancesheetadjustment != null)
                {
                    _MPRBSService.DeleteBalanceSheetAdjustment(mprbalancesheetadjustmentId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No mprbalancesheetadjustment found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getbalancesheetadjustment/{searchType}/{searchValue}/{number}")]
        public HttpResponseMessage GetBalanceSheetAdjustments(HttpRequestMessage request, string searchType, string searchValue, int number)
        {
            return GetHttpResponse(request, () =>
            {
                MPRBalanceSheetAdjustment[] mprbalancesheetadjustment = _MPRBSService.GetBalanceSheetAdjustments(searchType, searchValue, number);

                return request.CreateResponse<MPRBalanceSheetAdjustment[]>(HttpStatusCode.OK, mprbalancesheetadjustment);
            });
        }


        [HttpGet]
        [Route("getmprbalancesheetadjustment/{mprbalancesheetadjustmentId}")]
        public HttpResponseMessage GetBalanceSheetThreshold(HttpRequestMessage request, int mprbalancesheetadjustmentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MPRBalanceSheetAdjustment mprbalancesheetadjustment = _MPRBSService.GetBalanceSheetAdjustment(mprbalancesheetadjustmentId);

                // notice no need to create a seperate model object since BalanceSheetThreshold entity will do just fine
                response = request.CreateResponse<MPRBalanceSheetAdjustment>(HttpStatusCode.OK, mprbalancesheetadjustment);

                return response;
            });
        }



        [HttpGet]
        [Route("getCodebyUser")]
        public HttpResponseMessage GetCodebyUser(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                var userName = User.Identity.Name;
                MPRBalanceSheetAdjustment[] mprbalancesheetadjustment = _MPRBSService.GetCodebyUsers(userName).ToArray();

                List<BalanceSheetAdjDateModel> code = new List<BalanceSheetAdjDateModel>();

                List<string> codes = null;

                codes = mprbalancesheetadjustment.Select(c => c.Code).Distinct().ToList();

                foreach (var c in codes)
                    code.Add(new BalanceSheetAdjDateModel()
                    {
                        Code = c
                    });
                return request.CreateResponse<BalanceSheetAdjDateModel[]>(HttpStatusCode.OK, code.ToArray());
            });
        }


        [HttpGet]
        [Route("getbalancesheetadjustment/{code}")]
        public HttpResponseMessage GetBalanceSheetAdjustmentByCode(HttpRequestMessage request, string code)
        {
            return GetHttpResponse(request, () =>
            {
                var userName = User.Identity.Name;
                MPRBalanceSheetAdjustment[] mprbalancesheetadjustment = _MPRBSService.GetBalanceSheetAdjustmentByCode(code, userName);

                return request.CreateResponse<MPRBalanceSheetAdjustment[]>(HttpStatusCode.OK, mprbalancesheetadjustment);
            });
        }


        [HttpPost]
        [Route("deletebalancesheetadjustmentbycode")]
        public HttpResponseMessage DeleteMPRBalanceSheetAdjustment(HttpRequestMessage request, [FromBody] BalanceSheetAdjustment param)
        {
            return GetHttpResponse(request, () =>
            {
                var userName = User.Identity.Name;
                HttpResponseMessage response = null;

                _MPRBSService.DeleteMPRBalanceSheetAdjustment(param.code, userName);

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

    }
}
