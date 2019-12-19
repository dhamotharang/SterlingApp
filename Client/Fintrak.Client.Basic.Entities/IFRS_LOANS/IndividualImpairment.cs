using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class IndividualImpairment : ObjectBase
    {

        int _Id;
        string _RefNo;
        string _AccountNo;
        string _ProductName;
        DateTime _ValueDate;
        DateTime _MaturityDate;
        DateTime _RunDate;
        bool _Processed;
        bool _Active;

        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(() => Id);
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

        public string ProductName
        {
            get { return _ProductName; }
            set
            {
                if (_ProductName != value)
                {
                    _ProductName = value;
                    OnPropertyChanged(() => ProductName);
                }
            }
        }

        public DateTime ValueDate
        {
            get { return _ValueDate; }
            set
            {
                if (_ValueDate != value)
                {
                    _ValueDate = value;
                    OnPropertyChanged(() => ValueDate);
                }
            }
        }

        public DateTime MaturityDate
        {
            get { return _MaturityDate; }
            set
            {
                if (_MaturityDate != value)
                {
                    _MaturityDate = value;
                    OnPropertyChanged(() => MaturityDate);
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

        public bool Processed
        {
            get { return _Processed; }
            set
            {
                if (_Processed != value)
                {
                    _Processed = value;
                    OnPropertyChanged(() => Processed);
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


        class IndividualImpairmentValidator : AbstractValidator<IndividualImpairment>
        {
            public IndividualImpairmentValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
                RuleFor(obj => obj.AccountNo).NotEmpty().WithMessage("AccountNo is required.");
                RuleFor(obj => obj.ProductName).NotEmpty().WithMessage("ProductName is required.");

                //RuleFor(obj => obj.LGD).GreaterThan(0).WithMessage("LGD is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IndividualImpairmentValidator();
        }


    }
}