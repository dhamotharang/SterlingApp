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
    [RoutePrefix("api/historicalsectorialpd")]
    [UsesDisposableService]
    public class HistoricalSectorialPDApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public HistoricalSectorialPDApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatehistoricalSectorialPD")]
        public HttpResponseMessage UpdateHistoricalSectorialPD(HttpRequestMessage request, [FromBody]HistoricalSectorialPD historicalSectorialPDModel)
        {
            return GetHttpResponse(request, () =>
            {
                var historicalSectorialPD = _IFRS9Service.UpdateHistoricalSectorialPD(historicalSectorialPDModel);

                return request.CreateResponse<HistoricalSectorialPD>(HttpStatusCode.OK, historicalSectorialPD);
            });
        }

        [HttpPost]
        [Route("deletehistoricalSectorialPD")]
        public HttpResponseMessage DeleteHistoricalSectorialPD(HttpRequestMessage request, [FromBody]int historicalSectorialPDId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                HistoricalSectorialPD historicalSectorialPD = _IFRS9Service.GetHistoricalSectorialPD(historicalSectorialPDId);

                if (historicalSectorialPD != null)
                {
                    _IFRS9Service.DeleteHistoricalSectorialPD(historicalSectorialPDId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No historicalSectorialPD found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("gethistoricalsectorialpd/{historicalSectorialPDId}")]
        public HttpResponseMessage GetHistoricalSectorialPD(HttpRequestMessage request,int historicalSectorialPDId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                HistoricalSectorialPD historicalSectorialPD = _IFRS9Service.GetHistoricalSectorialPD(historicalSectorialPDId);

                // notice no need to create a seperate model object since HistoricalSectorialPD entity will do just fine
                response = request.CreateResponse<HistoricalSectorialPD>(HttpStatusCode.OK, historicalSectorialPD);

                return response;
            });
        }

        [HttpGet]
        [Route("availablehistoricalSectorialPDs")]
        public HttpResponseMessage GetAvailableHistoricalSectorialPDs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HistoricalSectorialPD[] historicalSectorialPDs = _IFRS9Service.GetAllHistoricalSectorialPDs();

                return request.CreateResponse<HistoricalSectorialPD[]>(HttpStatusCode.OK, historicalSectorialPDs);
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
        [Route("computepd")]
        public HttpResponseMessage ComputeHistoricalSectorialPD(HttpRequestMessage request, [FromBody] HistoricalPDParam param)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _IFRS9Service.ComputeHistoricalSectorialPD(param.ComputationType,param.CurYear, param.CurPeriod, param.PrevYear, param.PrevPeriod);
               

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }
    }
}
