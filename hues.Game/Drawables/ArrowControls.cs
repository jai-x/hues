using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace hues.Game.Drawables
{
    public class ArrowControls : CompositeDrawable
    {
        public Action ClickPrevious;
        public Action ClickNext;
        public Action ClickCentre;
        public Action ClickLabel;

        protected virtual string LabelText => "LABEL";
        protected virtual IconUsage CentreIcon => FontAwesome.Regular.QuestionCircle;

        public ArrowControls()
        {
            AutoSizeAxes = Axes.Both;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            InternalChildren = new Drawable[]
            {
                new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 9),
                    Text = LabelText,
                    Y = -21,
                },
                new CircularContainer
                {
                    Name = "Right",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Masking = true,
                    Size = new Vector2(20),
                    X = -13,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Colour4.Black,
                        },
                        new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = FontUsage.Default.With(size: 9),
                            X = -4,
                            Text = "<",
                        }
                    },
                },
                new CircularContainer
                {
                    Name = "Left",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Masking = true,
                    Size = new Vector2(20),
                    X = 13,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Colour4.Black,
                        },
                        new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = FontUsage.Default.With(size: 9),
                            X = 4,
                            Text = ">",
                        }
                    },
                },
                new CircularContainer
                {
                    Name = "Centre",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Masking = true,
                    Size = new Vector2(30),
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Colour4.Black,
                        },
                        new SpriteIcon
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(16),
                            Icon = CentreIcon,
                        }
                    },
                },
            };
        }
    }
}
