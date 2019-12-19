using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class FacClassConsolidated : ObjectBase
    {
        public int _ID;
        public string _AccountNo;
        public string _CustomerNo;
        public string _CustomerName;
        public string _HC1;
        public string _HC2;
        public string _Sector;
        public bool _Active;



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
        public string HC1
        {
            get { return _HC1; }
            set
            {
                if (_HC1 != value)
                {
                    _HC1 = value;
                    OnPropertyChanged(() => HC1);
                }
            }
        }
        public string HC2
        {
            get { return _HC2; }
            set
            {
                if (_HC2 != value)
                {
                    _HC2 = value;
                    OnPropertyChanged(() => HC2);
                }
            }
        }
        public string Sector
        {
            get { return _Sector; }
            set
            {
                if (_Sector != value)
                {
                    _Sector = value;
                    OnPropertyChanged(() => Sector);
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


        class FacClassConsolidatedValidator : AbstractValidator<FacClassConsolidated>
        {
            public FacClassConsolidatedValidator()
            {
                //RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new FacClassConsolidatedValidator();
        }
    }
}