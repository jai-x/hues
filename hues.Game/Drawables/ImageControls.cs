using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using hues.Game.Managers;

namespace hues.Game.Drawables
{
    public class ImageControls : ArrowControls
    {
        [Resolved]
        private ImageManager imageManager { get; set; }

        [Resolved(CanBeNull = true)]
        private SongList songList { get; set; }

        [Resolved(CanBeNull = true)]
        private ImageList imageList { get; set; }

        protected override string LabelText => "IMAGES";

        protected override Action LabelClick => () =>
        {
            imageList?.ToggleVisibility();
            songList?.Hide();
        };
        protected override Action PreviousClick => () => { imageManager.Previous(true); };
        protected override Action NextClick => () => { imageManager.Next(true); };
        protected override Action CentreClick => () =>
        {
            switch (imageManager.Mode)
            {
                case AdvanceMode.Stopped:
                    imageManager.Mode = AdvanceMode.Ordered;
                    break;
                case AdvanceMode.Ordered:
                    imageManager.Mode = AdvanceMode.Random;
                    break;
                case AdvanceMode.Random:
                    imageManager.Mode = AdvanceMode.Stopped;
                    break;
            }
            updateIcon();
        };

        private void updateIcon()
        {
            switch (imageManager.Mode)
            {
                case AdvanceMode.Ordered:
                    CentreIcon = FontAwesome.Solid.PlayCircle;
                    break;
                case AdvanceMode.Stopped:
                    CentreIcon = FontAwesome.Solid.PauseCircle;
                    break;
                case AdvanceMode.Random:
                    CentreIcon = FontAwesome.Solid.Random;
                    break;
            }
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            updateIcon();
        }
    }
}
