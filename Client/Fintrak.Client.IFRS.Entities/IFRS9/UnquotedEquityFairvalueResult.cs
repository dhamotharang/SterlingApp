using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class UnquotedEquityFairvalueResult : ObjectBase
    {
        int _UnquotedEquityFairvalueResultId;
        string _stock;
        string _sector;
        double _P_E;
        double _P_CF;
        double _P_BV;
        double _P_S;
        double _Estimated_Stock_value_PE;
        double _Estimated_Stock_value_PCF;
        double _Estimated_Stock_value_PBV;
        double _Estimated_Stock_value_PS;
        double _Estimated_Mean_Stock_value;
        double _Standard_deviation;
        double _Discount_loss;
        double _fairvalue;
        bool _Active;

        public int UnquotedEquityFairvalueResultId
        {
            get { return _UnquotedEquityFairvalueResultId; }
            set
            {
                if (_UnquotedEquityFairvalueResultId != value)
                {
                    _UnquotedEquityFairvalueResultId = value;
                    OnPropertyChanged(() => UnquotedEquityFairvalueResultId);
                }
            }
        }

        public double Estimated_Stock_value_PCF
        {
            get { return _Estimated_Stock_value_PCF; }
            set
            {
                if (_Estimated_Stock_value_PCF != value)
                {
                    _Estimated_Stock_value_PCF = value;
                    OnPropertyChanged(() => Estimated_Stock_value_PCF);
                }
            }
        }
        public double Estimated_Stock_value_PE
        {
            get { return _Estimated_Stock_value_PE; }
            set
            {
                if (_Estimated_Stock_value_PE != value)
                {
                    _Estimated_Stock_value_PE = value;
                    OnPropertyChanged(() => Estimated_Stock_value_PE);
                }
            }
        }
        public double Estimated_Mean_Stock_value
        {
            get { return _Estimated_Mean_Stock_value; }
            set
            {
                if (_Estimated_Mean_Stock_value != value)
                {
                    _Estimated_Mean_Stock_value = value;
                    OnPropertyChanged(() => Estimated_Mean_Stock_value);
                }
            }
        }
        public double Standard_deviation
        {
            get { return _Standard_deviation; }
            set
            {
                if (_Standard_deviation != value)
                {
                    _Standard_deviation = value;
                    OnPropertyChanged(() => Standard_deviation);
                }
            }
        }

        public string stock
        {
            get { return _stock; }
            set
            {
                if (_stock != value)
                {
                    _stock = value;
                    OnPropertyChanged(() => stock);
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

        public double P_BV
        {
            get { return _P_BV; }
            set
            {
                if (_P_BV != value)
                {
                    _P_BV = value;
                    OnPropertyChanged(() => P_BV);
                }
            }
        }
        public double P_E
        {
            get { return _P_E; }
            set
            {
                if (_P_E != value)
                {
                    _P_E = value;
                    OnPropertyChanged(() => P_E);
                }
            }
        }

        public double P_CF
        {
            get { return _P_CF; }
            set
            {
                if (_P_CF != value)
                {
                    _P_CF = value;
                    OnPropertyChanged(() => P_CF);
                }
            }
        }

        public double P_S
        {
            get { return _P_S; }
            set
            {
                if (_P_S != value)
                {
                    _P_S = value;
                    OnPropertyChanged(() => P_S);
                }
            }
        }
        public double Estimated_Stock_value_PS
        {
            get { return _Estimated_Stock_value_PS; }
            set
            {
                if (_Estimated_Stock_value_PS != value)
                {
                    _Estimated_Stock_value_PS = value;
                    OnPropertyChanged(() => Estimated_Stock_value_PS);
                }
            }
        }

        public double Estimated_Stock_value_PBV
        {
            get { return _Estimated_Stock_value_PBV; }
            set
            {
                if (_Estimated_Stock_value_PBV != value)
                {
                    _Estimated_Stock_value_PBV = value;
                    OnPropertyChanged(() => Estimated_Stock_value_PBV);
                }
            }
        }

         public double fairvalue
        {
            get { return _fairvalue; }
            set
            {
                if (_fairvalue != value)
                {
                    _fairvalue = value;
                    OnPropertyChanged(() => fairvalue);
                }
            }
        }

         public double Discount_loss
        {
            get { return _Discount_loss; }
            set
            {
                if (_Discount_loss != value)
                {
                    _Discount_loss = value;
                    OnPropertyChanged(() => Discount_loss);
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

        class UnquotedEquityFairvalueResultValidator : AbstractValidator<UnquotedEquityFairvalueResult>
        {
            public UnquotedEquityFairvalueResultValidator()
            {
             
            }
        }

        protected override IValidator GetValidator()
        {
            return new UnquotedEquityFairvalueResultValidator();
        }
    }
}
