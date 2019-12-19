using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class HistoricalSectorRating : ObjectBase
    {
        int _HistoricalSectorRatingId;
        string _Sector;
        string _Rating;
        DateTime _RunDate;
        int _Year;
        //string _CompanyCode;
        bool _Active;

        public int HistoricalSectorRatingId
        {
            get { return _HistoricalSectorRatingId; }
            set
            {
                if (_HistoricalSectorRatingId != value)
                {
                    _HistoricalSectorRatingId = value;
                    OnPropertyChanged(() => HistoricalSectorRatingId);
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

        public string Rating
        {
            get { return _Rating; }
            set
            {
                if (_Rating != value)
                {
                    _Rating = value;
                    OnPropertyChanged(() => Rating);
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


        //public string CompanyCode
        //{
        //    get { return _CompanyCode; }
        //    set
        //    {
        //        if (_CompanyCode != value)
        //        {
        //            _CompanyCode = value;
        //            OnPropertyChanged(() => CompanyCode);
        //        }
        //    }
        //}


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


        class HistoricalSectorRatingValidator : AbstractValidator<HistoricalSectorRating>
        {
            public HistoricalSectorRatingValidator()
            {
                RuleFor(obj => obj.Sector).NotEmpty().WithMessage("Sector is required.");
                RuleFor(obj => obj.Rating).NotEmpty().WithMessage("Rating is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new HistoricalSectorRatingValidator();
        }
    }
}
