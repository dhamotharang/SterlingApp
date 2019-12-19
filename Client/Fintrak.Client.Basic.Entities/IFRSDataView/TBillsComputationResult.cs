using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class TBillsComputationResult : ObjectBase
    {
        int _Id;
        string _RefNo;
        string _Description;
        string _DealTypeId;
        DateTime _ValueDate;
        DateTime _MaturityDate;
        int _TotalTenor;
        int _UsedDays;
        int _DaysToMaturity;
        decimal _Rate;
        double _FaceValue;
        double _CleanPrice;
        decimal _CurrentMarketYield;
        double _ComputedMarketPrice;
        double _IntrestReceivable;
        double _AmortizedCost;
        double _FairValueGain;
        string _Classification;
        int _Period;
        int _Year;
        DateTime _RunDate;
        int? _FairValueBasis;
        string _Currency;
        string _CompanyCode;


        public int Id

        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(() => Id);
                }
            }
        }


        public string RefNo
        {
            get { return _RefNo; }
            set
            {
                if (_RefNo != value)
                {
                    _RefNo = value;
                    OnPropertyChanged(() => RefNo);
                }
            }
        }

        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }

        public string DealTypeId
        {
            get { return _DealTypeId; }
            set
            {
                if (_DealTypeId != value)
                {
                    _DealTypeId = value;
                    OnPropertyChanged(() => DealTypeId);
                }
            }
        }

        public DateTime ValueDate
        {
            get { return _ValueDate; }
            set
            {
                if (_ValueDate != value)
                {
                    _ValueDate = value;
                    OnPropertyChanged(() => ValueDate);
                }
            }
        }

        public DateTime MaturityDate
        {
            get { return _MaturityDate; }
            set
            {
                if (_MaturityDate != value)
                {
                    _MaturityDate = value;
                    OnPropertyChanged(() => MaturityDate);
                }
            }
        }


        public int TotalTenor
        {
            get { return _TotalTenor; }
            set
            {
                if (_TotalTenor != value)
                {
                    _TotalTenor = value;
                    OnPropertyChanged(() => TotalTenor);
                }
            }
        }



        public int UsedDays
        {
            get { return _UsedDays; }
            set
            {
                if (_UsedDays != value)
                {
                    _UsedDays = value;
                    OnPropertyChanged(() => UsedDays);
                }
            }
        }

        public int DaysToMaturity
        {
            get { return _DaysToMaturity; }
            set
            {
                if (_DaysToMaturity != value)
                {
                    _DaysToMaturity = value;
                    OnPropertyChanged(() => DaysToMaturity);
                }
            }
        }



        public decimal Rate
        {
            get { return _Rate; }
            set
            {
                if (_Rate != value)
                {
                    _Rate = value;
                    OnPropertyChanged(() => Rate);
                }
            }
        }

        public double FaceValue
        {
            get { return _FaceValue; }
            set
            {
                if (_FaceValue != value)
                {
                    _FaceValue = value;
                    OnPropertyChanged(() => FaceValue);
                }
            }
        }
        public double CleanPrice
        {
            get { return _CleanPrice; }
            set
            {
                if (_CleanPrice != value)
                {
                    _CleanPrice = value;
                    OnPropertyChanged(() => CleanPrice);
                }
            }
        }


        public decimal CurrentMarketYield
        {
            get { return _CurrentMarketYield; }
            set
            {
                if (_CurrentMarketYield != value)
                {
                    _CurrentMarketYield = value;
                    OnPropertyChanged(() => CurrentMarketYield);
                }
            }
        }


        public double ComputedMarketPrice
        {
            get { return _ComputedMarketPrice; }
            set
            {
                if (_ComputedMarketPrice != value)
                {
                    _ComputedMarketPrice = value;
                    OnPropertyChanged(() => ComputedMarketPrice);
                }
            }
        }

        public double IntrestReceivable
        {
            get { return _IntrestReceivable; }
            set
            {
                if (_IntrestReceivable != value)
                {
                    _IntrestReceivable = value;
                    OnPropertyChanged(() => IntrestReceivable);
                }
            }
        }
        public double AmortizedCost
        {
            get { return _AmortizedCost; }
            set
            {
                if (_AmortizedCost != value)
                {
                    _AmortizedCost = value;
                    OnPropertyChanged(() => AmortizedCost);
                }
            }
        }

        public double FairValueGain
        {
            get { return _FairValueGain; }
            set
            {
                if (_FairValueGain != value)
                {
                    _FairValueGain = value;
                    OnPropertyChanged(() => FairValueGain);
                }
            }
        }

        public string Classification
        {
            get { return _Classification; }
            set
            {
                if (_Classification != value)
                {
                    _Classification = value;
                    OnPropertyChanged(() => Classification);
                }
            }
        }

        public int Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                    OnPropertyChanged(() => Period);
                }
            }
        }


        public int Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    OnPropertyChanged(() => Year);
                }
            }
        }



        public DateTime RunDate
        {
            get { return _RunDate; }
            set
            {
                if (_RunDate != value)
                {
                    _RunDate = value;
                    OnPropertyChanged(() => RunDate);
                }
            }
        }

        public int? FairValueBasis
        {
            get { return _FairValueBasis; }
            set
            {
                if (_FairValueBasis != value)
                {
                    _FairValueBasis = value;
                    OnPropertyChanged(() => FairValueBasis);
                }
            }
        }



        public string Currency
        {
            get { return _Currency; }
            set
            {
                if (_Currency != value)
                {
                    _Currency = value;
                    OnPropertyChanged(() => Currency);
                }
            }
        }

      
        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value;
                    OnPropertyChanged(() => CompanyCode);
                }
            }
        }



       


        class TBillsComputationResultValidator : AbstractValidator<TBillsComputationResult>
        {
            public TBillsComputationResultValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new TBillsComputationResultValidator();
        }
    }
}
