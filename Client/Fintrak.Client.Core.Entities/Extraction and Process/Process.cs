using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.Core.Entities
{
    public class Process : ObjectBase
    {
        int _ProcessId;
        string _Title;
        PackageRunType _RunType;
        string _PackageName;
        string _PackagePath;
        string _ProcedureName;
        int _ModuleId;
        int _Position;
        bool _Active;

        public int ProcessId
        {
            get { return _ProcessId; }
            set
            {
                if (_ProcessId != value)
                {
                    _ProcessId = value;
                    OnPropertyChanged(() => ProcessId);
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

        public PackageRunType RunType
        {
            get { return _RunType; }
            set
            {
                if (_RunType != value)
                {
                    _RunType = value;
                    OnPropertyChanged(() => RunType);
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

        public int ModuleId
        {
            get { return _ModuleId; }
            set
            {
                if (_ModuleId != value)
                {
                    _ModuleId = value;
                    OnPropertyChanged(() => ModuleId);
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
        public string RunTypeName
        {
            get
            {
                return string.Format("{0}", _RunType.ToString());
            }
        }


        class ProcessValidator : AbstractValidator<Process>
        {
            public ProcessValidator()
            {
                RuleFor(obj => obj.Title).NotEmpty().WithMessage("Title must not be empty.");
                RuleFor(obj => obj.PackageName).NotEmpty().WithMessage("Package Name must not be empty.");
                RuleFor(obj => obj.PackagePath).NotEmpty().WithMessage("Package Path must not be empty.");
                RuleFor(obj => obj.ProcedureName).NotEmpty().WithMessage("Procedure Name must not be empty.");
                RuleFor(obj => obj.ModuleId).GreaterThan(0).WithMessage("Module is require.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProcessValidator();
        }
    }
}
