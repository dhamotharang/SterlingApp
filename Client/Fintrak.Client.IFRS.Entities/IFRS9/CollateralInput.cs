using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class CollateralInput : ObjectBase
    {
        int _Collateral_Id;
        string _type;
        string _code;
        double _Collateral_haircut;
        double _Collateral_Growth_rate;
        double _Cost_of_recovery;
        double _Time_of_recovery;
        string _catergory;
        bool _Active;

        public int Collateral_Id
        {
            get { return _Collateral_Id; }
            set
            {
                if (_Collateral_Id != value)
                {
                    _Collateral_Id = value;
                    OnPropertyChanged(() => Collateral_Id);
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

        public double Collateral_haircut
        {
            get { return _Collateral_haircut; }
            set
            {
                if (_Collateral_haircut != value)
                {
                    _Collateral_haircut = value;
                    OnPropertyChanged(() => Collateral_haircut);
                }
            }
        }

        public double Collateral_Growth_rate
        {
            get { return _Collateral_Growth_rate; }
            set
            {
                if (_Collateral_Growth_rate != value)
                {
                    _Collateral_Growth_rate = value;
                    OnPropertyChanged(() => Collateral_Growth_rate);
                }
            }
        }

        public double Cost_of_recovery
        {
            get { return _Cost_of_recovery; }
            set
            {
                if (_Cost_of_recovery != value)
                {
                    _Cost_of_recovery = value;
                    OnPropertyChanged(() => Cost_of_recovery);
                }
            }
        }

        public double Time_of_recovery
        {
            get { return _Time_of_recovery; }
            set
            {
                if (_Time_of_recovery != value)
                {
                    _Time_of_recovery = value;
                    OnPropertyChanged(() => Time_of_recovery);
                }
            }
        }

        public string catergory
        {
            get { return _catergory; }
            set
            {
                if (_catergory != value)
                {
                    _catergory = value;
                    OnPropertyChanged(() => catergory);
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


        class CollateralInputValidator : AbstractValidator<CollateralInput>
        {
            public CollateralInputValidator()
            {
                RuleFor(obj => obj.type).NotEmpty().WithMessage("Type is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new CollateralInputValidator();
        }
    }
}
