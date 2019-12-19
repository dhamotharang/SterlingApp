using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class IFRSProduct : ObjectBase
    {
        int _ProductId;
        string _ProductCode;
        string _ScheduleTypeCode;
        double _MarketRate;
        double _PastDueRate;
        string _CompanyCode;
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

        public string ScheduleTypeCode
        {
            get { return _ScheduleTypeCode; }
            set
            {
                if (_ScheduleTypeCode != value)
                {
                    _ScheduleTypeCode = value;
                    OnPropertyChanged(() => ScheduleTypeCode);
                }
            }
        }

        public double MarketRate
        {
            get { return _MarketRate; }
            set
            {
                if (_MarketRate != value)
                {
                    _MarketRate = value;
                    OnPropertyChanged(() => MarketRate);
                }
            }
        }

        public double PastDueRate
        {
            get { return _PastDueRate; }
            set
            {
                if (_PastDueRate != value)
                {
                    _PastDueRate = value;
                    OnPropertyChanged(() => PastDueRate);
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


        class IFRSProductValidator : AbstractValidator<IFRSProduct>
        {
            public IFRSProductValidator()
            {
                RuleFor(obj => obj.ProductCode).NotEmpty().WithMessage("ProductCode is required.");
                RuleFor(obj => obj.ScheduleTypeCode).NotEmpty().WithMessage("ScheduleTypeCode is required.");
                RuleFor(obj => obj.ProductCode).NotEmpty().WithMessage("ProductCode is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IFRSProductValidator();
        }
    }
}
