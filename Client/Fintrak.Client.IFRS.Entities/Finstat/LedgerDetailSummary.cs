using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Client.IFRS.Entities
{
    public class LedgerDetailSummary : ObjectBase
    {
        int _SummaryId;
        string _InstrumentTypeName;
        string _Glcode;
        double _Facevalue;
        string _Currency;
        double _LedgerBal;
        double _Difference;
        bool _Active;

        public int SummaryId
        {
            get { return _SummaryId; }
            set
            {
                if (_SummaryId != value)
                {
                    _SummaryId = value;
                    OnPropertyChanged(() => SummaryId);
                }
            }
        }
   

        public string InstrumentTypeName
        {
            get { return _InstrumentTypeName; }
            set
            {
                if (_InstrumentTypeName != value)
                {
                    _InstrumentTypeName = value;
                    OnPropertyChanged(() => InstrumentTypeName);
                }
            }
        }

        public string Glcode
        {
            get { return _Glcode; }
            set
            {
                if (_Glcode != value)
                {
                    _Glcode = value;
                    OnPropertyChanged(() => Glcode);
                }
            }
        }


        public double Facevalue
        {
            get { return _Facevalue; }
            set
            {
                if (_Facevalue != value)
                {
                    _Facevalue = value;
                    OnPropertyChanged(() => Facevalue);
                }
            }
        }


        public string Currency
        {
            get { return _Currency; }
            set
            {
                if (_Currency != value)
                {
                    _Currency = value;
                    OnPropertyChanged(() => Currency);
                }
            }
        }

        public double LedgerBal
        {
            get { return _LedgerBal; }
            set
            {
                if (_LedgerBal != value)
                {
                    _LedgerBal = value;
                    OnPropertyChanged(() => LedgerBal);
                }
            }
        }

        public double Difference
        {
            get { return _Difference; }
            set
            {
                if (_Difference != value)
                {
                    _Difference = value;
                    OnPropertyChanged(() => Difference);
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


        
        class LedgerDetailSummaryValidator : AbstractValidator<LedgerDetailSummary>
        {
            public LedgerDetailSummaryValidator()
            {
               
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new LedgerDetailSummaryValidator();
        }
    }
}
