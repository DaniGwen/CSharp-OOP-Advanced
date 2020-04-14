namespace FestivalManager.Entities
{
	using System.Collections.Generic;
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
			throw new System.NotImplementedException();
		}

		public void AddSet(ISet performer)
		{
			throw new System.NotImplementedException();
		}

		public void AddSong(ISong song)
		{
			throw new System.NotImplementedException();
		}

		public IPerformer GetPerformer(string name)
		{
			throw new System.NotImplementedException();
		}

		public ISet GetSet(string name)
		{
			throw new System.NotImplementedException();
		}

		public ISong GetSong(string name)
		{
			throw new System.NotImplementedException();
		}

		public bool HasPerformer(string name)
		{
			throw new System.NotImplementedException();
		}

		public bool HasSet(string name)
		{
			throw new System.NotImplementedException();
		}

		public bool HasSong(string name)
		{
			throw new System.NotImplementedException();
		}
	}
}
