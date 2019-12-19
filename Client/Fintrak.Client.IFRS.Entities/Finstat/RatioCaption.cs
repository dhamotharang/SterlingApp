using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class RatioCaption : ObjectBase
    {
        int _RatioCaptionID;
        string _RatioCategory;
        string _RatioCaptionOpt;
        int _Position;
     
        bool _Active;

        public int RatioCaptionID
        {
            get { return _RatioCaptionID; }
            set
            {
                if (_RatioCaptionID != value)
                {
                    _RatioCaptionID = value;
                    OnPropertyChanged(() => RatioCaptionID);
                }
            }
        }

        public string RatioCategory
        {
            get { return _RatioCategory; }
            set
            {
                if (_RatioCategory != value)
                {
                    _RatioCategory = value;
                    OnPropertyChanged(() => RatioCategory);
                }
            }
        }

        public string RatioCaptionOpt
        {
            get { return _RatioCaptionOpt; }
            set
            {
                if (_RatioCaptionOpt != value)
                {
                    _RatioCaptionOpt = value;
                    OnPropertyChanged(() => RatioCaptionOpt);
                }
            }
        }

        public int Position
        {
            get { return _Position; }
            set
            {
                if (_Position != value)
                {
                    _Position = value;
                    OnPropertyChanged(() => Position);
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


        class RatioCaptionValidator : AbstractValidator<RatioCaption>
        {
            public RatioCaptionValidator()
            {
               // RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new RatioCaptionValidator();
        }
    }
}
