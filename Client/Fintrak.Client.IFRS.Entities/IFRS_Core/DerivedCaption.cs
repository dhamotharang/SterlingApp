using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class DerivedCaption : ObjectBase
    {
        int _DerivedCaptionId;
        string _Caption;
        string _DependencyCaption;
        double _Factor;
        string _CompanyCode;
        bool _Active;

        public int DerivedCaptionId
        {
            get { return _DerivedCaptionId; }
            set
            {
                if (_DerivedCaptionId != value)
                {
                    _DerivedCaptionId = value;
                    OnPropertyChanged(() => DerivedCaptionId);
                }
            }
        }

        public string Caption
        {
            get { return _Caption; }
            set
            {
                if (_Caption != value)
                {
                    _Caption = value;
                    OnPropertyChanged(() => Caption);
                }
            }
        }

        public string DependencyCaption
        {
            get { return _DependencyCaption; }
            set
            {
                if (_DependencyCaption != value)
                {
                    _DependencyCaption  = value;
                    OnPropertyChanged(() => DependencyCaption);
                }
            }
        }

        public double Factor
        {
            get { return _Factor; }
            set
            {
                if (_Factor != value)
                {
                    _Factor = value;
                    OnPropertyChanged(() => Factor);
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

        
        class DerivedCaptionValidator : AbstractValidator<DerivedCaption>
        {
            public DerivedCaptionValidator()
            {
                RuleFor(obj => obj.Caption).NotEmpty().WithMessage("Caption is required.");
                RuleFor(obj => obj.DependencyCaption).NotEmpty().WithMessage("Dependency Caption is required.");
                
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new DerivedCaptionValidator();
        }
    }
}
