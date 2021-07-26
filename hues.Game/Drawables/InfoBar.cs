using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osuTK;

using hues.Game.RespackElements;

namespace hues.Game.Drawables
{
    public class InfoBar : CompositeDrawable
    {
        [Resolved]
        private Bindable<RespackElements.Image> currentImage { get; set; }

        [Resolved]
        private Bindable<Hue> currentHue { get; set; }

        [Resolved]
        private Bindable<Song> currentSong { get; set; }

        private LabelBar imageLabel;
        private LabelBar hueLabel;
        private LabelBar songLabel;

        public InfoBar()
        {
            Size = new Vector2(800, 45);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Gray,
                },
                imageLabel = new LabelBar
                {
                    Name = "Image Label Bar",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Size = new Vector2(320, 17),
                    Y = 3,
                    X = -90,
                },
                hueLabel = new LabelBar
                {
                    Name = "Hue Label Bar",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Size = new Vector2(175, 17),
                    Y = 3,
                    X = 160,
                },
                songLabel = new LabelBar
                {
                    Name = "Song Label Bar",
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Size = new Vector2(500, 20),
                    Y = -3,
                },
                new SongControls
                {
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    Size = new Vector2(50, 40),
                    X = -90,
                },
                new ImageControls
                {
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    Size = new Vector2(50, 40),
                    X = -20,
                },
                new VolumeControl
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Size = new Vector2(100, 32),
                    X = 40,
                },
                new SettingsButton
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Size = new Vector2(25),
                    X = 5,
                },
            };

            currentImage.BindValueChanged(change => { imageLabel.SetText(change.NewValue?.Name); }, true);
            currentHue.BindValueChanged(change => { hueLabel.SetText(change.NewValue?.Name); }, true);
            currentSong.BindValueChanged(change => { songLabel.SetText(change.NewValue?.Title); }, true);
        }
    }
}
