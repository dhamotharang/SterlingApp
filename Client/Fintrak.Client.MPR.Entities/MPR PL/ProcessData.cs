using System;
using System.Linq;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class ProcessData : ObjectBase
    {
        int _ProcessDataId;
        string _CurrencyCode;
        DateTime _TransDate;
        string _Narrative;
        string _CustomerName;
        string _AccountOfficerCode;
        string _BranchCode;
        string _GLCode;
        string _GLName;
        string _DRCR;
        string _EntryUser;
        string _RelatedAccount;
        string _MISCode;
        string _RCREUser;
        decimal _Amount;
        DateTime _ValueDate;
        string _ProductCode;

        string _SUBGLCode;
        string _TransCode;
        string _TransReference;
        string _EventNo;
        string _BatchNo;
        string _TransClassification;
        string _EntryStatus;
        decimal _LCYAmount;
        double _ExchangeRate;
        bool _Active;

        public int ProcessDataId
        {
            get { return _ProcessDataId; }
            set
            {
                if (_ProcessDataId != value)
                {
                    _ProcessDataId = value;
                    OnPropertyChanged(() => ProcessDataId);
                }
            }
        }

        public string CurrencyCode
        {
            get { return _CurrencyCode; }
            set
            {
                if (_CurrencyCode != value)
                {
                    _CurrencyCode = value;
                    OnPropertyChanged(() => CurrencyCode);
                }
            }
        }

        public DateTime TransDate
        {
            get { return _TransDate; }
            set
            {
                if (_TransDate != value)
                {
                    _TransDate = value;
                    OnPropertyChanged(() => TransDate);
                }
            }
        }

        public string RCREUser
        {
            get { return _RCREUser; }
            set
            {
                if (_RCREUser != value)
                {
                    _RCREUser = value;
                    OnPropertyChanged(() => RCREUser);
                }
            }
        }

        public string MISCode
        {
            get { return _MISCode; }
            set
            {
                if (_MISCode != value)
                {
                    _MISCode = value;
                    OnPropertyChanged(() => MISCode);
                }
            }
        }

        public string DRCR
        {
            get { return _DRCR; }
            set
            {
                if (_DRCR != value)
                {
                    _DRCR = value;
                    OnPropertyChanged(() => DRCR);
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
        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    OnPropertyChanged(() => CustomerName);
                }
            }
        }

        public string AccountOfficerCode
        {
            get { return _AccountOfficerCode; }
            set
            {
                if (_AccountOfficerCode != value)
                {
                    _AccountOfficerCode = value;
                    OnPropertyChanged(() => AccountOfficerCode);
                }
            }
        }


        public string EntryUser
        {
            get { return _EntryUser; }
            set
            {
                if (_EntryUser != value)
                {
                    _EntryUser = value;
                    OnPropertyChanged(() => EntryUser);
                }
            }
        }

        public string RelatedAccount
        {
            get { return _RelatedAccount; }
            set
            {
                if (_RelatedAccount != value)
                {
                    _RelatedAccount = value;
                    OnPropertyChanged(() => RelatedAccount);
                }
            }
        }

        public decimal Amount
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


        public DateTime ValueDate
        {
            get { return _ValueDate; }
            set
            {
                if (_ValueDate != value)
                {
                    _ValueDate = value;
                    OnPropertyChanged(() => ValueDate);
                }
            }
        }


        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                if (_ProductCode != value)
                {
                    _ProductCode = value;
                    OnPropertyChanged(() => ProductCode);
                }
            }
        }



        public string SUBGLCode
        {
            get { return _SUBGLCode; }
            set
            {
                if (_SUBGLCode != value)
                {
                    _SUBGLCode = value;
                    OnPropertyChanged(() => SUBGLCode);
                }
            }
        }


        public string TransCode
        {
            get { return _TransCode; }
            set
            {
                if (_TransCode != value)
                {
                    _TransCode = value;
                    OnPropertyChanged(() => TransCode);
                }
            }
        }

        public string TransReference
        {
            get { return _TransReference; }
            set
            {
                if (_TransReference != value)
                {
                    _TransReference = value;
                    OnPropertyChanged(() => TransReference);
                }
            }

        }
        public string EventNo
        {
            get { return _EventNo; }
            set
            {
                if (_EventNo != value)
                {
                    _EventNo = value;
                    OnPropertyChanged(() => EventNo);
                }
            }
        }

        public string BatchNo
        {
            get { return _BatchNo; }
            set
            {
                if (_BatchNo != value)
                {
                    _BatchNo = value;
                    OnPropertyChanged(() => BatchNo);
                }
            }
        }


        public string TransClassification
        {
            get { return _TransClassification; }
            set
            {
                if (_TransClassification != value)
                {
                    _TransClassification = value;
                    OnPropertyChanged(() => TransClassification);
                }
            }
        }

        public string EntryStatus
        {
            get { return _EntryStatus; }
            set
            {
                if (_EntryStatus != value)
                {
                    _EntryStatus = value;
                    OnPropertyChanged(() => EntryStatus);
                }
            }
        }

        public decimal LCYAmount
        {
            get { return _LCYAmount; }
            set
            {
                if (_LCYAmount != value)
                {
                    _LCYAmount = value;
                    OnPropertyChanged(() => LCYAmount);
                }
            }
        }

        public double ExchangeRate
        {
            get { return _ExchangeRate; }
            set
            {
                if (_ExchangeRate != value)
                {
                    _ExchangeRate = value;
                    OnPropertyChanged(() => ExchangeRate);
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


        class ProcessDataValidator : AbstractValidator<ProcessData>
        {
            public ProcessDataValidator()
            {
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProcessDataValidator();
        }
    }
}