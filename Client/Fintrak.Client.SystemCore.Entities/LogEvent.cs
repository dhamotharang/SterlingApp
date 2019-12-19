using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class LogEvent : ObjectBase
    {
        string _Id;
        Guid _IdAsGuid;
        int _IdAsInteger;
        string _IdType;
        DateTime? _LogDate;
        string _LoggerProviderName;
        string _Source;
        string _MachineName;
        string _Type;
        string _Level;
        string _Message;
        string _StackTrace;
        string _AllXml;

        public string Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(() => Id);
                }
            }
        }

        public Guid IdAsGuid
        {
            get { return _IdAsGuid; }
            set
            {
                if (_IdAsGuid != value)
                {
                    _IdAsGuid = value;
                    OnPropertyChanged(() => IdAsGuid);
                }
            }
        }

        public int IdAsInteger
        {
            get { return _IdAsInteger; }
            set
            {
                if (_IdAsInteger != value)
                {
                    _IdAsInteger = value;
                    OnPropertyChanged(() => IdAsInteger);
                }
            }
        }

        public string IdType
        {
            get { return _IdType; }
            set
            {
                if (_IdType != value)
                {
                    _IdType = value;
                    OnPropertyChanged(() => IdType);
                }
            }
        }

        public DateTime? LogDate
        {
            get { return _LogDate; }
            set
            {
                if (_LogDate != value)
                {
                    _LogDate = value;
                    OnPropertyChanged(() => LogDate);
                }
            }
        }

        public string LoggerProviderName
        {
            get { return _LoggerProviderName; }
            set
            {
                if (_LoggerProviderName != value)
                {
                    _LoggerProviderName = value;
                    OnPropertyChanged(() => LoggerProviderName);
                }
            }
        }

        public string Source
        {
            get { return _Source; }
            set
            {
                if (_Source != value)
                {
                    _Source = value;
                    OnPropertyChanged(() => Source);
                }
            }
        }

        public string MachineName
        {
            get { return _MachineName; }
            set
            {
                if (_MachineName != value)
                {
                    _MachineName = value;
                    OnPropertyChanged(() => MachineName);
                }
            }
        }

        public string Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    OnPropertyChanged(() => Type);
                }
            }
        }

        public string Level
        {
            get { return _Level; }
            set
            {
                if (_Level != value)
                {
                    _Level = value;
                    OnPropertyChanged(() => Level);
                }
            }
        }

        public string Message
        {
            get { return _Message; }
            set
            {
                if (_Message != value)
                {
                    _Message = value;
                    OnPropertyChanged(() => Message);
                }
            }
        }

        public string StackTrace
        {
            get { return _StackTrace; }
            set
            {
                if (_StackTrace != value)
                {
                    _StackTrace = value;
                    OnPropertyChanged(() => StackTrace);
                }
            }
        }

        public string AllXml
        {
            get { return _AllXml; }
            set
            {
                if (_AllXml != value)
                {
                    _AllXml = value;
                    OnPropertyChanged(() => AllXml);
                }
            }
        }

        class LogEventValidator : AbstractValidator<LogEvent>
        {
            public LogEventValidator()
            {
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new LogEventValidator();
        }
    }
}
