using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class HistoricalClassification : ObjectBase
    {
        int _HistoricalClassificationId;
        string _SectorIndustry ;
        string _CustomerNo;
        string _CustomerName;
        string _Classification;
        string _SubClassification;
        string _Collateral_Type;
        double _OutstandingBal;
        double _RecoverableAmt;
        int _Period;
        int _Year;
        string _CompanyCode;
        bool _Active;

        public int HistoricalClassificationId
        {
            get { return _HistoricalClassificationId; }
            set
            {
                if (_HistoricalClassificationId != value)
                {
                    _HistoricalClassificationId = value;
                    OnPropertyChanged(() => HistoricalClassificationId);
                }
            }
        }

        public string SectorIndustry 
        {
            get { return _SectorIndustry ; }
            set
            {
                if (_SectorIndustry  != value)
                {
                    _SectorIndustry  = value;
                    OnPropertyChanged(() => SectorIndustry );
                }
            }
        }

        public string Collateral_Type
        {
            get { return _Collateral_Type; }
            set
            {
                if (_Collateral_Type != value)
                {
                    _Collateral_Type = value;
                    OnPropertyChanged(() => Collateral_Type);
                }
            }
        }

        public string CustomerNo
        {
            get { return _CustomerNo; }
            set
            {
                if (_CustomerNo != value)
                {
                    _CustomerNo = value;
                    OnPropertyChanged(() => CustomerNo);
                }
            }
        }


        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    OnPropertyChanged(() => CustomerName);
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


        public string SubClassification
        {
            get { return _SubClassification; }
            set
            {
                if (_SubClassification != value)
                {
                    _SubClassification = value;
                    OnPropertyChanged(() => SubClassification);
                }
            }
        }

        public double OutstandingBal
        {
            get { return _OutstandingBal; }
            set
            {
                if (_OutstandingBal != value)
                {
                    _OutstandingBal = value;
                    OnPropertyChanged(() => OutstandingBal);
                }
            }
        }

        public double RecoverableAmt
        {
            get { return _RecoverableAmt; }
            set
            {
                if (_RecoverableAmt != value)
                {
                    _RecoverableAmt = value;
                    OnPropertyChanged(() => RecoverableAmt);
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


        public bool Active
        {
            get { return _Active; }
            set
            {
                if (_Active != value)
                {
                    _Active = value;
                    OnPropertyChanged(() => Active);
                }
            }
        }


        class HistoricalClassificationValidator : AbstractValidator<HistoricalClassification>
        {
            public HistoricalClassificationValidator()
            {
                RuleFor(obj => obj.SectorIndustry ).NotEmpty().WithMessage("SectorIndustry  is required.");
                RuleFor(obj => obj.CustomerNo).NotEmpty().WithMessage("CustomerNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new HistoricalClassificationValidator();
        }
    }
}
