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
    [RoutePrefix("api/usermis")]
    [UsesDisposableService]
    public class UserMISApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public UserMISApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updateusermis")]
        public HttpResponseMessage UpdateUserMIS(HttpRequestMessage request, [FromBody]UserMIS teamusermisModel)
        {
            return GetHttpResponse(request, () =>
            {
                var teamusermis = _MPRCoreService.UpdateUserMIS(teamusermisModel);

                return request.CreateResponse<UserMIS>(HttpStatusCode.OK, teamusermis);
            });
        }

        [HttpPost]
        [Route("deleteuserMIS")]
        public HttpResponseMessage DeleteUserMIS(HttpRequestMessage request, [FromBody]int userMISId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                UserMIS userMIS = _MPRCoreService.GetUserMIS(userMISId);

                if (userMIS != null)
                {
                    _MPRCoreService.DeleteUserMIS(userMISId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No userMIS found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getuserMIS/{userMISId}")]
        public HttpResponseMessage GetUserMIS(HttpRequestMessage request, int userMISId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                UserMIS userMIS = _MPRCoreService.GetUserMIS(userMISId);

                // notice no need to create a seperate model object since UserMIS entity will do just fine
                response = request.CreateResponse<UserMIS>(HttpStatusCode.OK, userMIS);

                return response;
            });
        }

        [HttpGet]
        [Route("availableuserMISs")]
        public HttpResponseMessage GetAvailableUserMISs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                UserMIS[] userMISs = _MPRCoreService.GetAllUserMISs();

                return request.CreateResponse<UserMIS[]>(HttpStatusCode.OK, userMISs);
            });
        }

        [HttpGet]
        [Route("getusermisdetail/{userMISId}")]
        [Authorize]
        public HttpResponseMessage GetUserMISDetailInfo(HttpRequestMessage request, int userMISId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var model = new UserMISModel();

                model.UserMIS = _MPRCoreService.GetUserMIS(userMISId);

                var classificationTypes = _MPRCoreService.GetAllTeamClassificationTypes();
                var userClassifications = _MPRCoreService.GetAllUserClassificationMaps(model.UserMIS.LoginID);

                var userClassificationModels = new List<UserClassificationModel>();

                foreach (var classificationType in classificationTypes)
                {
                    var classifications = _MPRCoreService.GetAllTeamClassifications().Where(c => c.ClassificationTypeCode == classificationType.Code).ToArray();

                    var classificationCodes = classifications.Select(c => c.Code).Distinct();

                    var userClassification = userClassifications.Where(c => c.ClassificationTypeCode == classificationType.Code && c.LoginID == model.UserMIS.LoginID && classificationCodes.Contains(c.ClassificationCode)).FirstOrDefault();

                    userClassificationModels.Add(new UserClassificationModel()
                    {
                        UserClassificationMapId = userClassification == null ? 0 : userClassification.UserClassificationMapId,
                        LoginID = model.UserMIS.LoginID,
                        ClassificationCode = userClassification == null ? "" : userClassification.ClassificationCode,
                        ClassificationName = userClassification == null ? "" : userClassification.ClassificationCode,
                        Level = userClassification == null ? 0 : userClassification.Level,
                        ClassificationTypeCode = classificationType.Code,
                        ClassificationTypeName = classificationType.Name,
                        Classifications = classifications
                    });

                }

                model.Classifications = userClassificationModels.ToArray();


                response = request.CreateResponse<UserMISModel>(HttpStatusCode.OK, model);

                return response;
            });
        }

        [HttpPost]
        [Route("updateusermisdetail")]
        public HttpResponseMessage UpdateAccount(HttpRequestMessage request, [FromBody]UserMISModel_New userMisModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                UserMIS userMIS = _MPRCoreService.UpdateUserMIS(userMisModel.UserMIS);

                var existingMaps = _MPRCoreService.GetAllUserClassificationMaps(userMIS.LoginID);

                foreach (var map in existingMaps)
                {
                    _MPRCoreService.DeleteUserClassificationMap(map.UserClassificationMapId);
                }

                foreach (var map in userMisModel.Classifications)
                {
                    if (!string.IsNullOrEmpty(map.ClassificationCode))
                    {
                        var newMap = new UserClassificationMap()
                        {
                            LoginID = userMIS.LoginID,
                            ClassificationCode = map.ClassificationCode,
                            Level = map.Level,
                            ClassificationTypeCode = map.ClassificationTypeCode,
                            Active = true
                        };

                        _MPRCoreService.UpdateUserClassificationMap(newMap);

                    }
                }

                response = request.CreateResponse<UserMIS>(HttpStatusCode.OK, userMIS);

                return response;
            });
        }
    }
}
