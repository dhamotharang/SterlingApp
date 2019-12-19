using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class ProductMIS : ObjectBase
    {
        int _ProductMISId;
        string _ProductCode;
        string _CaptionCode;
        string _TeamDefinitionCode;
        string _TeamCode;
        string _AccountOfficerDefinitionCode;
        string _AccountOfficerCode;
        string _Year;
        string _CompanyCode;
        bool _Active;

        public int ProductMISId
        {
            get { return _ProductMISId; }
            set
            {
                if (_ProductMISId != value)
                {
                    _ProductMISId = value;
                    OnPropertyChanged(() => ProductMISId);
                }
            }
        }


        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                if (_ProductCode != value)
                {
                    _ProductCode = value;
                    OnPropertyChanged(() => ProductCode);
                }
            }
        }

        public string CaptionCode
        {
            get { return _CaptionCode; }
            set
            {
                if (_CaptionCode != value)
                {
                    _CaptionCode = value;
                    OnPropertyChanged(() => CaptionCode);
                }
            }
        }


        public string TeamDefinitionCode
        {
            get { return _TeamDefinitionCode; }
            set
            {
                if (_TeamDefinitionCode != value)
                {
                    _TeamDefinitionCode = value;
                    OnPropertyChanged(() => TeamDefinitionCode);
                }
            }
        }

       
       

        public string TeamCode
        {
            get { return _TeamCode; }
            set
            {
                if (_TeamCode != value)
                {
                    _TeamCode = value;
                    OnPropertyChanged(() => TeamCode);
                }
            }
        }


        public string AccountOfficerCode
        {
            get { return _AccountOfficerCode; }
            set
            {
                if (_AccountOfficerCode != value)
                {
                    _AccountOfficerCode = value;
                    OnPropertyChanged(() => AccountOfficerCode);
                }
            }
        }


        public string AccountOfficerDefinitionCode
        {
            get { return _AccountOfficerDefinitionCode; }
            set
            {
                if (_AccountOfficerDefinitionCode != value)
                {
                    _AccountOfficerDefinitionCode = value;
                    OnPropertyChanged(() => AccountOfficerDefinitionCode);
                }
            }
        }


        public string Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    OnPropertyChanged(() => Year);
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

        
        class ProductMISValidator : AbstractValidator<ProductMIS>
        {
            public ProductMISValidator()
            {
                RuleFor(obj => obj.ProductCode).NotEmpty().WithMessage("Product Code is required.");
                RuleFor(obj => obj.TeamCode).NotEmpty().WithMessage("Team is required.");
                RuleFor(obj => obj.TeamDefinitionCode).NotEmpty().WithMessage("Team Definition is required.");
                //RuleFor(obj => obj.AccountOfficerCode).NotEmpty().WithMessage("Team is required.");
                //RuleFor(obj => obj.AccountOfficerDefinitionCode).NotEmpty().WithMessage("AccountOfficer Definition Code is required.");
                RuleFor(obj => obj.CaptionCode).NotEmpty().WithMessage("Caption is required.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProductMISValidator();
        }
    }
}
