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
    [RoutePrefix("api/reconciliation")]
    [UsesDisposableService]
    public class ReconciliationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ReconciliationApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatereconciliation")]
        public HttpResponseMessage UpdateReconciliation(HttpRequestMessage request, [FromBody]Reconciliation reconciliationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var reconciliation = _IFRS9Service.UpdateReconciliation(reconciliationModel);

                return request.CreateResponse<Reconciliation>(HttpStatusCode.OK, reconciliation);
            });
        }

        [HttpPost]
        [Route("deletereconciliation")]
        public HttpResponseMessage DeleteReconciliation(HttpRequestMessage request, [FromBody]int reconciliationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Reconciliation reconciliation = _IFRS9Service.GetReconciliation(reconciliationId);

                if (reconciliation != null)
                {
                    _IFRS9Service.DeleteReconciliation(reconciliationId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No reconciliation found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getreconciliation/{reconciliationId}")]
        public HttpResponseMessage GetReconciliation(HttpRequestMessage request,int reconciliationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Reconciliation reconciliation = _IFRS9Service.GetReconciliation(reconciliationId);

                // notice no need to create a seperate model object since Reconciliation entity will do just fine
                response = request.CreateResponse<Reconciliation>(HttpStatusCode.OK, reconciliation);

                return response;
            });
        }

        [HttpGet]
        [Route("availablereconciliations")]
        public HttpResponseMessage GetAvailableReconciliations(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Reconciliation[] reconciliations = _IFRS9Service.GetAllReconciliations() ;

                return request.CreateResponse<Reconciliation[]>(HttpStatusCode.OK, reconciliations);
            });
        }
    }
}
