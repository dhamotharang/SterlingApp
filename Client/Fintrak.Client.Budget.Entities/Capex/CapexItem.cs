using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class CapexItem : ObjectBase
    {
        int _CapexItemId;
        string _Code;
        string _Name;
        string _CategoryCode;
        decimal _Cost;
        int _Position;
        bool _Budgetable;
        string _ReviewCode; 
        string _Year;
        bool _Active;

        public int CapexItemId
        {
            get { return _CapexItemId; }
            set
            {
                if (_CapexItemId != value)
                {
                    _CapexItemId = value;
                    OnPropertyChanged(() => CapexItemId);
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

        public string CategoryCode
        {
            get { return _CategoryCode; }
            set
            {
                if (_CategoryCode != value)
                {
                    _CategoryCode = value;
                    OnPropertyChanged(() => CategoryCode);
                }
            }
        }


        public decimal Cost
        {
            get { return _Cost; }
            set
            {
                if (_Cost != value)
                {
                    _Cost = value;
                    OnPropertyChanged(() => Cost);
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

        public bool Budgetable
        {
            get { return _Budgetable; }
            set
            {
                if (_Budgetable != value)
                {
                    _Budgetable = value;
                    OnPropertyChanged(() => Budgetable);
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

        public string LongDescription
        {
            get
            {
                return string.Format("{0} - {1}", _Code, _Name);
            }
        }

        
        class CapexItemValidator : AbstractValidator<CapexItem>
        {
            public CapexItemValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new CapexItemValidator();
        }
    }
}
