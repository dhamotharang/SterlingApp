using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class CostCentreDefinition : ObjectBase
    {
        int _CCDefinitionId;
        string _Code;
        string _Name;
        int _Position ;
        //string _Parent;
        string _Year;
        bool _Active;


        public int CCDefinitionId
        {
            get { return _CCDefinitionId; }
            set
            {
                if (_CCDefinitionId != value)
                {
                    _CCDefinitionId = value;
                    OnPropertyChanged(() => CCDefinitionId);
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


        public int Position 
        {
            get { return _Position ; }
            set
            {
                if (_Position  != value)
                {
                    _Position  = value;
                    OnPropertyChanged(() => Position );
                }
            }
        }

        //public string Parent
        //{
        //    get { return _Parent; }
        //    set
        //    {
        //        if (_Parent != value)
        //        {
        //            _Parent = value;
        //            OnPropertyChanged(() => Parent);
        //        }
        //    }
        //}

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


        
        class CostCentreDefinitionValidator : AbstractValidator<CostCentreDefinition>
        {
            public CostCentreDefinitionValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new CostCentreDefinitionValidator();
        }
    }
}
