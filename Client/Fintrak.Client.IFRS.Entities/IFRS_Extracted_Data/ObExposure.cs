using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class OBExposure : ObjectBase
    {
        int _obe_Id;
        string _Refno;
        string _OBE_Type;
        string _Mapped_OBE_Type;
        DateTime _ValueDate;
        DateTime _MaturityDate;
        string _Currency;
        decimal _Rate;
        double  _Amount;
        int _Flag;
        string _ProductType;
        string _SubType;
        string _CustomerName;
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



        class OBExposureValidator : AbstractValidator<OBExposure>
        {
            public OBExposureValidator()
            {
                RuleFor(obj => obj.obe_Id).NotEmpty().WithMessage("Reference no required");
            }
        }

        protected override IValidator GetValidator()
        {
            return new OBExposureValidator();
        }
    }
}
