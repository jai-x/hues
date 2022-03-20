using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace hues.Game.Drawables.Settings
{
    public class SettingsInfo : FillFlowContainer
    {
        [Resolved]
        private HuesGameBase game { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            Direction = FillDirection.Vertical;
            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopCentre;
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Width = 0.6f;
            Spacing = new Vector2(0, 10);

            Children = new Drawable[]
            {
                new SpriteText
                {
                    Text = "hues",
                    Colour = Colour4.Black,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FontUsage.Default.With(size: 30),
                },
                new SpriteText
                {
                    Text = (game.IsReleased ? "v" : "") + game.Version,
                    Colour = Colour4.Black,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 12),
                },
                new SpriteText
                {
                    Text = $"running on osu-framework v{game.FrameworkVersion}",
                    Colour = Colour4.Black,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 12),
                },
                new Box
                {
                    RelativeSizeAxes = Axes.X,
                    Height = 3,
                    Colour = Colour4.Black,
                },
                new InfoListing
                {
                    Title = "Beat glossary",
                    Contents = new string[]
                    {
                        "x Vertical blur (snare)",
                        "o Horizontal blur (bass)",
                        "- No blur",
                        "+ Blackout",
                        "¤ Whiteout",
                        "| Short blackout",
                        ": Color only",
                        "* Image only",
                        "X Vertical blur only",
                        "O Horizontal blur only",
                        "i Invert all colours",
                        "I Invert & change image",
                    },
                },
                new InfoListing
                {
                    Title = "Keybindings",
                    Contents = new string[]
                    {
                        "       F11: Window Mode",
                        " Ctrl + F1: Draw Visualiser",
                        " Ctrl + F2: Global Statistics",
                        " Ctrl + F3: Texture Visualiser",
                        " Ctrl + F9: Audio Mixer",
                        "Ctrl + F10: Debug Log Overlay",
                        "Ctrl + F11: FPS/Performance Overlay",
                    },
                },
            };
        }
    }

    internal class InfoListing : Container
    {
        public string Title { get; init; }
        public string[] Contents { get; init; }

        [BackgroundDependencyLoader]
        private void load()
        {
            Masking = true;
            CornerRadius = 15;
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Gray.Lighten(0.25f),
                },
                new FillFlowContainer
                {
                    Direction = FillDirection.Vertical,

                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,

                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,

                    Spacing = new Vector2(0, 5),
                    Padding = new MarginPadding(15),

                    Children = Contents.Select(line => new SpriteText
                    {
                        Text = line,
                        Font = FontUsage.Default.With(size: 11),
                        Colour = Colour4.Black,
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                    })
                    .Prepend(new SpriteText
                    {
                        Text = Title,
                        Font = FontUsage.Default.With(size: 14),
                        Colour = Colour4.Black,
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        Margin = new MarginPadding { Bottom = 10 },
                    })
                    .ToArray(),
                },
            };
        }
    }
}
