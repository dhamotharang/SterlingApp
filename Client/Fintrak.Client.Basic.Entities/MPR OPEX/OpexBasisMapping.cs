using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class OpexBasisMapping : ObjectBase
    {
        int _OpexBasisMappingId;
        string _GLCode;
        string _Description;
        string _Caption;
        string _LineCaption;
        string _MISCode;      
        bool _Active;

        public int OpexBasisMappingId
        {
            get { return _OpexBasisMappingId; }
            set
            {
                if (_OpexBasisMappingId != value)
                {
                    _OpexBasisMappingId = value;
                    OnPropertyChanged(() => OpexBasisMappingId);
                }
            }
        }


        public string GLCode
        {
            get { return _GLCode; }
            set
            {
                if (_GLCode != value)
                {
                    _GLCode = value;
                    OnPropertyChanged(() => GLCode);
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

        public string Caption
        {
            get { return _Caption; }
            set
            {
                if (_Caption != value)
                {
                    _Caption = value;
                    OnPropertyChanged(() => Caption);
                }
            }
        }
        
        public string LineCaption
        {
            get { return _LineCaption; }
            set
            {
                if (_LineCaption != value)
                {
                    _LineCaption = value;
                    OnPropertyChanged(() => LineCaption);
                }
            }
        }

        public string MISCode
        {
            get { return _MISCode; }
            set
            {
                if (_MISCode != value)
                {
                    _MISCode = value;
                    OnPropertyChanged(() => MISCode);
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

        //public string LongDescription
        //{
        //    get
        //    {
        //        return string.Format("{0} - {1}", _GLCode, _Description);
        //    }
        //}

        
        class OpexBasisMappingValidator : AbstractValidator<OpexBasisMapping>
        {
            public OpexBasisMappingValidator()
            {
                RuleFor(obj => obj.GLCode).NotEmpty().WithMessage("GLCode is required.");
                RuleFor(obj => obj.Description).NotEmpty().WithMessage("Description is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new OpexBasisMappingValidator();
        }
    }
}
