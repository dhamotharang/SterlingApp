using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class SetUp : ObjectBase
    {
        int _SetUpId;
        string _Parameter;
        string _Type;
        string _Value;
        //double? _Value_Number;
        //DateTime? _Value_Date;
        string _Code;
        bool _Active;

        public int SetUpId
        {
            get { return _SetUpId; }
            set
            {
                if (_SetUpId != value)
                {
                    _SetUpId = value;
                    OnPropertyChanged(() => SetUpId);
                }
            }
        }
        public string Parameter
        {
            get { return _Parameter; }
            set
            {
                if (_Parameter != value)
                {
                    _Parameter = value;
                    OnPropertyChanged(() => Parameter);
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
        //public double? Value_Number
        //{
        //    get { return _Value_Number; }
        //    set
        //    {
        //        if (_Value_Number != value)
        //        {
        //            _Value_Number = value;
        //            OnPropertyChanged(() => Value_Number);
        //        }
        //    }
        //}
        //public DateTime? Value_Date
        //{
        //    get { return _Value_Date; }
        //    set
        //    {
        //        if (_Value_Date != value)
        //        {
        //            _Value_Date = value;
        //            OnPropertyChanged(() => Value_Date);
        //        }
        //    }
        //}
        public string Value
        {
            get { return _Value; }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    OnPropertyChanged(() => Value);
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


        class SetUpValidator : AbstractValidator<SetUp>
        {
            public SetUpValidator()
            {
                //RuleFor(obj => obj.Threshold).NotEmpty().WithMessage("Threshold is required.");
                //RuleFor(obj => obj.Classification_Type).NotEmpty().WithMessage("Classification Type is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new SetUpValidator();
        }
    }
}
