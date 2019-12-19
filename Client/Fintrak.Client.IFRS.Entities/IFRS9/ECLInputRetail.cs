using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class ECLInputRetail : ObjectBase
    {
        int _EclInputRetailId;
        string _account_number;
        int _Stage;
        double _EIR;
        double _SeriesValue;
        double _Amount;
        bool _Active;

        public int EclInputRetailId
        {
            get { return _EclInputRetailId; }
            set
            {
                if (_EclInputRetailId != value)
                {
                    _EclInputRetailId = value;
                    OnPropertyChanged(() => EclInputRetailId);
                }
            }
        }

        public string account_number
        {
            get { return _account_number; }
            set
            {
                if (_account_number != value)
                {
                    _account_number = value;
                    OnPropertyChanged(() => account_number);
                }
            }
        }

        public double SeriesValue
        {
            get { return _SeriesValue; }
            set
            {
                if (_SeriesValue != value)
                {
                    _SeriesValue = value;
                    OnPropertyChanged(() => SeriesValue);
                }
            }
        }
        public double Amount
        {
            get { return _Amount; }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    OnPropertyChanged(() => Amount);
                }
            }
        }

        public int Stage
        {
            get { return _Stage; }
            set
            {
                if (_Stage != value)
                {
                    _Stage = value;
                    OnPropertyChanged(() => Stage);
                }
            }
        }
        public double EIR
        {
            get { return _EIR; }
            set
            {
                if (_EIR != value)
                {
                    _EIR = value;
                    OnPropertyChanged(() => EIR);
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


        class ECLInputRetailValidator : AbstractValidator<ECLInputRetail>
        {
            public ECLInputRetailValidator()
            {
                RuleFor(obj => obj.account_number).NotEmpty().WithMessage("account_number is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new ECLInputRetailValidator();
        }
    }
}
