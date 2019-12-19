using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class EquityStockComputationResult : ObjectBase
    {
        int _Id;
        string _RefNo;
        string _PortfolioID;
        string _Portfolio;
        string _ID_PortfolioGroup;
        string _ID_PortfolioGroupName;
        string _StockDescription;
        double _Cost;
        double _MarketQty;
        double _MarketPrice;
        string _Classification;
        bool _Quoted;
        double _FairValue;
        double _FairValueGainLoss;
        int _FairValueBasis;
        string _Currency;
        int _Period;
        int _Year;
        DateTime _RunDate;
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

        public string PortfolioID
        {
            get { return _PortfolioID; }
            set
            {
                if (_PortfolioID != value)
                {
                    _PortfolioID = value;
                    OnPropertyChanged(() => PortfolioID);
                }
            }
        }


        public string Portfolio
        {
            get { return _Portfolio; }
            set
            {
                if (_Portfolio != value)
                {
                    _Portfolio = value;
                    OnPropertyChanged(() => Portfolio);
                }
            }
        }

        public string ID_PortfolioGroup
        {
            get { return _ID_PortfolioGroup; }
            set
            {
                if (_ID_PortfolioGroup != value)
                {
                    _ID_PortfolioGroup = value;
                    OnPropertyChanged(() => ID_PortfolioGroup);
                }
            }
        }


        public string ID_PortfolioGroupName
        {
            get { return _ID_PortfolioGroupName; }
            set
            {
                if (_ID_PortfolioGroupName != value)
                {
                    _ID_PortfolioGroupName = value;
                    OnPropertyChanged(() => ID_PortfolioGroupName);
                }
            }
        }

        public string StockDescription
        {
            get { return _StockDescription; }
            set
            {
                if (_StockDescription != value)
                {
                    _StockDescription = value;
                    OnPropertyChanged(() => StockDescription);
                }
            }
        }

        public double Cost
        {
            get { return _Cost; }
            set
            {
                if (_Cost != value)
                {
                    _Cost = value;
                    OnPropertyChanged(() => Cost);
                }
            }
        }

        public double MarketQty
        {
            get { return _MarketQty; }
            set
            {
                if (_MarketQty != value)
                {
                    _MarketQty = value;
                    OnPropertyChanged(() => MarketQty);
                }
            }
        }

        public double MarketPrice
        {
            get { return _MarketPrice; }
            set
            {
                if (_MarketPrice != value)
                {
                    _MarketPrice = value;
                    OnPropertyChanged(() => MarketPrice);
                }
            }
        }

        public string Classification
        {
            get { return  _Classification; }
            set
            {
                if (_Classification != value)
                {
                    _Classification = value;
                    OnPropertyChanged(() => Classification);
                }
            }
        }

        public bool Quoted
        {
            get { return _Quoted; }
            set
            {
                if (_Quoted != value)
                {
                    _Quoted = value;
                    OnPropertyChanged(() => Quoted);
                }
            }
        }

        public double FairValue
        {
            get { return _FairValue; }
            set
            {
                if (_FairValue != value)
                {
                    _FairValue = value;
                    OnPropertyChanged(() => FairValue);
                }
            }
        }

        public double FairValueGainLoss
        {
            get { return _FairValueGainLoss; }
            set
            {
                if (_FairValueGainLoss != value)
                {
                    _FairValueGainLoss = value;
                    OnPropertyChanged(() => FairValueGainLoss);
                }
            }
        }

        public int FairValueBasis
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



        class EquityStockComputationResultValidator : AbstractValidator<EquityStockComputationResult>
        {
            public EquityStockComputationResultValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new EquityStockComputationResultValidator();
        }
    }
}
