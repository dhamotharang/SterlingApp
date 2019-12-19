using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class FacilityClassification : ObjectBase
    {
        int _Id;
        string _Refno;
        string _AccountNo;
        string _ProductType;
        string _SubType;
        string _FacilityType;
        string _Sector;
        DateTime _RunDate;
        bool _Active;

        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(() => Id);
                }
            }
        }

        public string Refno
        {
            get { return _Refno; }
            set
            {
                if (_Refno != value)
                {
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

        public string SubType
        {
            get { return _SubType; }
            set
            {
                if (_SubType != value)
                {
                    _SubType = value;
                    OnPropertyChanged(() => SubType);
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
        public string FacilityType
        {
            get { return _FacilityType; }
            set
            {
                if (_FacilityType != value)
                {
                    _FacilityType = value;
                    OnPropertyChanged(() => FacilityType);
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

        public DateTime RunDate
        {
            get { return _RunDate; }
            set
            {
                if (_RunDate != value)
                {
                    _RunDate = value;
                    OnPropertyChanged(() => RunDate);
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


        class FacilityClassificationValidator : AbstractValidator<FacilityClassification>
        {
            public FacilityClassificationValidator()
            {
                RuleFor(obj => obj.Refno).NotEmpty().WithMessage("Reference Number is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new FacilityClassificationValidator();
        }
    }
}
