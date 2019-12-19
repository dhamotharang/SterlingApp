using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.CDQM.Entities
{
    public class CDQMAddress : ObjectBase
    {
        int _AddressId;
        string _StreetName;
        string _City;
        string _PostalCode;
        string _LGA;
        string _State;
        bool _Active;

        public int AddressId
        {
            get { return _AddressId; }
            set
            {
                if (_AddressId != value)
                {
                    _AddressId = value;
                    OnPropertyChanged(() => AddressId);
                }
            }
        }

        public string StreetName
        {
            get { return _StreetName; }
            set
            {
                if (_StreetName != value)
                {
                    _StreetName = value;
                    OnPropertyChanged(() => StreetName);
                }
            }
        }

        public string City
        {
            get { return _City; }
            set
            {
                if (_City != value)
                {
                    _City = value;
                    OnPropertyChanged(() => City);
                }
            }
        }

        public string PostalCode
        {
            get { return _PostalCode; }
            set
            {
                if (_PostalCode != value)
                {
                    _PostalCode = value;
                    OnPropertyChanged(() => PostalCode);
                }
            }
        }

        public string State
        {
            get { return _State; }
            set
            {
                if (_State != value)
                {
                    _State = value;
                    OnPropertyChanged(() => State);
                }
            }
        }

        public string LGA
        {
            get { return _LGA; }
            set
            {
                if (_LGA != value)
                {
                    _LGA = value;
                    OnPropertyChanged(() => LGA);
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


        class CDQMAddressValidator : AbstractValidator<CDQMAddress>
        {
            public CDQMAddressValidator()
            {

            }
        }

        protected override IValidator GetValidator()
        {
            return new CDQMAddressValidator();
        }
    }
}
