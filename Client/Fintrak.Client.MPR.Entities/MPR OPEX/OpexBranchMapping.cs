using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class OpexBranchMapping : ObjectBase
    {
        int _OpexBranchMappingId;
        string _BranchCode;
        string _MisCode;
        bool _Active;


        public int OpexBranchMappingId
        {
            get { return _OpexBranchMappingId; }
            set
            {
                if (_OpexBranchMappingId != value)
                {
                    _OpexBranchMappingId = value;
                    OnPropertyChanged(() => OpexBranchMappingId);
                }
            }
        }


        public string BranchCode
        {
            get { return _BranchCode; }
            set
            {
                if (_BranchCode != value)
                {
                    _BranchCode = value;
                    OnPropertyChanged(() => BranchCode);
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



        class OpexBranchMappingValidator : AbstractValidator<OpexBranchMapping>
        {
            public OpexBranchMappingValidator()
            {
                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("Mis Code is required.");
                RuleFor(obj => obj.BranchCode).NotEmpty().WithMessage("Branch Code is required.");




            }
        }

        protected override IValidator GetValidator()
        {
            return new OpexBranchMappingValidator();
        }
    }
}
