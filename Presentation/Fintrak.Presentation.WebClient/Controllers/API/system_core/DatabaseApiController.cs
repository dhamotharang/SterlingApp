using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Client.SystemCore.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/database")]
    [UsesDisposableService]
    public class DatabaseApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public DatabaseApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatedatabase")]
        public HttpResponseMessage UpdateDatabase(HttpRequestMessage request, [FromBody]Database databaseModel)
        {
            return GetHttpResponse(request, () =>
            {
                var database = _CoreService.UpdateDatabase(databaseModel);

                return request.CreateResponse<Database>(HttpStatusCode.OK, database);
            });
        }

        [HttpPost]
        [Route("deletedatabase")]
        public HttpResponseMessage DeleteDatabase(HttpRequestMessage request, [FromBody]int databaseId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Database database = _CoreService.GetDatabase(databaseId);

                if (database != null)
                {
                    _CoreService.DeleteDatabase(databaseId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No database found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getdatabase/{databaseId}")]
        public HttpResponseMessage GetDatabase(HttpRequestMessage request, int databaseId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Database database = _CoreService.GetDatabase(databaseId);

                // notice no need to create a seperate model object since Database entity will do just fine
                response = request.CreateResponse<Database>(HttpStatusCode.OK, database);

                return response;
            });
        }

        [HttpGet]
        [Route("availabledatabase")]
        public HttpResponseMessage GetAvailableDatabases(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                DatabaseData[] database = _CoreService.GetAllDatabases();

                return request.CreateResponse<DatabaseData[]>(HttpStatusCode.OK, database);
            });
        }
    }
}
