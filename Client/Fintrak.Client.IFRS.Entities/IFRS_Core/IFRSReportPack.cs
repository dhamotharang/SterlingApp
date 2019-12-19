using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IFRSReportPack : ObjectBase
    {
        int _ReportPackId;
        string _ReportName;
        string _ReportDescription;
        int _SolutionId;
        string _CompanyCode;
        bool _Active;

        public int ReportPackId
        {
            get { return _ReportPackId; }
            set
            {
                if (_ReportPackId != value)
                {
                    _ReportPackId = value;
                    OnPropertyChanged(() => ReportPackId);
                }
            }
        }

        public string ReportName
        {
            get { return _ReportName; }
            set
            {
                if (_ReportName != value)
                {
                    _ReportName = value;
                    OnPropertyChanged(() => ReportName);
                }
            }
        }

        public string ReportDescription
        {
            get { return _ReportDescription; }
            set
            {
                if (_ReportDescription != value)
                {
                    _ReportDescription = value;
                    OnPropertyChanged(() => ReportDescription);
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
                    _CompanyCode = value;
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


        class IFRSReportPackValidator : AbstractValidator<IFRSReportPack>
        {
            public IFRSReportPackValidator()
            {
                RuleFor(obj => obj._ReportName).NotEmpty().WithMessage("Report Name is required.");
                RuleFor(obj => obj._ReportDescription).NotEmpty().WithMessage("Report Description is required.");
                
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new IFRSReportPackValidator();
        }
    }
}
