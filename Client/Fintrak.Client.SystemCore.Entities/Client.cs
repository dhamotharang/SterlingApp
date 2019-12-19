using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class Client : ObjectBase
    {
        int _ClientId;
        string _Code;
        string _Name;
        bool _Active;

        public int ClientId
        {
            get { return _ClientId; }
            set
            {
                if (_ClientId != value)
                {
                    _ClientId = value;
                    OnPropertyChanged(() => ClientId);
                }
            }
        }

        public string Code
        {
            get { return _Code; }
            set
            {
                if (_Code != value)
                {
                    _Code = value.Trim();
                    OnPropertyChanged(() => Code);
                }
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value.Trim();
                    OnPropertyChanged(() => Name);
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

        public string LongDescription
        {
            get
            {
                return string.Format("{0} - {1}", _Name, _Code);
            }
        }

        class ClientValidator : AbstractValidator<Client>
        {
            public ClientValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code must not be empty.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ClientValidator();
        }
    }
}
