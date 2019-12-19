using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.Budget.Entities
{
    public class Currency : ObjectBase
    {
        int _CurrencyId;
        string _Code;
        string _Name;
        string _Symbol;
        double _Rate;
        bool _IsBase;
        bool _Active;

        public int CurrencyId
        {
            get { return _CurrencyId; }
            set
            {
                if (_CurrencyId != value)
                {
                    _CurrencyId = value;
                    OnPropertyChanged(() => CurrencyId);
                }
            }
        }


        public string Code
        {
            get { return _Code; }
            set
            {
                if (_Code != value)
                {
                    _Code = value;
                    OnPropertyChanged(() => Code);
                }
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

        public string Symbol
        {
            get { return _Symbol; }
            set
            {
                if (_Symbol != value)
                {
                    _Symbol = value;
                    OnPropertyChanged(() => Symbol);
                }
            }
        }

        public double Rate
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
        public bool IsBase
        {
            get { return _IsBase; }
            set
            {
                if (_IsBase != value)
                {
                    _IsBase = value;
                    OnPropertyChanged(() => IsBase);
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

        public string LongDescription
        {
            get
            {
                return string.Format("{0} - {1}", _Name, _Symbol );
            }
        }

        class CurrencyValidator : AbstractValidator<Currency>
        {
            public CurrencyValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
                RuleFor(obj => obj.Symbol).NotEmpty().WithMessage("Symbol must not be empty.");
                
            }
        }

        protected override IValidator GetValidator()
        {
            return new CurrencyValidator();
        }
    }
}
