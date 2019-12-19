using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Core.Entities
{
    public class PackageSetup : ObjectBase
    {
        int _PackageSetupId;
        string _ExtractionPath;
        string _ProcessPath;
        bool _Active;

        public int PackageSetupId
        {
            get { return _PackageSetupId; }
            set
            {
                if (_PackageSetupId != value)
                {
                    _PackageSetupId = value;
                    OnPropertyChanged(() => PackageSetupId);
                }
            }
        }

        public string ExtractionPath
        {
            get { return _ExtractionPath; }
            set
            {
                if (_ExtractionPath != value)
                {
                    _ExtractionPath = value;
                    OnPropertyChanged(() => ExtractionPath);
                }
            }
        }

        public string ProcessPath
        {
            get { return _ProcessPath; }
            set
            {
                if (_ProcessPath != value)
                {
                    _ProcessPath = value;
                    OnPropertyChanged(() => ProcessPath);
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

        class PackageSetupValidator : AbstractValidator<PackageSetup>
        {
            public PackageSetupValidator()
            {
                
            }
        }

        protected override IValidator GetValidator()
        {
            return new PackageSetupValidator();
        }
    }
}
