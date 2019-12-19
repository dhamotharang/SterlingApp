using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.CDQM.Entities
{
    public class CDQMGenderGroup : ObjectBase
    {
        int _GenderGroupId;
        string _Title;
        string _GroupGender;     
        bool _Active;

        public int GenderGroupId
        {
            get { return _GenderGroupId; }
            set
            {
                if (_GenderGroupId != value)
                {
                    _GenderGroupId = value;
                    OnPropertyChanged(() => GenderGroupId);
                }
            }
        }

        public string Title
        {
            get { return _Title; }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    OnPropertyChanged(() => Title);
                }
            }
        }

        public string GroupGender
        {
            get { return _GroupGender; }
            set
            {
                if (_GroupGender != value)
                {
                    _GroupGender = value;
                    OnPropertyChanged(() => GroupGender);
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


        class CDQMGenderGroupValidator : AbstractValidator<CDQMGenderGroup>
        {
            public CDQMGenderGroupValidator()
            {

            }
        }

        protected override IValidator GetValidator()
        {
            return new CDQMGenderGroupValidator();
        }
    }
}
