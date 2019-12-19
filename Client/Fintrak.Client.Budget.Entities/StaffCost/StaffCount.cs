using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class StaffCount : TransactionObjectIntBase
    {
        int _StaffCountId;
        string _DefintionCode;
        string _MisCode;
        string _GradeCode;
        string _ClassificationCode;
        string _CurrencyCode;
        TransactionTypeEnum _TransactionType;
        CenterTypeEnum _CenterType;
        string _ReviewCode;
        string _Year;
        bool _Active;

        public int StaffCountId
        {
            get { return _StaffCountId; }
            set
            {
                if (_StaffCountId != value)
                {
                    _StaffCountId = value;
                    OnPropertyChanged(() => StaffCountId);
                }
            }
        }


        public string DefintionCode
        {
            get { return _DefintionCode; }
            set
            {
                if (_DefintionCode != value)
                {
                    _DefintionCode = value;
                    OnPropertyChanged(() => DefintionCode);
                }
            }
        }

        public string MisCode
        {
            get { return _MisCode; }
            set
            {
                if (_MisCode != value)
                {
                    _MisCode = value;
                    OnPropertyChanged(() => MisCode);
                }
            }
        }


        public string GradeCode
        {
            get { return _GradeCode; }
            set
            {
                if (_GradeCode != value)
                {
                    _GradeCode = value;
                    OnPropertyChanged(() => GradeCode);
                }
            }
        }


        public string ClassificationCode
        {
            get { return _ClassificationCode; }
            set
            {
                if (_ClassificationCode != value)
                {
                    _ClassificationCode = value;
                    OnPropertyChanged(() => ClassificationCode);
                }
            }
        }



        public string CurrencyCode
        {
            get { return _CurrencyCode; }
            set
            {
                if (_CurrencyCode != value)
                {
                    _CurrencyCode = value;
                    OnPropertyChanged(() => CurrencyCode);
                }
            }
        }

        public TransactionTypeEnum TransactionType
        {
            get { return _TransactionType; }
            set
            {
                if (_TransactionType != value)
                {
                    _TransactionType = value;
                    OnPropertyChanged(() => TransactionType);
                }
            }
        }


        public CenterTypeEnum CenterType
        {
            get { return _CenterType; }
            set
            {
                if (_CenterType != value)
                {
                    _CenterType = value;
                    OnPropertyChanged(() => CenterType);
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

        public string LongClassificationCode
        {
            get
            {
                return string.Format("{0} - {1}", _DefintionCode, _MisCode);
            }
        }

        
        class StaffCountValidator : AbstractValidator<StaffCount>
        {
            public StaffCountValidator()
            {
                RuleFor(obj => obj.DefintionCode).NotEmpty().WithMessage("DefintionCode is required.");
                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("MisCode is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new StaffCountValidator();
        }
    }
}
