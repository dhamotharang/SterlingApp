using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class RegressionOutput : ObjectBase
    {
        int _RegressionOutputId;
        string _Variable;
        double _Rsquare;
        double _Fstatistic;
        double _Pvalue;
        DateTime _Rundate;
        bool _Active;

        public int RegressionOutputId
        {
            get { return _RegressionOutputId; }
            set
            {
                if (_RegressionOutputId != value)
                {
                    _RegressionOutputId = value;
                    OnPropertyChanged(() => RegressionOutputId);
                }
            }
        }

        public string Variable
        {
            get { return _Variable; }
            set
            {
                if (_Variable != value)
                {
                    _Variable = value;
                    OnPropertyChanged(() => Variable);
                }
            }
        }

        public double Rsquare
        {
            get { return _Rsquare; }
            set
            {
                if (_Rsquare != value)
                {
                    _Rsquare = value;
                    OnPropertyChanged(() => Rsquare);
                }
            }
        }

        public double Fstatistic
        {
            get { return _Fstatistic; }
            set
            {
                if (_Fstatistic != value)
                {
                    _Fstatistic = value;
                    OnPropertyChanged(() => Fstatistic);
                }
            }
        }

        public double Pvalue
        {
            get { return _Pvalue; }
            set
            {
                if (_Pvalue != value)
                {
                    _Pvalue = value;
                    OnPropertyChanged(() => Pvalue);
                }
            }
        }

        public DateTime Rundate
        {
            get { return _Rundate; }
            set
            {
                if (_Rundate != value)
                {
                    _Rundate = value;
                    OnPropertyChanged(() => Rundate);
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


        class RegressionOutputValidator : AbstractValidator<RegressionOutput>
        {
            public RegressionOutputValidator()
            {
                //RuleFor(obj => obj.type).NotEmpty().WithMessage("Type is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new RegressionOutputValidator();
        }
    }
}
