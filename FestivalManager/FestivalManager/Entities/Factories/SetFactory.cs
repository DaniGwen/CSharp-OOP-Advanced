using System.Linq;
using System.Reflection;
using FestivalManager.Entities.Factories.Contracts;
using FestivalManager.Entities.Contracts;
using System;

namespace FestivalManager.Entities.Factories
{
    public class SetFactory : ISetFactory
    {
        public ISet CreateSet(string name, string type)
        {
            var typeofSet = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == type);

            var instance = (ISet)Activator.CreateInstance(typeofSet, name);

            return instance;
        }
    }
}
