using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class OpexGLBasis : ObjectBase
    {
        int _OpexGLBasisId;
        string _BranchCode;
        string _MISCode;
        string _Caption;
        double _Basis;
        string _Narration;      
        bool _Active;

        public int OpexGLBasisId
        {
            get { return _OpexGLBasisId; }
            set
            {
                if (_OpexGLBasisId != value)
                {
                    _OpexGLBasisId = value;
                    OnPropertyChanged(() => OpexGLBasisId);
                }
            }
        }


        public string BranchCode
        {
            get { return _BranchCode; }
            set
            {
                if (_BranchCode != value)
                {
                    _BranchCode = value;
                    OnPropertyChanged(() => BranchCode);
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

        public double Basis
        {
            get { return _Basis; }
            set
            {
                if (_Basis != value)
                {
                    _Basis = value;
                    OnPropertyChanged(() => Basis);
                }
            }
        }

        public string Narration
        {
            get { return _Narration; }
            set
            {
                if (_Narration != value)
                {
                    _Narration = value;
                    OnPropertyChanged(() => Narration);
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
        //        return string.Format("{0} - {1}", _BranchCode, _MISCode);
        //    }
        //}

        
        class OpexGLBasisValidator : AbstractValidator<OpexGLBasis>
        {
            public OpexGLBasisValidator()
            {
                RuleFor(obj => obj.BranchCode).NotEmpty().WithMessage("BranchCode is required.");
                RuleFor(obj => obj.MISCode).NotEmpty().WithMessage("MISCode is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new OpexGLBasisValidator();
        }
    }
}
