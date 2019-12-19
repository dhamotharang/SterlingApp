using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Common.Services.QueryService;
using Fintrak.Shared.Common.Services;
using System.IO;

namespace Fintrak.Client.IFRS.Proxies
{
    [Export(typeof(IIFRS9Service))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRS9Client : UserClientBase<IIFRS9Service>, IIFRS9Service
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        public UInt64 GetTotalRecordsCount(string tableName, string columnName, string searchParamS, Double? searchParamN)
        {
            return Channel.GetTotalRecordsCount(tableName, columnName, searchParamS, searchParamN);
        }

        #region ExternalRating

        public ExternalRating UpdateExternalRating(ExternalRating externalRating)
        {
            return Channel.UpdateExternalRating(externalRating);
        }

        public void DeleteExternalRating(int externalRatingId)
        {
            Channel.DeleteExternalRating(externalRatingId);
        }

        public ExternalRating GetExternalRating(int externalRatingId)
        {
            return Channel.GetExternalRating(externalRatingId);
        }

        public ExternalRating[] GetAllExternalRatings()
        {
            return Channel.GetAllExternalRatings();
        }


        #endregion

        #region HistoricalSectorRating

        public HistoricalSectorRating UpdateHistoricalSectorRating(HistoricalSectorRating historicalSectorRating)
        {
            return Channel.UpdateHistoricalSectorRating(historicalSectorRating);
        }

        public void DeleteHistoricalSectorRating(int historicalSectorRatingId)
        {
            Channel.DeleteHistoricalSectorRating(historicalSectorRatingId);
        }

        public HistoricalSectorRating GetHistoricalSectorRating(int historicalSectorRatingId)
        {
            return Channel.GetHistoricalSectorRating(historicalSectorRatingId);
        }

        public HistoricalSectorRating[] GetAllHistoricalSectorRatings()
        {
            return Channel.GetAllHistoricalSectorRatings();
        }


        #endregion

        #region InternalRatingBased

        public InternalRatingBased UpdateInternalRatingBased(InternalRatingBased internalRatingBased)
        {
            return Channel.UpdateInternalRatingBased(internalRatingBased);
        }

        public void DeleteInternalRatingBased(int internalRatingBasedId)
        {
            Channel.DeleteInternalRatingBased(internalRatingBasedId);
        }

        public InternalRatingBased GetInternalRatingBased(int internalRatingBasedId)
        {
            return Channel.GetInternalRatingBased(internalRatingBasedId);
        }

        public InternalRatingBased[] GetAllInternalRatingBaseds()
        {
            return Channel.GetAllInternalRatingBaseds();
        }


        #endregion

        #region MacroEconomic

        public MacroEconomic UpdateMacroEconomic(MacroEconomic macroEconomic)
        {
            return Channel.UpdateMacroEconomic(macroEconomic);
        }

        public void DeleteMacroEconomic(int macroEconomicId)
        {
            Channel.DeleteMacroEconomic(macroEconomicId);
        }

        public MacroEconomic GetMacroEconomic(int macroEconomicId)
        {
            return Channel.GetMacroEconomic(macroEconomicId);
        }

        public MacroEconomic[] GetAllMacroEconomics()
        {
            return Channel.GetAllMacroEconomics();
        }


        #endregion

        #region RatingMapping

        public RatingMapping UpdateRatingMapping(RatingMapping ratingMapping)
        {
            return Channel.UpdateRatingMapping(ratingMapping);
        }

        public void DeleteRatingMapping(int ratingMappingId)
        {
            Channel.DeleteRatingMapping(ratingMappingId);
        }

        public RatingMapping GetRatingMapping(int ratingMappingId)
        {
            return Channel.GetRatingMapping(ratingMappingId);
        }

        public RatingMapping[] GetAllRatingMappings()
        {
            return Channel.GetAllRatingMappings();
        }

        public RatingMappingData[] GetRatingMappings()
        {
            return Channel.GetRatingMappings();
        }
        #endregion

        #region Transition

        public Transition UpdateTransition(Transition transition)
        {
            return Channel.UpdateTransition(transition);
        }

        public void DeleteTransition(int transitionId)
        {
            Channel.DeleteTransition(transitionId);
        }

        public Transition GetTransition(int transitionId)
        {
            return Channel.GetTransition(transitionId);
        }

        public Transition[] GetAllTransitions()
        {
            return Channel.GetAllTransitions();
        }


        #endregion





        #region BondsECLComputationResult

        public BondsECLComputationResult UpdateBondsECLComputationResult(BondsECLComputationResult bondseclcomputationresult)
        {
            return Channel.UpdateBondsECLComputationResult(bondseclcomputationresult);
        }

        public void DeleteBondsECLComputationResult(int InstrumentID)
        {
            Channel.DeleteBondsECLComputationResult(InstrumentID);
        }

        public BondsECLComputationResult GetBondsECLComputationResult(int ID)
        {
            return Channel.GetBondsECLComputationResult(ID);
        }

        public BondsECLComputationResult[] GetAllBondsECLComputationResults()
        {
            return Channel.GetAllBondsECLComputationResults();
        }

        public BondsECLComputationResult[] GetBondsECLComputationResultBySearch(string searchParam, string path)
        {
            return Channel.GetBondsECLComputationResultBySearch(searchParam, path);
        }

        public BondsECLComputationResult[] GetBondsECLComputationResults(int defaultCount, string path)
        {
            return Channel.GetBondsECLComputationResults(defaultCount, path);
        }


        #endregion

        #region MonthlyObeEadSTRLB

        public MonthlyObeEadSTRLB UpdateMonthlyObeEadSTRLB(MonthlyObeEadSTRLB monthlyobeeadstrlb)
        {
            return Channel.UpdateMonthlyObeEadSTRLB(monthlyobeeadstrlb);
        }

        public void DeleteMonthlyObeEadSTRLB(int InstrumentID)
        {
            Channel.DeleteMonthlyObeEadSTRLB(InstrumentID);
        }

        public MonthlyObeEadSTRLB GetMonthlyObeEadSTRLB(int ID)
        {
            return Channel.GetMonthlyObeEadSTRLB(ID);
        }

        public MonthlyObeEadSTRLB[] GetAllMonthlyObeEadSTRLBs()
        {
            return Channel.GetAllMonthlyObeEadSTRLBs();
        }

        public MonthlyObeEadSTRLB[] GetMonthlyObeEadSTRLBBySearch(string searchParam)
        {
            return Channel.GetMonthlyObeEadSTRLBBySearch(searchParam);
        }

        public MonthlyObeEadSTRLB[] GetMonthlyObeEadSTRLBs(int defaultCount)
        {
            return Channel.GetMonthlyObeEadSTRLBs(defaultCount);
        }


        #endregion





        #region HistoricalClassification

        public HistoricalClassification UpdateHistoricalClassification(HistoricalClassification historicalClassification)
        {
            return Channel.UpdateHistoricalClassification(historicalClassification);
        }

        public void DeleteHistoricalClassification(int historicalClassificationId)
        {
            Channel.DeleteHistoricalClassification(historicalClassificationId);
        }

        public HistoricalClassification GetHistoricalClassification(int historicalClassificationId)
        {
            return Channel.GetHistoricalClassification(historicalClassificationId);
        }

        public HistoricalClassification[] GetAllHistoricalClassifications()
        {
            return Channel.GetAllHistoricalClassifications();
        }


        #endregion

        #region MacroEconomicHistorical

        public MacroEconomicHistorical UpdateMacroEconomicHistorical(MacroEconomicHistorical macroEconomicHistorical)
        {
            return Channel.UpdateMacroEconomicHistorical(macroEconomicHistorical);
        }

        public void DeleteMacroEconomicHistorical(int macroEconomicHistoricalId)
        {
            Channel.DeleteMacroEconomicHistorical(macroEconomicHistoricalId);
        }

        public MacroEconomicHistorical GetMacroEconomicHistorical(int macroEconomicHistoricalId)
        {
            return Channel.GetMacroEconomicHistorical(macroEconomicHistoricalId);
        }

        public MacroEconomicHistoricalData[] GetAllMacroEconomicHistoricals()
        {
            return Channel.GetAllMacroEconomicHistoricals();
        }


        #endregion

        #region NotchDifference

        public NotchDifference UpdateNotchDifference(NotchDifference notchDifference)
        {
            return Channel.UpdateNotchDifference(notchDifference);
        }

        public void DeleteNotchDifference(int notchDifferenceId)
        {
            Channel.DeleteNotchDifference(notchDifferenceId);
        }

        public NotchDifference GetNotchDifference(int notchDifferenceId)
        {
            return Channel.GetNotchDifference(notchDifferenceId);
        }

        public NotchDifference[] GetAllNotchDifferences()
        {
            return Channel.GetAllNotchDifferences();
        }


        #endregion

        #region SetUp

        public SetUp UpdateSetUp(SetUp setUp)
        {
            return Channel.UpdateSetUp(setUp);
        }

        public void DeleteSetUp(int setUpId)
        {
            Channel.DeleteSetUp(setUpId);
        }

        public SetUp GetSetUp(int setUpId)
        {
            return Channel.GetSetUp(setUpId);
        }

        public SetUp[] GetAllSetUps()
        {
            return Channel.GetAllSetUps();
        }


        #endregion

        #region HistoricalSectorialPD

        public HistoricalSectorialPD UpdateHistoricalSectorialPD(HistoricalSectorialPD historicalSectorialPD)
        {
            return Channel.UpdateHistoricalSectorialPD(historicalSectorialPD);
        }

        public void DeleteHistoricalSectorialPD(int historicalSectorialPDId)
        {
            Channel.DeleteHistoricalSectorialPD(historicalSectorialPDId);
        }

        public HistoricalSectorialPD GetHistoricalSectorialPD(int historicalSectorialPDId)
        {
            return Channel.GetHistoricalSectorialPD(historicalSectorialPDId);
        }

        public HistoricalSectorialPD[] GetAllHistoricalSectorialPDs()
        {
            return Channel.GetAllHistoricalSectorialPDs();
        }

        public string[] GetDistinctYear()
        {
            return Channel.GetDistinctYear();
        }
        public string[] GetDistinctPeriod()
        {
            return Channel.GetDistinctPeriod();
        }

        public void ComputeHistoricalSectorialPD(int computationType,int curYear, int curPeriod, int prevYear, int prevPeriod)
        {
            Channel.ComputeHistoricalSectorialPD(computationType,curYear, curPeriod, prevYear, prevPeriod);
        }
        #endregion

        #region HistoricalSectorialLGD

        public HistoricalSectorialLGD UpdateHistoricalSectorialLGD(HistoricalSectorialLGD historicalSectorialLGD)
        {
            return Channel.UpdateHistoricalSectorialLGD(historicalSectorialLGD);
        }

        public void DeleteHistoricalSectorialLGD(int historicalSectorialLGDId)
        {
            Channel.DeleteHistoricalSectorialLGD(historicalSectorialLGDId);
        }

        public HistoricalSectorialLGD GetHistoricalSectorialLGD(int historicalSectorialLGDId)
        {
            return Channel.GetHistoricalSectorialLGD(historicalSectorialLGDId);
        }

        public HistoricalSectorialLGD[] GetAllHistoricalSectorialLGDs()
        {
            return Channel.GetAllHistoricalSectorialLGDs();
        }

        public string[] GetDistinctLYear()
        {
            return Channel.GetDistinctYear();
        }
        public string[] GetDistinctLPeriod()
        {
            return Channel.GetDistinctPeriod();
        }

        public void ComputeHistoricalSectorialLGD(int computationType, int curYear, int curPeriod, int prevYear, int prevPeriod)
        {
            Channel.ComputeHistoricalSectorialLGD(computationType, curYear, curPeriod, prevYear, prevPeriod);
        }
        #endregion

        //#region ComputedForcastedPDLGD

        //public ComputedForcastedPDLGD UpdateComputedForcastedPDLGD(ComputedForcastedPDLGD computedForcastedPDLGD)
        //{
        //    return Channel.UpdateComputedForcastedPDLGD(computedForcastedPDLGD);
        //}

        //public void DeleteComputedForcastedPDLGD(int computedPDId)
        //{
        //    Channel.DeleteComputedForcastedPDLGD(computedPDId);
        //}

        //public ComputedForcastedPDLGD GetComputedForcastedPDLGD(int computedPDId)
        //{
        //    return Channel.GetComputedForcastedPDLGD(computedPDId);
        //}

        //public ComputedForcastedPDLGD[] GetAllComputedForcastedPDLGDs()
        //{
        //    return Channel.GetAllComputedForcastedPDLGDs();
        //}


        //#endregion

        #region SectorialRegressedPD

        public SectorialRegressedPD UpdateSectorialRegressedPD(SectorialRegressedPD sectorialRegressedPD)
        {
            return Channel.UpdateSectorialRegressedPD(sectorialRegressedPD);
        }

        public void DeleteSectorialRegressedPD(int sectorialRegressedPDId)
        {
            Channel.DeleteSectorialRegressedPD(sectorialRegressedPDId);
        }

        public SectorialRegressedPD GetSectorialRegressedPD(int sectorialRegressedPDId)
        {
            return Channel.GetSectorialRegressedPD(sectorialRegressedPDId);
        }

        public SectorialRegressedPD[] GetAllSectorialRegressedPDs()
        {
            return Channel.GetAllSectorialRegressedPDs();
        }


        #endregion

        #region SectorialRegressedLGD

        public SectorialRegressedLGD UpdateSectorialRegressedLGD(SectorialRegressedLGD sectorialRegressedLGD)
        {
            return Channel.UpdateSectorialRegressedLGD(sectorialRegressedLGD);
        }

        public void DeleteSectorialRegressedLGD(int sectorialRegressedLGDId)
        {
            Channel.DeleteSectorialRegressedLGD(sectorialRegressedLGDId);
        }

        public SectorialRegressedLGD GetSectorialRegressedLGD(int sectorialRegressedLGDId)
        {
            return Channel.GetSectorialRegressedLGD(sectorialRegressedLGDId);
        }

        public SectorialRegressedLGD[] GetAllSectorialRegressedLGDs()
        {
            return Channel.GetAllSectorialRegressedLGDs();
        }


        #endregion

        #region MacroEconomicVariable

        public MacroEconomicVariable UpdateMacroEconomicVariable(MacroEconomicVariable macroEconomicVariable)
        {
            return Channel.UpdateMacroEconomicVariable(macroEconomicVariable);
        }

        public void DeleteMacroEconomicVariable(int macroEconomicVariableId)
        {
            Channel.DeleteMacroEconomicVariable(macroEconomicVariableId);
        }

        public MacroEconomicVariable GetMacroEconomicVariable(int macroEconomicVariableId)
        {
            return Channel.GetMacroEconomicVariable(macroEconomicVariableId);
        }

        public MacroEconomicVariable[] GetAllMacroEconomicVariables()
        {
            return Channel.GetAllMacroEconomicVariables();
        }


        #endregion

        #region SectorVariableMapping

        public SectorVariableMapping UpdateSectorVariableMapping(SectorVariableMapping sectorVariableMapping)
        {
            return Channel.UpdateSectorVariableMapping(sectorVariableMapping);
        }

        public void DeleteSectorVariableMapping(int sectorVariableMappingId)
        {
            Channel.DeleteSectorVariableMapping(sectorVariableMappingId);
        }

        public SectorVariableMapping GetSectorVariableMapping(int sectorVariableMappingId)
        {
            return Channel.GetSectorVariableMapping(sectorVariableMappingId);
        }

        public SectorVariableMappingData[] GetAllSectorVariableMappings()
        {
            return Channel.GetAllSectorVariableMappings();
        }


        #endregion

        #region PitFormular

        public PitFormular UpdatePitFormular(PitFormular pitFormular)
        {
            return Channel.UpdatePitFormular(pitFormular);
        }

        public void DeletePitFormular(int pitFormularId)
        {
            Channel.DeletePitFormular(pitFormularId);
        }

        public PitFormular GetPitFormular(int pitFormularId)
        {
            return Channel.GetPitFormular(pitFormularId);
        }

        public PitFormular[] GetAllPitFormulars()
        {
            return Channel.GetAllPitFormulars();
        }


        #endregion

        #region PortfolioExposure

        public PortfolioExposure UpdatePortfolioExposure(PortfolioExposure portfolioExposure)
        {
            return Channel.UpdatePortfolioExposure(portfolioExposure);
        }

        public void DeletePortfolioExposure(int portfolioExposureId)
        {
            Channel.DeletePortfolioExposure(portfolioExposureId);
        }

        public PortfolioExposure GetPortfolioExposure(int portfolioExposureId)
        {
            return Channel.GetPortfolioExposure(portfolioExposureId);
        }

        public PortfolioExposure[] GetAllPortfolioExposures()
        {
            return Channel.GetAllPortfolioExposures();
        }

        public string GetAllPortfolioExposuresChart()
        {
            return Channel.GetAllPortfolioExposuresChart();
        }

        #endregion

        #region SectorialExposure

        public SectorialExposure UpdateSectorialExposure(SectorialExposure sectorialExposure)
        {
            return Channel.UpdateSectorialExposure(sectorialExposure);
        }

        public void DeleteSectorialExposure(int sectorialExposureId)
        {
            Channel.DeleteSectorialExposure(sectorialExposureId);
        }

        public SectorialExposure GetSectorialExposure(int sectorialExposureId)
        {
            return Channel.GetSectorialExposure(sectorialExposureId);
        }

        public SectorialExposure[] GetAllSectorialExposures()
        {
            return Channel.GetAllSectorialExposures();
        }

        public string GetAllSectorialExposuresChart()
        {
            return Channel.GetAllSectorialExposuresChart();
        }

        #endregion

        #region PiTPD

        public PiTPD UpdatePiTPD(PiTPD piTPD)
        {
            return Channel.UpdatePiTPD(piTPD);
        }

        public void DeletePiTPD(int piTPDId)
        {
            Channel.DeletePiTPD(piTPDId);
        }

        public PiTPD GetPiTPD(int piTPDId)
        {
            return Channel.GetPiTPD(piTPDId);
        }

        public PiTPD[] GetAllPiTPDs()
        {
            return Channel.GetAllPiTPDs();
        }

        public void RegressPD()
        {
            Channel.RegressPD();
        }

        #endregion

        #region EclCalculationModel

        public EclCalculationModel UpdateEclCalculationModel(EclCalculationModel loanBucketDistribution)
        {
            return Channel.UpdateEclCalculationModel(loanBucketDistribution);
        }

        public void DeleteEclCalculationModel(int loanBucketDistributionId)
        {
            Channel.DeleteEclCalculationModel(loanBucketDistributionId);
        }

        public EclCalculationModel GetEclCalculationModel(int loanBucketDistributionId)
        {
            return Channel.GetEclCalculationModel(loanBucketDistributionId);
        }

        public EclCalculationModel[] GetAllEclCalculationModels()
        {
            return Channel.GetAllEclCalculationModels();
        }


        #endregion

        #region LoanBucketDistribution

        public LoanBucketDistribution UpdateLoanBucketDistribution(LoanBucketDistribution loanBucketDistribution)
        {
            return Channel.UpdateLoanBucketDistribution(loanBucketDistribution);
        }

        public void DeleteLoanBucketDistribution(int loanBucketDistributionId)
        {
            Channel.DeleteLoanBucketDistribution(loanBucketDistributionId);
        }

        public LoanBucketDistribution GetLoanBucketDistribution(int loanBucketDistributionId)
        {
            return Channel.GetLoanBucketDistribution(loanBucketDistributionId);
        }

        public LoanBucketDistribution[] GetAllLoanBucketDistributions()
        {
            return Channel.GetAllLoanBucketDistributions();
        }

        public void PDDistribution()
        {
            Channel.PDDistribution();
        }

        public void PastDueDayDistribution()
        {
            Channel.PastDueDayDistribution();
        }

        public LoanBucketDistribution[] GetLoanAssessments(string bucket)
        {
            return Channel.GetLoanAssessments(bucket);
        }
        #endregion

        #region MacroeconomicVDisplay

        public MacroeconomicVDisplay UpdateMacroeconomicVDisplay(MacroeconomicVDisplay macroeconomicVDisplay)
        {
            return Channel.UpdateMacroeconomicVDisplay(macroeconomicVDisplay);
        }

        public void DeleteMacroeconomicVDisplay(int macroeconomicVDisplayId)
        {
            Channel.DeleteMacroeconomicVDisplay(macroeconomicVDisplayId);
        }

        public MacroeconomicVDisplay GetMacroeconomicVDisplay(int macroeconomicVDisplayId)
        {
            return Channel.GetMacroeconomicVDisplay(macroeconomicVDisplayId);
        }

        public MacroeconomicVDisplay[] GetAllMacroeconomicVDisplays()
        {
            return Channel.GetAllMacroeconomicVDisplays();
        }

        public string[] GetDistinctFHYear(string vType)
        {
            return Channel.GetDistinctFHYear(vType);
        }


        public MacroeconomicVDisplay[] GetMacroeconomicVDisplayByYear(int yr)
        {
            return Channel.GetMacroeconomicVDisplayByYear(yr);
        }
       
        #endregion

        #region LifeTimePDClassification

        public LifeTimePDClassification UpdateLifeTimePDClassification(LifeTimePDClassification lifeTimePDClassification)
        {
            return Channel.UpdateLifeTimePDClassification(lifeTimePDClassification);
        }

        public void DeleteLifeTimePDClassification(int lifeTimePDClassificationId)
        {
            Channel.DeleteLifeTimePDClassification(lifeTimePDClassificationId);
        }

        public LifeTimePDClassification GetLifeTimePDClassification(int lifeTimePDClassificationId)
        {
            return Channel.GetLifeTimePDClassification(lifeTimePDClassificationId);
        }

        public LifeTimePDClassification[] GetAllLifeTimePDClassifications()
        {
            return Channel.GetAllLifeTimePDClassifications();
        }


        #endregion


        #region IfrsEquityUnqouted

        public IfrsEquityUnqouted UpdateIfrsEquityUnqouted(IfrsEquityUnqouted ifrsEquityUnqouted)
        {
            return Channel.UpdateIfrsEquityUnqouted(ifrsEquityUnqouted);
        }

        public void DeleteIfrsEquityUnqouted(int ifrsEquityUnqoutedId)
        {
            Channel.DeleteIfrsEquityUnqouted(ifrsEquityUnqoutedId);
        }

        public IfrsEquityUnqouted GetIfrsEquityUnqouted(int ifrsEquityUnqoutedId)
        {
            return Channel.GetIfrsEquityUnqouted(ifrsEquityUnqoutedId);
        }

        public IfrsEquityUnqouted[] GetAllIfrsEquityUnqouteds()
        {
            return Channel.GetAllIfrsEquityUnqouteds();
        }


        #endregion

        #region IfrsStocksPrimaryData

        public IfrsStocksPrimaryData UpdateIfrsStocksPrimaryData(IfrsStocksPrimaryData ifrsStocksPrimaryData)
        {
            return Channel.UpdateIfrsStocksPrimaryData(ifrsStocksPrimaryData);
        }

        public void DeleteIfrsStocksPrimaryData(int ifrsStocksPrimaryDataId)
        {
            Channel.DeleteIfrsStocksPrimaryData(ifrsStocksPrimaryDataId);
        }

        public IfrsStocksPrimaryData GetIfrsStocksPrimaryData(int ifrsStocksPrimaryDataId)
        {
            return Channel.GetIfrsStocksPrimaryData(ifrsStocksPrimaryDataId);
        }

        public IfrsStocksPrimaryData[] GetAllIfrsStocksPrimaryDatas()
        {
            return Channel.GetAllIfrsStocksPrimaryDatas();
        }


        #endregion

        #region IfrsStocksMapping

        public IfrsStocksMapping UpdateIfrsStocksMapping(IfrsStocksMapping ifrsStocksMapping)
        {
            return Channel.UpdateIfrsStocksMapping(ifrsStocksMapping);
        }

        public void DeleteIfrsStocksMapping(int ifrsStocksMappingId)
        {
            Channel.DeleteIfrsStocksMapping(ifrsStocksMappingId);
        }

        public IfrsStocksMapping GetIfrsStocksMapping(int ifrsStocksMappingId)
        {
            return Channel.GetIfrsStocksMapping(ifrsStocksMappingId);
        }

        public IfrsStocksMappingData[] GetAllIfrsStocksMappings()
        {
            return Channel.GetAllIfrsStocksMappings();
        }


        #endregion

        #region Reconciliation

        public Reconciliation UpdateReconciliation(Reconciliation reconciliation)
        {
            return Channel.UpdateReconciliation(reconciliation);
        }

        public void DeleteReconciliation(int reconciliationId)
        {
            Channel.DeleteReconciliation(reconciliationId);
        }

        public Reconciliation GetReconciliation(int reconciliationId)
        {
            return Channel.GetReconciliation(reconciliationId);
        }

        public Reconciliation[] GetAllReconciliations()
        {
            return Channel.GetAllReconciliations();
        }


        #endregion

        #region ForecastedMacroeconimcsSensitivity

        public ForecastedMacroeconimcsSensitivity UpdateForecastedMacroeconimcsSensitivity(ForecastedMacroeconimcsSensitivity forecastedMacroeconimcsSensitivity)
        {
            return Channel.UpdateForecastedMacroeconimcsSensitivity(forecastedMacroeconimcsSensitivity);
        }

        public void DeleteForecastedMacroeconimcsSensitivity(int forecastedMacroeconimcsSensitivityId)
        {
            Channel.DeleteForecastedMacroeconimcsSensitivity(forecastedMacroeconimcsSensitivityId);
        }

        public ForecastedMacroeconimcsSensitivity GetForecastedMacroeconimcsSensitivity(int forecastedMacroeconimcsSensitivityId)
        {
            return Channel.GetForecastedMacroeconimcsSensitivity(forecastedMacroeconimcsSensitivityId);
        }

        public ForecastedMacroeconimcsSensitivityData[] GetAllForecastedMacroeconimcsSensitivitys()
        {
            return Channel.GetAllForecastedMacroeconimcsSensitivitys();
        }

        public void InsertSensitivityData(string microeconomic, int year, int types, float values)
        {
            Channel.InsertSensitivityData(microeconomic, year, types, values);
        }

        public void ComputeSensitivity()
        {
            Channel.ComputeSensitivity();
        }


        #endregion

        #region SummaryReport

        public SummaryReport UpdateSummaryReport(SummaryReport summaryReport)
        {
            return Channel.UpdateSummaryReport(summaryReport);
        }

        public void DeleteSummaryReport(int summaryReportId)
        {
            Channel.DeleteSummaryReport(summaryReportId);
        }

        public SummaryReport GetSummaryReport(int summaryReportId)
        {
            return Channel.GetSummaryReport(summaryReportId);
        }

        public SummaryReport[] GetAllSummaryReports()
        {
            return Channel.GetAllSummaryReports();
        }

        public string GetAllSummaryReportsChart()
        {
            return Channel.GetAllSummaryReportsChart();
        }

        #endregion

        #region FairValuationModel

        public FairValuationModel UpdateFairValuationModel(FairValuationModel fairValuationModel)
        {
            return Channel.UpdateFairValuationModel(fairValuationModel);
        }

        public void DeleteFairValuationModel(int fairValuationModelId)
        {
            Channel.DeleteFairValuationModel(fairValuationModelId);
        }

        public FairValuationModel GetFairValuationModel(int fairValuationModelId)
        {
            return Channel.GetFairValuationModel(fairValuationModelId);
        }

        public FairValuationModel[] GetAllFairValuationModels()
        {
            return Channel.GetAllFairValuationModels();
        }


        #endregion

        #region BucketExposure

        public BucketExposure UpdateBucketExposure(BucketExposure bucketExposure)
        {
            return Channel.UpdateBucketExposure(bucketExposure);
        }

        public void DeleteBucketExposure(int bucketExposureId)
        {
            Channel.DeleteBucketExposure(bucketExposureId);
        }

        public BucketExposure GetBucketExposure(int bucketExposureId)
        {
            return Channel.GetBucketExposure(bucketExposureId);
        }

        public BucketExposure[] GetAllBucketExposures()
        {
            return Channel.GetAllBucketExposures();
        }

        public string GetAllBucketExposuresChart()
        {
            return Channel.GetAllBucketExposuresChart();
        }

        #endregion

        #region ForecastedMacroeconimcsScenario

        public ForecastedMacroeconimcsScenario UpdateForecastedMacroeconimcsScenario(ForecastedMacroeconimcsScenario forecastedMacroeconimcsScenario)
        {
            return Channel.UpdateForecastedMacroeconimcsScenario(forecastedMacroeconimcsScenario);
        }

        public void DeleteForecastedMacroeconimcsScenario(int forecastedMacroeconimcsScenarioId)
        {
            Channel.DeleteForecastedMacroeconimcsScenario(forecastedMacroeconimcsScenarioId);
        }

        public ForecastedMacroeconimcsScenario GetForecastedMacroeconimcsScenario(int forecastedMacroeconimcsScenarioId)
        {
            return Channel.GetForecastedMacroeconimcsScenario(forecastedMacroeconimcsScenarioId);
        }

        public ForecastedMacroeconimcsScenarioData[] GetAllForecastedMacroeconimcsScenarios()
        {
            return Channel.GetAllForecastedMacroeconimcsScenarios();
        }

        public void InsertScenarioData(string sector, string microeconomic, int year, int types, float values)
        {
            Channel.InsertScenarioData(sector, microeconomic, year, types, values);
        }

        public void ComputeScenario()
        {
            Channel.ComputeScenario();
        }


        #endregion

        #region LoanSpreadScenario

        public LoanSpreadScenario UpdateLoanSpreadScenario(LoanSpreadScenario loanSpreadScenario)
        {
            return Channel.UpdateLoanSpreadScenario(loanSpreadScenario);
        }

        public void DeleteLoanSpreadScenario(int loanSpreadScenarioId)
        {
            Channel.DeleteLoanSpreadScenario(loanSpreadScenarioId);
        }

        public LoanSpreadScenario GetLoanSpreadScenario(int loanSpreadScenarioId)
        {
            return Channel.GetLoanSpreadScenario(loanSpreadScenarioId);
        }

        public LoanSpreadScenario[] GetAllLoanSpreadScenarios()
        {
            return Channel.GetAllLoanSpreadScenarios();
        }


        //public LoanSpreadScenario[] GetLoanAssessments(string bucket)
        //{
        //    return Channel.GetLoanAssessments(bucket);
        //}
        #endregion

        #region LoanSpreadSensitivity

        public LoanSpreadSensitivity UpdateLoanSpreadSensitivity(LoanSpreadSensitivity loanSpreadSensitivity)
        {
            return Channel.UpdateLoanSpreadSensitivity(loanSpreadSensitivity);
        }

        public void DeleteLoanSpreadSensitivity(int loanSpreadSensitivityId)
        {
            Channel.DeleteLoanSpreadSensitivity(loanSpreadSensitivityId);
        }

        public LoanSpreadSensitivity GetLoanSpreadSensitivity(int loanSpreadSensitivityId)
        {
            return Channel.GetLoanSpreadSensitivity(loanSpreadSensitivityId);
        }

        public LoanSpreadSensitivity[] GetAllLoanSpreadSensitivitys()
        {
            return Channel.GetAllLoanSpreadSensitivitys();
        }

        //public LoanSpreadSensitivity[] GetLoanAssessments(string bucket)
        //{
        //    return Channel.GetLoanAssessments(bucket);
        //}
        #endregion

        #region UnquotedEquityFairvalueResult

        public UnquotedEquityFairvalueResult UpdateUnquotedEquityFairvalueResult(UnquotedEquityFairvalueResult unquotedEquityFairvalueResult)
        {
            return Channel.UpdateUnquotedEquityFairvalueResult(unquotedEquityFairvalueResult);
        }

        public void DeleteUnquotedEquityFairvalueResult(int unquotedEquityFairvalueResultId)
        {
            Channel.DeleteUnquotedEquityFairvalueResult(unquotedEquityFairvalueResultId);
        }

        public UnquotedEquityFairvalueResult GetUnquotedEquityFairvalueResult(int unquotedEquityFairvalueResultId)
        {
            return Channel.GetUnquotedEquityFairvalueResult(unquotedEquityFairvalueResultId);
        }

        public UnquotedEquityFairvalueResult[] GetAllUnquotedEquityFairvalueResults()
        {
            return Channel.GetAllUnquotedEquityFairvalueResults();
        }

        #region PiTPDComparism

        public PiTPDComparism UpdatePiTPDComparism(PiTPDComparism piTPDComparism)
        {
            return Channel.UpdatePiTPDComparism(piTPDComparism);
        }

        public void DeletePiTPDComparism(int comparismPDId)
        {
            Channel.DeletePiTPDComparism(comparismPDId);
        }

        public PiTPDComparism GetPiTPDComparism(int comparismPDId)
        {
            return Channel.GetPiTPDComparism(comparismPDId);
        }

        public PiTPDComparism[] GetAllPiTPDComparisms()
        {
            return Channel.GetAllPiTPDComparisms();
        }

        //public string GetAllPiTPDComparismsChart()
        //{
        //    return Channel.GetAllPiTPDComparismsChart();
        //}

        #endregion

        #endregion

        #region MarkovMatrix

        public MarkovMatrix UpdateMarkovMatrix(MarkovMatrix markovMatrix)
        {
            return Channel.UpdateMarkovMatrix(markovMatrix);
        }

        public void DeleteMarkovMatrix(int markovMatrixId)
        {
            Channel.DeleteMarkovMatrix(markovMatrixId);
        }

        public MarkovMatrix GetMarkovMatrix(int markovMatrixId)
        {
            return Channel.GetMarkovMatrix(markovMatrixId);
        }

        public MarkovMatrix[] GetAllMarkovMatrixs()
        {
            return Channel.GetAllMarkovMatrixs();
        }

        public MarkovMatrixData[] GetMarkovMatrixs()
        {
            return Channel.GetMarkovMatrixs();
        }
        #endregion

        #region CCFModelling

        public CCFModelling UpdateCCFModelling(CCFModelling ccfModelling)
        {
            return Channel.UpdateCCFModelling(ccfModelling);
        }

        public void DeleteCCFModelling(int ccfModellingId)
        {
            Channel.DeleteCCFModelling(ccfModellingId);
        }

        public CCFModelling GetCCFModelling(int ccfModellingId)
        {
            return Channel.GetCCFModelling(ccfModellingId);
        }

        public CCFModelling[] GetAllCCFModellings()
        {
            return Channel.GetAllCCFModellings();
        }

        #endregion

        #region ECLComparism

        public ECLComparism UpdateECLComparism(ECLComparism eclComparism)
        {
            return Channel.UpdateECLComparism(eclComparism);
        }

        public void DeleteECLComparism(int eclComparismId)
        {
            Channel.DeleteECLComparism(eclComparismId);
        }

        public ECLComparism GetECLComparism(int eclComparismId)
        {
            return Channel.GetECLComparism(eclComparismId);
        }

        public ECLComparism[] GetAllECLComparisms()
        {
            return Channel.GetAllECLComparisms();
        }


        #endregion

        #region ForeignEADExchnageRAte

        public ForeignEADExchangeRate UpdateForeignEADExchangeRate(ForeignEADExchangeRate foreignEADExchangeRate)
        {
            return Channel.UpdateForeignEADExchangeRate(foreignEADExchangeRate);
        }

        public void DeleteForeignEADExchangeRate(int foreignEADExchangeRateId)
        {
            Channel.DeleteForeignEADExchangeRate(foreignEADExchangeRateId);
        }

        public ForeignEADExchangeRate GetForeignEADExchangeRate(int foreignEADExchnageRateId)
        {
            return Channel.GetForeignEADExchangeRate(foreignEADExchnageRateId);
        }

        public ForeignEADExchangeRate[] GetAllForeignEADExchangeRates()
        {
            return Channel.GetAllForeignEADExchangeRates();
        }


        #endregion

        //Begin Victor Segment

        #region EuroBondSpread

        public EuroBondSpread UpdateEuroBondSpread(EuroBondSpread euroBondSpread)
        {
            return Channel.UpdateEuroBondSpread(euroBondSpread);
        }

        public void DeleteEuroBondSpread(int euroBondSpreadId)
        {
            Channel.DeleteEuroBondSpread(euroBondSpreadId);
        }

        public EuroBondSpread GetEuroBondSpread(int euroBondSpreadId)
        {
            return Channel.GetEuroBondSpread(euroBondSpreadId);
        }

        public EuroBondSpread[] GetAllEuroBondSpreads()
        {
            return Channel.GetAllEuroBondSpreads();
        }


        #endregion

        #region PlacementComputationResult

        public PlacementComputationResult UpdatePlacementComputationResult(PlacementComputationResult placementComputationResult)
        {
            return Channel.UpdatePlacementComputationResult(placementComputationResult);
        }

        public void DeletePlacementComputationResult(int placementComputationResultId)
        {
            Channel.DeletePlacementComputationResult(placementComputationResultId);
        }

        public PlacementComputationResult GetPlacementComputationResult(int placementComputationResultId)
        {
            return Channel.GetPlacementComputationResult(placementComputationResultId);
        }

        public PlacementComputationResult[] GetAllPlacementComputationResults()
        {
            return Channel.GetAllPlacementComputationResults();
        }


        #endregion

        #region LgdComputationResultPlacement

        public LgdComputationResultPlacement UpdateLgdComputationResultPlacement(LgdComputationResultPlacement lgdComputationResultPlacement)
        {
            return Channel.UpdateLgdComputationResultPlacement(lgdComputationResultPlacement);
        }

        public void DeleteLgdComputationResultPlacement(int lgdComputationResultPlacementId)
        {
            Channel.DeleteLgdComputationResultPlacement(lgdComputationResultPlacementId);
        }

        public LgdComputationResultPlacement GetLgdComputationResultPlacement(int lgdComputationResultPlacementId)
        {
            return Channel.GetLgdComputationResultPlacement(lgdComputationResultPlacementId);
        }

        public LgdComputationResultPlacement[] GetAllLgdComputationResultPlacements()
        {
            return Channel.GetAllLgdComputationResultPlacements();
        }


        #endregion

        #region LgdComputationResult

        public LgdComputationResult UpdateLgdComputationResult(LgdComputationResult lgdComputationResult)
        {
            return Channel.UpdateLgdComputationResult(lgdComputationResult);
        }

        public void DeleteLgdComputationResult(int lgdComputationResultId)
        {
            Channel.DeleteLgdComputationResult(lgdComputationResultId);
        }

        public LgdComputationResult GetLgdComputationResult(int lgdComputationResultId)
        {
            return Channel.GetLgdComputationResult(lgdComputationResultId);
        }

        public LgdComputationResult[] GetAllLgdComputationResults()
        {
            return Channel.GetAllLgdComputationResults();
        }


        #endregion

        #region LocalBondSpread

        public LocalBondSpread UpdateLocalBondSpread(LocalBondSpread localBondSpread)
        {
            return Channel.UpdateLocalBondSpread(localBondSpread);
        }

        public void DeleteLocalBondSpread(int localBondSpreadId)
        {
            Channel.DeleteLocalBondSpread(localBondSpreadId);
        }

        public LocalBondSpread GetLocalBondSpread(int localBondSpreadId)
        {
            return Channel.GetLocalBondSpread(localBondSpreadId);
        }

        public LocalBondSpread[] GetAllLocalBondSpreads()
        {
            return Channel.GetAllLocalBondSpreads();
        }


        #endregion

        #region MarginalPDDistribution

        public MarginalPDDistribution UpdateMarginalPDDistribution(MarginalPDDistribution marginalPDDistribution)
        {
            return Channel.UpdateMarginalPDDistribution(marginalPDDistribution);
        }

        public void DeleteMarginalPDDistribution(int marginalPDDistributionId)
        {
            Channel.DeleteMarginalPDDistribution(marginalPDDistributionId);
        }

        public MarginalPDDistribution GetMarginalPDDistribution(int marginalPDDistributionId)
        {
            return Channel.GetMarginalPDDistribution(marginalPDDistributionId);
        }

        public MarginalPDDistribution[] GetAllMarginalPDDistributions()
        {
            return Channel.GetAllMarginalPDDistributions();
        }


        #endregion

        #region BondMarginalPDDistribution

        public BondMarginalPDDistribution UpdateBondMarginalPDDistribution(BondMarginalPDDistribution bondMarginalPDDistribution)
        {
            return Channel.UpdateBondMarginalPDDistribution(bondMarginalPDDistribution);
        }

        public void DeleteBondMarginalPDDistribution(int bondMarginalPDDistributionId)
        {
            Channel.DeleteBondMarginalPDDistribution(bondMarginalPDDistributionId);
        }

        public BondMarginalPDDistribution GetBondMarginalPDDistribution(int bondMarginalPDDistributionId)
        {
            return Channel.GetBondMarginalPDDistribution(bondMarginalPDDistributionId);
        }

        public BondMarginalPDDistribution[] GetAllBondMarginalPDDistributions()
        {
            return Channel.GetAllBondMarginalPDDistributions();
        }


        #endregion

        #region MarginalPDDistributionPlacement

        public MarginalPDDistributionPlacement UpdateMarginalPDDistributionPlacement(MarginalPDDistributionPlacement marginalPDDistributionPlacement)
        {
            return Channel.UpdateMarginalPDDistributionPlacement(marginalPDDistributionPlacement);
        }

        public void DeleteMarginalPDDistributionPlacement(int marginalPDDistributionPlacementId)
        {
            Channel.DeleteMarginalPDDistributionPlacement(marginalPDDistributionPlacementId);
        }

        public MarginalPDDistributionPlacement GetMarginalPDDistributionPlacement(int marginalPDDistributionPlacementId)
        {
            return Channel.GetMarginalPDDistributionPlacement(marginalPDDistributionPlacementId);
        }

        public MarginalPDDistributionPlacement[] GetAllMarginalPDDistributionPlacements()
        {
            return Channel.GetAllMarginalPDDistributionPlacements();
        }


        #endregion

        #region EclComputationResult

        public EclComputationResult UpdateEclComputationResult(EclComputationResult eclComputationResult)
        {
            return Channel.UpdateEclComputationResult(eclComputationResult);
        }

        public void DeleteEclComputationResult(int eclComputationResultId)
        {
            Channel.DeleteEclComputationResult(eclComputationResultId);
        }

        public EclComputationResult GetEclComputationResult(int eclComputationResultId)
        {
            return Channel.GetEclComputationResult(eclComputationResultId);
        }

        public EclComputationResult[] GetAllEclComputationResults()
        {
            return Channel.GetAllEclComputationResults();
        }

        public EclComputationResult[] GetAllEclComputationResultsByStage(int stage)
        {
            return Channel.GetAllEclComputationResultsByStage(stage);
        }


        #endregion

        #region BondEclComputationResult

        public BondEclComputationResult UpdateBondEclComputationResult(BondEclComputationResult bondEclComputationResult)
        {
            return Channel.UpdateBondEclComputationResult(bondEclComputationResult);
        }

        public void DeleteBondEclComputationResult(int bondEclComputationResultId)
        {
            Channel.DeleteBondEclComputationResult(bondEclComputationResultId);
        }

        public BondEclComputationResult GetBondEclComputationResult(int bondEclComputationResultId)
        {
            return Channel.GetBondEclComputationResult(bondEclComputationResultId);
        }

        public BondEclComputationResult[] GetAllBondEclComputationResults()
        {
            return Channel.GetAllBondEclComputationResults();
        }


        #endregion

        #region TBillEclComputationResult

        public TBillEclComputationResult UpdateTBillEclComputationResult(TBillEclComputationResult tBillEclComputationResult)
        {
            return Channel.UpdateTBillEclComputationResult(tBillEclComputationResult);
        }

        public void DeleteTBillEclComputationResult(int tBillEclComputationResultId)
        {
            Channel.DeleteTBillEclComputationResult(tBillEclComputationResultId);
        }

        public TBillEclComputationResult GetTBillEclComputationResult(int tBillEclComputationResultId)
        {
            return Channel.GetTBillEclComputationResult(tBillEclComputationResultId);
        }

        public TBillEclComputationResult[] GetAllTBillEclComputationResults()
        {
            return Channel.GetAllTBillEclComputationResults();
        }


        #endregion

        #region PlacementEclComputationResult

        public PlacementEclComputationResult UpdatePlacementEclComputationResult(PlacementEclComputationResult placementEclComputationResult)
        {
            return Channel.UpdatePlacementEclComputationResult(placementEclComputationResult);
        }

        public void DeletePlacementEclComputationResult(int placementEclComputationResultId)
        {
            Channel.DeletePlacementEclComputationResult(placementEclComputationResultId);
        }

        public PlacementEclComputationResult GetPlacementEclComputationResult(int placementEclComputationResultId)
        {
            return Channel.GetPlacementEclComputationResult(placementEclComputationResultId);
        }

        public PlacementEclComputationResult[] GetAllPlacementEclComputationResults()
        {
            return Channel.GetAllPlacementEclComputationResults();
        }


        #endregion

        #region LcBgEclComputationResult

        public LcBgEclComputationResult UpdateLcBgEclComputationResult(LcBgEclComputationResult lcBgEclComputationResult)
        {
            return Channel.UpdateLcBgEclComputationResult(lcBgEclComputationResult);
        }

        public void DeleteLcBgEclComputationResult(int lcBgEclComputationResultId)
        {
            Channel.DeleteLcBgEclComputationResult(lcBgEclComputationResultId);
        }

        public LcBgEclComputationResult GetLcBgEclComputationResult(int lcBgEclComputationResultId)
        {
            return Channel.GetLcBgEclComputationResult(lcBgEclComputationResultId);
        }

        public LcBgEclComputationResult[] GetAllLcBgEclComputationResults()
        {
            return Channel.GetAllLcBgEclComputationResults();
        }


        #endregion
        
        //End Victor Segment

        #region ComputedForcastedPDLGD

        public ComputedForcastedPDLGD UpdateComputedForcastedPDLGD(ComputedForcastedPDLGD computedForcastedPDLGD)
        {
            return Channel.UpdateComputedForcastedPDLGD(computedForcastedPDLGD);
        }

        public void DeleteComputedForcastedPDLGD(int computedPDId)
        {
            Channel.DeleteComputedForcastedPDLGD(computedPDId);
        }

        public ComputedForcastedPDLGD GetComputedForcastedPDLGD(int computedPDId)
        {
            return Channel.GetComputedForcastedPDLGD(computedPDId);
        }

        public ComputedForcastedPDLGD[] GetAllComputedForcastedPDLGDs()
        {
            return Channel.GetAllComputedForcastedPDLGDs();
        }

        public ComputedForcastedPDLGD[] GetListComputedForcastedPDLGDs()
        {
            return Channel.GetListComputedForcastedPDLGDs();
        }


        #endregion

        #region ComputedForcastedPDLGDForeign

        public ComputedForcastedPDLGDForeign UpdateComputedForcastedPDLGDForeign(ComputedForcastedPDLGDForeign computedForcastedPDLGDForeign)
        {
            return Channel.UpdateComputedForcastedPDLGDForeign(computedForcastedPDLGDForeign);
        }

        public void DeleteComputedForcastedPDLGDForeign(int computedPDId)
        {
            Channel.DeleteComputedForcastedPDLGDForeign(computedPDId);
        }

        public ComputedForcastedPDLGDForeign GetComputedForcastedPDLGDForeign(int computedPDId)
        {
            return Channel.GetComputedForcastedPDLGDForeign(computedPDId);
        }

        public ComputedForcastedPDLGDForeign[] GetAllComputedForcastedPDLGDForeigns()
        {
            return Channel.GetAllComputedForcastedPDLGDForeigns();
        }

        public ComputedForcastedPDLGDForeign[] GetListComputedForcastedPDLGDForeigns()
        {
            return Channel.GetListComputedForcastedPDLGDForeigns();
        }


        #endregion

        #region MacroEconomicsNPL

        public MacroEconomicsNPL UpdateMacroEconomicsNPL(MacroEconomicsNPL macroEconomicsNPL)
        {
            return Channel.UpdateMacroEconomicsNPL(macroEconomicsNPL);
        }

        public void DeleteMacroEconomicsNPL(int macroeconomicnplId)
        {
            Channel.DeleteMacroEconomicsNPL(macroeconomicnplId);
        }

        public MacroEconomicsNPL GetMacroEconomicsNPL(int macroeconomicnplId)
        {
            return Channel.GetMacroEconomicsNPL(macroeconomicnplId);
        }
        public MacroEconomicsNPL[] GetMacroEconomicsNPLByScenario(string scenario)
        {
            return Channel.GetMacroEconomicsNPLByScenario(scenario);
        }

        public MacroEconomicsNPL[] GetAllMacroEconomicsNPLs()
        {
            return Channel.GetAllMacroEconomicsNPLs();
        }

        #endregion

        #region MonthlyDiscountFactor

        public MonthlyDiscountFactor UpdateMonthlyDiscountFactor(MonthlyDiscountFactor monthlyDiscountFactor)
        {
            return Channel.UpdateMonthlyDiscountFactor(monthlyDiscountFactor);
        }

        public void DeleteMonthlyDiscountFactor(int MonthlyDiscountFactor_Id)
        {
            Channel.DeleteMonthlyDiscountFactor(MonthlyDiscountFactor_Id);
        }

        public MonthlyDiscountFactor GetMonthlyDiscountFactor(int MonthlyDiscountFactor_Id)
        {
            return Channel.GetMonthlyDiscountFactor(MonthlyDiscountFactor_Id);
        }

        public MonthlyDiscountFactor[] GetAllMonthlyDiscountFactors()
        {
            return Channel.GetAllMonthlyDiscountFactors();
        }

        public MonthlyDiscountFactor[] GetMonthlyDiscountFactorByRefNo(string RefNo)
        {
            return Channel.GetMonthlyDiscountFactorByRefNo(RefNo);
        }


        #endregion

        #region MonthlyDiscountFactorBond

        public MonthlyDiscountFactorBond UpdateMonthlyDiscountFactorBond(MonthlyDiscountFactorBond monthlyDiscountFactorBond)
        {
            return Channel.UpdateMonthlyDiscountFactorBond(monthlyDiscountFactorBond);
        }

        public void DeleteMonthlyDiscountFactorBond(int MonthlyDiscountFactorBond_Id)
        {
            Channel.DeleteMonthlyDiscountFactorBond(MonthlyDiscountFactorBond_Id);
        }

        public MonthlyDiscountFactorBond GetMonthlyDiscountFactorBond(int MonthlyDiscountFactorBond_Id)
        {
            return Channel.GetMonthlyDiscountFactorBond(MonthlyDiscountFactorBond_Id);
        }

        public MonthlyDiscountFactorBond[] GetAllMonthlyDiscountFactorBonds()
        {
            return Channel.GetAllMonthlyDiscountFactorBonds();
        }

        public MonthlyDiscountFactorBond[] GetMonthlyDiscountFactorBondByRefNo(string RefNo)
        {
            return Channel.GetMonthlyDiscountFactorBondByRefNo(RefNo);
        }


        #endregion

        #region MonthlyDiscountFactorPlacement

        public MonthlyDiscountFactorPlacement UpdateMonthlyDiscountFactorPlacement(MonthlyDiscountFactorPlacement monthlyDiscountFactor)
        {
            return Channel.UpdateMonthlyDiscountFactorPlacement(monthlyDiscountFactor);
        }

        public void DeleteMonthlyDiscountFactorPlacement(int MonthlyDiscountFactorPlacement_Id)
        {
            Channel.DeleteMonthlyDiscountFactorPlacement(MonthlyDiscountFactorPlacement_Id);
        }

        public MonthlyDiscountFactorPlacement GetMonthlyDiscountFactorPlacement(int MonthlyDiscountFactorPlacement_Id)
        {
            return Channel.GetMonthlyDiscountFactorPlacement(MonthlyDiscountFactorPlacement_Id);
        }

        public MonthlyDiscountFactorPlacement[] GetAllMonthlyDiscountFactorPlacements()
        {
            return Channel.GetAllMonthlyDiscountFactorPlacements();
        }

        public MonthlyDiscountFactorPlacement[] GetMonthlyDiscountFactorPlacementByRefNo(string RefNo)
        {
            return Channel.GetMonthlyDiscountFactorPlacementByRefNo(RefNo);
        }


        #endregion

        #region IfrsPdSeriesByRating

        public IfrsPdSeriesByRating UpdateIfrsPdSeriesByRating(IfrsPdSeriesByRating IfrsPdSeriesByRating)
        {
            return Channel.UpdateIfrsPdSeriesByRating(IfrsPdSeriesByRating);
        }
        public void DeleteIfrsPdSeriesByRating(int IfrsPdSeriesByRatingId)
        {
            Channel.DeleteIfrsPdSeriesByRating(IfrsPdSeriesByRatingId);
        }

        public IfrsPdSeriesByRating GetIfrsPdSeriesBySno(int Sno)
        {
            return Channel.GetIfrsPdSeriesBySno(Sno);
        }

        public IfrsPdSeriesByRating[] GetAllIfrsPdSeriesByRatings()
        {
            return Channel.GetAllIfrsPdSeriesByRatings();
        }

        public string[] GetAllRatings() 
        {
            return Channel.GetAllRatings();
        }

        public IfrsPdSeriesByRating[] GetIfrsPdSeriesByRating(string code)
        {
            return Channel.GetIfrsPdSeriesByRating(code);
        }

        #endregion

        #region IfrsRetailPdSeries

        public IfrsRetailPdSeries UpdateIfrsRetailPdSeries(IfrsRetailPdSeries IfrsRetailPdSeries)
        {
            return Channel.UpdateIfrsRetailPdSeries(IfrsRetailPdSeries);
        }
        public void DeleteIfrsRetailPdSeries(int PdSeriesId)
        {
            Channel.DeleteIfrsRetailPdSeries(PdSeriesId);
        }

        public IfrsRetailPdSeries GetIfrsRetailPdSeriesById(int PdSeriesId)
        {
            return Channel.GetIfrsRetailPdSeriesById(PdSeriesId);
        }

        public IfrsRetailPdSeries[] GetAvailableIfrsRetailPdSeries(QueryOptions queryOptions)
        {
            return Channel.GetAvailableIfrsRetailPdSeries(queryOptions);
        }

        #endregion

        #region IfrsLgdScenarioByInstrument

        public IfrsLgdScenarioByInstrument UpdateIfrsLgdScenarioByInstrument(IfrsLgdScenarioByInstrument IfrsLgdScenarioByInstrument)
        {
            return Channel.UpdateIfrsLgdScenarioByInstrument(IfrsLgdScenarioByInstrument);
        }
        public void DeleteIfrsLgdScenarioByInstrument(int InstrumentId)
        {
            Channel.DeleteIfrsLgdScenarioByInstrument(InstrumentId);
        }

        public IfrsLgdScenarioByInstrument GetIfrsLgdScenarioByInstrumentId(int InstrumentId)
        {
            return Channel.GetIfrsLgdScenarioByInstrumentId(InstrumentId);
        }

        public IfrsLgdScenarioByInstrument[] GetAllIfrsLgdScenarioByInstruments()
        {
            return Channel.GetAllIfrsLgdScenarioByInstruments();
        }

        public string[] GetAllInstruments()
        {
            return Channel.GetAllInstruments();
        }

        public IfrsLgdScenarioByInstrument[] GetIfrsLgdScenarioByInstrument(string code)
        {
            return Channel.GetIfrsLgdScenarioByInstrument(code);
        }

        #endregion

        #region MacroVarRecoveryRates

        public MacroVarRecoveryRates UpdateMacroVarRecoveryRates(MacroVarRecoveryRates MacroVarRecoveryRates)
        {
            return Channel.UpdateMacroVarRecoveryRates(MacroVarRecoveryRates);
        }
        public void DeleteMacroVarRecoveryRates(int InstrumentId)
        {
            Channel.DeleteMacroVarRecoveryRates(InstrumentId);
        }

        public MacroVarRecoveryRates GetMacroVarRecoveryRatesById(int RecoveryRatesId)
        {
            return Channel.GetMacroVarRecoveryRatesById(RecoveryRatesId);
        }

        public MacroVarRecoveryRates[] GetAllMacroVarRecoveryRates()
        {
            return Channel.GetAllMacroVarRecoveryRates();
        }

        #endregion

        #region IfrsCorporateEcl

        public IfrsCorporateEcl UpdateIfrsCorporateEcl(IfrsCorporateEcl IfrsCorporateEcl)
        {
            return Channel.UpdateIfrsCorporateEcl(IfrsCorporateEcl);
        }
        public void DeleteIfrsCorporateEcl(int IfrsCorporateEclId)
        {
            Channel.DeleteIfrsCorporateEcl(IfrsCorporateEclId);
        }

        public IfrsCorporateEcl GetIfrsCorporateEclById(int id)
        {
            return Channel.GetIfrsCorporateEclById(id);
        }

        public IfrsCorporateEcl[] GetAllIfrsCorporateEcls(bool export)
        {
            return Channel.GetAllIfrsCorporateEcls(export);
        }

        public IfrsCorporateEcl[] GetIfrsCorporateEclByRefNo(string refNo)
        {
            return Channel.GetIfrsCorporateEclByRefNo(refNo);
        }

        #endregion

        #region InvestmentOthersECL

        public InvestmentOthersECL UpdateInvestmentOthersECL(InvestmentOthersECL investmentOthersECL)
        {
            return Channel.UpdateInvestmentOthersECL(investmentOthersECL);
        }

        public void DeleteInvestmentOthersECL(int ecl_Id)
        {
            Channel.DeleteInvestmentOthersECL(ecl_Id);
        }

        public InvestmentOthersECL GetInvestmentOthersECL(int ecl_Id)
        {
            return Channel.GetInvestmentOthersECL(ecl_Id);
        }

        public InvestmentOthersECL[] GetAllInvestmentOthersECLs()
        {
            return Channel.GetAllInvestmentOthersECLs();
        }

        public InvestmentOthersECL[] GetInvestmentOthersECLByRefNo(string RefNo)
        {
            return Channel.GetInvestmentOthersECLByRefNo(RefNo);
        }


        #endregion

        #region IfrsSectorCCF

        public IfrsSectorCCF UpdateIfrsSectorCCF(IfrsSectorCCF IfrsSectorCCF)
        {
            return Channel.UpdateIfrsSectorCCF(IfrsSectorCCF);
        }
        public void DeleteIfrsSectorCCF(int InstrumentId)
        {
            Channel.DeleteIfrsSectorCCF(InstrumentId);
        }

        public IfrsSectorCCF GetIfrsSectorCCFById(int SectorId)
        {
            return Channel.GetIfrsSectorCCFById(SectorId);
        }

        public IfrsSectorCCF[] GetAllIfrsSectorCCFs(string Type)
        {
            return Channel.GetAllIfrsSectorCCFs(Type);
        }

        public Sector[] GetAllSectorsForCCF()
        {
            return Channel.GetAllSectorsForCCF();
        }

        #endregion
        
        #region ProbabilityWeighted

        public ProbabilityWeighted UpdateProbabilityWeighted(ProbabilityWeighted probabilityWeighted)
        {
            return Channel.UpdateProbabilityWeighted(probabilityWeighted);
        }

        public void DeleteProbabilityWeighted(int ProbabilityWeighted_Id)
        {
            Channel.DeleteProbabilityWeighted(ProbabilityWeighted_Id);
        }

        public ProbabilityWeighted GetProbabilityWeighted(int ProbabilityWeighted_Id)
        {
            return Channel.GetProbabilityWeighted(ProbabilityWeighted_Id);
        }

        public ProbabilityWeighted[] GetAllProbabilityWeighteds()
        {
            return Channel.GetAllProbabilityWeighteds();
        }

        public ProbabilityWeighted[] GetProbabilityWeightedByInstrumentType(string InstrumentType)
        {
            return Channel.GetProbabilityWeightedByInstrumentType(InstrumentType);
        }


        #endregion

        #region MacrovariableEstimate

        public MacrovariableEstimate UpdateMacrovariableEstimate(MacrovariableEstimate macrovariableEstimate)
        {
            return Channel.UpdateMacrovariableEstimate(macrovariableEstimate);
        }

        public void DeleteMacrovariableEstimate(int MacrovariableEstimate_Id)
        {
            Channel.DeleteMacrovariableEstimate(MacrovariableEstimate_Id);
        }

        public MacrovariableEstimate GetMacrovariableEstimate(int MacrovariableEstimate_Id)
        {
            return Channel.GetMacrovariableEstimate(MacrovariableEstimate_Id);
        }

        public MacrovariableEstimate[] GetAllMacrovariableEstimates()
        {
            return Channel.GetAllMacrovariableEstimates();
        }

        public MacrovariableEstimate[] GetMacrovariableEstimateByCategory(string Category)
        {
            return Channel.GetMacrovariableEstimateByCategory(Category);
        }


        #endregion

        #region SectorMapping

        public SectorMapping UpdateSectorMapping(SectorMapping sectorMapping)
        {
            return Channel.UpdateSectorMapping(sectorMapping);
        }

        public void DeleteSectorMapping(int SectorMapping_Id)
        {
            Channel.DeleteSectorMapping(SectorMapping_Id);
        }

        public SectorMapping GetSectorMapping(int SectorMapping_Id)
        {
            return Channel.GetSectorMapping(SectorMapping_Id);
        }

        public SectorMapping[] GetAllSectorMappings()
        {
            return Channel.GetAllSectorMappings();
        }

        //public SectorMapping[] GetSectorMappingBySource(string Source)
        //{
        //    return Channel.GetSectorMappingBySource(Source);
        //}


        #endregion

        #region Sector

        public Sector UpdateSector(Sector sector)
        {
            return Channel.UpdateSector(sector);
        }

        public void DeleteSector(int sectorId)
        {
            Channel.DeleteSector(sectorId);
        }

        public Sector GetSector(int sectorId)
        {
            return Channel.GetSector(sectorId);
        }

        public Sector[] GetAllSectors()
        {
            return Channel.GetAllSectors();
        }

        public Sector[] GetSectorBySource(string Source)
        {
            return Channel.GetSectorBySource(Source);
        }


        #endregion

        #region Assumption

        public Assumption UpdateAssumption(Assumption assumption)
        {
            return Channel.UpdateAssumption(assumption);
        }

        public void DeleteAssumption(int assumptionId)
        {
            Channel.DeleteAssumption(assumptionId);
        }

        public Assumption GetAssumption(int assumptionId)
        {
            return Channel.GetAssumption(assumptionId);
        }

        public Assumption[] GetAllAssumptions()
        {
            return Channel.GetAllAssumptions();
        }

        #endregion

        #region SandPRating

        public SandPRating UpdateSandPRating(SandPRating sandPRating)
        {
            return Channel.UpdateSandPRating(sandPRating);
        }

        public void DeleteSandPRating(int SandPRating_Id)
        {
            Channel.DeleteSandPRating(SandPRating_Id);
        }

        public SandPRating GetSandPRating(int SandPRating_Id)
        {
            return Channel.GetSandPRating(SandPRating_Id);
        }

        public SandPRating[] GetAllSandPRatings()
        {
            return Channel.GetAllSandPRatings();
        }

        #endregion

        #region HistoricalDefaultedAccounts

        public HistoricalDefaultedAccounts UpdateHistoricalDefaultedAccount(HistoricalDefaultedAccounts historicalDefaultedAccounts)
        {
            return Channel.UpdateHistoricalDefaultedAccount(historicalDefaultedAccounts);
        }

        public void DeleteHistoricalDefaultedAccount(int DefaultedAccountId)
        {
            Channel.DeleteHistoricalDefaultedAccount(DefaultedAccountId);
        }

        public HistoricalDefaultedAccounts GetHistoricalDefaultedAccount(int DefaultedAccountId)
        {
            return Channel.GetHistoricalDefaultedAccount(DefaultedAccountId);
        }

        public HistoricalDefaultedAccounts[] GetAllHistoricalDefaultedAccounts()
        {
            return Channel.GetAllHistoricalDefaultedAccounts();
        }

        #endregion

        #region OffBalancesheetECL

        public OffBalancesheetECL UpdateOffBalancesheetECL(OffBalancesheetECL offBalancesheetECL)
        {
            return Channel.UpdateOffBalancesheetECL(offBalancesheetECL);
        }

        public void DeleteOffBalancesheetECL(int obe_Id)
        {
            Channel.DeleteOffBalancesheetECL(obe_Id);
        }

        public OffBalancesheetECL GetOffBalancesheetECL(int obe_Id)
        {
            return Channel.GetOffBalancesheetECL(obe_Id);
        }

        public OffBalancesheetECL[] GetAllOffBalancesheetECLs()
        {
            return Channel.GetAllOffBalancesheetECLs();
        }

        public OffBalancesheetECL[] GetOffBalancesheetECLBySearch(string SearchParam)
        {
            return Channel.GetOffBalancesheetECLBySearch(SearchParam);
        }


        #endregion

        #region ImpairmentResultOBE

        public ImpairmentResultOBE UpdateImpairmentResultOBE(ImpairmentResultOBE impairmentResultOBE)
        {
            return Channel.UpdateImpairmentResultOBE(impairmentResultOBE);
        }

        public void DeleteImpairmentResultOBE(int Impairment_Id)
        {
            Channel.DeleteImpairmentResultOBE(Impairment_Id);
        }

        public ImpairmentResultOBE GetImpairmentResultOBE(int Impairment_Id)
        {
            return Channel.GetImpairmentResultOBE(Impairment_Id);
        }

        public ImpairmentResultOBE[] GetAllImpairmentResultOBEs()
        {
            return Channel.GetAllImpairmentResultOBEs();
        }

        public ImpairmentResultOBE[] GetImpairmentResultOBEBySearch(string SearchParam)
        {
            return Channel.GetImpairmentResultOBEBySearch(SearchParam);
        }


        #endregion

        #region ImpairmentResultRetail

        public ImpairmentResultRetail UpdateImpairmentResultRetail(ImpairmentResultRetail impairmentResultRetail)
        {
            return Channel.UpdateImpairmentResultRetail(impairmentResultRetail);
        }

        public void DeleteImpairmentResultRetail(int Impairment_Id)
        {
            Channel.DeleteImpairmentResultRetail(Impairment_Id);
        }

        public ImpairmentResultRetail GetImpairmentResultRetail(int Impairment_Id)
        {
            return Channel.GetImpairmentResultRetail(Impairment_Id);
        }

        public ImpairmentResultRetail[] GetAllImpairmentResultRetails()
        {
            return Channel.GetAllImpairmentResultRetails();
        }

        public ImpairmentResultRetail[] GetImpairmentResultRetailBySearch(string SearchParam)
        {
            return Channel.GetImpairmentResultRetailBySearch(SearchParam);
        }


        #endregion

        #region ImpairmentInvestment

        public ImpairmentInvestment UpdateImpairmentInvestment(ImpairmentInvestment impairmentInvestment)
        {
            return Channel.UpdateImpairmentInvestment(impairmentInvestment);
        }

        public void DeleteImpairmentInvestment(int Investment_Id)
        {
            Channel.DeleteImpairmentInvestment(Investment_Id);
        }

        public ImpairmentInvestment GetImpairmentInvestment(int Investment_Id)
        {
            return Channel.GetImpairmentInvestment(Investment_Id);
        }

        public ImpairmentInvestment[] GetAllImpairmentInvestments()
        {
            return Channel.GetAllImpairmentInvestments();
        }


        #endregion

        #region ImpairmentCorporate

        public ImpairmentCorporate UpdateImpairmentCorporate(ImpairmentCorporate impairmentCorporate)
        {
            return Channel.UpdateImpairmentCorporate(impairmentCorporate);
        }

        public void DeleteImpairmentCorporate(int Corporate_Id)
        {
            Channel.DeleteImpairmentCorporate(Corporate_Id);
        }

        public ImpairmentCorporate GetImpairmentCorporate(int Corporate_Id)
        {
            return Channel.GetImpairmentCorporate(Corporate_Id);
        }

        public ImpairmentCorporate[] GetAllImpairmentCorporates()
        {
            return Channel.GetAllImpairmentCorporates();
        }


        #endregion

        #region IfrsFinalRetailOutput

        public IfrsFinalRetailOutput UpdateIfrsFinalRetailOutput(IfrsFinalRetailOutput IfrsFinalRetailOutput)
        {
            return Channel.UpdateIfrsFinalRetailOutput(IfrsFinalRetailOutput);
        }
        public void DeleteIfrsFinalRetailOutput(int IfrsFinalRetailOutputId)
        {
            Channel.DeleteIfrsFinalRetailOutput(IfrsFinalRetailOutputId);
        }

        public IfrsFinalRetailOutput GetIfrsFinalRetailOutput(int Id)
        {
            return Channel.GetIfrsFinalRetailOutput(Id);
        }

        public IfrsFinalRetailOutput[] GetAllIfrsFinalRetailOutput()
        {
            return Channel.GetAllIfrsFinalRetailOutput();
        }

        public IfrsFinalRetailOutput[] GetIfrsFinalRetailOutputByAccountNo(string accountNo)
        {
            return Channel.GetIfrsFinalRetailOutputByAccountNo(accountNo);
        }

        #endregion
        
        #region IfrsCustomerPD

        public IfrsCustomerPD UpdateIfrsCustomerPD(IfrsCustomerPD IfrsCustomerPD)
        {
            return Channel.UpdateIfrsCustomerPD(IfrsCustomerPD);
        }
        public void DeleteIfrsCustomerPD(int IfrsCustomerPDId)
        {
            Channel.DeleteIfrsCustomerPD(IfrsCustomerPDId);
        }

        public IfrsCustomerPD GetIfrsCustomerPD(int CustomerPDId)
        {
            return Channel.GetIfrsCustomerPD(CustomerPDId);
        }

        public IfrsCustomerPD[] GetAllIfrsCustomerPDs()
        {
            return Channel.GetAllIfrsCustomerPDs();
        }

        public string[] GetAllCustomerRatings()
        {
            return Channel.GetAllCustomerRatings();
        }

        public IfrsCustomerPD[] GetIfrsCustomerPDByRating(string rating)
        {
            return Channel.GetIfrsCustomerPDByRating(rating);
        }

#endregion

        #region IfrsCorporatePdSeries

        public IfrsCorporatePdSeries UpdateIfrsCorporatePdSeries(IfrsCorporatePdSeries IfrsCorporatePdSeries)
        {
            return Channel.UpdateIfrsCorporatePdSeries(IfrsCorporatePdSeries);
        }
        public void DeleteIfrsCorporatePdSeries(int PdSeriesId)
        {
            Channel.DeleteIfrsCorporatePdSeries(PdSeriesId);
        }

        public IfrsCorporatePdSeries GetIfrsCorporatePdSeriesById(int Sno)
        {
            return Channel.GetIfrsCorporatePdSeriesById(Sno);
        }

        public IfrsCorporatePdSeries[] GetAvailableIfrsCorporatePdSeries(QueryOptions queryOptions)
        {
            return Channel.GetAvailableIfrsCorporatePdSeries(queryOptions);
        }

        public IfrsCorporatePdSeries[] GetAllIfrsCorporatePdSeries()
        {
            return Channel.GetAllIfrsCorporatePdSeries();
        }

        public string GetExportIfrsCorporatePdSeries(string Path)
        {
            return Channel.GetExportIfrsCorporatePdSeries(Path);
        }


        #endregion

        #region ECLInputRetail

        public ECLInputRetail UpdatEclInputRetail(ECLInputRetail eclInputRetail)
        {
            return Channel.UpdatEclInputRetail(eclInputRetail);
        }

        public void DeleteEclInputRetail(int eclInputRetailId)
        {
            Channel.DeleteEclInputRetail(eclInputRetailId);
        }

        public ECLInputRetail GetEclInputRetail(int eclInputRetailId)
        {
            return Channel.GetEclInputRetail(eclInputRetailId);
        }

        public ECLInputRetail[] GetAllEclInputRetails()
        {
            return Channel.GetAllEclInputRetails();
        }

        public ECLInputRetail[] GetAllEclInputRetailsByRefno(string refNo)
        {
            return Channel.GetAllEclInputRetailsByRefno(refNo);
        }


        #endregion

        #region StaffLoansComputationResult

        public StaffLoansComputationResult UpdateStaffLoansComputationResult(StaffLoansComputationResult staffLoansComputationResult)
        {
            return Channel.UpdateStaffLoansComputationResult(staffLoansComputationResult);
        }

        public void DeleteStaffLoansComputationResult(int StaffLoan_Id)
        {
            Channel.DeleteStaffLoansComputationResult(StaffLoan_Id);
        }

        public StaffLoansComputationResult GetStaffLoansComputationResult(int StaffLoan_Id)
        {
            return Channel.GetStaffLoansComputationResult(StaffLoan_Id);
        }

        public StaffLoansComputationResult[] GetAllStaffLoansComputationResults()
        {
            return Channel.GetAllStaffLoansComputationResults();
        }

        public StaffLoansComputationResult[] GetStaffLoansComputationResultBySearch(string SearchParam)
        {
            return Channel.GetStaffLoansComputationResultBySearch(SearchParam);
        }


        #endregion

        #region CollateralInput

        public CollateralInput UpdateCollateralInput(CollateralInput collateralInput)
        {
            return Channel.UpdateCollateralInput(collateralInput);
        }

        public void DeleteCollateralInput(int Collateral_Id)
        {
            Channel.DeleteCollateralInput(Collateral_Id);
        }

        public CollateralInput GetCollateralInput(int Collateral_Id)
        {
            return Channel.GetCollateralInput(Collateral_Id);
        }

        public CollateralInput[] GetAllCollateralInputs()
        {
            return Channel.GetAllCollateralInputs();
        }

        #endregion

        #region SPCumulativePD

        public SPCumulativePD UpdateSPCumulativePD(SPCumulativePD sPCumulativePD)
        {
            return Channel.UpdateSPCumulativePD(sPCumulativePD);
        }

        public void DeleteSPCumulativePD(int SPCumulative_Id)
        {
            Channel.DeleteSPCumulativePD(SPCumulative_Id);
        }

        public SPCumulativePD GetSPCumulativePD(int SPCumulative_Id)
        {
            return Channel.GetSPCumulativePD(SPCumulative_Id);
        }

        public SPCumulativePD[] GetAllSPCumulativePDs()
        {
            return Channel.GetAllSPCumulativePDs();
        }

        #endregion

        #region LoanCommitmentComputationResult

        public LoanCommitmentComputationResult UpdateLoanCommitmentComputationResult(LoanCommitmentComputationResult loanCommitmentComputationResult)
        {
            return Channel.UpdateLoanCommitmentComputationResult(loanCommitmentComputationResult);
        }

        public void DeleteLoanCommitmentComputationResult(int LoanCommitmentComputationResult_Id)
        {
            Channel.DeleteLoanCommitmentComputationResult(LoanCommitmentComputationResult_Id);
        }

        public LoanCommitmentComputationResult GetLoanCommitmentComputationResult(int LoanCommitmentComputationResult_Id)
        {
            return Channel.GetLoanCommitmentComputationResult(LoanCommitmentComputationResult_Id);
        }

        public LoanCommitmentComputationResult[] GetAllLoanCommitmentComputationResults()
        {
            return Channel.GetAllLoanCommitmentComputationResults();
        }

        #endregion

        #region LgdInputFactor

        public LgdInputFactor UpdateLgdInputFactor(LgdInputFactor collateralInput)
        {
            return Channel.UpdateLgdInputFactor(collateralInput);
        }

        public void DeleteLgdInputFactor(int Collateral_Id)
        {
            Channel.DeleteLgdInputFactor(Collateral_Id);
        }

        public LgdInputFactor GetLgdInputFactor(int Collateral_Id)
        {
            return Channel.GetLgdInputFactor(Collateral_Id);
        }

        public LgdInputFactor[] GetAllLgdInputFactors()
        {
            return Channel.GetAllLgdInputFactors();
        }

        #endregion

        #region RegressionOutput

        public RegressionOutput UpdateRegressionOutput(RegressionOutput regressionOutput)
        {
            return Channel.UpdateRegressionOutput(regressionOutput);
        }

        public void DeleteRegressionOutput(int RegressionOutputId)
        {
            Channel.DeleteRegressionOutput(RegressionOutputId);
        }

        public RegressionOutput GetRegressionOutput(int RegressionOutputId)
        {
            return Channel.GetRegressionOutput(RegressionOutputId);
        }

        public RegressionOutput[] GetAllRegressionOutputs()
        {
            return Channel.GetAllRegressionOutputs();
        }

        #endregion

        #region MacroeconomicsVariableScenario

        public MacroeconomicsVariableScenario UpdateMacroeconomicsVariableScenario(MacroeconomicsVariableScenario macroeconomicsVariableScenario)
        {
            return Channel.UpdateMacroeconomicsVariableScenario(macroeconomicsVariableScenario);
        }

        public void DeleteMacroeconomicsVariableScenario(int MacroeconomicsId)
        {
            Channel.DeleteMacroeconomicsVariableScenario(MacroeconomicsId);
        }

        public MacroeconomicsVariableScenario GetMacroeconomicsVariableScenario(int MacroeconomicsId)
        {
            return Channel.GetMacroeconomicsVariableScenario(MacroeconomicsId);
        }

        public MacroeconomicsVariableScenario[] GetAllMacroeconomicsVariableScenarios()
        {
            return Channel.GetAllMacroeconomicsVariableScenarios();
        }

        public MacroeconomicsVariableScenario[] GetMacroeconomicsVariableScenariosByFlag(int flag)
        {
            return Channel.GetMacroeconomicsVariableScenariosByFlag(flag);
        }

        #endregion

        #region FacilityStaging

        public FacilityStaging UpdateFacilityStaging(FacilityStaging facilityStaging)
        {
            return Channel.UpdateFacilityStaging(facilityStaging);
        }

        public void DeleteFacilityStaging(int facId)
        {
            Channel.DeleteFacilityStaging(facId);
        }

        public FacilityStaging GetFacilityStaging(int facId)
        {
            return Channel.GetFacilityStaging(facId);
        }

        public FacilityStaging[] GetAllFacilityStagings(int defaultCount, string path)
        {
            return Channel.GetAllFacilityStagings(defaultCount, path);
        }

        public FacilityStaging[] GetEntityByParam(string param)
        {
            return Channel.GetEntityByParam(param);
        }


        #endregion

        #region FacilityClassification

        public FacilityClassification UpdateFacilityClassification(FacilityClassification facClassification)
        {
            return Channel.UpdateFacilityClassification(facClassification);
        }

        public void DeleteFacilityClassification(int facClassId)
        {
            Channel.DeleteFacilityClassification(facClassId);
        }

        public FacilityClassification GetFacilityClassificationbyId(int facClassId)
        {
            return Channel.GetFacilityClassificationbyId(facClassId);
        }
        public FacilityClassification[] GetFacilityClassification(int defaultCount,string type)
        {
            return Channel.GetFacilityClassification(defaultCount,type);
        }

        public FacilityClassification[] GetFacilityClassificationBySearch(string type, string searchParam)
        {
            return Channel.GetFacilityClassificationBySearch(type, searchParam);
        }

        public string[] GetProductTypes()
        {
            return Channel.GetProductTypes();
        }

        public string[] GetsubTypes(string productType)
        {
            return Channel.GetsubTypes(productType);
        }
        #endregion

        #region SICR

        public SICRParameters UpdateSICRParameters(SICRParameters sicrParameters)
        {
            return Channel.UpdateSICRParameters(sicrParameters);
        }

        public void DeleteSICRParameters(int sicrParametersId)
        {
            Channel.DeleteSICRParameters(sicrParametersId);
        }

        public SICRParameters GetSICRParameters(int sicrParametersId)
        {
            return Channel.GetSICRParameters(sicrParametersId);
        }

        public SICRParameters[] GetAllSICRParameters()
        {
            return Channel.GetAllSICRParameters();
        }


        #endregion

        #region ObeEclComputation

        public ObeEclComputation UpdateObeEclComputation(ObeEclComputation obeeclcomputation)
        {
            return Channel.UpdateObeEclComputation(obeeclcomputation);
        }

        public void DeleteObeEclComputation(int Id)
        {
            Channel.DeleteObeEclComputation(Id);
        }

        public ObeEclComputation GetObeEclComputation(int Id)
        {
            return Channel.GetObeEclComputation(Id);
        }

        public ObeEclComputation[] GetAllObeEclComputation()
        {
            return Channel.GetAllObeEclComputation();
        }

        public ObeEclComputation[] GetObeEclComputationBySearch(string searchParam, string path)
        {
            return Channel.GetObeEclComputationBySearch(searchParam, path);
        }

        public ObeEclComputation[] GetObeEclComputations(int defaultCount, string path)
        {
            return Channel.GetObeEclComputations(defaultCount, path);
        }

        #endregion

        #region MarginalCCFStr

        public MarginalCCFStr UpdateMarginalCCFStr(MarginalCCFStr marginalccfstr)
        {
            return Channel.UpdateMarginalCCFStr(marginalccfstr);
        }

        public void DeleteMarginalCCFStr(int Id)
        {
            Channel.DeleteMarginalCCFStr(Id);
        }

        public MarginalCCFStr GetMarginalCCFStr(int Id)
        {
            return Channel.GetMarginalCCFStr(Id);
        }

        public MarginalCCFStr[] GetAllMarginalCCFStr()
        {
            return Channel.GetAllMarginalCCFStr();
        }

        public MarginalCCFStr[] GetMarginalCCFStrBySearch(string searchParam)
        {
            return Channel.GetMarginalCCFStrBySearch(searchParam);
        }

        public MarginalCCFStr[] GetMarginalCCFStrs(int defaultCount)
        {
            return Channel.GetMarginalCCFStrs(defaultCount);
        }

        #endregion

        #region LoanSignificantFlag

        public LoanSignificantFlag UpdateLoanSignificantFlag(LoanSignificantFlag loansignificantflag)
        {
            return Channel.UpdateLoanSignificantFlag(loansignificantflag);
        }

        public void DeleteLoanSignificantFlag(int Id)
        {
            Channel.DeleteLoanSignificantFlag(Id);
        }

        public LoanSignificantFlag GetLoanSignificantFlag(int Id)
        {
            return Channel.GetLoanSignificantFlag(Id);
        }

        public LoanSignificantFlag[] GetAllLoanSignificantFlag()
        {
            return Channel.GetAllLoanSignificantFlag();
        }

        public LoanSignificantFlag[] GetLoanSignificantFlagBySearch(string searchParam)
        {
            return Channel.GetLoanSignificantFlagBySearch(searchParam);
        }

        public LoanSignificantFlag[] GetLoanSignificantFlags(int defaultCount)
        {
            return Channel.GetLoanSignificantFlags(defaultCount);
        }

        #endregion

        #region LoansECLComputationResult

        public LoansECLComputationResult UpdateLoansECLComputationResult(LoansECLComputationResult loanseclcomputationresult)
        {
            return Channel.UpdateLoansECLComputationResult(loanseclcomputationresult);
        }

        public void DeleteLoansECLComputationResult(int Id)
        {
            Channel.DeleteLoansECLComputationResult(Id);
        }

        public LoansECLComputationResult GetLoansECLComputationResult(int Id)
        {
            return Channel.GetLoansECLComputationResult(Id);
        }

        public LoansECLComputationResult[] GetAllLoansECLComputationResult()
        {
            return Channel.GetAllLoansECLComputationResult();
        }

        public LoansECLComputationResult[] GetLoansECLComputationResultBySearch(string searchParam, string path)
        {
            return Channel.GetLoansECLComputationResultBySearch(searchParam, path);
        }

        public LoansECLComputationResult[] GetLoansECLComputationResults(int defaultCount, string path)
        {
            return Channel.GetLoansECLComputationResults(defaultCount, path);
        }

        #endregion
      
        #region IfrsInvestment

        public IfrsInvestment UpdateIfrsInvestment(IfrsInvestment ifrsinvestment)
        {
            return Channel.UpdateIfrsInvestment(ifrsinvestment);
        }

        public void DeleteIfrsInvestment(int Id)
        {
            Channel.DeleteIfrsInvestment(Id);
        }

        public IfrsInvestment GetIfrsInvestment(int Id)
        {
            return Channel.GetIfrsInvestment(Id);
        }

        public IfrsInvestment[] GetAllIfrsInvestments()
        {
            return Channel.GetAllIfrsInvestments();
        }

        #endregion
        
        #region IfrsBondLGD

        public IfrsBondLGD UpdateIfrsBondLGD(IfrsBondLGD ifrsbondlgd)
        {
            return Channel.UpdateIfrsBondLGD(ifrsbondlgd);
        }

        public void DeleteIfrsBondLGD(int Id)
        {
            Channel.DeleteIfrsBondLGD(Id);
        }

        public IfrsBondLGD GetIfrsBondLGD(int Id)
        {
            return Channel.GetIfrsBondLGD(Id);
        }

        public IfrsBondLGD[] GetAllIfrsBondLGD(int defaultcount, string path)
        {
            return Channel.GetAllIfrsBondLGD(defaultcount, path);
        }


        public IfrsBondLGD[] GetRecordByRefNo(string searchParam)
        {
            return Channel.GetRecordByRefNo(searchParam);
        }


        #endregion

        #region MarginalPddSTRLB

        public MarginalPddSTRLB UpdateMarginalPddSTRLB(MarginalPddSTRLB marginalpddstrlb)
        {
            return Channel.UpdateMarginalPddSTRLB(marginalpddstrlb);
        }

        public void DeleteMarginalPddSTRLB(int Id)
        {
            Channel.DeleteMarginalPddSTRLB(Id);
        }

        public MarginalPddSTRLB GetMarginalPddSTRLB(int Id)
        {
            return Channel.GetMarginalPddSTRLB(Id);
        }

        public MarginalPddSTRLB[] GetAllMarginalPddSTRLB()
        {
            return Channel.GetAllMarginalPddSTRLB();
        }

        public MarginalPddSTRLB[] GetMarginalPddSTRLBBySearch(string searchParam)
        {
            return Channel.GetMarginalPddSTRLBBySearch(searchParam);
        }

        public MarginalPddSTRLB[] GetMarginalPddSTRLBs(int defaultCount, string path)
        {
            return Channel.GetMarginalPddSTRLBs(defaultCount, path);
        }

        #endregion

        #region ODEclComputationResult

        public ODEclComputationResult UpdateODEclComputationResult(ODEclComputationResult odeclcomputationresult)
        {
            return Channel.UpdateODEclComputationResult(odeclcomputationresult);
        }

        public void DeleteODEclComputationResult(int Id)
        {
            Channel.DeleteODEclComputationResult(Id);
        }

        public ODEclComputationResult GetODEclComputationResult(int Id)
        {
            return Channel.GetODEclComputationResult(Id);
        }

        public ODEclComputationResult[] GetAllODEclComputationResult()
        {
            return Channel.GetAllODEclComputationResult();
        }

        public ODEclComputationResult[] GetODEclComputationResultBySearch(string searchParam, string path)
        {
            return Channel.GetODEclComputationResultBySearch(searchParam, path);
        }

        public ODEclComputationResult[] GetODEclComputationResults(int defaultCount, string path)
        {
            return Channel.GetODEclComputationResults(defaultCount, path);
        }

        #endregion

        #region MarginalPdObeDistr

        public MarginalPdObeDistr UpdateMarginalPdObeDistr(MarginalPdObeDistr marginalpdobedistr)
        {
            return Channel.UpdateMarginalPdObeDistr(marginalpdobedistr);
        }

        public void DeleteMarginalPdObeDistr(int Id)
        {
            Channel.DeleteMarginalPdObeDistr(Id);
        }

        public MarginalPdObeDistr GetMarginalPdObeDistr(int Id)
        {
            return Channel.GetMarginalPdObeDistr(Id);
        }

        public MarginalPdObeDistr[] GetAllMarginalPdObeDistr()
        {
            return Channel.GetAllMarginalPdObeDistr();
        }

        public MarginalPdObeDistr[] GetMarginalPdObeDistrBySearch(string searchParam)
        {
            return Channel.GetMarginalPdObeDistrBySearch(searchParam);
        }

        public MarginalPdObeDistr[] GetMarginalPdObeDistrs(int defaultCount)
        {
            return Channel.GetMarginalPdObeDistrs(defaultCount);
        }

        #endregion

        #region MarginalPdODDistr

        public MarginalPdODDistr UpdateMarginalPdODDistr(MarginalPdODDistr marginalpdODdistr)
        {
            return Channel.UpdateMarginalPdODDistr(marginalpdODdistr);
        }

        public void DeleteMarginalPdODDistr(int Id)
        {
            Channel.DeleteMarginalPdODDistr(Id);
        }

        public MarginalPdODDistr GetMarginalPdODDistr(int Id)
        {
            return Channel.GetMarginalPdODDistr(Id);
        }

        public MarginalPdODDistr[] GetAllMarginalPdODDistr()
        {
            return Channel.GetAllMarginalPdODDistr();
        }

        public MarginalPdODDistr[] GetMarginalPdODDistrBySearch(string searchParam)
        {
            return Channel.GetMarginalPdODDistrBySearch(searchParam);
        }

        public MarginalPdODDistr[] GetMarginalPdODDistrs(int defaultCount)
        {
            return Channel.GetMarginalPdODDistrs(defaultCount);
        }

        #endregion

        #region LGDComptResult

        public LGDComptResult UpdateLGDComptResult(LGDComptResult lgdcomptresult)
        {
            return Channel.UpdateLGDComptResult(lgdcomptresult);
        }

        public void DeleteLGDComptResult(int Id)
        {
            Channel.DeleteLGDComptResult(Id);
        }

        public LGDComptResult GetLGDComptResult(int Id)
        {
            return Channel.GetLGDComptResult(Id);
        }

        public LGDComptResult[] GetAllLGDComptResult()
        {
            return Channel.GetAllLGDComptResult();
        }

        public LGDComptResult[] GetLGDComptResultBySearch(string searchParam)
        {
            return Channel.GetLGDComptResultBySearch(searchParam);
        }

        public LGDComptResult[] GetLGDComptResults(int defaultCount, string path)
        {
            return Channel.GetLGDComptResults(defaultCount, path);
        }

        #endregion

        #region ObeLGDComptResult

        public ObeLGDComptResult UpdateObeLGDComptResult(ObeLGDComptResult obelgdcomptresult)
        {
            return Channel.UpdateObeLGDComptResult(obelgdcomptresult);
        }

        public void DeleteObeLGDComptResult(int Id)
        {
            Channel.DeleteObeLGDComptResult(Id);
        }

        public ObeLGDComptResult GetObeLGDComptResult(int Id)
        {
            return Channel.GetObeLGDComptResult(Id);
        }

        public ObeLGDComptResult[] GetAllObeLGDComptResult()
        {
            return Channel.GetAllObeLGDComptResult();
        }

        public ObeLGDComptResult[] GetObeLGDComptResultBySearch(string searchParam)
        {
            return Channel.GetObeLGDComptResultBySearch(searchParam);
        }

        public ObeLGDComptResult[] GetObeLGDComptResults(int defaultCount, string path)
        {
            return Channel.GetObeLGDComptResults(defaultCount, path);
        }

        #endregion

        #region IfrsBondsMonthlyEAD

        public IfrsBondsMonthlyEAD UpdateIfrsBondsMonthlyEAD(IfrsBondsMonthlyEAD ifrsbondsmonthlyead)
        {
            return Channel.UpdateIfrsBondsMonthlyEAD(ifrsbondsmonthlyead);
        }

        public void DeleteIfrsBondsMonthlyEAD(int Id)
        {
            Channel.DeleteIfrsBondsMonthlyEAD(Id);
        }

        public IfrsBondsMonthlyEAD GetIfrsBondsMonthlyEAD(int Id)
        {
            return Channel.GetIfrsBondsMonthlyEAD(Id);
        }

        public IfrsBondsMonthlyEAD[] GetAllIfrsBondsMonthlyEAD(int defaultcount)
        {
            return Channel.GetAllIfrsBondsMonthlyEAD(defaultcount);
        }

        public IfrsBondsMonthlyEAD[] GetIfrsBondMonthlyEADBySearch(string searchParam)
        {
            return Channel.GetIfrsBondMonthlyEADBySearch(searchParam);
        }


        #endregion

        #region IfrsMonthlyEAD

        public IfrsMonthlyEAD UpdateIfrsMonthlyEAD(IfrsMonthlyEAD ifrsmonthlyead)
        {
            return Channel.UpdateIfrsMonthlyEAD(ifrsmonthlyead);
        }

        public void DeleteIfrsMonthlyEAD(int Id)
        {
            Channel.DeleteIfrsMonthlyEAD(Id);
        }

        public IfrsMonthlyEAD GetIfrsMonthlyEAD(int Id)
        {
            return Channel.GetIfrsMonthlyEAD(Id);
        }

        public IfrsMonthlyEAD[] GetAllIfrsMonthlyEAD(int defaultcount)
        {
            return Channel.GetAllIfrsMonthlyEAD(defaultcount);
        }

        public IfrsMonthlyEAD[] GetIfrsMonthlyEADBySearch(string searchParam)
        {
            return Channel.GetIfrsMonthlyEADBySearch(searchParam);
        }


        #endregion

        #region LoanClassificationSICRSignFlag

        public LoanClassificationSICRSignFlag UpdateLoanClassificationSICRSignFlag(LoanClassificationSICRSignFlag loanClassSignFlag)
        {
            return Channel.UpdateLoanClassificationSICRSignFlag(loanClassSignFlag);
        }

        public void DeleteLoanClassificationSICRSignFlag(int Id)
        {
            Channel.DeleteLoanClassificationSICRSignFlag(Id);
        }

        public LoanClassificationSICRSignFlag GetLoanClassificationSICRSignFlag(int Id)
        {
            return Channel.GetLoanClassificationSICRSignFlag(Id);
        }
        public LoanClassificationSignificantFlagData[] GetAllLoanClassificationSICRSignFlagData()
        {
            return Channel.GetAllLoanClassificationSICRSignFlagData();
        }

        public KeyValueData[] GetGroupedClassification()
        {
            return Channel.GetGroupedClassification();
        }

        public KeyValueData[] GetSplitClassification(int loanClassId)
        {
            return Channel.GetSplitClassification(loanClassId);
        }

        #endregion

        #region LoanECLResult

        public LoanECLResult UpdateLoanECLResult(LoanECLResult loaneclresult)
        {
            return Channel.UpdateLoanECLResult(loaneclresult);
        }

        public void DeleteLoanECLResult(int InstrumentID)
        {
            Channel.DeleteLoanECLResult(InstrumentID);
        }

        public LoanECLResult GetLoanECLResult(int ID)
        {
            return Channel.GetLoanECLResult(ID);
        }

        public LoanECLResult[] GetAllLoanECLResults()
        {
            return Channel.GetAllLoanECLResults();
        }

        public LoanECLResult[] GetLoanECLResultBySearch(string searchParam)
        {
            return Channel.GetLoanECLResultBySearch(searchParam);
        }

        public LoanECLResult[] GetLoanECLResults(int defaultCount, string path)
        {
            return Channel.GetLoanECLResults(defaultCount, path);
        }


        #endregion

        #region ObeECLResult

        public ObeECLResult UpdateObeECLResult(ObeECLResult obeeclresult)
        {
            return Channel.UpdateObeECLResult(obeeclresult);
        }

        public void DeleteObeECLResult(int InstrumentID)
        {
            Channel.DeleteObeECLResult(InstrumentID);
        }

        public ObeECLResult GetObeECLResult(int ID)
        {
            return Channel.GetObeECLResult(ID);
        }

        public ObeECLResult[] GetAllObeECLResults()
        {
            return Channel.GetAllObeECLResults();
        }

        public ObeECLResult[] GetObeECLResultBySearch(string searchParam)
        {
            return Channel.GetObeECLResultBySearch(searchParam);
        }

        public ObeECLResult[] GetObeECLResults(int defaultCount, string path)
        {
            return Channel.GetObeECLResults(defaultCount, path);
        }


        #endregion

        #region BondsECLResult

        public BondsECLResult UpdateBondsECLResult(BondsECLResult bondseclresult)
        {
            return Channel.UpdateBondsECLResult(bondseclresult);
        }

        public void DeleteBondsECLResult(int InstrumentID)
        {
            Channel.DeleteBondsECLResult(InstrumentID);
        }

        public BondsECLResult GetBondsECLResult(int ID)
        {
            return Channel.GetBondsECLResult(ID);
        }

        public BondsECLResult[] GetAllBondsECLResults()
        {
            return Channel.GetAllBondsECLResults();
        }

        public BondsECLResult[] GetBondsECLResultBySearch(string searchParam)
        {
            return Channel.GetBondsECLResultBySearch(searchParam);
        }

        public BondsECLResult[] GetBondsECLResults(int defaultCount, string path)
        {
            return Channel.GetBondsECLResults(defaultCount, path);
        }


        #endregion

        #region OdECLResult

        public OdECLResult UpdateOdECLResult(OdECLResult odeclresult)
        {
            return Channel.UpdateOdECLResult(odeclresult);
        }

        public void DeleteOdECLResult(int InstrumentID)
        {
            Channel.DeleteOdECLResult(InstrumentID);
        }

        public OdECLResult GetOdECLResult(int ID)
        {
            return Channel.GetOdECLResult(ID);
        }

        public OdECLResult[] GetAllOdECLResults()
        {
            return Channel.GetAllOdECLResults();
        }

        public OdECLResult[] GetOdECLResultBySearch(string searchParam)
        {
            return Channel.GetOdECLResultBySearch(searchParam);
        }

        public OdECLResult[] GetOdECLResults(int defaultCount, string path)
        {
            return Channel.GetOdECLResults(defaultCount, path);
        }


        #endregion
        
        #region CollateralGrowthRate

        public CollateralGrowthRate UpdateCollateralGrowthRate(CollateralGrowthRate collateralgrowthrate)
        {
            return Channel.UpdateCollateralGrowthRate(collateralgrowthrate);
        }

        public void DeleteCollateralGrowthRate(int InstrumentID)
        {
            Channel.DeleteCollateralGrowthRate(InstrumentID);
        }

        public CollateralGrowthRate GetCollateralGrowthRate(int ID)
        {
            return Channel.GetCollateralGrowthRate(ID);
        }

        public CollateralGrowthRate[] GetAllCollateralGrowthRates()
        {
            return Channel.GetAllCollateralGrowthRates();
        }

        public CollateralGrowthRate[] GetCollateralGrowthRateBySearch(string searchParam)
        {
            return Channel.GetCollateralGrowthRateBySearch(searchParam);
        }

        public CollateralGrowthRate[] GetCollateralGrowthRates(int defaultCount)
        {
            return Channel.GetCollateralGrowthRates(defaultCount);
        }


        #endregion

        #region CummulativeDefaultAmt

        public CummulativeDefaultAmt UpdateCummulativeDefaultAmt(CummulativeDefaultAmt cummulativedefaultamt)
        {
            return Channel.UpdateCummulativeDefaultAmt(cummulativedefaultamt);
        }

        public void DeleteCummulativeDefaultAmt(int InstrumentID)
        {
            Channel.DeleteCummulativeDefaultAmt(InstrumentID);
        }

        public CummulativeDefaultAmt GetCummulativeDefaultAmt(int ID)
        {
            return Channel.GetCummulativeDefaultAmt(ID);
        }

        public CummulativeDefaultAmt[] GetAllCummulativeDefaultAmts()
        {
            return Channel.GetAllCummulativeDefaultAmts();
        }

        public CummulativeDefaultAmt[] GetCummulativeDefaultAmtBySearch(string searchParam)
        {
            return Channel.GetCummulativeDefaultAmtBySearch(searchParam);
        }

        public CummulativeDefaultAmt[] GetCummulativeDefaultAmts(int defaultCount, string path)
        {
            return Channel.GetCummulativeDefaultAmts(defaultCount, path);
        }


        #endregion

        #region CummulativeLifetimePd

        public CummulativeLifetimePd UpdateCummulativeLifetimePd(CummulativeLifetimePd cummulativelifetimepd)
        {
            return Channel.UpdateCummulativeLifetimePd(cummulativelifetimepd);
        }

        public void DeleteCummulativeLifetimePd(int InstrumentID)
        {
            Channel.DeleteCummulativeLifetimePd(InstrumentID);
        }

        public CummulativeLifetimePd GetCummulativeLifetimePd(int ID)
        {
            return Channel.GetCummulativeLifetimePd(ID);
        }

        public CummulativeLifetimePd[] GetAllCummulativeLifetimePds()
        {
            return Channel.GetAllCummulativeLifetimePds();
        }

        public CummulativeLifetimePd[] GetCummulativeLifetimePdBySearch(string searchParam)
        {
            return Channel.GetCummulativeLifetimePdBySearch(searchParam);
        }

        public CummulativeLifetimePd[] GetCummulativeLifetimePds(int defaultCount, string path)
        {
            return Channel.GetCummulativeLifetimePds(defaultCount, path);
        }


        #endregion

        #region CollateralRecAmtStaging

        public CollateralRecAmtStaging UpdateCollateralRecAmtStaging(CollateralRecAmtStaging loaneclresult)
        {
            return Channel.UpdateCollateralRecAmtStaging(loaneclresult);
        }

        public void DeleteCollateralRecAmtStaging(int InstrumentID)
        {
            Channel.DeleteCollateralRecAmtStaging(InstrumentID);
        }

        public CollateralRecAmtStaging GetCollateralRecAmtStaging(int ID)
        {
            return Channel.GetCollateralRecAmtStaging(ID);
        }

        public CollateralRecAmtStaging[] GetAllCollateralRecAmtStagings()
        {
            return Channel.GetAllCollateralRecAmtStagings();
        }

        public CollateralRecAmtStaging[] GetCollateralRecAmtStagingBySearch(string searchParam)
        {
            return Channel.GetCollateralRecAmtStagingBySearch(searchParam);
        }

        public CollateralRecAmtStaging[] GetCollateralRecAmtStagings(int defaultCount)
        {
            return Channel.GetCollateralRecAmtStagings(defaultCount);
        }


        #endregion
        
        #region HistoricalDefaultFreq

        public HistoricalDefaultFreq UpdateHistoricalDefaultFreq(HistoricalDefaultFreq historicaldefaultfreq)
        {
            return Channel.UpdateHistoricalDefaultFreq(historicaldefaultfreq);
        }

        public void DeleteHistoricalDefaultFreq(int InstrumentID)
        {
            Channel.DeleteHistoricalDefaultFreq(InstrumentID);
        }

        public HistoricalDefaultFreq GetHistoricalDefaultFreq(int ID)
        {
            return Channel.GetHistoricalDefaultFreq(ID);
        }

        public HistoricalDefaultFreq[] GetAllHistoricalDefaultFreqs()
        {
            return Channel.GetAllHistoricalDefaultFreqs();
        }

        public HistoricalDefaultFreq[] GetHistoricalDefaultFreqBySearch(string searchParam)
        {
            return Channel.GetHistoricalDefaultFreqBySearch(searchParam);
        }

        public HistoricalDefaultFreq[] GetHistoricalDefaultFreqs(int defaultCount, string path)
        {
            return Channel.GetHistoricalDefaultFreqs(defaultCount, path);
        }


        #endregion

        #region CummulativePD

        public CummulativePD UpdateCummulativePD(CummulativePD cummulativepd)
        {
            return Channel.UpdateCummulativePD(cummulativepd);
        }

        public void DeleteCummulativePD(int ID)
        {
            Channel.DeleteCummulativePD(ID);
        }

        public CummulativePD GetCummulativePD(int ID)
        {
            return Channel.GetCummulativePD(ID);
        }

        public CummulativePD[] GetAllCummulativePDs()
        {
            return Channel.GetAllCummulativePDs();
        }

        public CummulativePD[] GetCummulativePDBySearch(string searchParam)
        {
            return Channel.GetCummulativePDBySearch(searchParam);
        }

        public CummulativePD[] GetCummulativePDs(int defaultCount, string path)
        {
            return Channel.GetCummulativePDs(defaultCount, path);
        }


        #endregion
        
        #region MarginalCCFPivotSTRLB

        public MarginalCCFPivotSTRLB UpdateMarginalCCFPivotSTRLB(MarginalCCFPivotSTRLB marginalccfpivotstrlb)
        {
            return Channel.UpdateMarginalCCFPivotSTRLB(marginalccfpivotstrlb);
        }

        public void DeleteMarginalCCFPivotSTRLB(int ID)
        {
            Channel.DeleteMarginalCCFPivotSTRLB(ID);
        }

        public MarginalCCFPivotSTRLB GetMarginalCCFPivotSTRLB(int ID)
        {
            return Channel.GetMarginalCCFPivotSTRLB(ID);
        }

        public MarginalCCFPivotSTRLB[] GetAllMarginalCCFPivotSTRLBs()
        {
            return Channel.GetAllMarginalCCFPivotSTRLBs();
        }

        public MarginalCCFPivotSTRLB[] GetMarginalCCFPivotSTRLBBySearch(string searchParam)
        {
            return Channel.GetMarginalCCFPivotSTRLBBySearch(searchParam);
        }

        public MarginalCCFPivotSTRLB[] GetMarginalCCFPivotSTRLBs(int defaultCount)
        {
            return Channel.GetMarginalCCFPivotSTRLBs(defaultCount);
        }


        #endregion

        #region CcfAnalysisOverDraftSTRLB

        public CcfAnalysisOverDraftSTRLB UpdateCcfAnalysisOverDraftSTRLB(CcfAnalysisOverDraftSTRLB ccfanalysisoverdraftstrlb)
        {
            return Channel.UpdateCcfAnalysisOverDraftSTRLB(ccfanalysisoverdraftstrlb);
        }

        public void DeleteCcfAnalysisOverDraftSTRLB(int InstrumentID)
        {
            Channel.DeleteCcfAnalysisOverDraftSTRLB(InstrumentID);
        }

        public CcfAnalysisOverDraftSTRLB GetCcfAnalysisOverDraftSTRLB(int ID)
        {
            return Channel.GetCcfAnalysisOverDraftSTRLB(ID);
        }

        public CcfAnalysisOverDraftSTRLB[] GetAllCcfAnalysisOverDraftSTRLBs()
        {
            return Channel.GetAllCcfAnalysisOverDraftSTRLBs();
        }

        public CcfAnalysisOverDraftSTRLB[] GetCcfAnalysisOverDraftSTRLBBySearch(string searchParam)
        {
            return Channel.GetCcfAnalysisOverDraftSTRLBBySearch(searchParam);
        }

        public CcfAnalysisOverDraftSTRLB[] GetCcfAnalysisOverDraftSTRLBs(int defaultCount)
        {
            return Channel.GetCcfAnalysisOverDraftSTRLBs(defaultCount);
        }


        #endregion
        
        #region IfrsProjectedCummDefaultFrq


        public void DeleteIfrsProjectedCummDefaultFrq(int Id)
        {
            Channel.DeleteIfrsProjectedCummDefaultFrq(Id);
        }

        public IfrsProjectedCummDefaultFrq GetIfrsProjectedCummDefaultFrq(int Id)
        {
            return Channel.GetIfrsProjectedCummDefaultFrq(Id);
        }

        public IfrsProjectedCummDefaultFrq[] GetAllIfrsProjectedCummDefaultFrq(int defaultcount, string path)
        {
            return Channel.GetAllIfrsProjectedCummDefaultFrq(defaultcount, path);
        }




        #endregion
        
        #region IfrsMonthlyForwardPDMacroVar



        #region IfrsPdTermStructure

        public IfrsPdTermStructure UpdateIfrsPdTermStructure(IfrsPdTermStructure IfrsPdTermStructure)
        {
            return Channel.UpdateIfrsPdTermStructure(IfrsPdTermStructure);
        }

        public void DeleteIfrsPdTermStructure(int ID)
        {
            Channel.DeleteIfrsPdTermStructure(ID);
        }

        public IfrsPdTermStructure GetIfrsPdTermStructure(int ID)
        {
            return Channel.GetIfrsPdTermStructure(ID);
        }

        public IfrsPdTermStructure[] GetAllIfrsPdTermStructures()
        {
            return Channel.GetAllIfrsPdTermStructures();
        }

        #endregion



        #region ConsolidatedLoans

        public ConsolidatedLoans UpdateConsolidatedLoans(ConsolidatedLoans consolidatedloans)
        {
            return Channel.UpdateConsolidatedLoans(consolidatedloans);
        }

        public void DeleteConsolidatedLoans(int ID)
        {
            Channel.DeleteConsolidatedLoans(ID);
        }

        public ConsolidatedLoans GetConsolidatedLoans(int ID)
        {
            return Channel.GetConsolidatedLoans(ID);
        }

        public ConsolidatedLoans[] GetAllConsolidatedLoanss()
        {
            return Channel.GetAllConsolidatedLoanss();
        }

        public ConsolidatedLoans[] GetConsolidatedLoansBySearch(string searchParam)
        {
            return Channel.GetConsolidatedLoansBySearch(searchParam);
        }

        public ConsolidatedLoans[] GetConsolidatedLoanss(int defaultCount)
        {
            return Channel.GetConsolidatedLoanss(defaultCount);
        }


        #endregion



        public void DeleteIfrsMonthlyForwardPDMacroVar(int Id)
        {
            Channel.DeleteIfrsMonthlyForwardPDMacroVar(Id);
        }

        public IfrsMonthlyForwardPDMacroVar GetIfrsMonthlyForwardPDMacroVar(int Id)
        {
            return Channel.GetIfrsMonthlyForwardPDMacroVar(Id);
        }

        public IfrsMonthlyForwardPDMacroVar[] GetAllIfrsMonthlyForwardPDMacroVar(int defaultcount, string path)
        {
            return Channel.GetAllIfrsMonthlyForwardPDMacroVar(defaultcount, path);
        }




        #endregion
        
        #region IfrsMarginalPDByScenario


        public IfrsMarginalPDByScenerio UpdateIfrsMarginalPDByScenerio(IfrsMarginalPDByScenerio assumption)
        {
            return Channel.UpdateIfrsMarginalPDByScenerio(assumption);
        }

        public void DeleteIfrsMarginalPDByScenerio(int ID)
        {
            Channel.DeleteIfrsMarginalPDByScenerio(ID);
        }

        public IfrsMarginalPDByScenerio GetIfrsMarginalPDByScenerio(int ID)
        {
            return Channel.GetIfrsMarginalPDByScenerio(ID);
        }

        public IfrsMarginalPDByScenerio[] GetAllIfrsMarginalPDByScenerio(int defaultcount, string path)
        {
            return Channel.GetAllIfrsMarginalPDByScenerio(defaultcount, path);
        }




        #endregion
        
        #region InvestmentMarginalPd

        public InvestmentMarginalPd UpdateInvestmentMarginalPd(InvestmentMarginalPd investmentmarginalpd)
        {
            return Channel.UpdateInvestmentMarginalPd(investmentmarginalpd);
        }

        public void DeleteInvestmentMarginalPd(int ID)
        {
            Channel.DeleteInvestmentMarginalPd(ID);
        }

        public InvestmentMarginalPd GetInvestmentMarginalPd(int ID)
        {
            return Channel.GetInvestmentMarginalPd(ID);
        }

        public InvestmentMarginalPd[] GetAllInvestmentMarginalPds()
        {
            return Channel.GetAllInvestmentMarginalPds();
        }

        public InvestmentMarginalPd[] GetInvestmentMarginalPdBySearch(string searchParam)
        {
            return Channel.GetInvestmentMarginalPdBySearch(searchParam);
        }

        public InvestmentMarginalPd[] GetInvestmentMarginalPds(int defaultCount)
        {
            return Channel.GetInvestmentMarginalPds(defaultCount);
        }


        #endregion

        #region OverdraftMonthlyEAD

        public OverdraftMonthlyEAD UpdateOverdraftMonthlyEAD(OverdraftMonthlyEAD odmonthlyead)
        {
            return Channel.UpdateOverdraftMonthlyEAD(odmonthlyead);
        }

        public void DeleteOverdraftMonthlyEAD(int Id)
        {
            Channel.DeleteOverdraftMonthlyEAD(Id);
        }

        public OverdraftMonthlyEAD GetOverdraftMonthlyEAD(int Id)
        {
            return Channel.GetOverdraftMonthlyEAD(Id);
        }

        public OverdraftMonthlyEAD[] GetAllOverdraftMonthlyEAD(int defaultcount)
        {
            return Channel.GetAllOverdraftMonthlyEAD(defaultcount);
        }

        public OverdraftMonthlyEAD[] GetOverdraftMonthlyEADBySearch(string searchParam)
        {
            return Channel.GetOverdraftMonthlyEADBySearch(searchParam);
        }


        #endregion

        #region OverdraftLGDComputation

        public OverdraftLGDComputation UpdateOverdraftLGDComputation(OverdraftLGDComputation lgdcomptresult)
        {
            return Channel.UpdateOverdraftLGDComputation(lgdcomptresult);
        }

        public void DeleteOverdraftLGDComputation(int Id)
        {
            Channel.DeleteOverdraftLGDComputation(Id);
        }

        public OverdraftLGDComputation GetOverdraftLGDComputation(int Id)
        {
            return Channel.GetOverdraftLGDComputation(Id);
        }

        public OverdraftLGDComputation[] GetAllOverdraftLGDComputation()
        {
            return Channel.GetAllOverdraftLGDComputation();
        }

        public OverdraftLGDComputation[] GetOverdraftLGDComputationBySearch(string searchParam)
        {
            return Channel.GetOverdraftLGDComputationBySearch(searchParam);
        }

        public OverdraftLGDComputation[] GetOverdraftLGDComputations(int defaultCount, string path)
        {
            return Channel.GetOverdraftLGDComputations(defaultCount, path);
        }

        #endregion



        #region IfrsLoansInfo

        public IfrsLoansInfo UpdateIfrsLoansInfo(IfrsLoansInfo lgdcomptresult)
        {
            return Channel.UpdateIfrsLoansInfo(lgdcomptresult);
        }

        public void DeleteIfrsLoansInfo(int Id)
        {
            Channel.DeleteIfrsLoansInfo(Id);
        }

        public IfrsLoansInfo GetIfrsLoansInfo(int Id)
        {
            return Channel.GetIfrsLoansInfo(Id);
        }

        public IfrsLoansInfo[] GetAllIfrsLoansInfo()
        {
            return Channel.GetAllIfrsLoansInfo();
        }

        public IfrsLoansInfo[] GetIfrsLoansInfoBySearch(string searchParam)
        {
            return Channel.GetIfrsLoansInfoBySearch(searchParam);
        }

        public IfrsLoansInfo[] GetIfrsLoansInfos(int defaultCount, string path)
        {
            return Channel.GetIfrsLoansInfos(defaultCount, path);
        }

        #endregion


        #region IfrsLgdProjections

        public IfrsLgdProjections UpdateIfrsLgdProjections(IfrsLgdProjections lgdcomptresult)
        {
            return Channel.UpdateIfrsLgdProjections(lgdcomptresult);
        }

        public void DeleteIfrsLgdProjections(int Id)
        {
            Channel.DeleteIfrsLgdProjections(Id);
        }

        /*public IfrsLgdProjections GetIfrsLgdProjections(int Id)
        {
            return Channel.GetIfrsLgdProjections(Id);
        } */

        public IfrsLgdProjections[] GetAllIfrsLgdProjections()
        {
            return Channel.GetAllIfrsLgdProjections();
        }

        public IfrsLgdProjections[] GetIfrsLgdProjectionsBySearch(string searchParam)
        {
            return Channel.GetIfrsLgdProjectionsBySearch(searchParam);
        }

        public IfrsLgdProjections[] GetIfrsLgdProjections(int defaultCount, string path)
        {
            return Channel.GetIfrsLgdProjections(defaultCount, path);
        }

        #endregion




        #region IfrsPDProjection

        public IfrsPDProjection UpdateIfrsPDProjection(IfrsPDProjection lgdcomptresult)
        {
            return Channel.UpdateIfrsPDProjection(lgdcomptresult);
        }

        public void DeleteIfrsPDProjection(int Id)
        {
            Channel.DeleteIfrsPDProjection(Id);
        }

        public IfrsPDProjection GetIfrsPDProjection(int Id)
        {
            return Channel.GetIfrsPDProjection(Id);
        } 

        public IfrsPDProjection[] GetAllIfrsPDProjection()
        {
            return Channel.GetAllIfrsPDProjection();
        }

        public IfrsPDProjection[] GetIfrsPDProjectionBySearch(string searchParam)
        {
            return Channel.GetIfrsPDProjectionBySearch(searchParam);
        }

        public IfrsPDProjection[] GetIfrsPDProjections(int defaultCount, string path)
        {
            return Channel.GetIfrsPDProjections(defaultCount, path);
        }

        #endregion


    }
}
