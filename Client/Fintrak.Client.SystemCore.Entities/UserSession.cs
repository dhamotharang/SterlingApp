using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class UserSession : ObjectBase
    {
        int _UserSessionId;
        int _UserId;
        string _CompanyCode;
        int _DatabaseId;
        bool _CanExpire;
        bool _Active;

        public int UserSessionId
        {
            get { return _UserSessionId; }
            set
            {
                if (_UserSessionId != value)
                {
                    _UserSessionId = value;
                    OnPropertyChanged(() => UserSessionId);
                }
            }
        }

        public int UserId
        {
            get { return _UserId; }
            set
            {
                if (_UserId != value)
                {
                    _UserId = value;
                    OnPropertyChanged(() => UserId);
                }
            }
        }

        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value.Trim();
                    OnPropertyChanged(() => CompanyCode);
                }
            }
        }

        public int DatabaseId
        {
            get { return _DatabaseId; }
            set
            {
                if (_DatabaseId != value)
                {
                    _DatabaseId = value;
                    OnPropertyChanged(() => DatabaseId);
                }
            }
        }

        public bool CanExpire
        {
            get { return _CanExpire; }
            set
            {
                if (_CanExpire != value)
                {
                    _CanExpire = value;
                    OnPropertyChanged(() => CanExpire);
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

       

        class UserSessionValidator : AbstractValidator<UserSession>
        {
            public UserSessionValidator()
            {
                RuleFor(obj => obj.CompanyCode).NotEmpty().WithMessage("CompanyCode must not be empty.");
                RuleFor(obj => obj.UserId).NotEmpty().WithMessage("UserId must not be empty.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new UserSessionValidator();
        }
    }
}
