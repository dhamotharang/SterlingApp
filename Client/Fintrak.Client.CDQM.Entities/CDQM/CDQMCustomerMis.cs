using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.CDQM.Entities
{
    public class CDQMCustomerMIS : ObjectBase
    {
        int _CustomerMISId;
        string _TargetMarketCode;
        string _TargetMarketName;
        string _SegmentCode;
        string _SegmentName;
        string _GroupCode;
        string _GroupName;
        string _DivisionCode;
        string _DivisionName;
        bool _Active;

        public int CustomerMISId
        {
            get { return _CustomerMISId; }
            set
            {
                if (_CustomerMISId != value)
                {
                    _CustomerMISId = value;
                    OnPropertyChanged(() => CustomerMISId);
                }
            }
        }

        public string TargetMarketCode
        {
            get { return _TargetMarketCode; }
            set
            {
                if (_TargetMarketCode != value)
                {
                    _TargetMarketCode = value;
                    OnPropertyChanged(() => TargetMarketCode);
                }
            }
        }

        public string TargetMarketName
        {
            get { return _TargetMarketName; }
            set
            {
                if (_TargetMarketName != value)
                {
                    _TargetMarketName = value;
                    OnPropertyChanged(() => TargetMarketName);
                }
            }
        }

        public string SegmentCode
        {
            get { return _SegmentCode; }
            set
            {
                if (_SegmentCode != value)
                {
                    _SegmentCode = value;
                    OnPropertyChanged(() => SegmentCode);
                }
            }
        }

        public string SegmentName
        {
            get { return _SegmentName; }
            set
            {
                if (_SegmentName != value)
                {
                    _SegmentName = value;
                    OnPropertyChanged(() => SegmentName);
                }
            }
        }

        public string GroupCode
        {
            get { return _GroupCode; }
            set
            {
                if (_GroupCode != value)
                {
                    _GroupCode = value;
                    OnPropertyChanged(() => GroupCode);
                }
            }
        }

        public string GroupName
        {
            get { return _GroupName; }
            set
            {
                if (_GroupName != value)
                {
                    _GroupName = value;
                    OnPropertyChanged(() => GroupName);
                }
            }
        }

        public string DivisionCode
        {
            get { return _DivisionCode; }
            set
            {
                if (_DivisionCode != value)
                {
                    _DivisionCode = value;
                    OnPropertyChanged(() => DivisionCode);
                }
            }
        }

        public string DivisionName
        {
            get { return _DivisionName; }
            set
            {
                if (_DivisionName != value)
                {
                    _DivisionName = value;
                    OnPropertyChanged(() => DivisionName);
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


        class CDQMCustomerMISValidator : AbstractValidator<CDQMCustomerMIS>
        {
            public CDQMCustomerMISValidator()
            {

            }
        }

        protected override IValidator GetValidator()
        {
            return new CDQMCustomerMISValidator();
        }
    }
}
