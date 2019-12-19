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
    [RoutePrefix("api/spcumulativepd")]
    [UsesDisposableService]
    public class SPCumulativePDApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SPCumulativePDApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatespcumulativepd")]
        public HttpResponseMessage UpdateSPCumulativePD(HttpRequestMessage request, [FromBody]SPCumulativePD spcumulativepdModel)
        {
            return GetHttpResponse(request, () =>
            {
                var spcumulativepd = _IFRS9Service.UpdateSPCumulativePD(spcumulativepdModel);

                return request.CreateResponse<SPCumulativePD>(HttpStatusCode.OK, spcumulativepd);
            });
        }

        [HttpPost]
        [Route("deletespcumulativepd")]
        public HttpResponseMessage DeleteSPCumulativePD(HttpRequestMessage request, [FromBody]int SPCumulative_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SPCumulativePD spcumulativepd = _IFRS9Service.GetSPCumulativePD(SPCumulative_Id);

                if (spcumulativepd != null)
                {
                    _IFRS9Service.DeleteSPCumulativePD(SPCumulative_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No SPCumulativePD found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getspcumulativepd/{SPCumulative_Id}")]
        public HttpResponseMessage GetSPCumulativePD(HttpRequestMessage request,int SPCumulative_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SPCumulativePD spcumulativepd = _IFRS9Service.GetSPCumulativePD(SPCumulative_Id);

                // notice no need to create a seperate model object since SPCumulativePD entity will do just fine
                response = request.CreateResponse<SPCumulativePD>(HttpStatusCode.OK, spcumulativepd);

                return response;
            });
        }

        [HttpGet]
        [Route("availablespcumulativepds")]
        public HttpResponseMessage GetAvailableSPCumulativePDs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SPCumulativePD[] spcumulativepds = _IFRS9Service.GetAllSPCumulativePDs();

                return request.CreateResponse<SPCumulativePD[]>(HttpStatusCode.OK, spcumulativepds);
            });
        }
    }
}
