using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class MessagingSubscription : ObjectBase
    {
        int _MessagingSubscriptionId;
        string _Recipents;
        string _Subjects;
        string _eMessage;
        string _Report;
        DateTime _Rundate;
        string _FileType;
        string _ReportID;
        bool _TriggeredBy;
        bool _Active;

        public int MessagingSubscriptionId
        {
            get { return _MessagingSubscriptionId; }
            set
            {
                if (_MessagingSubscriptionId != value)
                {
                    _MessagingSubscriptionId = value;
                    OnPropertyChanged(() => MessagingSubscriptionId);
                }
            }
        }

        public string Recipents
        {
            get { return _Recipents; }
            set
            {
                if (_Recipents != value)
                {
                    _Recipents = value;
                    OnPropertyChanged(() => Recipents);
                }
            }
        }

        public string Subjects
        {
            get { return _Subjects; }
            set
            {
                if (_Subjects != value)
                {
                    _Subjects = value;
                    OnPropertyChanged(() => Subjects);
                }
            }
        }

       

        public string eMessage
        {
            get { return _eMessage; }
            set
            {
                if (_eMessage != value)
                {
                    _eMessage = value;
                    OnPropertyChanged(() => eMessage);
                }
            }
        }

        public string Report
        {
            get { return _Report; }
            set
            {
                if (_Report != value)
                {
                    _Report = value;
                    OnPropertyChanged(() => Report);
                }
            }
        }

        public DateTime Rundate
        {
            get { return _Rundate; }
            set
            {
                if (_Rundate != value)
                {
                    _Rundate = value;
                    OnPropertyChanged(() => Rundate);
                }
            }
        }

        public string FileType
        {
            get { return _FileType; }
            set
            {
                if (_FileType != value)
                {
                    _FileType = value;
                    OnPropertyChanged(() => FileType);
                }
            }
        }



        public string ReportID
        {
            get { return _ReportID; }
            set
            {
                if (_ReportID != value)
                {
                    _ReportID = value;
                    OnPropertyChanged(() => ReportID);
                }
            }
        }
        public bool TriggeredBy
        {
            get { return _TriggeredBy; }
            set
            {
                if (_TriggeredBy != value)
                {
                    _TriggeredBy = value;
                    OnPropertyChanged(() => TriggeredBy);
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

        
        class MessagingSubscriptionValidator : AbstractValidator<MessagingSubscription>
        {
            public MessagingSubscriptionValidator()
            {

                RuleFor(obj => obj.Recipents).NotEmpty().WithMessage("Recipents is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MessagingSubscriptionValidator();
        }
    }
}
