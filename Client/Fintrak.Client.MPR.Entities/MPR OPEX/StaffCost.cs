using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class StaffCost : ObjectBase
    {
        int _StaffCostId;
        string _EmployeeCode;
        string _EmployeeName;
        string _Level;
        string _BranchCode;
        decimal _Amount;
        string _MISCode;
        bool _Active;


        public int StaffCostId
        {
            get { return _StaffCostId; }
            set
            {
                if (_StaffCostId != value)
                {
                    _StaffCostId = value;
                    OnPropertyChanged(() => StaffCostId);
                }
            }
        }

        public string EmployeeCode
        {
            get { return _EmployeeCode; }
            set
            {
                if (_EmployeeCode != value)
                {
                    _EmployeeCode = value;
                    OnPropertyChanged(() => EmployeeCode);
                }
            }
        }

        public string EmployeeName
        {
            get { return _EmployeeName; }
            set
            {
                if (_EmployeeName != value)
                {
                    _EmployeeName = value;
                    OnPropertyChanged(() => EmployeeName);
                }
            }
        }

        public string Level
        {
            get { return _Level; }
            set
            {
                if (_Level != value)
                {
                    _Level = value;
                    OnPropertyChanged(() => Level);
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

        public decimal Amount
        {
            get { return _Amount; }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    OnPropertyChanged(() => Amount);
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


        
        class StaffCostValidator : AbstractValidator<StaffCost>
        {
            public StaffCostValidator()
            {
                RuleFor(obj => obj.EmployeeCode).NotEmpty().WithMessage("EmployeeCode is required.");               
             }
        }

        protected override IValidator GetValidator()
        {
            return new StaffCostValidator();
        }
    }
}
