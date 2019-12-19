using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class SectorialRegressedLGD : ObjectBase
    {
        int _SectorialRegressedLGDId;
        string _SectorCode;
        int _Year;
        string _Description;
        string _lgd;
        DateTime _RunDate;
        string _CompanyCode;
        bool _Active;

        public int SectorialRegressedLGDId
        {
            get { return _SectorialRegressedLGDId; }
            set
            {
                if (_SectorialRegressedLGDId != value)
                {
                    _SectorialRegressedLGDId = value;
                    OnPropertyChanged(() => SectorialRegressedLGDId);
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

        public string lgd
        {
            get { return _lgd; }
            set
            {
                if (_lgd != value)
                {
                    _lgd = value;
                    OnPropertyChanged(() => lgd);
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


        class SectorialRegressedLGDValidator : AbstractValidator<SectorialRegressedLGD>
        {
            public SectorialRegressedLGDValidator()
            {
                RuleFor(obj => obj.SectorCode).NotEmpty().WithMessage("SectorCode is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new SectorialRegressedLGDValidator();
        }
    }
}
