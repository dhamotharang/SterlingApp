using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class Ratios : ObjectBase
    {
        int _RatiosId;
        string _MainCaption;
        string _Numerator;
        string _Denominator;
        bool _ProRatio;
        bool _Bsin;

        bool _Active;

        public int RatiosId
        {
            get { return _RatiosId; }
            set
            {
                if (_RatiosId != value)
                {
                    _RatiosId = value;
                    OnPropertyChanged(() => RatiosId);
                }
            }
        }

        public string MainCaption
        {
            get { return _MainCaption; }
            set
            {
                if (_MainCaption != value)
                {
                    _MainCaption = value;
                    OnPropertyChanged(() => MainCaption);
                }
            }
        }


        public string Numerator
        {
            get { return _Numerator; }
            set
            {
                if (_Numerator != value)
                {
                    _Numerator = value;
                    OnPropertyChanged(() => Numerator);
                }
            }
        }


        public string Denominator
        {
            get { return _Denominator; }
            set
            {
                if (_Denominator != value)
                {
                    _Denominator = value;
                    OnPropertyChanged(() => Denominator);
                }
            }
        }

        public bool ProRatio
        {
            get { return _ProRatio; }
            set
            {
                if (_ProRatio != value)
                {
                    _ProRatio = value;
                    OnPropertyChanged(() => ProRatio);
                }
            }
        }


        public bool Bsin
        {
            get { return _Bsin; }
            set
            {
                if (_Bsin != value)
                {
                    _Bsin = value;
                    OnPropertyChanged(() => Bsin);
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


        class RatiosValidator : AbstractValidator<Ratios>
        {
            public RatiosValidator()
            {
                RuleFor(obj => obj.MainCaption).NotEmpty().WithMessage("Main Caption is required.");
                RuleFor(obj => obj.Numerator).NotEmpty().WithMessage("Numerator is required.");
                RuleFor(obj => obj.Denominator).NotEmpty().WithMessage("Denominator is required.");



            }
        }

        protected override IValidator GetValidator()
        {
            return new RatiosValidator();
        }
    }
}
