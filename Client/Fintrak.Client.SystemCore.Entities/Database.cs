using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class Database : ObjectBase
    {
        int _DatabaseId;
        string _Title;
        string _DatabaseName;
        string _ServerName;
        string _ServiceServerName;
        string _UserName;
        string _Password;
        string _IntegratedSecurity;
        int _SolutionId;
        string _CompanyCode;
        bool _Active;

        public int DatabaseId
        {
            get { return _DatabaseId; }
            set
            {
                if (_DatabaseId != value)
                {
                    _DatabaseId = value;
                    OnPropertyChanged(() => DatabaseId);
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
                    _Title = value.Trim();
                    OnPropertyChanged(() => Title);
                }
            }
        }

        public string DatabaseName
        {
            get { return _DatabaseName; }
            set
            {
                if (_DatabaseName != value)
                {
                    _DatabaseName = value.Trim();
                    OnPropertyChanged(() => DatabaseName);
                }
            }
        }

        public string ServerName
        {
            get { return _ServerName; }
            set
            {
                if (_ServerName != value)
                {
                    _ServerName = value.Trim();
                    OnPropertyChanged(() => ServerName);
                }
            }
        }
        public string ServiceServerName
        {
            get { return _ServiceServerName; }
            set
            {
                if (_ServiceServerName != value)
                {
                    _ServiceServerName = value.Trim();
                    OnPropertyChanged(() => ServiceServerName);
                }
            }
        }

        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value.Trim();
                    OnPropertyChanged(() => UserName);
                }
            }
        }

        public string Password
        {
            get { return _Password; }
            set
            {
                if (_Password != value)
                {
                    _Password = value.Trim();
                    OnPropertyChanged(() => Password);
                }
            }
        }

        public string IntegratedSecurity
        {
            get { return _IntegratedSecurity; }
            set
            {
                if (_IntegratedSecurity != value)
                {
                    _IntegratedSecurity = value.Trim();
                    OnPropertyChanged(() => IntegratedSecurity);
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

        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value.Trim();
                    OnPropertyChanged(() => CompanyCode);
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
                return string.Format("{0} - {1}", _DatabaseName, _Title );
            }
        }

        class DatabaseValidator : AbstractValidator<Database>
        {
            public DatabaseValidator()
            {
                RuleFor(obj => obj.DatabaseName).NotEmpty().WithMessage("DatabaseName must not be empty.");
                RuleFor(obj => obj.Title).NotEmpty().WithMessage("Title must not be empty.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new DatabaseValidator();
        }
    }
}
