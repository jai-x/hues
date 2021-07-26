using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;

using hues.Game.Managers;

namespace hues.Game.Drawables
{
    public class ImageControls : ArrowControls
    {
        [Resolved]
        private ImageManager imageManager { get; set; }

        protected override string LabelText => "IMAGES";

        protected override Action LabelClick => () => Logger.Log("Image Label Click");
        protected override Action PreviousClick => () => { imageManager.Previous(true); };
        protected override Action NextClick => () => { imageManager.Next(true); };
        protected override Action CentreClick => () =>
        {
            switch (imageManager.Mode)
            {
                case AdvanceMode.Ordered:
                    imageManager.Mode = AdvanceMode.Stopped;
                    break;
                case AdvanceMode.Stopped:
                    imageManager.Mode = AdvanceMode.Ordered;
                    break;
                default:
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
                    CentreIcon = FontAwesome.Solid.Question;
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
