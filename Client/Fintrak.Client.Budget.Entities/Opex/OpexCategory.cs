using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class OpexCategory : ObjectBase
    {
        int _OpexCategoryId;
        string _Code;
        string _Name;
        string _ParentCode;
        int _Position;
        string _Description;
        string _ReviewCode;  
        string _Year;
        bool _Active;

        public int OpexCategoryId
        {
            get { return _OpexCategoryId; }
            set
            {
                if (_OpexCategoryId != value)
                {
                    _OpexCategoryId = value;
                    OnPropertyChanged(() => OpexCategoryId);
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

        
        class OpexCategoryValidator : AbstractValidator<OpexCategory>
        {
            public OpexCategoryValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new OpexCategoryValidator();
        }
    }
}
