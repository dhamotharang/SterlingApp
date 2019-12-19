using System;
using System.Linq;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class MPRCommFee : ObjectBase
    {
        int _CommFee_Id;
        string _Miscode;
        string _AccountOfficer_Code;
        string _CustomerName;
        string _BranchCode;
        string _Inc_Exp;
        decimal _Amount;
        string _CurrencyType;
        string _GL_Code;
        string _RelatedAccount;
        string _Narrative;
        int _Period;
        int _Year;
        string _CustomerCode;
        DateTime _P_Date;
        string _Caption;
        string _GroupCaption;
        string _Tran_ID;
        DateTime _Tran_Date;
        string _GLName;
        string _EntryStatus;
        decimal _Rate;
        decimal _Raw_Amt;
        string _Sub_Head_GL_Code;
        string _ProductCode;
        string _Trans_Code;
        string _Ref_Num;
        string _Rcre_User_Id;
        string _Entry_User_Id;
        string _BrokerCode;
        string _RemapCaption;
        string _RemapGroupName;
        int _IsMT;
        string _MainCaption;
        int _ID;
        bool _Active;

        public int CommFee_Id
        {
            get { return _CommFee_Id; }
            set
            {
                if (_CommFee_Id != value)
                {
                    _CommFee_Id = value;
                    OnPropertyChanged(() => CommFee_Id);
                }
            }
        }

        public string Miscode
        {
            get { return _Miscode; }
            set
            {
                if (_Miscode != value)
                {
                    _Miscode = value;
                    OnPropertyChanged(() => Miscode);
                }
            }
        }

        public string AccountOfficer_Code
        {
            get { return _AccountOfficer_Code; }
            set
            {
                if (_AccountOfficer_Code != value)
                {
                    _AccountOfficer_Code = value;
                    OnPropertyChanged(() => AccountOfficer_Code);
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

        public string Inc_Exp
        {
            get { return _Inc_Exp; }
            set
            {
                if (_Inc_Exp != value)
                {
                    _Inc_Exp = value;
                    OnPropertyChanged(() => Inc_Exp);
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

        public string CurrencyType
        {
            get { return _CurrencyType; }
            set
            {
                if (_CurrencyType != value)
                {
                    _CurrencyType = value;
                    OnPropertyChanged(() => CurrencyType);
                }
            }
        }


        public string GL_Code
        {
            get { return _GL_Code; }
            set
            {
                if (_GL_Code != value)
                {
                    _GL_Code = value;
                    OnPropertyChanged(() => GL_Code);
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

        public int Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                    OnPropertyChanged(() => Period);
                }
            }
        }


        public int Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    OnPropertyChanged(() => Year);
                }
            }
        }

        public string CustomerCode
        {
            get { return _CustomerCode; }
            set
            {
                if (_CustomerCode != value)
                {
                    _CustomerCode = value;
                    OnPropertyChanged(() => CustomerCode);
                }
            }
        }

        public DateTime P_Date
        {
            get { return _P_Date; }
            set
            {
                if (_P_Date != value)
                {
                    _P_Date = value;
                    OnPropertyChanged(() => P_Date);
                }
            }
        }


        public string Caption
        {
            get { return _Caption; }
            set
            {
                if (_Caption != value)
                {
                    _Caption = value;
                    OnPropertyChanged(() => Caption);
                }
            }
        }


        public string GroupCaption
        {
            get { return _GroupCaption; }
            set
            {
                if (_GroupCaption != value)
                {
                    _GroupCaption = value;
                    OnPropertyChanged(() => GroupCaption);
                }
            }
        }


        public string Tran_ID
        {
            get { return _Tran_ID; }
            set
            {
                if (_Tran_ID != value)
                {
                    _Tran_ID = value;
                    OnPropertyChanged(() => Tran_ID);
                }
            }
        }


        public DateTime Tran_Date
        {
            get { return _Tran_Date; }
            set
            {
                if (_Tran_Date != value)
                {
                    _Tran_Date = value;
                    OnPropertyChanged(() => Tran_Date);
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


        public decimal Rate
        {
            get { return _Rate; }
            set
            {
                if (_Rate != value)
                {
                    _Rate = value;
                    OnPropertyChanged(() => Rate);
                }
            }
        }


        public decimal Raw_Amt
        {
            get { return _Raw_Amt; }
            set
            {
                if (_Raw_Amt != value)
                {
                    _Raw_Amt = value;
                    OnPropertyChanged(() => Raw_Amt);
                }
            }
        }


        public string Sub_Head_GL_Code
        {
            get { return _Sub_Head_GL_Code; }
            set
            {
                if (_Sub_Head_GL_Code != value)
                {
                    _Sub_Head_GL_Code = value;
                    OnPropertyChanged(() => Sub_Head_GL_Code);
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


        public string Trans_Code
        {
            get { return _Trans_Code; }
            set
            {
                if (_Trans_Code != value)
                {
                    _Trans_Code = value;
                    OnPropertyChanged(() => Trans_Code);
                }
            }
        }


        public string Ref_Num
        {
            get { return _Ref_Num; }
            set
            {
                if (_Ref_Num != value)
                {
                    _Ref_Num = value;
                    OnPropertyChanged(() => Ref_Num);
                }
            }
        }


        public string Rcre_User_Id
        {
            get { return _Rcre_User_Id; }
            set
            {
                if (_Rcre_User_Id != value)
                {
                    _Rcre_User_Id = value;
                    OnPropertyChanged(() => Rcre_User_Id);
                }
            }
        }


        public string Entry_User_Id
        {
            get { return _Entry_User_Id; }
            set
            {
                if (_Entry_User_Id != value)
                {
                    _Entry_User_Id = value;
                    OnPropertyChanged(() => Entry_User_Id);
                }
            }
        }


        public string BrokerCode
        {
            get { return _BrokerCode; }
            set
            {
                if (_BrokerCode != value)
                {
                    _BrokerCode = value;
                    OnPropertyChanged(() => BrokerCode);
                }
            }
        }


        public string RemapCaption
        {
            get { return _RemapCaption; }
            set
            {
                if (_RemapCaption != value)
                {
                    _RemapCaption = value;
                    OnPropertyChanged(() => RemapCaption);
                }
            }
        }


        public string RemapGroupName
        {
            get { return _RemapGroupName; }
            set
            {
                if (_RemapGroupName != value)
                {
                    _RemapGroupName = value;
                    OnPropertyChanged(() => RemapGroupName);
                }
            }
        }


        public int IsMT
        {
            get { return _IsMT; }
            set
            {
                if (_IsMT != value)
                {
                    _IsMT = value;
                    OnPropertyChanged(() => IsMT);
                }
            }
        }


        public string MainCaption
        {
            get { return _MainCaption; }
            set
            {
                if (_MainCaption != value)
                {
                    _MainCaption = value;
                    OnPropertyChanged(() => MainCaption);
                }
            }
        }


        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged(() => ID);
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


        class MPRCommFeeValidator : AbstractValidator<MPRCommFee>
        {
            public MPRCommFeeValidator()
            {
                RuleFor(obj => obj.Miscode).NotEmpty().WithMessage("Miscode is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MPRCommFeeValidator();
        }
    }
}