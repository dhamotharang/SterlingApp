using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class AutoPostingTemplate : ObjectBase
    {
        int _AutoPostingTemplateId;
        string _Title;
        string _Action;
        bool _Active;

        public int AutoPostingTemplateId
        {
            get { return _AutoPostingTemplateId; }
            set
            {
                if (_AutoPostingTemplateId != value)
                {
                    _AutoPostingTemplateId = value;
                    OnPropertyChanged(() => AutoPostingTemplateId);
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

        public string Action
        {
            get { return _Action; }
            set
            {
                if (_Action != value)
                {
                    _Action = value;
                    OnPropertyChanged(() => Action);
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

        
        class AutoPostingTemplateValidator : AbstractValidator<AutoPostingTemplate>
        {
            public AutoPostingTemplateValidator()
            {
                RuleFor(obj => obj.Action).NotEmpty().WithMessage("Action is required.");
                RuleFor(obj => obj.Title).NotEmpty().WithMessage("Action is required.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new AutoPostingTemplateValidator();
        }
    }
}
