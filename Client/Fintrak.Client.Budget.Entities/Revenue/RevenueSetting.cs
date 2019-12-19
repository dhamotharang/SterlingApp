using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class RevenueSetting : ObjectBase
    {
        int _RevenueSettingId;
        bool _UseRatioSharing;
        bool _UseTopNCustomers;
        bool _ActivateCustomerLevelBudget;
        string _GroupDefinitionCode;
        bool _UseGroupAggregate;
        string _Year;
        string _ReviewCode; 
        bool _Active;

        public int RevenueSettingId
        {
            get { return _RevenueSettingId; }
            set
            {
                if (_RevenueSettingId != value)
                {
                    _RevenueSettingId = value;
                    OnPropertyChanged(() => RevenueSettingId);
                }
            }
        }

        public bool UseRatioSharing
        {
            get { return _UseRatioSharing; }
            set
            {
                if (_UseRatioSharing != value)
                {
                    _UseRatioSharing = value;
                    OnPropertyChanged(() => UseRatioSharing);
                }
            }
        }

        public bool UseTopNCustomers
        {
            get { return _UseTopNCustomers; }
            set
            {
                if (_UseTopNCustomers != value)
                {
                    _UseTopNCustomers = value;
                    OnPropertyChanged(() => UseTopNCustomers);
                }
            }
        }

        public bool ActivateCustomerLevelBudget
        {
            get { return _ActivateCustomerLevelBudget; }
            set
            {
                if (_ActivateCustomerLevelBudget != value)
                {
                    _ActivateCustomerLevelBudget = value;
                    OnPropertyChanged(() => ActivateCustomerLevelBudget);
                }
            }
        }

        public string GroupDefinitionCode
        {
            get { return _GroupDefinitionCode; }
            set
            {
                if (_GroupDefinitionCode != value)
                {
                    _GroupDefinitionCode = value;
                    OnPropertyChanged(() => GroupDefinitionCode);
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

        public bool UseGroupAggregate
        {
            get { return _UseGroupAggregate; }
            set
            {
                if (_UseGroupAggregate != value)
                {
                    _UseGroupAggregate = value;
                    OnPropertyChanged(() => UseGroupAggregate);
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

        class RevenueSettingValidator : AbstractValidator<RevenueSetting>
        {
            public RevenueSettingValidator()
            {
                RuleFor(obj => obj.UseRatioSharing).NotEmpty().WithMessage("UseRatioSharing is required.");
                RuleFor(obj => obj.UseTopNCustomers).NotEmpty().WithMessage("UseTopNCustomers is required.");
                RuleFor(obj => obj.GroupDefinitionCode).NotEmpty().WithMessage("Definition is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new RevenueSettingValidator();
        }
    }
}
