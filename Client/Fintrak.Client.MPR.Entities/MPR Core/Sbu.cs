using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class Sbu : ObjectBase
    {
        int _SbuId;
        string _SbuCode;
        string _Description;
        bool _Active;


        public int SbuId
        {
            get { return _SbuId; }
            set
            {
                if (_SbuId != value)
                {
                    _SbuId = value;
                    OnPropertyChanged(() => SbuId);
                }
            }
        }


        public string SbuCode
        {
            get { return _SbuCode; }
            set
            {
                if (_SbuCode != value)
                {
                    _SbuCode = value;
                    OnPropertyChanged(() => SbuCode);
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


        class SbuValidator : AbstractValidator<Sbu>
        {
            public SbuValidator()
            {
                RuleFor(obj => obj.Description).NotEmpty().WithMessage("Description is required.");
                RuleFor(obj => obj.SbuCode).NotEmpty().WithMessage("Sbu Code is required.");


            }
        }

        protected override IValidator GetValidator()
        {
            return new SbuValidator();
        }
    }
}
