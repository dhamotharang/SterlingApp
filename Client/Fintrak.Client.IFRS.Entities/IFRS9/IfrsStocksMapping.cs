using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsStocksMapping : ObjectBase
    {
        int _IfrsStocksMappingId;
        string _Unqouted_stock_code;
        string _StockDescription;
        string _Qouted_stock_code;

        bool _Active;

        public int IfrsStocksMappingId
        {
            get { return _IfrsStocksMappingId; }
            set
            {
                if (_IfrsStocksMappingId != value)
                {
                    _IfrsStocksMappingId = value;
                    OnPropertyChanged(() => IfrsStocksMappingId);
                }
            }
        }

        public string Unqouted_stock_code
        {
            get { return _Unqouted_stock_code; }
            set
            {
                if (_Unqouted_stock_code != value)
                {
                    _Unqouted_stock_code = value;
                    OnPropertyChanged(() => Unqouted_stock_code);
                }
            }
        }

        public string StockDescription
        {
            get { return _StockDescription; }
            set
            {
                if (_StockDescription != value)
                {
                    _StockDescription = value;
                    OnPropertyChanged(() => StockDescription);
                }
            }
        }


        public string Qouted_stock_code
        {
            get { return _Qouted_stock_code; }
            set
            {
                if (_Qouted_stock_code != value)
                {
                    _Qouted_stock_code = value;
                    OnPropertyChanged(() => Qouted_stock_code);
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


        class IfrsStocksMappingValidator : AbstractValidator<IfrsStocksMapping>
        {
            public IfrsStocksMappingValidator()
            {
                RuleFor(obj => obj.Unqouted_stock_code).NotEmpty().WithMessage("Unqouted Stock is required.");
                RuleFor(obj => obj.Qouted_stock_code).NotEmpty().WithMessage("Qouted Stock is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsStocksMappingValidator();
        }
    }
}
