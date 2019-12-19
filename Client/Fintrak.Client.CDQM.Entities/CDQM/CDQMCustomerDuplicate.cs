using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.CDQM.Entities
{
    public class CDQMCustomerDuplicate : ObjectBase
    {
        int _CUST_DUPLICATES_ID;
        string _COD_CUST_ID;
        string _COD_GROUP_ID;
        string _NAM_GROUP_NAME;
        string _NAM_CUST_FULL;
        string _DAT_BIRTH_CUST;
        string _TXT_CUST_SEX;
        double _Score;
        bool _NotDuplicate;
        bool _Active;

        public int CUST_DUPLICATES_ID
        {
            get { return _CUST_DUPLICATES_ID; }
            set
            {
                if (_CUST_DUPLICATES_ID != value)
                {
                    _CUST_DUPLICATES_ID = value;
                    OnPropertyChanged(() => CUST_DUPLICATES_ID);
                }
            }
        }

        public string COD_CUST_ID
        {
            get { return _COD_CUST_ID; }
            set
            {
                if (_COD_CUST_ID != value)
                {
                    _COD_CUST_ID = value;
                    OnPropertyChanged(() => COD_CUST_ID);
                }
            }
        }

        public string COD_GROUP_ID
        {
            get { return _COD_GROUP_ID; }
            set
            {
                if (_COD_GROUP_ID != value)
                {
                    _COD_GROUP_ID = value;
                    OnPropertyChanged(() => COD_GROUP_ID);
                }
            }
        }

        public string NAM_GROUP_NAME
        {
            get { return _NAM_GROUP_NAME; }
            set
            {
                if (_NAM_GROUP_NAME != value)
                {
                    _NAM_GROUP_NAME = value;
                    OnPropertyChanged(() => NAM_GROUP_NAME);
                }
            }
        }

        public string NAM_CUST_FULL
        {
            get { return _NAM_CUST_FULL; }
            set
            {
                if (_NAM_CUST_FULL != value)
                {
                    _NAM_CUST_FULL = value;
                    OnPropertyChanged(() => NAM_CUST_FULL);
                }
            }
        }

        public string DAT_BIRTH_CUST
        {
            get { return _DAT_BIRTH_CUST; }
            set
            {
                if (_DAT_BIRTH_CUST != value)
                {
                    _DAT_BIRTH_CUST = value;
                    OnPropertyChanged(() => DAT_BIRTH_CUST);
                }
            }
        }

        public string TXT_CUST_SEX
        {
            get { return _TXT_CUST_SEX; }
            set
            {
                if (_TXT_CUST_SEX != value)
                {
                    _TXT_CUST_SEX = value;
                    OnPropertyChanged(() => TXT_CUST_SEX);
                }
            }
        }

        public double Score
        {
            get { return _Score; }
            set
            {
                if (_Score != value)
                {
                    _Score = value;
                    OnPropertyChanged(() => Score);
                }
            }
        }

        public bool NotDuplicate
        {
            get { return _NotDuplicate; }
            set
            {
                if (_NotDuplicate != value)
                {
                    _NotDuplicate = value;
                    OnPropertyChanged(() => NotDuplicate);
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


        class CDQMCustomerDuplicateValidator : AbstractValidator<CDQMCustomerDuplicate>
        {
            public CDQMCustomerDuplicateValidator()
            {

            }
        }

        protected override IValidator GetValidator()
        {
            return new CDQMCustomerDuplicateValidator();
        }
    }
}
