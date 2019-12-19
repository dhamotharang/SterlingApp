using Fintrak.Client.MPR.Proxies;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;


namespace Fintrak.Client.MPR.Bootstrapper
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            return Init(null);
        }

        public static CompositionContainer Init(ICollection<ComposablePartCatalog> catalogParts)
        {
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MPRBSClient).Assembly));

            if (catalogParts != null)
                foreach (var part in catalogParts)
                    catalog.Catalogs.Add(part);
            
            CompositionContainer container = new CompositionContainer(catalog);
            
            return container;
        }

    }
}
