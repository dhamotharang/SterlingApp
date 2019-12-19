using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsCustomerAccount : ObjectBase
    {
        int _CustAccountId;
        string _CustomerNo;
        string _AccountNo;
        string _AccountName;
        string _Sector;
        string _SubSector;
        string _Status;
        bool _IsDormant;
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
        public string CustomerNo
        {
            get { return _CustomerNo; }
            set
            {
                if (_CustomerNo != value)
                {
                    _CustomerNo = value;
                    OnPropertyChanged(() => CustomerNo);
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

        public bool IsDormant
        {
            get { return _IsDormant; }
            set
            {
                if (_IsDormant != value)
                {
                    _IsDormant = value;
                    OnPropertyChanged(() => IsDormant);
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

        class IfrsCustomerAccountValidator : AbstractValidator<IfrsCustomerAccount>
        {
            public IfrsCustomerAccountValidator()
            {
                RuleFor(obj => obj.CustomerNo).NotEmpty().WithMessage("CustomerNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsCustomerAccountValidator();
        }
    }
}
