using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class OpexVolumeBasedRate : ObjectBase
    {
        int _OpexVolumeBasedRateId;
        string _DefintionCode;
        string _MisCode;
        string _ItemCode;
        CenterTypeEnum _CenterType;
        string _ReviewCode; 
        string _Year;
        bool _Active;

        public int OpexVolumeBasedRateId
        {
            get { return _OpexVolumeBasedRateId; }
            set
            {
                if (_OpexVolumeBasedRateId != value)
                {
                    _OpexVolumeBasedRateId = value;
                    OnPropertyChanged(() => OpexVolumeBasedRateId);
                }
            }
        }

        public string DefintionCode
        {
            get { return _DefintionCode; }
            set
            {
                if (_DefintionCode != value)
                {
                    _DefintionCode = value;
                    OnPropertyChanged(() => DefintionCode);
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


        public string ItemCode
        {
            get { return _ItemCode; }
            set
            {
                if (_ItemCode != value)
                {
                    _ItemCode = value;
                    OnPropertyChanged(() => ItemCode);
                }
            }
        }


        public CenterTypeEnum CenterType
        {
            get { return _CenterType; }
            set
            {
                if (_CenterType != value)
                {
                    _CenterType = value;
                    OnPropertyChanged(() => CenterType);
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


        public string ReviewCode
        {
            get { return _ReviewCode; }
            set
            {
                if (_ReviewCode != value)
                {
                    _ReviewCode = value;
                    OnPropertyChanged(() => ReviewCode);
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

        public string LongDescription
        {
            get
            {
                return string.Format("{0} - {1}", _DefintionCode, _Year);
            }
        }

        
        class OpexVolumeBasedRateValidator : AbstractValidator<OpexVolumeBasedRate>
        {
            public OpexVolumeBasedRateValidator()
            {
                RuleFor(obj => obj.DefintionCode).NotEmpty().WithMessage("DefintionCode is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new OpexVolumeBasedRateValidator();
        }
    }
}
