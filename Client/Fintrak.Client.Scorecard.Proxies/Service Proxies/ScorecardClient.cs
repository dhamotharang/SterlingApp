using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Scorecard.Contracts;
using Fintrak.Client.Scorecard.Entities;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Scorecard.Proxies
{
    [Export(typeof(IScorecardService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ScorecardClient : UserClientBase<IScorecardService>, IScorecardService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region SCDActual

        public SCDActual UpdateSCDActual(SCDActual scdActual)
        {
            return Channel.UpdateSCDActual(scdActual);
        }

        public void DeleteSCDActual(int actualId)
        {
            Channel.DeleteSCDActual(actualId);
        }

        public SCDActual GetSCDActual(int actualId)
        {
            return Channel.GetSCDActual(actualId);
        }

        public SCDActual[] GetAllSCDActuals()
        {
            return Channel.GetAllSCDActuals();
        }

        public SCDActual[] GetCaption()
        {
            return Channel.GetCaption();
        }


        #endregion

        #region SCDCategory

        public SCDCategory UpdateSCDCategory(SCDCategory scdCategory)
        {
            return Channel.UpdateSCDCategory(scdCategory);
        }

        public void DeleteSCDCategory(int categoryId)
        {
            Channel.DeleteSCDCategory(categoryId);
        }

        public SCDCategory GetSCDCategory(int categoryId)
        {
            return Channel.GetSCDCategory(categoryId);
        }

        public SCDCategoryData[] GetAllSCDCategorys()
        {
            return Channel.GetAllSCDCategorys();
        }

        #endregion

        #region SCDKPIClassification

        public SCDKPIClassification UpdateSCDKPIClassification(SCDKPIClassification scdClassification)
        {
            return Channel.UpdateSCDKPIClassification(scdClassification);
        }

        public void DeleteSCDKPIClassification(int classificationId)
        {
            Channel.DeleteSCDKPIClassification(classificationId);
        }

        public SCDKPIClassification GetSCDKPIClassification(int classificationId)
        {
            return Channel.GetSCDKPIClassification(classificationId);
        }

        public SCDKPIClassificationData[] GetAllSCDKPIClassifications()
        {
            return Channel.GetAllSCDKPIClassifications();
        }

        #endregion

        #region SCDConfiguration

        public SCDConfiguration UpdateSCDConfiguration(SCDConfiguration scdConfiguration)
        {
            return Channel.UpdateSCDConfiguration(scdConfiguration);
        }

        public SCDConfiguration GetFirstSetup()
        {
            return Channel.GetFirstSetup();
        }

        #endregion

        #region SCDKPI

        public SCDKPI UpdateSCDKPI(SCDKPI scdKPI)
        {
            return Channel.UpdateSCDKPI(scdKPI);
        }

        public void DeleteSCDKPI(int kpiId)
        {
            Channel.DeleteSCDKPI(kpiId);
        }

        public SCDKPI GetSCDKPI(int kpiId)
        {
            return Channel.GetSCDKPI(kpiId);
        }

        public SCDKPIData[] GetAllSCDKPIs()
        {
            return Channel.GetAllSCDKPIs();
        }

        #endregion

        #region SCDKPIActualMap

        public SCDKPIActualMap UpdateSCDKPIActualMap(SCDKPIActualMap scdKPIActualMap)
        {
            return Channel.UpdateSCDKPIActualMap(scdKPIActualMap);
        }

        public void DeleteSCDKPIActualMap(int mapId)
        {
            Channel.DeleteSCDKPIActualMap(mapId);
        }

        public SCDKPIActualMap GetSCDKPIActualMap(int mapId)
        {
            return Channel.GetSCDKPIActualMap(mapId);
        }


        //public SCDKPIActualMap[] GetAllSCDKPIActualMaps()
        //{
        //    return Channel.GetAllSCDKPIActualMaps();
        //}

        public SCDKPIActualMapData[] GetAllSCDKPIActualMaps()
        {
            return Channel.GetAllSCDKPIActualMaps();
        }

        #endregion

        #region SCDKPIEntry

        public SCDKPIEntry UpdateSCDKPIEntry(SCDKPIEntry scdKPIEntry)
        {
            return Channel.UpdateSCDKPIEntry(scdKPIEntry);
        }

        public void DeleteSCDKPIEntry(int entryId)
        {
            Channel.DeleteSCDKPIEntry(entryId);
        }

        public SCDKPIEntry GetSCDKPIEntry(int entryId)
        {
            return Channel.GetSCDKPIEntry(entryId);
        }

        public SCDKPIEntryData[] GetAllSCDKPIEntrys()
        {
            return Channel.GetAllSCDKPIEntrys();
        }

        #endregion

        #region SCDKPITargetMap

        public SCDKPITargetMap UpdateSCDKPITargetMap(SCDKPITargetMap scdKPITargetMap)
        {
            return Channel.UpdateSCDKPITargetMap(scdKPITargetMap);
        }

        public void DeleteSCDKPITargetMap(int mapId)
        {
            Channel.DeleteSCDKPITargetMap(mapId);
        }

        public SCDKPITargetMap GetSCDKPITargetMap(int mapId)
        {
            return Channel.GetSCDKPITargetMap(mapId);
        }


        //public SCDKPITargetMap[] GetAllSCDKPITargetMaps()
        //{
        //    return Channel.GetAllSCDKPITargetMaps();
        //}

        public SCDKPITargetMapData[] GetAllSCDKPITargetMaps()
        {
            return Channel.GetAllSCDKPITargetMaps();
        }


        #endregion

        #region SCDParticipant

        public SCDParticipant UpdateSCDParticipant(SCDParticipant scdParticipant)
        {
            return Channel.UpdateSCDParticipant(scdParticipant);
        }

        public void DeleteSCDParticipant(int participantId)
        {
            Channel.DeleteSCDParticipant(participantId);
        }

        public SCDParticipant GetSCDParticipant(int participantId)
        {
            return Channel.GetSCDParticipant(participantId);
        }

        public SCDParticipantData[] GetAllSCDParticipants()
        {
            return Channel.GetAllSCDParticipants();
        }

        #endregion

        #region SCDTarget

        public SCDTarget UpdateSCDTarget(SCDTarget scdTarget)
        {
            return Channel.UpdateSCDTarget(scdTarget);
        }

        public void DeleteSCDTarget(int targetId)
        {
            Channel.DeleteSCDTarget(targetId);
        }

        public SCDTarget GetSCDTarget(int targetId)
        {
            return Channel.GetSCDTarget(targetId);
        }

        public SCDTarget[] GetAllSCDTargets()
        {
            return Channel.GetAllSCDTargets();
        }

        public SCDTarget[] GetCaptions()
        {
            return Channel.GetCaptions();
        }

        #endregion

        #region SCDTeamMap

        public SCDTeamMap UpdateSCDTeamMap(SCDTeamMap scdTeamMap)
        {
            return Channel.UpdateSCDTeamMap(scdTeamMap);
        }

        public void DeleteSCDTeamMap(int teamMapId)
        {
            Channel.DeleteSCDTeamMap(teamMapId);
        }

        public SCDTeamMap GetSCDTeamMap(int teamMapId)
        {
            return Channel.GetSCDTeamMap(teamMapId);
        }

        public SCDTeamMapData[] GetAllSCDTeamMaps()
        {
            return Channel.GetAllSCDTeamMaps();
        }

        #endregion

        #region SCDThreshold

        public SCDThreshold UpdateSCDThreshold(SCDThreshold scdThreshold)
        {
            return Channel.UpdateSCDThreshold(scdThreshold);
        }

        public void DeleteSCDThreshold(int thresholdId)
        {
            Channel.DeleteSCDThreshold(thresholdId);
        }

        public SCDThreshold GetSCDThreshold(int thresholdId)
        {
            return Channel.GetSCDThreshold(thresholdId);
        }

        public SCDThresholdData[] GetAllSCDThresholds()
        {
            return Channel.GetAllSCDThresholds();
        }

        #endregion

        #region SCDTeamClassification

        public SCDTeamClassification UpdateSCDTeamClassification(SCDTeamClassification scdTeamClassification)
        {
            return Channel.UpdateSCDTeamClassification(scdTeamClassification);
        }

        public void DeleteSCDTeamClassification(int teamClassificationId)
        {
            Channel.DeleteSCDTeamClassification(teamClassificationId);
        }

        public SCDTeamClassification GetSCDTeamClassification(int teamClassificationId)
        {
            return Channel.GetSCDTeamClassification(teamClassificationId);
        }


        public SCDTeamClassification[] GetAllSCDTeamClassifications()
        {
            return Channel.GetAllSCDTeamClassifications();
        }

        #endregion

    }
}
