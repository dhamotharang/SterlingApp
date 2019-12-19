using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class CostCentre : ObjectBase
    {
        int _CostCentreId;
        string _Code;
        string _Name;
        string _DefinitionCode ;
        string _Parent;
        string _Year;
        bool _Active;


        public int CostCentreId
        {
            get { return _CostCentreId; }
            set
            {
                if (_CostCentreId != value)
                {
                    _CostCentreId = value;
                    OnPropertyChanged(() => CostCentreId);
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


        public string DefinitionCode 
        {
            get { return _DefinitionCode ; }
            set
            {
                if (_DefinitionCode  != value)
                {
                    _DefinitionCode  = value;
                    OnPropertyChanged(() => DefinitionCode );
                }
            }
        }

        public string Parent
        {
            get { return _Parent; }
            set
            {
                if (_Parent != value)
                {
                    _Parent = value;
                    OnPropertyChanged(() => Parent);
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


        
        class CostCentreValidator : AbstractValidator<CostCentre>
        {
            public CostCentreValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new CostCentreValidator();
        }
    }
}
