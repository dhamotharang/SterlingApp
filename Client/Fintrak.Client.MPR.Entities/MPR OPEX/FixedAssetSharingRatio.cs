using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class FixedAssetSharingRatio : ObjectBase
    {
        int _FixedAssetSharingRatioId;
        string _Branch;
        string _SBUCode;
        double _Ratio;
        bool _Active;


        public int FixedAssetSharingRatioId
        {
            get { return _FixedAssetSharingRatioId; }
            set
            {
                if (_FixedAssetSharingRatioId != value)
                {
                    _FixedAssetSharingRatioId = value;
                    OnPropertyChanged(() => FixedAssetSharingRatioId);
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
        public string SBUCode
        {
            get { return _SBUCode; }
            set
            {
                if (_SBUCode != value)
                {
                    _SBUCode = value;
                    OnPropertyChanged(() => SBUCode);
                }
            }
        }
       

        public double Ratio
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


        
        class FixedAssetSharingRatioValidator : AbstractValidator<FixedAssetSharingRatio>
        {
            public FixedAssetSharingRatioValidator()
            {
                RuleFor(obj => obj.Branch).NotEmpty().WithMessage("Branch is required.");               
             }
        }

        protected override IValidator GetValidator()
        {
            return new FixedAssetSharingRatioValidator();
        }
    }
}
