using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class FeeVolumeBasedSetup : ObjectBase
    {
        int _FeeVolumeBasedSetupId;
        string _FeeCode;
        string _MakeUpCode;
        string _Year;
        string _ReviewCode; 
        bool _Active;

        public int FeeVolumeBasedSetupId
        {
            get { return _FeeVolumeBasedSetupId; }
            set
            {
                if (_FeeVolumeBasedSetupId != value)
                {
                    _FeeVolumeBasedSetupId = value;
                    OnPropertyChanged(() => FeeVolumeBasedSetupId);
                }
            }
        }

        public string FeeCode
        {
            get { return _FeeCode; }
            set
            {
                if (_FeeCode != value)
                {
                    _FeeCode = value;
                    OnPropertyChanged(() => FeeCode);
                }
            }
        }

        public string MakeUpCode
        {
            get { return _MakeUpCode; }
            set
            {
                if (_MakeUpCode != value)
                {
                    _MakeUpCode = value;
                    OnPropertyChanged(() => MakeUpCode);
                }
            }
        }


        public string Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    OnPropertyChanged(() => Year);
                }
            }
        }


        public string ReviewCode
        {
            get { return _ReviewCode; }
            set
            {
                if (_ReviewCode != value)
                {
                    _ReviewCode = value;
                    OnPropertyChanged(() => ReviewCode);
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

        class FeeVolumeBasedSetupValidator : AbstractValidator<FeeVolumeBasedSetup>
        {
            public FeeVolumeBasedSetupValidator()
            {
                RuleFor(obj => obj.FeeCode).NotEmpty().WithMessage("FeeCode is required.");
                RuleFor(obj => obj.MakeUpCode).NotEmpty().WithMessage("MakeUpCode is required.");              
            }
        }

        protected override IValidator GetValidator()
        {
            return new FeeVolumeBasedSetupValidator();
        }
    }
}
