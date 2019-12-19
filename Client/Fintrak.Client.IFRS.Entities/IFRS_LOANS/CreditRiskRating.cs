using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class CreditRiskRating : ObjectBase
    {
        int _CreditRiskRatingId;
        string _Code;
        double _EP;
        double _LGD;
        double _PD;
        string _Description;
        string _CompanyCode;
        bool _Active;

        public int CreditRiskRatingId
        {
            get { return _CreditRiskRatingId; }
            set
            {
                if (_CreditRiskRatingId != value)
                {
                    _CreditRiskRatingId = value;
                    OnPropertyChanged(() => CreditRiskRatingId);
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

        public double EP
        {
            get { return _EP; }
            set
            {
                if (_EP != value)
                {
                    _EP = value;
                    OnPropertyChanged(() => EP);
                }
            }
        }

        public double LGD
        {
            get { return _LGD; }
            set
            {
                if (_LGD != value)
                {
                    _LGD = value;
                    OnPropertyChanged(() => LGD);
                }
            }
        }

        public double PD
        {
            get { return _PD; }
            set
            {
                if (_PD != value)
                {
                    _PD = value;
                    OnPropertyChanged(() => PD);
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


        class CreditRiskRatingValidator : AbstractValidator<CreditRiskRating>
        {
            public CreditRiskRatingValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.EP).GreaterThan(0).WithMessage("EP is required.");
                RuleFor(obj => obj.PD).GreaterThan(0).WithMessage("PD is required.");
                RuleFor(obj => obj.LGD).GreaterThan(0).WithMessage("LGD is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new CreditRiskRatingValidator();
        }
    }
}
