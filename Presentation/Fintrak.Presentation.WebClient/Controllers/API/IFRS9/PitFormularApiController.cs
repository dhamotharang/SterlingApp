using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/pitformular")]
    [UsesDisposableService]
    public class PitFormularApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PitFormularApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        //[HttpPost]
        //[Route("updatepitFormular")]
        //public HttpResponseMessage UpdatePitFormular(HttpRequestMessage request, [FromBody]PitFormular pitFormularModel)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        var pitFormular = _IFRS9Service.UpdatePitFormular(pitFormularModel);

        //        return request.CreateResponse<PitFormular>(HttpStatusCode.OK, pitFormular);
        //    });
        //}

        //[HttpPost]
        //[Route("deletepitFormular")]
        //public HttpResponseMessage DeletePitFormular(HttpRequestMessage request, [FromBody]int pitFormularId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        // not that calling the WCF service here will authenticate access to the data
        //        PitFormular pitFormular = _IFRS9Service.GetPitFormular(pitFormularId);

        //        if (pitFormular != null)
        //        {
        //            _IFRS9Service.DeletePitFormular(pitFormularId);

        //            response = request.CreateResponse(HttpStatusCode.OK);
        //        }
        //        else
        //            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No pitFormular found under that ID.");

        //        return response;
        //    });
        //}

        //[HttpGet]
        //[Route("getpitFormular/{pitFormularId}")]
        //public HttpResponseMessage GetPitFormular(HttpRequestMessage request,int pitFormularId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        PitFormular pitFormular = _IFRS9Service.GetPitFormular(pitFormularId);

        //        // notice no need to create a seperate model object since PitFormular entity will do just fine
        //        response = request.CreateResponse<PitFormular>(HttpStatusCode.OK, pitFormular);

        //        return response;
        //    });
        //}

        [HttpGet]
        [Route("availablepitFormulars")]
        public HttpResponseMessage GetAvailablePitFormulars(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PitFormular[] pitFormulars = _IFRS9Service.GetAllPitFormulars();

                return request.CreateResponse<PitFormular[]>(HttpStatusCode.OK, pitFormulars);
            });
        }
    }
}
