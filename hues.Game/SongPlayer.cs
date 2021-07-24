using System;

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Logging;

using hues.Game.RespackElements;
using hues.Game.Stores;

namespace hues.Game
{
    public class SongPlayer : Component
    {
        [Resolved]
        private Bindable<Song> currentSong { get; set; }

        [Resolved]
        private Bindable<PlayableSong> currentPlayable { get; set; }

        [Resolved]
        private RespackTrackStore trackStore { get; set; }

        public bool AutoPlay = true;

        protected override void LoadComplete()
        {
            currentSong.BindValueChanged(change => update(change.NewValue));
        }

        private void update(Song song)
        {
            if (song == null)
            {
                currentPlayable.Value?.Stop();
                currentPlayable.Value?.Dispose();
                currentPlayable.Value = null;
                return;
            }

            if (song == currentPlayable.Value?.Song)
                return;

            var buildupTrack = trackStore.Get(song.BuildupSource);
            var loopTrack = trackStore.Get(song.LoopSource);

            var newPlayable = new PlayableSong(song, buildupTrack, loopTrack);
            var oldPlayable = currentPlayable.Value;

            oldPlayable?.Stop();

            currentPlayable.Value = newPlayable;

            if (AutoPlay)
                currentPlayable.Value.Start();

            oldPlayable?.Dispose();
        }

        protected override void Dispose(bool isDisposing)
        {
            currentPlayable.Value?.Stop();
            currentPlayable.Value?.Dispose();
            currentPlayable.Value = null;
            base.Dispose(isDisposing);
        }
    }
}
