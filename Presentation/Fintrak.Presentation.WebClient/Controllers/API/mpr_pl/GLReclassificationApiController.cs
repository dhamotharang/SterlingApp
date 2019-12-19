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
    [RoutePrefix("api/glreclassification")]
    [UsesDisposableService]
    public class GLReclassificationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public GLReclassificationApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        [HttpPost]
        [Route("updateglreclassification")]
        public HttpResponseMessage UpdateGLReclassification(HttpRequestMessage request, [FromBody]GLReclassification glReclassificationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var glReclassification = _MPRPLService.UpdateGLReclassification(glReclassificationModel);

                return request.CreateResponse<GLReclassification>(HttpStatusCode.OK, glReclassification);
            });
        }

        [HttpPost]
        [Route("deleteglReclassification")]
        public HttpResponseMessage DeleteGLReclassification(HttpRequestMessage request, [FromBody]int glReclassificationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                GLReclassification glReclassification = _MPRPLService.GetGLReclassification(glReclassificationId);

                if (glReclassification != null)
                {
                    _MPRPLService.DeleteGLReclassification(glReclassificationId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No glReclassification found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getglReclassification/{glReclassificationId}")]
        public HttpResponseMessage GetGLReclassification(HttpRequestMessage request, int glReclassificationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                GLReclassification glReclassification = _MPRPLService.GetGLReclassification(glReclassificationId);

                // notice no need to create a seperate model object since GLReclassification entity will do just fine
                response = request.CreateResponse<GLReclassification>(HttpStatusCode.OK, glReclassification);

                return response;
            });
        }

        [HttpGet]
        [Route("availableglReclassification")]
        public HttpResponseMessage GetAvailableGLReclassification(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                GLReclassificationData[] glReclassification = _MPRPLService.GetAllGLReclassifications();

                return request.CreateResponse<GLReclassificationData[]>(HttpStatusCode.OK, glReclassification);
            });
        }
    }
}
