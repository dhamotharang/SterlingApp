using System;
using System.Linq;
using Fintrak.Client.Budget.Entities;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class RevenuePool : TransactionObjectDecimalBase
    {
        int _RevenuePoolId;
        string _DefintionCode;
        string _MisCode;
        string _ProductCode;
        string _CustomerName;
        string _AccountNo;
        string _CurrencyCode;
        string _Year;
        string _ReviewCode; 
        bool _Active;

        public int RevenuePoolId
        {
            get { return _RevenuePoolId; }
            set
            {
                if (_RevenuePoolId != value)
                {
                    _RevenuePoolId = value;
                    OnPropertyChanged(() => RevenuePoolId);
                }
            }
        }

        public string DefintionCode
        {
            get { return _DefintionCode; }
            set
            {
                if (_DefintionCode != value)
                {
                    _DefintionCode = value;
                    OnPropertyChanged(() => DefintionCode);
                }
            }
        }

        public string MisCode
        {
            get { return _MisCode; }
            set
            {
                if (_MisCode != value)
                {
                    _MisCode = value;
                    OnPropertyChanged(() => MisCode);
                }
            }
        }

        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                if (_ProductCode != value)
                {
                    _ProductCode = value;
                    OnPropertyChanged(() => ProductCode);
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

        public string AccountNo
        {
            get { return _AccountNo; }
            set
            {
                if (_AccountNo != value)
                {
                    _AccountNo = value;
                    OnPropertyChanged(() => AccountNo);
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

        class RevenuePoolValidator : AbstractValidator<RevenuePool>
        {
            public RevenuePoolValidator()
            {
                RuleFor(obj => obj.DefintionCode).NotEmpty().WithMessage("DefintionCode is required.");
                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("MisCode is required.");
                RuleFor(obj => obj.CustomerName).NotEmpty().WithMessage("Definition is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new RevenuePoolValidator();
        }
    }
}
