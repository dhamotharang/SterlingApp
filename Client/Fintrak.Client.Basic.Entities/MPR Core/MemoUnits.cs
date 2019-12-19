using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class MemoUnits : ObjectBase
    {
        int _MemoUnitsId;
        string _Code;
        string _Name;
        string _CompanyCode;
        bool _Active;

        public int MemoUnitsId
        {
            get { return _MemoUnitsId; }
            set
            {
                if (_MemoUnitsId != value)
                {
                    _MemoUnitsId = value;
                    OnPropertyChanged(() => MemoUnitsId);
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


        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(() => Name);
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

        
        class MemoUnitsValidator : AbstractValidator<MemoUnits>
        {
            public MemoUnitsValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required."); 
            }
        }

        protected override IValidator GetValidator()
        {
            return new MemoUnitsValidator();
        }
    }
}
