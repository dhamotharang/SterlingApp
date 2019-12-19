using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Core.Entities
{
    public class Extraction : ObjectBase
    {
        int _ExtractionId;
        string _Title;
        string _PackageName;
        string _PackagePath;
        string _ProcedureName;
        string _Script;
        string _Solution;

        public int ExtractionId
        {
            get { return _ExtractionId; }
            set
            {
                if (_ExtractionId != value)
                {
                    _ExtractionId = value;
                    OnPropertyChanged(() => ExtractionId);
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

        public string PackageName
        {
            get { return _PackageName; }
            set
            {
                if (_PackageName != value)
                {
                    _PackageName = value;
                    OnPropertyChanged(() => PackageName);
                }
            }
        }

        public string PackagePath
        {
            get { return _PackagePath; }
            set
            {
                if (_PackagePath != value)
                {
                    _PackagePath = value;
                    OnPropertyChanged(() => PackagePath);
                }
            }
        }

        public string ProcedureName
        {
            get { return _ProcedureName; }
            set
            {
                if (_ProcedureName != value)
                {
                    _ProcedureName = value;
                    OnPropertyChanged(() => ProcedureName);
                }
            }
        }

        public string Script
        {
            get { return _Script; }
            set
            {
                if (_Script != value)
                {
                    _Script = value;
                    OnPropertyChanged(() => Script);
                }
            }
        }

        public string Solution
        {
            get { return _Solution; }
            set
            {
                if (_Solution != value)
                {
                    _Solution = value;
                    OnPropertyChanged(() => Solution);
                }
            }
        }

        //public string LongDescription
        //{
        //    get
        //    {
        //        return string.Format("{0} - {1}", _Name, _Alias);
        //    }
        //}

        class ExtractionValidator : AbstractValidator<Extraction>
        {
            public ExtractionValidator()
            {
                RuleFor(obj => obj.Title).NotEmpty().WithMessage("Title must not be empty.");
                RuleFor(obj => obj.PackageName).NotEmpty().WithMessage("PackageName must not be empty.");
                RuleFor(obj => obj.ProcedureName).NotEmpty().WithMessage("ProcedureName must not be empty.");
                RuleFor(obj => obj.Solution).NotEmpty().WithMessage("Solution must not be empty.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ExtractionValidator();
        }
    }
}
