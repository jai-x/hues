using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;

namespace hues.Game.Drawables
{
    public class PopUpList : VisibilityContainer
    {
        private FillFlowContainer flow;

        [BackgroundDependencyLoader]
        private void load()
        {
            Masking = true;
            BorderColour = Colour4.Black;
            BorderThickness = 3;

            Children = new Drawable[]
            {
                new Box
                {
                    Colour = Colour4.LightGray,
                    RelativeSizeAxes = Axes.Both,
                },
                new BasicScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    ScrollbarVisible = true,
                    ScrollbarOverlapsContent = false,
                    Child = flow = new FillFlowContainer
                    {
                        Direction = FillDirection.Vertical,
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                    },
                }
            };
        }

        protected override void PopIn() => this.FadeIn(200);

        protected override void PopOut() => this.FadeOut(200);

        protected void ClearFlow() => flow.Clear();

        protected void AddToFlow(string text, Action action)
        {
            if (flow.Count == 0)
                flow.Add(new Spacer());

            flow.Add(new ListLine(text, action));

            flow.Add(new Spacer());
        }

        private class Spacer : Box
        {
            [BackgroundDependencyLoader]
            private void load()
            {
                RelativeSizeAxes = Axes.X;
                Height = 2;
                Colour = Colour4.Black;
            }
        }

        private class ListLine : CompositeDrawable
        {
            private Box background;
            private readonly string text;
            private readonly Action action;
            private readonly Colour4 noHoverColour = Colour4.Transparent;
            private readonly Colour4 hoverColour = Colour4.LightGray.Lighten(5);

            public ListLine(string text, Action action)
            {
                this.text = text;
                this.action = action;
            }

            [BackgroundDependencyLoader]
            private void load()
            {
                RelativeSizeAxes = Axes.X;
                Height = 12;
                Padding = new MarginPadding { Horizontal = 3 };

                InternalChildren = new Drawable[]
                {
                    background = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = noHoverColour,
                    },
                    new SpriteText
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Text = text,
                        Colour = Colour4.Black,
                        Font = FontUsage.Default.With(size: 9),
                        X = 3,
                    },
                };
            }

            protected override bool OnClick(ClickEvent e)
            {
                if (action == null)
                    return false;

                action.Invoke();
                return true;
            }

            protected override void OnHoverLost(HoverLostEvent e) => background.Colour = noHoverColour;

            protected override bool OnHover(HoverEvent e)
            {
                background.Colour = hoverColour;
                return true;
            }
        }
    }
}
