using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class FacilityStaging : ObjectBase
    {
        int _facId;
        string _Refno;
        string _AccountNo;
        string _CustomerName;
        string _CustomerNo;
        string _FacilityType;
        int _Stage;
        DateTime _ReportDate;
        bool _Active;

        public int facId
        {
            get { return _facId; }
            set
            {
                if (_facId != value)
                {
                    _facId = value;
                    OnPropertyChanged(() => facId);
                }
            }
        }

        public string Refno
        {
            get { return _Refno; }
            set
            {
                if (_Refno != value)
                {
                    _Refno = value;
                    OnPropertyChanged(() => Refno);
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

        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    OnPropertyChanged(() => CustomerName);
                }
            }
        }

        public string CustomerNo
        {
            get { return _CustomerNo; }
            set
            {
                if (_CustomerNo != value)
                {
                    _CustomerNo = value;
                    OnPropertyChanged(() => CustomerNo);
                }
            }
        }
        public string FacilityType
        {
            get { return _FacilityType; }
            set
            {
                if (_FacilityType != value)
                {
                    _FacilityType = value;
                    OnPropertyChanged(() => FacilityType);
                }
            }
        }

        public int Stage
        {
            get { return _Stage; }
            set
            {
                if (_Stage != value)
                {
                    _Stage = value;
                    OnPropertyChanged(() => Stage);
                }
            }
        }

        public DateTime ReportDate
        {
            get { return _ReportDate; }
            set
            {
                if (_ReportDate != value)
                {
                    _ReportDate = value;
                    OnPropertyChanged(() => ReportDate);
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


        class FacilityStagingValidator : AbstractValidator<FacilityStaging>
        {
            public FacilityStagingValidator()
            {
                RuleFor(obj => obj.Refno).NotEmpty().WithMessage("Reference Number is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new FacilityStagingValidator();
        }
    }
}
