using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class SectorMapping : ObjectBase
    {
        int _SectorMapping_Id;
        string _CBNSector;
        string _LGDSectorMapping;
        string _CCFMapping;
        bool _Active;

        public int SectorMapping_Id
        {
            get { return _SectorMapping_Id; }
            set
            {
                if (_SectorMapping_Id != value)
                {
                    _SectorMapping_Id = value;
                    OnPropertyChanged(() => SectorMapping_Id);
                }
            }
        }

        public string CBNSector
        {
            get { return _CBNSector; }
            set
            {
                if (_CBNSector != value)
                {
                    _CBNSector = value;
                    OnPropertyChanged(() => CBNSector);
                }
            }
        }


        public string LGDSectorMapping
        {
            get { return _LGDSectorMapping; }
            set
            {
                if (_LGDSectorMapping != value)
                {
                    _LGDSectorMapping = value;
                    OnPropertyChanged(() => LGDSectorMapping);
                }
            }
        }


        public string CCFMapping
        {
            get { return _CCFMapping; }
            set
            {
                if (_CCFMapping != value)
                {
                    _CCFMapping = value;
                    OnPropertyChanged(() => CCFMapping);
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


        class SectorMappingValidator : AbstractValidator<SectorMapping>
        {
            public SectorMappingValidator()
            {
                RuleFor(obj => obj.CBNSector).NotEmpty().WithMessage("CBNSector is required.");
                RuleFor(obj => obj.LGDSectorMapping).NotEmpty().WithMessage("LGDSectorMapping is required.");
                RuleFor(obj => obj.CCFMapping).NotEmpty().WithMessage("CCFMapping is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new SectorMappingValidator();
        }
    }
}
