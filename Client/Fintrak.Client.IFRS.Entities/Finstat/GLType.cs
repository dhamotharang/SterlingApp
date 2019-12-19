using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class GLType : ObjectBase
    {
        int _GLTypeId;
        string _Name;
        string _Description;
        bool _Active;

        public int GLTypeId
        {
            get { return _GLTypeId; }
            set
            {
                if (_GLTypeId != value)
                {
                    _GLTypeId = value;
                    OnPropertyChanged(() => GLTypeId);
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

        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
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

        
        class GLTypeValidator : AbstractValidator<GLType>
        {
            public GLTypeValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new GLTypeValidator();
        }
    }
}
