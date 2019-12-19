using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class ChangesInEquity : ObjectBase
    {
        int _ChangesInEquityId;
        DateTime _Rundate;
        string _Caption;
        double _ShareCapital;
        double _SharePremium;
        double _PropertyRevaluationReserve;
        double _TranslationReserve;
        double _FairValueReserve;
        double _RegulatoryRiskReserve;
        double _OtherRegulatoryReserves;
        double _CapitalReserve;
        double _ShareBonusReserve;
        double _OtherReserves;
        double _RetainedEarnings;
        double _NonControllingInterest;
        string _CompanyCode;
        bool _Active;

        public int ChangesInEquityId
        {
            get { return _ChangesInEquityId; }
            set
            {
                if (_ChangesInEquityId != value)
                {
                    _ChangesInEquityId = value;
                    OnPropertyChanged(() => ChangesInEquityId);
                }
            }
        }

        public DateTime Rundate
        {
            get { return _Rundate; }
            set
            {
                if (_Rundate != value)
                {
                    _Rundate = value;
                    OnPropertyChanged(() => Rundate);
                }
            }
        }

        public string Caption
        {
            get { return _Caption; }
            set
            {
                if (_Caption != value)
                {
                    _Caption = value;
                    OnPropertyChanged(() => Caption);
                }
            }
        }

        public double ShareCapital
        {
            get { return _ShareCapital; }
            set
            {
                if (_ShareCapital != value)
                {
                    _ShareCapital = value;
                    OnPropertyChanged(() => ShareCapital);
                }
            }
        }

        public double SharePremium
        {
            get { return _SharePremium; }
            set
            {
                if (_SharePremium != value)
                {
                    _SharePremium = value;
                    OnPropertyChanged(() => SharePremium);
                }
            }
        }

        public double PropertyRevaluationReserve
        {
            get { return _PropertyRevaluationReserve; }
            set
            {
                if (_PropertyRevaluationReserve != value)
                {
                    _PropertyRevaluationReserve = value;
                    OnPropertyChanged(() => PropertyRevaluationReserve);
                }
            }
        }

        public double TranslationReserve
        {
            get { return _TranslationReserve; }
            set
            {
                if (_TranslationReserve != value)
                {
                    _TranslationReserve = value;
                    OnPropertyChanged(() => TranslationReserve);
                }
            }
        }

        public double FairValueReserve
        {
            get { return _FairValueReserve; }
            set
            {
                if (_FairValueReserve != value)
                {
                    _FairValueReserve = value;
                    OnPropertyChanged(() => FairValueReserve);
                }
            }
        }

        public double RegulatoryRiskReserve
        {
            get { return _RegulatoryRiskReserve; }
            set
            {
                if (_RegulatoryRiskReserve != value)
                {
                    _RegulatoryRiskReserve = value;
                    OnPropertyChanged(() => RegulatoryRiskReserve);
                }
            }
        }

        public double OtherRegulatoryReserves
        {
            get { return _OtherRegulatoryReserves; }
            set
            {
                if (_OtherRegulatoryReserves != value)
                {
                    _OtherRegulatoryReserves = value;
                    OnPropertyChanged(() => OtherRegulatoryReserves);
                }
            }
        }

        public double CapitalReserve
        {
            get { return _CapitalReserve; }
            set
            {
                if (_CapitalReserve != value)
                {
                    _CapitalReserve = value;
                    OnPropertyChanged(() => CapitalReserve);
                }
            }
        }

        public double ShareBonusReserve
        {
            get { return _ShareBonusReserve; }
            set
            {
                if (_ShareBonusReserve != value)
                {
                    _ShareBonusReserve = value;
                    OnPropertyChanged(() => ShareBonusReserve);
                }
            }
        }

        public double OtherReserves
        {
            get { return _OtherReserves; }
            set
            {
                if (_OtherReserves != value)
                {
                    _OtherReserves = value;
                    OnPropertyChanged(() => OtherReserves);
                }
            }
        }

        public double RetainedEarnings
        {
            get { return _RetainedEarnings; }
            set
            {
                if (_RetainedEarnings != value)
                {
                    _RetainedEarnings = value;
                    OnPropertyChanged(() => RetainedEarnings);
                }
            }
        }

        public double NonControllingInterest
        {
            get { return _NonControllingInterest; }
            set
            {
                if (_NonControllingInterest != value)
                {
                    _NonControllingInterest = value;
                    OnPropertyChanged(() => NonControllingInterest);
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

        class ReportValidator : AbstractValidator<ChangesInEquity>
        {
            public ReportValidator()
            {
               
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new ReportValidator();
        }
    }
}
