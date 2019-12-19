using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class PostingGLMapping : ObjectBase
    {
        int _ID;
        string _BS_GL;
        string _PL_GL;
        string _BS_Description;
        string _PL_Description;
        string _ProductCategory;
        string _Classification;
        bool _Active;

        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }

        public string BS_GL
        {
            get { return _BS_GL; }
            set
            {
                if (_BS_GL != value)
                {
                    _BS_GL = value;
                    OnPropertyChanged(() => BS_GL);
                }
            }
        }

        public string PL_GL
        {
            get { return _PL_GL; }
            set
            {
                if (_PL_GL != value)
                {
                    _PL_GL = value;
                    OnPropertyChanged(() => PL_GL);
                }
            }
        }

        public string BS_Description
        {
            get { return _BS_Description; }
            set
            {
                if (_BS_Description != value)
                {
                    _BS_Description = value;
                    OnPropertyChanged(() => BS_Description);
                }
            }
        }

        public string PL_Description
        {
            get { return _PL_Description; }
            set
            {
                if (_PL_Description != value)
                {
                    _PL_Description = value;
                    OnPropertyChanged(() => PL_Description);
                }
            }
        }

        public string ProductCategory
        {
            get { return _ProductCategory; }
            set
            {
                if (_ProductCategory != value)
                {
                    _ProductCategory = value;
                    OnPropertyChanged(() => ProductCategory);
                }
            }
        }

        public string Classification
        {
            get { return _Classification; }
            set
            {
                if (_Classification != value)
                {
                    _Classification = value;
                    OnPropertyChanged(() => Classification);
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


        class PostingGLMappingValidator : AbstractValidator<PostingGLMapping>
        {
            public PostingGLMappingValidator()
            {
                //RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new PostingGLMappingValidator();
        }
    }
}
