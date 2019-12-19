using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class ExpenseBasis : ObjectBase
    {
        int _ExpenseBasisId;
        string _Code;
        string _Name;
        OpexCategory _Category;
        BasisValueType _ValueType;
        BasisItemType _ItemType;
        string _TeamDefinitionCode;
        bool _Active;


        public int ExpenseBasisId
        {
            get { return _ExpenseBasisId; }
            set
            {
                if (_ExpenseBasisId != value)
                {
                    _ExpenseBasisId = value;
                    OnPropertyChanged(() => ExpenseBasisId);
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

        public OpexCategory Category
        {
            get { return _Category; }
            set
            {
                if (_Category != value)
                {
                    _Category = value;
                    OnPropertyChanged(() => Category);
                }
            }
        }

        public BasisValueType ValueType 
        {
            get { return _ValueType; }
            set
            {
                if (_ValueType != value)
                {
                    _ValueType = value;
                    OnPropertyChanged(() => ValueType);
                }
            }
        }

        public BasisItemType ItemType
        {
            get { return _ItemType; }
            set
            {
                if (_ItemType != value)
                {
                    _ItemType = value;
                    OnPropertyChanged(() => ItemType);
                }
            }
        }

        public string TeamDefinitionCode
        {
            get { return _TeamDefinitionCode; }
            set
            {
                if (_TeamDefinitionCode != value)
                {
                    _TeamDefinitionCode = value;
                    OnPropertyChanged(() => TeamDefinitionCode);
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

        public string CategoryName
        {
            get { return _Category.ToString(); }
        }

        public string ValueTypeName
        {
            get { return _ValueType.ToString(); }
        }

        public string ItemTypeName
        {
            get { return _ItemType.ToString(); }
        }
        
        class ExpenseBasisValidator : AbstractValidator<ExpenseBasis>
        {
            public ExpenseBasisValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new ExpenseBasisValidator();
        }
    }
}
