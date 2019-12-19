using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class GLReclassification : ObjectBase
    {
        int _GLReclassificationId;
        string _GLAccount;
        string _CaptionCode;
        string _CompanyCode;
        bool _Active;


        public int GLReclassificationId
        {
            get { return _GLReclassificationId; }
            set
            {
                if (_GLReclassificationId != value)
                {
                    _GLReclassificationId = value;
                    OnPropertyChanged(() => GLReclassificationId);
                }
            }
        }

        public string GLAccount
        {
            get { return _GLAccount; }
            set
            {
                if (_GLAccount != value)
                {
                    _GLAccount = value;
                    OnPropertyChanged(() => GLAccount);
                }
            }
        }

        public string CaptionCode
        {
            get { return _CaptionCode; }
            set
            {
                if (_CaptionCode != value)
                {
                    _CaptionCode = value;
                    OnPropertyChanged(() => CaptionCode);
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


        
        class GLReclassificationValidator : AbstractValidator<GLReclassification>
        {
            public GLReclassificationValidator()
            {
                RuleFor(obj => obj.GLAccount).NotEmpty().WithMessage("GLAccount is required.");
                RuleFor(obj => obj.CaptionCode).NotEmpty().WithMessage("Caption is required.");
                RuleFor(obj => obj.CompanyCode).NotEmpty().WithMessage("Company is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new GLReclassificationValidator();
        }
    }
}
