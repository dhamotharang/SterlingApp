using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class OpexBusinessRule : ObjectBase
    {
        int _OpexBusinessRuleId;
        string _Source;
        string _BasisCaption;
        string _Target;
        string _Description;
        double _Ratio;
        string _Template;
        int _Position;
        string _Total;
        string _Type;
        bool _Active;


        public int OpexBusinessRuleId
        {
            get { return _OpexBusinessRuleId; }
            set
            {
                if (_OpexBusinessRuleId != value)
                {
                    _OpexBusinessRuleId = value;
                    OnPropertyChanged(() => OpexBusinessRuleId);
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

        public string BasisCaption
        {
            get { return _BasisCaption; }
            set
            {
                if (_BasisCaption != value)
                {
                    _BasisCaption = value;
                    OnPropertyChanged(() => BasisCaption);
                }
            }
        }

        public string Target
        {
            get { return _Target; }
            set
            {
                if (_Target != value)
                {
                    _Target = value;
                    OnPropertyChanged(() => Target);
                }
            }
        }

        public double Ratio
        {
            get { return _Ratio; }
            set
            {
                if (_Ratio != value)
                {
                    _Ratio = value;
                    OnPropertyChanged(() => Ratio);
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

        public string Template
        {
            get { return _Template; }
            set
            {
                if (_Template != value)
                {
                    _Template = value;
                    OnPropertyChanged(() => Template);
                }
            }
        }
        public int Position
        {
            get { return _Position; }
            set
            {
                if (_Position != value)
                {
                    _Position = value;
                    OnPropertyChanged(() => Position);
                }
            }
        }

       

        public string Total
        {
            get { return _Total; }
            set
            {
                if (_Total != value)
                {
                    _Total = value;
                    OnPropertyChanged(() => Total);
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


        
        class OpexBusinessRuleValidator : AbstractValidator<OpexBusinessRule>
        {
            public OpexBusinessRuleValidator()
            {
                RuleFor(obj => obj.Source).NotEmpty().WithMessage("Source is required.");               
             }
        }

        protected override IValidator GetValidator()
        {
            return new OpexBusinessRuleValidator();
        }
    }
}
