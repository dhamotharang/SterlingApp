using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IntegralFee : ObjectBase
    {
        int _IntegralFeeId;
        string _RefNo;
        string _AccountNo;
        string _Description;
        DateTime _Date;
        double _FeeAmount;
        string _CompanyCode;
        bool _Active;


        public int IntegralFeeId

        {
            get { return _IntegralFeeId; }
            set
            {
                if (_IntegralFeeId != value)
                {
                    _IntegralFeeId = value;
                    OnPropertyChanged(() => IntegralFeeId);
                }
            }
        }
        public string RefNo
        {
            get { return _RefNo; }
            set
            {
                if (_RefNo != value)
                {
                    _RefNo = value;
                    OnPropertyChanged(() => RefNo);
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
       
        public DateTime Date
        {
            get { return _Date; }
            set
            {
                if (_Date != value)
                {
                    _Date = value;
                    OnPropertyChanged(() => Date);
                }
            }
        }


        public double FeeAmount
        {
            get { return _FeeAmount; }
            set
            {
                if (_FeeAmount != value)
                {
                    _FeeAmount = value;
                    OnPropertyChanged(() => FeeAmount);
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
        class IntegralFeeValidator : AbstractValidator<IntegralFee>
        {
            public IntegralFeeValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IntegralFeeValidator();
        }
    }
}
