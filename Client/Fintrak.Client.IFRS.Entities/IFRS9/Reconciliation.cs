using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class Reconciliation : ObjectBase
    {
        int _ReconciliationId;
        string _Description;
        double _NCIAnnualGrossAmount;
        double _NCIAnnualAllowanceECL;
        double _NCILifetimeGrossAmount;
        double _NCILifetimeAllowanceECL;
        double _CILifetimeGrossAmount;
        double _CILifetimeAllowanceECL;
        double _TotalLifetimeGrossAmount;
        double _TotalLifetimeAllowanceECL;
        bool _Active;

        public int ReconciliationId
        {
            get { return _ReconciliationId; }
            set
            {
                if (_ReconciliationId != value)
                {
                    _ReconciliationId = value;
                    OnPropertyChanged(() => ReconciliationId);
                }
            }
        }

        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }

        public double NCIAnnualGrossAmount
        {
            get { return _NCIAnnualGrossAmount; }
            set
            {
                if (_NCIAnnualGrossAmount != value)
                {
                    _NCIAnnualGrossAmount = value;
                    OnPropertyChanged(() => NCIAnnualGrossAmount);
                }
            }
        }

        public double NCIAnnualAllowanceECL
        {
            get { return _NCIAnnualAllowanceECL; }
            set
            {
                if (_NCIAnnualAllowanceECL != value)
                {
                    _NCIAnnualAllowanceECL = value;
                    OnPropertyChanged(() => NCIAnnualAllowanceECL);
                }
            }
        }


        public double NCILifetimeGrossAmount
        {
            get { return _NCILifetimeGrossAmount; }
            set
            {
                if (_NCILifetimeGrossAmount != value)
                {
                    _NCILifetimeGrossAmount = value;
                    OnPropertyChanged(() => NCILifetimeGrossAmount);
                }
            }
        }

        public double NCILifetimeAllowanceECL
        {
            get { return _NCILifetimeAllowanceECL; }
            set
            {
                if (_NCILifetimeAllowanceECL != value)
                {
                    _NCILifetimeAllowanceECL = value;
                    OnPropertyChanged(() => NCILifetimeAllowanceECL);
                }
            }
        }


        public double CILifetimeGrossAmount
        {
            get { return _CILifetimeGrossAmount; }
            set
            {
                if (_CILifetimeGrossAmount != value)
                {
                    _CILifetimeGrossAmount = value;
                    OnPropertyChanged(() => CILifetimeGrossAmount);
                }
            }
        }


        public double CILifetimeAllowanceECL
        {
            get { return _CILifetimeAllowanceECL; }
            set
            {
                if (_CILifetimeAllowanceECL != value)
                {
                    _CILifetimeAllowanceECL = value;
                    OnPropertyChanged(() => CILifetimeAllowanceECL);
                }
            }
        }

        public double TotalLifetimeGrossAmount
        {
            get { return _TotalLifetimeGrossAmount; }
            set
            {
                if (_TotalLifetimeGrossAmount != value)
                {
                    _TotalLifetimeGrossAmount = value;
                    OnPropertyChanged(() => TotalLifetimeGrossAmount);
                }
            }
        }


        public double TotalLifetimeAllowanceECL
        {
            get { return _TotalLifetimeAllowanceECL; }
            set
            {
                if (_TotalLifetimeAllowanceECL != value)
                {
                    _TotalLifetimeAllowanceECL = value;
                    OnPropertyChanged(() => TotalLifetimeAllowanceECL);
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


        class ReconciliationValidator : AbstractValidator<Reconciliation>
        {
            public ReconciliationValidator()
            {
                RuleFor(obj => obj.Description).NotEmpty().WithMessage("Description is required.");
            } 
        }

        protected override IValidator GetValidator()
        {
            return new ReconciliationValidator();
        }
    }
}
