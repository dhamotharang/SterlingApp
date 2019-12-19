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


namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/checklist")]
    [UsesDisposableService]
    public class CheckListApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CheckListApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        //[HttpPost]
        //[Route("updatechecklist")]
        //public HttpResponseMessage Updatechecklist(HttpRequestMessage request, [FromBody]CheckList checklistModel)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        var checklist = _MPRPLService.UpdateCheckList(checklistModel);

        //        return request.CreateResponse<CheckList>(HttpStatusCode.OK, checklist);
        //    });
        //}

        //[HttpPost]
        //[Route("deletechecklist")]
        //public HttpResponseMessage Deletechecklist(HttpRequestMessage request, [FromBody]int checklistId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        // not that calling the WCF service here will authenticate access to the data
        //        CheckList checklist = _MPRPLService.GetCheckList(checklistId);

        //        if (checklist != null)
        //        {
        //            _MPRPLService.DeleteCheckList(checklistId);

        //            response = request.CreateResponse(HttpStatusCode.OK);
        //        }
        //        else
        //            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No checklist found under that ID.");

        //        return response;
        //    });
        //}

 

        //[HttpGet]
        //[Route("getchecklist/{checklistId}")]
        //public HttpResponseMessage Getchecklist(HttpRequestMessage request, int checklistId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        CheckList checklist = _MPRPLService.GetCheckList(checklistId);

        //        // notice no need to create a seperate model object since CheckList entity will do just fine
        //        response = request.CreateResponse<CheckList>(HttpStatusCode.OK, checklist);

        //        return response;
        //    });
        //}



        [HttpGet]
        [Route("availablechecklist")] 
        public HttpResponseMessage GetAvailablechecklist(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CheckList[] checklist = _MPROPEXService.GetAllCheckLists();

                return request.CreateResponse<CheckList[]>(HttpStatusCode.OK, checklist);
            });
        }

        [HttpGet]
        [Route("runchecklist")]
        public HttpResponseMessage RunCheckList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CheckListData[] checklist = _MPROPEXService.RunCheckList();

                return request.CreateResponse<CheckListData[]>(HttpStatusCode.OK, checklist);
            });
        }
    }
}
