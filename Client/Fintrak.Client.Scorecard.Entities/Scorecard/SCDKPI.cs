using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Scorecard.Framework;
using FluentValidation;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.Scorecard.Entities
{
    public class SCDKPI : ObjectBase
    {
        int _KPIId;
        KPIDirection _Direction;
        string _Code;
        PeriodType _PeriodType;
        string _Name;
        string _Description;
        string _CategoryCode;
        string _Formula;
        bool _IsKPICalculated;
        AggregateMethods _AggregateMethod;
        bool _IsTargetCalculated;
        string _ScoreFormula;
        bool _Active;

        public int KPIId
        {
            get { return _KPIId; }
            set
            {
                if (_KPIId != value)
                {
                    _KPIId = value;
                    OnPropertyChanged(() => KPIId);
                }
            }
        }

        public KPIDirection Direction
        {
            get { return _Direction; }
            set
            {
                if (_Direction != value)
                {
                    _Direction = value;
                    OnPropertyChanged(() => Direction);
                }
            }
        }


        public AggregateMethods AggregateMethod
        {
            get { return _AggregateMethod; }
            set
            {
                if (_AggregateMethod != value)
                {
                    _AggregateMethod = value;
                    OnPropertyChanged(() => AggregateMethod);
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

        public PeriodType PeriodType
        {
            get { return _PeriodType; }
            set
            {
                if (_PeriodType != value)
                {
                    _PeriodType = value;
                    OnPropertyChanged(() => PeriodType);
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

        public string CategoryCode
        {
            get { return _CategoryCode; }
            set
            {
                if (_CategoryCode != value)
                {
                    _CategoryCode = value;
                    OnPropertyChanged(() => CategoryCode);
                }
            }
        }

        public string Formula
        {
            get { return _Formula; }
            set
            {
                if (_Formula != value)
                {
                    _Formula = value;
                    OnPropertyChanged(() => Formula);
                }
            }
        }


        public bool IsKPICalculated
        {
            get { return _IsKPICalculated; }
            set
            {
                if (_IsKPICalculated != value)
                {
                    _IsKPICalculated = value;
                    OnPropertyChanged(() => IsKPICalculated);
                }
            }
        }

        public bool IsTargetCalculated
        {
            get { return _IsTargetCalculated; }
            set
            {
                if (_IsTargetCalculated != value)
                {
                    _IsTargetCalculated = value;
                    OnPropertyChanged(() => IsTargetCalculated);
                }
            }
        }

        public string ScoreFormula
        {
            get { return _ScoreFormula; }
            set
            {
                if (_ScoreFormula != value)
                {
                    _ScoreFormula = value;
                    OnPropertyChanged(() => ScoreFormula);
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


        
        class SCDKPIValidator : AbstractValidator<SCDKPI>
        {
            public SCDKPIValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Direction).NotEmpty().WithMessage("Direction is required.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new SCDKPIValidator();
        }
    }
}
