using System;
using System.Linq;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class PLCaptionNew : ObjectBase
    {
        int _PLCaptionId;
        string _CaptionCode;
        string _CaptionName;
        AccountTypeEnum _AccountType;
        int _Position;
        ModuleOwnerType _ModuleOwnerType;
        string _ParentCode;
        string _Color;
        string _CompanyCode;
        bool _FlagOpex;
        bool _Active;

        public int PLCaptionId
        {
            get { return _PLCaptionId; }
            set
            {
                if (_PLCaptionId != value)
                {
                    _PLCaptionId = value;
                    OnPropertyChanged(() => PLCaptionId);
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

        public string CaptionName
        {
            get { return _CaptionName; }
            set
            {
                if (_CaptionName != value)
                {
                    _CaptionName = value;
                    OnPropertyChanged(() => CaptionName);
                }
            }
        }

        public AccountTypeEnum AccountType
        {
            get { return _AccountType; }
            set
            {
                if (_AccountType != value)
                {
                    _AccountType = value;
                    OnPropertyChanged(() => AccountType);
                }
            }
        }


        public int Position
        {
            get { return _Position; }
            set
            {
                if (_Position != value)
                {
                    _Position = value;
                    OnPropertyChanged(() => Position);
                }
            }
        }

        public string ParentCode
        {
            get { return _ParentCode; }
            set
            {
                if (_ParentCode != value)
                {
                    _ParentCode = value;
                    OnPropertyChanged(() => ParentCode);
                }
            }
        }


        public string Color
        {
            get { return _Color; }
            set
            {
                if (_Color != value)
                {
                    _Color = value;
                    OnPropertyChanged(() => Color);
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

        public bool FlagOpex
        {
            get { return _FlagOpex; }
            set
            {
                if (_FlagOpex != value)
                {
                    _FlagOpex = value;
                    OnPropertyChanged(() => FlagOpex);
                }
            }
        }


        class PLCaptionValidator : AbstractValidator<PLCaption>
        {
            public PLCaptionValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Capton Name is required.");
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Caption Code is required.");
                RuleFor(obj => obj.AccountType).NotEmpty().WithMessage("AccountType is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new PLCaptionValidator();
        }
    }
}
