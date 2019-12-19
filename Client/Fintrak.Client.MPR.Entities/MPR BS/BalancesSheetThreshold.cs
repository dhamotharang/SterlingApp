using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class BalanceSheetThreshold : ObjectBase
    {
        int _BalanceSheetThresholdId;
        string _CaptionCode;
        string _ProductCode;
        double _Rate;
        string _CompanyCode;
        bool _Active;

        public int BalanceSheetThresholdId
        {
            get { return _BalanceSheetThresholdId; }
            set
            {
                if (_BalanceSheetThresholdId != value)
                {
                    _BalanceSheetThresholdId = value;
                    OnPropertyChanged(() => BalanceSheetThresholdId);
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

        public double Rate
        {
            get { return _Rate; }
            set
            {
                if (_Rate != value)
                {
                    _Rate = value;
                    OnPropertyChanged(() => Rate);
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

        
        class BalanceSheetThresholdValidator : AbstractValidator<BalanceSheetThreshold>
        {
            public BalanceSheetThresholdValidator()
            {
                RuleFor(obj => obj.CaptionCode).NotEmpty().WithMessage("Caption is required.");
                RuleFor(obj => obj.ProductCode).NotEmpty().WithMessage("Product Code is required.");
                RuleFor(obj => obj.Rate).GreaterThan(0).WithMessage("Rate is required.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new BalanceSheetThresholdValidator();
        }
    }
}
