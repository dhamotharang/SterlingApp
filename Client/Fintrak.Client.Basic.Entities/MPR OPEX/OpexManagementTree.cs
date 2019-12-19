using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class OpexManagementTree : ObjectBase
    {
        int _OpexMgtTreeId;
        string _CostCentreMISCode;
        string _TeamDefinitionCode;
        string _TeamCode;
        string _AccountOfficerDefinitionCode;
        string _AccountOfficerCode;
        double _Ratio;
        bool _Active;


        public int OpexMgtTreeId
        {
            get { return _OpexMgtTreeId; }
            set
            {
                if (_OpexMgtTreeId != value)
                {
                    _OpexMgtTreeId = value;
                    OnPropertyChanged(() => OpexMgtTreeId);
                }
            }
        }

        public string CostCentreMISCode
        {
            get { return _CostCentreMISCode; }
            set
            {
                if (_CostCentreMISCode != value)
                {
                    _CostCentreMISCode = value;
                    OnPropertyChanged(() => CostCentreMISCode);
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

       

        public double Ratio
        {
            get { return _Ratio; }
            set
            {
                if (_Ratio != value)
                {
                    _Ratio = value;
                    OnPropertyChanged(() => Ratio);
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


        
        class OpexManagementTreeValidator : AbstractValidator<OpexManagementTree>
        {
            public OpexManagementTreeValidator()
            {
                RuleFor(obj => obj.CostCentreMISCode).NotEmpty().WithMessage("CostCentreMISCode is required.");               
             }
        }

        protected override IValidator GetValidator()
        {
            return new OpexManagementTreeValidator();
        }
    }
}
