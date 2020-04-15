// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to your project (entities/controllers/etc)
namespace FestivalManager.Tests
{
    using FestivalManager.Core.Controllers;
    using FestivalManager.Entities;
    using FestivalManager.Entities.Instruments;
    using FestivalManager.Entities.Sets;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class SetControllerTests
    {
        [Test]
        public void Test()
        {
            var stage = new Stage();

            var setController = new SetController(stage);

            var performer = new Performer("Jikata", 49);

            var song = new Song("Generation", new TimeSpan(0, 5, 0));

            var guitar = new Guitar();
            performer.AddInstrument(guitar);

            var set = new Medium("MediumSet");

            set.AddPerformer(performer);
            set.AddSong(song);

            stage.AddSet(set);

            var actualResult = setController.PerformSets();
            var expected = "1. MediumSet:" + Environment.NewLine +
                "-- 1. Generation (05:00)" + Environment.NewLine +
                "-- Set Successful";

            var actualResultWear = guitar.Wear;
            var expectedWear = 40;

            Assert.AreEqual(expectedWear, actualResultWear);
            Assert.AreEqual(expected, actualResult);
            

            var nullController = new SetController(null);

            Assert.That(() => nullController.PerformSets(), Throws.Exception);

            setController.PerformSets();
            var resultFromlast = setController.PerformSets();
            var wearAfterSecondPerform = guitar.Wear;

            var expectedWeardown = 0;
            var expectedResultFromLast = "1. MediumSet:\r\n-- Did not perform";

            Assert.AreEqual(expectedResultFromLast, resultFromlast);
            Assert.AreEqual(expectedWeardown, wearAfterSecondPerform);
        }
    }
}