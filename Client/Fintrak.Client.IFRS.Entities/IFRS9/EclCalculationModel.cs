using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class EclCalculationModel : ObjectBase
    {
        int _EclModelId;
        string _Code;
        string _Description;
        bool _Active;

        public int EclModelId
        {
            get { return _EclModelId; }
            set
            {
                if (_EclModelId != value)
                {
                    _EclModelId = value;
                    OnPropertyChanged(() => EclModelId);
                }
            }
        }

        public string Code
        {
            get { return _Code; }
            set
            {
                if (_Code != value)
                {
                    _Code = value;
                    OnPropertyChanged(() => Code);
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


        class EclCalculationModelValidator : AbstractValidator<EclCalculationModel>
        {
            public EclCalculationModelValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new EclCalculationModelValidator();
        }
    }
}
