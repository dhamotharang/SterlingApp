using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.Core.Entities
{
    public class FinancialType : ObjectBase
    {
        int _FinancialTypeId;
        string _Code;
        string _Name;
        int? _ParentId;
        bool _Active;

        public int FinancialTypeId
        {
            get { return _FinancialTypeId; }
            set
            {
                if (_FinancialTypeId != value)
                {
                    _FinancialTypeId = value;
                    OnPropertyChanged(() => FinancialTypeId);
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


        class FinancialTypeValidator : AbstractValidator<FinancialType>
        {
            public FinancialTypeValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code must not be empty.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new FinancialTypeValidator();
        }
    }
}
