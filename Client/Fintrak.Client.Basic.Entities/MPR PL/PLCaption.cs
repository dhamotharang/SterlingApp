using System;
using System.Linq;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class PLCaption : ObjectBase
    {
        int _PLCaptionId;
        string _Code;
        string _Name;
        AccountTypeEnum _AccountType;
        int _Position;
        int? _ParentId;
        string _Color;
        string _CompanyCode;
        bool _FlagOpex;
        bool _Active;

        public int CaptionId
        {
            get { return _PLCaptionId; }
            set
            {
                if (_PLCaptionId != value)
                {
                    _PLCaptionId = value;
                    OnPropertyChanged(() => CaptionId);
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

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(() => Name);
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

        public int? ParentId
        {
            get { return _ParentId; }
            set
            {
                if (_ParentId != value)
                {
                    _ParentId = value;
                    OnPropertyChanged(() => ParentId);
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
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.AccountType).NotEmpty().WithMessage("AccountType is required.");

             }
        }

        protected override IValidator GetValidator()
        {
            return new PLCaptionValidator();
        }
    }
}
