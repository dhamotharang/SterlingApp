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
    [RoutePrefix("api/collateralinput")]
    [UsesDisposableService]
    public class CollateralInputApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CollateralInputApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatecollateralinput")]
        public HttpResponseMessage UpdateCollateralInput(HttpRequestMessage request, [FromBody]CollateralInput collateralinputModel)
        {
            return GetHttpResponse(request, () =>
            {
                var collateralinput = _IFRS9Service.UpdateCollateralInput(collateralinputModel);

                return request.CreateResponse<CollateralInput>(HttpStatusCode.OK, collateralinput);
            });
        }

        [HttpPost]
        [Route("deletecollateralinput")]
        public HttpResponseMessage DeleteCollateralInput(HttpRequestMessage request, [FromBody]int Collateral_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CollateralInput collateralinput = _IFRS9Service.GetCollateralInput(Collateral_Id);

                if (collateralinput != null)
                {
                    _IFRS9Service.DeleteCollateralInput(Collateral_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No CollateralInput found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcollateralinput/{Collateral_Id}")]
        public HttpResponseMessage GetCollateralInput(HttpRequestMessage request,int Collateral_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CollateralInput collateralinput = _IFRS9Service.GetCollateralInput(Collateral_Id);

                // notice no need to create a seperate model object since CollateralInput entity will do just fine
                response = request.CreateResponse<CollateralInput>(HttpStatusCode.OK, collateralinput);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecollateralinputs")]
        public HttpResponseMessage GetAvailableCollateralInputs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CollateralInput[] collateralinputs = _IFRS9Service.GetAllCollateralInputs();

                return request.CreateResponse<CollateralInput[]>(HttpStatusCode.OK, collateralinputs);
            });
        }
    }
}
