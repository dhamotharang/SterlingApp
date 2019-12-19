using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Scorecard.Contracts;
using Fintrak.Client.Scorecard.Entities;
using CodeEntities = Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/actual")]
    [UsesDisposableService]
    public class ActualApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ActualApiController(IScorecardService scoreCardService)
        {
            _ScorecardService = scoreCardService;
        }

        IScorecardService _ScorecardService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ScorecardService);
        }

        [HttpPost]
        [Route("updateactual")]
        public HttpResponseMessage UpdateActual(HttpRequestMessage request, [FromBody]SCDActual actualModel)
        {
            return GetHttpResponse(request, () =>
            {
                var actual = _ScorecardService.UpdateSCDActual(actualModel);

                return request.CreateResponse<SCDActual>(HttpStatusCode.OK, actual);
            });
        }

        [HttpPost]
        [Route("deleteactual")]
        public HttpResponseMessage DeleteActual(HttpRequestMessage request, [FromBody]int actualId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SCDActual actual = _ScorecardService.GetSCDActual(actualId);

                if (actual != null)
                {
                    _ScorecardService.DeleteSCDActual(actualId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No actual found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getactual/{actualId}")]
        public HttpResponseMessage GetActual(HttpRequestMessage request, int actualId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SCDActual actual = _ScorecardService.GetSCDActual(actualId);

                // notice no need to create a seperate model object since Actual entity will do just fine
                response = request.CreateResponse<SCDActual>(HttpStatusCode.OK, actual);

                return response;
            });
        }

        [HttpGet]
        [Route("availableactual")]
        public HttpResponseMessage GetAvailableActuals(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SCDActual[] actual = _ScorecardService.GetAllSCDActuals();

                return request.CreateResponse<SCDActual[]>(HttpStatusCode.OK, actual);
            });
        }

        //[HttpGet]
        //[Route("getcaptions")]
        //public HttpResponseMessage GetCaptions(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        List<string> captions = _ScorecardService.GetAllSCDActuals().Select(c=>c.Caption).Distinct().ToList();

        //        List<KeyValueModel> results = new List<KeyValueModel>();

        //        foreach (var caption in captions)
        //            results.Add(new KeyValueModel() { Key= 0, Value = caption });

        //        return request.CreateResponse<KeyValueModel[]>(HttpStatusCode.OK, results.ToArray());
        //    });
        //}


        [HttpGet]
        [Route("getcaptions")]
        public HttpResponseMessage GetReferenceNos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SCDActual[] actual = _ScorecardService.GetCaption().ToArray();

                List<SCDActualDateModel> caption = new List<SCDActualDateModel>();

                List<string> captions = null;

                captions = actual.Select(c => c.Caption).Distinct().ToList();

                foreach (var c in captions)
                    caption.Add(new SCDActualDateModel()
                    {
                        Caption = c
                    });
                return request.CreateResponse<SCDActualDateModel[]>(HttpStatusCode.OK, caption.ToArray());
            });
        }
    }
}
