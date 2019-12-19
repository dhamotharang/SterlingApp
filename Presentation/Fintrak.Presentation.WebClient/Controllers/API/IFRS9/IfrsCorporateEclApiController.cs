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
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/ifrscorporateecl")]
    [UsesDisposableService]
    public class IfrsCorporateEclApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsCorporateEclApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateIfrsCorporateEcl")]
        public HttpResponseMessage UpdateIfrsCorporateEcl(HttpRequestMessage request, [FromBody]IfrsCorporateEcl IfrsCorporateEclModel)
        {
            return GetHttpResponse(request, () =>
            {
                var IfrsCorporateEcl = _IFRS9Service.UpdateIfrsCorporateEcl(IfrsCorporateEclModel);

                return request.CreateResponse<IfrsCorporateEcl>(HttpStatusCode.OK, IfrsCorporateEcl);
            });
        }

        [HttpPost]
        [Route("deleteIfrsCorporateEcl")]
        public HttpResponseMessage DeleteIfrsCorporateEcl(HttpRequestMessage request, [FromBody]int IfrsCorporateEclId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsCorporateEcl IfrsCorporateEcl = _IFRS9Service.GetIfrsCorporateEclById(IfrsCorporateEclId);

                if (IfrsCorporateEcl != null)
                {
                    _IFRS9Service.DeleteIfrsCorporateEcl(IfrsCorporateEclId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IfrsCorporateEcl found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getIfrsCorporateEclByRefNo/{refNo}")]
        public HttpResponseMessage GetIfrsCorporateEclByRefNo(HttpRequestMessage request, string refNo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsCorporateEcl[] IfrsCorporateEcl = _IFRS9Service.GetIfrsCorporateEclByRefNo(refNo);

                // notice no need to create a seperate model object since IfrsCorporateEcl entity will do just fine
                response = request.CreateResponse<IfrsCorporateEcl[]>(HttpStatusCode.OK, IfrsCorporateEcl);

                return response;
            });
        }

        [HttpGet]
        [Route("getAllIfrsCorporateEcls")]
        public HttpResponseMessage GetAvailableIfrsCorporateEcls(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                bool export = false;
                IfrsCorporateEcl[] IfrsCorporateEcls = _IFRS9Service.GetAllIfrsCorporateEcls(export);

                return request.CreateResponse<IfrsCorporateEcl[]>(HttpStatusCode.OK, IfrsCorporateEcls);
            });
        }

        //[HttpGet]
        //[Route("exportAllIfrsCorporateEcls")]
        //public HttpResponseMessage ExportAllIfrsCorporateEcls(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        var output = new ExcelService();
        //        bool export = true;

        //        IfrsCorporateEcl[] IfrsCorporateEcls = _IFRS9Service.GetAllIfrsCorporateEcls(export);
        //        var content = output.Export<IfrsCorporateEcl>(IfrsCorporateEcls.ToList());

        //        var response = new HttpResponseMessage(HttpStatusCode.OK)
        //        {
        //            //Content = new ByteArrayContent(content.ToArray())
        //            Content = new StreamContent(content)
        //        };

        //        response = request.CreateResponse<IfrsCorporateEcl[]>(HttpStatusCode.OK, IfrsCorporateEcls);
        //        response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        //        {
        //            FileName = "data.xls"
        //        };
        //        response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.ms-excel");
        //        return response;
        //    });
        //}
    }
}
