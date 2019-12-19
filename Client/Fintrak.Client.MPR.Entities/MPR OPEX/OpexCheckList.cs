using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class OpexCheckList : ObjectBase
    {
        int _OpexCheckListId;
        string _Type;
        string _Source;
        string _Caption;
        decimal _Actual;
        bool _Active;

        public int OpexCheckListId
        {
            get { return _OpexCheckListId; }
            set
            {
                if (_OpexCheckListId != value)
                {
                    _OpexCheckListId = value;
                    OnPropertyChanged(() => OpexCheckListId);
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

        public string Source
        {
            get { return _Source; }
            set
            {
                if (_Source != value)
                {
                    _Source = value;
                    OnPropertyChanged(() => Source);
                }
            }
        }

        public string Caption
        {
            get { return _Caption; }
            set
            {
                if (_Caption != value)
                {
                    _Caption = value;
                    OnPropertyChanged(() => Caption);
                }
            }
        }

        public decimal Actual
        {
            get { return _Actual; }
            set
            {
                if (_Actual != value)
                {
                    _Actual = value;
                    OnPropertyChanged(() => Actual);
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

        //public string LongDescription
        //{
        //    get
        //    {
        //        return string.Format("{0} - {1}", _Type, _Source);
        //    }
        //}

        
        class OpexCheckListValidator : AbstractValidator<OpexCheckList>
        {
            public OpexCheckListValidator()
            {
                RuleFor(obj => obj.Type).NotEmpty().WithMessage("Type is required.");
                RuleFor(obj => obj.Source).NotEmpty().WithMessage("Source is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new OpexCheckListValidator();
        }
    }
}
