using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Presentation.WebClient.Models;


namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/messagingsubscription")]
    [UsesDisposableService]
    public class MessagingSubscriptionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MessagingSubscriptionApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updatemessagingsubscription")]
        public HttpResponseMessage UpdateMessagingSubscription(HttpRequestMessage request, [FromBody]MessagingSubscription messagingsubscriptionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var messagingsubscription = _MPRCoreService.UpdateMessagingSubscription(messagingsubscriptionModel);

                return request.CreateResponse<MessagingSubscription>(HttpStatusCode.OK, messagingsubscription);
            });
        }

        [HttpPost]
        [Route("deletemessagingSubscription")]
        public HttpResponseMessage DeleteMessagingSubscription(HttpRequestMessage request, [FromBody]int messagingSubscriptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MessagingSubscription messagingSubscription = _MPRCoreService.GetMessagingSubscription(messagingSubscriptionId);

                if (messagingSubscription != null)
                {
                    _MPRCoreService.DeleteMessagingSubscription(messagingSubscriptionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No messagingSubscription found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmessagingSubscription/{messagingSubscriptionId}")]
        public HttpResponseMessage GetMessagingSubscription(HttpRequestMessage request, int messagingSubscriptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MessagingSubscription messagingSubscription = _MPRCoreService.GetMessagingSubscription(messagingSubscriptionId);

                // notice no need to create a seperate model object since MessagingSubscription entity will do just fine
                response = request.CreateResponse<MessagingSubscription>(HttpStatusCode.OK, messagingSubscription);

                return response;
            });
        }


        [HttpGet]
        [Route("getmessagingSubscriptionRecipients/{recipients}")]
        public HttpResponseMessage GetMessagingSubscriptionByRecipients(HttpRequestMessage request, string recipients)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Revenue[] messagingSubscription = _MPRCoreService.GetMessagingSubscriptionByRecipients(recipients);

                // notice no need to create a seperate model object since MessagingSubscription entity will do just fine
                response = request.CreateResponse<Revenue[]>(HttpStatusCode.OK, messagingSubscription);

                return response;
            });
        }



        [HttpGet]
        [Route("rundate")]
        public HttpResponseMessage GetRecipents(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                DateTime[] Recipents = _MPRCoreService.GetRecipents();
                List<RecipentsModel> recipents = new List<RecipentsModel>();
                foreach (var c in Recipents)
                    recipents.Add(new RecipentsModel()
                    {
                        Rundate = c

                    });

                return request.CreateResponse<RecipentsModel[]>(HttpStatusCode.OK, recipents.ToArray());
            });
        }

        [HttpGet]
        [Route("report")]
        public HttpResponseMessage GetReports(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string[] Report = _MPRCoreService.GetReports();
                List<ReportModel> reports = new List<ReportModel>();
                foreach (var c in Report)
                    reports.Add(new ReportModel()
                    {
                        Description = c

                    });

                return request.CreateResponse<ReportModel[]>(HttpStatusCode.OK, reports.ToArray());
            });
        }

        //[HttpGet]
        //[Route("availablemessagingSubscriptions")]
        //public HttpResponseMessage GetAvailableMessagingSubscriptions(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        MessagingSubscriptionData[] messagingSubscriptions = _MPRCoreService.GetAllMessagingSubscriptions();

        //        return request.CreateResponse<MessagingSubscriptionData[]>(HttpStatusCode.OK, messagingSubscriptions);
        //    });
        //}

        //[HttpPost]
        //[Route("deleteselectedlist/{selectedIds}")]
        //public HttpResponseMessage DeleteLoanSetupIdList(string selectedIds)
        //{
        //    _MPRCoreService.DeleteSelectedIds(selectedIds);
        //    return Request.CreateResponse(HttpStatusCode.OK);

        //}
    }
}
