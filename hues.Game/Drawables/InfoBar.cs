using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
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
                    X = -90,
                    Y = 6,
                },
                new ImageControls
                {
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    X = -20,
                    Y = 6,
                }
            };

            currentImage.BindValueChanged(change => { imageLabel.LabelText = change.NewValue?.Name; }, true);
            currentHue.BindValueChanged(change => { hueLabel.LabelText = change.NewValue?.Name; }, true);
            currentSong.BindValueChanged(change => { songLabel.LabelText = change.NewValue?.Title; }, true);
        }
    }

    public class LabelBar : CompositeDrawable
    {
        public LabelBar()
        {
            Anchor = Anchor.BottomCentre;
            Origin = Anchor.BottomCentre;
            Size = new Vector2(500, 20);
            Y = -3;
        }

        public string LabelText = "[none]";
        public int LabelTextSize = 12;

        protected override void LoadComplete()
        {
            InternalChildren = new Drawable[]
            {
                new CircularContainer
                {
                    Masking = true,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Colour4.Black,
                        },
                        new SpriteText
                        {
                            Y = 1,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = FontUsage.Default.With(size: LabelTextSize),
                            Text = LabelText ?? "[none]",
                        },
                    },
                },
            };
        }
    }
}
