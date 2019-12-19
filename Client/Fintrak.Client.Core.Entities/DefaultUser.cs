using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.Core.Entities
{
    public class DefaultUser : ObjectBase
    {
        int _DefaultUserId;
        string _LoginID;
        int _SolutionId;      
        bool _Active;

        public int DefaultUserId
        {
            get { return _DefaultUserId; }
            set
            {
                if (_DefaultUserId != value)
                {
                    _DefaultUserId = value;
                    OnPropertyChanged(() => DefaultUserId);
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

        

        public int SolutionId
        {
            get { return _SolutionId; }
            set
            {
                if (_SolutionId != value)
                {
                    _SolutionId = value;
                    OnPropertyChanged(() => SolutionId);
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

       
        class DefaultUserValidator : AbstractValidator<DefaultUser>
        {
            public DefaultUserValidator()
            {
                RuleFor(obj => obj.LoginID).NotEmpty().WithMessage("LoginID must not be empty.");
                RuleFor(obj => obj.SolutionId).NotEmpty().WithMessage("Solution must not be empty.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new DefaultUserValidator();
        }
    }
}
