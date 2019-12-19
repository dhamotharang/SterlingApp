using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class InstrumentTypeGLMap : ObjectBase
    {
        int _InstrumentTypeGLMapId;
        int _InstrumentTypeId;
        int _GLTypeId;
        string _GLCode;
        string _CompanyCode;
        bool _Active;

        public int InstrumentTypeGLMapId
        {
            get { return _InstrumentTypeGLMapId; }
            set
            {
                if (_InstrumentTypeGLMapId != value)
                {
                    _InstrumentTypeGLMapId = value;
                    OnPropertyChanged(() => InstrumentTypeGLMapId);
                }
            }
        }

        public int InstrumentTypeId
        {
            get { return _InstrumentTypeId; }
            set
            {
                if (_InstrumentTypeId != value)
                {
                    _InstrumentTypeId = value;
                    OnPropertyChanged(() => InstrumentTypeId);
                }
            }
        }

        public int GLTypeId
        {
            get { return _GLTypeId; }
            set
            {
                if (_GLTypeId != value)
                {
                    _GLTypeId = value;
                    OnPropertyChanged(() => GLTypeId);
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

        
        class InstrumentTypeGLMapValidator : AbstractValidator<InstrumentTypeGLMap>
        {
            public InstrumentTypeGLMapValidator()
            {
                RuleFor(obj => obj.GLCode).NotEmpty().WithMessage("GLCode is required.");
                RuleFor(obj => obj.InstrumentTypeId).GreaterThan(0).WithMessage("Instrument Type is required.");
                RuleFor(obj => obj.GLTypeId).GreaterThan(0).WithMessage("GL Type is required.");               
            }
        }

        protected override IValidator GetValidator()
        {
            return new InstrumentTypeGLMapValidator();
        }
    }
}
