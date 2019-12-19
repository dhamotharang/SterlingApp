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
    [RoutePrefix("api/revenue")]
    [UsesDisposableService]
    public class revenueApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public revenueApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        [HttpPost]
        [Route("updaterevenue")]
        public HttpResponseMessage Updaterevenue(HttpRequestMessage request, [FromBody]Revenue revenueModel)
        {
            return GetHttpResponse(request, () =>
            {
                var revenue = _MPRPLService.UpdateRevenue(revenueModel);

                return request.CreateResponse<Revenue>(HttpStatusCode.OK, revenue);
            });
        }

        [HttpPost]
        [Route("deleterevenue")]
        public HttpResponseMessage Deleterevenue(HttpRequestMessage request, [FromBody]int revenueId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Revenue revenue = _MPRPLService.GetRevenue(revenueId);

                if (revenue != null)
                {
                    _MPRPLService.DeleteRevenue(revenueId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No revenue found under that ID.");

                return response;
            });
        }

 

        [HttpGet]
        [Route("getrevenue/{revenueId}")]
        public HttpResponseMessage Getrevenue(HttpRequestMessage request, int revenueId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Revenue revenue = _MPRPLService.GetRevenue(revenueId);

                // notice no need to create a seperate model object since Revenue entity will do just fine
                response = request.CreateResponse<Revenue>(HttpStatusCode.OK, revenue);

                return response;
            });
        }


        [HttpGet]
        [Route("getrevenue/{searchType}/{searchValue}/{number}/{runDate}/{toDate}")]
        public HttpResponseMessage GetBalanceSheets(HttpRequestMessage request, string searchType, string searchValue, int number, DateTime runDate, DateTime toDate)
        {

            if (HttpContext.Current.Session["result"] != null)
                HttpContext.Current.Session["result"] = null;

            return GetHttpResponse(request, () =>
            {
                Revenue[] revenue = _MPRPLService.GetAllRevenues(searchType, searchValue, number, runDate, toDate);

                if (HttpContext.Current.Session["result"] == null)
                {
                    HttpContext.Current.Session["result"] = revenue;
                }

                return request.CreateResponse<Revenue[]>(HttpStatusCode.OK, revenue);
            });
        }

        [HttpGet]
        [Route("availablerevenue/{searchType}/{searchValue}/{number}")]
        public HttpResponseMessage GetAvailablerevenue(HttpRequestMessage request, string searchType, string searchValue, int number)
        {

            if (HttpContext.Current.Session["result"] != null)
                HttpContext.Current.Session["result"] = null;

            return GetHttpResponse(request, () =>
            {
                Revenue[] revenue = _MPRPLService.GetRevenues(searchType,searchValue, number);


                if (HttpContext.Current.Session["result"] == null)
                {
                    HttpContext.Current.Session["result"] = revenue;
                }
                return request.CreateResponse<Revenue[]>(HttpStatusCode.OK, revenue);
            });
        }



        [HttpGet]
        [Route("getDate")]
        public HttpResponseMessage GetReferenceNos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Revenue[] revenue = _MPRPLService.GetRunDate().ToArray();

                List<ReveueDateModel> runDate = new List<ReveueDateModel>();

                List<DateTime> rundates = null;

                rundates = revenue.Select(c => c.RunDate).Distinct().ToList();

                foreach (var c in rundates)
                    runDate.Add(new ReveueDateModel()
                    {
                        RunDate = c
                    });
                return request.CreateResponse<ReveueDateModel[]>(HttpStatusCode.OK, runDate.ToArray());
            });
        }

        [HttpGet]
        [Route("getTodate")]
        public HttpResponseMessage GetReferenceToDate(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Revenue[] revenue = _MPRPLService.GetRunDate().ToArray();

                List<ReveueDateModel> toDate = new List<ReveueDateModel>();

                List<DateTime> todates = null;

                todates = revenue.Select(c => c.RunDate).Distinct().ToList();

                foreach (var c in todates)
                    toDate.Add(new ReveueDateModel()
                    {
                        ToDate = c
                    });
                return request.CreateResponse<ReveueDateModel[]>(HttpStatusCode.OK, toDate.ToArray());
            });
        }


    }
}
