using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class MPRGLMapping : ObjectBase
    {
        int _MPRGLMappingId;
        string _GLCode;
        string _CaptionCode;
        string _CompanyCode;
        bool _Active;


        public int MPRGLMappingId
        {
            get { return _MPRGLMappingId; }
            set
            {
                if (_MPRGLMappingId != value)
                {
                    _MPRGLMappingId = value;
                    OnPropertyChanged(() => MPRGLMappingId);
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


        
        class MPRGLMappingValidator : AbstractValidator<MPRGLMapping>
        {
            public MPRGLMappingValidator()
            {
                RuleFor(obj => obj.GLCode).NotEmpty().WithMessage("GL is required.");
                RuleFor(obj => obj.CaptionCode).NotEmpty().WithMessage("Caption is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new MPRGLMappingValidator();
        }
    }
}
