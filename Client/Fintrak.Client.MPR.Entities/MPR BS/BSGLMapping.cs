using System;
using System.Linq;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class BSGLMapping : ObjectBase
    {
        int _BSGLMappingId;
        string _GLCode;
        string _ProductCode;
        string _CompanyCode; 
        bool _Active;

        public int BSGLMappingId
        {
            get { return _BSGLMappingId; }
            set
            {
                if (_BSGLMappingId != value)
                {
                    _BSGLMappingId = value;
                    OnPropertyChanged(() => BSGLMappingId);
                }
            }
        }

        public string GLCode
        {
            get { return _GLCode; }
            set
            {
                if (_GLCode != value)
                {
                    _GLCode = value;
                    OnPropertyChanged(() => GLCode);
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

        
        class BSGLMappingValidator : AbstractValidator<BSGLMapping>
        {
            public BSGLMappingValidator()
            {
                RuleFor(obj => obj.ProductCode).NotEmpty().WithMessage("Product is required.");
                RuleFor(obj => obj.GLCode).NotEmpty().WithMessage("GLCode is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new BSGLMappingValidator();
        }
    }
}
