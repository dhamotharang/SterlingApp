using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class SbuType : ObjectBase
    {
        int _SbuTypeId;
        string _Sbu;
        string _Type;
        bool _Active;

        public int SbuTypeId
        {
            get { return _SbuTypeId; }
            set
            {
                if (_SbuTypeId != value)
                {
                    _SbuTypeId = value;
                    OnPropertyChanged(() => SbuTypeId);
                }
            }
        }


        public string Sbu
        {
            get { return _Sbu; }
            set
            {
                if (_Sbu != value)
                {
                    _Sbu = value;
                    OnPropertyChanged(() => Sbu);
                }
            }
        }


        public string Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    OnPropertyChanged(() => Type);
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


        class SbuTypeValidator : AbstractValidator<SbuType>
        {
            public SbuTypeValidator()
            {
                RuleFor(obj => obj.Type).NotEmpty().WithMessage("Type is required.");
                RuleFor(obj => obj.Sbu).NotEmpty().WithMessage("Sbu is required.");




            }
        }

        protected override IValidator GetValidator()
        {
            return new SbuTypeValidator();
        }
    }
}
