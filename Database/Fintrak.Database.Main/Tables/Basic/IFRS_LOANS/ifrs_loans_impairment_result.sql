
CREATE TABLE [dbo].[ifrs_loans_impairment_result](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccountNo] [varchar](250) NOT NULL,
	[RefNo] [varchar](250) NOT NULL,
	[ProductCategory] [varchar](150) NULL,
	[ProductCode] [varchar](150) NULL,
	[ProductName] [varchar](150) NULL,
	[ProductType] [varchar](150) NULL,
	[Classification] [varchar](250) NULL,
	[Performing] [varchar](250) NULL,
	[WatchListed] [bit] NULL,
	[Significant] [bit] NULL,
	[AgedBaseOnLastCr] [int] NULL,
	[Amount] [money] NULL DEFAULT 0,
	[PrincipalOutstandingBal] [money] NULL DEFAULT 0,
	[Interest_Receiv_Pay_UnEarn] [money] NULL DEFAULT 0,
	[InterestInSuspense] [money] NULL DEFAULT 0,
	[AmortizedBalance] [money] NULL DEFAULT 0,
	[TotalAmortizedCost] [money] NULL DEFAULT 0,
	[ImpairmentTrigger] [varchar](50) NULL,
	[InitialSelection] [varchar](50) NULL,
	[DaysToMaturity] [int] NULL,
	[ExpiredDays] [int] NULL,
	[PeriodicInterestRepayment] [money] NULL DEFAULT 0,
	[PeriodicCFPerPrincRepayment] [money] NULL DEFAULT 0,
	[RecoverableRate] [decimal](18, 4) NULL,
	[PMT] [money] NULL DEFAULT 0,
	[RecoverableAmount] [money] NULL DEFAULT 0,
	[CollateralRecoverableAmt] [money] NULL DEFAULT 0,
	[TotalRecoverableAmount] [money] NULL DEFAULT 0,
	[ImpairmentSwitchTest] [money] NULL DEFAULT 0,
	[Finalselection] [varchar](50) NULL,
	[SpecificImpairment] [money] NULL DEFAULT 0,
	[CollectiveImpairment] [money] NULL DEFAULT 0,
	[TotalImpairment] [money] NULL DEFAULT 0,
	[FairValueAmount] [money] NULL DEFAULT 0,
	[FairValueGain] [money] NULL DEFAULT 0,
	[CollateralValue] [money] NULL DEFAULT 0,
	[CollateralHaircut] [money] NULL DEFAULT 0,
	[CollateralCategory] [varchar](250) NULL,
	[NPER] [int] NULL,
	[Period] [int] NULL,
	[Year] [int] NULL,
	[RunDate] [date] NULL,
	[CompanyCode] [varchar](50) NULL,
	[StaffFairValueAmount] [money] NULL DEFAULT 0,
	[StaffFairValueGain] [money] NULL DEFAULT 0, 
    CONSTRAINT [PK_ifrs_loans_impairment_result] PRIMARY KEY ([ID])
) ON [PRIMARY]