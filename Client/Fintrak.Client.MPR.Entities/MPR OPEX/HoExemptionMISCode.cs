using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class HoExemptionMISCode : ObjectBase
    {
        int _Id;
        string _MIS_Code;      
        bool _Active;


        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(() => Id);
                }
            }
        }

        public string MIS_Code
        {
            get { return _MIS_Code; }
            set
            {
                if (_MIS_Code != value)
                {
                    _MIS_Code = value;
                    OnPropertyChanged(() => MIS_Code);
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


        
        class HoExemptionMISCodeValidator : AbstractValidator<HoExemptionMISCode>
        {
            public HoExemptionMISCodeValidator()
            {
                RuleFor(obj => obj.MIS_Code).NotEmpty().WithMessage("MISCode is required.");
          
             }
        }

        protected override IValidator GetValidator()
        {
            return new HoExemptionMISCodeValidator();
        }
    }
}
