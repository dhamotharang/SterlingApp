using System;
using System.Linq;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class BSINOtherInformation : ObjectBase
    {
        int _BSINOtherInformationId;
        string _Segment;
        string _OtherCaption;
        string _MainCaption;
        string _SubCaption;
        string _Currency;
        bool _BSIN;
        bool _Active;

        public int BSINOtherInformationId
        {
            get { return _BSINOtherInformationId; }
            set
            {
                if (_BSINOtherInformationId != value)
                {
                    _BSINOtherInformationId = value;
                    OnPropertyChanged(() => BSINOtherInformationId);
                }
            }
        }

        public string Segment
        {
            get { return _Segment; }
            set
            {
                if (_Segment != value)
                {
                    _Segment = value;
                    OnPropertyChanged(() => Segment);
                }
            }
        }

        public string OtherCaption
        {
            get { return _OtherCaption; }
            set
            {
                if (_OtherCaption != value)
                {
                    _OtherCaption = value;
                    OnPropertyChanged(() => OtherCaption);
                }
            }
        }

        public string SubCaption
        {
            get { return _SubCaption; }
            set
            {
                if (_SubCaption != value)
                {
                    _SubCaption = value;
                    OnPropertyChanged(() => SubCaption);
                }
            }
        }

        public string Currency
        {
            get { return _Currency; }
            set
            {
                if (_Currency != value)
                {
                    _Currency = value;
                    OnPropertyChanged(() => Currency);
                }
            }
        }

        public bool BSIN
        {
            get { return _BSIN; }
            set
            {
                if (_BSIN != value)
                {
                    _BSIN = value;
                    OnPropertyChanged(() => BSIN);
                }
            }
        }

        public string MainCaption
        {
            get { return _MainCaption; }
            set
            {
                if (_MainCaption != value)
                {
                    _MainCaption = value;
                    OnPropertyChanged(() => MainCaption);
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


        class BSINOtherInformationValidator : AbstractValidator<BSINOtherInformation>
        {
            public BSINOtherInformationValidator()
            {
                RuleFor(obj => obj.OtherCaption).NotEmpty().WithMessage("OtherCaption Name is required.");
                RuleFor(obj => obj.Segment).NotEmpty().WithMessage("Segment Code is required.");
                RuleFor(obj => obj.Currency).NotEmpty().WithMessage("Currency Type is required.");
                RuleFor(obj => obj.BSIN).NotEmpty().WithMessage("BSIN Type is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new BSINOtherInformationValidator();
        }
    }
}
