namespace Computers.Tests
{
    using NUnit.Framework;

    public class ComputerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var computer = new Computer("Asus");

            var actual = computer.Name;
            var expected = "Asus";

            var part2 = new Part("cpu", 50);
            var part = new Part("cooller", 5.50m);
           
            computer.AddPart(part);

            var expectedCount = 1;

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedCount, computer.Parts.Count);

            var partFrom = computer.GetPart(part.Name);
            var expectedName = "cooller";

            Assert.AreEqual(expectedName, partFrom.Name);

            computer.AddPart(part2);
            var totalprice = computer.TotalPrice;
            var ex = 55.50;
            Assert.AreEqual(ex, totalprice);
          
            computer.RemovePart(part);

            var expectedRemoved = 1;
            Assert.AreEqual(expectedRemoved, computer.Parts.Count);

        }
    }
}