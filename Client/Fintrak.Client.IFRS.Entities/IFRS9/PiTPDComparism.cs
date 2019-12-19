using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class PiTPDComparism : ObjectBase
    {
        int _ComparismPDId;
        int _Year;
        string _Grouping;
        string _Description;
        string _Type;
        double _BaseLinePD;
        double _StressedPiTPD;
        double _Movement;
        bool _Active;

        public int ComparismPDId
        {
            get { return _ComparismPDId; }
            set
            {
                if (_ComparismPDId != value)
                {
                    _ComparismPDId = value;
                    OnPropertyChanged(() => ComparismPDId);
                }
            }
        }

        public int Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    OnPropertyChanged(() => Year);
                }
            }
        }

        public string Grouping
        {
            get { return _Grouping; }
            set
            {
                if (_Grouping != value)
                {
                    _Grouping = value;
                    OnPropertyChanged(() => Grouping);
                }
            }
        }

        public string Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    OnPropertyChanged(() => Type);
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

        public double BaseLinePD
        {
            get { return _BaseLinePD; }
            set
            {
                if (_BaseLinePD != value)
                {
                    _BaseLinePD = value;
                    OnPropertyChanged(() => BaseLinePD);
                }
            }
        }

        public double Movement
        {
            get { return _Movement; }
            set
            {
                if (_Movement != value)
                {
                    _Movement = value;
                    OnPropertyChanged(() => Movement);
                }
            }
        }

        public double StressedPiTPD
        {
            get { return _StressedPiTPD; }
            set
            {
                if (_StressedPiTPD != value)
                {
                    _StressedPiTPD = value;
                    OnPropertyChanged(() => StressedPiTPD);
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


        class PiTPDComparismValidator : AbstractValidator<PiTPDComparism>
        {
            public PiTPDComparismValidator()
            {
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new PiTPDComparismValidator();
        }
    }
}
