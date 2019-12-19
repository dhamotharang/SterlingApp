using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.Core.Entities
{
    public class ExtractionTrigger : ObjectBase
    {
        int _ExtractionTriggerId;
        int _ExtractionId;
        string _Code;
        PackageStatus _Status;
        string _Remark;
        string _UserName;
        DateTime _StartDate;
        DateTime _EndDate;
        DateTime? _RunTime;
        bool _NeedToArchive;
        bool _Active;


        public int ExtractionTriggerId
        {
            get { return _ExtractionTriggerId; }
            set
            {
                if (_ExtractionTriggerId != value)
                {
                    _ExtractionTriggerId = value;
                    OnPropertyChanged(() => ExtractionTriggerId);
                }
            }
        }

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

        public string Code
        {
            get { return _Code; }
            set
            {
                if (_Code != value)
                {
                    _Code = value;
                    OnPropertyChanged(() => Code);
                }
            }
        }

        public PackageStatus Status
        {
            get { return _Status; }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    OnPropertyChanged(() => Status);
                }
            }
        }

        public string Remark
        {
            get { return _Remark; }
            set
            {
                if (_Remark != value)
                {
                    _Remark = value;
                    OnPropertyChanged(() => Remark);
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
                    _UserName = value;
                    OnPropertyChanged(() => UserName);
                }
            }
        }

        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                if (_StartDate != value)
                {
                    _StartDate = value;
                    OnPropertyChanged(() => StartDate);
                }
            }
        }

        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                if (_EndDate != value)
                {
                    _EndDate = value;
                    OnPropertyChanged(() => EndDate);
                }
            }
        }

        public DateTime? RunTime
        {
            get { return _RunTime; }
            set
            {
                if (_RunTime != value)
                {
                    _RunTime = value;
                    OnPropertyChanged(() => RunTime);
                }
            }
        }

        public bool NeedToArchive
        {
            get { return _NeedToArchive; }
            set
            {
                if (_NeedToArchive != value)
                {
                    _NeedToArchive = value;
                    OnPropertyChanged(() => NeedToArchive);
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

        public string StatusName
        {
            get
            {
                return string.Format("{0}", _Status.ToString());
            }
        }

        class ExtractionTriggerValidator : AbstractValidator<ExtractionTrigger>
        {
            public ExtractionTriggerValidator()
            {
               
             
            }
        }

        protected override IValidator GetValidator()
        {
            return new ExtractionTriggerValidator();
        }
    }
}
