using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class TeamUser : ObjectBase
    {
        int _TeamUserId;
        string _LoginID;
        string _PCDefinitionCode;
        string _PCMisCode;
        string _CCDefinitionCode;
        string _CCMisCode;
        bool _IsLock;
        bool _Active;

        public int TeamUserId
        {
            get { return _TeamUserId; }
            set
            {
                if (_TeamUserId != value)
                {
                    _TeamUserId = value;
                    OnPropertyChanged(() => TeamUserId);
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

        public string PCDefinitionCode
        {
            get { return _PCDefinitionCode; }
            set
            {
                if (_PCDefinitionCode != value)
                {
                    _PCDefinitionCode = value;
                    OnPropertyChanged(() => PCDefinitionCode);
                }
            }
        }

        public string PCMisCode
        {
            get { return _PCMisCode; }
            set
            {
                if (_PCMisCode != value)
                {
                    _PCMisCode = value;
                    OnPropertyChanged(() => PCMisCode);
                }
            }
        }

        public string CCDefinitionCode
        {
            get { return _CCDefinitionCode; }
            set
            {
                if (_CCDefinitionCode != value)
                {
                    _CCDefinitionCode = value;
                    OnPropertyChanged(() => CCDefinitionCode);
                }
            }
        }


        public string CCMisCode
        {
            get { return _CCMisCode; }
            set
            {
                if (_CCMisCode != value)
                {
                    _CCMisCode = value;
                    OnPropertyChanged(() => CCMisCode);
                }
            }
        }

        public bool IsLock
        {
            get { return _IsLock; }
            set
            {
                if (_IsLock != value)
                {
                    _IsLock = value;
                    OnPropertyChanged(() => IsLock);
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

        class TeamUserValidator : AbstractValidator<TeamUser>
        {
            public TeamUserValidator()
            {
                RuleFor(obj => obj.LoginID).NotEmpty().WithMessage("LoginID is required.");
                RuleFor(obj => obj.PCDefinitionCode).NotEmpty().WithMessage("PCDefinitionCode is required.");
                RuleFor(obj => obj.CCDefinitionCode).NotEmpty().WithMessage("Definition is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new TeamUserValidator();
        }
    }
}
