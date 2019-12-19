using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class MemoProductMap : ObjectBase
    {
        int _MemoProductMapId;
        string _Code;
        string _ProductCode;
        bool _Active;

        public int MemoProductMapId
        {
            get { return _MemoProductMapId; }
            set
            {
                if (_MemoProductMapId != value)
                {
                    _MemoProductMapId = value;
                    OnPropertyChanged(() => MemoProductMapId);
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

        
        class MemoProductMapValidator : AbstractValidator<MemoProductMap>
        {
            public MemoProductMapValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.ProductCode).NotEmpty().WithMessage("ProductCode is required."); 
            }
        }

        protected override IValidator GetValidator()
        {
            return new MemoProductMapValidator();
        }
    }
}
