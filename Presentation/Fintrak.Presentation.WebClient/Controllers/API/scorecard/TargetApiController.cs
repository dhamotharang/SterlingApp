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
    [RoutePrefix("api/target")]
    [UsesDisposableService]
    public class TargetApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TargetApiController(IScorecardService scoreCardService)
        {
            _ScorecardService = scoreCardService;
        }

        IScorecardService _ScorecardService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ScorecardService);
        }

        [HttpPost]
        [Route("updatetarget")]
        public HttpResponseMessage UpdateTarget(HttpRequestMessage request, [FromBody]SCDTarget targetModel)
        {
            return GetHttpResponse(request, () =>
            {
                var target = _ScorecardService.UpdateSCDTarget(targetModel);

                return request.CreateResponse<SCDTarget>(HttpStatusCode.OK, target);
            });
        }

        [HttpPost]
        [Route("deletetarget")]
        public HttpResponseMessage DeleteTarget(HttpRequestMessage request, [FromBody]int targetId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SCDTarget target = _ScorecardService.GetSCDTarget(targetId);

                if (target != null)
                {
                    _ScorecardService.DeleteSCDTarget(targetId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No target found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("gettarget/{targetId}")]
        public HttpResponseMessage GetTarget(HttpRequestMessage request, int targetId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SCDTarget target = _ScorecardService.GetSCDTarget(targetId);

                // notice no need to create a seperate model object since Target entity will do just fine
                response = request.CreateResponse<SCDTarget>(HttpStatusCode.OK, target);

                return response;
            });
        }

        [HttpGet]
        [Route("availabletarget")]
        public HttpResponseMessage GetAvailableTargets(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SCDTarget[] target = _ScorecardService.GetAllSCDTargets();

                return request.CreateResponse<SCDTarget[]>(HttpStatusCode.OK, target);
            });
        }

        //[HttpGet]
        //[Route("getcaptions")]
        //public HttpResponseMessage GetCaptions(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        List<string> captions = _ScorecardService.GetAllSCDTargets().Select(c => c.Caption).Distinct().ToList();

        //        List<KeyValueModel> results = new List<KeyValueModel>();

        //        foreach (var caption in captions)
        //            results.Add(new KeyValueModel() { Key = 0, Value = caption });

        //        return request.CreateResponse<KeyValueModel[]>(HttpStatusCode.OK, results.ToArray());
        //    });
        //}


        [HttpGet]
        [Route("getcaptions")]
        public HttpResponseMessage GetCaptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SCDTarget[] target = _ScorecardService.GetCaptions().ToArray();

                List<SCDTargetDateModel> caption = new List<SCDTargetDateModel>();

                List<string> captions = null;

                captions = target.Select(c => c.Caption).Distinct().ToList();

                foreach (var c in captions)
                    caption.Add(new SCDTargetDateModel()
                    {
                        Caption = c
                    });
                return request.CreateResponse<SCDTargetDateModel[]>(HttpStatusCode.OK, caption.ToArray());
            });
        }

    }
}
