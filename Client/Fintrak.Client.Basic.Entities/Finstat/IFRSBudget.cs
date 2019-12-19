using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class IFRSBudget : ObjectBase
    {
        int _IFRSBudgetId;
        string _CaptionName;
        decimal _StretchBudget;
        string _CompanyCode;
        decimal _BoardBudget;
        DateTime _ReportDate;
        bool _Active;

        public int IFRSBudgetId
        {
            get { return _IFRSBudgetId; }
            set
            {
                if (_IFRSBudgetId != value)
                {
                    _IFRSBudgetId = value;
                    OnPropertyChanged(() => IFRSBudgetId);
                }
            }
        }

        public string CaptionName
        {
            get { return _CaptionName; }
            set
            {
                if (_CaptionName != value)
                {
                    _CaptionName = value;
                    OnPropertyChanged(() => CaptionName);
                }
            }
        }
       
       

        public decimal StretchBudget
        {
            get { return _StretchBudget; }
            set
            {
                if (_StretchBudget != value)
                {
                    _StretchBudget = value;
                    OnPropertyChanged(() => StretchBudget);
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

        public decimal BoardBudget
        {
            get { return _BoardBudget; }
            set
            {
                if (_BoardBudget != value)
                {
                    _BoardBudget = value;
                    OnPropertyChanged(() => BoardBudget);
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

       

        class ReportValidator : AbstractValidator<IFRSBudget>
        {
            public ReportValidator()
            {
               
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new ReportValidator();
        }
    }
}
