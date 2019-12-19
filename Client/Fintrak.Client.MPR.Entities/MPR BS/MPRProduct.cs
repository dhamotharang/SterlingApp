using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class MPRProduct : ObjectBase
    {
        int _ProductId;
        string _ProductCode;
        string _CaptionCode;
        string _VolumeGL;
        string _InterestGL;
        bool _Budgetable;
        bool _IsNotional;
        double _Rate;
        string _CompanyCode;
        bool _Active;
        ModuleOwnerType _ModuleOwnerType;
        public int ProductId
        {
            get { return _ProductId; }
            set
            {
                if (_ProductId != value)
                {
                    _ProductId = value;
                    OnPropertyChanged(() => ProductId);
                }
            }
        }

        public ModuleOwnerType ModuleOwnerType
        {
            get { return _ModuleOwnerType; }
            set
            {
                if (_ModuleOwnerType != value)
                {
                    _ModuleOwnerType = value;
                    OnPropertyChanged(() => ModuleOwnerType);
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


        public string VolumeGL
        {
            get { return _VolumeGL; }
            set
            {
                if (_VolumeGL != value)
                {
                    _VolumeGL = value;
                    OnPropertyChanged(() => VolumeGL);
                }
            }
        }

        public string InterestGL
        {
            get { return _InterestGL; }
            set
            {
                if (_InterestGL != value)
                {
                    _InterestGL = value;
                    OnPropertyChanged(() => InterestGL);
                }
            }
        }

        public bool Budgetable
        {
            get { return _Budgetable; }
            set
            {
                if (_Budgetable != value)
                {
                    _Budgetable = value;
                    OnPropertyChanged(() => Budgetable);
                }
            }
        }


        public bool IsNotional
        {
            get { return _IsNotional; }
            set
            {
                if (_IsNotional != value)
                {
                    _IsNotional = value;
                    OnPropertyChanged(() => IsNotional);
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

        
        class MPRProductValidator : AbstractValidator<MPRProduct>
        {
            public MPRProductValidator()
            {
                RuleFor(obj => obj.CaptionCode).NotEmpty().WithMessage("Caption is required.");
                RuleFor(obj => obj.ProductCode).NotEmpty().WithMessage("Product Code is required.");            
            }
        }

        protected override IValidator GetValidator()
        {
            return new MPRProductValidator();
        }
    }
}
