using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.IFRS.Proxies
{
    [Export(typeof(IIFRSCoreService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSCoreClient : UserClientBase<IIFRSCoreService>, IIFRSCoreService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region IFRSRegistry

        public IFRSRegistry UpdateIFRSRegistry(IFRSRegistry ifrsRegistry)
        {
            return Channel.UpdateIFRSRegistry(ifrsRegistry);
        }

        public void DeleteIFRSRegistry(int ifrsRegistryId)
        {
            Channel.DeleteIFRSRegistry(ifrsRegistryId);
        }

        public IFRSRegistry GetIFRSRegistry(int ifrsRegistryId)
        {
            return Channel.GetIFRSRegistry(ifrsRegistryId);
        }

        public IFRSRegistryData[] GetAllIFRSRegistries(int flag)
        {
            return Channel.GetAllIFRSRegistries(flag);
        }

        public string[] GetDistinctRefNotes()
        {
            return Channel.GetDistinctRefNotes();
        }

        #endregion

        #region DerivedCaption

        public DerivedCaption UpdateDerivedCaption(DerivedCaption derivedCaption)
        {
            return Channel.UpdateDerivedCaption(derivedCaption);
        }

        public void DeleteDerivedCaption(int derivedCaptionId)
        {
            Channel.DeleteDerivedCaption(derivedCaptionId);
        }

        public DerivedCaption GetDerivedCaption(int derivedCaptionId)
        {
            return Channel.GetDerivedCaption(derivedCaptionId);
        }

        public DerivedCaption[] GetAllDerivedCaptions()
        {
            return Channel.GetAllDerivedCaptions();
        }


        #endregion

        #region QualitativeNote
        public void DeleteQualitativeNote(int qualitativeNoteId)
        {
            Channel.DeleteQualitativeNote(qualitativeNoteId);
        }

        public QualitativeNote GetQualitativeNote(int qualitativeNoteId)
        {
            return Channel.GetQualitativeNote(qualitativeNoteId);
        }

        public QualitativeNote[] GetAllQualitativeNotes()
        {
            return Channel.GetAllQualitativeNotes();
        }

        public void UpdateQualitativeNote(string refNote, string topNote, string bottomNote, DateTime runDate,int reportTyp)
        {
            Channel.UpdateQualitativeNote(refNote, topNote, bottomNote, runDate, reportTyp);
        }

        #endregion

        #region InterimQualitativeNote
        public void DeleteInterimQualitativeNote(int qualitativeNoteId)
        {
            Channel.DeleteInterimQualitativeNote(qualitativeNoteId);
        }

        public InterimQualitativeNote GetInterimQualitativeNote(int qualitativeNoteId)
        {
            return Channel.GetInterimQualitativeNote(qualitativeNoteId);
        }

        public InterimQualitativeNote[] GetInterimQualitativeNoteByType(int rType)
        {
            return Channel.GetInterimQualitativeNoteByType(rType);
        }

        public InterimQualitativeNote[] GetAllInterimQualitativeNotes()
        {
            return Channel.GetAllInterimQualitativeNotes();
        }

        public void UpdateInterimQualitativeNote(string report, string topNote, string bottomNote, DateTime runDate, int reportType)
        {
            Channel.UpdateInterimQualitativeNote(report, topNote, bottomNote, runDate, reportType);
        }


        public string[] GetReportNamesbyType(int rType)
        {
            return Channel.GetReportNamesbyType(rType);
        }
        #endregion

        #region IFRSReportPackViewer
        public IFRSReportPack[] GetAllIFRSReportPacks()
        {
            return Channel.GetAllIFRSReportPacks();
        }

        public IFRSReport[] GetAllRunDates()
        {
            return Channel.GetAllRunDates();
        }

       public string ReturnReportUrl(string reportName,DateTime runDate)
        {
            return Channel.ReturnReportUrl(reportName, runDate);
        }

        #endregion

        #region IFRS_Revacct_Registry

       public IFRSRevacctRegistry UpdateIFRSRevacctRegistry(IFRSRevacctRegistry iFRSRevacctRegistry)
       {
           return Channel.UpdateIFRSRevacctRegistry(iFRSRevacctRegistry);
       }

       public void DeleteIFRSRevacctRegistry(int ifrs_Revacct_RegistryId)
       {
           Channel.DeleteIFRSRevacctRegistry(ifrs_Revacct_RegistryId);
       }

       public IFRSRevacctRegistry GetIFRSRevacctRegistry(int iFRSRevacctRegistryId)
       {
           return Channel.GetIFRSRevacctRegistry(iFRSRevacctRegistryId);
       }

       public IFRSRevacctRegistryData[] GetAllIFRSRevacctRegistries()
       {
           return Channel.GetAllIFRSRevacctRegistries();
       }

       //public string[] GetDistinctRefNotes()
       //{
       //    return Channel.GetDistinctRefNotes();
       //}

       #endregion

    }
}
