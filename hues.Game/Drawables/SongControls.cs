using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using hues.Game.Managers;

namespace hues.Game.Drawables
{
    public class SongControls : ArrowControls
    {
        [Resolved]
        private SongManager songManager { get; set; }

        [Resolved(CanBeNull = true)]
        private SongList songList { get; set; }

        [Resolved(CanBeNull = true)]
        private ImageList imageList { get; set; }

        protected override string LabelText => "SONGS";

        protected override Action LabelClick => () =>
        {
            songList?.ToggleVisibility();
            imageList?.Hide();
        };
        protected override Action PreviousClick => () => { songManager.Previous(true); };
        protected override Action NextClick => () => { songManager.Next(true); };
        protected override Action CentreClick => () => { songManager.Random(); };

        protected override void LoadComplete()
        {
            base.LoadComplete();
            CentreIcon = FontAwesome.Solid.Random;
        }
    }
}
