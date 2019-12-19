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
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/consolidatedtrialbalance")]
    [UsesDisposableService]
    public class ConsolidatedTrialBalanceApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ConsolidatedTrialBalanceApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpGet]
        [Route("getconsolidatedtrialbalanceGAAP/{CompanyCode}")]
        public HttpResponseMessage GetAllTrialBalanceConsolidated(HttpRequestMessage request,string CompanyCode)
        {
            return GetHttpResponse(request, () =>
            {
                var item = new ConsolidatedTrialBalanceGapModel();
               
                decimal obsGL;
                decimal ifrsGL;
                decimal bsGL;
                TrialBalanceConsolidated[] trialbalanceconsolidated = _FinstatService.GetAllTrialBalanceConsolidated(CompanyCode);
                decimal totalbal = 0;
                foreach (var tbal in trialbalanceconsolidated)
                {
                    totalbal = totalbal + tbal.LCY_Balance;
                    if (tbal.GLCode == "SBNSBGL.9999O")
                    {
                        obsGL = tbal.LCY_Balance;
                        item.OBSAutoBalance = obsGL;
                    }
                    else if (tbal.GLCode == "SBNSBGLIF.7999")
                    {
                        ifrsGL = tbal.LCY_Balance;
                        item.IFRSAutoBalance = ifrsGL;
                    }
                    else if (tbal.GLCode == "SBNSBGL.8999")
                    {
                        bsGL = tbal.LCY_Balance;
                        item.BSAutoBalance = bsGL;
                    }
                }
                item.TrialBalance = trialbalanceconsolidated;
                item.TranslatedBalance = totalbal;

                return request.CreateResponse<ConsolidatedTrialBalanceGapModel>(HttpStatusCode.OK, item);
            });
        }

        [HttpGet]
        [Route("getconsolidatedtrialbalanceIFRS/{CompanyCode}")]
        public HttpResponseMessage GetAllTrialBalanceConsolidatedIFRS(HttpRequestMessage request,string CompanyCode)
        {
            return GetHttpResponse(request, () =>
            {
                var item = new ConsolidatedTrialBalanceModel();
                decimal obsGL;
                decimal ifrsGL;
                decimal bsGL;
                TrialBalanceConsolidatedIFRS[] trialbalanceconsolidatedifrs = _FinstatService.GetAllTrialBalanceConsolidatedIFRS(CompanyCode);
                decimal totalbal = 0;
                foreach (var tbal in trialbalanceconsolidatedifrs)
                {
                    totalbal = totalbal + tbal.LCY_Balance;
                    if (tbal.GLCode == "SBNSBGL.9999O")
                    {
                        obsGL = tbal.LCY_Balance;
                        item.OBSAutoBalance = obsGL;
                    }
                    else if (tbal.GLCode == "SBNSBGLIF.7999")
                    {
                        ifrsGL = tbal.LCY_Balance;
                        item.IFRSAutoBalance = ifrsGL;
                    }
                    else if (tbal.GLCode == "SBNSBGL.8999")
                    {
                        bsGL = tbal.LCY_Balance;
                        item.BSAutoBalance = bsGL;
                    }
                }
                item.TrialBalance = trialbalanceconsolidatedifrs;
                item.TranslatedBalance = totalbal;

                return request.CreateResponse<ConsolidatedTrialBalanceModel>(HttpStatusCode.OK, item);
            });
        }

    }
}
