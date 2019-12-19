using System;
using System.Linq;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeCentralVaultSchedule : ObjectBase
    {
        int _IncomeCentralVaultScheduleId;
        string _BranchCode;  
        int _Period;
        DateTime _DatePosted;
        int _Year;
        decimal _Volume;
        string _Currency;
        bool _Active;

        public int IncomeCentralVaultScheduleId
        {
            get { return _IncomeCentralVaultScheduleId; }
            set
            {
                if (_IncomeCentralVaultScheduleId != value)
                {
                    _IncomeCentralVaultScheduleId = value;
                    OnPropertyChanged(() => IncomeCentralVaultScheduleId);
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

        //public DateTime Rundate
        //{
        //    get { return _DatePosted; }
        //    set
        //    {
        //        if (_DatePosted != value)
        //        {
        //            _DatePosted = value;
        //            OnPropertyChanged(() => DatePosted);
        //        }
        //    }
        //}
       

        public decimal Volume
        {
            get { return _Volume; }
            set
            {
                if (_Volume != value)
                {
                    _Volume = value;
                    OnPropertyChanged(() => Volume);
                }
            }
        }

        public int Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                    OnPropertyChanged(() => Period);
                }
            }
        }

        public int Year
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

        public DateTime DatePosted
        {
            get { return _DatePosted; }
            set
            {
                if (_DatePosted !=  DateTime.Today.Date)
                {
                    _DatePosted =  DateTime.Today.Date;
                    OnPropertyChanged(() => DatePosted);
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


        class IncomeCentralVaultScheduleValidator : AbstractValidator<IncomeCentralVaultSchedule>
        {
            public IncomeCentralVaultScheduleValidator()
            {
                RuleFor(obj => obj._BranchCode).NotEmpty().WithMessage("Branch Code is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeCentralVaultScheduleValidator();
        }
    }
}