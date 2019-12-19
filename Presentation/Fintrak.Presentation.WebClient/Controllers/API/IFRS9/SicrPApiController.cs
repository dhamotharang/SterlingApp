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
    [RoutePrefix("api/sicrparameters")]
    [UsesDisposableService]
    public class SICRParametersListController : ApiControllerBase
    {
        [ImportingConstructor]
        public SICRParametersListController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatesicrparameters")]
        public HttpResponseMessage UpdateSICRParameters(HttpRequestMessage request, [FromBody]SICRParameters sicrparametersModel)
        {
            return GetHttpResponse(request, () =>
            {
                var sicrparameters = _IFRS9Service.UpdateSICRParameters(sicrparametersModel);

                return request.CreateResponse<SICRParameters>(HttpStatusCode.OK, sicrparameters);
            });
        }

        [HttpPost]
        [Route("deletesicrparameters")]
        public HttpResponseMessage DeleteSector(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SICRParameters sicrparameters = _IFRS9Service.GetSICRParameters(Id);

                if (sicrparameters != null)
                {
                    _IFRS9Service.DeleteSICRParameters(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No sicrparameters found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getsicrparameters/{Id}")]
        public HttpResponseMessage GetSector(HttpRequestMessage request,int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SICRParameters sicrparameters = _IFRS9Service.GetSICRParameters(Id);

                // notice no need to create a seperate model object since SICRParametersentity will do just fine
                response = request.CreateResponse<SICRParameters>(HttpStatusCode.OK, sicrparameters);

                return response;
            });
        }

   
 

    [HttpGet]
    [Route("getsicrparameters")]
    public HttpResponseMessage GetAllSICRParameters(HttpRequestMessage request)
    {
      return GetHttpResponse(request, () =>
      {
        SICRParameters[] sicrparameters = _IFRS9Service.GetAllSICRParameters();

        return request.CreateResponse<SICRParameters[]>(HttpStatusCode.OK, sicrparameters);
      });
    }


  }
}
