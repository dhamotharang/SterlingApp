using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class TeamDefinition : ObjectBase
    {
        int _TeamDefinitionId;
        string _Code;
        string _Name;
        int _Position;
        bool _CanClassified;
        string _Year;
        bool _IsOfficer;
        string _ReviewCode; 
        bool _Active;

        public int TeamDefinitionId
        {
            get { return _TeamDefinitionId; }
            set
            {
                if (_TeamDefinitionId != value)
                {
                    _TeamDefinitionId = value;
                    OnPropertyChanged(() => TeamDefinitionId);
                }
            }
        }


        public int Position
        {
            get { return _Position; }
            set
            {
                if (_Position != value)
                {
                    _Position = value;
                    OnPropertyChanged(() => Position);
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


       

        public bool CanClassified
        {
            get { return _CanClassified; }
            set
            {
                if (_CanClassified != value)
                {
                    _CanClassified = value;
                    OnPropertyChanged(() => CanClassified);
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

        public bool IsOfficer
        {
            get { return _IsOfficer; }
            set
            {
                if (_IsOfficer != value)
                {
                    _IsOfficer = value;
                    OnPropertyChanged(() => IsOfficer);
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

        
        class TeamDefinitionValidator : AbstractValidator<TeamDefinition>
        {
            public TeamDefinitionValidator()
            {
                RuleFor(obj => obj.Position).GreaterThan(0).WithMessage("Position is required.");
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("FiscalYear is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new TeamDefinitionValidator();
        }
    }
}
