using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IFRSRevacctRegistry : ObjectBase
    {
        int _RevenueId;
        string _Code;
        string _Caption;
        int _Position;
        string _RefNote;
        string _FinType;
        string _FinSubType;
        int? _ParentId;
        bool _IsTotalLine;
        string _Color;
        int _Class;
        string _CompanyCode;
        bool _Active;

        public int RevenueId
        {
            get { return _RevenueId; }
            set
            {
                if (_RevenueId != value)
                {
                    _RevenueId = value;
                    OnPropertyChanged(() => RevenueId);
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

        public string RefNote
        {
            get { return _RefNote; }
            set
            {
                if (_RefNote != value)
                {
                    _RefNote = value;
                    OnPropertyChanged(() => RefNote);
                }
            }
        }

        public string FinType
        {
            get { return _FinType; }
            set
            {
                if (_FinType != value)
                {
                    _FinType = value;
                    OnPropertyChanged(() => FinType);
                }
            }
        }

        public string FinSubType
        {
            get { return _FinSubType; }
            set
            {
                if (_FinSubType != value)
                {
                    _FinSubType = value;
                    OnPropertyChanged(() => FinSubType);
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

        public bool IsTotalLine
        {
            get { return _IsTotalLine; }
            set
            {
                if (_IsTotalLine != value)
                {
                    _IsTotalLine = value;
                    OnPropertyChanged(() => IsTotalLine);
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

        public int Class
        {
            get { return _Class; }
            set
            {
                if (_Class != value)
                {
                    _Class = value;
                    OnPropertyChanged(() => Class);
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


        class IFRSRevacctRegistryValidator : AbstractValidator<IFRSRevacctRegistry>
        {
            public IFRSRevacctRegistryValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Caption).NotEmpty().WithMessage("Caption is required.");
                RuleFor(obj => obj.Position).GreaterThan(0).WithMessage("Position is required.");


            }
        }

        protected override IValidator GetValidator()
        {
            return new IFRSRevacctRegistryValidator();
        }
    }
}
