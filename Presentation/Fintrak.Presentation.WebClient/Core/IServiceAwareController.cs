using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.Core
{
    public interface IServiceAwareController
    {
        void RegisterDisposableServices(List<IServiceContract> disposableServices);

        List<IServiceContract> DisposableServices { get; }
    }
}
