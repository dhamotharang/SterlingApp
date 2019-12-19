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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/bsinotherinformation")]
    [UsesDisposableService]
    public class BSINOtherInformationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BSINOtherInformationApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updatebsinotherinformation")]
        public HttpResponseMessage UpdateBSINOtherInformation(HttpRequestMessage request, [FromBody]BSINOtherInformation bsINOtherInformationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var bsINOtherInformation = _MPRBSService.UpdateBSINOtherInformation(bsINOtherInformationModel);

                return request.CreateResponse<BSINOtherInformation>(HttpStatusCode.OK, bsINOtherInformation);
            });
        }

        [HttpPost]
        [Route("deletebsinotherinformation")]
        public HttpResponseMessage DeleteBSINOtherInformation(HttpRequestMessage request, [FromBody]int bsINOtherInformationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                BSINOtherInformation bsINOtherInformation = _MPRBSService.GetBSINOtherInformation(bsINOtherInformationId);

                if (bsINOtherInformation != null)
                {
                    _MPRBSService.DeleteBSINOtherInformation(bsINOtherInformationId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No bsINOtherInformation found under that ID.");

                return response;
            });
        }






        //[HttpGet]
        //[Route("getbudgetbsINOtherInformation/{budgetBSINOtherInformationName}")]
        //public HttpResponseMessage GetBudgetBSINOtherInformationByName(HttpRequestMessage request, string budgetBSINOtherInformationName)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        BSINOtherInformation[] bsINOtherInformation = _MPRBSService.GetAllBudgetBSINOtherInformationsByCaptionName(budgetBSINOtherInformationName);

        //        // notice no need to create a seperate model object since BSINOtherInformation entity will do just fine
        //        response = request.CreateResponse<BSINOtherInformation[]>(HttpStatusCode.OK, bsINOtherInformation);

        //        return response;
        //    });
        //}



        //[HttpGet]
        //[Route("getmprbsINOtherInformation/{mprBSINOtherInformationName}")]
        //public HttpResponseMessage GetMPRBSINOtherInformationByName(HttpRequestMessage request, string mprBSINOtherInformationName)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        BSINOtherInformation[] bsINOtherInformation = _MPRBSService.GetAllMPRBSINOtherInformationsByCaptionName(mprBSINOtherInformationName);

        //        // notice no need to create a seperate model object since BSINOtherInformation entity will do just fine
        //        response = request.CreateResponse<BSINOtherInformation[]>(HttpStatusCode.OK, bsINOtherInformation);

        //        return response;
        //    });
        //}




        [HttpGet]
        [Route("getbsinotherinformation/{bsINOtherInformationId}")]
        public HttpResponseMessage GetBSINOtherInformation(HttpRequestMessage request, int bsINOtherInformationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BSINOtherInformation bsINOtherInformation = _MPRBSService.GetBSINOtherInformation(bsINOtherInformationId);

                // notice no need to create a seperate model object since BSINOtherInformation entity will do just fine
                response = request.CreateResponse<BSINOtherInformation>(HttpStatusCode.OK, bsINOtherInformation);

                return response;
            });
        }



        [HttpGet]
        [Route("availablebsinotherinformations")]
        public HttpResponseMessage GetAvailableBSINOtherInformations(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BSINOtherInformation[] bsINOtherInformations = _MPRBSService.GetAllBSINOtherInformations();

                return request.CreateResponse<BSINOtherInformation[]>(HttpStatusCode.OK, bsINOtherInformations);
            });
        }

        [HttpGet]
        [Route("availablebgetallbsplcaptions")]
        public HttpResponseMessage GetAvailableGetAllBsPlCaptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IEnumerable<BSCaption> bsPLCaption = _MPRBSService.GetAllBsPlCaptions();

                return request.CreateResponse<IEnumerable<BSCaption>>(HttpStatusCode.OK, bsPLCaption);
            });
        }

        //[HttpGet]
        //[Route("availablemprbsinotherinformations")]
        //public HttpResponseMessage GetAvailableMPRBSINOtherInformations(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        BSINOtherInformation[] bsINOtherInformations = _MPRBSService.GetAllMPRBSINOtherInformations();

        //        return request.CreateResponse<BSINOtherInformation[]>(HttpStatusCode.OK, bsINOtherInformations);
        //    });
        //}

        //[HttpGet]
        //[Route("availablebudgetbsinotherinformations")]
        //public HttpResponseMessage GetAvailableBudgetBSINOtherInformations(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        BSINOtherInformation[] bsINOtherInformations = _MPRBSService.GetAllBudgetBSINOtherInformations();

        //        return request.CreateResponse<BSINOtherInformation[]>(HttpStatusCode.OK, bsINOtherInformations);
        //    });
        //}



    }
}
