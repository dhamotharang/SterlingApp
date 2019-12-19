using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.CDQM.Entities
{
    public class CDQMProduct : ObjectBase
    {
        int _ProductId;
        string _ProductCode;
        string _ProductName;
        bool _IsCardable;
        string _CustomerType;
        int _MinimumAge;
        int _MaximumAge;
        string _ProductSegment;
        string _CustomerMIS;
        bool _Active;

        public int ProductId
        {
            get { return _ProductId; }
            set
            {
                if (_ProductId != value)
                {
                    _ProductId = value;
                    OnPropertyChanged(() => ProductId);
                }
            }
        }

        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                if (_ProductCode != value)
                {
                    _ProductCode = value;
                    OnPropertyChanged(() => ProductCode);
                }
            }
        }

        public string ProductName
        {
            get { return _ProductName; }
            set
            {
                if (_ProductName != value)
                {
                    _ProductName = value;
                    OnPropertyChanged(() => ProductName);
                }
            }
        }

        public bool IsCardable
        {
            get { return _IsCardable; }
            set
            {
                if (_IsCardable != value)
                {
                    _IsCardable = value;
                    OnPropertyChanged(() => IsCardable);
                }
            }
        }

        public string CustomerType
        {
            get { return _CustomerType; }
            set
            {
                if (_CustomerType != value)
                {
                    _CustomerType = value;
                    OnPropertyChanged(() => CustomerType);
                }
            }
        }

        public int MinimumAge
        {
            get { return _MinimumAge; }
            set
            {
                if (_MinimumAge != value)
                {
                    _MinimumAge = value;
                    OnPropertyChanged(() => MinimumAge);
                }
            }
        }

        public int MaximumAge
        {
            get { return _MaximumAge; }
            set
            {
                if (_MaximumAge != value)
                {
                    _MaximumAge = value;
                    OnPropertyChanged(() => MaximumAge);
                }
            }
        }

        public string ProductSegment
        {
            get { return _ProductSegment; }
            set
            {
                if (_ProductSegment != value)
                {
                    _ProductSegment = value;
                    OnPropertyChanged(() => ProductSegment);
                }
            }
        }

        public string CustomerMIS
        {
            get { return _CustomerMIS; }
            set
            {
                if (_CustomerMIS != value)
                {
                    _CustomerMIS = value;
                    OnPropertyChanged(() => CustomerMIS);
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


        class CDQMProductValidator : AbstractValidator<CDQMProduct>
        {
            public CDQMProductValidator()
            {

            }
        }

        protected override IValidator GetValidator()
        {
            return new CDQMProductValidator();
        }
    }
}
