using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class InvestmentMarginalPd : ObjectBase
    {

        int _ID;
        string _Refno;
        string _Portfolio_Desc;
        string _RatingAgency;
        string _InitRating;
        string _SandRating;


        double __1;
        double __2;
        double __3;
        double __4;
        double __5;
        double __6;
        double __7;
        double __8;
        double __9;
        double __10;
        double __11;
        double __12;
        double __13;
        double __14;
        double __15;

        bool _Active;

        public int ID {
            get { return _ID; }
            set {
                if (_ID != value) {
                    _ID = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }

        public string Refno {
            get { return _Refno; }
            set {
                if (_Refno != value) {
                    _Refno = value;
                    OnPropertyChanged(() => Refno);
                }
            }
        }
        public string Portfolio_Desc {
            get { return _Portfolio_Desc; }
            set {
                if (_Portfolio_Desc != value) {
                    _Portfolio_Desc = value;
                    OnPropertyChanged(() => Portfolio_Desc);
                }
            }
        }

        public string RatingAgency {
            get { return _RatingAgency; }
            set {
                if (_RatingAgency != value) {
                    _RatingAgency = value;
                    OnPropertyChanged(() => RatingAgency);
                }
            }
        }

        public string InitRating {
            get { return _InitRating; }
            set {
                if (_InitRating != value) {
                    _InitRating = value;
                    OnPropertyChanged(() => InitRating);
                }
            }
        }

        public string SandRating {
            get { return _SandRating; }
            set {
                if (_SandRating != value) {
                    _SandRating = value;
                    OnPropertyChanged(() => SandRating);
                }
            }
        }
  

        public double _1 {
            get { return __1; }
            set {
                if (__1 != value) {
                    __1 = value;
                    OnPropertyChanged(() => _1);
                }
            }
        }

        public double _2 {
            get { return __2; }
            set {
                if (__2 != value) {
                    __2 = value;
                    OnPropertyChanged(() => _2);
                }
            }
        }
        public double _3 {
            get { return __3; }
            set {
                if (__3 != value) {
                    __3 = value;
                    OnPropertyChanged(() => _3);
                }
            }
        }
        public double _4 {
            get { return __4; }
            set {
                if (__4 != value) {
                    __4 = value;
                    OnPropertyChanged(() => _4);
                }
            }
        }
        public double _5 {
            get { return __5; }
            set {
                if (__5 != value) {
                    __5 = value;
                    OnPropertyChanged(() => _5);
                }
            }
        }
        public double _6 {
            get { return __6; }
            set {
                if (__6 != value) {
                    __6 = value;
                    OnPropertyChanged(() => _6);
                }
            }
        }
        public double _7 {
            get { return __7; }
            set {
                if (__7 != value) {
                    __7 = value;
                    OnPropertyChanged(() => _7);
                }
            }
        }
        public double _8 {
            get { return __8; }
            set {
                if (__8 != value) {
                    __8 = value;
                    OnPropertyChanged(() => _8);
                }
            }
        }
        public double _9 {
            get { return __9; }
            set {
                if (__9 != value) {
                    __9 = value;
                    OnPropertyChanged(() => _9);
                }
            }
        }
        public double _10 {
            get { return __10; }
            set {
                if (__10 != value) {
                    __10 = value;
                    OnPropertyChanged(() => _10);
                }
            }
        }
        public double _11 {
            get { return __11; }
            set {
                if (__11 != value) {
                    __11 = value;
                    OnPropertyChanged(() => _11);
                }
            }
        }
        public double _12 {
            get { return __12; }
            set {
                if (__12 != value) {
                    __12 = value;
                    OnPropertyChanged(() => _12);
                }
            }
        }
        public double _13 {
            get { return __13; }
            set {
                if (__13 != value) {
                    __13 = value;
                    OnPropertyChanged(() => _13);
                }
            }
        }
        public double _14 {
            get { return __14; }
            set {
                if (__14 != value) {
                    __14 = value;
                    OnPropertyChanged(() => _14);
                }
            }
        }
        public double _15 {
            get { return __15; }
            set {
                if (__15 != value) {
                    __15 = value;
                    OnPropertyChanged(() => _15);
                }
            }
        }


        public bool Active {
            get { return _Active; }
            set {
                if (_Active != value) {
                    _Active = value;
                    OnPropertyChanged(() => Active);
                }
            }
        }


        class InvestmentMarginalPdValidator : AbstractValidator<InvestmentMarginalPd>
        {
            public InvestmentMarginalPdValidator()
            {
                //RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");        
            }
        }

        protected override IValidator GetValidator()
        {
            return new InvestmentMarginalPdValidator();
        }
    }
}
