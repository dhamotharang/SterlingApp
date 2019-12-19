using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class NonProductRate : ObjectBase
    {
        int _NonProductRateId;
        string _ProductCode;
        string _Period;
        string _Year;
        double _Rate;
        string _CompanyCode;
        bool _Active;

        public int NonProductRateId
        {
            get { return _NonProductRateId; }
            set
            {
                if (_NonProductRateId != value)
                {
                    _NonProductRateId = value;
                    OnPropertyChanged(() => NonProductRateId);
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

        
        class NonProductRateValidator : AbstractValidator<NonProductRate>
        {
            public NonProductRateValidator()
            {
                RuleFor(obj => obj.ProductCode).NotEmpty().WithMessage("Product Code is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new NonProductRateValidator();
        }
    }
}
