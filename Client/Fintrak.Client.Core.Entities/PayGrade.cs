using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.Core.Entities
{
    public class PayGrade : ObjectBase
    {
        int _PayGradeId;
        string _Code;
        string _Name;
        decimal _GrossPay;
        decimal _NetPay;
        decimal _ThirteenthMonth;
        bool _Active;

        public int PayGradeId
        {
            get { return _PayGradeId; }
            set
            {
                if (_PayGradeId != value)
                {
                    _PayGradeId = value;
                    OnPropertyChanged(() => PayGradeId);
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

        public decimal NetPay
        {
            get { return _NetPay; }
            set
            {
                if (_NetPay != value)
                {
                    _NetPay = value;
                    OnPropertyChanged(() => NetPay);
                }
            }
        }

        public decimal ThirteenthMonth
        {
            get { return _ThirteenthMonth; }
            set
            {
                if (_ThirteenthMonth != value)
                {
                    _ThirteenthMonth = value;
                    OnPropertyChanged(() => ThirteenthMonth);
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

        public string LongDescription
        {
            get
            {
                return string.Format("{0} - {1}", _Name, _GrossPay );
            }
        }

        class PayGradeValidator : AbstractValidator<PayGrade>
        {
            public PayGradeValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code must not be empty.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new PayGradeValidator();
        }
    }
}
