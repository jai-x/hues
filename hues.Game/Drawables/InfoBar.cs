using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using hues.Game.Elements;

namespace hues.Game.Drawables
{
    public class InfoBar : CompositeDrawable
    {
        [Resolved]
        private Bindable<Elements.Image> currentImage { get; set; }

        [Resolved]
        private Bindable<Hue> currentHue { get; set; }

        [Resolved]
        private Bindable<Song> currentSong { get; set; }

        [Resolved(CanBeNull = true)]
        private Settings settings { get; set; }

        private LabelBar imageLabel;
        private LabelBar hueLabel;
        private LabelBar songLabel;

        public InfoBar()
        {
            Size = new Vector2(800, 50);
            Masking = true;
            BorderColour = Colour4.DimGray;
            BorderThickness = 3;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Name = "Background",
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Gray,
                },
                imageLabel = new LabelBar
                {
                    Name = "Image Label Bar",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Size = new Vector2(320, 17),
                    Y = 4,
                    X = -90,
                },
                hueLabel = new LabelBar
                {
                    Name = "Hue Label Bar",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Size = new Vector2(175, 17),
                    Y = 4,
                    X = 160,
                },
                songLabel = new LabelBar
                {
                    Name = "Song Label Bar",
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Size = new Vector2(500, 20),
                    Y = -4,
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
                    Size = new Vector2(100, 35),
                    X = 40,
                },
                new ClickableContainer
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Size = new Vector2(20),
                    X = 10,
                    Action = () => { settings?.ToggleVisibility(); },
                    Child = new SpriteIcon
                    {
                        RelativeSizeAxes = Axes.Both,
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        Icon = FontAwesome.Solid.Cog,
                    },
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            currentImage.BindValueChanged(change => { imageLabel.SetText(change.NewValue?.Name); }, true);
            currentHue.BindValueChanged(change => { hueLabel.SetText(change.NewValue?.Name); }, true);
            currentSong.BindValueChanged(change => { songLabel.SetText(change.NewValue?.Title); }, true);
        }
    }
}
