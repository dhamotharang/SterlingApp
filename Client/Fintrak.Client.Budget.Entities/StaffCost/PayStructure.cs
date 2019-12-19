using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class PayStructure : ObjectBase
    {
        int _PayStructureId;
        string _GradeCode;
        string _ClassificationCode;
        decimal _GrossPay;
        decimal _ThirtheenMonth;
        string _ReviewCode; 
        string _Year;
        bool _Active;

        public int PayStructureId
        {
            get { return _PayStructureId; }
            set
            {
                if (_PayStructureId != value)
                {
                    _PayStructureId = value;
                    OnPropertyChanged(() => PayStructureId);
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


        public decimal GrossPay
        {
            get { return _GrossPay; }
            set
            {
                if (_GrossPay != value)
                {
                    _GrossPay = value;
                    OnPropertyChanged(() => GrossPay);
                }
            }
        }

        public decimal ThirtheenMonth
        {
            get { return _ThirtheenMonth; }
            set
            {
                if (_ThirtheenMonth != value)
                {
                    _ThirtheenMonth = value;
                    OnPropertyChanged(() => ThirtheenMonth);
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

        public string LongThirtheenMonth
        {
            get
            {
                return string.Format("{0} - {1}", _GradeCode, _ClassificationCode);
            }
        }

        
        class PayStructureValidator : AbstractValidator<PayStructure>
        {
            public PayStructureValidator()
            {
                RuleFor(obj => obj.GradeCode).NotEmpty().WithMessage("GradeCode is required.");
                RuleFor(obj => obj.ClassificationCode).NotEmpty().WithMessage("ClassificationCode is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new PayStructureValidator();
        }
    }
}
