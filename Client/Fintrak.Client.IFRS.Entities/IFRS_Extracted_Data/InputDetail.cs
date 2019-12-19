using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class InputDetail : ObjectBase
    {
        int _InputDetailId;
        string _RefNo;
        string _OldRating;
        string _Rating;
        Nullable<double> _OldCollateralValue;
        Nullable<double> _CollateralValue;
        int? _OldStage;
        int? _Stage;
        string _OldCollateralType;
        string _CollateralType;
        string _CustomerName;
        bool _Active;


        public int InputDetailId

        {
            get { return _InputDetailId; }
            set
            {
                if (_InputDetailId != value)
                {
                    _InputDetailId = value;
                    OnPropertyChanged(() => InputDetailId);
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
        public string OldRating
        {
            get { return _OldRating; }
            set
            {
                if (_OldRating != value)
                {
                    _OldRating = value;
                    OnPropertyChanged(() => OldRating);
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

        public Nullable<double> OldCollateralValue
        {
            get { return _OldCollateralValue; }
            set
            {
                if (_OldCollateralValue != value)
                {
                    _OldCollateralValue = value;
                    OnPropertyChanged(() => OldCollateralValue);
                }
            }
        }

        public Nullable<double> CollateralValue
        {
            get { return _CollateralValue; }
            set
            {
                if (_CollateralValue != value)
                {
                    _CollateralValue = value;
                    OnPropertyChanged(() => CollateralValue);
                }
            }
        }

       
        public int? OldStage
        {
            get { return _OldStage; }
            set
            {
                if (_OldStage != value)
                {
                    _OldStage = value;
                    OnPropertyChanged(() => OldStage);
                }
            }
        }

       
        public int? Stage
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

        public string CollateralType
        {
            get { return _CollateralType; }
            set
            {
                if (_CollateralType != value)
                {
                    _CollateralType = value;
                    OnPropertyChanged(() => CollateralType);
                }
            }
        }

        public string OldCollateralType
        {
            get { return _OldCollateralType; }
            set
            {
                if (_OldCollateralType != value)
                {
                    _OldCollateralType = value;
                    OnPropertyChanged(() => OldCollateralType);
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
        class InputDetailValidator : AbstractValidator<InputDetail>
        {
            public InputDetailValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new InputDetailValidator();
        }
    }
}
