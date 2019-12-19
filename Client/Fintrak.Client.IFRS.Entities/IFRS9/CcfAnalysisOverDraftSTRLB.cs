using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class CcfAnalysisOverDraftSTRLB : ObjectBase
    {
        int _ID;
        int _Seq;
        string _CCFType;
        double _InitialCCF;
        double _CCF;
        double _MonthlyCCF;
        double _CumMonthlyCCF;
        double _MarginalCCF;
        bool _Active;

        public int ID {
            get { return _ID; }
            set {
                if (_ID != value) {
                    _ID = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }
        public int Seq {
            get { return _Seq; }
            set {
                if (_Seq != value) {
                    _Seq = value;
                    OnPropertyChanged(() => Seq);
                }
            }
        }
        public string CCFType {
            get { return _CCFType; }
            set {
                if (_CCFType != value) {
                    _CCFType = value;
                    OnPropertyChanged(() => CCFType);
                }
            }
        }
        public double InitialCCF {
            get { return _InitialCCF; }
            set {
                if (_InitialCCF != value) {
                    _InitialCCF = value;
                    OnPropertyChanged(() => InitialCCF);
                }
            }
        }
        public double CCF {
            get { return _CCF; }
            set {
                if (_CCF != value) {
                    _CCF = value;
                    OnPropertyChanged(() => CCF);
                }
            }
        }
        public double MonthlyCCF {
            get { return _MonthlyCCF; }
            set {
                if (_MonthlyCCF != value) {
                    _MonthlyCCF = value;
                    OnPropertyChanged(() => MonthlyCCF);
                }
            }
        }
        public double CumMonthlyCCF {
            get { return _CumMonthlyCCF; }
            set {
                if (_CumMonthlyCCF != value) {
                    _CumMonthlyCCF = value;
                    OnPropertyChanged(() => CumMonthlyCCF);
                }
            }
        }
        public double MarginalCCF {
            get { return _MarginalCCF; }
            set {
                if (_MarginalCCF != value) {
                    _MarginalCCF = value;
                    OnPropertyChanged(() => MarginalCCF);
                }
            }
        }

        public bool Active {
            get { return _Active; }
            set {
                if (_Active != value) {
                    _Active = value;
                    OnPropertyChanged(() => Active);
                }
            }
        }


        class CcfAnalysisOverDraftSTRLBValidator : AbstractValidator<CcfAnalysisOverDraftSTRLB>
        {
            public CcfAnalysisOverDraftSTRLBValidator()
            {
                //RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");        
            }
        }

        protected override IValidator GetValidator()
        {
            return new CcfAnalysisOverDraftSTRLBValidator();
        }
    }
}
