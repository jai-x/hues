using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;

namespace hues.Game.Stores
{
    public class RespackTrackStore : ITrackStore
    {
        private readonly ITrackStore backing;

        public RespackTrackStore(ITrackStore backing)
        {
            this.backing = backing;
            // Set Volume to half by default
            this.backing.Volume.Value = 0.5;
        }

        #region ITrackStore
        public Track GetVirtual(double length) => backing.GetVirtual(length);
        #endregion

        #region IResourceStore<Track>
        public Track Get(string name) => backing.Get(name);
        public Task<Track> GetAsync(string name, CancellationToken cancellationToken = default)
            => backing.GetAsync(name, cancellationToken);
        public Stream GetStream(string name) => backing.GetStream(name);
        public IEnumerable<string> GetAvailableResources() => backing.GetAvailableResources();
        #endregion

        #region IDisposable
        public void Dispose() => backing.Dispose();
        #endregion

        #region IAdjustableAudioComponent
        public void BindAdjustments(IAggregateAudioAdjustment component) => backing.BindAdjustments(component);
        public void UnbindAdjustments(IAggregateAudioAdjustment component) => backing.UnbindAdjustments(component);
        public void AddAdjustment(AdjustableProperty type, IBindable<double> adjustBindable) => backing.AddAdjustment(type, adjustBindable);
        public void RemoveAdjustment(AdjustableProperty type, IBindable<double> adjustBindable) => backing.RemoveAdjustment(type, adjustBindable);
        public void RemoveAllAdjustments(AdjustableProperty type) => backing.RemoveAllAdjustments(type);
        public BindableNumber<double> Volume => backing.Volume;
        public BindableNumber<double> Balance => backing.Balance;
        public BindableNumber<double> Frequency => backing.Frequency;
        public BindableNumber<double> Tempo => backing.Tempo;
        #endregion

        #region IAggregateAudioAdjustment
        public IBindable<double> AggregateVolume => backing.AggregateVolume;
        public IBindable<double> AggregateBalance => backing.AggregateBalance;
        public IBindable<double> AggregateFrequency => backing.AggregateFrequency;
        public IBindable<double> AggregateTempo => backing.AggregateTempo;
        #endregion
    }
}
