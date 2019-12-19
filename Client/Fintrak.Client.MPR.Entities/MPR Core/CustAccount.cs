using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class CustAccount : ObjectBase
    {
        int _CustAccountId;
        string _CustNo;
        string _AccountNo;
        string _AccountName;
        //string _CustName;
        string _Sector;
        string _SubSector;
        string _TeamCode;
        string _AccountOfficerCode;
        string _ProductCode;
        string _BranchCode;
        string _Currency;
        DateTime _DateOpened;
        string _Status;
        string _SettlementAcct;
        string _CompanyCode;
        bool _Active;

        public int CustAccountId
        {
            get { return _CustAccountId; }
            set
            {
                if (_CustAccountId != value)
                {
                    _CustAccountId = value;
                    OnPropertyChanged(() => CustAccountId);
                }
            }
        }

        public string CustNo
        {
            get { return _CustNo; }
            set
            {
                if (_CustNo != value)
                {
                    _CustNo = value;
                    OnPropertyChanged(() => CustNo);
                }
            }
        }


        public string AccountNo
        {
            get { return _AccountNo; }
            set
            {
                if (_AccountNo != value)
                {
                    _AccountNo = value;
                    OnPropertyChanged(() => AccountNo);
                }
            }
        }


        public string AccountName
        {
            get { return _AccountName; }
            set
            {
                if (_AccountName != value)
                {
                    _AccountName = value;
                    OnPropertyChanged(() => AccountName);
                }
            }
        }

        //public string CustName
        //{
        //    get { return _CustName; }
        //    set
        //    {
        //        if (_CustName != value)
        //        {
        //            _CustName = value;
        //            OnPropertyChanged(() => CustName);
        //        }
        //    }
        //}

        public string Sector
        {
            get { return _Sector; }
            set
            {
                if (_Sector != value)
                {
                    _Sector = value;
                    OnPropertyChanged(() => Sector);
                }
            }
        }

        public string SubSector
        {
            get { return _SubSector; }
            set
            {
                if (_SubSector != value)
                {
                    _SubSector = value;
                    OnPropertyChanged(() => SubSector);
                }
            }
        }

        public string TeamCode
        {
            get { return _TeamCode; }
            set
            {
                if (_TeamCode != value)
                {
                    _TeamCode = value;
                    OnPropertyChanged(() => TeamCode);
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

        public DateTime DateOpened
        {
            get { return _DateOpened; }
            set
            {
                if (_DateOpened != value)
                {
                    _DateOpened = value;
                    OnPropertyChanged(() => DateOpened);
                }
            }
        }


        public string Status
        {
            get { return _Status; }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    OnPropertyChanged(() => Status);
                }
            }
        }

        public string SettlementAcct
        {
            get { return _SettlementAcct; }
            set
            {
                if (_SettlementAcct != value)
                {
                    _SettlementAcct = value;
                    OnPropertyChanged(() => SettlementAcct);
                }
            }
        }

        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value;
                    OnPropertyChanged(() => CompanyCode);
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

        
        class CustAccountValidator : AbstractValidator<CustAccount>
        {
            public CustAccountValidator()
            {
                RuleFor(obj => obj.CustNo).NotEmpty().WithMessage("Account is required.");
                RuleFor(obj => obj.Sector).NotEmpty().WithMessage("AccountOfficer is required.");
                RuleFor(obj => obj.AccountName).NotEmpty().WithMessage("Team is required.");
       
             
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new CustAccountValidator();
        }
    }
}
