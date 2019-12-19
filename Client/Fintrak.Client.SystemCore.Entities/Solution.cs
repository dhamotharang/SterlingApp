using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class Solution : ObjectBase
    {
        int _SolutionId;
        string _Name;
        string _Alias;
        bool _Active;

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

        class SolutionValidator : AbstractValidator<Solution>
        {
            public SolutionValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
                RuleFor(obj => obj.Alias).NotEmpty().WithMessage("Alias must not be empty.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new SolutionValidator();
        }
    }
}
