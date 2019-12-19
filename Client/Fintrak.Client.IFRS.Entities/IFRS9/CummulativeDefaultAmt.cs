using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class CummulativeDefaultAmt : ObjectBase
    {
        int _Id;
        string _ProductType;
        string _sub_type;
        int _Origin_yr;
        double _AdjBal;
        double _OutBalAfter_yr;
        double _OutBalAfter_yr1;
        double _OutBalAfter_yr2;
        double _OutBalAfter_yr3;
        double _OutBalAfter_yr4;
        double _OutBalAfter_yr5;
        double _OutBalAfter_yr6;

        bool _Active;

        public int Id {
            get { return _Id; }
            set {
                if (_Id != value) {
                    _Id = value;
                    OnPropertyChanged(() => Id);
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

        public string sub_type {
            get { return _sub_type; }
            set {
                if (_sub_type != value) {
                    _sub_type = value;
                    OnPropertyChanged(() => sub_type);
                }
            }
        }

        public int Origin_yr {
            get { return _Origin_yr; }
            set {
                if (_Origin_yr != value) {
                    _Origin_yr = value;
                    OnPropertyChanged(() => Origin_yr);
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
        

        public double OutBalAfter_yr {
            get { return _OutBalAfter_yr; }
            set {
                if (_OutBalAfter_yr != value) {
                    _OutBalAfter_yr = value;
                    OnPropertyChanged(() => OutBalAfter_yr);
                }
            }
        }


        public double OutBalAfter_yr1 {
            get { return _OutBalAfter_yr1; }
            set {
                if (_OutBalAfter_yr1 != value) {
                    _OutBalAfter_yr1 = value;
                    OnPropertyChanged(() => OutBalAfter_yr1);
                }
            }
        }


        public double OutBalAfter_yr2 {
            get { return _OutBalAfter_yr2; }
            set {
                if (_OutBalAfter_yr2 != value) {
                    _OutBalAfter_yr2 = value;
                    OnPropertyChanged(() => OutBalAfter_yr2);
                }
            }
        }


        public double OutBalAfter_yr3 {
            get { return _OutBalAfter_yr3; }
            set {
                if (_OutBalAfter_yr3 != value) {
                    _OutBalAfter_yr3 = value;
                    OnPropertyChanged(() => OutBalAfter_yr3);
                }
            }
        }


        public double OutBalAfter_yr4 {
            get { return _OutBalAfter_yr4; }
            set {
                if (_OutBalAfter_yr4 != value) {
                    _OutBalAfter_yr4 = value;
                    OnPropertyChanged(() => OutBalAfter_yr4);
                }
            }
        }


        public double OutBalAfter_yr5 {
            get { return _OutBalAfter_yr5; }
            set {
                if (_OutBalAfter_yr5 != value) {
                    _OutBalAfter_yr5 = value;
                    OnPropertyChanged(() => OutBalAfter_yr5);
                }
            }
        }


        public double OutBalAfter_yr6 {
            get { return _OutBalAfter_yr6; }
            set {
                if (_OutBalAfter_yr6 != value) {
                    _OutBalAfter_yr6 = value;
                    OnPropertyChanged(() => OutBalAfter_yr6);
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


        class CummulativeDefaultAmtValidator : AbstractValidator<CummulativeDefaultAmt>
        {
            public CummulativeDefaultAmtValidator()
            {
                //RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");        
            }
        }

        protected override IValidator GetValidator()
        {
            return new CummulativeDefaultAmtValidator();
        }
    }
}
