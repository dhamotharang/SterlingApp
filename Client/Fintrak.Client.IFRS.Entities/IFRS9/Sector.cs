using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class Sector : ObjectBase
    {
        int _SectorId;
        string _Code;
        string _Description;
        string _Source;
        bool _Active;

        public int SectorId
        {
            get { return _SectorId; }
            set
            {
                if (_SectorId != value)
                {
                    _SectorId = value;
                    OnPropertyChanged(() => SectorId);
                }
            }
        }

        public string Code
        {
            get { return _Code; }
            set
            {
                if (_Code != value)
                {
                    _Code = value;
                    OnPropertyChanged(() => Code);
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


        public string Source
        {
            get { return _Source; }
            set
            {
                if (_Source != value)
                {
                    _Source = value;
                    OnPropertyChanged(() => Source);
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


        class SectorValidator : AbstractValidator<Sector>
        {
            public SectorValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Description).NotEmpty().WithMessage("Description is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new SectorValidator();
        }
    }
}
