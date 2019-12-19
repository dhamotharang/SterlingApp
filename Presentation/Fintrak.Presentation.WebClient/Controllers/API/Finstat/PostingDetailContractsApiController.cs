using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Shared.IFRS.Framework;
using System.Web.Hosting;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/postingdetailcontracts")]
    [UsesDisposableService]
    public class PostingDetailContractsApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PostingDetailContractsApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpGet]
        [Route("getpostingdetailcontracts")]
        public HttpResponseMessage GetPostingDetailContractsByFilter(HttpRequestMessage request, string filter)
        {
            return GetHttpResponse(request, () =>
            {
                if (string.IsNullOrEmpty(filter)) filter = " "; 
                if (filter.Contains("ExportData"))
                {
                    string path = HostingEnvironment.MapPath("~");
                    PostingDetailContracts[] postingDetailsContracts = _FinstatService.GetPostingDetailContractsByFilter(filter, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "Posting_Adjustment_Contract.zip");
                    return response;
                }
                else
                {
                    PostingDetailContracts[] postingDetailsContracts = _FinstatService.GetPostingDetailContractsByFilter(filter, null);
                    return request.CreateResponse(HttpStatusCode.OK, postingDetailsContracts.ToArray());
                }
            });
        }


        [HttpGet]
        [Route("getfilters/{count}")]
        public HttpResponseMessage GetDistinctFilters(HttpRequestMessage request, int count)
        {
            return GetHttpResponse(request, () =>
            {
                IEnumerable<string> fiters = _FinstatService.GetDistinctPostingFilters(count);

                var dropDown = fiters.Select(e => new
                {
                    name = e,
                    icon = "",
                    maker = "",
                    ticked = false
                });

                return request.CreateResponse(HttpStatusCode.OK, dropDown);

            });
        }
    }
}
