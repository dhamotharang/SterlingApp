using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class Menu : ObjectBase
    {
        int _MenuId;
        string _Name;
        string _Alias;
        string _Action;
        string _ActionUrl;
        byte[] _Image;
        string _ImageUrl;
        int? _ParentId;
        int _ModuleId;
        bool _Active;

        public int MenuId
        {
            get { return _MenuId; }
            set
            {
                if (_MenuId != value)
                {
                    _MenuId = value;
                    OnPropertyChanged(() => MenuId);
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
                    _Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

        public string Alias
        {
            get { return _Alias; }
            set
            {
                if (_Alias != value)
                {
                    _Alias = value;
                    OnPropertyChanged(() => Alias);
                }
            }
        }

        public string Action
        {
            get { return _Action; }
            set
            {
                if (_Action != value)
                {
                    _Action = value;
                    OnPropertyChanged(() => Action);
                }
            }
        }

        public string ActionUrl
        {
            get { return _ActionUrl; }
            set
            {
                if (_ActionUrl != value)
                {
                    _ActionUrl = value;
                    OnPropertyChanged(() => ActionUrl);
                }
            }
        }

        public byte[] Image
        {
            get { return _Image; }
            set
            {
                if (_Image != value)
                {
                    _Image = value;
                    OnPropertyChanged(() => Image);
                }
            }
        }

        public string ImageUrl
        {
            get { return _ImageUrl; }
            set
            {
                if (_ImageUrl != value)
                {
                    _ImageUrl = value;
                    OnPropertyChanged(() => ImageUrl);
                }
            }
        }

        public int? ParentId
        {
            get { return _ParentId; }
            set
            {
                if (_ParentId != value)
                {
                    _ParentId = value;
                    OnPropertyChanged(() => ParentId);
                }
            }
        }

        public int ModuleId
        {
            get { return _ModuleId; }
            set
            {
                if (_ModuleId != value)
                {
                    _ModuleId = value;
                    OnPropertyChanged(() => ModuleId);
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
                return string.Format("{0} - {1}", _Name, _Alias );
            }
        }

        class MenuValidator : AbstractValidator<Menu>
        {
            public MenuValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
                RuleFor(obj => obj.Alias).NotEmpty().WithMessage("Alias must not be empty.");
                RuleFor(obj => obj.ModuleId).GreaterThan(0).WithMessage("Module is require.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MenuValidator();
        }
    }
}
