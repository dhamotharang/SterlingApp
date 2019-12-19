using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class RatingMapping : ObjectBase
    {
        int _RatingMappingId;
        int _Credit_Risk_Id;
        int _External_Rating_Id;
        //string _CompanyCode;
        bool _Active;

        public int RatingMappingId
        {
            get { return _RatingMappingId; }
            set
            {
                if (_RatingMappingId != value)
                {
                    _RatingMappingId = value;
                    OnPropertyChanged(() => RatingMappingId);
                }
            }
        }

        public int Credit_Risk_Id
        {
            get { return _Credit_Risk_Id; }
            set
            {
                if (_Credit_Risk_Id != value)
                {
                    _Credit_Risk_Id = value;
                    OnPropertyChanged(() => Credit_Risk_Id);
                }
            }
        }

        public int External_Rating_Id
        {
            get { return _External_Rating_Id; }
            set
            {
                if (_External_Rating_Id != value)
                {
                    _External_Rating_Id = value;
                    OnPropertyChanged(() => External_Rating_Id);
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


        class RatingMappingValidator : AbstractValidator<RatingMapping>
        {
            public RatingMappingValidator()
            {
                RuleFor(obj => obj.Credit_Risk_Id).NotEmpty().WithMessage("Credit Risk is required.");
                RuleFor(obj => obj.External_Rating_Id).NotEmpty().WithMessage("External Rating is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new RatingMappingValidator();
        }
    }
}
