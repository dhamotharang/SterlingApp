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
//using System.Web.Mvc;


namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/mprbalancesheet")]
    [UsesDisposableService]
    public class MPRBalanceSheetApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MPRBalanceSheetApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }


        [HttpPost]
        [Route("updatemprbalancesheet")]
        public HttpResponseMessage UpdateMPRBalanceSheet(HttpRequestMessage request, [FromBody]MPRBalanceSheet mprbalancesheetModel)
        {
            return GetHttpResponse(request, () =>
            {
                var mprbalancesheet = _MPRBSService.UpdateBalanceSheet(mprbalancesheetModel);

                return request.CreateResponse<MPRBalanceSheet>(HttpStatusCode.OK, mprbalancesheet);
            });
        }

        [HttpPost]
        [Route("deletemprbalancesheet")]
        public HttpResponseMessage DeleteMPRBalanceSheet(HttpRequestMessage request, [FromBody]int balancesheetId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MPRBalanceSheet mprbalancesheet = _MPRBSService.GetBalanceSheet(balancesheetId);

                if (mprbalancesheet != null)
                {
                    _MPRBSService.DeleteBalanceSheet(balancesheetId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No balance sheet found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmprbalancesheet/{balancesheetId}")]
        public HttpResponseMessage GetMPRBalanceSheet(HttpRequestMessage request, int balancesheetId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MPRBalanceSheet mprbalancesheet = _MPRBSService.GetBalanceSheet(balancesheetId);

                // notice no need to create a seperate model object since MPRBalanceSheet entity will do just fine
                response = request.CreateResponse<MPRBalanceSheet>(HttpStatusCode.OK, mprbalancesheet);

                return response;
            });
        }

        [HttpGet]
        [Route("getbalancesheet/{searchType}/{searchValue}/{number}/{runDate}")]
        public HttpResponseMessage GetBalanceSheets(HttpRequestMessage request, string searchType, string searchValue, int number, DateTime runDate)
        {
            if (HttpContext.Current.Session["result"] != null)
                HttpContext.Current.Session["result"] = null;

            return GetHttpResponse(request, () =>
            {
                MPRBalanceSheet[] mprbalancesheet = _MPRBSService.GetAllBalanceSheets(searchType, searchValue, number, runDate);

                if (HttpContext.Current.Session["result"] == null)
                {
                    HttpContext.Current.Session["result"] = mprbalancesheet;
                }
                return request.CreateResponse<MPRBalanceSheet[]>(HttpStatusCode.OK, mprbalancesheet);
            });
        }

        //public ActionResult LoadExcel(string searchType, string searchValue, int number, DateTime runDate)
        //{
        //    string contentType = "application/vnd.ms-excel";
        //    MPRBalanceSheet[] mprbalancesheet = _MPRBSService.GetAllBalanceSheets(searchType, searchValue, number, runDate);
        //    this.Response.ContentType = contentType;
        //    this.Response.BinaryWrite(ms.ToArray());
        //    this.Response.End();
        //    return new FileStreamResult(Response.OutputStream, contentType);
        //}

        [HttpGet]
        [Route("availablemprbalancesheet/{number}")]
        public HttpResponseMessage GetAllAvailableMPRBalanceSheets(HttpRequestMessage request, int number)
        {
            if (HttpContext.Current.Session["result"] != null)
                HttpContext.Current.Session["result"] = null;

            return GetHttpResponse(request, () =>
            {
                MPRBalanceSheet[] mprbalancesheet = _MPRBSService.GetmprBalanceSheets(number);


                if (HttpContext.Current.Session["result"] == null)
                {
                    HttpContext.Current.Session["result"] = mprbalancesheet;
                }
                return request.CreateResponse<MPRBalanceSheet[]>(HttpStatusCode.OK, mprbalancesheet);
            });
        }


        [HttpGet]
        [Route("getDate")]
        public HttpResponseMessage GetReferenceNos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MPRBalanceSheet[] mprbalancesheet = _MPRBSService.GetRunDate().ToArray();

                List<BalanceSheetDateModel> runDate = new List<BalanceSheetDateModel>();

                List<DateTime> rundates = null;

                rundates = mprbalancesheet.Select(c => c.Rundate).Distinct().ToList();

                foreach (var c in rundates)
                    runDate.Add(new BalanceSheetDateModel()
                    {
                        RunDate = c
                    });
                return request.CreateResponse<BalanceSheetDateModel[]>(HttpStatusCode.OK, runDate.ToArray());
            });
        }

        //[HttpGet]
        //[Route("getTodate")]
        //public HttpResponseMessage GetReferenceToDate(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        MPRBalanceSheet[] mprbalancesheet = _MPRBSService.GetAllMPRBalanceSheets().ToArray();

        //        List<BalanceSheetDateModel> toDate = new List<BalanceSheetDateModel>();

        //        List<DateTime> todates = null;

        //        todates = mprbalancesheet.Select(c => c.Rundate).Distinct().ToList();

        //        foreach (var c in todates)
        //            toDate.Add(new BalanceSheetDateModel()
        //            {
        //                ToDate = c
        //            });
        //        return request.CreateResponse<BalanceSheetDateModel[]>(HttpStatusCode.OK, toDate.ToArray());
        //    });
        //}

    }
}
