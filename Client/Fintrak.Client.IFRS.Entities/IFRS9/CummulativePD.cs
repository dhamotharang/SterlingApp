using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class CummulativePD : ObjectBase
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
        double _OutBalAfter_Yr6;
        double _OutBalAfter_Yr7;
        double _OutBalAfter_Yr8;
        double _OutBalAfter_Yr9;
        double _OutBalAfter_Yr10;
        double _OutBalAfter_Yr11;
        double _OutBalAfter_Yr12;
        double _OutBalAfter_Yr13;
        double _OutBalAfter_Yr14;
        double _OutBalAfter_Yr15;
        double _OutBalAfter_Yr16;
        double _OutBalAfter_Yr17;
        double _OutBalAfter_Yr18;
        double _OutBalAfter_Yr19;
        double _OutBalAfter_Yr20;
        double _OutBalAfter_Yr21;
        double _OutBalAfter_Yr22;
        double _OutBalAfter_Yr23;
        double _OutBalAfter_Yr24;
        double _OutBalAfter_Yr25;
        double _OutBalAfter_Yr26;
        double _OutBalAfter_Yr27;
        double _OutBalAfter_Yr28;
        double _OutBalAfter_Yr29;
        double _OutBalAfter_Yr30;
        double _OutBalAfter_Yr31;
        double _OutBalAfter_Yr32 ;
        double _OutBalAfter_Yr33;
        double _OutBalAfter_Yr34;
        double _OutBalAfter_Yr35;
        double _OutBalAfter_Yr36;
        double _OutBalAfter_Yr37;
        double _OutBalAfter_Yr38;
        double _OutBalAfter_Yr39;
        double _OutBalAfter_Yr40;

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
        public double OutBalAfter_Yr6 {
            get { return _OutBalAfter_Yr6; }
            set {
                if (_OutBalAfter_Yr6 != value) {
                    _OutBalAfter_Yr6 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr6);
                }
            }
        }
        public double OutBalAfter_Yr7 {
            get { return _OutBalAfter_Yr7; }
            set {
                if (_OutBalAfter_Yr7 != value) {
                    _OutBalAfter_Yr7 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr7);
                }
            }
        }
        public double OutBalAfter_Yr8 {
            get { return _OutBalAfter_Yr8; }
            set {
                if (_OutBalAfter_Yr8 != value) {
                    _OutBalAfter_Yr8 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr8);
                }
            }
        }
        public double OutBalAfter_Yr9 {
            get { return _OutBalAfter_Yr9; }
            set {
                if (_OutBalAfter_Yr9 != value) {
                    _OutBalAfter_Yr9 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr9);
                }
            }
        }
        public double OutBalAfter_Yr10 {
            get { return _OutBalAfter_Yr10; }
            set {
                if (_OutBalAfter_Yr10 != value) {
                    _OutBalAfter_Yr10 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr10);
                }
            }
        }
        public double OutBalAfter_Yr11 {
            get { return _OutBalAfter_Yr11; }
            set {
                if (_OutBalAfter_Yr11 != value) {
                    _OutBalAfter_Yr11 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr11);
                }
            }
        }
        public double OutBalAfter_Yr12 {
            get { return _OutBalAfter_Yr12; }
            set {
                if (_OutBalAfter_Yr12 != value) {
                    _OutBalAfter_Yr12 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr12);
                }
            }
        }
        public double OutBalAfter_Yr13 {
            get { return _OutBalAfter_Yr13; }
            set {
                if (_OutBalAfter_Yr13 != value) {
                    _OutBalAfter_Yr13 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr13);
                }
            }
        }
        public double OutBalAfter_Yr14 {
            get { return _OutBalAfter_Yr14; }
            set {
                if (_OutBalAfter_Yr14 != value) {
                    _OutBalAfter_Yr14 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr14);
                }
            }
        }
        public double OutBalAfter_Yr15 {
            get { return _OutBalAfter_Yr15; }
            set {
                if (_OutBalAfter_Yr15 != value) {
                    _OutBalAfter_Yr15 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr15);
                }
            }
        }
        public double OutBalAfter_Yr16 {
            get { return _OutBalAfter_Yr16; }
            set {
                if (_OutBalAfter_Yr16 != value) {
                    _OutBalAfter_Yr16 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr16);
                }
            }
        }
        public double OutBalAfter_Yr17 {
            get { return _OutBalAfter_Yr17; }
            set {
                if (_OutBalAfter_Yr17 != value) {
                    _OutBalAfter_Yr17 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr17);
                }
            }
        }
        public double OutBalAfter_Yr18 {
            get { return _OutBalAfter_Yr18; }
            set {
                if (_OutBalAfter_Yr18 != value) {
                    _OutBalAfter_Yr18 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr18);
                }
            }
        }
        public double OutBalAfter_Yr19 {
            get { return _OutBalAfter_Yr19; }
            set {
                if (_OutBalAfter_Yr19 != value) {
                    _OutBalAfter_Yr19 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr19);
                }
            }
        }
        public double OutBalAfter_Yr20 {
            get { return _OutBalAfter_Yr20; }
            set {
                if (_OutBalAfter_Yr20 != value) {
                    _OutBalAfter_Yr20 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr20);
                }
            }
        }
        public double OutBalAfter_Yr21 {
            get { return _OutBalAfter_Yr21; }
            set {
                if (_OutBalAfter_Yr21 != value) {
                    _OutBalAfter_Yr21 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr21);
                }
            }
        }
        public double OutBalAfter_Yr22 {
            get { return _OutBalAfter_Yr22; }
            set {
                if (_OutBalAfter_Yr22 != value) {
                    _OutBalAfter_Yr22 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr22);
                }
            }
        }
        public double OutBalAfter_Yr23 {
            get { return _OutBalAfter_Yr23; }
            set {
                if (_OutBalAfter_Yr23 != value) {
                    _OutBalAfter_Yr23 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr23);
                }
            }
        }
        public double OutBalAfter_Yr24 {
            get { return _OutBalAfter_Yr24; }
            set {
                if (_OutBalAfter_Yr24 != value) {
                    _OutBalAfter_Yr24 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr24);
                }
            }
        }
        public double OutBalAfter_Yr25 {
            get { return _OutBalAfter_Yr25; }
            set {
                if (_OutBalAfter_Yr25 != value) {
                    _OutBalAfter_Yr25 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr25);
                }
            }
        }
        public double OutBalAfter_Yr26 {
            get { return _OutBalAfter_Yr26; }
            set {
                if (_OutBalAfter_Yr26 != value) {
                    _OutBalAfter_Yr26 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr26);
                }
            }
        }
        public double OutBalAfter_Yr27 {
            get { return _OutBalAfter_Yr27; }
            set {
                if (_OutBalAfter_Yr27 != value) {
                    _OutBalAfter_Yr27 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr27);
                }
            }
        }
        public double OutBalAfter_Yr28 {
            get { return _OutBalAfter_Yr28; }
            set {
                if (_OutBalAfter_Yr28 != value) {
                    _OutBalAfter_Yr28 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr28);
                }
            }
        }
        public double OutBalAfter_Yr29 {
            get { return _OutBalAfter_Yr29; }
            set {
                if (_OutBalAfter_Yr29 != value) {
                    _OutBalAfter_Yr29 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr29);
                }
            }
        }
        public double OutBalAfter_Yr30 {
            get { return _OutBalAfter_Yr30; }
            set {
                if (_OutBalAfter_Yr30 != value) {
                    _OutBalAfter_Yr30 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr30);
                }
            }
        }
        public double OutBalAfter_Yr31 {
            get { return _OutBalAfter_Yr31; }
            set {
                if (_OutBalAfter_Yr31 != value) {
                    _OutBalAfter_Yr31 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr31);
                }
            }
        }
        public double OutBalAfter_Yr32 {
            get { return _OutBalAfter_Yr32; }
            set {
                if (_OutBalAfter_Yr32 != value) {
                    _OutBalAfter_Yr32 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr32);
                }
            }
        }
        public double OutBalAfter_Yr33 {
            get { return _OutBalAfter_Yr33; }
            set {
                if (_OutBalAfter_Yr33 != value) {
                    _OutBalAfter_Yr33 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr33);
                }
            }
        }
        public double OutBalAfter_Yr34 {
            get { return _OutBalAfter_Yr34; }
            set {
                if (_OutBalAfter_Yr34 != value) {
                    _OutBalAfter_Yr34 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr34);
                }
            }
        }
        public double OutBalAfter_Yr35 {
            get { return _OutBalAfter_Yr35; }
            set {
                if (_OutBalAfter_Yr35 != value) {
                    _OutBalAfter_Yr35 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr35);
                }
            }
        }
        public double OutBalAfter_Yr36 {
            get { return _OutBalAfter_Yr36; }
            set {
                if (_OutBalAfter_Yr36 != value) {
                    _OutBalAfter_Yr36 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr36);
                }
            }
        }
        public double OutBalAfter_Yr37 {
            get { return _OutBalAfter_Yr37; }
            set {
                if (_OutBalAfter_Yr37 != value) {
                    _OutBalAfter_Yr37 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr37);
                }
            }
        }
        public double OutBalAfter_Yr38 {
            get { return _OutBalAfter_Yr38; }
            set {
                if (_OutBalAfter_Yr38 != value) {
                    _OutBalAfter_Yr38 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr38);
                }
            }
        }
        public double OutBalAfter_Yr39 {
            get { return _OutBalAfter_Yr39; }
            set {
                if (_OutBalAfter_Yr39 != value) {
                    _OutBalAfter_Yr39 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr39);
                }
            }
        }
        public double OutBalAfter_Yr40 {
            get { return _OutBalAfter_Yr40; }
            set {
                if (_OutBalAfter_Yr40 != value) {
                    _OutBalAfter_Yr40 = value;
                    OnPropertyChanged(() => OutBalAfter_Yr40);
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


        class CummulativePDValidator : AbstractValidator<CummulativePD>
        {
            public CummulativePDValidator()
            {
                //RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");        
            }
        }

        protected override IValidator GetValidator()
        {
            return new CummulativePDValidator();
        }
    }
}
