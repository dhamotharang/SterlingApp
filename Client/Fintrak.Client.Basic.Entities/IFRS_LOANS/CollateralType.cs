using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class CollateralType : ObjectBase
    {
        int _CollateralTypeId;
        string _Code;
        string _Name;
        string _CategoryCode;
        string _CompanyCode;
        bool _Active;

        public int CollateralTypeId
        {
            get { return _CollateralTypeId; }
            set
            {
                if (_CollateralTypeId != value)
                {
                    _CollateralTypeId = value;
                    OnPropertyChanged(() => CollateralTypeId);
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

        public string CategoryCode
        {
            get { return _CategoryCode; }
            set
            {
                if (_CategoryCode != value)
                {
                    _CategoryCode = value;
                    OnPropertyChanged(() => CategoryCode);
                }
            }
        }

        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value;
                    OnPropertyChanged(() => CompanyCode);
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


        class CollateralTypeValidator : AbstractValidator<CollateralType>
        {
            public CollateralTypeValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
                RuleFor(obj => obj.CategoryCode).NotEmpty().WithMessage("CategoryCode is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new CollateralTypeValidator();
        }
    }
}
