using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class OpexMISReplacement : ObjectBase
    {
        int _OpexMISReplacementId;
        string _OldMISCode;
        string _MISCode;
        bool _Active;


        public int OpexMISReplacementId
        {
            get { return _OpexMISReplacementId; }
            set
            {
                if (_OpexMISReplacementId != value)
                {
                    _OpexMISReplacementId = value;
                    OnPropertyChanged(() => OpexMISReplacementId);
                }
            }
        }

        public string OldMISCode
        {
            get { return _OldMISCode; }
            set
            {
                if (_OldMISCode != value)
                {
                    _OldMISCode = value;
                    OnPropertyChanged(() => OldMISCode);
                }
            }
        }

        public string MISCode
        {
            get { return _MISCode; }
            set
            {
                if (_MISCode != value)
                {
                    _MISCode = value;
                    OnPropertyChanged(() => MISCode);
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


        
        class OpexMISReplacementValidator : AbstractValidator<OpexMISReplacement>
        {
            public OpexMISReplacementValidator()
            {
                RuleFor(obj => obj.OldMISCode).NotEmpty().WithMessage("OldMISCode is required.");
                RuleFor(obj => obj.MISCode).NotEmpty().WithMessage("MISCode is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new OpexMISReplacementValidator();
        }
    }
}
