using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class Team : ObjectBase
    {
        int _TeamId;
        string _Code;
        string _Name;
        string _ParentCode;
        string _DefinitionCode;
        string _Year;
        string _ClassificationCode;
        string _ReviewCode; 
        bool _Active;

        public int TeamId
        {
            get { return _TeamId; }
            set
            {
                if (_TeamId != value)
                {
                    _TeamId = value;
                    OnPropertyChanged(() => TeamId);
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
                    _Code = value;
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
                    _Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

        public string ParentCode
        {
            get { return _ParentCode; }
            set
            {
                if (_ParentCode != value)
                {
                    _ParentCode = value;
                    OnPropertyChanged(() => ParentCode);
                }
            }
        }

        public string DefinitionCode
        {
            get { return _DefinitionCode; }
            set
            {
                if (_DefinitionCode != value)
                {
                    _DefinitionCode = value;
                    OnPropertyChanged(() => DefinitionCode);
                }
            }
        }


        public string Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    OnPropertyChanged(() => Year);
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


        public string ReviewCode
        {
            get { return _ReviewCode; }
            set
            {
                if (_ReviewCode != value)
                {
                    _ReviewCode = value;
                    OnPropertyChanged(() => ReviewCode);
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

        class TeamValidator : AbstractValidator<Team>
        {
            public TeamValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
                RuleFor(obj => obj.DefinitionCode).NotEmpty().WithMessage("Definition is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new TeamValidator();
        }
    }
}
