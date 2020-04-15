using System;
using System.Globalization;
using System.Linq;
using System.Text;
using FestivalManager.Core.Controllers.Contracts;
using FestivalManager.Entities.Contracts;
using FestivalManager.Entities;
using System.Reflection;
using FestivalManager.Entities.Factories.Contracts;
using FestivalManager.Entities.Factories;

namespace FestivalManager.Core.Controllers
{
    public class FestivalController : IFestivalController
    {
        private const string TimeFormat = "mm\\:ss";

        private readonly IInstrumentFactory instrumentFactory;
        private readonly IPerformerFactory performerFactory;
        private readonly ISongFactory songFactory;

        private readonly IStage stage;

        public FestivalController(IStage stage)
        {
            this.stage = stage;
            this.songFactory = new SongFactory();
            this.performerFactory = new PerformerFactory();
            this.instrumentFactory = new InstrumentFactory();
        }

        public string ProduceReport()
        {
            var result = string.Empty;

            var totalTicks = this.stage.Sets.Sum(s => s.ActualDuration.Ticks);

            var ticksToTimespan = TimeSpan.FromTicks(totalTicks);

            result += "Results:\n";

            result += ($"Festival length: {ticksToTimespan.TotalMinutes:00}:{ticksToTimespan.Seconds:00}") + "\n";

            foreach (var set in this.stage.Sets)
            {
                result += ($"--{set.Name} ({set.ActualDuration.TotalMinutes:00}:{set.ActualDuration.Seconds:00}):") + "\n";

                var performersOrderedDescendingByAge = set.Performers.OrderByDescending(p => p.Age);

                foreach (var performer in performersOrderedDescendingByAge)
                {
                    var instruments = string.Join(", ", performer.Instruments
                        .OrderByDescending(i => i.Wear));

                    result += ($"---{performer.Name} ({instruments})") + "\n";
                }

                if (!set.Songs.Any())
                    result += ("--No songs played") + "\n";
                else
                {
                    result += ("--Songs played:") + "\n";
                    foreach (var song in set.Songs)
                    {
                        result += ($"----{song.Name} ({song.Duration.ToString(TimeFormat)})") + "\n";
                    }
                }
            }

            return result;
        }

        public string RegisterSet(string[] args)
        {
            var name = args[0];
            var type = args[1];

            //check????
            try
            {
                var typeofSet = Assembly.GetCallingAssembly()
                                .GetTypes()
                                .FirstOrDefault(t => t.Name == type);

                var instance = (ISet)Activator.CreateInstance(typeofSet, name);

                this.stage.AddSet(instance);

                return $"Registered {type} set";
            }
            catch (Exception e)
            {
                throw new Exception("Check RegisterSet method", e);
            }
        }

        public string SignUpPerformer(string[] args)
        {
            try
            {
                var name = args[0];
                var age = int.Parse(args[1]);

                var instrumentArgs = args.Skip(2).ToArray();

                var performer = this.performerFactory.CreatePerformer(name, age);

                if (instrumentArgs.Length != 0)
                {
                    var instruments = instrumentArgs
                    .Select(i => this.instrumentFactory.CreateInstrument(i))
                    .ToArray();

                    foreach (var instrument in instruments)
                    {
                        performer.AddInstrument(instrument);
                    }
                }

                this.stage.AddPerformer(performer);

                return $"Registered performer {performer.Name}";
            }
            catch (Exception e)
            {
                throw new Exception("Check SignUpPerformer", e);
            }
        }

        public string RegisterSong(string[] args)
        {
            try
            {
                var name = args[0];
                var duration = TimeSpan.Parse("00:" + args[1]);

                var song = this.songFactory.CreateSong(name, duration);

                this.stage.AddSong(song);

                return $"Registered song {song.Name} ({song.Duration.ToString(TimeFormat)})";
            }
            catch (Exception e)
            {
                throw new Exception("Check Registersong", e);
            }
        }

        public string AddSongToSet(string[] args)
        {
            try
            {
                var songName = args[0];
                var setName = args[1];

                if (!this.stage.HasSet(setName))
                {
                    throw new InvalidOperationException("Invalid set provided");
                }

                if (!this.stage.HasSong(songName))
                {
                    throw new InvalidOperationException("Invalid song provided");
                }

                var set = this.stage.GetSet(setName);
                var song = this.stage.GetSong(songName);

                set.AddSong(song);

                return $"Added {song.ToString()} to {set.Name}";
            }
            catch (Exception e)
            {
                return $"ERROR: {e.Message}";
            }
        }

        public string AddPerformerToSet(string[] args)
        {
            try
            {
                var name = args[0];
                var setName = args[1];

                if (!this.stage.HasPerformer(name))
                {
                    throw new InvalidOperationException("Invalid performer provided");
                }
                if (!this.stage.HasSet(setName))
                {
                    throw new InvalidOperationException("Invalid set provided");
                }

                var set = this.stage.GetSet(setName);
                var performer = this.stage.GetPerformer(name);

                set.AddPerformer(performer);

                return $"Added {performer.Name} to {set.Name}";
            }
            catch (Exception e)
            {
                return $"ERROR: {e.Message}";
            }
           
        }

        public string RepairInstruments(string[] args)
        {
            var instrumentsToRepair = this.stage.Performers
                .SelectMany(p => p.Instruments)
                .Where(i => i.Wear < 100)
                .ToArray();

            foreach (var instrument in instrumentsToRepair)
            {
                instrument.Repair();
            }

            return $"Repaired {instrumentsToRepair.Length} instruments";
        }
    }
}