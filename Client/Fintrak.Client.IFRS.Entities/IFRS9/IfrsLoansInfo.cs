using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsLoansInfo : ObjectBase
    {

        int _Id;
        string _Refno;
        string _ProductType;
        double _OutstandingBal;
        int _Number_Payment_OutStanding;
        int _DaysPastDue;
        DateTime _Rundate;
       
        bool _Active;

        public int Id {
            get { return _Id; }
            set {
                if (_Id != value) {
                    _Id = value;
                    OnPropertyChanged(() => Id);
                }
            }
        }

        public string Refno {
            get { return _Refno; }
            set {
                if (_Refno != value) {
                    _Refno = value;
                    OnPropertyChanged(() => Refno);
                }
            }
        }

        public string ProductType {
            get { return _ProductType; }
            set {
                if (_ProductType != value) {
                    _ProductType = value;
                    OnPropertyChanged(() => ProductType);
                }
            }
        }


        public double OutstandingBal {
            get { return _OutstandingBal; }
            set {
                if (_OutstandingBal != value) {
                    _OutstandingBal = value;
                    OnPropertyChanged(() => OutstandingBal);
                }
            }
        }


        public int Number_Payment_OutStanding
        {
            get { return _Number_Payment_OutStanding; }
            set
            {
                if (_Number_Payment_OutStanding != value)
                {
                    _Number_Payment_OutStanding = value;
                    OnPropertyChanged(() => Number_Payment_OutStanding);
                }
            }
        }



        public int  DaysPastDue
        {
            get { return _DaysPastDue; }
            set
            {
                if (_DaysPastDue != value)
                {
                    _DaysPastDue = value;
                    OnPropertyChanged(() => DaysPastDue);
                }
            }
        }


        public DateTime Rundate {
            get { return _Rundate; }
            set {
                if (_Rundate != value) {
                    _Rundate = value;
                    OnPropertyChanged(() => Rundate);
                }
            }
        }

     

        public bool Active {
            get { return _Active; }
            set {
                if (_Active != value) {
                    _Active = value;
                    OnPropertyChanged(() => Active);
                }
            }
        }
        class IfrsLoansInfoValidator : AbstractValidator<IfrsLoansInfo>
        {
            public IfrsLoansInfoValidator(){               
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsLoansInfoValidator();
        }

    }
}
