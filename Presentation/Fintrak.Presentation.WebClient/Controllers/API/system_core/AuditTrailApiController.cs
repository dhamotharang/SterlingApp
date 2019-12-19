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
using Fintrak.Shared.SystemCore.Framework;
using audit = Fintrak.Shared.AuditService;

//api/auditTrail/availableauditTrails  

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/auditTrail")]
    [UsesDisposableService]
    public class AuditTrailApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AuditTrailApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpGet]
        [Route("getauditTrail/{auditTrailId}")]
        public HttpResponseMessage GetAuditTrail(HttpRequestMessage request,int auditTrailId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                audit.AuditTrail auditTrail = _CoreService.GetAuditTrail(auditTrailId);

                // notice no need to create a seperate model object since AuditTrail entity will do just fine
                response = request.CreateResponse<audit.AuditTrail>(HttpStatusCode.OK, auditTrail);

                return response;
            });
        }

        [HttpGet]
        [Route("getaudittrailbydate/{fromDate}/{toDate}")]
        public HttpResponseMessage GetAuditTrailByDate(HttpRequestMessage request,DateTime fromDate, DateTime toDate)
        {
            return GetHttpResponse(request, () =>
            {
                audit.AuditTrail[] auditTrails = _CoreService.GetAuditTrails(fromDate, toDate);

                return request.CreateResponse<audit.AuditTrail[]>(HttpStatusCode.OK, auditTrails);
            });
        }

        [HttpGet]
        [Route("getaudittrailbyaction/{actions}/{fromDate}/{toDate}")]
        public HttpResponseMessage GetAuditTrailByAction(HttpRequestMessage request, string actions, DateTime fromDate, DateTime toDate)
        {
            return GetHttpResponse(request, () =>
            {
                audit.AuditTrail[] audittrail = _CoreService.GetAuditTrailByAction(actions, fromDate, toDate);

                return request.CreateResponse<audit.AuditTrail[]>(HttpStatusCode.OK, audittrail);
            });
        }

        

        [HttpGet]
        [Route("getaudittrailbytable/{tableName}/{fromDate}/{toDate}")]
        public HttpResponseMessage GetAuditTrailByTable(HttpRequestMessage request, string tableName, DateTime fromDate, DateTime toDate)
        {
            return GetHttpResponse(request, () =>
            {
                audit.AuditTrail[] auditTrails = _CoreService.GetAuditTrailByTable(tableName, fromDate, toDate);

                return request.CreateResponse<audit.AuditTrail[]>(HttpStatusCode.OK, auditTrails);
            });
        }


        [HttpGet]
        [Route("availableaudittrail")]
        public HttpResponseMessage GetAvailableAuditTrail(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                audit.AuditTrail[] auditTrails = _CoreService.GetAllAuditTrails();

                return request.CreateResponse<audit.AuditTrail[]>(HttpStatusCode.OK, auditTrails);
            });
        }


        [HttpGet]
        [Route("getaudittrailbytab/{actions}")]
        public HttpResponseMessage GetAuditTrailByTab(HttpRequestMessage request, audit.AuditAction actions)
        {
            return GetHttpResponse(request, () =>
            {
                audit.AuditTrail[] auditTrails = _CoreService.GetAuditTrailByTab(actions);

                return request.CreateResponse<audit.AuditTrail[]>(HttpStatusCode.OK, auditTrails);
            });
        }

        [HttpGet]
        [Route("getaudittrailbytable/{loginID}/{fromDate}/{toDate}")]
        public HttpResponseMessage GetAuditTrailByLoginID(HttpRequestMessage request, string loginID, DateTime fromDate, DateTime toDate)
        {
            return GetHttpResponse(request, () =>
            {
                audit.AuditTrail[] auditTrails = _CoreService.GetAuditTrailByLoginID(loginID, fromDate, toDate);

                return request.CreateResponse<audit.AuditTrail[]>(HttpStatusCode.OK, auditTrails);
            });
        }
    }
}
