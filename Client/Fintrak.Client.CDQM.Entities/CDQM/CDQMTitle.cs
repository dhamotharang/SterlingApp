using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.CDQM.Entities
{
    public class CDQMTitle : ObjectBase
    {
        int _TitleId;
        string _Invalid;
        string _Valid;     
        bool _Active;

        public int TitleId
        {
            get { return _TitleId; }
            set
            {
                if (_TitleId != value)
                {
                    _TitleId = value;
                    OnPropertyChanged(() => TitleId);
                }
            }
        }

        public string Invalid
        {
            get { return _Invalid; }
            set
            {
                if (_Invalid != value)
                {
                    _Invalid = value;
                    OnPropertyChanged(() => Invalid);
                }
            }
        }

        public string Valid
        {
            get { return _Valid; }
            set
            {
                if (_Valid != value)
                {
                    _Valid = value;
                    OnPropertyChanged(() => Valid);
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


        class CDQMTitleValidator : AbstractValidator<CDQMTitle>
        {
            public CDQMTitleValidator()
            {

            }
        }

        protected override IValidator GetValidator()
        {
            return new CDQMTitleValidator();
        }
    }
}
