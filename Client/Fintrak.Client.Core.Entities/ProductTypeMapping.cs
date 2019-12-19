using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.Core.Entities
{
    public class ProductTypeMapping : ObjectBase
    {
        int _ProductTypeMappingId;
        string _ProductCode;
        string _ProductType;
        bool _Active;

        public int ProductTypeMappingId
        {
            get { return _ProductTypeMappingId; }
            set
            {
                if (_ProductTypeMappingId != value)
                {
                    _ProductTypeMappingId = value;
                    OnPropertyChanged(() => ProductTypeMappingId);
                }
            }
        }

        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                if (_ProductCode != value)
                {
                    _ProductCode = value;
                    OnPropertyChanged(() => ProductCode);
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

        class ProductTypeMappingValidator : AbstractValidator<ProductTypeMapping>
        {
            public ProductTypeMappingValidator()
            {
                RuleFor(obj => obj.ProductCode).NotEmpty().WithMessage("Product is require.");
                RuleFor(obj => obj.ProductType).NotEmpty().WithMessage("Product Type is require.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProductTypeMappingValidator();
        }
    }
}
