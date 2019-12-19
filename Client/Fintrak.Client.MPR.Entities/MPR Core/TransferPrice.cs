using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;


namespace Fintrak.Client.MPR.Entities
{
    public class TransferPrice : ObjectBase
    {
        int _TransferPriceId;
        string _ProductCode;
        string _CaptionCode;
        double _Rate;
        string _DefinitionCode;
        string _MisCode;
        string _Year;
        string _Period;
        int _SolutionId;
        string _CompanyCode;
        bool _Active;

        public int TransferPriceId
        {
            get { return _TransferPriceId; }
            set
            {
                if (_TransferPriceId != value)
                {
                    _TransferPriceId = value;
                    OnPropertyChanged(() => TransferPriceId);
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

        public string DefinitionCode
        {
            get { return _DefinitionCode; }
            set
            {
                if (_DefinitionCode != value)
                {
                    _DefinitionCode = value;
                    OnPropertyChanged(() => DefinitionCode);
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

        public string Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                    OnPropertyChanged(() => Period);
                }
            }
        }

        public int SolutionId
        {
            get { return _SolutionId; }
            set
            {
                if (_SolutionId != value)
                {
                    _SolutionId = value;
                    OnPropertyChanged(() => SolutionId);
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

        
        class TransferPriceValidator : AbstractValidator<TransferPrice>
        {
            public TransferPriceValidator()
            {
                RuleFor(obj => obj.ProductCode).NotEmpty().WithMessage("Product Code is required.");
                RuleFor(obj => obj.CaptionCode).NotEmpty().WithMessage("Caption is required.");
                RuleFor(obj => obj.Rate).GreaterThan(0).WithMessage("Rate is required.");
                RuleFor(obj => obj.DefinitionCode).NotEmpty().WithMessage("TeamDefinition is required.");
                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("Team is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("FiscalYear is required.");
                RuleFor(obj => obj.Period).NotEmpty().WithMessage("FiscalPeriod is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new TransferPriceValidator();
        }
    }
}
