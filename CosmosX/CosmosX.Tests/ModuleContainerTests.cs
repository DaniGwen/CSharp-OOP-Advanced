namespace CosmosX.Tests
{
    using CosmosX.Entities.Containers;
    using CosmosX.Entities.Modules.Absorbing;
    using CosmosX.Entities.Modules.Energy;
    using NUnit.Framework;
    using System;
    using System.Collections;
    using System.Reflection;

    [TestFixture]
    public class ModuleContainerTests
    {
        [Test]
        public void AddEnergyModuleTest()
        {
            int expectedCount = 0;

            var moduleContainer = new ModuleContainer(10);

            var energyModule = new CryogenRod(1, 100);
            moduleContainer.AddEnergyModule(energyModule);

            FieldInfo fieldEnergyModules = moduleContainer.GetType()
                .GetField("energyModules", BindingFlags.NonPublic | BindingFlags.Instance);

            expectedCount = 1;
            var actualCount = (IDictionary)fieldEnergyModules.GetValue(moduleContainer);

            Assert.AreEqual(expectedCount, actualCount.Count);

            // Test modulesByInput
            FieldInfo fieldModulesByInput = moduleContainer.GetType()
                .GetField("modulesByInput", BindingFlags.NonPublic | BindingFlags.Instance);

            var actualCountOfList = (IList)fieldModulesByInput.GetValue(moduleContainer);
            expectedCount = 1;

            Assert.AreEqual(expectedCount, actualCountOfList.Count);


            // Test for null
            Assert.That(() => moduleContainer.AddEnergyModule(null), Throws.ArgumentException);
        }

        [Test]
        public void AddAbsorbingModuleTest()
        {
            var moduleContainer = new ModuleContainer(10);

            var absorbingModule = new HeatProcessor(1, 100);
            moduleContainer.AddAbsorbingModule(absorbingModule);

            FieldInfo fieldAbsorbingModules = moduleContainer.GetType()
                .GetField("absorbingModules", BindingFlags.NonPublic | BindingFlags.Instance);

            var actualCount = (IDictionary)fieldAbsorbingModules.GetValue(moduleContainer);
            var expectedCount = 1;

            Assert.AreEqual(expectedCount, actualCount.Count);

            Assert.That(() => moduleContainer.AddAbsorbingModule(null), Throws.ArgumentException);

        }
    }
}