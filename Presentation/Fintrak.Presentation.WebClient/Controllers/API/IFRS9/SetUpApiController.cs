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
    [RoutePrefix("api/setup")]
    [UsesDisposableService]
    public class SetUpApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SetUpApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatesetUp")]
        public HttpResponseMessage UpdateSetUp(HttpRequestMessage request, [FromBody]SetUp setUpModel)
        {
            return GetHttpResponse(request, () =>
            {
                var setUp = _IFRS9Service.UpdateSetUp(setUpModel);

                return request.CreateResponse<SetUp>(HttpStatusCode.OK, setUp);
            });
        }

        [HttpPost]
        [Route("deletesetUp")]
        public HttpResponseMessage DeleteSetUp(HttpRequestMessage request, [FromBody]int setUpId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SetUp setUp = _IFRS9Service.GetSetUp(setUpId);

                if (setUp != null)
                {
                    _IFRS9Service.DeleteSetUp(setUpId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No setUp found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getsetUp/{setUpId}")]
        public HttpResponseMessage GetSetUp(HttpRequestMessage request,int setUpId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SetUp setUp = _IFRS9Service.GetSetUp(setUpId);

                // notice no need to create a seperate model object since SetUp entity will do just fine
                response = request.CreateResponse<SetUp>(HttpStatusCode.OK, setUp);

                return response;
            });
        }

        [HttpGet]
        [Route("availablesetUps")]
        public HttpResponseMessage GetAvailableSetUps(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SetUp[] setUps = _IFRS9Service.GetAllSetUps();

                return request.CreateResponse<SetUp[]>(HttpStatusCode.OK, setUps);
            });
        }
    }
}
