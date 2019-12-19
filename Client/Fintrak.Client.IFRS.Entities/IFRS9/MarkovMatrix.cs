using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MarkovMatrix : ObjectBase
    {
        int _MarkovMatrixId;
        string _Sector;
        int _Year;
        double _InitialPD;
        double _InitialNonPD;
        double _PDmatrix;
        double _NPDmatrix;
        bool _Active;

        public int MarkovMatrixId
        {
            get { return _MarkovMatrixId; }
            set
            {
                if (_MarkovMatrixId != value)
                {
                    _MarkovMatrixId = value;
                    OnPropertyChanged(() => MarkovMatrixId);
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


        public double InitialPD
        {
            get { return _InitialPD; }
            set
            {
                if (_InitialPD != value)
                {
                    _InitialPD = value;
                    OnPropertyChanged(() => InitialPD);
                }
            }
        }

        public double InitialNonPD
        {
            get { return _InitialNonPD; }
            set
            {
                if (_InitialNonPD != value)
                {
                    _InitialNonPD = value;
                    OnPropertyChanged(() => InitialNonPD);
                }
            }
        }

        public double PDmatrix
        {
            get { return _PDmatrix; }
            set
            {
                if (_PDmatrix != value)
                {
                    _PDmatrix = value;
                    OnPropertyChanged(() => PDmatrix);
                }
            }
        }

        public double NPDmatrix
        {
            get { return _NPDmatrix; }
            set
            {
                if (_NPDmatrix != value)
                {
                    _NPDmatrix= value;
                    OnPropertyChanged(() => NPDmatrix);
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
        class MarkovMatrixValidator : AbstractValidator<MarkovMatrix>
        {
            public MarkovMatrixValidator()
            {
                RuleFor(obj => obj.Sector).NotEmpty().WithMessage("Sector Code is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MarkovMatrixValidator();
        }
    }
}
