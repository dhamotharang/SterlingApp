using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class InternalRatingBased : ObjectBase
    {
        int _InternalRatingBasedId;
        string _Code;
        int _Rank;
        double _PD;
        string _Description;
        double _PD_LowerBoundary;
        double _PD_UpperBoundary;
        string _CompanyCode;
        string _SP_PD_Structure;
        bool _Low_Level_Credit;
        bool _Active;

        public int InternalRatingBasedId
        {
            get { return _InternalRatingBasedId; }
            set
            {
                if (_InternalRatingBasedId != value)
                {
                    _InternalRatingBasedId = value;
                    OnPropertyChanged(() => InternalRatingBasedId);
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

        public double PD
        {
            get { return _PD;}
            set
            {
                if (_PD != value)
                {
                    _PD = value;
                    OnPropertyChanged(() => PD);
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

        public double PD_LowerBoundary
        {
            get { return _PD_LowerBoundary; }
            set
            {
                if (_PD_LowerBoundary != value)
                {
                    _PD_LowerBoundary = value;
                    OnPropertyChanged(() => PD_LowerBoundary);
                }
            }
        }

        public double PD_UpperBoundary
        {
            get { return _PD_UpperBoundary; }
            set
            {
                if (_PD_UpperBoundary != value)
                {
                    _PD_UpperBoundary = value;
                    OnPropertyChanged(() => PD_UpperBoundary);
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
                    _CompanyCode = value;
                    OnPropertyChanged(() => CompanyCode);
                }
            }
        }

        public int Rank
        {
            get { return _Rank; }
            set
            {
                if (_Rank != value)
                {
                    _Rank = value;
                    OnPropertyChanged(() => Rank);
                }
            }
        }


        public string SP_PD_Structure
        {
            get { return _SP_PD_Structure; }
            set
            {
                if (_SP_PD_Structure != value)
                {
                    _SP_PD_Structure = value;
                    OnPropertyChanged(() => SP_PD_Structure);
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
        public bool Low_Level_Credit
        {
            get { return _Low_Level_Credit; }
            set
            {
                if (_Low_Level_Credit != value)
                {
                    _Low_Level_Credit = value;
                    OnPropertyChanged(() => Low_Level_Credit);
                }
            }
        }

        class InternalRatingBasedValidator : AbstractValidator<InternalRatingBased>
        {
            public InternalRatingBasedValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.PD).NotEmpty().WithMessage("PD is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new InternalRatingBasedValidator();
        }
    }
}
