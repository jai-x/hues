using System.Collections.Generic;
using System.Linq;

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Graphics;

namespace hues.Game
{
    public class SongManager : Component
    {
        [Resolved]
        private AudioManager audioManager { get; set; }

        [Resolved]
        private Bindable<WorkingSong> workingSong { get; set; }

        private WorkingSong current;

        private Song[] songs;
        private int position;

        public SongManager(Song[] songs)
        {
            this.songs = songs;
            position = 0;
        }

        public void Next()
        {
            if (position == songs.Length - 1)
                position = 0;
            else
                position++;

            update();
        }

        public void Previous()
        {
            if (position == 0)
                position = songs.Length - 1;
            else
                position--;

            update();
        }

        private void update()
        {
            var oldWorking = current;
            var newWorking = new WorkingSong(songs[position], audioManager);

            newWorking.Load();
            oldWorking?.Stop();

            workingSong.Value = newWorking;
            current = newWorking;

            newWorking.Start();
            oldWorking?.Dispose();

            oldWorking = null;
        }

        protected override void LoadComplete()
        {
            update();
        }

        protected override void Dispose(bool isDisposing)
        {
            workingSong.Value = null;
            workingSong.UnbindAll();

            current?.Dispose();
            base.Dispose(isDisposing);
        }
    }
}
