using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsCustomerPD : ObjectBase
    {
        int _CustomerPDId;
        string _CustomerId;
        double? _PD;
        string _Rating;
        string _SP;
        DateTime? _Rundate;
        bool _Active;

        public int CustomerPDId
        {
            get { return _CustomerPDId; }
            set
            {
                if (_CustomerPDId != value)
                {
                    _CustomerPDId = value;
                    OnPropertyChanged(() => CustomerPDId);
                }
            }
        }

        public string CustomerId
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

        public double? PD
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

        public string Rating
        {
            get { return _Rating; }
            set
            {
                if (_Rating != value)
                {
                    _Rating = value;
                    OnPropertyChanged(() => Rating);
                }
            }
        }

        public string SP
        {
            get { return _SP; }
            set
            {
                if (_SP != value)
                {
                    _SP = value;
                    OnPropertyChanged(() => SP);
                }
            }
        }

        public DateTime? Rundate
        {
            get { return _Rundate; }
            set
            {
                if (_Rundate != value)
                {
                    _Rundate = value;
                    OnPropertyChanged(() => Rundate);
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

        class IfrsCustomerPDValidator : AbstractValidator<IfrsCustomerPD>
        {
            public IfrsCustomerPDValidator()
            {
                RuleFor(obj => obj.Rating).NotEmpty().WithMessage("Rating is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsCustomerPDValidator();
        }
    }
}
