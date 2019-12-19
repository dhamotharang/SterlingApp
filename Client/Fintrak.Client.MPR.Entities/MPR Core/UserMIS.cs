using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class UserMIS : ObjectBase
    {
        int _UserMISId;
        string _LoginID;
        string _ProfitCenterDefinitionCode;
        string _ProfitCenterMisCode;
        string _CostCenterDefinitionCode;
        string _CostCenterMisCode;
        bool _Active;

        public int UserMISId
        {
            get { return _UserMISId; }
            set
            {
                if (_UserMISId != value)
                {
                    _UserMISId = value;
                    OnPropertyChanged(() => UserMISId);
                }
            }
        }

        public string LoginID
        {
            get { return _LoginID; }
            set
            {
                if (_LoginID != value)
                {
                    _LoginID = value;
                    OnPropertyChanged(() => LoginID);
                }
            }
        }

        public string ProfitCenterDefinitionCode
        {
            get { return _ProfitCenterDefinitionCode; }
            set
            {
                if (_ProfitCenterDefinitionCode != value)
                {
                    _ProfitCenterDefinitionCode = value;
                    OnPropertyChanged(() => ProfitCenterDefinitionCode);
                }
            }
        }

        public string ProfitCenterMisCode
        {
            get { return _ProfitCenterMisCode; }
            set
            {
                if (_ProfitCenterMisCode != value)
                {
                    _ProfitCenterMisCode = value;
                    OnPropertyChanged(() => ProfitCenterMisCode);
                }
            }
        }

        public string CostCenterDefinitionCode
        {
            get { return _CostCenterDefinitionCode; }
            set
            {
                if (_CostCenterDefinitionCode != value)
                {
                    _CostCenterDefinitionCode = value;
                    OnPropertyChanged(() => CostCenterDefinitionCode);
                }
            }
        }

        public string CostCenterMisCode
        {
            get { return _CostCenterMisCode; }
            set
            {
                if (_CostCenterMisCode != value)
                {
                    _CostCenterMisCode = value;
                    OnPropertyChanged(() => CostCenterMisCode);
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

        class UserMISValidator : AbstractValidator<UserMIS>
        {
            public UserMISValidator()
            {
                RuleFor(obj => obj.LoginID).NotEmpty().WithMessage("LoginID is required.");
                
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new UserMISValidator();
        }
    }
}
