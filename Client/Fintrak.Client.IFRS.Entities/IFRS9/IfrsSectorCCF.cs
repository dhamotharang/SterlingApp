using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsSectorCCF : ObjectBase
    {
        int _SectorId;
        string _code;
        string _sector;
        double? _ccf;
        double? _lgdDownturn;
        double? _lgdOptimistic;
        double? _lgdBest;
        double? _Pd;
        string _type;
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

        public string code
        {
            get { return _code; }
            set
            {
                if (_code != value)
                {
                    _code = value;
                    OnPropertyChanged(() => code);
                }
            }
        }

        public string sector
        {
            get { return _sector; }
            set
            {
                if (_sector != value)
                {
                    _sector = value;
                    OnPropertyChanged(() => sector);
                }
            }
        }

        public double? ccf
        {
            get { return _ccf; }
            set
            {
                if (_ccf != value)
                {
                    _ccf = value;
                    OnPropertyChanged(() => ccf);
                }
            }
        }

        public double? lgdDownturn
        {
            get { return _lgdDownturn; }
            set
            {
                if (_lgdDownturn != value)
                {
                    _lgdDownturn = value;
                    OnPropertyChanged(() => lgdDownturn);
                }
            }
        }

        public double? lgdOptimistic
        {
            get { return _lgdOptimistic; }
            set
            {
                if (_lgdOptimistic != value)
                {
                    _lgdOptimistic = value;
                    OnPropertyChanged(() => lgdOptimistic);
                }
            }
        }

        public double? lgdBest
        {
            get { return _lgdBest; }
            set
            {
                if (_lgdBest != value)
                {
                    _lgdBest = value;
                    OnPropertyChanged(() => lgdBest);
                }
            }
        }

        public double? Pd
        {
            get { return _Pd; }
            set
            {
                if (_Pd != value)
                {
                    _Pd = value;
                    OnPropertyChanged(() => Pd);
                }
            }
        }

        public string type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged(() => type);
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

        class IfrsSectorCCFValidator : AbstractValidator<IfrsSectorCCF>
        {
            public IfrsSectorCCFValidator()
            {
                RuleFor(obj => obj.code).NotEmpty().WithMessage("code is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsSectorCCFValidator();
        }
    }
}
