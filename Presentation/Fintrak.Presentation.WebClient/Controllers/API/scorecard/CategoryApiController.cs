using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Scorecard.Contracts;
using Fintrak.Client.Scorecard.Entities;
using CodeEntities = Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/category")]
    [UsesDisposableService]
    public class CategoryApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CategoryApiController(IScorecardService scoreCardService)
        {
            _ScorecardService = scoreCardService;
        }

        IScorecardService _ScorecardService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ScorecardService);
        }

        [HttpPost]
        [Route("updatecategory")]
        public HttpResponseMessage UpdateCategory(HttpRequestMessage request, [FromBody]SCDCategory categoryModel)
        {
            return GetHttpResponse(request, () =>
            {
                var category = _ScorecardService.UpdateSCDCategory(categoryModel);

                return request.CreateResponse<SCDCategory>(HttpStatusCode.OK, category);
            });
        }

        [HttpPost]
        [Route("deletecategory")]
        public HttpResponseMessage DeleteCategory(HttpRequestMessage request, [FromBody]int categoryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SCDCategory category = _ScorecardService.GetSCDCategory(categoryId);

                if (category != null)
                {
                    _ScorecardService.DeleteSCDCategory(categoryId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No category found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcategory/{categoryId}")]
        public HttpResponseMessage GetCategory(HttpRequestMessage request, int categoryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SCDCategory category = _ScorecardService.GetSCDCategory(categoryId);

                // notice no need to create a seperate model object since SCDCategory entity will do just fine
                response = request.CreateResponse<SCDCategory>(HttpStatusCode.OK, category);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecategory")]
        public HttpResponseMessage GetAvailableCategorys(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SCDCategoryData[] category = _ScorecardService.GetAllSCDCategorys();

                return request.CreateResponse<SCDCategoryData[]>(HttpStatusCode.OK, category);
            });
        }
    }
}
