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
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/collateralcategory")]
    [UsesDisposableService]
    public class CollateralCategoryApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CollateralCategoryApiController(IIFRSLoanService loanService)
        {
            _LoanService = loanService;
        }

        IIFRSLoanService _LoanService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_LoanService);
        }

        [HttpPost]
        [Route("updatecollateralcategory")]
        public HttpResponseMessage UpdateCollateralCategory(HttpRequestMessage request, [FromBody]CollateralCategory collateralCategoryModel)
            {
            return GetHttpResponse(request, () =>
            {
                var collateralCategory = _LoanService.UpdateCollateralCategory(collateralCategoryModel);

                return request.CreateResponse<CollateralCategory>(HttpStatusCode.OK, collateralCategory);
            });
        }

        [HttpPost]
        [Route("deletecollateralCategory")]
        public HttpResponseMessage DeleteCollateralCategory(HttpRequestMessage request, [FromBody]int collateralCategoryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CollateralCategory collateralCategory = _LoanService.GetCollateralCategory(collateralCategoryId);

                if (collateralCategory != null)
                {
                    _LoanService.DeleteCollateralCategory(collateralCategoryId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No collateralCategory found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcollateralCategory/{collateralCategoryId}")]
        public HttpResponseMessage GetCollateralCategory(HttpRequestMessage request, int collateralCategoryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CollateralCategory collateralCategory = _LoanService.GetCollateralCategory(collateralCategoryId);

                // notice no need to create a seperate model object since CollateralCategory entity will do just fine
                response = request.CreateResponse<CollateralCategory>(HttpStatusCode.OK, collateralCategory);

                return response;
            });
        }

        [HttpGet]
        [Route("getcollateralcategorywithchildren/{categoryId}")]
        public HttpResponseMessage GetCollateralCategoryWithType(HttpRequestMessage request,int categoryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var item = new CollateralModel();

                CollateralCategory collateralCategory = _LoanService.GetCollateralCategory(categoryId);
                CollateralTypeData[] collateralTypes = _LoanService.GetCollateralTypeByCategory(collateralCategory.Code);

                item.CollateralCategory = collateralCategory;
                item.CollateralType = collateralTypes;

                // notice no need to create a seperate model object since CollateralCategory entity will do just fine
                response = request.CreateResponse<CollateralModel>(HttpStatusCode.OK, item);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecollateralcategories")]
        public HttpResponseMessage GetAvailableCollateralCategories(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CollateralCategory[] collateralCategories = _LoanService.GetAllCollateralCategorys();

                return request.CreateResponse<CollateralCategory[]>(HttpStatusCode.OK, collateralCategories);
            });
        }
      //  CollateralTypeData[] collateralTypes = _LoanService.GetCollateralTypeByCategory(collateralCategory.Code);
       







    }
}
