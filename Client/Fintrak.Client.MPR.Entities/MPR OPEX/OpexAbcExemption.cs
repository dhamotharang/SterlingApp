using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class OpexAbcExemption : ObjectBase
    {
        int _OpexAbcExemptionId;
        string _MisCode;
        string _CompanyCode; 
        bool _Active;


        public int OpexAbcExemptionId
        {
            get { return _OpexAbcExemptionId; }
            set
            {
                if (_OpexAbcExemptionId != value)
                {
                    _OpexAbcExemptionId = value;
                    OnPropertyChanged(() => OpexAbcExemptionId);
                }
            }
        }

        public string MisCode
        {
            get { return _MisCode; }
            set
            {
                if (_MisCode != value)
                {
                    _MisCode = value;
                    OnPropertyChanged(() => MisCode);
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


        
        class OpexAbcExemptionValidator : AbstractValidator<OpexAbcExemption>
        {
            public OpexAbcExemptionValidator()
            {
                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("MisCode is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new OpexAbcExemptionValidator();
        }
    }
}
