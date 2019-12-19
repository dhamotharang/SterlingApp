using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class Extraction : ObjectBase
    {
        int _ExtractionId;
        string _Title;
        string _PackageName;
        string _PackagePath;
        string _ProcedureName;
        string _ScriptText;
        int _SolutionId;
        int _Position;
        bool _Active;

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

        public string ScriptText
        {
            get { return _ScriptText; }
            set
            {
                if (_ScriptText != value)
                {
                    _ScriptText = value;
                    OnPropertyChanged(() => ScriptText);
                }
            }
        }

      

        public int SolutionId
        {
            get { return _SolutionId; }
            set
            {
                if (_SolutionId != value)
                {
                    _SolutionId = value;
                    OnPropertyChanged(() => SolutionId);
                }
            }
        }
        public int Position
        {
            get { return _Position; }
            set
            {
                if (_Position != value)
                {
                    _Position = value;
                    OnPropertyChanged(() => Position);
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

        public string LongDescription
        {
            get
            {
                return string.Format("{0}", _Title );
            }
        }

        class ExtractionValidator : AbstractValidator<Extraction>
        {
            public ExtractionValidator()
            {
                RuleFor(obj => obj.Title).NotEmpty().WithMessage("Title must not be empty.");
                RuleFor(obj => obj.PackageName).NotEmpty().WithMessage("Package Name must not be empty.");
                RuleFor(obj => obj.PackagePath).NotEmpty().WithMessage("Package Path must not be empty.");
                RuleFor(obj => obj.ProcedureName).NotEmpty().WithMessage("Procedure Name must not be empty.");
                RuleFor(obj => obj.ScriptText).NotEmpty().WithMessage("Script Text must not be empty.");
                RuleFor(obj => obj.SolutionId).GreaterThan(0).WithMessage("Solution is require.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ExtractionValidator();
        }
    }
}
