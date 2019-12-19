using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class HistoricalSectorialPD : ObjectBase
    {
        int _HistoricalSectorialPDId;
        string _SectorCode;
        string _SectorName;
        int _Year;
        int _Period;
        double _PD;
        double _AvgPD;
        bool _Active;

        public int HistoricalSectorialPDId
        {
            get { return _HistoricalSectorialPDId; }
            set
            {
                if (_HistoricalSectorialPDId != value)
                {
                    _HistoricalSectorialPDId = value;
                    OnPropertyChanged(() => HistoricalSectorialPDId);
                }
            }
        }

        public string SectorCode
        {
            get { return _SectorCode; }
            set
            {
                if (_SectorCode != value)
                {
                    _SectorCode = value;
                    OnPropertyChanged(() => SectorCode);
                }
            }
        }

        public string SectorName
        {
            get { return _SectorName; }
            set
            {
                if (_SectorName != value)
                {
                    _SectorName = value;
                    OnPropertyChanged(() => SectorName);
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


        public int Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                    OnPropertyChanged(() => Period);
                }
            }
        }


        public double PD
        {
            get { return _PD; }
            set
            {
                if (_PD != value)
                {
                    _PD = value;
                    OnPropertyChanged(() => PD);
                }
            }
        }

        public double AvgPD
        {
            get { return _AvgPD; }
            set
            {
                if (_AvgPD != value)
                {
                    _AvgPD = value;
                    OnPropertyChanged(() => AvgPD);
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


        class HistoricalSectorialPDValidator : AbstractValidator<HistoricalSectorialPD>
        {
            public HistoricalSectorialPDValidator()
            {
                RuleFor(obj => obj.SectorCode).NotEmpty().WithMessage("Sector is required.");
                RuleFor(obj => obj.SectorName).NotEmpty().WithMessage("Rating is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new HistoricalSectorialPDValidator();
        }
    }
}
