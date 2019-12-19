using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Core.Entities
{
    public class Upload : ObjectBase
    {
        int _UploadId;
        string _Title;
        string _Code;
        string _Template;
        string _Action;
        string _TruncateAction;
        string _PostUploadAction;
        string _Verification;
        int _SolutionId;
        int _Position;
        bool _BulkUpload;
        bool _Active;

        public int UploadId
        {
            get { return _UploadId; }
            set
            {
                if (_UploadId != value)
                {
                    _UploadId = value;
                    OnPropertyChanged(() => UploadId);
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

        public string PostUploadAction
        {
            get { return _PostUploadAction; }
            set
            {
                if (_PostUploadAction != value)
                {
                    _PostUploadAction = value;
                    OnPropertyChanged(() => PostUploadAction);
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

        public string Template
        {
            get { return _Template; }
            set
            {
                if (_Template != value)
                {
                    _Template = value;
                    OnPropertyChanged(() => Template);
                }
            }
        }

        public string Action
        {
            get { return _Action; }
            set
            {
                if (_Action != value)
                {
                    _Action = value;
                    OnPropertyChanged(() => Action);
                }
            }
        }

        public string TruncateAction
        {
            get { return _TruncateAction; }
            set
            {
                if (_TruncateAction != value)
                {
                    _TruncateAction = value;
                    OnPropertyChanged(() => TruncateAction);
                }
            }
        }
        public string Verification
        {
            get { return _Verification; }
            set
            {
                if (_Verification != value)
                {
                    _Verification = value;
                    OnPropertyChanged(() => Verification);
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

        public bool BulkUpload
        {
            get { return _BulkUpload; }
            set
            {
                if (_BulkUpload != value)
                {
                    _BulkUpload = value;
                    OnPropertyChanged(() => BulkUpload);
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

        class UploadValidator : AbstractValidator<Upload>
        {
            public UploadValidator()
            {
                RuleFor(obj => obj.Title).NotEmpty().WithMessage("Title must not be empty.");
                RuleFor(obj => obj.Template).NotEmpty().WithMessage("Template must not be empty.");
                RuleFor(obj => obj.Action).NotEmpty().WithMessage("Action must not be empty.");
                RuleFor(obj => obj.SolutionId).GreaterThan(0).WithMessage("Solution is require.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new UploadValidator();
        }
    }
}
