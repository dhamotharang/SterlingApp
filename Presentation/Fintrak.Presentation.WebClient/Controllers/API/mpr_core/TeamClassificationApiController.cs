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
using Fintrak.Shared.Common.Extensions;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/teamclassification")]
    [UsesDisposableService]
    public class TeamClassificationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TeamClassificationApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updateteamclassification")]
        public HttpResponseMessage UpdateTeamClassification(HttpRequestMessage request, [FromBody]TeamClassification teamteamclassificationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var teamteamclassification = _MPRCoreService.UpdateTeamClassification(teamteamclassificationModel);

                return request.CreateResponse<TeamClassification>(HttpStatusCode.OK, teamteamclassification);
            });
        }

        [HttpPost]
        [Route("deleteteamClassification")]
        public HttpResponseMessage DeleteTeamClassification(HttpRequestMessage request, [FromBody]int teamClassificationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                TeamClassification teamClassification = _MPRCoreService.GetTeamClassification(teamClassificationId);

                if (teamClassification != null)
                {
                    _MPRCoreService.DeleteTeamClassification(teamClassificationId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No teamClassification found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getteamClassification/{teamClassificationId}")]
        public HttpResponseMessage GetTeamClassification(HttpRequestMessage request, int teamClassificationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TeamClassification teamClassification = _MPRCoreService.GetTeamClassification(teamClassificationId);

                // notice no need to create a seperate model object since TeamClassification entity will do just fine
                response = request.CreateResponse<TeamClassification>(HttpStatusCode.OK, teamClassification);

                return response;
            });
        }

        [HttpGet]
        [Route("availableteamClassifications")]
        public HttpResponseMessage GetAvailableTeamClassifications(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TeamClassification[] teamClassifications = _MPRCoreService.GetAllTeamClassifications();

                return request.CreateResponse<TeamClassification[]>(HttpStatusCode.OK, teamClassifications);
            });
        }

        [HttpGet]
        [Route("getClassifications/{typeCode}")]
        public HttpResponseMessage GetClassifications(HttpRequestMessage request,string typeCode)
        {
            return GetHttpResponse(request, () =>
            {
                TeamClassification[] teamClassifications = _MPRCoreService.GetTeamClassifications(typeCode);

                return request.CreateResponse<TeamClassification[]>(HttpStatusCode.OK, teamClassifications);
            });
        }

        [HttpGet]
        [Route("getActiveClassifications/{typeCode}")]
        public HttpResponseMessage GetActiveClassifications(HttpRequestMessage request, string typeCode)
        {
            return GetHttpResponse(request, () =>
            {
                TeamClassification[] classifications = _MPRCoreService.GetAllTeamClassifications().Where (c=>c.ClassificationTypeCode == typeCode ).ToArray();

                var classificationType = _MPRCoreService.GetAllTeamClassificationTypes().Where(c => c.Code == typeCode).FirstOrDefault();

                var maxPosition = classificationType.MaximumLevel;

                IEnumerable<ClassificationDeepModel> nodes = classifications.RecursiveJoin(element => element.Code, element => element.ParentCode,
                (TeamClassification element, int index, int depth, IEnumerable<ClassificationDeepModel> children) =>
                {
                    return new ClassificationDeepModel()
                    {
                        ClassificationId = element.TeamClassificationId,
                        Code = element.Code,
                        Name = element.Name,
                        ParentCode = element.ParentCode,
                        ParentName = "",
                        TypeCode = element.ClassificationTypeCode,
                        Level = element.Level,
                        Children = children,
                        Depth = element.Level 
                    };
                });

                var selectedClassifications = new List<ClassificationDeepModel>();

                var roots = nodes.Where(c => c.Level == 1);
                foreach (var model in roots)
                {
                    if (model.Children.Count() > 0)
                    {
                        selectedClassifications.AddRange(GetClassificationRepresentatives(model));
                    }
                    else
                    {
                        selectedClassifications.Add(model);
                    }
                }

                return request.CreateResponse<ClassificationDeepModel[]>(HttpStatusCode.OK, selectedClassifications.ToArray());
            });
        }

        private ClassificationDeepModel[] GetClassificationRepresentatives(ClassificationDeepModel model)
        {
            var selectedClassifications = new List<ClassificationDeepModel>();

            foreach (var child in model.Children )
            {
                if (child.Children.Count() > 0)
                {
                    selectedClassifications.AddRange(GetClassificationRepresentatives(child));
                }
                else
                {
                    selectedClassifications.Add(child);
                }
            }

            return selectedClassifications.ToArray();
        }
    }
}
