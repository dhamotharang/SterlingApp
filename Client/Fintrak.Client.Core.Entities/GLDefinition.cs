using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Core.Entities
{
    public class GLDefinition : ObjectBase
    {
        int _GLDefinitionId;
        string _GL_Code;
        string _Description;       
        bool _Active;

        public int GLDefinitionId
        {
            get { return _GLDefinitionId; }
            set
            {
                if (_GLDefinitionId != value)
                {
                    _GLDefinitionId = value;
                    OnPropertyChanged(() => GLDefinitionId);
                }
            }
        }

        public string GL_Code
        {
            get { return _GL_Code; }
            set
            {
                if (_GL_Code != value)
                {
                    _GL_Code = value;
                    OnPropertyChanged(() => GL_Code);
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

        
        class GLDefinitionValidator : AbstractValidator<GLDefinition>
        {
            public GLDefinitionValidator()
            {
                RuleFor(obj => obj.GL_Code).NotEmpty().WithMessage("GLCode is required."); 
            }
        }

        protected override IValidator GetValidator()
        {
            return new GLDefinitionValidator();
        }
    }
}
