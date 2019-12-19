using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class ExternalRating : ObjectBase
    {
        int _ExternalRatingId;
        string _Agency;
        string _Rating;
        string _Description;
        string _Category;
        string _CompanyCode;
        bool _Active;

        public int ExternalRatingId
        {
            get { return _ExternalRatingId; }
            set
            {
                if (_ExternalRatingId != value)
                {
                    _ExternalRatingId = value;
                    OnPropertyChanged(() => ExternalRatingId);
                }
            }
        }

        public string Agency
        {
            get { return _Agency; }
            set
            {
                if (_Agency != value)
                {
                    _Agency = value;
                    OnPropertyChanged(() => Agency);
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

        public string Category
        {
            get { return _Category; }
            set
            {
                if (_Category != value)
                {
                    _Category = value;
                    OnPropertyChanged(() => Category);
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


        class ExternalRatingValidator : AbstractValidator<ExternalRating>
        {
            public ExternalRatingValidator()
            {
                RuleFor(obj => obj.Agency).NotEmpty().WithMessage("Agency is required.");
                RuleFor(obj => obj.Rating).NotEmpty().WithMessage("Rating is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ExternalRatingValidator();
        }
    }
}
