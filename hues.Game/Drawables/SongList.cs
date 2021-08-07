using osu.Framework.Allocation;
using osu.Framework.Bindables;
using hues.Game.Elements;

namespace hues.Game.Drawables
{
    public class SongList : PopUpList
    {
        [Resolved]
        private BindableList<Song> allSongs { get; set; }

        [Resolved]
        private Bindable<Song> currentSong { get; set; }

        public SongList()
        {
            Height = 150;
            Width = 435;
        }

        private void updateSongs()
        {

            ClearFlow();

            if (allSongs.Count == 0)
            {
                AddToFlow("No Songs! (Drag a Respack into the window)", null);
                return;
            }

            foreach (var song in allSongs)
            {
                AddToFlow(song.Title, () => { currentSong.Value = song; });
            }
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            updateSongs();

            allSongs.BindCollectionChanged((_,_) => { Schedule(updateSongs); });
        }
    }
}
