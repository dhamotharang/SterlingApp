using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class SectorialRegressedPD : ObjectBase
    {
        int _SectorialRegressedPDId;
        string _SectorCode;
        int _Year;
        string _Description;
        double _AnnualPD;
        double _LifeTimePD;
        DateTime _RunDate;
        string _CompanyCode;
        bool _Active;

        public int SectorialRegressedPDId
        {
            get { return _SectorialRegressedPDId; }
            set
            {
                if (_SectorialRegressedPDId != value)
                {
                    _SectorialRegressedPDId = value;
                    OnPropertyChanged(() => SectorialRegressedPDId);
                }
            }
        }

        public string SectorCode
        {
            get { return _SectorCode; }
            set
            {
                if (_SectorCode != value)
                {
                    _SectorCode = value;
                    OnPropertyChanged(() => SectorCode);
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


        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }

        public double AnnualPD
        {
            get { return _AnnualPD; }
            set
            {
                if (_AnnualPD != value)
                {
                    _AnnualPD = value;
                    OnPropertyChanged(() => AnnualPD);
                }
            }
        }

        public double LifeTimePD
        {
            get { return _LifeTimePD; }
            set
            {
                if (_LifeTimePD != value)
                {
                    _LifeTimePD = value;
                    OnPropertyChanged(() => LifeTimePD);
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


        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value;
                    OnPropertyChanged(() => CompanyCode);
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


        class SectorialRegressedPDValidator : AbstractValidator<SectorialRegressedPD>
        {
            public SectorialRegressedPDValidator()
            {
                RuleFor(obj => obj.SectorCode).NotEmpty().WithMessage("SectorCode is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new SectorialRegressedPDValidator();
        }
    }
}
