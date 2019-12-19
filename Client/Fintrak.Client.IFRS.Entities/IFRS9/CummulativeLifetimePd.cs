using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class CummulativeLifetimePd : ObjectBase
    {
        int _Id;
        string _ProductType;
        string _sub_type;
        string _CummulativeLifeTimePD;
        double _OutBalAfter_yr;
        double _OutBalAfter_yr1;
        double _OutBalAfter_yr2;
        double _OutBalAfter_yr3;
        double _OutBalAfter_yr4;
        double _OutBalAfter_yr5;
        double _OutBalAfter_yr6;
        double _OutBalAfter_yr7;
        double _OutBalAfter_yr8;
        double _OutBalAfter_yr9;
        double _OutBalAfter_yr10;
        double _OutBalAfter_yr11;
        double _OutBalAfter_yr12;
        double _OutBalAfter_yr13;
        double _OutBalAfter_yr14;
        double _OutBalAfter_yr15;
        double _OutBalAfter_yr16;
        double _OutBalAfter_yr17;
        double _OutBalAfter_yr18;
        double _OutBalAfter_yr19;
        double _OutBalAfter_yr20;
        double _OutBalAfter_yr21;
        double _OutBalAfter_yr22;
        double _OutBalAfter_yr23;
        double _OutBalAfter_yr24;
        double _OutBalAfter_yr25;
        double _OutBalAfter_yr26;
        double _OutBalAfter_yr27;
        double _OutBalAfter_yr28;
        double _OutBalAfter_yr29;
        double _OutBalAfter_yr30;
        double _OutBalAfter_yr31;
        double _OutBalAfter_yr32;
        double _OutBalAfter_yr33;
        double _OutBalAfter_yr34;
        double _OutBalAfter_yr35;
        double _OutBalAfter_yr36;
        double _OutBalAfter_yr37;
        double _OutBalAfter_yr38;
        double _OutBalAfter_yr39;
        double _OutBalAfter_yr40;
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

        public string CummulativeLifeTimePD {
            get { return _CummulativeLifeTimePD; }
            set {
                if (_CummulativeLifeTimePD != value) {
                    _CummulativeLifeTimePD = value;
                    OnPropertyChanged(() => CummulativeLifeTimePD);
                }
            }
        }
        public double OutBalAfter_yr
        {
            get { return _OutBalAfter_yr; }
            set
            {
                if (_OutBalAfter_yr != value)
                {
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
        public double OutBalAfter_yr7 {
            get { return _OutBalAfter_yr7; }
            set {
                if (_OutBalAfter_yr7 != value) {
                    _OutBalAfter_yr7 = value;
                    OnPropertyChanged(() => OutBalAfter_yr7);
                }
            }
        }
        public double OutBalAfter_yr8 {
            get { return _OutBalAfter_yr8; }
            set {
                if (_OutBalAfter_yr8 != value) {
                    _OutBalAfter_yr8 = value;
                    OnPropertyChanged(() => OutBalAfter_yr8);
                }
            }
        }
        public double OutBalAfter_yr9 {
            get { return _OutBalAfter_yr9; }
            set {
                if (_OutBalAfter_yr9 != value) {
                    _OutBalAfter_yr9 = value;
                    OnPropertyChanged(() => OutBalAfter_yr9);
                }
            }
        }
        public double OutBalAfter_yr10 {
            get { return _OutBalAfter_yr10; }
            set {
                if (_OutBalAfter_yr10 != value) {
                    _OutBalAfter_yr10 = value;
                    OnPropertyChanged(() => OutBalAfter_yr10);
                }
            }
        }
        public double OutBalAfter_yr11 {
            get { return _OutBalAfter_yr11; }
            set {
                if (_OutBalAfter_yr11 != value) {
                    _OutBalAfter_yr11 = value;
                    OnPropertyChanged(() => OutBalAfter_yr11);
                }
            }
        }
        public double OutBalAfter_yr12 {
            get { return _OutBalAfter_yr12; }
            set {
                if (_OutBalAfter_yr12 != value) {
                    _OutBalAfter_yr12 = value;
                    OnPropertyChanged(() => OutBalAfter_yr12);
                }
            }
        }
        public double OutBalAfter_yr13 {
            get { return _OutBalAfter_yr13; }
            set {
                if (_OutBalAfter_yr13 != value) {
                    _OutBalAfter_yr13 = value;
                    OnPropertyChanged(() => OutBalAfter_yr13);
                }
            }
        }
        public double OutBalAfter_yr14 {
            get { return _OutBalAfter_yr14; }
            set {
                if (_OutBalAfter_yr14 != value) {
                    _OutBalAfter_yr14 = value;
                    OnPropertyChanged(() => OutBalAfter_yr14);
                }
            }
        }
        public double OutBalAfter_yr15 {
            get { return _OutBalAfter_yr15; }
            set {
                if (_OutBalAfter_yr15 != value) {
                    _OutBalAfter_yr15 = value;
                    OnPropertyChanged(() => OutBalAfter_yr15);
                }
            }
        }
        public double OutBalAfter_yr16 {
            get { return _OutBalAfter_yr16; }
            set {
                if (_OutBalAfter_yr16 != value) {
                    _OutBalAfter_yr16 = value;
                    OnPropertyChanged(() => OutBalAfter_yr16);
                }
            }
        }
        public double OutBalAfter_yr17 {
            get { return _OutBalAfter_yr17; }
            set {
                if (_OutBalAfter_yr17 != value) {
                    _OutBalAfter_yr17 = value;
                    OnPropertyChanged(() => OutBalAfter_yr17);
                }
            }
        }
        public double OutBalAfter_yr18 {
            get { return _OutBalAfter_yr18; }
            set {
                if (_OutBalAfter_yr18 != value) {
                    _OutBalAfter_yr18 = value;
                    OnPropertyChanged(() => OutBalAfter_yr18);
                }
            }
        }
        public double OutBalAfter_yr19 {
            get { return _OutBalAfter_yr19; }
            set {
                if (_OutBalAfter_yr19 != value) {
                    _OutBalAfter_yr19 = value;
                    OnPropertyChanged(() => OutBalAfter_yr19);
                }
            }
        }
        public double OutBalAfter_yr20 {
            get { return _OutBalAfter_yr20; }
            set {
                if (_OutBalAfter_yr20 != value) {
                    _OutBalAfter_yr20 = value;
                    OnPropertyChanged(() => OutBalAfter_yr20);
                }
            }
        }
        public double OutBalAfter_yr21 {
            get { return _OutBalAfter_yr21; }
            set {
                if (_OutBalAfter_yr21 != value) {
                    _OutBalAfter_yr21 = value;
                    OnPropertyChanged(() => OutBalAfter_yr21);
                }
            }
        }
        public double OutBalAfter_yr22 {
            get { return _OutBalAfter_yr22; }
            set {
                if (_OutBalAfter_yr22 != value) {
                    _OutBalAfter_yr22 = value;
                    OnPropertyChanged(() => OutBalAfter_yr22);
                }
            }
        }
        public double OutBalAfter_yr23 {
            get { return _OutBalAfter_yr23; }
            set {
                if (_OutBalAfter_yr23 != value) {
                    _OutBalAfter_yr23 = value;
                    OnPropertyChanged(() => OutBalAfter_yr23);
                }
            }
        }
        public double OutBalAfter_yr24 {
            get { return _OutBalAfter_yr24; }
            set {
                if (_OutBalAfter_yr24 != value) {
                    _OutBalAfter_yr24 = value;
                    OnPropertyChanged(() => OutBalAfter_yr24);
                }
            }
        }
        public double OutBalAfter_yr25 {
            get { return _OutBalAfter_yr25; }
            set {
                if (_OutBalAfter_yr25 != value) {
                    _OutBalAfter_yr25 = value;
                    OnPropertyChanged(() => OutBalAfter_yr25);
                }
            }
        }
        public double OutBalAfter_yr26 {
            get { return _OutBalAfter_yr26; }
            set {
                if (_OutBalAfter_yr26 != value) {
                    _OutBalAfter_yr26 = value;
                    OnPropertyChanged(() => OutBalAfter_yr26);
                }
            }
        }
        public double OutBalAfter_yr27 {
            get { return _OutBalAfter_yr27; }
            set {
                if (_OutBalAfter_yr27 != value) {
                    _OutBalAfter_yr27 = value;
                    OnPropertyChanged(() => OutBalAfter_yr27);
                }
            }
        }
        public double OutBalAfter_yr28 {
            get { return _OutBalAfter_yr28; }
            set {
                if (_OutBalAfter_yr28 != value) {
                    _OutBalAfter_yr28 = value;
                    OnPropertyChanged(() => OutBalAfter_yr28);
                }
            }
        }
        public double OutBalAfter_yr29 {
            get { return _OutBalAfter_yr29; }
            set {
                if (_OutBalAfter_yr29 != value) {
                    _OutBalAfter_yr29 = value;
                    OnPropertyChanged(() => OutBalAfter_yr29);
                }
            }
        }
        public double OutBalAfter_yr30 {
            get { return _OutBalAfter_yr30; }
            set {
                if (_OutBalAfter_yr30 != value) {
                    _OutBalAfter_yr30 = value;
                    OnPropertyChanged(() => OutBalAfter_yr30);
                }
            }
        }
        public double OutBalAfter_yr31 {
            get { return _OutBalAfter_yr31; }
            set {
                if (_OutBalAfter_yr31 != value) {
                    _OutBalAfter_yr31 = value;
                    OnPropertyChanged(() => OutBalAfter_yr31);
                }
            }
        }
        public double OutBalAfter_yr32 {
            get { return _OutBalAfter_yr32; }
            set {
                if (_OutBalAfter_yr32 != value) {
                    _OutBalAfter_yr32 = value;
                    OnPropertyChanged(() => OutBalAfter_yr32);
                }
            }
        }
        public double OutBalAfter_yr33 {
            get { return _OutBalAfter_yr33; }
            set {
                if (_OutBalAfter_yr33 != value) {
                    _OutBalAfter_yr33 = value;
                    OnPropertyChanged(() => OutBalAfter_yr33);
                }
            }
        }
        public double OutBalAfter_yr34 {
            get { return _OutBalAfter_yr34; }
            set {
                if (_OutBalAfter_yr34 != value) {
                    _OutBalAfter_yr34 = value;
                    OnPropertyChanged(() => OutBalAfter_yr34);
                }
            }
        }
        public double OutBalAfter_yr35 {
            get { return _OutBalAfter_yr35; }
            set {
                if (_OutBalAfter_yr35 != value) {
                    _OutBalAfter_yr35 = value;
                    OnPropertyChanged(() => OutBalAfter_yr35);
                }
            }
        }
        public double OutBalAfter_yr36 {
            get { return _OutBalAfter_yr36; }
            set {
                if (_OutBalAfter_yr36 != value) {
                    _OutBalAfter_yr36 = value;
                    OnPropertyChanged(() => OutBalAfter_yr36);
                }
            }
        }
        public double OutBalAfter_yr37 {
            get { return _OutBalAfter_yr37; }
            set {
                if (_OutBalAfter_yr37 != value) {
                    _OutBalAfter_yr37 = value;
                    OnPropertyChanged(() => OutBalAfter_yr37);
                }
            }
        }
        public double OutBalAfter_yr38 {
            get { return _OutBalAfter_yr38; }
            set {
                if (_OutBalAfter_yr38 != value) {
                    _OutBalAfter_yr38 = value;
                    OnPropertyChanged(() => OutBalAfter_yr38);
                }
            }
        }
        public double OutBalAfter_yr39 {
            get { return _OutBalAfter_yr39; }
            set {
                if (_OutBalAfter_yr39 != value) {
                    _OutBalAfter_yr39 = value;
                    OnPropertyChanged(() => OutBalAfter_yr39);
                }
            }
        }
        public double OutBalAfter_yr40 {
            get { return _OutBalAfter_yr40; }
            set {
                if (_OutBalAfter_yr40 != value) {
                    _OutBalAfter_yr40 = value;
                    OnPropertyChanged(() => OutBalAfter_yr40);
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


        class CummulativeLifetimePdValidator : AbstractValidator<CummulativeLifetimePd>
        {
            public CummulativeLifetimePdValidator()
            {
                //RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");        
            }
        }

        protected override IValidator GetValidator()
        {
            return new CummulativeLifetimePdValidator();
        }
    }
}
