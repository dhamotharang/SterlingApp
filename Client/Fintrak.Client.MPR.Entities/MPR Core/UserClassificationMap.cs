using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class UserClassificationMap : ObjectBase
    {
        int _UserClassificationMapId;
        string _LoginID;
        string _ClassificationCode;
        int _Level;
        string _ClassificationTypeCode;
        bool _Active;

        public int UserClassificationMapId
        {
            get { return _UserClassificationMapId; }
            set
            {
                if (_UserClassificationMapId != value)
                {
                    _UserClassificationMapId = value;
                    OnPropertyChanged(() => UserClassificationMapId);
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

        public string ClassificationCode
        {
            get { return _ClassificationCode; }
            set
            {
                if (_ClassificationCode != value)
                {
                    _ClassificationCode = value;
                    OnPropertyChanged(() => ClassificationCode);
                }
            }
        }

        public int Level
        {
            get { return _Level; }
            set
            {
                if (_Level != value)
                {
                    _Level = value;
                    OnPropertyChanged(() => Level);
                }
            }
        }

        public string ClassificationTypeCode
        {
            get { return _ClassificationTypeCode; }
            set
            {
                if (_ClassificationTypeCode != value)
                {
                    _ClassificationTypeCode = value;
                    OnPropertyChanged(() => ClassificationTypeCode);
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

        class UserClassificationMapValidator : AbstractValidator<UserClassificationMap>
        {
            public UserClassificationMapValidator()
            {
                RuleFor(obj => obj.LoginID).NotEmpty().WithMessage("LoginID is required.");
                
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new UserClassificationMapValidator();
        }
    }
}
