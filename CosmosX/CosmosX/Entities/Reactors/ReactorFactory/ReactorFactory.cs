using CosmosX.Entities.Containers;
using CosmosX.Entities.Containers.Contracts;
using CosmosX.Entities.Reactors.Contracts;
using CosmosX.Entities.Reactors.ReactorFactory.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CosmosX.Entities.Reactors.ReactorFactory
{
    public class ReactorFactory : IReactorFactory
    {
        private readonly string reactorSuffix = "Reactor";

        public IReactor CreateReactor(string reactorTypeName, int id, IContainer container, int additionalParameter)
        {
            var type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == reactorTypeName + reactorSuffix);

            var reactor = (IReactor)Activator.CreateInstance(type, id, container, additionalParameter);

            return reactor;
        }
    }
}
