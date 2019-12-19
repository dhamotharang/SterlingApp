using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Core;
using System.Collections.Generic;

namespace Fintrak.Shared.Common.ServiceModel
{
    public class ManagerBase
    {
        public ManagerBase()
        {
            OperationContext context = OperationContext.Current;
            if (context != null)
            {
                try
                {
                    _LoginName = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("String", "System");
                    _CompanyCode = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("Company", "System");
                    if (_LoginName.IndexOf(@"\") > -1) _LoginName = string.Empty;
                }
                catch (Exception ex)
                {
                    _LoginName = string.Empty;
                    _CompanyCode = string.Empty;
                }
            }

            if (ObjectBase.Container != null)
                ObjectBase.Container.SatisfyImportsOnce(this);

            //RegisterModule();

        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public virtual void RegisterModule()
        {

        }

        protected string _LoginName = string.Empty;
        protected string _CompanyCode = string.Empty;

        protected virtual bool AllowAccessToOperation(string moduleName, List<string> groupNames)
        {
            return false;
        }

        protected virtual string GetContextConnection()
        {
            return string.Empty;
        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codetoExecute)
        {
            try
            {
                return codetoExecute.Invoke();
            }
            catch (AuthorizationValidationException ex)
            {
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch (ModuleException ex)
            {
                throw new FaultException<ModuleException>(ex, ex.Message);
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        protected void ExecuteFaultHandledOperation(Action codetoExecute)
        {
            try
            {
                codetoExecute.Invoke();
            }
            catch (AuthorizationValidationException ex)
            {
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch (ModuleException ex)
            {
                throw new FaultException<ModuleException>(ex, ex.Message);
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

    }
}
