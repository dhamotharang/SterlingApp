using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/macroeconomicvdisplay")]
    [UsesDisposableService]
    public class MacroeconomicVDisplayApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MacroeconomicVDisplayApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemacroeconomicVDisplay")]
        public HttpResponseMessage UpdateMacroeconomicVDisplay(HttpRequestMessage request, [FromBody]MacroeconomicVDisplay macroeconomicVDisplayModel)
        {
            return GetHttpResponse(request, () =>
            {
                var macroeconomicVDisplay = _IFRS9Service.UpdateMacroeconomicVDisplay(macroeconomicVDisplayModel);

                return request.CreateResponse<MacroeconomicVDisplay>(HttpStatusCode.OK, macroeconomicVDisplay);
            });
        }

        [HttpPost]
        [Route("deletemacroeconomicVDisplay")]
        public HttpResponseMessage DeleteMacroeconomicVDisplay(HttpRequestMessage request, [FromBody]int macroeconomicVDisplayId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MacroeconomicVDisplay macroeconomicVDisplay = _IFRS9Service.GetMacroeconomicVDisplay(macroeconomicVDisplayId);

                if (macroeconomicVDisplay != null)
                {
                    _IFRS9Service.DeleteMacroeconomicVDisplay(macroeconomicVDisplayId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No macroeconomicVDisplay found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmacroeconomicvdisplay/{macroeconomicVDisplayId}")]
        public HttpResponseMessage GetMacroeconomicVDisplay(HttpRequestMessage request,int macroeconomicVDisplayId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MacroeconomicVDisplay macroeconomicVDisplay = _IFRS9Service.GetMacroeconomicVDisplay(macroeconomicVDisplayId);

                // notice no need to create a seperate model object since MacroeconomicVDisplay entity will do just fine
                response = request.CreateResponse<MacroeconomicVDisplay>(HttpStatusCode.OK, macroeconomicVDisplay);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemacroeconomicVDisplays")]
        public HttpResponseMessage GetAvailableMacroeconomicVDisplays(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MacroeconomicVDisplay[] macroeconomicVDisplays = _IFRS9Service.GetAllMacroeconomicVDisplays();

                return request.CreateResponse<MacroeconomicVDisplay[]>(HttpStatusCode.OK, macroeconomicVDisplays);
            });
        }

        [HttpGet]
        [Route("getyears/{vType}")]
        public HttpResponseMessage GetYears(HttpRequestMessage request,string vType)
        {
            return GetHttpResponse(request, () =>
            {
                string[] years = _IFRS9Service.GetDistinctFHYear(vType);
                List<ReferenceNoModel> cYears = new List<ReferenceNoModel>();
                foreach (var c in years)
                    cYears.Add(new ReferenceNoModel()
                    {
                        RefNo = c

                    });

                return request.CreateResponse<ReferenceNoModel[]>(HttpStatusCode.OK, cYears.ToArray());
            });
        }

        [HttpGet]
        [Route("getmacroevarbyyear/{yr}")]
        public HttpResponseMessage GetMacroeconomicVDisplayByYear(HttpRequestMessage request, int yr)
        {
            return GetHttpResponse(request, () =>
            {
                MacroeconomicVDisplay[] macroeconomicVDisplays = _IFRS9Service.GetMacroeconomicVDisplayByYear(yr);

                return request.CreateResponse<MacroeconomicVDisplay[]>(HttpStatusCode.OK, macroeconomicVDisplays);
            });
        }

        
    }
}
