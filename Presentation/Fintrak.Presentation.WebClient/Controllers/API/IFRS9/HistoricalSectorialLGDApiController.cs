using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/historicalsectoriallgd")]
    [UsesDisposableService]
    public class HistoricalSectorialLGDApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public HistoricalSectorialLGDApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("ulgdatehistoricalSectorialLGD")]
        public HttpResponseMessage UpdateHistoricalSectorialLGD(HttpRequestMessage request, [FromBody]HistoricalSectorialLGD historicalSectorialLGDModel)
        {
            return GetHttpResponse(request, () =>
            {
                var historicalSectorialLGD = _IFRS9Service.UpdateHistoricalSectorialLGD(historicalSectorialLGDModel);

                return request.CreateResponse<HistoricalSectorialLGD>(HttpStatusCode.OK, historicalSectorialLGD);
            });
        }

        [HttpPost]
        [Route("deletehistoricalSectorialLGD")]
        public HttpResponseMessage DeleteHistoricalSectorialLGD(HttpRequestMessage request, [FromBody]int historicalSectorialLGDId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                HistoricalSectorialLGD historicalSectorialLGD = _IFRS9Service.GetHistoricalSectorialLGD(historicalSectorialLGDId);

                if (historicalSectorialLGD != null)
                {
                    _IFRS9Service.DeleteHistoricalSectorialLGD(historicalSectorialLGDId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No historicalSectorialLGD found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("gethistoricalsectoriallgd/{historicalSectorialLGDId}")]
        public HttpResponseMessage GetHistoricalSectorialLGD(HttpRequestMessage request,int historicalSectorialLGDId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                HistoricalSectorialLGD historicalSectorialLGD = _IFRS9Service.GetHistoricalSectorialLGD(historicalSectorialLGDId);

                // notice no need to create a seperate model object since HistoricalSectorialLGD entity will do just fine
                response = request.CreateResponse<HistoricalSectorialLGD>(HttpStatusCode.OK, historicalSectorialLGD);

                return response;
            });
        }

        [HttpGet]
        [Route("availablehistoricalSectorialLGDs")]
        public HttpResponseMessage GetAvailableHistoricalSectorialLGDs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HistoricalSectorialLGD[] historicalSectorialLGDs = _IFRS9Service.GetAllHistoricalSectorialLGDs();

                return request.CreateResponse<HistoricalSectorialLGD[]>(HttpStatusCode.OK, historicalSectorialLGDs);
            });
        }

        [HttpGet]
        [Route("getyears")]
        public HttpResponseMessage GetYears(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string[] years = _IFRS9Service.GetDistinctYear();
                List<ReferenceNoModel> cYears = new List<ReferenceNoModel>();
                foreach (var c in years)
                    cYears.Add(new ReferenceNoModel()
                    {
                        RefNo = c

                    });

                return request.CreateResponse<ReferenceNoModel[]>(HttpStatusCode.OK, cYears.ToArray());
            });
        }

        [HttpGet]
        [Route("getperiods")]
        public HttpResponseMessage GetPeriods(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string[] periods = _IFRS9Service.GetDistinctPeriod();
                List<ReferenceNoModel> cPeriods = new List<ReferenceNoModel>();
                foreach (var c in periods)
                    cPeriods.Add(new ReferenceNoModel()
                    {
                        RefNo = c

                    });

                return request.CreateResponse<ReferenceNoModel[]>(HttpStatusCode.OK, cPeriods.ToArray());
            });
        }

        [HttpPost]
        [Route("computelgd")]
        public HttpResponseMessage ComputeHistoricalSectorialLGD(HttpRequestMessage request, [FromBody] HistoricalPDParam param)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _IFRS9Service.ComputeHistoricalSectorialLGD(param.ComputationType,param.CurYear, param.CurPeriod, param.PrevYear, param.PrevPeriod);
               

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }
    }
}
