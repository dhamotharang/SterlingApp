using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class ManagementTree : ObjectBase
    {
        int _ManagementTreeId;
        string _AccountNo;
        string _TeamDefinitionCode;
        string _TeamCode;
        string _AccountOfficerDefinitionCode;
        string _AccountOfficerCode;
        double _Rate;
        string _Year;
        string _CompanyCode;
        bool _Active;

        public int ManagementTreeId
        {
            get { return _ManagementTreeId; }
            set
            {
                if (_ManagementTreeId != value)
                {
                    _ManagementTreeId = value;
                    OnPropertyChanged(() => ManagementTreeId);
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


        public string TeamDefinitionCode
        {
            get { return _TeamDefinitionCode; }
            set
            {
                if (_TeamDefinitionCode != value)
                {
                    _TeamDefinitionCode = value;
                    OnPropertyChanged(() => TeamDefinitionCode);
                }
            }
        }

        public string TeamCode
        {
            get { return _TeamCode; }
            set
            {
                if (_TeamCode != value)
                {
                    _TeamCode = value;
                    OnPropertyChanged(() => TeamCode);
                }
            }
        }

        public string AccountOfficerDefinitionCode
        {
            get { return _AccountOfficerDefinitionCode; }
            set
            {
                if (_AccountOfficerDefinitionCode != value)
                {
                    _AccountOfficerDefinitionCode = value;
                    OnPropertyChanged(() => AccountOfficerDefinitionCode);
                }
            }
        }

        public string AccountOfficerCode
        {
            get { return _AccountOfficerCode; }
            set
            {
                if (_AccountOfficerCode != value)
                {
                    _AccountOfficerCode = value;
                    OnPropertyChanged(() => AccountOfficerCode);
                }
            }
        }

        public double Rate
        {
            get { return _Rate; }
            set
            {
                if (_Rate != value)
                {
                    _Rate = value;
                    OnPropertyChanged(() => Rate);
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


        class ManagementTreeValidator : AbstractValidator<ManagementTree>
        {
            public ManagementTreeValidator()
            {
                RuleFor(obj => obj.AccountNo).NotEmpty().WithMessage("Account No is required.");
                RuleFor(obj => obj.AccountOfficerCode).NotEmpty().WithMessage("Account Officer is required.");
                RuleFor(obj => obj.TeamCode).NotEmpty().WithMessage("Team is required.");
                RuleFor(obj => obj.Rate).GreaterThan(0).WithMessage("Rate is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ManagementTreeValidator();
        }
    }
}
