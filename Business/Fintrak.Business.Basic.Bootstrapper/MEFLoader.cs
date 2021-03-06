using System;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Fintrak.Data.Basic;
using Fintrak.Shared.Common.Data;

namespace Fintrak.Business.Basic.Bootstrapper
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(GLTypeRepository).Assembly));
            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(DataRepositoryFactory).Assembly));
           
            CompositionContainer container = new CompositionContainer(catalog);

            return container;
        }

    }
}
