using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class Servicese : ObjectBase
    {
        int _ServicesId;
        string _ServicesCode;
        string _Service;
        string _ServiceType;
        string _ServiceCat;
        double _Weight;
        bool _Active;

        public int ServicesId
        {
            get { return _ServicesId; }
            set
            {
                if (_ServicesId != value)
                {
                    _ServicesId = value;
                    OnPropertyChanged(() => ServicesId);
                }
            }
        }


        public string ServicesCode
        {
            get { return _ServicesCode; }
            set
            {
                if (_ServicesCode != value)
                {
                    _ServicesCode = value;
                    OnPropertyChanged(() => ServicesCode);
                }
            }
        }

        public string Service
        {
            get { return _Service; }
            set
            {
                if (_Service != value)
                {
                    _Service = value;
                    OnPropertyChanged(() => Service);
                }
            }
        }

        public string ServiceType
        {
            get { return _ServiceType; }
            set
            {
                if (_ServiceType != value)
                {
                    _ServiceType = value;
                    OnPropertyChanged(() => ServiceType);
                }
            }
        }

        public string ServiceCat
        {
            get { return _ServiceCat; }
            set
            {
                if (_ServiceCat != value)
                {
                    _ServiceCat = value;
                    OnPropertyChanged(() => ServiceCat);
                }
            }
        }

        public double Weight
        {
            get { return _Weight; }
            set
            {
                if (_Weight != value)
                {
                    _Weight = value;
                    OnPropertyChanged(() => Weight);
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



        class ServicesValidator : AbstractValidator<Servicese>
        {
            public ServicesValidator()
            {

                RuleFor(obj => obj.Service).NotEmpty().WithMessage("Service is required.");
                RuleFor(obj => obj.ServicesCode).NotEmpty().WithMessage("Services Code is required.");
                RuleFor(obj => obj.ServiceType).NotEmpty().WithMessage("Service Type is required.");
                RuleFor(obj => obj.ServiceCat).NotEmpty().WithMessage("Service Category is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new ServicesValidator();
        }
    }
}
