using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class CollateralDetails : ObjectBase
    {
        public int _ID;
        public double? _YOO;
        public string _AccountNo;
        public string _CustomerName;
        public DateTime _MaturityDate;
        public string _HC1;
        public string _HC2;
        public string _ColType1;
        public string _Col_Qualifier1;
        public string _Perfection_Status1;
        public double? _ColAmt1;
        public string _ColType2;
        public string _Col_Qualifier2;
        public string _Perfection_Status2;
        public double? _ColAmt2;
        public string _ColType3;
        public string _Col_Qualifier3;
        public string _Perfection_Status3;
        public double? _ColAmt3;
        public string _ColType4;
        public string _Col_Qualifier4;
        public string _Perfection_Status4;
        public double? _ColAmt4;
        public DateTime _DateLoaded;
        public bool _Active;



        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }
        public double? YOO
        {
            get { return _YOO; }
            set
            {
                if (_YOO != value)
                {
                    _YOO = value;
                    OnPropertyChanged(() => YOO);
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
        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    OnPropertyChanged(() => CustomerName);
                }
            }
        }
        public DateTime MaturityDate
        {
            get { return _MaturityDate; }
            set
            {
                if (_MaturityDate != value)
                {
                    _MaturityDate = value;
                    OnPropertyChanged(() => MaturityDate);
                }
            }
        }
        public string HC1
        {
            get { return _HC1; }
            set
            {
                if (_HC1 != value)
                {
                    _HC1 = value;
                    OnPropertyChanged(() => HC1);
                }
            }
        }
        public string HC2
        {
            get { return _HC2; }
            set
            {
                if (_HC2 != value)
                {
                    _HC2 = value;
                    OnPropertyChanged(() => HC2);
                }
            }
        }
        public string ColType1
        {
            get { return _ColType1; }
            set
            {
                if (_ColType1 != value)
                {
                    _ColType1 = value;
                    OnPropertyChanged(() => ColType1);
                }
            }
        }
        public string Col_Qualifier1
        {
            get { return _Col_Qualifier1; }
            set
            {
                if (_Col_Qualifier1 != value)
                {
                    _Col_Qualifier1 = value;
                    OnPropertyChanged(() => Col_Qualifier1);
                }
            }
        }
        public string Perfection_Status1
        {
            get { return _Perfection_Status1; }
            set
            {
                if (_Perfection_Status1 != value)
                {
                    _Perfection_Status1 = value;
                    OnPropertyChanged(() => Perfection_Status1);
                }
            }
        }
        public double? ColAmt1
        {
            get { return _ColAmt1; }
            set
            {
                if (_ColAmt1 != value)
                {
                    _ColAmt1 = value;
                    OnPropertyChanged(() => ColAmt1);
                }
            }
        }
        public string ColType2
        {
            get { return _ColType2; }
            set
            {
                if (_ColType2 != value)
                {
                    _ColType2 = value;
                    OnPropertyChanged(() => ColType2);
                }
            }
        }
        public string Col_Qualifier2
        {
            get { return _Col_Qualifier2; }
            set
            {
                if (_Col_Qualifier2 != value)
                {
                    _Col_Qualifier2 = value;
                    OnPropertyChanged(() => Col_Qualifier2);
                }
            }
        }
        public string Perfection_Status2
        {
            get { return _Perfection_Status2; }
            set
            {
                if (_Perfection_Status2 != value)
                {
                    _Perfection_Status2 = value;
                    OnPropertyChanged(() => Perfection_Status2);
                }
            }
        }
        public double? ColAmt2
        {
            get { return _ColAmt2; }
            set
            {
                if (_ColAmt2 != value)
                {
                    _ColAmt2 = value;
                    OnPropertyChanged(() => ColAmt2);
                }
            }
        }
        public string ColType3
        {
            get { return _ColType3; }
            set
            {
                if (_ColType3 != value)
                {
                    _ColType3 = value;
                    OnPropertyChanged(() => ColType3);
                }
            }
        }
        public string Col_Qualifier3
        {
            get { return _Col_Qualifier3; }
            set
            {
                if (_Col_Qualifier3 != value)
                {
                    _Col_Qualifier3 = value;
                    OnPropertyChanged(() => Col_Qualifier3);
                }
            }
        }
        public string Perfection_Status3
        {
            get { return _Perfection_Status3; }
            set
            {
                if (_Perfection_Status3 != value)
                {
                    _Perfection_Status3 = value;
                    OnPropertyChanged(() => Perfection_Status3);
                }
            }
        }
        public double? ColAmt3
        {
            get { return _ColAmt3; }
            set
            {
                if (_ColAmt3 != value)
                {
                    _ColAmt3 = value;
                    OnPropertyChanged(() => ColAmt3);
                }
            }
        }
        public string ColType4
        {
            get { return _ColType4; }
            set
            {
                if (_ColType4 != value)
                {
                    _ColType4 = value;
                    OnPropertyChanged(() => ColType4);
                }
            }
        }
        public string Col_Qualifier4
        {
            get { return _Col_Qualifier4; }
            set
            {
                if (_Col_Qualifier4 != value)
                {
                    _Col_Qualifier4 = value;
                    OnPropertyChanged(() => Col_Qualifier4);
                }
            }
        }
        public string Perfection_Status4
        {
            get { return _Perfection_Status4; }
            set
            {
                if (_Perfection_Status4 != value)
                {
                    _Perfection_Status4 = value;
                    OnPropertyChanged(() => Perfection_Status4);
                }
            }
        }
        public double? ColAmt4
        {
            get { return _ColAmt4; }
            set
            {
                if (_ColAmt4 != value)
                {
                    _ColAmt4 = value;
                    OnPropertyChanged(() => ColAmt4);
                }
            }
        }
        public DateTime DateLoaded
        {
            get { return _DateLoaded; }
            set
            {
                if (_DateLoaded != value)
                {
                    _DateLoaded = value;
                    OnPropertyChanged(() => DateLoaded);
                }
            }
        }





        public bool Active
        {
            get { return _Active; }
            set
            {
                if (_Active != value)
                {
                    _Active = value;
                    OnPropertyChanged(() => Active);
                }
            }
        }


        class CollateralDetailsValidator : AbstractValidator<CollateralDetails>
        {
            public CollateralDetailsValidator()
            {
                //RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new CollateralDetailsValidator();
        }
    }
}