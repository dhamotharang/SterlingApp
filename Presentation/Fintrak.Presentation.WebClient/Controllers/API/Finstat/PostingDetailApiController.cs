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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/postingdetail")]
    [UsesDisposableService]
    public class PostingDetailApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PostingDetailApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpGet]
        [Route("getpostingdetail/{reportType}")]
        public HttpResponseMessage GetAvailablePostingDetailsByType(HttpRequestMessage request, int reportType)
        {
            return GetHttpResponse(request, () =>
            {
                PostingDetailData[] postingDetails = null;
           
                    postingDetails = _FinstatService.GetPostingDetailsByType(reportType);

                return request.CreateResponse<PostingDetailData[]>(HttpStatusCode.OK, postingDetails);
            });
        }
    }
}
