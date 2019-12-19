using System;
using System.Linq;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class FairValueBasisExemption : ObjectBase
    {
        int _FairValueBasisExemptionId;
        IFRSInstrument _InstrumentType;
        string _RefNo;
        int _BasisLevel;
        string _CompanyCode;
        bool _Active;

        public int FairValueBasisExemptionId
        {
            get { return _FairValueBasisExemptionId; }
            set
            {
                if (_FairValueBasisExemptionId != value)
                {
                    _FairValueBasisExemptionId = value;
                    OnPropertyChanged(() => FairValueBasisExemptionId);
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

        public string RefNo
        {
            get { return _RefNo; }
            set
            {
                if (_RefNo != value)
                {
                    _RefNo = value;
                    OnPropertyChanged(() => RefNo);
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


        class FairValueBasisExemptionValidator : AbstractValidator<FairValueBasisExemption>
        {
            public FairValueBasisExemptionValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
                RuleFor(obj => obj.BasisLevel).NotEmpty().WithMessage("BasisLevel is required.");
                RuleFor(obj => obj.InstrumentType).NotEmpty().WithMessage("InstrumentType is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new FairValueBasisExemptionValidator();
        }
    }
}
