using System;
using System.Linq;
using Fintrak.Client.Budget.Entities;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class FeeGroupEntry : TransactionObjectDecimalBase
    {
        int _FeeGroupEntryId;
        string _DefintionCode;
        string _MisCode;
        string _GroupCode;
        string _CurrencyCode;
        string _Year;
        string _ReviewCode; 
        bool _Active;

        public int FeeGroupEntryId
        {
            get { return _FeeGroupEntryId; }
            set
            {
                if (_FeeGroupEntryId != value)
                {
                    _FeeGroupEntryId = value;
                    OnPropertyChanged(() => FeeGroupEntryId);
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

        class FeeGroupEntryValidator : AbstractValidator<FeeGroupEntry>
        {
            public FeeGroupEntryValidator()
            {
                RuleFor(obj => obj.DefintionCode).NotEmpty().WithMessage("DefintionCode is required.");
                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("MisCode is required.");            
            }
        }

        protected override IValidator GetValidator()
        {
            return new FeeGroupEntryValidator();
        }
    }
}
