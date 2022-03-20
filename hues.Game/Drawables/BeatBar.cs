using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace hues.Game.Drawables
{
    public class BeatBar : CompositeDrawable
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Size = new Vector2(800, 30);
            Masking = true;
            BorderColour = Colour4.DimGray;
            BorderThickness = 3;

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Name = "Background",
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Gray,
                },
                new CircularContainer
                {
                    Masking = true,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.98f, 0.7f),
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Colour4.Black,
                        },
                        new BeatLine
                        {
                            CharSize = 11,
                            MaxChars = 41,
                            Direction = BeatlineDirection.Left,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.CentreLeft,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(0.5f, 0.7f),
                            X = 5,
                        },
                        new BeatLine
                        {
                            CharSize = 11,
                            MaxChars = 41,
                            Direction = BeatlineDirection.Right,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.CentreRight,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(0.5f, 0.7f),
                            X = -5,
                        },
                        new BeatCircle
                        {
                            CharSize = 18,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(30),
                        }
                    },
                },
            };
        }
    }
}
