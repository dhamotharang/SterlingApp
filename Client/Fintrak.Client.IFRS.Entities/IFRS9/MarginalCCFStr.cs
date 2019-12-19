using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MarginalCCFStr : ObjectBase
    {
        int _Id;
        int _seq;
        string _OBEType;
        decimal _MonthlyCCF;
        decimal _MarginalCCF;
        bool _Active;

        public int Id {
            get { return _Id; }
            set {
                if (_Id != value) {
                    _Id = value;
                    OnPropertyChanged(() => Id);
                }
            }
        }

        public int seq {
            get { return _seq; }
            set {
                if (_seq != value) {
                    _seq = value;
                    OnPropertyChanged(() => seq);
                }
            }
        }

        public string OBEType {
            get { return _OBEType; }
            set {
                if (_OBEType != value) {
                    _OBEType = value;
                    OnPropertyChanged(() => OBEType);
                }
            }
        }


        public decimal MonthlyCCF {
            get { return _MonthlyCCF; }
            set {
                if (_MonthlyCCF != value) {
                    _MonthlyCCF = value;
                    OnPropertyChanged(() => MonthlyCCF);
                }
            }
        }

        public decimal MarginalCCF {
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


        class MarginalCCFStrValidator : AbstractValidator<MarginalCCFStr>
        {
            public MarginalCCFStrValidator(){
                //RuleFor(obj => obj.seq).NotEmpty().WithMessage("seq is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MarginalCCFStrValidator();
        }
    }
}




