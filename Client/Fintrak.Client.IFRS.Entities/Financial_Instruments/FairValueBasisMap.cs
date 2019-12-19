using System;
using System.Linq;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class FairValueBasisMap : ObjectBase
    {
        int _FairValueBasisMapId;
        IFRSInstrument _InstrumentType;
        FIClassification _Classification;
        int _BasisLevel;
        string _CompanyCode;
        bool _Active;

        public int FairValueBasisMapId
        {
            get { return _FairValueBasisMapId; }
            set
            {
                if (_FairValueBasisMapId != value)
                {
                    _FairValueBasisMapId = value;
                    OnPropertyChanged(() => FairValueBasisMapId);
                }
            }
        }


        public IFRSInstrument InstrumentType
        {
            get { return _InstrumentType; }
            set
            {
                if (_InstrumentType != value)
                {
                    _InstrumentType = value;
                    OnPropertyChanged(() => InstrumentType);
                }
            }
        }

        public FIClassification Classification
        {
            get { return _Classification; }
            set
            {
                if (_Classification != value)
                {
                    _Classification = value;
                    OnPropertyChanged(() => Classification);
                }
            }
        }

        public int BasisLevel
        {
            get { return _BasisLevel; }
            set
            {
                if (_BasisLevel != value)
                {
                    _BasisLevel = value;
                    OnPropertyChanged(() => BasisLevel);
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


        class FairValueBasisMapValidator : AbstractValidator<FairValueBasisMap>
        {
            public FairValueBasisMapValidator()
            {
                RuleFor(obj => obj.Classification).NotEmpty().WithMessage("Classification is required.");
                RuleFor(obj => obj.BasisLevel).NotEmpty().WithMessage("BasisLevel is required.");
                RuleFor(obj => obj.InstrumentType).NotEmpty().WithMessage("IntrumentType is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new FairValueBasisMapValidator();
        }
    }
}
