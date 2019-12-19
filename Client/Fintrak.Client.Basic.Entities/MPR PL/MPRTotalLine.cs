using System;
using System.Linq;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class MPRTotalLine : ObjectBase
    {
        int _MPRTotalLineId;
        string _Name;
        int _Position;
        int? _ParentId;
        string _Color;
        string _CompanyCode;
        bool _Active;

        public int MPRTotalLineId
        {
            get { return _MPRTotalLineId; }
            set
            {
                if (_MPRTotalLineId != value)
                {
                    _MPRTotalLineId = value;
                    OnPropertyChanged(() => MPRTotalLineId);
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

        
        class MPRTotalLineValidator : AbstractValidator<MPRTotalLine>
        {
            public MPRTotalLineValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new MPRTotalLineValidator();
        }
    }
}
