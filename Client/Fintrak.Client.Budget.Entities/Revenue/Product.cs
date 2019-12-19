using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class Product : ObjectBase
    {
        int _ProductId;
        string _Code;
        string _Name;
        string _CurrencyCode;
        string _CategoryCode;
        string _GroupCode;
        string _CaptionCode;
        string _Year;
        string _ProductClass;
        string _ClassificationCode;
        string _OtherCode;
        int _Position;
        bool _Budgetable;
        bool _VolumeBase;
        bool _Visibility;
        string _ReviewCode; 
        bool _Active;

        public int ProductId
        {
            get { return _ProductId; }
            set
            {
                if (_ProductId != value)
                {
                    _ProductId = value;
                    OnPropertyChanged(() => ProductId);
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

        public string CurrencyCode
        {
            get { return _CurrencyCode; }
            set
            {
                if (_CurrencyCode != value)
                {
                    _CurrencyCode = value;
                    OnPropertyChanged(() => CurrencyCode);
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

        public string ProductClass
        {
            get { return _ProductClass; }
            set
            {
                if (_ProductClass != value)
                {
                    _ProductClass = value;
                    OnPropertyChanged(() => ProductClass);
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

        public string OtherCode
        {
            get { return _OtherCode; }
            set
            {
                if (_OtherCode != value)
                {
                    _OtherCode = value;
                    OnPropertyChanged(() => OtherCode);
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

        public bool VolumeBase
        {
            get { return _VolumeBase; }
            set
            {
                if (_VolumeBase != value)
                {
                    _VolumeBase = value;
                    OnPropertyChanged(() => VolumeBase);
                }
            }
        }

        public bool Visibility
        {
            get { return _Visibility; }
            set
            {
                if (_Visibility != value)
                {
                    _Visibility = value;
                    OnPropertyChanged(() => Visibility);
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

        class ProductValidator : AbstractValidator<Product>
        {
            public ProductValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
                RuleFor(obj => obj.CategoryCode).NotEmpty().WithMessage("Definition is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProductValidator();
        }
    }
}
