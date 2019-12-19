using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class LowCostRemap : ObjectBase
    {
        int _LowCostRemapId;
        string _LowCostItem;
        string _Remmaped;
        string _FreqLevel;
        bool _Active;


        public int LowCostRemapId
        {
            get { return _LowCostRemapId; }
            set
            {
                if (_LowCostRemapId != value)
                {
                    _LowCostRemapId = value;
                    OnPropertyChanged(() => LowCostRemapId);
                }
            }
        }

        public string LowCostItem
        {
            get { return _LowCostItem; }
            set
            {
                if (_LowCostItem != value)
                {
                    _LowCostItem = value;
                    OnPropertyChanged(() => LowCostItem);
                }
            }
        }

        public string Remmaped
        {
            get { return _Remmaped; }
            set
            {
                if (_Remmaped != value)
                {
                    _Remmaped = value;
                    OnPropertyChanged(() => Remmaped);
                }
            }
        }


        public string FreqLevel
        {
            get { return _FreqLevel; }
            set
            {
                if (_FreqLevel != value)
                {
                    _FreqLevel = value;
                    OnPropertyChanged(() => FreqLevel);
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



        class LowCostRemapValidator : AbstractValidator<LowCostRemap>
        {
            public LowCostRemapValidator()
            {
                RuleFor(obj => obj.LowCostItem).NotEmpty().WithMessage("LowCostItem is required.");
                RuleFor(obj => obj.Remmaped).NotEmpty().WithMessage("Remmaped is required.");
                RuleFor(obj => obj.FreqLevel).NotEmpty().WithMessage("Freq Level is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new LowCostRemapValidator();
        }
    }
}
