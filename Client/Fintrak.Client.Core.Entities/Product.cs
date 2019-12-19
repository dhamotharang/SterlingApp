using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.Core.Entities
{
    public class Product : ObjectBase
    {
        int _ProductId;
        string _Code;
        string _Name;
        string _AssetGL;
        string _LiabilityGL;
        string _IncomeGL;
        string _ExpenseGL;
        bool _IsSwitch;
        bool _Active;

        public int ProductId
        {
            get { return _ProductId; }
            set
            {
                if (_ProductId != value)
                {
                    _ProductId = value;
                    OnPropertyChanged(() => ProductId);
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
                    _Code = value.Trim();
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
                    _Name = value.Trim();
                    OnPropertyChanged(() => Name);
                }
            }
        }

        public string AssetGL
        {
            get { return _AssetGL; }
            set
            {
                if (_AssetGL != value)
                {
                    _AssetGL = value.Trim();
                    OnPropertyChanged(() => AssetGL);
                }
            }
        }

        public string LiabilityGL
        {
            get { return _LiabilityGL; }
            set
            {
                if (_LiabilityGL != value)
                {
                    _LiabilityGL = value.Trim();
                    OnPropertyChanged(() => LiabilityGL);
                }
            }
        }

        public string IncomeGL
        {
            get { return _IncomeGL; }
            set
            {
                if (_IncomeGL != value)
                {
                    _IncomeGL = value.Trim();
                    OnPropertyChanged(() => IncomeGL);
                }
            }
        }

        public string ExpenseGL
        {
            get { return _ExpenseGL; }
            set
            {
                if (_ExpenseGL != value)
                {
                    _ExpenseGL = value.Trim();
                    OnPropertyChanged(() => ExpenseGL);
                }
            }
        }


        public bool IsSwitch
        {
            get { return _IsSwitch; }
            set
            {
                if (_IsSwitch != value)
                {
                    _IsSwitch = value;
                    OnPropertyChanged(() => IsSwitch);
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
                return string.Format("{0} - {1}", _Name, _Code );
            }
        }

        class ProductValidator : AbstractValidator<Product>
        {
            public ProductValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code must not be empty.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProductValidator();
        }
    }
}
