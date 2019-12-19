using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class CollateralRecAmtStaging : ObjectBase
    {
        int _ID;
        string _Refno;
        string _CustomerName;
        DateTime _date_pmt;
        DateTime _MaturityDate;
        string _ProductType;
        string _SubType;
        string _CollateralDescription;
        string _CollateralType;
        double _ColAmt1;
        double _ColAmt2;
        double _ColAmt3;
        double _ColAmt4;
        string _MappedColType1;
        string _MappedColType2;
        string _MappedColType3;
        string _MappedColType4;
        double _ColRecAmt1;
        double _ColRecAmt2;
        double _ColRecAmt3;
        double _ColRecAmt4;
        double _growthrate1;
        double _growthrate2;
        double _growthrate3;
        double _growthrate4;
        double _finCol1;
        double _finCol2;
        double _finCol3;
        double _finCol4;
        double _TotalColRecAmt;
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

        public string CustomerName {
            get { return _CustomerName; }
            set {
                if (_CustomerName != value) {
                    _CustomerName = value;
                    OnPropertyChanged(() => CustomerName);
                }
            }
        }

        public DateTime date_pmt {
            get { return _date_pmt; }
            set {
                if (_date_pmt != value) {
                    _date_pmt = value;
                    OnPropertyChanged(() => date_pmt);
                }
            }
        }

        public DateTime MaturityDate {
            get { return _MaturityDate; }
            set {
                if (_MaturityDate != value) {
                    _MaturityDate = value;
                    OnPropertyChanged(() => MaturityDate);
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


        public string SubType {
            get { return _SubType; }
            set {
                if (_SubType != value) {
                    _SubType = value;
                    OnPropertyChanged(() => SubType);
                }
            }
        }

        public string CollateralDescription {
            get { return _CollateralDescription; }
            set {
                if (_CollateralDescription != value) {
                    _CollateralDescription = value;
                    OnPropertyChanged(() => CollateralDescription);
                }
            }
        }

        public string CollateralType {
            get { return _CollateralType; }
            set {
                if (_CollateralType != value) {
                    _CollateralType = value;
                    OnPropertyChanged(() => CollateralType);
                }
            }
        }

        public double ColAmt1 {
            get { return _ColAmt1; }
            set {
                if (_ColAmt1 != value) {
                    _ColAmt1 = value;
                    OnPropertyChanged(() => ColAmt1);
                }
            }
        }

        public double ColAmt2 {
            get { return _ColAmt2; }
            set {
                if (_ColAmt2 != value) {
                    _ColAmt2 = value;
                    OnPropertyChanged(() => ColAmt2);
                }
            }
        }

        public double ColAmt3 {
            get { return _ColAmt3; }
            set {
                if (_ColAmt3 != value) {
                    _ColAmt3 = value;
                    OnPropertyChanged(() => ColAmt3);
                }
            }
        }

        public double ColAmt4 {
            get { return _ColAmt4; }
            set {
                if (_ColAmt4 != value) {
                    _ColAmt4 = value;
                    OnPropertyChanged(() => ColAmt4);
                }
            }
        }

        public string MappedColType1 {
            get { return _MappedColType1; }
            set {
                if (_MappedColType1 != value) {
                    _MappedColType1 = value;
                    OnPropertyChanged(() => MappedColType1);
                }
            }
        }

        public string MappedColType2 {
            get { return _MappedColType2; }
            set {
                if (_MappedColType2 != value) {
                    _MappedColType2 = value;
                    OnPropertyChanged(() => MappedColType2);
                }
            }
        }
        public string MappedColType3 {
            get { return _MappedColType3; }
            set {
                if (_MappedColType3 != value) {
                    _MappedColType3 = value;
                    OnPropertyChanged(() => MappedColType3);
                }
            }
        }

        public string MappedColType4 {
            get { return _MappedColType4; }
            set {
                if (_MappedColType4 != value) {
                    _MappedColType4 = value;
                    OnPropertyChanged(() => MappedColType4);
                }
            }
        }

        public double ColRecAmt1 {
            get { return _ColRecAmt1; }
            set {
                if (_ColRecAmt1 != value) {
                    _ColRecAmt1 = value;
                    OnPropertyChanged(() => ColRecAmt1);
                }
            }
        }

        public double ColRecAmt2 {
            get { return _ColRecAmt2; }
            set {
                if (_ColRecAmt2 != value) {
                    _ColRecAmt2 = value;
                    OnPropertyChanged(() => ColRecAmt2);
                }
            }
        }

        public double ColRecAmt3 {
            get { return _ColRecAmt3; }
            set {
                if (_ColRecAmt3 != value) {
                    _ColRecAmt3 = value;
                    OnPropertyChanged(() => ColRecAmt3);
                }
            }
        }

        public double ColRecAmt4 {
            get { return _ColRecAmt4; }
            set {
                if (_ColRecAmt4 != value) {
                    _ColRecAmt4 = value;
                    OnPropertyChanged(() => ColRecAmt4);
                }
            }
        }

        public double growthrate1 {
            get { return _growthrate1; }
            set {
                if (_growthrate1 != value) {
                    _growthrate1 = value;
                    OnPropertyChanged(() => growthrate1);
                }
            }
        }

        public double growthrate2 {
            get { return _growthrate2; }
            set {
                if (_growthrate2 != value) {
                    _growthrate2 = value;
                    OnPropertyChanged(() => growthrate2);
                }
            }
        }

        public double growthrate3 {
            get { return _growthrate3; }
            set {
                if (_growthrate3 != value) {
                    _growthrate3 = value;
                    OnPropertyChanged(() => growthrate3);
                }
            }
        }

        public double growthrate4 {
            get { return _growthrate4; }
            set {
                if (_growthrate4 != value) {
                    _growthrate4 = value;
                    OnPropertyChanged(() => growthrate4);
                }
            }
        }

        public double finCol1 {
            get { return _finCol1; }
            set {
                if (_finCol1 != value) {
                    _finCol1 = value;
                    OnPropertyChanged(() => finCol1);
                }
            }
        }
        public double finCol2 {
            get { return _finCol2; }
            set {
                if (_finCol2 != value) {
                    _finCol2 = value;
                    OnPropertyChanged(() => finCol2);
                }
            }
        }
        public double finCol3 {
            get { return _finCol3; }
            set {
                if (_finCol3 != value) {
                    _finCol3 = value;
                    OnPropertyChanged(() => finCol3);
                }
            }
        }
        public double finCol4 {
            get { return _finCol4; }
            set {
                if (_finCol4 != value) {
                    _finCol4 = value;
                    OnPropertyChanged(() => finCol4);
                }
            }
        }

        public double TotalColRecAmt {
            get { return _TotalColRecAmt; }
            set {
                if (_TotalColRecAmt != value) {
                    _TotalColRecAmt = value;
                    OnPropertyChanged(() => TotalColRecAmt);
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


        class CollateralRecAmtStagingValidator : AbstractValidator<CollateralRecAmtStaging>
        {
            public CollateralRecAmtStagingValidator()
            {
                //RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");        
            }
        }

        protected override IValidator GetValidator()
        {
            return new CollateralRecAmtStagingValidator();
        }
    }
}
