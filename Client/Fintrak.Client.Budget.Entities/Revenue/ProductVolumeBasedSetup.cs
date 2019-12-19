using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class ProductVolumeBasedSetup : ObjectBase
    {
        int _ProductVolumeBasedSetupId;
        string _MakeUpCode;       
        string _Year;
        string _ProductCode;     
        string _ReviewCode; 
        bool _Active;

        public int ProductVolumeBasedSetupId
        {
            get { return _ProductVolumeBasedSetupId; }
            set
            {
                if (_ProductVolumeBasedSetupId != value)
                {
                    _ProductVolumeBasedSetupId = value;
                    OnPropertyChanged(() => ProductVolumeBasedSetupId);
                }
            }
        }

        public string MakeUpCode
        {
            get { return _MakeUpCode; }
            set
            {
                if (_MakeUpCode != value)
                {
                    _MakeUpCode = value;
                    OnPropertyChanged(() => MakeUpCode);
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


        public string ReviewCode
        {
            get { return _ReviewCode; }
            set
            {
                if (_ReviewCode != value)
                {
                    _ReviewCode = value;
                    OnPropertyChanged(() => ReviewCode);
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

        class ProductVolumeBasedSetupValidator : AbstractValidator<ProductVolumeBasedSetup>
        {
            public ProductVolumeBasedSetupValidator()
            {
                RuleFor(obj => obj.ProductCode).NotEmpty().WithMessage("Product is required.");
                RuleFor(obj => obj.MakeUpCode).NotEmpty().WithMessage("MakeUpCode is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProductVolumeBasedSetupValidator();
        }
    }
}
