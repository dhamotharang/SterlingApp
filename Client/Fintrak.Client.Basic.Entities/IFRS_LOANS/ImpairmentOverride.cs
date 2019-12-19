using System;
using System.Linq;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class ImpairmentOverride : ObjectBase
    {
        int _ImpairmentOverrideId;
        string _RefNo;
        string _AccountNo;
        ImpairmentClassification _Classification;
        string _Reason;
        string _CompanyCode;
        bool _Active;

        public int ImpairmentOverrideId
        {
            get { return _ImpairmentOverrideId; }
            set
            {
                if (_ImpairmentOverrideId != value)
                {
                    _ImpairmentOverrideId = value;
                    OnPropertyChanged(() => ImpairmentOverrideId);
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

       
        public string AccountNo
        {
            get { return _AccountNo; }
            set
            {
                if (_AccountNo != value)
                {
                    _AccountNo = value;
                    OnPropertyChanged(() => AccountNo);
                }
            }
        }


        public ImpairmentClassification Classification
        {
            get { return _Classification; }
            set
            {
                if (_Classification != value)
                {
                    _Classification = value;
                    OnPropertyChanged(() => Classification);
                }
            }
        }


        public string Reason
        {
            get { return _Reason; }
            set
            {
                if (_Reason != value)
                {
                    _Reason = value;
                    OnPropertyChanged(() => Reason);
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


        class ImpairmentOverrideValidator : AbstractValidator<ImpairmentOverride>
        {
            public ImpairmentOverrideValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ImpairmentOverrideValidator();
        }
    }
}
