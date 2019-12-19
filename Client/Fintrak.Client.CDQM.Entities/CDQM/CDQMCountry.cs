using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.CDQM.Entities
{
    public class CDQMCountry : ObjectBase
    {
        int _CountryId;
        string _Invalid;
        string _Valid;     
        bool _Active;

        public int CountryId
        {
            get { return _CountryId; }
            set
            {
                if (_CountryId != value)
                {
                    _CountryId = value;
                    OnPropertyChanged(() => CountryId);
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


        class CDQMCountryValidator : AbstractValidator<CDQMCountry>
        {
            public CDQMCountryValidator()
            {

            }
        }

        protected override IValidator GetValidator()
        {
            return new CDQMCountryValidator();
        }
    }
}
