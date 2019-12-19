using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.CDQM.Entities
{
    public class CDQMETLConfiguration : ObjectBase
    {
        int _ETLConfigurationId;
        string _ConfigurationFilter;
        string _ConfigurationValue;
        string _PackagePath;
        string _ConfiguredValueType;
        bool _Active;

        public int ETLConfigurationId
        {
            get { return _ETLConfigurationId; }
            set
            {
                if (_ETLConfigurationId != value)
                {
                    _ETLConfigurationId = value;
                    OnPropertyChanged(() => ETLConfigurationId);
                }
            }
        }

        public string ConfigurationFilter
        {
            get { return _ConfigurationFilter; }
            set
            {
                if (_ConfigurationFilter != value)
                {
                    _ConfigurationFilter = value;
                    OnPropertyChanged(() => ConfigurationFilter);
                }
            }
        }

        public string ConfigurationValue
        {
            get { return _ConfigurationValue; }
            set
            {
                if (_ConfigurationValue != value)
                {
                    _ConfigurationValue = value;
                    OnPropertyChanged(() => ConfigurationValue);
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

        public string ConfiguredValueType
        {
            get { return _ConfiguredValueType; }
            set
            {
                if (_ConfiguredValueType != value)
                {
                    _ConfiguredValueType = value;
                    OnPropertyChanged(() => ConfiguredValueType);
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


        class CDQMETLConfigurationValidator : AbstractValidator<CDQMETLConfiguration>
        {
            public CDQMETLConfigurationValidator()
            {

            }
        }

        protected override IValidator GetValidator()
        {
            return new CDQMETLConfigurationValidator();
        }
    }
}
