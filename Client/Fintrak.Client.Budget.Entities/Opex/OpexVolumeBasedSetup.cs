using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class OpexVolumeBasedSetup : ObjectBase
    {
        int _OpexVolumeBasedSetupId;
        string _OpexCode;
        string _ProductCode;
        string _ReviewCode; 
        string _Year;
        bool _Active;

        public int OpexVolumeBasedSetupId
        {
            get { return _OpexVolumeBasedSetupId; }
            set
            {
                if (_OpexVolumeBasedSetupId != value)
                {
                    _OpexVolumeBasedSetupId = value;
                    OnPropertyChanged(() => OpexVolumeBasedSetupId);
                }
            }
        }

        public string OpexCode
        {
            get { return _OpexCode; }
            set
            {
                if (_OpexCode != value)
                {
                    _OpexCode = value;
                    OnPropertyChanged(() => OpexCode);
                }
            }
        }


        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                if (_ProductCode != value)
                {
                    _ProductCode = value;
                    OnPropertyChanged(() => ProductCode);
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
                return string.Format("{0} - {1}", _OpexCode, _Year);
            }
        }

        
        class OpexVolumeBasedSetupValidator : AbstractValidator<OpexVolumeBasedSetup>
        {
            public OpexVolumeBasedSetupValidator()
            {
                RuleFor(obj => obj.OpexCode).NotEmpty().WithMessage("OpexCode is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new OpexVolumeBasedSetupValidator();
        }
    }
}
