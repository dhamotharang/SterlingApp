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
using System.Web.Hosting;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/historicaldefaultfreq")]
    [UsesDisposableService]
    public class HistoricalDefaultFreqApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public HistoricalDefaultFreqApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatehistoricaldefaultfreq")]
        public HttpResponseMessage UpdateHistoricalDefaultFreq(HttpRequestMessage request, [FromBody]HistoricalDefaultFreq historicaldefaultfreqModel)
        {
            return GetHttpResponse(request, () =>
            {
                var historicaldefaultfreq = _IFRS9Service.UpdateHistoricalDefaultFreq(historicaldefaultfreqModel);

                return request.CreateResponse<HistoricalDefaultFreq>(HttpStatusCode.OK, historicaldefaultfreq);
            });
        }

        [HttpPost]
        [Route("deletehistoricaldefaultfreq")]
        public HttpResponseMessage DeleteHistoricalDefaultFreq(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                HistoricalDefaultFreq historicaldefaultfreq = _IFRS9Service.GetHistoricalDefaultFreq(ID);

                if (historicaldefaultfreq != null)
                {
                    _IFRS9Service.DeleteHistoricalDefaultFreq(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No HistoricalDefaultFreq found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("gethistoricaldefaultfreq/{ID}")]
        public HttpResponseMessage GetHistoricalDefaultFreq(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                HistoricalDefaultFreq historicaldefaultfreq = _IFRS9Service.GetHistoricalDefaultFreq(ID);

                // notice no need to create a seperate model object since HistoricalDefaultFreq entity will do just fine
                response = request.CreateResponse<HistoricalDefaultFreq>(HttpStatusCode.OK, historicaldefaultfreq);

                return response;
            });
        }

        [HttpGet]
        [Route("availablehistoricaldefaultfreqs/{defaultCount}")]
        public HttpResponseMessage GetAvailableHistoricalDefaultFreqs(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    HistoricalDefaultFreq[] historicaldefaultfreqs = _IFRS9Service.GetHistoricalDefaultFreqs(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "Historical%20Default.zip");
                    return response;
                }
                else
                {
                    HistoricalDefaultFreq[] historicaldefaultfreqs = _IFRS9Service.GetHistoricalDefaultFreqs(defaultCount, null);
                    return request.CreateResponse<HistoricalDefaultFreq[]>(HttpStatusCode.OK, historicaldefaultfreqs);
                }
            });
        }



        [HttpGet]
        [Route("getHistoricalDefaultFreqbysearch/{searchParam}")]
        public HttpResponseMessage GetHistoricalDefaultFreqBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                HistoricalDefaultFreq[] historicaldefaultfreqs = _IFRS9Service.GetHistoricalDefaultFreqBySearch(searchParam);
                return request.CreateResponse<HistoricalDefaultFreq[]>(HttpStatusCode.OK, historicaldefaultfreqs.ToArray());
            });
        }





    }
}
