using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Proxies
{
    [Export(typeof(IServiceFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ServiceFactory : IServiceFactory
    {
        T IServiceFactory.CreateClient<T>()
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}
