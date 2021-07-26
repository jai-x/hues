using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Logging;

using hues.Game.Stores;

namespace hues.Game.Drawables
{
    public class VolumeControl : CompositeDrawable
    {
        [Resolved]
        private RespackTrackStore trackStore { get; set; }

        protected override void LoadComplete()
        {
            InternalChildren = new Drawable[]
            {
                new SpriteText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Text = "VOLUME",
                    Font = FontUsage.Default.With(size: 11),
                    Y = 3,
                },
                new VolumeSlider
                {
                    Current = trackStore.Volume,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.X,
                    Height = 16,
                },
            };
        }

        private class VolumeSlider : SliderBar<double>
        {
            private Box handle;

            protected override void LoadComplete()
            {
                base.LoadComplete();

                InternalChildren = new Drawable[]
                {
                    new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.X,
                        Colour = Colour4.DarkGray,
                        Height = 5,
                    },
                    handle = new Box
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Y,
                        Colour = Colour4.White,
                        Width = 5,
                    }
                };
            }

            protected override void UpdateValue(float value)
            {
                var offset = (float) Math.Round(DrawWidth * value);
                handle.MoveToX(offset, 300, Easing.OutQuint);
            }
        }
    }
}
