using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.CDQM.Entities
{
    public class CDQMCustomerPersistent : ObjectBase
    {
        int _CUSTOMER_PERSISTENT_ID;
        string _COD_CUST_ID;
        string _BVN;
        string _COD_ACCT_NO_DEFAULT;
        string _DAT_CUST_OPEN;
        string _FLG_STAFF;
        string _FLG_LOC_GLOB;
        string _COD_CC_HOMEBRN;
        string _NAM_CUST_SHRT;
        string _NAM_CUST_FULL;
        string _NAM_CUST_LAST;
        string _NAM_CUST_MID;
        string _NAM_CUST_FIRST;
        int _TXT_PROFESS_CAT;
        string _TXT_CUSTADR_ADD1;
        string _TXT_CUSTADR_ADD2;
        string _TXT_CUSTADR_ADD3;
        string _NAM_CUSTADR_CITY;
        string _NAM_CUSTADR_STATE;
        string _REF_CUST_PHONE;
        string _REF_CUST_EMAIL;
        string _DAT_BIRTH_CUST;
        string _TXT_CUST_SEX;
        int _COD_CUST_MARSTAT;
        string _NAM_CUST_SPOUSE;
        string _TXT_SPOUSE_OCCPN;
        int _CTR_DPNDT_CHLDRN;
        string _TXT_CUST_NATNLTY;
        string _DAT_INCORPORATED;
        string _COD_BUSINESS_REGNO;
        string _TXT_CUST_RESIDENCE;
        string _DAT_LAST_MNT;
        string _MIS_CODE;
        string _RM_CODE;
        string _RM_NAME;
        string _PRODUCT_CODE;
        string _PRODUCT_NAME;
        int _IS_CARDABLE;
        string _BUSINESS_DIVISION;
        string _CUSTOMER_SEGMENT;
        string _CUSTOMER_SUB_SEGMENT;
        string _PRODUCT_SEGMENT;
        int _IS_CARDHOLDER;
        int _HAS_INTERNETBANKING;
        int _HAS_MOBILEBANKING;
        string _CREDIT_RATING;
        string _CBN_SECTOR;
        string _CBN_SUB_SECTOR;
        int _AGE;
        decimal _AMOUNT;
        int _TRANS_COUNT;
        int _ACCOUNTCOUNT;
        int _IS_MERCHANTABLE;
        int _IS_MERCHANT;
        string _CUSTOMER_TYPE;
        int _MAXIMUM_AGE;
        int _MINIMUM_AGE;
        int _PRODUCTSTATUS;
        string _CUST_FULL_NAME_TITLE;
        string _cust_birthdate_status;
        string _PRODUCT_STATUS;
        string _CUST_FULL_ADDRESS;
        string _DERIVED_STATE;
        string _PostalCode;
        string _LGA;
        string _NAM_CUSTADR_CNTRY;
        string _country_status;
        string _email_status;
        string _gender_status;
        string _TXT_CUST_PREFIX;
        string _prefix_status;
        string _DERIVED_GENDER;
        string _COD_GROUP_ID;
        int __key_in;
        int __key_out;
        double __score;
        string _NAM_CUST_FULL_clean;
        string _DAT_BIRTH_CUST_clean;
        string _TXT_CUST_SEX_clean;
        double __Similarity_NAM_CUST_FULL;
        DateTime _LoadDate;
        string _CustomerMisCode;
        bool _IsGolden;
        bool _Active;

        public int CUSTOMER_PERSISTENT_ID
        {
            get { return _CUSTOMER_PERSISTENT_ID; }
            set
            {
                if (_CUSTOMER_PERSISTENT_ID != value)
                {
                    _CUSTOMER_PERSISTENT_ID = value;
                    OnPropertyChanged(() => CUSTOMER_PERSISTENT_ID);
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

        public string BVN
        {
            get { return _BVN; }
            set
            {
                if (_BVN != value)
                {
                    _BVN = value;
                    OnPropertyChanged(() => BVN);
                }
            }
        }

        public string COD_ACCT_NO_DEFAULT
        {
            get { return _COD_ACCT_NO_DEFAULT; }
            set
            {
                if (_COD_ACCT_NO_DEFAULT != value)
                {
                    _COD_ACCT_NO_DEFAULT = value;
                    OnPropertyChanged(() => COD_ACCT_NO_DEFAULT);
                }
            }
        }

        public string DAT_CUST_OPEN
        {
            get { return _DAT_CUST_OPEN; }
            set
            {
                if (_DAT_CUST_OPEN != value)
                {
                    _DAT_CUST_OPEN = value;
                    OnPropertyChanged(() => DAT_CUST_OPEN);
                }
            }
        }

        public string FLG_STAFF
        {
            get { return _FLG_STAFF; }
            set
            {
                if (_FLG_STAFF != value)
                {
                    _FLG_STAFF = value;
                    OnPropertyChanged(() => FLG_STAFF);
                }
            }
        }

        public string FLG_LOC_GLOB
        {
            get { return _FLG_LOC_GLOB; }
            set
            {
                if (_FLG_LOC_GLOB != value)
                {
                    _FLG_LOC_GLOB = value;
                    OnPropertyChanged(() => FLG_LOC_GLOB);
                }
            }
        }

        public string COD_CC_HOMEBRN
        {
            get { return _COD_CC_HOMEBRN; }
            set
            {
                if (_COD_CC_HOMEBRN != value)
                {
                    _COD_CC_HOMEBRN = value;
                    OnPropertyChanged(() => COD_CC_HOMEBRN);
                }
            }
        }

        public string NAM_CUST_SHRT
        {
            get { return _NAM_CUST_SHRT; }
            set
            {
                if (_NAM_CUST_SHRT != value)
                {
                    _NAM_CUST_SHRT = value;
                    OnPropertyChanged(() => NAM_CUST_SHRT);
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

        public string NAM_CUST_LAST
        {
            get { return _NAM_CUST_LAST; }
            set
            {
                if (_NAM_CUST_LAST != value)
                {
                    _NAM_CUST_LAST = value;
                    OnPropertyChanged(() => NAM_CUST_LAST);
                }
            }
        }

        public string NAM_CUST_MID
        {
            get { return _NAM_CUST_MID; }
            set
            {
                if (_NAM_CUST_MID != value)
                {
                    _NAM_CUST_MID = value;
                    OnPropertyChanged(() => NAM_CUST_MID);
                }
            }
        }

        public string NAM_CUST_FIRST
        {
            get { return _NAM_CUST_FIRST; }
            set
            {
                if (_NAM_CUST_FIRST != value)
                {
                    _NAM_CUST_FIRST = value;
                    OnPropertyChanged(() => NAM_CUST_FIRST);
                }
            }
        }

        public int TXT_PROFESS_CAT
        {
            get { return _TXT_PROFESS_CAT; }
            set
            {
                if (_TXT_PROFESS_CAT != value)
                {
                    _TXT_PROFESS_CAT = value;
                    OnPropertyChanged(() => TXT_PROFESS_CAT);
                }
            }
        }

        public string TXT_CUSTADR_ADD1
        {
            get { return _TXT_CUSTADR_ADD1; }
            set
            {
                if (_TXT_CUSTADR_ADD1 != value)
                {
                    _TXT_CUSTADR_ADD1 = value;
                    OnPropertyChanged(() => TXT_CUSTADR_ADD1);
                }
            }
        }

        public string TXT_CUSTADR_ADD2
        {
            get { return _TXT_CUSTADR_ADD2; }
            set
            {
                if (_TXT_CUSTADR_ADD2 != value)
                {
                    _TXT_CUSTADR_ADD2 = value;
                    OnPropertyChanged(() => TXT_CUSTADR_ADD2);
                }
            }
        }

        public string TXT_CUSTADR_ADD3
        {
            get { return _TXT_CUSTADR_ADD3; }
            set
            {
                if (_TXT_CUSTADR_ADD3 != value)
                {
                    _TXT_CUSTADR_ADD3 = value;
                    OnPropertyChanged(() => TXT_CUSTADR_ADD3);
                }
            }
        }

        public string NAM_CUSTADR_CITY
        {
            get { return _NAM_CUSTADR_CITY; }
            set
            {
                if (_NAM_CUSTADR_CITY != value)
                {
                    _NAM_CUSTADR_CITY = value;
                    OnPropertyChanged(() => NAM_CUSTADR_CITY);
                }
            }
        }

        public string NAM_CUSTADR_STATE
        {
            get { return _NAM_CUSTADR_STATE; }
            set
            {
                if (_NAM_CUSTADR_STATE != value)
                {
                    _NAM_CUSTADR_STATE = value;
                    OnPropertyChanged(() => NAM_CUSTADR_STATE);
                }
            }
        }

        public string REF_CUST_PHONE
        {
            get { return _REF_CUST_PHONE; }
            set
            {
                if (_REF_CUST_PHONE != value)
                {
                    _REF_CUST_PHONE = value;
                    OnPropertyChanged(() => REF_CUST_PHONE);
                }
            }
        }

        public string REF_CUST_EMAIL
        {
            get { return _REF_CUST_EMAIL; }
            set
            {
                if (_REF_CUST_EMAIL != value)
                {
                    _REF_CUST_EMAIL = value;
                    OnPropertyChanged(() => REF_CUST_EMAIL);
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

        public int COD_CUST_MARSTAT
        {
            get { return _COD_CUST_MARSTAT; }
            set
            {
                if (_COD_CUST_MARSTAT != value)
                {
                    _COD_CUST_MARSTAT = value;
                    OnPropertyChanged(() => COD_CUST_MARSTAT);
                }
            }
        }

        public string NAM_CUST_SPOUSE
        {
            get { return _NAM_CUST_SPOUSE; }
            set
            {
                if (_NAM_CUST_SPOUSE != value)
                {
                    _NAM_CUST_SPOUSE = value;
                    OnPropertyChanged(() => NAM_CUST_SPOUSE);
                }
            }
        }

        public string TXT_SPOUSE_OCCPN
        {
            get { return _TXT_SPOUSE_OCCPN; }
            set
            {
                if (_TXT_SPOUSE_OCCPN != value)
                {
                    _TXT_SPOUSE_OCCPN = value;
                    OnPropertyChanged(() => TXT_SPOUSE_OCCPN);
                }
            }
        }

        public int CTR_DPNDT_CHLDRN
        {
            get { return _CTR_DPNDT_CHLDRN; }
            set
            {
                if (_CTR_DPNDT_CHLDRN != value)
                {
                    _CTR_DPNDT_CHLDRN = value;
                    OnPropertyChanged(() => CTR_DPNDT_CHLDRN);
                }
            }
        }


        public string TXT_CUST_NATNLTY
        {
            get { return _TXT_CUST_NATNLTY; }
            set
            {
                if (_TXT_CUST_NATNLTY != value)
                {
                    _TXT_CUST_NATNLTY = value;
                    OnPropertyChanged(() => TXT_CUST_NATNLTY);
                }
            }
        }

        public string DAT_INCORPORATED
        {
            get { return _DAT_INCORPORATED; }
            set
            {
                if (_DAT_INCORPORATED != value)
                {
                    _DAT_INCORPORATED = value;
                    OnPropertyChanged(() => DAT_INCORPORATED);
                }
            }
        }

        public string COD_BUSINESS_REGNO
        {
            get { return _COD_BUSINESS_REGNO; }
            set
            {
                if (_COD_BUSINESS_REGNO != value)
                {
                    _COD_BUSINESS_REGNO = value;
                    OnPropertyChanged(() => COD_BUSINESS_REGNO);
                }
            }
        }

        public string TXT_CUST_RESIDENCE
        {
            get { return _TXT_CUST_RESIDENCE; }
            set
            {
                if (_TXT_CUST_RESIDENCE != value)
                {
                    _TXT_CUST_RESIDENCE = value;
                    OnPropertyChanged(() => TXT_CUST_RESIDENCE);
                }
            }
        }

        public string DAT_LAST_MNT
        {
            get { return _DAT_LAST_MNT; }
            set
            {
                if (_DAT_LAST_MNT != value)
                {
                    _DAT_LAST_MNT = value;
                    OnPropertyChanged(() => DAT_LAST_MNT);
                }
            }
        }

        public string MIS_CODE
        {
            get { return _MIS_CODE; }
            set
            {
                if (_MIS_CODE != value)
                {
                    _MIS_CODE = value;
                    OnPropertyChanged(() => MIS_CODE);
                }
            }
        }

        public string RM_CODE
        {
            get { return _RM_CODE; }
            set
            {
                if (_RM_CODE != value)
                {
                    _RM_CODE = value;
                    OnPropertyChanged(() => RM_CODE);
                }
            }
        }

        public string RM_NAME
        {
            get { return _RM_NAME; }
            set
            {
                if (_RM_NAME != value)
                {
                    _RM_NAME = value;
                    OnPropertyChanged(() => RM_NAME);
                }
            }
        }

        public string PRODUCT_CODE
        {
            get { return _PRODUCT_CODE; }
            set
            {
                if (_PRODUCT_CODE != value)
                {
                    _PRODUCT_CODE = value;
                    OnPropertyChanged(() => PRODUCT_CODE);
                }
            }
        }

        public string PRODUCT_NAME
        {
            get { return _PRODUCT_NAME; }
            set
            {
                if (_PRODUCT_NAME != value)
                {
                    _PRODUCT_NAME = value;
                    OnPropertyChanged(() => PRODUCT_NAME);
                }
            }
        }

        public int IS_CARDABLE
        {
            get { return _IS_CARDABLE; }
            set
            {
                if (_IS_CARDABLE != value)
                {
                    _IS_CARDABLE = value;
                    OnPropertyChanged(() => IS_CARDABLE);
                }
            }
        }

        public string BUSINESS_DIVISION
        {
            get { return _BUSINESS_DIVISION; }
            set
            {
                if (_BUSINESS_DIVISION != value)
                {
                    _BUSINESS_DIVISION = value;
                    OnPropertyChanged(() => BUSINESS_DIVISION);
                }
            }
        }

        public string CUSTOMER_SEGMENT
        {
            get { return _CUSTOMER_SEGMENT; }
            set
            {
                if (_CUSTOMER_SEGMENT != value)
                {
                    _CUSTOMER_SEGMENT = value;
                    OnPropertyChanged(() => CUSTOMER_SEGMENT);
                }
            }
        }

        public string CUSTOMER_SUB_SEGMENT
        {
            get { return _CUSTOMER_SUB_SEGMENT; }
            set
            {
                if (_CUSTOMER_SUB_SEGMENT != value)
                {
                    _CUSTOMER_SUB_SEGMENT = value;
                    OnPropertyChanged(() => CUSTOMER_SUB_SEGMENT);
                }
            }
        }

        public string PRODUCT_SEGMENT
        {
            get { return _PRODUCT_SEGMENT; }
            set
            {
                if (_PRODUCT_SEGMENT != value)
                {
                    _PRODUCT_SEGMENT = value;
                    OnPropertyChanged(() => PRODUCT_SEGMENT);
                }
            }
        }

        public int IS_CARDHOLDER
        {
            get { return _IS_CARDHOLDER; }
            set
            {
                if (_IS_CARDHOLDER != value)
                {
                    _IS_CARDHOLDER = value;
                    OnPropertyChanged(() => IS_CARDHOLDER);
                }
            }
        }

        public int HAS_INTERNETBANKING
        {
            get { return _HAS_INTERNETBANKING; }
            set
            {
                if (_HAS_INTERNETBANKING != value)
                {
                    _HAS_INTERNETBANKING = value;
                    OnPropertyChanged(() => HAS_INTERNETBANKING);
                }
            }
        }

        public int HAS_MOBILEBANKING
        {
            get { return _HAS_MOBILEBANKING; }
            set
            {
                if (_HAS_MOBILEBANKING != value)
                {
                    _HAS_MOBILEBANKING = value;
                    OnPropertyChanged(() => HAS_MOBILEBANKING);
                }
            }
        }

        public string CREDIT_RATING
        {
            get { return _CREDIT_RATING; }
            set
            {
                if (_CREDIT_RATING != value)
                {
                    _CREDIT_RATING = value;
                    OnPropertyChanged(() => CREDIT_RATING);
                }
            }
        }

        public string CBN_SECTOR
        {
            get { return _CBN_SECTOR; }
            set
            {
                if (_CBN_SECTOR != value)
                {
                    _CBN_SECTOR = value;
                    OnPropertyChanged(() => CBN_SECTOR);
                }
            }
        }

        public string CBN_SUB_SECTOR
        {
            get { return _CBN_SUB_SECTOR; }
            set
            {
                if (_CBN_SUB_SECTOR != value)
                {
                    _CBN_SUB_SECTOR = value;
                    OnPropertyChanged(() => CBN_SUB_SECTOR);
                }
            }
        }

        public int AGE
        {
            get { return _AGE; }
            set
            {
                if (_AGE != value)
                {
                    _AGE = value;
                    OnPropertyChanged(() => AGE);
                }
            }
        }

        public decimal AMOUNT
        {
            get { return _AMOUNT; }
            set
            {
                if (_AMOUNT != value)
                {
                    _AMOUNT = value;
                    OnPropertyChanged(() => AMOUNT);
                }
            }
        }

        public int TRANS_COUNT
        {
            get { return _TRANS_COUNT; }
            set
            {
                if (_TRANS_COUNT != value)
                {
                    _TRANS_COUNT = value;
                    OnPropertyChanged(() => TRANS_COUNT);
                }
            }
        }

        public int ACCOUNTCOUNT
        {
            get { return _ACCOUNTCOUNT; }
            set
            {
                if (_ACCOUNTCOUNT != value)
                {
                    _ACCOUNTCOUNT = value;
                    OnPropertyChanged(() => ACCOUNTCOUNT);
                }
            }
        }

        public int IS_MERCHANTABLE
        {
            get { return _IS_MERCHANTABLE; }
            set
            {
                if (_IS_MERCHANTABLE != value)
                {
                    _IS_MERCHANTABLE = value;
                    OnPropertyChanged(() => IS_MERCHANTABLE);
                }
            }
        }


        public int IS_MERCHANT
        {
            get { return _IS_MERCHANT; }
            set
            {
                if (_IS_MERCHANT != value)
                {
                    _IS_MERCHANT = value;
                    OnPropertyChanged(() => IS_MERCHANT);
                }
            }
        }

        public string CUSTOMER_TYPE
        {
            get { return _CUSTOMER_TYPE; }
            set
            {
                if (_CUSTOMER_TYPE != value)
                {
                    _CUSTOMER_TYPE = value;
                    OnPropertyChanged(() => CUSTOMER_TYPE);
                }
            }
        }

        public int MAXIMUM_AGE
        {
            get { return _MAXIMUM_AGE; }
            set
            {
                if (_MAXIMUM_AGE != value)
                {
                    _MAXIMUM_AGE = value;
                    OnPropertyChanged(() => MAXIMUM_AGE);
                }
            }
        }

        public int MINIMUM_AGE
        {
            get { return _MINIMUM_AGE; }
            set
            {
                if (_MINIMUM_AGE != value)
                {
                    _MINIMUM_AGE = value;
                    OnPropertyChanged(() => MINIMUM_AGE);
                }
            }
        }

        public int PRODUCTSTATUS
        {
            get { return _PRODUCTSTATUS; }
            set
            {
                if (_PRODUCTSTATUS != value)
                {
                    _PRODUCTSTATUS = value;
                    OnPropertyChanged(() => PRODUCTSTATUS);
                }
            }
        }

        public string CUST_FULL_NAME_TITLE
        {
            get { return _CUST_FULL_NAME_TITLE; }
            set
            {
                if (_CUST_FULL_NAME_TITLE != value)
                {
                    _CUST_FULL_NAME_TITLE = value;
                    OnPropertyChanged(() => CUST_FULL_NAME_TITLE);
                }
            }
        }

        public string cust_birthdate_status
        {
            get { return _cust_birthdate_status; }
            set
            {
                if (_cust_birthdate_status != value)
                {
                    _cust_birthdate_status = value;
                    OnPropertyChanged(() => cust_birthdate_status);
                }
            }
        }

        public string PRODUCT_STATUS
        {
            get { return _PRODUCT_STATUS; }
            set
            {
                if (_PRODUCT_STATUS != value)
                {
                    _PRODUCT_STATUS = value;
                    OnPropertyChanged(() => PRODUCT_STATUS);
                }
            }
        }

        public string CUST_FULL_ADDRESS
        {
            get { return _CUST_FULL_ADDRESS; }
            set
            {
                if (_CUST_FULL_ADDRESS != value)
                {
                    _CUST_FULL_ADDRESS = value;
                    OnPropertyChanged(() => CUST_FULL_ADDRESS);
                }
            }
        }

        public string DERIVED_STATE
        {
            get { return _DERIVED_STATE; }
            set
            {
                if (_DERIVED_STATE != value)
                {
                    _DERIVED_STATE = value;
                    OnPropertyChanged(() => DERIVED_STATE);
                }
            }
        }

        public string PostalCode
        {
            get { return _PostalCode; }
            set
            {
                if (_PostalCode != value)
                {
                    _PostalCode = value;
                    OnPropertyChanged(() => PostalCode);
                }
            }
        }

        public string LGA
        {
            get { return _LGA; }
            set
            {
                if (_LGA != value)
                {
                    _LGA = value;
                    OnPropertyChanged(() => LGA);
                }
            }
        }

        public string NAM_CUSTADR_CNTRY
        {
            get { return _NAM_CUSTADR_CNTRY; }
            set
            {
                if (_NAM_CUSTADR_CNTRY != value)
                {
                    _NAM_CUSTADR_CNTRY = value;
                    OnPropertyChanged(() => NAM_CUSTADR_CNTRY);
                }
            }
        }

        public string country_status
        {
            get { return _country_status; }
            set
            {
                if (_country_status != value)
                {
                    _country_status = value;
                    OnPropertyChanged(() => country_status);
                }
            }
        }

        public string email_status
        {
            get { return _email_status; }
            set
            {
                if (_email_status != value)
                {
                    _email_status = value;
                    OnPropertyChanged(() => email_status);
                }
            }
        }

        public string gender_status
        {
            get { return _gender_status; }
            set
            {
                if (_gender_status != value)
                {
                    _gender_status = value;
                    OnPropertyChanged(() => gender_status);
                }
            }
        }

        public string TXT_CUST_PREFIX
        {
            get { return _TXT_CUST_PREFIX; }
            set
            {
                if (_TXT_CUST_PREFIX != value)
                {
                    _TXT_CUST_PREFIX = value;
                    OnPropertyChanged(() => TXT_CUST_PREFIX);
                }
            }
        }

        public string prefix_status
        {
            get { return _prefix_status; }
            set
            {
                if (_prefix_status != value)
                {
                    _prefix_status = value;
                    OnPropertyChanged(() => prefix_status);
                }
            }
        }

        public string DERIVED_GENDER
        {
            get { return _DERIVED_GENDER; }
            set
            {
                if (_DERIVED_GENDER != value)
                {
                    _DERIVED_GENDER = value;
                    OnPropertyChanged(() => DERIVED_GENDER);
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

        public int _key_in
        {
            get { return __key_in; }
            set
            {
                if (__key_in != value)
                {
                    __key_in = value;
                    OnPropertyChanged(() => _key_in);
                }
            }
        }

        public int _key_out
        {
            get { return __key_out; }
            set
            {
                if (__key_out != value)
                {
                    __key_out = value;
                    OnPropertyChanged(() => _key_out);
                }
            }
        }

        public double _score
        {
            get { return __score; }
            set
            {
                if (__score != value)
                {
                    __score = value;
                    OnPropertyChanged(() => _score);
                }
            }
        }

        public string NAM_CUST_FULL_clean
        {
            get { return _NAM_CUST_FULL_clean; }
            set
            {
                if (_NAM_CUST_FULL_clean != value)
                {
                    _NAM_CUST_FULL_clean = value;
                    OnPropertyChanged(() => NAM_CUST_FULL_clean);
                }
            }
        }

        public string DAT_BIRTH_CUST_clean
        {
            get { return _DAT_BIRTH_CUST_clean; }
            set
            {
                if (_DAT_BIRTH_CUST_clean != value)
                {
                    _DAT_BIRTH_CUST_clean = value;
                    OnPropertyChanged(() => DAT_BIRTH_CUST_clean);
                }
            }
        }

        public string TXT_CUST_SEX_clean
        {
            get { return _TXT_CUST_SEX_clean; }
            set
            {
                if (_TXT_CUST_SEX_clean != value)
                {
                    _TXT_CUST_SEX_clean = value;
                    OnPropertyChanged(() => TXT_CUST_SEX_clean);
                }
            }
        }

        public double _Similarity_NAM_CUST_FULL
        {
            get { return __Similarity_NAM_CUST_FULL; }
            set
            {
                if (__Similarity_NAM_CUST_FULL != value)
                {
                    __Similarity_NAM_CUST_FULL = value;
                    OnPropertyChanged(() => _Similarity_NAM_CUST_FULL);
                }
            }
        }

        public string CustomerMisCode
        {
            get { return _CustomerMisCode; }
            set
            {
                if (_CustomerMisCode != value)
                {
                    _CustomerMisCode = value;
                    OnPropertyChanged(() => CustomerMisCode);
                }
            }
        }

        public bool IsGolden
        {
            get { return _IsGolden; }
            set
            {
                if (_IsGolden != value)
                {
                    _IsGolden = value;
                    OnPropertyChanged(() => IsGolden);
                }
            }
        }
        public DateTime LoadDate
        {
            get { return _LoadDate; }
            set
            {
                if (_LoadDate != value)
                {
                    _LoadDate = value;
                    OnPropertyChanged(() => LoadDate);
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


        class CDQMCustomerPersistentValidator : AbstractValidator<CDQMCustomerPersistent>
        {
            public CDQMCustomerPersistentValidator()
            {

            }
        }

        protected override IValidator GetValidator()
        {
            return new CDQMCustomerPersistentValidator();
        }
    }
}
