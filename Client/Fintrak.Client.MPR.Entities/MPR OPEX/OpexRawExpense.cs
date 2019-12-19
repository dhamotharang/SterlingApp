using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class OpexRawExpense : ObjectBase
    {
        int _OpexRawExpenseId;
        string _GLCode;
        string _GLName;
        DateTime _PostDate;
        double _Amount;
        string _Description;
        string _CheckMisCode;
        string _MisCode;
        string _BranchCode;
        string _TranID;
        string _SubGLCode;
        double _DR;
        double _CR;
        string _Narrative;
        bool _Active;


        public int OpexRawExpenseId
        {
            get { return _OpexRawExpenseId; }
            set
            {
                if (_OpexRawExpenseId != value)
                {
                    _OpexRawExpenseId = value;
                    OnPropertyChanged(() => OpexRawExpenseId);
                }
            }
        }

        public string GLCode
        {
            get { return _GLCode; }
            set
            {
                if (_GLCode != value)
                {
                    _GLCode = value;
                    OnPropertyChanged(() => GLCode);
                }
            }
        }

        public string GLName
        {
            get { return _GLName; }
            set
            {
                if (_GLName != value)
                {
                    _GLName = value;
                    OnPropertyChanged(() => GLName);
                }
            }
        }

        public DateTime PostDate
        {
            get { return _PostDate; }
            set
            {
                if (_PostDate != value)
                {
                    _PostDate = value;
                    OnPropertyChanged(() => PostDate);
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

        public double Amount
        {
            get { return _Amount; }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    OnPropertyChanged(() => Amount);
                }
            }
        }

        public string CheckMisCode
        {
            get { return _CheckMisCode; }
            set
            {
                if (_CheckMisCode != value)
                {
                    _CheckMisCode = value;
                    OnPropertyChanged(() => CheckMisCode);
                }
            }
        }
        public double DR
        {
            get { return _DR; }
            set
            {
                if (_DR != value)
                {
                    _DR = value;
                    OnPropertyChanged(() => DR);
                }
            }
        }



        public double CR
        {
            get { return _CR; }
            set
            {
                if (_CR != value)
                {
                    _CR = value;
                    OnPropertyChanged(() => CR);
                }
            }
        }


        public string Narrative
        {
            get { return _Narrative; }
            set
            {
                if (_Narrative != value)
                {
                    _Narrative = value;
                    OnPropertyChanged(() => Narrative);
                }
            }
        }



        public string SubGLCode
        {
            get { return _SubGLCode; }
            set
            {
                if (_SubGLCode != value)
                {
                    _SubGLCode = value;
                    OnPropertyChanged(() => SubGLCode);
                }
            }
        }

        public string MisCode
        {
            get { return _MisCode; }
            set
            {
                if (_MisCode != value)
                {
                    _MisCode = value;
                    OnPropertyChanged(() => MisCode);
                }
            }
        }



        public string BranchCode
        {
            get { return _BranchCode; }
            set
            {
                if (_BranchCode != value)
                {
                    _BranchCode = value;
                    OnPropertyChanged(() => BranchCode);
                }
            }
        }


        public string TranID
        {
            get { return _TranID; }
            set
            {
                if (_TranID != value)
                {
                    _TranID = value;
                    OnPropertyChanged(() => TranID);
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


        
        class OpexRawExpenseValidator : AbstractValidator<OpexRawExpense>
        {
            public OpexRawExpenseValidator()
            {
              
             }
        }

        protected override IValidator GetValidator()
        {
            return new OpexRawExpenseValidator();
        }
    }
}
