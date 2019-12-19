using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class PostingDetail : ObjectBase
    {
        int _PostingDetailId;
        string _TransDescription;
        string _Indicator;
        string _GLCode;
        decimal _GAAPAmount;
        decimal _IFRSAmount;
        DateTime _RunDate;
        string _CompanyCode;
        bool _Active;

        public int PostingDetailId
        {
            get { return _PostingDetailId; }
            set
            {
                if (_PostingDetailId != value)
                {
                    _PostingDetailId = value;
                    OnPropertyChanged(() => PostingDetailId);
                }
            }
        }
   

        public string TransDescription
        {
            get { return _TransDescription; }
            set
            {
                if (_TransDescription != value)
                {
                    _TransDescription = value;
                    OnPropertyChanged(() => TransDescription);
                }
            }
        }

        public string Indicator
        {
            get { return _Indicator; }
            set
            {
                if (_Indicator != value)
                {
                    _Indicator = value;
                    OnPropertyChanged(() => Indicator);
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

        public decimal GAAPAmount
        {
            get { return _GAAPAmount; }
            set
            {
                if (_GAAPAmount != value)
                {
                    _GAAPAmount = value;
                    OnPropertyChanged(() => GAAPAmount);
                }
            }
        }

        public decimal IFRSAmount
        {
            get { return _IFRSAmount; }
            set
            {
                if (_IFRSAmount != value)
                {
                    _IFRSAmount = value;
                    OnPropertyChanged(() => IFRSAmount);
                }
            }
        }

       

        public DateTime RunDate
        {
            get { return _RunDate; }
            set
            {
                if (_RunDate != value)
                {
                    _RunDate = value;
                    OnPropertyChanged(() => RunDate);
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


        
        class PostingDetailValidator : AbstractValidator<PostingDetail>
        {
            public PostingDetailValidator()
            {
               
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new PostingDetailValidator();
        }
    }
}
