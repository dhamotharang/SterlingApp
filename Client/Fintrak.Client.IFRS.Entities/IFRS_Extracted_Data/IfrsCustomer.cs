using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsCustomer : ObjectBase
    {
        int _CustomerId;
        string _CustomerNo;
        string _CustomerName;
        string _CustType;
        string _CreditRating;
        string _Country;
        string _CustCategory;
        string _LGD_Type;
        string _CompanyCode;
        bool _Active;


        public int CustomerId

        {
            get { return _CustomerId; }
            set
            {
                if (_CustomerId != value)
                {
                    _CustomerId = value;
                    OnPropertyChanged(() => CustomerId);
                }
            }
        }
        public string CustomerNo
        {
            get { return _CustomerNo; }
            set
            {
                if (_CustomerNo != value)
                {
                    _CustomerNo = value;
                    OnPropertyChanged(() => CustomerNo);
                }
            }
        }
        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    OnPropertyChanged(() => CustomerName);
                }
            }
        }
        public string CustType
        {
            get { return _CustType; }
            set
            {
                if (_CustType != value)
                {
                    _CustType = value;
                    OnPropertyChanged(() => CustType);
                }
            }
        }

        public string CreditRating
        {
            get { return _CreditRating; }
            set
            {
                if (_CreditRating != value)
                {
                    _CreditRating = value;
                    OnPropertyChanged(() => CreditRating);
                }
            }
        }


        public string Country
        {
            get { return _Country; }
            set
            {
                if (_Country != value)
                {
                    _Country = value;
                    OnPropertyChanged(() => Country);
                }
            }
        }

        public string LGD_Type
        {
            get { return _LGD_Type; }
            set
            {
                if (_LGD_Type != value)
                {
                    _LGD_Type = value;
                    OnPropertyChanged(() => LGD_Type);
                }
            }
        }

        public string CustCategory
        {
            get { return _CustCategory; }
            set
            {
                if (_CustCategory != value)
                {
                    _CustCategory = value;
                    OnPropertyChanged(() => CustCategory);
                }
            }
        }


       
        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value;
                    OnPropertyChanged(() => CompanyCode);
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

        class IfrsCustomerValidator : AbstractValidator<IfrsCustomer>
        {
            public IfrsCustomerValidator()
            {
                RuleFor(obj => obj.CustomerNo).NotEmpty().WithMessage("CustomerNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsCustomerValidator();
        }
    }
}
