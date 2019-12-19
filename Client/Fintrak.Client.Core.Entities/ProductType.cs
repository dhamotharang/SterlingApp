using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.Core.Entities
{
    public class ProductType : ObjectBase
    {
        int _ProductTypeId;
        string _Name;
        bool _Active;

        public int ProductTypeId
        {
            get { return _ProductTypeId; }
            set
            {
                if (_ProductTypeId != value)
                {
                    _ProductTypeId = value;
                    OnPropertyChanged(() => ProductTypeId);
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
                return string.Format("{0}", _Name );
            }
        }

        class ProductTypeValidator : AbstractValidator<ProductType>
        {
            public ProductTypeValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProductTypeValidator();
        }
    }
}
