using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class HistoricalSectorialLGD : ObjectBase
    {
        int _HistoricalSectorialLGDId;
        string _SectorCode;
        string _SectorName;
        int _Year;
        int _Period;
        double _LGD;
        double _AvgLGD;
        bool _Active;

        public int HistoricalSectorialLGDId
        {
            get { return _HistoricalSectorialLGDId; }
            set
            {
                if (_HistoricalSectorialLGDId != value)
                {
                    _HistoricalSectorialLGDId = value;
                    OnPropertyChanged(() => HistoricalSectorialLGDId);
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


        public double LGD
        {
            get { return _LGD; }
            set
            {
                if (_LGD != value)
                {
                    _LGD = value;
                    OnPropertyChanged(() => LGD);
                }
            }
        }

        public double AvgLGD
        {
            get { return _AvgLGD; }
            set
            {
                if (_AvgLGD != value)
                {
                    _AvgLGD = value;
                    OnPropertyChanged(() => AvgLGD);
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


        class HistoricalSectorialLGDValidator : AbstractValidator<HistoricalSectorialLGD>
        {
            public HistoricalSectorialLGDValidator()
            {
                RuleFor(obj => obj.SectorCode).NotEmpty().WithMessage("Sector is required.");
                RuleFor(obj => obj.SectorName).NotEmpty().WithMessage("Rating is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new HistoricalSectorialLGDValidator();
        }
    }
}
