using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class CCFModelling : ObjectBase
    {
        int _CCFModellingId;
        string _Refno;
        string _AccountNo;
        string _Grouping;
        DateTime _ValueDate;
        DateTime _DefaultDate;
        double _PrincipalOutstandingBal;
        double _ODLimit;
        string _Rating;
        string _Bucket;       
        double _BalatRefDate;
        double _DefaultDatebal;
        double _CCF;
        double _DrawnAmt;
        double _UnDrawnAmt;
        double _UtilizationFactor;
        int _TimeOfDefault;
        double _UndrawnPercentage;
        bool _Active;

        public int CCFModellingId
        {
            get { return _CCFModellingId; }
            set
            {
                if (_CCFModellingId != value)
                {
                    _CCFModellingId = value;
                    OnPropertyChanged(() => CCFModellingId);
                }
            }
        }

        public double UndrawnPercentage
        {
            get { return _UndrawnPercentage; }
            set
            {
                if (_UndrawnPercentage != value)
                {
                    _UndrawnPercentage = value;
                    OnPropertyChanged(() => UndrawnPercentage);
                }
            }
        }
        public double UtilizationFactor
        {
            get { return _UtilizationFactor; }
            set
            {
                if (_UtilizationFactor != value)
                {
                    _UtilizationFactor = value;
                    OnPropertyChanged(() => UtilizationFactor);
                }
            }
        }
        public double DefaultDatebal
        {
            get { return _DefaultDatebal; }
            set
            {
                if (_DefaultDatebal != value)
                {
                    _DefaultDatebal = value;
                    OnPropertyChanged(() => DefaultDatebal);
                }
            }
        }

        public int TimeOfDefault
        {
            get { return _TimeOfDefault; }
            set
            {
                if (_TimeOfDefault != value)
                {
                    _TimeOfDefault = value;
                    OnPropertyChanged(() => TimeOfDefault);
                }
            }
        }
        public double UnDrawnAmt
        {
            get { return _UnDrawnAmt; }
            set
            {
                if (_UnDrawnAmt != value)
                {
                    _UnDrawnAmt = value;
                    OnPropertyChanged(() => UnDrawnAmt);
                }
            }
        }
        public double DrawnAmt
        {
            get { return _DrawnAmt; }
            set
            {
                if (_DrawnAmt != value)
                {
                    _DrawnAmt = value;
                    OnPropertyChanged(() => DrawnAmt);
                }
            }
        }
        public double BalatRefDate
        {
            get { return _BalatRefDate; }
            set
            {
                if (_BalatRefDate != value)
                {
                    _BalatRefDate = value;
                    OnPropertyChanged(() => BalatRefDate);
                }
            }
        }

         public double CCF
        {
            get { return _CCF; }
            set
            {
                if (_CCF != value)
                {
                    _CCF = value;
                    OnPropertyChanged(() => CCF);
                }
            }
        }
        public double PrincipalOutstandingBal
        {
            get { return _PrincipalOutstandingBal; }
            set
            {
                if (_PrincipalOutstandingBal != value)
                {
                    _PrincipalOutstandingBal = value;
                    OnPropertyChanged(() => PrincipalOutstandingBal);
                }
            }
        }
        public double ODLimit
        {
            get { return _ODLimit; }
            set
            {
                if (_ODLimit != value)
                {
                    _ODLimit = value;
                    OnPropertyChanged(() => ODLimit);
                }
            }
        }

        public string Bucket
        {
            get { return _Bucket; }
            set
            {
                if (_Bucket != value)
                {
                    _Bucket = value;
                    OnPropertyChanged(() => Bucket);
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


        public string Rating
        {
            get { return _Rating; }
            set
            {
                if (_Rating != value)
                {
                    _Rating = value;
                    OnPropertyChanged(() => Rating);
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


        public string Grouping
        {
            get { return _Grouping; }
            set
            {
                if (_Grouping != value)
                {
                    _Grouping = value;
                    OnPropertyChanged(() => Grouping);
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

        public DateTime DefaultDate
        {
            get { return _DefaultDate; }
            set
            {
                if (_DefaultDate != value)
                {
                    _DefaultDate = value;
                    OnPropertyChanged(() => DefaultDate);
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


        class CCFModellingValidator : AbstractValidator<CCFModelling>
        {
            public CCFModellingValidator()
            {
                RuleFor(obj => obj.Refno).NotEmpty().WithMessage("Refno is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new CCFModellingValidator();
        }
    }
}
