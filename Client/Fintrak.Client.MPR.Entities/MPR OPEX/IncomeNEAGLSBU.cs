using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeNEAGLSBU : ObjectBase
    {
        int _IncomeNEAGLSBUId;
        string _GLCode;
        string _SBU;
        bool _Active;


        public int IncomeNEAGLSBUId
        {
            get { return _IncomeNEAGLSBUId; }
            set
            {
                if (_IncomeNEAGLSBUId != value)
                {
                    _IncomeNEAGLSBUId = value;
                    OnPropertyChanged(() => IncomeNEAGLSBUId);
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



        public string SBU
        {
            get { return _SBU; }
            set
            {
                if (_SBU != value)
                {
                    _SBU = value;
                    OnPropertyChanged(() => SBU);
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



        class IncomeNEAGLSBUValidator : AbstractValidator<IncomeNEAGLSBU>
        {
            public IncomeNEAGLSBUValidator()
            {
                RuleFor(obj => obj.SBU).NotEmpty().WithMessage("SBU is required.");
                RuleFor(obj => obj.GLCode).NotEmpty().WithMessage("GL Code is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeNEAGLSBUValidator();
        }
    }
}
