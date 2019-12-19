using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class OperationReview : ObjectBase
    {
        int _OperationReviewId;
        string _Code;
        string _Name;
        string _Description;
        string _OperationCode;
        OperationStatusEnum _Status;
        bool _Active;

        public int OperationReviewId
        {
            get { return _OperationReviewId; }
            set
            {
                if (_OperationReviewId != value)
                {
                    _OperationReviewId = value;
                    OnPropertyChanged(() => OperationReviewId);
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

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }


        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }


        public string OperationCode
        {
            get { return _OperationCode; }
            set
            {
                if (_OperationCode != value)
                {
                    _OperationCode = value;
                    OnPropertyChanged(() => OperationCode);
                }
            }
        }

        public OperationStatusEnum Status
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
        public string LongDescription
        {
            get
            {
                return string.Format("{0} - {1}", _Code, _Name);
            }
        }

        
        class OperationReviewValidator : AbstractValidator<OperationReview>
        {
            public OperationReviewValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new OperationReviewValidator();
        }
    }
}
