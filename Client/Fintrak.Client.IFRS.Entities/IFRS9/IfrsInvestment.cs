using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsInvestment : ObjectBase
    {
        int _Id;
        int _INVType_Code;
        string _TypeCode;
        string _Description;
        double  _Rate;
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

        public int INVType_Code
        {
            get { return _INVType_Code; }
            set
            {
                if (_INVType_Code != value)
                {
                    _INVType_Code = value;
                    OnPropertyChanged(() => INVType_Code);
                }
            }
        }


    public string TypeCode
    {
      get { return _TypeCode; }
      set
      {
        if (_TypeCode != value)
        {
          _TypeCode = value;
          OnPropertyChanged(() => TypeCode);
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



    public double Rate
    {
      get { return _Rate; }
      set
      {
        if (_Rate != value)
        {
          _Rate = value;
          OnPropertyChanged(() => Rate);
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


        class IfrsInvestmentValidator : AbstractValidator<IfrsInvestment>
        {
            public IfrsInvestmentValidator()
            {
                RuleFor(obj => obj.TypeCode).NotEmpty().WithMessage("Type Code is required.");
                RuleFor(obj => obj.Description).NotEmpty().WithMessage("Description is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsInvestmentValidator();
        }
    }
}
