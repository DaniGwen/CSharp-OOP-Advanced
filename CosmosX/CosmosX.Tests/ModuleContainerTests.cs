namespace CosmosX.Tests
{
    
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

        [Test]
        public void RemoveOldestModuleTest()
        {
            var moduleContainer = new ModuleContainer(10);

            var absorbingModule = new HeatProcessor(1, 100);
            var energyModule = new CryogenRod(2, 100);

            moduleContainer.AddAbsorbingModule(absorbingModule);
            moduleContainer.AddEnergyModule(energyModule);

            MethodInfo removeOldestModule = moduleContainer.GetType()
                 .GetMethod("RemoveOldestModule", BindingFlags.NonPublic | BindingFlags.Instance);

            FieldInfo fieldAbsorbingModules = moduleContainer.GetType()
               .GetField("absorbingModules", BindingFlags.NonPublic | BindingFlags.Instance);

            FieldInfo fieldEnergyModules = moduleContainer.GetType()
                .GetField("energyModules", BindingFlags.NonPublic | BindingFlags.Instance);

            var energyModuleField = (IDictionary)fieldEnergyModules.GetValue(moduleContainer);
            var expectedResultForEnergyModule = 1;

            var absorbingModules = (IDictionary)fieldAbsorbingModules.GetValue(moduleContainer);
            var expectedForAbsorbingModules = 0;

            removeOldestModule.Invoke(moduleContainer, null);

            FieldInfo fieldModulesByInput = moduleContainer.GetType()
               .GetField("modulesByInput", BindingFlags.NonPublic | BindingFlags.Instance);

            var actualValue = (IList)fieldModulesByInput.GetValue(moduleContainer);
            var expected = 1;

            Assert.AreEqual(expectedResultForEnergyModule, energyModuleField.Count);
            Assert.AreEqual(expectedForAbsorbingModules, absorbingModules.Count);
            Assert.AreEqual(expected, actualValue.Count);
        }

        [Test]
        public void TotalEnergyOutputProperty()
        {
            var moduleContainer = new ModuleContainer(10);

            var absorbingModule = new HeatProcessor(1, 100);
            var energyModule = new CryogenRod(2, 100);
            var energyModule2 = new CryogenRod(3, 100);

            moduleContainer.AddAbsorbingModule(absorbingModule);
            moduleContainer.AddEnergyModule(energyModule);
            moduleContainer.AddEnergyModule(energyModule2);

            var actualEnergyOutput = moduleContainer.GetType()
                .GetProperty("TotalEnergyOutput", BindingFlags.Public | BindingFlags.Instance)
                .GetValue(moduleContainer);

            var expectedOutput = 200;

            Assert.AreEqual(expectedOutput, actualEnergyOutput);
        }

        [Test]
        public void TotalHeatAbsorbingProperty()
        {
            var moduleContainer = new ModuleContainer(10);

            var absorbingModule = new HeatProcessor(1, 100);
            var absorbingModule2 = new HeatProcessor(2, 100);

            moduleContainer.AddAbsorbingModule(absorbingModule);
            moduleContainer.AddAbsorbingModule(absorbingModule2);

            var actualHeatAbsorbing = moduleContainer.GetType()
                .GetProperty("TotalHeatAbsorbing", BindingFlags.Public | BindingFlags.Instance)
                .GetValue(moduleContainer);

            var expectedOutput = 200;

            Assert.AreEqual(expectedOutput, actualHeatAbsorbing);
        }

        [Test]
        public void UniqueIdTest()
        {
            var moduleContainer = new ModuleContainer(10);

            var energyModule = new CryogenRod(1, 100);
            var energyModule2 = new CryogenRod(2, 100);

            moduleContainer.AddEnergyModule(energyModule);
            moduleContainer.AddEnergyModule(energyModule2);

            var energyModules = (IDictionary)moduleContainer.GetType()
                .GetField("energyModules", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(moduleContainer);

            var actualIds = energyModules.Keys;
            var expectedId = 1;

            foreach (var actualId in actualIds)
            {
                Assert.That(expectedId == (int)actualId);
                expectedId++;
            }
        }
    }
}