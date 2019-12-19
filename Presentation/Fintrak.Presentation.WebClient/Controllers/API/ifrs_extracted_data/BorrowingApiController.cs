using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/borrowing")]
    [UsesDisposableService]
    public class borrowingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public borrowingApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateborrowing")]
        public HttpResponseMessage Updateborrowing(HttpRequestMessage request, [FromBody]Borrowings borrowingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var borrowing = _IFRSDataService.UpdateBorrowings(borrowingModel);

                return request.CreateResponse<Borrowings>(HttpStatusCode.OK, borrowing);
            });
        }


        [HttpPost]
        [Route("deleteborrowing")]
        public HttpResponseMessage Deleteborrowing(HttpRequestMessage request, [FromBody]int borrowingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Borrowings borrowing = _IFRSDataService.GetBorrowings(borrowingId);

                if (borrowing != null)
                {
                    _IFRSDataService.DeleteBorrowings(borrowingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No borrowing found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("availableborrowing")]
        public HttpResponseMessage GetAvailableborrowings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Borrowings[] borrowing = _IFRSDataService.GetAllBorrowings().ToArray();

                return request.CreateResponse<Borrowings[]>(HttpStatusCode.OK, borrowing.ToArray());
            });
        }

        [HttpGet]
        [Route("getborrowing/{borrowingId}")]
        public HttpResponseMessage GetBorrowing(HttpRequestMessage request, int borrowingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Borrowings borrowing = _IFRSDataService.GetBorrowings(borrowingId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<Borrowings>(HttpStatusCode.OK, borrowing);

                return response;
            });
        }
                
    }
}
