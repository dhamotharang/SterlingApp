using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class LoanSignificantFlag : ObjectBase
    {


        int     _Id;
        int     _LoanClassificationId;
        string  _ProductType;
        string  _SubType;
        string  _SICR_Flag;
        int     _SignificantNo;
        bool    _Active;



        public int Id {
            get { return _Id; }
            set {
                if (_Id != value) {
                    _Id = value;
                    OnPropertyChanged(() => Id);
                }
            }
        }


        public int LoanClassificationId {
            get { return _LoanClassificationId; }
            set {
                if (_LoanClassificationId != value) {
                    _LoanClassificationId = value;
                    OnPropertyChanged(() => LoanClassificationId);
                }
            }
        }

        public string ProductType {
            get { return _ProductType; }
            set {
                if (_ProductType != value) {
                    _ProductType = value;
                    OnPropertyChanged(() => ProductType);
                }
            }
        }


        public string SubType {
            get { return _SubType; }
            set {
                if (_SubType != value) {
                    _SubType = value;
                    OnPropertyChanged(() => SubType);
                }
            }
        }

        public string SICR_Flag {
            get { return _SICR_Flag; }
            set {
                if (_SICR_Flag != value) {
                    _SICR_Flag = value;
                    OnPropertyChanged(() => SICR_Flag);
                }
            }
        }


        public int SignificantNo {
            get { return _SignificantNo; }
            set {
                if (_SignificantNo != value) {
                    _SignificantNo = value;
                    OnPropertyChanged(() => SignificantNo);
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


        class LoanSignificantFlagValidator : AbstractValidator<LoanSignificantFlag>{
            public LoanSignificantFlagValidator(){
              //RuleFor(obj => obj.LoanClassificationId).NotEmpty().WithMessage("LoanClassificationId is required.");
            }
        }

        protected override IValidator GetValidator(){
            return new LoanSignificantFlagValidator();
        }
    }
}
