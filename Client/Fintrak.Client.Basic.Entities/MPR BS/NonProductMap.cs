using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class NonProductMap : ObjectBase
    {
        int _NonProductMapId;
        string _NonProductCode;
        string _CaptionCode;
        string _ProductCode;
        string _CompanyCode;
        bool _Active;

        public int NonProductMapId
        {
            get { return _NonProductMapId; }
            set
            {
                if (_NonProductMapId != value)
                {
                    _NonProductMapId = value;
                    OnPropertyChanged(() => NonProductMapId);
                }
            }
        }

        public string NonProductCode
        {
            get { return _NonProductCode; }
            set
            {
                if (_NonProductCode != value)
                {
                    _NonProductCode = value;
                    OnPropertyChanged(() => NonProductCode);
                }
            }
        }

        public string CaptionCode
        {
            get { return _CaptionCode; }
            set
            {
                if (_CaptionCode != value)
                {
                    _CaptionCode = value;
                    OnPropertyChanged(() => CaptionCode);
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

        
        class NonProductMapValidator : AbstractValidator<NonProductMap>
        {
            public NonProductMapValidator()
            {
                RuleFor(obj => obj.NonProductCode).NotEmpty().WithMessage("NonProduct Code is required.");
                RuleFor(obj => obj.CaptionCode).NotEmpty().WithMessage("Caption is required.");
                RuleFor(obj => obj.ProductCode).NotEmpty().WithMessage("Product Code is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new NonProductMapValidator();
        }
    }
}
