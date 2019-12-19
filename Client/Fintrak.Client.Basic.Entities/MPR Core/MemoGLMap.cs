using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class MemoGLMap : ObjectBase
    {
        int _MemoGLMapId;
        string _Code;
        string _GLCode;
        bool _Active;

        public int MemoGLMapId
        {
            get { return _MemoGLMapId; }
            set
            {
                if (_MemoGLMapId != value)
                {
                    _MemoGLMapId = value;
                    OnPropertyChanged(() => MemoGLMapId);
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

        
        class MemoGLMapValidator : AbstractValidator<MemoGLMap>
        {
            public MemoGLMapValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.GLCode).NotEmpty().WithMessage("GLCode is required."); 
            }
        }

        protected override IValidator GetValidator()
        {
            return new MemoGLMapValidator();
        }
    }
}
