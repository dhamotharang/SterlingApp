using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class FeeItem : ObjectBase
    {
        int _FeeItemId;
        string _Code;
        string _Name;
        string _GroupCode;
        string _CaptionCode;
        string _CategoryCode;
        string _CalculationType;
        string _Movement;
        FeeUnitEnum _Unit;
        int _Position;
        bool _Budgetable;
        string _Year;
        string _ReviewCode; 
        bool _Active;

        public int FeeItemId
        {
            get { return _FeeItemId; }
            set
            {
                if (_FeeItemId != value)
                {
                    _FeeItemId = value;
                    OnPropertyChanged(() => FeeItemId);
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

        public string GroupCode
        {
            get { return _GroupCode; }
            set
            {
                if (_GroupCode != value)
                {
                    _GroupCode = value;
                    OnPropertyChanged(() => GroupCode);
                }
            }
        }

        public string CaptionCode
        {
            get { return _CaptionCode; }
            set
            {
                if (_CaptionCode != value)
                {
                    _CaptionCode = value;
                    OnPropertyChanged(() => CaptionCode);
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

        public string CalculationType
        {
            get { return _CalculationType; }
            set
            {
                if (_CalculationType != value)
                {
                    _CalculationType = value;
                    OnPropertyChanged(() => CalculationType);
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




        public string Movement
        {
            get { return _Movement; }
            set
            {
                if (_Movement != value)
                {
                    _Movement = value;
                    OnPropertyChanged(() => Movement);
                }
            }
        }

        public FeeUnitEnum Unit
        {
            get { return _Unit; }
            set
            {
                if (_Unit != value)
                {
                    _Unit = value;
                    OnPropertyChanged(() => Unit);
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

        class FeeItemValidator : AbstractValidator<FeeItem>
        {
            public FeeItemValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
                RuleFor(obj => obj.CaptionCode).NotEmpty().WithMessage("Definition is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new FeeItemValidator();
        }
    }
}
