using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class MISReplacement : ObjectBase
    {
        int _MISReplacementId;
        string _OldMISCode;
        string _DefinitionCode;
        string _MISCode;
        string _Year;
        string _CompanyCode;
        bool _Active;

        public int MISReplacementId
        {
            get { return _MISReplacementId; }
            set
            {
                if (_MISReplacementId != value)
                {
                    _MISReplacementId = value;
                    OnPropertyChanged(() => MISReplacementId);
                }
            }
        }

        public string OldMISCode
        {
            get { return _OldMISCode; }
            set
            {
                if (_OldMISCode != value)
                {
                    _OldMISCode = value;
                    OnPropertyChanged(() => OldMISCode);
                }
            }
        }

        public string DefinitionCode
        {
            get { return _DefinitionCode; }
            set
            {
                if (_DefinitionCode != value)
                {
                    _DefinitionCode = value;
                    OnPropertyChanged(() => DefinitionCode);
                }
            }
        }


        public string MISCode
        {
            get { return _MISCode; }
            set
            {
                if (_MISCode != value)
                {
                    _MISCode = value;
                    OnPropertyChanged(() => MISCode);
                }
            }
        }


        public string Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    OnPropertyChanged(() => Year);
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

        
        class MISReplacementValidator : AbstractValidator<MISReplacement>
        {
            public MISReplacementValidator()
            {
                RuleFor(obj => obj.OldMISCode).NotEmpty().WithMessage("OldMis Code is required.");
                RuleFor(obj => obj.MISCode).NotEmpty().WithMessage("MISCode Code is required.");
                //RuleFor(obj => obj.Year).NotEmpty().WithMessage("FiscalYear is required."); 
            }
        }

        protected override IValidator GetValidator()
        {
            return new MISReplacementValidator();
        }
    }
}
