using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class HistoricalDefaultFreq : ObjectBase
    {
        int _ID;
        string _ProductType;
        string _Sub_type;
        int _Origin_Yr;
        double _AdjBal;


        double _OutBalAfter_Yr;
        double _OutBalAfter_Yr1;
        double _OutBalAfter_Yr2;
        double _OutBalAfter_Yr3;
        double _OutBalAfter_Yr4;
        double _OutBalAfter_Yr5;

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


        public string ProductType {
            get { return _ProductType; }
            set {
                if (_ProductType != value) {
                    _ProductType = value;
                    OnPropertyChanged(() => ProductType);
                }
            }
        }

        public string Sub_type {
            get { return _Sub_type; }
            set {
                if (_Sub_type != value) {
                    _Sub_type = value;
                    OnPropertyChanged(() => Sub_type);
                }
            }
        }

        public int Origin_Yr {
            get { return _Origin_Yr; }
            set {
                if (_Origin_Yr != value) {
                    _Origin_Yr = value;
                    OnPropertyChanged(() => Origin_Yr);
                }
            }
        }

        public double AdjBal {
            get { return _AdjBal; }
            set {
                if (_AdjBal != value) {
                    _AdjBal = value;
                    OnPropertyChanged(() => AdjBal);
                }
            }
        }


        public double OutBalAfter_Yr {
            get { return _OutBalAfter_Yr; }
            set {
                if (_OutBalAfter_Yr != value) {
                    _OutBalAfter_Yr = value;
                    OnPropertyChanged(() => OutBalAfter_Yr);
                }
            }
        }

        public double OutBalAfter_Yr1 {
            get { return _OutBalAfter_Yr1; }
            set {
                if (_OutBalAfter_Yr1 != value) {
                    _OutBalAfter_Yr1 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr1);
                }
            }
        }
        public double OutBalAfter_Yr2 {
            get { return _OutBalAfter_Yr2; }
            set {
                if (_OutBalAfter_Yr2 != value) {
                    _OutBalAfter_Yr2 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr2);
                }
            }
        }
        public double OutBalAfter_Yr3 {
            get { return _OutBalAfter_Yr3; }
            set {
                if (_OutBalAfter_Yr3 != value) {
                    _OutBalAfter_Yr3 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr3);
                }
            }
        }
        public double OutBalAfter_Yr4 {
            get { return _OutBalAfter_Yr4; }
            set {
                if (_OutBalAfter_Yr4 != value) {
                    _OutBalAfter_Yr4 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr4);
                }
            }
        }
        public double OutBalAfter_Yr5 {
            get { return _OutBalAfter_Yr5; }
            set {
                if (_OutBalAfter_Yr5 != value) {
                    _OutBalAfter_Yr5 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr5);
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


        class HistoricalDefaultFreqValidator : AbstractValidator<HistoricalDefaultFreq>
        {
            public HistoricalDefaultFreqValidator()
            {
                //RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");        
            }
        }

        protected override IValidator GetValidator()
        {
            return new HistoricalDefaultFreqValidator();
        }
    }
}
