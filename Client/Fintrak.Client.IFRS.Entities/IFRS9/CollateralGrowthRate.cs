using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class CollateralGrowthRate : ObjectBase
    {
        int _ID;
        int _seq;
        string _TypeCode;
        double _Inflation;
        double _Depreciation;
        DateTime _date_pmt;
        double _GrowthRate;

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

        public int seq {
            get { return _seq; }
            set {
                if (_seq != value) {
                    _seq = value;
                    OnPropertyChanged(() => seq);
                }
            }
        }

        public string TypeCode {
            get { return _TypeCode; }
            set {
                if (_TypeCode != value) {
                    _TypeCode = value;
                    OnPropertyChanged(() => TypeCode);
                }
            }
        }

        public double Inflation {
            get { return _Inflation; }
            set {
                if (_Inflation != value) {
                    _Inflation = value;
                    OnPropertyChanged(() => Inflation);
                }
            }
        }

        public double Depreciation {
            get { return _Depreciation; }
            set {
                if (_Depreciation != value) {
                    _Depreciation = value;
                    OnPropertyChanged(() => Depreciation);
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

        public double GrowthRate {
            get { return _GrowthRate; }
            set {
                if (_GrowthRate != value) {
                    _GrowthRate = value;
                    OnPropertyChanged(() => GrowthRate);
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


        class CollateralGrowthRateValidator : AbstractValidator<CollateralGrowthRate>
        {
            public CollateralGrowthRateValidator()
            {
                //RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");        
            }
        }

        protected override IValidator GetValidator()
        {
            return new CollateralGrowthRateValidator();
        }
    }
}
