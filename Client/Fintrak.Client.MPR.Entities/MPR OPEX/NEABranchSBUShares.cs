using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class NEABranchSBUShares : ObjectBase
    {
        int _NEABranchSBUSharesId;
        string _Branch;
        string _SBU;
        decimal _Ratio;
        bool _Active;


        public int NEABranchSBUSharesId
        {
            get { return _NEABranchSBUSharesId; }
            set
            {
                if (_NEABranchSBUSharesId != value)
                {
                    _NEABranchSBUSharesId = value;
                    OnPropertyChanged(() => NEABranchSBUSharesId);
                }
            }
        }

        public string Branch
        {
            get { return _Branch; }
            set
            {
                if (_Branch != value)
                {
                    _Branch = value;
                    OnPropertyChanged(() => Branch);
                }
            }
        }

        public string SBU
        {
            get { return _SBU; }
            set
            {
                if (_SBU != value)
                {
                    _SBU = value;
                    OnPropertyChanged(() => SBU);
                }
            }
        }


        public decimal Ratio
        {
            get { return _Ratio; }
            set
            {
                if (_Ratio != value)
                {
                    _Ratio = value;
                    OnPropertyChanged(() => Ratio);
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


        
        class NEABranchSBUSharesValidator : AbstractValidator<NEABranchSBUShares>
        {
            public NEABranchSBUSharesValidator()
            {
                RuleFor(obj => obj.Branch).NotEmpty().WithMessage("Branch is required.");
                RuleFor(obj => obj.SBU).NotEmpty().WithMessage("SBU is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new NEABranchSBUSharesValidator();
        }
    }
}
