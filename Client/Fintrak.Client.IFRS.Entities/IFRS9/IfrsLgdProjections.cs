using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsLgdProjections : ObjectBase
    {

        int _ID;
        string _AccountNo;
        string _Refno;
        string _ProductType;
        string _Scenerio;
        DateTime _Processdate;
        double _EAD;
        double _CollateralForecast;
        string _Stage;
        double _CureRate;
        double _RecoveryRate;
        double _LGDProjection;

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

        public string AccountNo
        {
            get { return _AccountNo; }
            set
            {
                if (_AccountNo != value)
                {
                    _AccountNo = value;
                    OnPropertyChanged(() => AccountNo);
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


        public string Scenerio {
            get { return _Scenerio; }
            set {
                if (_Scenerio != value) {
                    _Scenerio = value;
                    OnPropertyChanged(() => Scenerio);
                }
            }
        }


        public DateTime Processdate
        {
            get { return _Processdate; }
            set
            {
                if (_Processdate != value)
                {
                    _Processdate = value;
                    OnPropertyChanged(() => Processdate);
                }
            }
        }



        public double  EAD
        {
            get { return _EAD; }
            set
            {
                if (_EAD != value)
                {
                    _EAD = value;
                    OnPropertyChanged(() => EAD);
                }
            }
        }



        public string Stage
        {
            get { return _Stage; }
            set
            {
                if (_Stage != value)
                {
                    _Stage = value;
                    OnPropertyChanged(() => Stage);
                }
            }
        }

        public double RecoveryRate
        {
            get { return _RecoveryRate; }
            set
            {
                if (_RecoveryRate != value)
                {
                    _RecoveryRate = value;
                    OnPropertyChanged(() => RecoveryRate);
                }
            }
        }



        public double CureRate
        {
            get { return _CureRate; }
            set
            {
                if (_CureRate != value)
                {
                    _CureRate = value;
                    OnPropertyChanged(() => CureRate);
                }
            }
        }

      



        public double CollateralForecast
        {
            get { return _CollateralForecast; }
            set
            {
                if (_CollateralForecast != value)
                {
                    _CollateralForecast = value;
                    OnPropertyChanged(() => CollateralForecast);
                }
            }
        }



        public double LGDProjection
        {
            get { return _LGDProjection; }
            set
            {
                if (_LGDProjection != value)
                {
                    _LGDProjection = value;
                    OnPropertyChanged(() => LGDProjection);
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
        class IfrsLgdProjectionsValidator : AbstractValidator<IfrsLgdProjections>
        {
            public IfrsLgdProjectionsValidator(){               
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsLgdProjectionsValidator();
        }

    }
}
