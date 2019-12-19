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
    [RoutePrefix("api/sectorialregressedlgd")]
    [UsesDisposableService]
    public class SectorialRegressedLGDApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SectorialRegressedLGDApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        //[HttpPost]
        //[Route("updatesectorialRegressedLGD")]
        //public HttpResponseMessage UpdateSectorialRegressedLGD(HttpRequestMessage request, [FromBody]SectorialRegressedLGD sectorialRegressedLGDModel)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        var sectorialRegressedLGD = _IFRS9Service.UpdateSectorialRegressedLGD(sectorialRegressedLGDModel);

        //        return request.CreateResponse<SectorialRegressedLGD>(HttpStatusCode.OK, sectorialRegressedLGD);
        //    });
        //}

        //[HttpPost]
        //[Route("deletesectorialRegressedLGD")]
        //public HttpResponseMessage DeleteSectorialRegressedLGD(HttpRequestMessage request, [FromBody]int sectorialRegressedLGDId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        // not that calling the WCF service here will authenticate access to the data
        //        SectorialRegressedLGD sectorialRegressedLGD = _IFRS9Service.GetSectorialRegressedLGD(sectorialRegressedLGDId);

        //        if (sectorialRegressedLGD != null)
        //        {
        //            _IFRS9Service.DeleteSectorialRegressedLGD(sectorialRegressedLGDId);

        //            response = request.CreateResponse(HttpStatusCode.OK);
        //        }
        //        else
        //            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No sectorialRegressedLGD found under that ID.");

        //        return response;
        //    });
        //}

        //[HttpGet]
        //[Route("getsectorialRegressedLGD/{sectorialRegressedLGDId}")]
        //public HttpResponseMessage GetSectorialRegressedLGD(HttpRequestMessage request,int sectorialRegressedLGDId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        SectorialRegressedLGD sectorialRegressedLGD = _IFRS9Service.GetSectorialRegressedLGD(sectorialRegressedLGDId);

        //        // notice no need to create a seperate model object since SectorialRegressedLGD entity will do just fine
        //        response = request.CreateResponse<SectorialRegressedLGD>(HttpStatusCode.OK, sectorialRegressedLGD);

        //        return response;
        //    });
        //}

        [HttpGet]
        [Route("availablesectorialRegressedLGDs")]
        public HttpResponseMessage GetAvailableSectorialRegressedLGDs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SectorialRegressedLGD[] sectorialRegressedLGDs = _IFRS9Service.GetAllSectorialRegressedLGDs();

                return request.CreateResponse<SectorialRegressedLGD[]>(HttpStatusCode.OK, sectorialRegressedLGDs);
            });
        }
    }
}
