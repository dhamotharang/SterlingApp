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
    [RoutePrefix("api/opexglbasis")]
    [UsesDisposableService]  
    public class OpexGLBasisApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexGLBasisApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexglBasis")]
        public HttpResponseMessage UpdateOpexGLBasis(HttpRequestMessage request, [FromBody]OpexGLBasis opexGLBasisModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opexGLBasis = _MPROPEXService.UpdateOpexGLBasis(opexGLBasisModel);

                return request.CreateResponse<OpexGLBasis>(HttpStatusCode.OK, opexGLBasis);
            });
        }

        [HttpPost]
        [Route("deleteopexglBasis")]
        public HttpResponseMessage DeleteOpexGLBasis(HttpRequestMessage request, [FromBody]int opexGLBasisId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexGLBasis opexGLBasis = _MPROPEXService.GetOpexGLBasis(opexGLBasisId);

                if (opexGLBasis != null)
                {
                    _MPROPEXService.DeleteOpexGLBasis(opexGLBasisId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OpexGL Basis found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopexglBasis/{opexGLBasisId}")]
        public HttpResponseMessage GetOpexGLBasis(HttpRequestMessage request, int opexGLBasisId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexGLBasis opexGLBasis = _MPROPEXService.GetOpexGLBasis(opexGLBasisId);

                // notice no need to create a seperate model object since OpexGLBasis entity will do just fine
                response = request.CreateResponse<OpexGLBasis>(HttpStatusCode.OK, opexGLBasis);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopexglBasis")]
        public HttpResponseMessage GetAvailableOpexGLBasis(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexGLBasis[] opexGLBasis = _MPROPEXService.GetAllOpexGLBasiss();


                return request.CreateResponse<OpexGLBasis[]>(HttpStatusCode.OK, opexGLBasis);
            });
        }
    }
}
