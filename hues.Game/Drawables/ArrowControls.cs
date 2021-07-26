using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace hues.Game.Drawables
{
    public abstract class ArrowControls : CompositeDrawable
    {
        public Action ClickPrevious;
        public Action ClickNext;
        public Action ClickCentre;
        public Action ClickLabel;

        private SpriteIcon centreIcon;
        protected IconUsage CentreIcon
        {
            get => centreIcon.Icon;
            set => centreIcon.Icon = value;
        }
        protected virtual string LabelText { get; }

        protected virtual Action LabelClick { get; }
        protected virtual Action PreviousClick { get; }
        protected virtual Action NextClick { get; }
        protected virtual Action CentreClick { get; }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            InternalChildren = new Drawable[]
            {
                new ClickableContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Action = LabelClick,
                    Child = new SpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = FontUsage.Default.With(size: 9),
                        Text = LabelText,
                    },
                },
                new Container
                {
                    AutoSizeAxes = Axes.Both,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Y = 1,
                    Children = new Drawable[]
                    {
                        new ClickableContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            AutoSizeAxes = Axes.Both,
                            Action = PreviousClick,
                            X = -13,
                            Child = new CircularContainer
                            {
                                Name = "Previous",
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Masking = true,
                                Size = new Vector2(26, 22),
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
                        },
                        new ClickableContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            AutoSizeAxes = Axes.Both,
                            Action = NextClick,
                            X = 13,
                            Child = new CircularContainer
                            {
                                Name = "Next",
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Masking = true,
                                Size = new Vector2(26, 22),
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
                        },
                        new ClickableContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            AutoSizeAxes = Axes.Both,
                            Action = CentreClick,
                            Child = new CircularContainer
                            {
                                Name = "Centre",
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Masking = true,
                                Size = new Vector2(31),
                                Children = new Drawable[]
                                {
                                    new Box
                                    {
                                        RelativeSizeAxes = Axes.Both,
                                        Colour = Colour4.Black,
                                    },
                                    centreIcon = new SpriteIcon
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        Size = new Vector2(16),
                                        //Icon = CentreIcon,
                                    },
                                },
                            },
                        },
                    }
                }
            };
        }
    }
}
