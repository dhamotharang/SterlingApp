using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsLoanClassification : ObjectBase
    {
        int _ID;
        string _ProductType;
        string _SubType;
        bool _Active;

        public int ID
        {
            get { return ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }

        public string ProductType
        {
            get { return _ProductType; }
            set
            {
                if (_ProductType != value)
                {
                    _ProductType = value;
                    OnPropertyChanged(() => ProductType);
                }
            }
        }


        public string SubType
        {
            get { return _SubType; }
            set
            {
                if (_SubType != value)
                {
                    _SubType = value;
                    OnPropertyChanged(() => SubType);
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


        class IfrsLoanClassificationValidator : AbstractValidator<IfrsLoanClassification>
        {
            public IfrsLoanClassificationValidator()
            {
                RuleFor(obj => obj.ProductType).NotEmpty().WithMessage("Product Type is required.");
                RuleFor(obj => obj.SubType).NotEmpty().WithMessage("SubType is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsLoanClassificationValidator();
        }
    }
}
