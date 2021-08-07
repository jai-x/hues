using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace hues.Game.Drawables
{
    public class PopUpList : VisibilityContainer
    {
        protected override void PopIn()
        {
            this.FadeIn(200);
        }

        protected override void PopOut()
        {
            this.FadeOut(200);
        }


        public PopUpList()
        {
            Masking = true;
            BorderColour = Colour4.Black;
            BorderThickness = 1;
        }

        private FillFlowContainer flow;

        [BackgroundDependencyLoader]
        private void load()
        {
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
                        Spacing = new Vector2(0, 2),
                        Direction = FillDirection.Vertical,
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                    },
                }
            };
        }

        protected void ClearFlow()
        {
            flow.Clear();
        }

        protected void AddToFlow(string text, Action action)
        {
            flow.Add(new ClickableContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Action = action,
                Child = new SpriteText
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Text = text,
                    Colour = Colour4.Black,
                    Font = FontUsage.Default.With(size: 9),
                    Margin = new MarginPadding { Horizontal = 3, Vertical = 1 },
                },
            });
        }
    }
}
