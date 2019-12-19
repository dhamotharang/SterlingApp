using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class OBExposureCCF : ObjectBase
    {
        int _obe_Id;
        string _RefNo;
        string _OBE_Type;
        string _Mapped_OBE_Type;
        DateTime _ValueDate;
        DateTime _MaturityDate;
        string _Currency;
        decimal _Rate;
        double  _Amount;
        bool _Default_Flag;
        DateTime _Date_Of_Devolement;
        double _Default_Amount_Crystallize;
        int _DOI;
        double _YTM;
        double _TimeAtInvocation_yr;
        int _Flag;
        string _ProductType;
        string _SubType;
        string _CustomerName;
        double _OneYearPriorBal;
        double _DefaultBal;
        double _OneYearPriorUndrawn;
        double _AdditionalDrawnWithinYr;
        bool _Active;
        public int obe_Id

        {
            get { return _obe_Id; }
            set
            {
                if (_obe_Id != value)
                {
                    _obe_Id = value;
                    OnPropertyChanged(() => obe_Id);
                }
            }
        }
        public double AdditionalDrawnWithinYr
        {
            get { return _AdditionalDrawnWithinYr; }
            set
            {
                if (_AdditionalDrawnWithinYr != value)
                {
                    _AdditionalDrawnWithinYr = value;
                    OnPropertyChanged(() => AdditionalDrawnWithinYr);
                }
            }
        }
        public double OneYearPriorUndrawn
        {
            get { return _OneYearPriorUndrawn; }
            set
            {
                if (_OneYearPriorUndrawn != value)
                {
                    _OneYearPriorUndrawn = value;
                    OnPropertyChanged(() => OneYearPriorUndrawn);
                }
            }
        }
        public double DefaultBal
        {
            get { return _DefaultBal; }
            set
            {
                if (_DefaultBal != value)
                {
                    _DefaultBal = value;
                    OnPropertyChanged(() => DefaultBal);
                }
            }
        }
        public double OneYearPriorBal
        {
            get { return _OneYearPriorBal; }
            set
            {
                if (_OneYearPriorBal != value)
                {
                    _OneYearPriorBal = value;
                    OnPropertyChanged(() => OneYearPriorBal);
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
        public string OBE_Type
        {
            get { return _OBE_Type; }
            set
            {
                if (_OBE_Type != value)
                {
                    _OBE_Type = value;
                    OnPropertyChanged(() => OBE_Type);
                }
            }
        }
        public string ProductType
        {
            get { return _ProductType; }
            set
            {
                if (_ProductType != value)
                {
                    _ProductType = value;
                    OnPropertyChanged(() => ProductType);
                }
            }
        }

        public string SubType
        {
            get { return _SubType; }
            set
            {
                if (_SubType != value)
                {
                    _SubType = value;
                    OnPropertyChanged(() => SubType);
                }
            }
        }
        public string Mapped_OBE_Type
        {
            get { return _Mapped_OBE_Type; }
            set
            {
                if (_Mapped_OBE_Type != value)
                {
                    _Mapped_OBE_Type = value;
                    OnPropertyChanged(() => Mapped_OBE_Type);
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

        public string Currency
        {
            get { return _Currency;  }
            set
            {
                if (_Currency != value)
                {
                    _Currency = value;
                    OnPropertyChanged(() => Currency);
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


       
        public double Amount
        {
            get { return _Amount; }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    OnPropertyChanged(() => Amount);
                }
            }
        }
 
        public bool Default_Flag
        {
            get { return _Default_Flag; }
            set
            {
                if (_Default_Flag != value)
                {
                    _Default_Flag = value;
                    OnPropertyChanged(() => Default_Flag);
                }
            }
        }



        public DateTime Date_Of_Devolement
        {
            get { return _Date_Of_Devolement; }
            set
            {
                if (_Date_Of_Devolement != value)
                {
                    _Date_Of_Devolement = value;
                    OnPropertyChanged(() => Date_Of_Devolement);
                }
            }
        }

        public double Default_Amount_Crystallize
        {
            get { return _Default_Amount_Crystallize; }
            set
            {
                if (_Default_Amount_Crystallize != value)
                {
                    _Default_Amount_Crystallize = value;
                    OnPropertyChanged(() => Default_Amount_Crystallize);
                }
            }
        }

        public int DOI
        {
            get { return _DOI; }
            set
            {
                if (_DOI != value)
                {
                    _DOI = value;
                    OnPropertyChanged(() => DOI);
                }
            }
        }

        public double YTM
        {
            get { return _YTM; }
            set
            {
                if (_YTM != value)
                {
                    _YTM = value;
                    OnPropertyChanged(() => YTM);
                }
            }
        }



        public double TimeAtInvocation_yr
        {
            get { return _TimeAtInvocation_yr; }
            set
            {
                if (_TimeAtInvocation_yr != value)
                {
                    _TimeAtInvocation_yr = value;
                    OnPropertyChanged(() => TimeAtInvocation_yr);
                }
            }
        }

        public int Flag
        {
            get { return _Flag; }
            set
            {
                if (_Flag!= value)
                {
                    _Flag = value;
                    OnPropertyChanged(() => Flag);
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



        class OBExposureCCFValidator : AbstractValidator<OBExposureCCF>
        {
            public OBExposureCCFValidator()
            {
                RuleFor(obj => obj.obe_Id).NotEmpty().WithMessage("Reference no required");
            }
        }

        protected override IValidator GetValidator()
        {
            return new OBExposureCCFValidator();
        }
    }
}
