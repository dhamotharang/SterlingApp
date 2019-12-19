using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsPDProjection : ObjectBase
    {

        int _ID;
        string _AccountNo;
        string _Refno;
        string _ProductName;
        string _ProductType;
        string _Sector;
        string _Scenerio;
        double _EAD;
        double _EIR;
        DateTime _date_pmt;
        string _Stage;
        double _PD;

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

        public string ProductName {
            get { return _ProductName; }
            set {
                if (_ProductName != value) {
                    _ProductName = value;
                    OnPropertyChanged(() => ProductName);
                }
            }
        }


        public string ProductType
        {
            get { return _ProductType; }
            set
            {
                if (_ProductType != value)
                {
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




        public string Sector
        {
            get { return _Sector; }
            set
            {
                if (_Sector != value)
                {
                    _Sector = value;
                    OnPropertyChanged(() => Sector);
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


        public double EIR
        {
            get { return _EIR; }
            set
            {
                if (_EIR != value)
                {
                    _EIR = value;
                    OnPropertyChanged(() => EIR);
                }
            }
        }




        public double PD
        {
            get { return _PD; }
            set
            {
                if (_PD != value)
                {
                    _PD = value;
                    OnPropertyChanged(() => PD);
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



        public DateTime date_pmt
        {
            get { return _date_pmt; }
            set
            {
                if (_date_pmt != value)
                {
                    _date_pmt = value;
                    OnPropertyChanged(() => date_pmt);
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
        class IfrsPDProjectionValidator : AbstractValidator<IfrsPDProjection>
        {
            public IfrsPDProjectionValidator(){               
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsPDProjectionValidator();
        }

    }
}
