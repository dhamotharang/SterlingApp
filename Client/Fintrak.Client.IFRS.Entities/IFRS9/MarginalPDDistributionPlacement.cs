using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MarginalPDDistributionPlacement : ObjectBase
    {
        int _ID;
        string _AccountNo;
        string _Refno;
        int _SeqNo;
        string _Currency;
        string _CurrentRating;
        DateTime _date_pmt;
        double _AmortizedCost;
        int _TenorInDays;
        int _TotalTenorInDays;
        int _Staging;
        double _PD;
        double _CumulativePD;
        double _MarginalPD;
        DateTime _Rundate;
        bool _Active;

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

        public double MarginalPD
        {
            get { return _MarginalPD; }
            set
            {
                if (_MarginalPD != value)
                {
                    _MarginalPD = value;
                    OnPropertyChanged(() => MarginalPD);
                }
            }
        }

        public double CumulativePD
        {
            get { return _CumulativePD; }
            set
            {
                if (_CumulativePD != value)
                {
                    _CumulativePD = value;
                    OnPropertyChanged(() => CumulativePD);
                }
            }
        }

        public double PD
        {
            get { return _PD; }
            set
            {
                if (_PD != value)
                {
                    _PD = value;
                    OnPropertyChanged(() => PD);
                }
            }
        }

        public int Staging
        {
            get { return _Staging; }
            set
            {
                if (_Staging != value)
                {
                    _Staging = value;
                    OnPropertyChanged(() => Staging);
                }
            }
        }

        public int TotalTenorInDays
        {
            get { return _TotalTenorInDays; }
            set
            {
                if (_TotalTenorInDays != value)
                {
                    _TotalTenorInDays = value;
                    OnPropertyChanged(() => TotalTenorInDays);
                }
            }
        }

        public int TenorInDays
        {
            get { return _TenorInDays; }
            set
            {
                if (_TenorInDays != value)
                {
                    _TenorInDays = value;
                    OnPropertyChanged(() => TenorInDays);
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

        public DateTime date_pmt
        {
            get { return _date_pmt; }
            set
            {
                if (_date_pmt != value)
                {
                    _date_pmt = value;
                    OnPropertyChanged(() => date_pmt);
                }
            }
        }

        public string CurrentRating
        {
            get { return _CurrentRating; }
            set
            {
                if (_CurrentRating != value)
                {
                    _CurrentRating = value;
                    OnPropertyChanged(() => CurrentRating);
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

        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }

        public string AccountNo
        {
            get { return _AccountNo; }
            set
            {
                if (_AccountNo != value)
                {
                    _AccountNo = value;
                    OnPropertyChanged(() => AccountNo);
                }
            }
        }


        public int SeqNo
        {
            get { return _SeqNo; }
            set
            {
                if (_SeqNo != value)
                {
                    _SeqNo = value;
                    OnPropertyChanged(() => SeqNo);
                }
            }
        }

        public string Refno
        {
            get { return _Refno; }
            set
            {
                if (_Refno != value)
                {
                    _Refno = value;
                    OnPropertyChanged(() => Refno);
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


        class MarginalPDDistributionPlacementValidator : AbstractValidator<MarginalPDDistributionPlacement>
        {
            public MarginalPDDistributionPlacementValidator()
            {
                //RuleFor(obj => obj._AccountNo).NotEmpty().WithMessage("AccountNo is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new MarginalPDDistributionPlacementValidator();
        }
    }
}
