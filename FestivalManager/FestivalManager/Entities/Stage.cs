﻿namespace FestivalManager.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    public class Stage : IStage
    {
        private readonly List<ISet> sets;
        private readonly List<ISong> songs;
        private readonly List<IPerformer> performers;

        protected Stage()
        {
            this.sets = new List<ISet>();
            this.songs = new List<ISong>();
            this.performers = new List<IPerformer>();
        }

        public IReadOnlyCollection<ISet> Sets => this.sets.AsReadOnly();
        public IReadOnlyCollection<ISong> Songs => this.songs.AsReadOnly();
        public IReadOnlyCollection<IPerformer> Performers => this.performers.AsReadOnly();

        public void AddPerformer(IPerformer performer)
        {
            if (performer != null)
            {
                this.performers.Add(performer);
            }
            else
            {
                throw new ArgumentNullException("Performer can not be null!");
            }
        }

        public void AddSet(ISet set)
        {
            if (set == null)
            {
                throw new ArgumentNullException("Set can not be null!");
            }

            this.sets.Add(set);
        }

        public void AddSong(ISong song)
        {
            if (song == null)
            {
                throw new ArgumentNullException("Song can not be null!");
            }

            this.songs.Add(song);
        }

        public IPerformer GetPerformer(string name)
                     => this.performers.FirstOrDefault(p => p.Name == name);

        public ISet GetSet(string name)
                     => this.sets.FirstOrDefault(s => s.Name == name);

        public ISong GetSong(string name)
                     => this.songs.FirstOrDefault(s => s.Name == name);

        public bool HasPerformer(string name)
                     => this.performers.Any(p => p.Name == name);

        public bool HasSet(string name)
                     => this.sets.Any(s => s.Name == name);

        public bool HasSong(string name)
                     => this.songs.Any(s => s.Name == name);
    }
}
