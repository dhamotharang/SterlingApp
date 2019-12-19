using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;


namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/mprcommfee")]
    [UsesDisposableService]
    public class mprcommfeeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public mprcommfeeApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        [HttpPost]
        [Route("updatemprcommfee")]
        public HttpResponseMessage Updatemprcommfee(HttpRequestMessage request, [FromBody]MPRCommFee mprcommfeeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var mprcommfee = _MPRPLService.UpdateMPRCommFee(mprcommfeeModel);

                return request.CreateResponse<MPRCommFee>(HttpStatusCode.OK, mprcommfee);
            });
        }

        [HttpPost]
        [Route("deletemprcommfee")]
        public HttpResponseMessage Deletemprcommfee(HttpRequestMessage request, [FromBody]int CommFee_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // note that calling the WCF service here will authenticate access to the data
                MPRCommFee mprcommfee = _MPRPLService.GetMPRCommFee(CommFee_Id);

                if (mprcommfee != null)
                {
                    _MPRPLService.DeleteMPRCommFee(CommFee_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No MPR Commission & Fee found under that ID.");

                return response;
            });
        }

 

        [HttpGet]
        [Route("getmprcommfee/{commfee_Id}")]
        public HttpResponseMessage Getmprcommfee(HttpRequestMessage request, int CommFee_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MPRCommFee mprcommfee = _MPRPLService.GetMPRCommFee(CommFee_Id);

                // notice no need to create a seperate model object since MPRCommFee entity will do just fine
                response = request.CreateResponse<MPRCommFee>(HttpStatusCode.OK, mprcommfee);

                return response;
            });
        }



        [HttpGet]
        [Route("getmprcommfeebysearch/{searchValue}")]
        public HttpResponseMessage Getmprcommfeebysearch(HttpRequestMessage request, string searchValue)
        {

            if (HttpContext.Current.Session["result"] != null)
                HttpContext.Current.Session["result"] = null;

            return GetHttpResponse(request, () =>
            {
                MPRCommFee[] mprcommfee = _MPRPLService.GetMPRCommFeesBySearch(searchValue);

                if (HttpContext.Current.Session["result"] == null)
                {
                    HttpContext.Current.Session["result"] = mprcommfee;
                }

                return request.CreateResponse<MPRCommFee[]>(HttpStatusCode.OK, mprcommfee);
            });
        }


        //[HttpGet]
        //[Route("getmprcommfeeby/{searchType}/{searchValue}")]
        //public HttpResponseMessage Getmprcommfeeby(HttpRequestMessage request, string searchType, string searchValue)
        //{

        //    if (HttpContext.Current.Session["result"] != null)
        //        HttpContext.Current.Session["result"] = null;

        //    return GetHttpResponse(request, () =>
        //    {
        //        MPRCommFee[] mprcommfee = _MPRPLService.GetMPRCommFeesBy(searchType, searchValue);

        //        if (HttpContext.Current.Session["result"] == null)
        //        {
        //            HttpContext.Current.Session["result"] = mprcommfee;
        //        }

        //        return request.CreateResponse<MPRCommFee[]>(HttpStatusCode.OK, mprcommfee);
        //    });
        //}


        [HttpGet]
        [Route("getmprcommfees")]
        public HttpResponseMessage Getmprcommfees(HttpRequestMessage request)
        {

            if (HttpContext.Current.Session["result"] != null)
                HttpContext.Current.Session["result"] = null;

            return GetHttpResponse(request, () =>
            {
                MPRCommFee[] mprcommfee = _MPRPLService.GetMPRCommFees();

                if (HttpContext.Current.Session["result"] == null)
                {
                    HttpContext.Current.Session["result"] = mprcommfee;
                }

                return request.CreateResponse<MPRCommFee[]>(HttpStatusCode.OK, mprcommfee);
            });
        }
    }
}
