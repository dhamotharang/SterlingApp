using System;
using System.Linq;
using Fintrak.Shared.SystemCore.Framework;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class AuditTrail : ObjectBase
    {
        int _AuditTrailId;
        DateTime _RevisionStamp;
        string _TableName;
        string  _UserName;
        AuditAction _Actions;
        string _ActionDescription;
        string _OldData;
        string _NewData;
        string _ChangedColumns;
        bool _Active;

        public int AuditTrailId
        {
            get { return _AuditTrailId; }
            set
            {
                if (_AuditTrailId != value)
                {
                    _AuditTrailId = value;
                    OnPropertyChanged(() => AuditTrailId);
                }
            }
        }

        public DateTime RevisionStamp
        {
            get { return _RevisionStamp; }
            set
            {
                if (_RevisionStamp != value)
                {
                    _RevisionStamp = value;
                    OnPropertyChanged(() => RevisionStamp);
                }
            }
        }

        public string TableName
        {
            get { return _TableName; }
            set
            {
                if (_TableName != value)
                {
                    _TableName = value;
                    OnPropertyChanged(() => TableName);
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

        public AuditAction Actions
        {
            get { return _Actions; }
            set
            {
                if (_Actions != value)
                {
                    _Actions = value;
                    OnPropertyChanged(() => Actions);
                }
            }
        }


        public string ActionDescription
        {
            get { return _ActionDescription; }
            set
            {
                if (_ActionDescription != value)
                {
                    _ActionDescription = value;
                    OnPropertyChanged(() => ActionDescription);
                }
            }
        }

        public string OldData
        {
            get { return _OldData; }
            set
            {
                if (_OldData != value)
                {
                    _OldData = value;
                    OnPropertyChanged(() => OldData);
                }
            }
        }

        public string NewData
        {
            get { return _NewData; }
            set
            {
                if (_NewData != value)
                {
                    _NewData = value;
                    OnPropertyChanged(() => NewData);
                }
            }
        }

        public string ChangedColumns
        {
            get { return _ChangedColumns; }
            set
            {
                if (_ChangedColumns != value)
                {
                    _ChangedColumns = value;
                    OnPropertyChanged(() => ChangedColumns);
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

        public string ActionName
        {
            get 
            {
                switch (_Actions)
                {
                    case AuditAction.C:
                        return "Create";
                    case AuditAction.U:
                        return "Update";
                    case AuditAction.D:
                        return "Delete";
                    case AuditAction.E:
                        return "Upload";
                    default:
                        return "Upload";
                }
            }
        }

        class AuditTrailValidator : AbstractValidator<AuditTrail>
        {
            public AuditTrailValidator()
            {
                RuleFor(obj => obj.TableName).NotEmpty().WithMessage("TableName must not be empty.");
                RuleFor(obj => obj.UserName).NotEmpty().WithMessage("UserName must not be empty.");
     
            }
        }

        protected override IValidator GetValidator()
        {
            return new AuditTrailValidator();
        }
    }
}
