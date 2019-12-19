using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsEquityUnqouted : ObjectBase
    {
        int _IfrsEquityUnqoutedId;
        string _Stock_description;
        string _Stock_code;
        double _Units;
        double _EPS;
        double _Book_value;
        double _Cash_flow;
        double _Sales;
        string _Sector_code;
        bool _Active;

        public int IfrsEquityUnqoutedId
        {
            get { return _IfrsEquityUnqoutedId; }
            set
            {
                if (_IfrsEquityUnqoutedId != value)
                {
                    _IfrsEquityUnqoutedId = value;
                    OnPropertyChanged(() => IfrsEquityUnqoutedId);
                }
            }
        }

        public string Stock_description
        {
            get { return _Stock_description; }
            set
            {
                if (_Stock_description != value)
                {
                    _Stock_description = value;
                    OnPropertyChanged(() => Stock_description);
                }
            }
        }

        public string Stock_code
        {
            get { return _Stock_code; }
            set
            {
                if (_Stock_code != value)
                {
                    _Stock_code = value;
                    OnPropertyChanged(() => Stock_code);
                }
            }
        }


        public double Units
        {
            get { return _Units; }
            set
            {
                if (_Units != value)
                {
                    _Units = value;
                    OnPropertyChanged(() => Units);
                }
            }
        }

        public double EPS
        {
            get { return _EPS; }
            set
            {
                if (_EPS != value)
                {
                    _EPS = value;
                    OnPropertyChanged(() => EPS);
                }
            }
        }


        public double Book_value
        {
            get { return _Book_value; }
            set
            {
                if (_Book_value != value)
                {
                    _Book_value = value;
                    OnPropertyChanged(() => Book_value);
                }
            }
        }


        public double Cash_flow
        {
            get { return _Cash_flow; }
            set
            {
                if (_Cash_flow != value)
                {
                    _Cash_flow = value;
                    OnPropertyChanged(() => Cash_flow);
                }
            }
        }

        public double Sales
        {
            get { return _Sales; }
            set
            {
                if (_Sales != value)
                {
                    _Sales = value;
                    OnPropertyChanged(() => Sales);
                }
            }
        }


        public string Sector_code
        {
            get { return _Sector_code; }
            set
            {
                if (_Sector_code != value)
                {
                    _Sector_code = value;
                    OnPropertyChanged(() => Sector_code);
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


        class IfrsEquityUnqoutedValidator : AbstractValidator<IfrsEquityUnqouted>
        {
            public IfrsEquityUnqoutedValidator()
            {
                RuleFor(obj => obj.Stock_description).NotEmpty().WithMessage("Stock Description is required.");
                RuleFor(obj => obj.Stock_code).NotEmpty().WithMessage("Stock Code is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsEquityUnqoutedValidator();
        }
    }
}
