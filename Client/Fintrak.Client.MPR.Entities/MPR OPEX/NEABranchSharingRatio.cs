using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class NEABranchSharingRatio : ObjectBase
    {
        int _NEABranchSharingRatioId;
        string _OwnerBranch;
        string _Beneficiary;
        string _ServiceCategory;
        string _GL;
        decimal _Ratio;
        bool _Active;


        public int NEABranchSharingRatioId
        {
            get { return _NEABranchSharingRatioId; }
            set
            {
                if (_NEABranchSharingRatioId != value)
                {
                    _NEABranchSharingRatioId = value;
                    OnPropertyChanged(() => NEABranchSharingRatioId);
                }
            }
        }

        public string OwnerBranch
        {
            get { return _OwnerBranch; }
            set
            {
                if (_OwnerBranch != value)
                {
                    _OwnerBranch = value;
                    OnPropertyChanged(() => OwnerBranch);
                }
            }
        }

        public string Beneficiary
        {
            get { return _Beneficiary; }
            set
            {
                if (_Beneficiary != value)
                {
                    _Beneficiary = value;
                    OnPropertyChanged(() => Beneficiary);
                }
            }
        }


        public string ServiceCategory
        {
            get { return _ServiceCategory; }
            set
            {
                if (_ServiceCategory != value)
                {
                    _ServiceCategory = value;
                    OnPropertyChanged(() => ServiceCategory);
                }
            }
        }

        public string GL
        {
            get { return _GL; }
            set
            {
                if (_GL != value)
                {
                    _GL = value;
                    OnPropertyChanged(() => GL);
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



        class NEABranchSharingRatioValidator : AbstractValidator<NEABranchSharingRatio>
        {
            public NEABranchSharingRatioValidator()
            {
                RuleFor(obj => obj.OwnerBranch).NotEmpty().WithMessage("OwnerBranch is required.");
                RuleFor(obj => obj.Beneficiary).NotEmpty().WithMessage("Beneficiary is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new NEABranchSharingRatioValidator();
        }
    }
}
