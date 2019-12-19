using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MarginalCCFPivotSTRLB : ObjectBase
    {
        int _ID;

        string _OBEType;
        string _COL;

        double _MCCF0; double _MCCF1; double _MCCF2; double _MCCF3; double _MCCF4; double _MCCF5;
        double _MCCF6; double _MCCF7; double _MCCF8; double _MCCF9; double _MCCF10; double _MCCF11;
        double _MCCF12; double _MCCF13; double _MCCF14; double _MCCF15; double _MCCF16; double _MCCF17;
        double _MCCF18; double _MCCF19; double _MCCF20; double _MCCF21; double _MCCF22; double _MCCF23;
        double _MCCF24; double _MCCF25; double _MCCF26; double _MCCF27; double _MCCF28; double _MCCF29;
        double _MCCF30; double _MCCF31; double _MCCF32; double _MCCF33; double _MCCF34; double _MCCF35;
        double _MCCF36; double _MCCF37; double _MCCF38; double _MCCF39; double _MCCF40; double _MCCF41;
        double _MCCF42; double _MCCF43; double _MCCF44; double _MCCF45; double _MCCF46; double _MCCF47;
        double _MCCF48; double _MCCF49; double _MCCF50; double _MCCF51; double _MCCF52; double _MCCF53;
        double _MCCF54; double _MCCF55; double _MCCF56; double _MCCF57; double _MCCF58; double _MCCF59;
        double _MCCF60; double _MCCF61; double _MCCF62; double _MCCF63; double _MCCF64; double _MCCF65;
        double _MCCF66; double _MCCF67; double _MCCF68; double _MCCF69; double _MCCF70; double _MCCF71;
        double _MCCF72; double _MCCF73; double _MCCF74; double _MCCF75; double _MCCF76; double _MCCF77;
        double _MCCF78; double _MCCF79; double _MCCF80; double _MCCF81; double _MCCF82; double _MCCF83;
        double _MCCF84; double _MCCF85; double _MCCF86; double _MCCF87; double _MCCF88; double _MCCF89;
        double _MCCF90;
        
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


        public string OBEType {
            get { return _OBEType; }
            set {
                if (_OBEType != value) {
                    _OBEType = value;
                    OnPropertyChanged(() => OBEType);
                }
            }
        }

        public string COL {
            get { return _COL; }
            set {
                if (_COL != value) {
                    _COL = value;
                    OnPropertyChanged(() => COL);
                }
            }
        }



        public double MCCF0 {
            get { return _MCCF0; }
            set {
                if (_MCCF0 != value) {
                    _MCCF0 = value;
                    OnPropertyChanged(() => MCCF0);
                }
            }
        }
        public double MCCF1 {
            get { return _MCCF1; }
            set {
                if (_MCCF1 != value) {
                    _MCCF1 = value;
                    OnPropertyChanged(() => MCCF1);
                }
            }
        }
        public double MCCF2 {
            get { return _MCCF2; }
            set {
                if (_MCCF2 != value) {
                    _MCCF2 = value;
                    OnPropertyChanged(() => MCCF2);
                }
            }
        }
        public double MCCF3 {
            get { return _MCCF3; }
            set {
                if (_MCCF3 != value) {
                    _MCCF3 = value;
                    OnPropertyChanged(() => MCCF3);
                }
            }
        }
        public double MCCF4 {
            get { return _MCCF4; }
            set {
                if (_MCCF4 != value) {
                    _MCCF4 = value;
                    OnPropertyChanged(() => MCCF4);
                }
            }
        }
        public double MCCF5 {
            get { return _MCCF5; }
            set {
                if (_MCCF5 != value) {
                    _MCCF5 = value;
                    OnPropertyChanged(() => MCCF5);
                }
            }
        }
        public double MCCF6 {
            get { return _MCCF6; }
            set {
                if (_MCCF6 != value) {
                    _MCCF6 = value;
                    OnPropertyChanged(() => MCCF6);
                }
            }
        }
        public double MCCF7 {
            get { return _MCCF7; }
            set {
                if (_MCCF7 != value) {
                    _MCCF7 = value;
                    OnPropertyChanged(() => MCCF7);
                }
            }
        }
        public double MCCF8 {
            get { return _MCCF8; }
            set {
                if (_MCCF8 != value) {
                    _MCCF8 = value;
                    OnPropertyChanged(() => MCCF8);
                }
            }
        }
        public double MCCF9 {
            get { return _MCCF9; }
            set {
                if (_MCCF9 != value) {
                    _MCCF9 = value;
                    OnPropertyChanged(() => MCCF9);
                }
            }
        }
        public double MCCF10 {
            get { return _MCCF10; }
            set {
                if (_MCCF10 != value) {
                    _MCCF10 = value;
                    OnPropertyChanged(() => MCCF10);
                }
            }
        }
        public double MCCF11 {
            get { return _MCCF11; }
            set {
                if (_MCCF11 != value) {
                    _MCCF11 = value;
                    OnPropertyChanged(() => MCCF11);
                }
            }
        }
        public double MCCF12 {
            get { return _MCCF12; }
            set {
                if (_MCCF12 != value) {
                    _MCCF12 = value;
                    OnPropertyChanged(() => MCCF12);
                }
            }
        }
        public double MCCF13 {
            get { return _MCCF13; }
            set {
                if (_MCCF13 != value) {
                    _MCCF13 = value;
                    OnPropertyChanged(() => MCCF13);
                }
            }
        }
        public double MCCF14 {
            get { return _MCCF14; }
            set {
                if (_MCCF14 != value) {
                    _MCCF14 = value;
                    OnPropertyChanged(() => MCCF14);
                }
            }
        }
        public double MCCF15 {
            get { return _MCCF15; }
            set {
                if (_MCCF15 != value) {
                    _MCCF15 = value;
                    OnPropertyChanged(() => MCCF15);
                }
            }
        }
        public double MCCF16 {
            get { return _MCCF16; }
            set {
                if (_MCCF16 != value) {
                    _MCCF16 = value;
                    OnPropertyChanged(() => MCCF16);
                }
            }
        }
        public double MCCF17 {
            get { return _MCCF17; }
            set {
                if (_MCCF17 != value) {
                    _MCCF17 = value;
                    OnPropertyChanged(() => MCCF17);
                }
            }
        }
        public double MCCF18 {
            get { return _MCCF18; }
            set {
                if (_MCCF18 != value) {
                    _MCCF18 = value;
                    OnPropertyChanged(() => MCCF18);
                }
            }
        }
        public double MCCF19 {
            get { return _MCCF19; }
            set {
                if (_MCCF19 != value) {
                    _MCCF19 = value;
                    OnPropertyChanged(() => MCCF19);
                }
            }
        }
        public double MCCF20 {
            get { return _MCCF20; }
            set {
                if (_MCCF20 != value) {
                    _MCCF20 = value;
                    OnPropertyChanged(() => MCCF20);
                }
            }
        }
        public double MCCF21 {
            get { return _MCCF21; }
            set {
                if (_MCCF21 != value) {
                    _MCCF21 = value;
                    OnPropertyChanged(() => MCCF21);
                }
            }
        }
        public double MCCF22 {
            get { return _MCCF22; }
            set {
                if (_MCCF22 != value) {
                    _MCCF22 = value;
                    OnPropertyChanged(() => MCCF22);
                }
            }
        }
        public double MCCF23 {
            get { return _MCCF23; }
            set {
                if (_MCCF23 != value) {
                    _MCCF23 = value;
                    OnPropertyChanged(() => MCCF23);
                }
            }
        }
        public double MCCF24 {
            get { return _MCCF24; }
            set {
                if (_MCCF24 != value) {
                    _MCCF24 = value;
                    OnPropertyChanged(() => MCCF24);
                }
            }
        }
        public double MCCF25 {
            get { return _MCCF25; }
            set {
                if (_MCCF25 != value) {
                    _MCCF25 = value;
                    OnPropertyChanged(() => MCCF25);
                }
            }
        }
        public double MCCF26 {
            get { return _MCCF26; }
            set {
                if (_MCCF26 != value) {
                    _MCCF26 = value;
                    OnPropertyChanged(() => MCCF26);
                }
            }
        }
        public double MCCF27 {
            get { return _MCCF27; }
            set {
                if (_MCCF27 != value) {
                    _MCCF27 = value;
                    OnPropertyChanged(() => MCCF27);
                }
            }
        }
        public double MCCF28 {
            get { return _MCCF28; }
            set {
                if (_MCCF28 != value) {
                    _MCCF28 = value;
                    OnPropertyChanged(() => MCCF28);
                }
            }
        }
        public double MCCF29 {
            get { return _MCCF29; }
            set {
                if (_MCCF29 != value) {
                    _MCCF29 = value;
                    OnPropertyChanged(() => MCCF29);
                }
            }
        }
        public double MCCF30 {
            get { return _MCCF30; }
            set {
                if (_MCCF30 != value) {
                    _MCCF30 = value;
                    OnPropertyChanged(() => MCCF30);
                }
            }
        }
        public double MCCF31 {
            get { return _MCCF31; }
            set {
                if (_MCCF31 != value) {
                    _MCCF31 = value;
                    OnPropertyChanged(() => MCCF31);
                }
            }
        }
        public double MCCF32 {
            get { return _MCCF32; }
            set {
                if (_MCCF32 != value) {
                    _MCCF32 = value;
                    OnPropertyChanged(() => MCCF32);
                }
            }
        }
        public double MCCF33 {
            get { return _MCCF33; }
            set {
                if (_MCCF33 != value) {
                    _MCCF33 = value;
                    OnPropertyChanged(() => MCCF33);
                }
            }
        }
        public double MCCF34 {
            get { return _MCCF34; }
            set {
                if (_MCCF34 != value) {
                    _MCCF34 = value;
                    OnPropertyChanged(() => MCCF34);
                }
            }
        }
        public double MCCF35 {
            get { return _MCCF35; }
            set {
                if (_MCCF35 != value) {
                    _MCCF35 = value;
                    OnPropertyChanged(() => MCCF35);
                }
            }
        }
        public double MCCF36 {
            get { return _MCCF36; }
            set {
                if (_MCCF36 != value) {
                    _MCCF36 = value;
                    OnPropertyChanged(() => MCCF36);
                }
            }
        }
        public double MCCF37 {
            get { return _MCCF37; }
            set {
                if (_MCCF37 != value) {
                    _MCCF37 = value;
                    OnPropertyChanged(() => MCCF37);
                }
            }
        }
        public double MCCF38 {
            get { return _MCCF38; }
            set {
                if (_MCCF38 != value) {
                    _MCCF38 = value;
                    OnPropertyChanged(() => MCCF38);
                }
            }
        }
        public double MCCF39 {
            get { return _MCCF39; }
            set {
                if (_MCCF39 != value) {
                    _MCCF39 = value;
                    OnPropertyChanged(() => MCCF39);
                }
            }
        }
        public double MCCF40 {
            get { return _MCCF40; }
            set {
                if (_MCCF40 != value) {
                    _MCCF40 = value;
                    OnPropertyChanged(() => MCCF40);
                }
            }
        }
        public double MCCF41 {
            get { return _MCCF41; }
            set {
                if (_MCCF41 != value) {
                    _MCCF41 = value;
                    OnPropertyChanged(() => MCCF41);
                }
            }
        }
        public double MCCF42 {
            get { return _MCCF42; }
            set {
                if (_MCCF42 != value) {
                    _MCCF42 = value;
                    OnPropertyChanged(() => MCCF42);
                }
            }
        }
        public double MCCF43 {
            get { return _MCCF43; }
            set {
                if (_MCCF43 != value) {
                    _MCCF43 = value;
                    OnPropertyChanged(() => MCCF43);
                }
            }
        }
        public double MCCF44 {
            get { return _MCCF44; }
            set {
                if (_MCCF44 != value) {
                    _MCCF44 = value;
                    OnPropertyChanged(() => MCCF44);
                }
            }
        }
        public double MCCF45 {
            get { return _MCCF45; }
            set {
                if (_MCCF45 != value) {
                    _MCCF45 = value;
                    OnPropertyChanged(() => MCCF45);
                }
            }
        }
        public double MCCF46 {
            get { return _MCCF46; }
            set {
                if (_MCCF46 != value) {
                    _MCCF46 = value;
                    OnPropertyChanged(() => MCCF46);
                }
            }
        }
        public double MCCF47 {
            get { return _MCCF47; }
            set {
                if (_MCCF47 != value) {
                    _MCCF47 = value;
                    OnPropertyChanged(() => MCCF47);
                }
            }
        }
        public double MCCF48 {
            get { return _MCCF48; }
            set {
                if (_MCCF48 != value) {
                    _MCCF48 = value;
                    OnPropertyChanged(() => MCCF48);
                }
            }
        }
        public double MCCF49 {
            get { return _MCCF49; }
            set {
                if (_MCCF49 != value) {
                    _MCCF49 = value;
                    OnPropertyChanged(() => MCCF49);
                }
            }
        }
        public double MCCF50 {
            get { return _MCCF50; }
            set {
                if (_MCCF50 != value) {
                    _MCCF50 = value;
                    OnPropertyChanged(() => MCCF50);
                }
            }
        }
        public double MCCF51 {
            get { return _MCCF51; }
            set {
                if (_MCCF51 != value) {
                    _MCCF51 = value;
                    OnPropertyChanged(() => MCCF51);
                }
            }
        }
        public double MCCF52 {
            get { return _MCCF52; }
            set {
                if (_MCCF52 != value) {
                    _MCCF52 = value;
                    OnPropertyChanged(() => MCCF52);
                }
            }
        }
        public double MCCF53 {
            get { return _MCCF53; }
            set {
                if (_MCCF53 != value) {
                    _MCCF53 = value;
                    OnPropertyChanged(() => MCCF53);
                }
            }
        }
        public double MCCF54 {
            get { return _MCCF54; }
            set {
                if (_MCCF54 != value) {
                    _MCCF54 = value;
                    OnPropertyChanged(() => MCCF54);
                }
            }
        }
        public double MCCF55 {
            get { return _MCCF55; }
            set {
                if (_MCCF55 != value) {
                    _MCCF55 = value;
                    OnPropertyChanged(() => MCCF55);
                }
            }
        }
        public double MCCF56 {
            get { return _MCCF56; }
            set {
                if (_MCCF56 != value) {
                    _MCCF56 = value;
                    OnPropertyChanged(() => MCCF56);
                }
            }
        }
        public double MCCF57 {
            get { return _MCCF57; }
            set {
                if (_MCCF57 != value) {
                    _MCCF57 = value;
                    OnPropertyChanged(() => MCCF57);
                }
            }
        }
        public double MCCF58 {
            get { return _MCCF58; }
            set {
                if (_MCCF58 != value) {
                    _MCCF58 = value;
                    OnPropertyChanged(() => MCCF58);
                }
            }
        }
        public double MCCF59 {
            get { return _MCCF59; }
            set {
                if (_MCCF59 != value) {
                    _MCCF59 = value;
                    OnPropertyChanged(() => MCCF59);
                }
            }
        }
        public double MCCF60 {
            get { return _MCCF60; }
            set {
                if (_MCCF60 != value) {
                    _MCCF60 = value;
                    OnPropertyChanged(() => MCCF60);
                }
            }
        }
        public double MCCF61 {
            get { return _MCCF61; }
            set {
                if (_MCCF61 != value) {
                    _MCCF61 = value;
                    OnPropertyChanged(() => MCCF61);
                }
            }
        }
        public double MCCF62 {
            get { return _MCCF62; }
            set {
                if (_MCCF62 != value) {
                    _MCCF62 = value;
                    OnPropertyChanged(() => MCCF62);
                }
            }
        }
        public double MCCF63 {
            get { return _MCCF63; }
            set {
                if (_MCCF63 != value) {
                    _MCCF63 = value;
                    OnPropertyChanged(() => MCCF63);
                }
            }
        }
        public double MCCF64 {
            get { return _MCCF64; }
            set {
                if (_MCCF64 != value) {
                    _MCCF64 = value;
                    OnPropertyChanged(() => MCCF64);
                }
            }
        }
        public double MCCF65 {
            get { return _MCCF65; }
            set {
                if (_MCCF65 != value) {
                    _MCCF65 = value;
                    OnPropertyChanged(() => MCCF65);
                }
            }
        }
        public double MCCF66 {
            get { return _MCCF66; }
            set {
                if (_MCCF66 != value) {
                    _MCCF66 = value;
                    OnPropertyChanged(() => MCCF66);
                }
            }
        }
        public double MCCF67 {
            get { return _MCCF67; }
            set {
                if (_MCCF67 != value) {
                    _MCCF67 = value;
                    OnPropertyChanged(() => MCCF67);
                }
            }
        }
        public double MCCF68 {
            get { return _MCCF68; }
            set {
                if (_MCCF68 != value) {
                    _MCCF68 = value;
                    OnPropertyChanged(() => MCCF68);
                }
            }
        }
        public double MCCF69 {
            get { return _MCCF69; }
            set {
                if (_MCCF69 != value) {
                    _MCCF69 = value;
                    OnPropertyChanged(() => MCCF69);
                }
            }
        }
        public double MCCF70 {
            get { return _MCCF70; }
            set {
                if (_MCCF70 != value) {
                    _MCCF70 = value;
                    OnPropertyChanged(() => MCCF70);
                }
            }
        }
        public double MCCF71 {
            get { return _MCCF71; }
            set {
                if (_MCCF71 != value) {
                    _MCCF71 = value;
                    OnPropertyChanged(() => MCCF71);
                }
            }
        }
        public double MCCF72 {
            get { return _MCCF72; }
            set {
                if (_MCCF72 != value) {
                    _MCCF72 = value;
                    OnPropertyChanged(() => MCCF72);
                }
            }
        }
        public double MCCF73 {
            get { return _MCCF73; }
            set {
                if (_MCCF73 != value) {
                    _MCCF73 = value;
                    OnPropertyChanged(() => MCCF73);
                }
            }
        }
        public double MCCF74 {
            get { return _MCCF74; }
            set {
                if (_MCCF74 != value) {
                    _MCCF74 = value;
                    OnPropertyChanged(() => MCCF74);
                }
            }
        }
        public double MCCF75 {
            get { return _MCCF75; }
            set {
                if (_MCCF75 != value) {
                    _MCCF75 = value;
                    OnPropertyChanged(() => MCCF75);
                }
            }
        }
        public double MCCF76 {
            get { return _MCCF76; }
            set {
                if (_MCCF76 != value) {
                    _MCCF76 = value;
                    OnPropertyChanged(() => MCCF76);
                }
            }
        }
        public double MCCF77 {
            get { return _MCCF77; }
            set {
                if (_MCCF77 != value) {
                    _MCCF77 = value;
                    OnPropertyChanged(() => MCCF77);
                }
            }
        }
        public double MCCF78 {
            get { return _MCCF78; }
            set {
                if (_MCCF78 != value) {
                    _MCCF78 = value;
                    OnPropertyChanged(() => MCCF78);
                }
            }
        }
        public double MCCF79 {
            get { return _MCCF79; }
            set {
                if (_MCCF79 != value) {
                    _MCCF79 = value;
                    OnPropertyChanged(() => MCCF79);
                }
            }
        }
        public double MCCF80 {
            get { return _MCCF80; }
            set {
                if (_MCCF80 != value) {
                    _MCCF80 = value;
                    OnPropertyChanged(() => MCCF80);
                }
            }
        }
        public double MCCF81 {
            get { return _MCCF81; }
            set {
                if (_MCCF81 != value) {
                    _MCCF81 = value;
                    OnPropertyChanged(() => MCCF81);
                }
            }
        }
        public double MCCF82 {
            get { return _MCCF82; }
            set {
                if (_MCCF82 != value) {
                    _MCCF82 = value;
                    OnPropertyChanged(() => MCCF82);
                }
            }
        }
        public double MCCF83 {
            get { return _MCCF83; }
            set {
                if (_MCCF83 != value) {
                    _MCCF83 = value;
                    OnPropertyChanged(() => MCCF83);
                }
            }
        }
        public double MCCF84 {
            get { return _MCCF84; }
            set {
                if (_MCCF84 != value) {
                    _MCCF84 = value;
                    OnPropertyChanged(() => MCCF84);
                }
            }
        }
        public double MCCF85 {
            get { return _MCCF85; }
            set {
                if (_MCCF85 != value) {
                    _MCCF85 = value;
                    OnPropertyChanged(() => MCCF85);
                }
            }
        }
        public double MCCF86 {
            get { return _MCCF86; }
            set {
                if (_MCCF86 != value) {
                    _MCCF86 = value;
                    OnPropertyChanged(() => MCCF86);
                }
            }
        }
        public double MCCF87 {
            get { return _MCCF87; }
            set {
                if (_MCCF87 != value) {
                    _MCCF87 = value;
                    OnPropertyChanged(() => MCCF87);
                }
            }
        }
        public double MCCF88 {
            get { return _MCCF88; }
            set {
                if (_MCCF88 != value) {
                    _MCCF88 = value;
                    OnPropertyChanged(() => MCCF88);
                }
            }
        }
        public double MCCF89 {
            get { return _MCCF89; }
            set {
                if (_MCCF89 != value) {
                    _MCCF89 = value;
                    OnPropertyChanged(() => MCCF89);
                }
            }
        }
        public double MCCF90 {
            get { return _MCCF90; }
            set {
                if (_MCCF90 != value) {
                    _MCCF90 = value;
                    OnPropertyChanged(() => MCCF90);
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


        class MarginalCCFPivotSTRLBValidator : AbstractValidator<MarginalCCFPivotSTRLB>
        {
            public MarginalCCFPivotSTRLBValidator()
            {
                //RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");        
            }
        }

        protected override IValidator GetValidator()
        {
            return new MarginalCCFPivotSTRLBValidator();
        }
    }
}
