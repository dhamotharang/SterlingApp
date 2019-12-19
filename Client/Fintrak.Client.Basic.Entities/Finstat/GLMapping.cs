using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class GLMapping : ObjectBase
    {
        int _GLMappingId;
        string _GLCode;
        string _GLDescription;
        string _GLParentCode;
        string _CaptionCode;
        string _SubCaption;
        string _SubCaption1;
        string _SubCaption2;
        string _SubCaption3;
        string _SubCaption4;
        string _CompanyCode;
        int _SubPosition;
        bool _Active;

        public int GLMappingId
        {
            get { return _GLMappingId; }
            set
            {
                if (_GLMappingId != value)
                {
                    _GLMappingId = value;
                    OnPropertyChanged(() => GLMappingId);
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

        public string GLDescription
        {
            get { return _GLDescription; }
            set
            {
                if (_GLDescription != value)
                {
                    _GLDescription = value;
                    OnPropertyChanged(() => GLDescription);
                }
            }
        }

        public string GLParentCode
        {
            get { return _GLParentCode; }
            set
            {
                if (_GLParentCode != value)
                {
                    _GLParentCode = value;
                    OnPropertyChanged(() => GLParentCode);
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
        
        public string SubCaption
        {
            get { return _SubCaption; }
            set
            {
                if (_SubCaption != value)
                {
                    _SubCaption = value;
                    OnPropertyChanged(() => SubCaption);
                }
            }
        }

        public string SubCaption1
        {
            get { return _SubCaption1; }
            set
            {
                if (_SubCaption1 != value)
                {
                    _SubCaption1 = value;
                    OnPropertyChanged(() => SubCaption1);
                }
            }
        }

        public string SubCaption2
        {
            get { return _SubCaption2; }
            set
            {
                if (_SubCaption2 != value)
                {
                    _SubCaption2 = value;
                    OnPropertyChanged(() => SubCaption2);
                }
            }
        }

        public string SubCaption3
        {
            get { return _SubCaption3; }
            set
            {
                if (_SubCaption3 != value)
                {
                    _SubCaption3 = value;
                    OnPropertyChanged(() => SubCaption3);
                }
            }
        }

        public string SubCaption4
        {
            get { return _SubCaption4; }
            set
            {
                if (_SubCaption4 != value)
                {
                    _SubCaption4 = value;
                    OnPropertyChanged(() => SubCaption4);
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

        public int SubPosition
        {
            get { return _SubPosition; }
            set
            {
                if (_SubPosition != value)
                {
                    _SubPosition = value;
                    OnPropertyChanged(() => SubPosition);
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
                return string.Format("{0} - {1}", _GLCode, _GLDescription);
            }
        }

        
        class GLMappingValidator : AbstractValidator<GLMapping>
        {
            public GLMappingValidator()
            {
                RuleFor(obj => obj.GLCode).NotEmpty().WithMessage("GLCode is required.");
                RuleFor(obj => obj.CompanyCode).NotEmpty().WithMessage("Company is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new GLMappingValidator();
        }
    }
}
