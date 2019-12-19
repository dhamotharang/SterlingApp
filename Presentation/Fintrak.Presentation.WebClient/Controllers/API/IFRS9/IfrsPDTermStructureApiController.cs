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
    [RoutePrefix("api/IfrsPdTermStructure")]
    [UsesDisposableService]
    public class IfrsPdTermStructureListController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsPdTermStructureListController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateIfrsPdTermStructure")]
        public HttpResponseMessage UpdateIfrsPdTermStructure(HttpRequestMessage request, [FromBody]IfrsPdTermStructure IfrsPdTermStructureModel)
        {
            return GetHttpResponse(request, () =>
            {
                var IfrsPdTermStructure = _IFRS9Service.UpdateIfrsPdTermStructure(IfrsPdTermStructureModel);

                return request.CreateResponse<IfrsPdTermStructure>(HttpStatusCode.OK, IfrsPdTermStructure);
            });
        }

        [HttpPost]
        [Route("deleteIfrsPdTermStructure")]
        public HttpResponseMessage DeleteIfrsPdTermStructure(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsPdTermStructure IfrsPdTermStructure = _IFRS9Service.GetIfrsPdTermStructure(ID);

                if (IfrsPdTermStructure != null)
                {
                    _IFRS9Service.DeleteIfrsPdTermStructure(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IfrsPdTermStructure found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getIfrsPdTermStructure/{Id}")]
        public HttpResponseMessage GetIfrsPdTermStructure(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsPdTermStructure IfrsPdTermStructure = _IFRS9Service.GetIfrsPdTermStructure(ID);

                // notice no need to create a seperate model object since IfrsPdTermStructureentity will do just fine
                response = request.CreateResponse<IfrsPdTermStructure>(HttpStatusCode.OK, IfrsPdTermStructure);

                return response;
            });
        }

   
 

    [HttpGet]
    [Route("getIfrsPdTermStructures")]
    public HttpResponseMessage GetAllIfrsPdTermStructure(HttpRequestMessage request)
    {
      return GetHttpResponse(request, () =>
      {
        IfrsPdTermStructure[] IfrsPdTermStructure = _IFRS9Service.GetAllIfrsPdTermStructures();

        return request.CreateResponse<IfrsPdTermStructure[]>(HttpStatusCode.OK, IfrsPdTermStructure);
      });
    }


  }
}
