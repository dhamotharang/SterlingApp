using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class BranchDefaultMIS : ObjectBase
    {
        int _BranchDefaultMISId;
        string _BranchCode;
        string _DefinitionCode;
        string _MisCode;
        string _Year;
        string _CompanyCode;
        bool _Active;

        public int BranchDefaultMISId
        {
            get { return _BranchDefaultMISId; }
            set
            {
                if (_BranchDefaultMISId != value)
                {
                    _BranchDefaultMISId = value;
                    OnPropertyChanged(() => BranchDefaultMISId);
                }
            }
        }

        public string BranchCode
        {
            get { return _BranchCode; }
            set
            {
                if (_BranchCode != value)
                {
                    _BranchCode = value;
                    OnPropertyChanged(() => BranchCode);
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


        public string MisCode
        {
            get { return _MisCode; }
            set
            {
                if (_MisCode != value)
                {
                    _MisCode = value;
                    OnPropertyChanged(() => MisCode);
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

        
        class BranchDefaultMISValidator : AbstractValidator<BranchDefaultMIS>
        {
            public BranchDefaultMISValidator()
            {
                RuleFor(obj => obj.BranchCode).NotEmpty().WithMessage("Branch Code is required.");
                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("MisCode is required.");
                RuleFor(obj => obj.DefinitionCode).NotEmpty().WithMessage("Team Definition Code is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new BranchDefaultMISValidator();
        }
    }
}
