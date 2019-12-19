using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;


namespace Fintrak.Client.Basic.Entities
{
    public class RevenueBudget : ObjectBase
    {
        int _BudgetId;
        string _CaptionName;
        string _Year;
        string _MisCode;
        decimal _Mth1;
        decimal _Mth2;
        decimal _Mth3;
        decimal _Mth4;
        string _CompanyCode;
        decimal _Mth5;
        decimal _Mth6;
        decimal _Mth7;
        decimal _Mth8;
        decimal _Mth9;
        decimal _Mth10;
        decimal _Mth11;
        decimal _Mth12;
        bool _Active;

        public int BudgetId
        {
            get { return _BudgetId; }
            set
            {
                if (_BudgetId != value)
                {
                    _BudgetId = value;
                    OnPropertyChanged(() => BudgetId);
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

        public decimal Mth1
        {
            get { return _Mth1; }
            set
            {
                if (_Mth1 != value)
                {
                    _Mth1 = value;
                    OnPropertyChanged(() => Mth1);
                }
            }
        }

        public decimal Mth2
        {
            get { return _Mth2; }
            set
            {
                if (_Mth2 != value)
                {
                    _Mth2 = value;
                    OnPropertyChanged(() => Mth2);
                }
            }
        }


        public decimal Mth3
        {
            get { return _Mth3; }
            set
            {
                if (_Mth3 != value)
                {
                    _Mth3 = value;
                    OnPropertyChanged(() => Mth3);
                }
            }
        }

        public decimal Mth4
        {
            get { return _Mth4; }
            set
            {
                if (_Mth4 != value)
                {
                    _Mth4 = value;
                    OnPropertyChanged(() => Mth4);
                }
            }
        }

        public decimal Mth5
        {
            get { return _Mth5; }
            set
            {
                if (_Mth5 != value)
                {
                    _Mth5 = value;
                    OnPropertyChanged(() => Mth5);
                }
            }
        }

        public decimal Mth6
        {
            get { return _Mth6; }
            set
            {
                if (_Mth6 != value)
                {
                    _Mth6 = value;
                    OnPropertyChanged(() => Mth6);
                }
            }
        }


        public decimal Mth7
        {
            get { return _Mth7; }
            set
            {
                if (_Mth7 != value)
                {
                    _Mth7 = value;
                    OnPropertyChanged(() => Mth7);
                }
            }
        }

        public decimal Mth8
        {
            get { return _Mth8; }
            set
            {
                if (_Mth8 != value)
                {
                    _Mth8 = value;
                    OnPropertyChanged(() => Mth8);
                }
            }
        }



        public decimal Mth9
        {
            get { return _Mth9; }
            set
            {
                if (_Mth9 != value)
                {
                    _Mth9 = value;
                    OnPropertyChanged(() => Mth9);
                }
            }
        }

        public decimal Mth10
        {
            get { return _Mth10; }
            set
            {
                if (_Mth10 != value)
                {
                    _Mth10 = value;
                    OnPropertyChanged(() => Mth10);
                }
            }
        }


        public decimal Mth11
        {
            get { return _Mth11; }
            set
            {
                if (_Mth11 != value)
                {
                    _Mth11 = value;
                    OnPropertyChanged(() => Mth11);
                }
            }
        }

        public decimal Mth12
        {
            get { return _Mth12; }
            set
            {
                if (_Mth12 != value)
                {
                    _Mth12 = value;
                    OnPropertyChanged(() => Mth12);
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


        class RevenueBudgetValidator : AbstractValidator<RevenueBudget>
        {
            public RevenueBudgetValidator()
            {
                RuleFor(obj => obj.CaptionName).NotEmpty().WithMessage("CaptionName is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new RevenueBudgetValidator();
        }
    }
}
