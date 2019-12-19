using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class BondSummary : ObjectBase
    {
        int _BondId;
        string _Refno;
        DateTime _Valuedate;
        double _Facevalue;
        double _QtyTraded;
        DateTime _MaturityDate;
        double _Consideration;
        DateTime _Rundate;
        bool _Active;

        public int BondId
        {
            get { return _BondId; }
            set
            {
                if (_BondId != value)
                {
                    _BondId = value;
                    OnPropertyChanged(() => BondId);
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

        public DateTime Valuedate
        {
            get { return _Valuedate; }
            set
            {
                if (_Valuedate != value)
                {
                    _Valuedate = value;
                    OnPropertyChanged(() => Valuedate);
                }
            }
        }

        public double Facevalue
        {
            get { return _Facevalue; }
            set
            {
                if (_Facevalue != value)
                {
                    _Facevalue = value;
                    OnPropertyChanged(() => Facevalue);
                }
            }
        }

        public double QtyTraded
        {
            get { return _QtyTraded; }
            set
            {
                if (_QtyTraded != value)
                {
                    _QtyTraded = value;
                    OnPropertyChanged(() => QtyTraded);
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

        public double Consideration
        {
            get { return _Consideration; }
            set
            {
                if (_Consideration != value)
                {
                    _Consideration = value;
                    OnPropertyChanged(() => Consideration);
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


        class BondSummaryValidator : AbstractValidator<BondSummary>
        {
            public BondSummaryValidator()
            {
                
            }
        }

        protected override IValidator GetValidator()
        {
            return new BondSummaryValidator();
        }
    }
}
