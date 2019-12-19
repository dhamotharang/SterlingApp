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
    [RoutePrefix("api/individualimpairment")]
    [UsesDisposableService]
    public class IndividualImpairmentApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IndividualImpairmentApiController(IIFRSLoanService loanService)
        {
            _LoanService = loanService;
        }

        IIFRSLoanService _LoanService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_LoanService);
        }

        [HttpPost]
        [Route("updateindividualimpairment")]
        public HttpResponseMessage UpdateIndividualImpairment(HttpRequestMessage request, [FromBody]IndividualImpairment individualImpairmentModel)
        {
            return GetHttpResponse(request, () =>
            {
                var individualImpairment = _LoanService.UpdateIndividualImpairment(individualImpairmentModel);

                return request.CreateResponse<IndividualImpairment>(HttpStatusCode.OK, individualImpairment);
            });
        }

        [HttpPost]
        [Route("deleteindividualImpairment")]
        public HttpResponseMessage DeleteIndividualImpairment(HttpRequestMessage request, [FromBody]int individualImpairmentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IndividualImpairment individualImpairment = _LoanService.GetIndividualImpairment(individualImpairmentId);

                if (individualImpairment != null)
                {
                    _LoanService.DeleteIndividualImpairment(individualImpairmentId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No individualImpairment found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getindividualImpairment/{individualImpairmentId}")]
        public HttpResponseMessage GetIndividualImpairment(HttpRequestMessage request, int individualImpairmentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IndividualImpairment individualImpairment = _LoanService.GetIndividualImpairment(individualImpairmentId);

                // notice no need to create a seperate model object since IndividualImpairment entity will do just fine
                response = request.CreateResponse<IndividualImpairment>(HttpStatusCode.OK, individualImpairment);

                return response;
            });
        }


        [HttpGet]
        [Route("availableindividualimpairments")]
        public HttpResponseMessage GetAvailablelndividualImpairments(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IndividualImpairmentData[] individualImpairments = _LoanService.GetAlllndividualImpairments();

                return request.CreateResponse<IndividualImpairmentData[]>(HttpStatusCode.OK, individualImpairments);
            });
        }


        [HttpGet]
        [Route("availablereferenceNumbers")]
        public HttpResponseMessage GetAvailableReferenceNumbers(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string[] refNos = _LoanService.GetAvailableReferenceNumbers();

                var model = new List<KeyValueData>();

                foreach (var s in refNos)
                    model.Add(new KeyValueData() { Key = s, Value = s });

                return request.CreateResponse<KeyValueData[]>(HttpStatusCode.OK, model.ToArray());
               
            });
        }


        [HttpGet]
        [Route("getindividualimpairmentbyrefno/{refno}")]
        public HttpResponseMessage GetIndividualImpairments(HttpRequestMessage request, string refno)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                //IndividualScheduleData[] individualschedule = _LoanService.GetIndividualImpairments(refno);

                IndividualImpairmentData[] individualImpairment = _LoanService.GetIndividualImpairments(refno);

                // notice no need to create a seperate model object since BondComputation entity will do just fine
                response = request.CreateResponse<IndividualImpairmentData[]>(HttpStatusCode.OK, individualImpairment);

                return response;
            });
        }

    }
}