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
    [RoutePrefix("api/sectorialregressedpd")]
    [UsesDisposableService]
    public class SectorialRegressedPDApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SectorialRegressedPDApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        //[HttpPost]
        //[Route("updatesectorialRegressedPD")]
        //public HttpResponseMessage UpdateSectorialRegressedPD(HttpRequestMessage request, [FromBody]SectorialRegressedPD sectorialRegressedPDModel)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        var sectorialRegressedPD = _IFRS9Service.UpdateSectorialRegressedPD(sectorialRegressedPDModel);

        //        return request.CreateResponse<SectorialRegressedPD>(HttpStatusCode.OK, sectorialRegressedPD);
        //    });
        //}

        //[HttpPost]
        //[Route("deletesectorialRegressedPD")]
        //public HttpResponseMessage DeleteSectorialRegressedPD(HttpRequestMessage request, [FromBody]int sectorialRegressedPDId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        // not that calling the WCF service here will authenticate access to the data
        //        SectorialRegressedPD sectorialRegressedPD = _IFRS9Service.GetSectorialRegressedPD(sectorialRegressedPDId);

        //        if (sectorialRegressedPD != null)
        //        {
        //            _IFRS9Service.DeleteSectorialRegressedPD(sectorialRegressedPDId);

        //            response = request.CreateResponse(HttpStatusCode.OK);
        //        }
        //        else
        //            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No sectorialRegressedPD found under that ID.");

        //        return response;
        //    });
        //}

        //[HttpGet]
        //[Route("getsectorialRegressedPD/{sectorialRegressedPDId}")]
        //public HttpResponseMessage GetSectorialRegressedPD(HttpRequestMessage request,int sectorialRegressedPDId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        SectorialRegressedPD sectorialRegressedPD = _IFRS9Service.GetSectorialRegressedPD(sectorialRegressedPDId);

        //        // notice no need to create a seperate model object since SectorialRegressedPD entity will do just fine
        //        response = request.CreateResponse<SectorialRegressedPD>(HttpStatusCode.OK, sectorialRegressedPD);

        //        return response;
        //    });
        //}

        [HttpGet]
        [Route("availablesectorialRegressedPDs")]
        public HttpResponseMessage GetAvailableSectorialRegressedPDs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SectorialRegressedPD[] sectorialRegressedPDs = _IFRS9Service.GetAllSectorialRegressedPDs();

                return request.CreateResponse<SectorialRegressedPD[]>(HttpStatusCode.OK, sectorialRegressedPDs);
            });
        }
    }
}
