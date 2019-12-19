using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class MemoAccountMap : ObjectBase
    {
        int _MemoAccountMapId;
        string _Code;
        string _AccountNo;
        bool _Active;

        public int MemoAccountMapId
        {
            get { return _MemoAccountMapId; }
            set
            {
                if (_MemoAccountMapId != value)
                {
                    _MemoAccountMapId = value;
                    OnPropertyChanged(() => MemoAccountMapId);
                }
            }
        }

        public string Code
        {
            get { return _Code; }
            set
            {
                if (_Code != value)
                {
                    _Code = value;
                    OnPropertyChanged(() => Code);
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

        
        class MemoAccountMapValidator : AbstractValidator<MemoAccountMap>
        {
            public MemoAccountMapValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.AccountNo).NotEmpty().WithMessage("AccountNo is required."); 
            }
        }

        protected override IValidator GetValidator()
        {
            return new MemoAccountMapValidator();
        }
    }
}
