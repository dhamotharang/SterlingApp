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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/individualschedule")]
    [UsesDisposableService]
    public class IndividualScheduleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IndividualScheduleApiController(IIFRSLoanService loanService)
        {
            _LoanService = loanService;
        }

        IIFRSLoanService _LoanService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_LoanService);
        }

        [HttpPost]
        [Route("updateindividualschedule")]
        public HttpResponseMessage UpdateIndividualSchedule(HttpRequestMessage request, [FromBody]IndividualSchedule individualScheduleModel)
        {
            return GetHttpResponse(request, () =>
            {
                var individualSchedule = _LoanService.UpdateIndividualSchedule(individualScheduleModel);

                return request.CreateResponse<IndividualSchedule>(HttpStatusCode.OK, individualSchedule);
            });
        }

        [HttpPost]
        [Route("deleteindividualschedule")]
        public HttpResponseMessage DeleteIndividualSchedule(HttpRequestMessage request, [FromBody]int individualScheduleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IndividualSchedule individualSchedule = _LoanService.GetIndividualSchedule(individualScheduleId);

                if (individualSchedule != null)
                {
                    _LoanService.DeleteIndividualSchedule(individualScheduleId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No individualschedule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getindividualschedule/{individualscheduleId}")]
        public HttpResponseMessage GetIndividualSchedule(HttpRequestMessage request,int individualScheduleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IndividualSchedule individualSchedule = _LoanService.GetIndividualSchedule(individualScheduleId);

                // notice no need to create a seperate model object since IndividualSchedule entity will do just fine
                response = request.CreateResponse<IndividualSchedule>(HttpStatusCode.OK, individualSchedule);

                return response;
            });
        }

        [HttpGet]
        [Route("availableindividualschedules")]
        public HttpResponseMessage GetAvailableIndividualSchedules(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IndividualScheduleData[] individualSchedules = _LoanService.GetAllIndividualSchedules();

                return request.CreateResponse<IndividualScheduleData[]>(HttpStatusCode.OK, individualSchedules);
            });
        }

        [HttpGet]
        [Route("getdistinctrefnos")]
        public HttpResponseMessage GetDistinctRefnos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string[] refNos = _LoanService.GetDistinctRefNo();

                var model = new List<KeyValueData>();

                foreach (var s in refNos)
                    model.Add(new KeyValueData() { Key=s,Value = s });

                return request.CreateResponse<KeyValueData[]>(HttpStatusCode.OK, model.ToArray());
            });
        }

        [HttpGet]
        [Route("getindividualschedulebyrefno/{refno}")]
        public HttpResponseMessage GetIndividualschedulebyrefno(HttpRequestMessage request, string refno)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;


                IndividualScheduleData[] individualschedule = _LoanService.GetIndividualSchedulebyRefNo(refno);

                response = request.CreateResponse<IndividualScheduleData[]>(HttpStatusCode.OK, individualschedule);

                return response;
                //var model = new List<IndividualKeyData>();

                //foreach (var s in individualschedule)
                //    model.Add(new IndividualKeyData() { RefNo = s.RefNo, FeeAmount = s.FeeAmount, Amount = s.Amount,AmountPrinEnd=s.AmountPrinEnd,Processed=s.Processed,ValueDate=s.ValueDate,MaturityDate=s.MaturityDate,IRR=s.IRR });

                //return request.CreateResponse<IndividualKeyData[]>(HttpStatusCode.OK, model.ToArray());

             //   return response;
            });
        }
    }
}
