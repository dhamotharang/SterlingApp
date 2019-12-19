using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class General : ObjectBase
    {
        int _GeneralId;
        string _Host;
        string _Email;
        string _Password;
        bool _EnableCompanyDefaultLogin;
        string _DefaultCompanyCode;
        bool _Active;

        public int GeneralId
        {
            get { return _GeneralId; }
            set
            {
                if (_GeneralId != value)
                {
                    _GeneralId = value;
                    OnPropertyChanged(() => GeneralId);
                }
            }
        }

        public string Host
        {
            get { return _Host; }
            set
            {
                if (_Host != value)
                {
                    _Host = value;
                    OnPropertyChanged(() => Host);
                }
            }
        }

        public string Email
        {
            get { return _Email; }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    OnPropertyChanged(() => Email);
                }
            }
        }

        public string Password
        {
            get { return _Password; }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    OnPropertyChanged(() => Password);
                }
            }
        }

        public string DefaultCompanyCode
        {
            get { return _DefaultCompanyCode; }
            set
            {
                if (_DefaultCompanyCode != value)
                {
                    _DefaultCompanyCode = value;
                    OnPropertyChanged(() => DefaultCompanyCode);
                }
            }
        }

        public bool EnableCompanyDefaultLogin
        {
            get { return _EnableCompanyDefaultLogin; }
            set
            {
                if (_EnableCompanyDefaultLogin != value)
                {
                    _EnableCompanyDefaultLogin = value;
                    OnPropertyChanged(() => EnableCompanyDefaultLogin);
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

        class GeneralValidator : AbstractValidator<General>
        {
            public GeneralValidator()
            {
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new GeneralValidator();
        }
    }
}
