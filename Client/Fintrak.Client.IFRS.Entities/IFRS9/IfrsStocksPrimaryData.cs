using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsStocksPrimaryData : ObjectBase
    {
        int _IfrsStocksPrimaryDataId;
        string _Company_name;
        string _Stock_code;
        double _Current_stock_price;
        double _Share_volume;
        double _EPS;
        double _Book_value;
        double _Cash_flow;
        double _Sales;
        string _Sector_code;
        bool _Active;

        public int IfrsStocksPrimaryDataId
        {
            get { return _IfrsStocksPrimaryDataId; }
            set
            {
                if (_IfrsStocksPrimaryDataId != value)
                {
                    _IfrsStocksPrimaryDataId = value;
                    OnPropertyChanged(() => IfrsStocksPrimaryDataId);
                }
            }
        }

        public string Company_name
        {
            get { return _Company_name; }
            set
            {
                if (_Company_name != value)
                {
                    _Company_name = value;
                    OnPropertyChanged(() => Company_name);
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

        public double Current_stock_price
        {
            get { return _Current_stock_price; }
            set
            {
                if (_Current_stock_price != value)
                {
                    _Current_stock_price = value;
                    OnPropertyChanged(() => Current_stock_price);
                }
            }
        }


        public double Share_volume
        {
            get { return _Share_volume; }
            set
            {
                if (_Share_volume != value)
                {
                    _Share_volume = value;
                    OnPropertyChanged(() => Share_volume);
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


        class IfrsStocksPrimaryDataValidator : AbstractValidator<IfrsStocksPrimaryData>
        {
            public IfrsStocksPrimaryDataValidator()
            {
                RuleFor(obj => obj.Company_name).NotEmpty().WithMessage("Company is required.");
                RuleFor(obj => obj.Stock_code).NotEmpty().WithMessage("Stock Code is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsStocksPrimaryDataValidator();
        }
    }
}
