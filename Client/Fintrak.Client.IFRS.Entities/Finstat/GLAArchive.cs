using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class GLAArchive : ObjectBase
    {
        int _GLAdjustmentId;
        string _AdjustmentCode;
        string _GLCode;
        string _Narration;
        int _Indicator;
        decimal _Amount;
        string _CompanyCode;
        string _BranchCode;
        string _Currency;
        int _ReportType;
        DateTime _RunDate;
        int _AdjustmentType;
        bool _Posted;
        bool _Active;


        public int GLAdjustmentId
        {
            get { return _GLAdjustmentId; }
            set
            {
                if (_GLAdjustmentId != value)
                {
                    _GLAdjustmentId = value;
                    OnPropertyChanged(() => GLAdjustmentId);
                }
            }
        }

        public string AdjustmentCode
        {
            get { return _AdjustmentCode; }
            set
            {
                if (_AdjustmentCode != value)
                {
                    _AdjustmentCode = value;
                    OnPropertyChanged(() => AdjustmentCode);
                }
            }
        }

        public string GLCode
        {
            get { return _GLCode; }
            set
            {
                if (_GLCode != value)
                {
                    _GLCode = value;
                    OnPropertyChanged(() => GLCode);
                }
            }
        }

        public string Narration
        {
            get { return _Narration; }
            set
            {
                if (_Narration != value)
                {
                    _Narration = value;
                    OnPropertyChanged(() => Narration);
                }
            }
        }

        public int Indicator
        {
            get { return _Indicator; }
            set
            {
                if (_Indicator != value)
                {
                    _Indicator = value;
                    OnPropertyChanged(() => Indicator);
                }
            }
        }

        public decimal Amount
        {
            get { return _Amount; }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    OnPropertyChanged(() => Amount);
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

        public string BranchCode
        {
            get { return _BranchCode; }
            set
            {
                if (_BranchCode != value)
                {
                    _BranchCode = value;
                    OnPropertyChanged(() => BranchCode);
                }
            }
        }

        public string Currency
        {
            get { return _Currency; }
            set
            {
                if (_Currency != value)
                {
                    _Currency = value;
                    OnPropertyChanged(() => Currency);
                }
            }
        }

        public int ReportType
        {
            get { return _ReportType; }
            set
            {
                if (_ReportType != value)
                {
                    _ReportType = value;
                    OnPropertyChanged(() => ReportType);
                }
            }
        }

        public DateTime RunDate
        {
            get { return _RunDate; }
            set
            {
                if (_RunDate != value)
                {
                    _RunDate = value;
                    OnPropertyChanged(() => RunDate);
                }
            }
        }

        public int AdjustmentType
        {
            get { return _AdjustmentType; }
            set
            {
                if (_AdjustmentType != value)
                {
                    _AdjustmentType = value;
                    OnPropertyChanged(() => AdjustmentType);
                }
            }
        }



        public bool Posted
        {
            get { return _Posted; }
            set
            {
                if (_Posted != value)
                {
                    _Posted = value;
                    OnPropertyChanged(() => Posted);
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


        class GLAArchiveValidator : AbstractValidator<GLAArchive>
        {
            public GLAArchiveValidator()
            {
                //RuleFor(obj => obj.GLCode).NotEmpty().WithMessage("GLCode is required.");
                //RuleFor(obj => obj.Narration).NotEmpty().WithMessage("Narration is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new GLAArchiveValidator();
        }
    }
}
