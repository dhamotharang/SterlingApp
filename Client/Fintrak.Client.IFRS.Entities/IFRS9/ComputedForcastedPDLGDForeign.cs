using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class ComputedForcastedPDLGDForeign : ObjectBase
    {
        int _ComputedPDId;
        string _Sector_Code;
        int _Type;
        int _Year;
        double? _PD_LGD;
        double? _AdversePD;
        double? _OptimisticPD;
        double? _PD;
        DateTime _Rundate;
        bool _Active;

        public int ComputedPDId
        {
            get { return _ComputedPDId; }
            set
            {
                if (_ComputedPDId != value)
                {
                    _ComputedPDId = value;
                    OnPropertyChanged(() => ComputedPDId);
                }
            }
        }

        public string Sector_Code
        {
            get { return _Sector_Code; }
            set
            {
                if (_Sector_Code != value)
                {
                    _Sector_Code = value;
                    OnPropertyChanged(() => Sector_Code);
                }
            }
        }


        public int Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    OnPropertyChanged(() => Type);
                }
            }
        }


        public int Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    OnPropertyChanged(() => Year);
                }
            }
        }


        public double? PD_LGD
        {
            get { return _PD_LGD; }
            set
            {
                if (_PD_LGD != value)
                {
                    _PD_LGD = value;
                    OnPropertyChanged(() => PD_LGD);
                }
            }
        }


        public double? AdversePD
        {
            get { return _AdversePD; }
            set
            {
                if (_AdversePD != value)
                {
                    _AdversePD = value;
                    OnPropertyChanged(() => AdversePD);
                }
            }
        }


        public double? OptimisticPD
        {
            get { return _OptimisticPD; }
            set
            {
                if (_OptimisticPD != value)
                {
                    _OptimisticPD = value;
                    OnPropertyChanged(() => OptimisticPD);
                }
            }
        }

        public double? PD
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

        public DateTime Rundate
        {
            get { return _Rundate; }
            set
            {
                if (_Rundate != value)
                {
                    _Rundate = value;
                    OnPropertyChanged(() => Rundate);
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
        class ComputedForcastedPDLGDForeignValidator : AbstractValidator<ComputedForcastedPDLGDForeign>
        {
            public ComputedForcastedPDLGDForeignValidator()
            {
                RuleFor(obj => obj.Sector_Code).NotEmpty().WithMessage("Sector Code is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
                RuleFor(obj => obj.PD_LGD).NotEmpty().WithMessage("PD/GLD is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ComputedForcastedPDLGDForeignValidator();
        }
    }
}
