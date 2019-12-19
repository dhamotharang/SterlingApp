using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class CheckList : ObjectBase
    {
        int _CheckListId;
        double _ACTUAL;
        string _SOURCE;
        string _type;
        string _CAPTION;
        bool _Active;


        public int CheckListId
        {
            get { return _CheckListId; }
            set
            {
                if (_CheckListId != value)
                {
                    _CheckListId = value;
                    OnPropertyChanged(() => CheckListId);
                }
            }
        }

        public double ACTUAL
        {
            get { return _ACTUAL; }
            set
            {
                if (_ACTUAL != value)
                {
                    _ACTUAL = value;
                    OnPropertyChanged(() => ACTUAL);
                }
            }
        }

        public string SOURCE
        {
            get { return _SOURCE; }
            set
            {
                if (_SOURCE != value)
                {
                    _SOURCE = value;
                    OnPropertyChanged(() => SOURCE);
                }
            }
        }


        public string type 
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged(() => type);
                }
            }
        }

        public string CAPTION
        {
            get { return _CAPTION; }
            set
            {
                if (_CAPTION != value)
                {
                    _CAPTION = value;
                    OnPropertyChanged(() => CAPTION);
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


        
        class CheckListValidator : AbstractValidator<CheckList>
        {
            public CheckListValidator()
            {

             }
        }

        protected override IValidator GetValidator()
        {
            return new CheckListValidator();
        }
    }
}
