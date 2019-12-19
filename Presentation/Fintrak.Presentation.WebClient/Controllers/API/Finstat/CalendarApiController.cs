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
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/calendar")]
    [UsesDisposableService]
    public class CalendarApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CalendarApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpPost]
        [Route("updatecalendar")]
        public HttpResponseMessage UpdateCalendar(HttpRequestMessage request, [FromBody]Calendar calendarModel)
        {
            return GetHttpResponse(request, () =>
            {
                var calendar = _FinstatService.UpdateCalendar(calendarModel);

                return request.CreateResponse<Calendar>(HttpStatusCode.OK, calendar);
            });
        }

        [HttpPost]
        [Route("deletecalendar")]
        public HttpResponseMessage DeleteCalendar(HttpRequestMessage request, [FromBody]int CalId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Calendar calendar = _FinstatService.GetCalendar(CalId);

                if (calendar != null)
                {
                    _FinstatService.DeleteCalendar(CalId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Calendar found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcalendar/{CalId}")]
        public HttpResponseMessage GetCalendar(HttpRequestMessage request,int CalId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Calendar calendar = _FinstatService.GetCalendar(CalId);

                // notice no need to create a seperate model object since Calendar entity will do just fine
                response = request.CreateResponse<Calendar>(HttpStatusCode.OK, calendar);

                return response;
            });
        }

        [HttpGet]
        [Route("getcalendarexception/{runDate}")]
        public HttpResponseMessage GetCalendarException(HttpRequestMessage request, DateTime runDate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Calendar[] calendar = _FinstatService.GetCalendarException(runDate);

                // notice no need to create a seperate model object since Calendar entity will do just fine
                response = request.CreateResponse<Calendar[]>(HttpStatusCode.OK, calendar.ToArray());

                return response;
            });
        }

        [HttpGet]
        [Route("availablecalendars")]
        public HttpResponseMessage GetAvailableCalendars(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Calendar[] calendars = _FinstatService.GetAllCalendars();

                return request.CreateResponse<Calendar[]>(HttpStatusCode.OK, calendars);
            });
        }
    }
}
