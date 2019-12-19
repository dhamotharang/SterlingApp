using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class TeamSetting : ObjectBase
    {
        int _TeamSettingId;
        bool _EnableBudgetToMPRSynch;
        bool _EnableMPRToBudgetSynch;
        bool _Active;

        public int TeamSettingId
        {
            get { return _TeamSettingId; }
            set
            {
                if (_TeamSettingId != value)
                {
                    _TeamSettingId = value;
                    OnPropertyChanged(() => TeamSettingId);
                }
            }
        }


        public bool EnableBudgetToMPRSynch
        {
            get { return _EnableBudgetToMPRSynch; }
            set
            {
                if (_EnableBudgetToMPRSynch != value)
                {
                    _EnableBudgetToMPRSynch = value;
                    OnPropertyChanged(() => EnableBudgetToMPRSynch);
                }
            }
        }


        public bool EnableMPRToBudgetSynch
        {
            get { return _EnableMPRToBudgetSynch; }
            set
            {
                if (_EnableMPRToBudgetSynch != value)
                {
                    _EnableMPRToBudgetSynch = value;
                    OnPropertyChanged(() => EnableMPRToBudgetSynch);
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

        
        class TeamSettingValidator : AbstractValidator<TeamSetting>
        {
            public TeamSettingValidator()
            {
                RuleFor(obj => obj.EnableBudgetToMPRSynch).NotEmpty().WithMessage("EnableBudgetToMPRSynch is required.");
                RuleFor(obj => obj.EnableMPRToBudgetSynch).NotEmpty().WithMessage("EnableMPRToBudgetSynch is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new TeamSettingValidator();
        }
    }
}
