using System;
using System.Linq;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class InstrumentType : ObjectBase
    {
        int _InstrumentTypeId;
        string _Name;
        IFRSInstrument _Instrument;
        int? _ParentId;
        bool _Active;

        public int InstrumentTypeId
        {
            get { return _InstrumentTypeId; }
            set
            {
                if (_InstrumentTypeId != value)
                {
                    _InstrumentTypeId = value;
                    OnPropertyChanged(() => InstrumentTypeId);
                }
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

        public IFRSInstrument Instrument
        {
            get { return _Instrument; }
            set
            {
                if (_Instrument != value)
                {
                    _Instrument = value;
                    OnPropertyChanged(() => Instrument);
                }
            }
        }

        public int? ParentId
        {
            get { return _ParentId; }
            set
            {
                if (_ParentId != value)
                {
                    _ParentId = value;
                    OnPropertyChanged(() => ParentId);
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

        
        class InstrumentTypeValidator : AbstractValidator<InstrumentType>
        {
            public InstrumentTypeValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
      
            }
        }

        protected override IValidator GetValidator()
        {
            return new InstrumentTypeValidator();
        }
    }
}
