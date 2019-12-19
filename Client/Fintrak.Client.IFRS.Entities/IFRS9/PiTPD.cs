using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class PiTPD : ObjectBase
    {
        int _PiTPDId;
        string _Sector;
        string _Yr2017;
        string _Yr2018;
        string _Yr2019;
        string _Yr2020;
      
        bool _Active;

        public int PiTPDId
        {
            get { return _PiTPDId; }
            set
            {
                if (_PiTPDId != value)
                {
                    _PiTPDId = value;
                    OnPropertyChanged(() => PiTPDId);
                }
            }
        }

        public string Sector
        {
            get { return _Sector; }
            set
            {
                if (_Sector != value)
                {
                    _Sector = value;
                    OnPropertyChanged(() => Sector);
                }
            }
        }

        public string Yr2017
        {
            get { return _Yr2017; }
            set
            {
                if (_Yr2017 != value)
                {
                    _Yr2017 = value;
                    OnPropertyChanged(() => Yr2017);
                }
            }
        }


        public string Yr2018
        {
            get { return _Yr2018; }
            set
            {
                if (_Yr2018 != value)
                {
                    _Yr2018 = value;
                    OnPropertyChanged(() => Yr2018);
                }
            }
        }


        public string Yr2019
        {
            get { return _Yr2019; }
            set
            {
                if (_Yr2019 != value)
                {
                    _Yr2019 = value;
                    OnPropertyChanged(() => Yr2019);
                }
            }
        }

        public string Yr2020
        {
            get { return _Yr2020; }
            set
            {
                if (_Yr2020 != value)
                {
                    _Yr2020 = value;
                    OnPropertyChanged(() => Yr2020);
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


        class PitPDValidator : AbstractValidator<PiTPD>
        {
            public PitPDValidator()
            {
                RuleFor(obj => obj.Sector).NotEmpty().WithMessage("Sector is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new PitPDValidator();
        }
    }
}
