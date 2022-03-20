using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;

namespace hues.Game.Drawables.Settings
{
    public class SettingsMultiOptionWithLabel<T> : FillFlowContainer
    {
        public string Label { get; init; }
        public T[] Items { get; init; }

        public Bindable<T> Current => options.Current;

        private SettingsMultiOption<T> options;

        [BackgroundDependencyLoader]
        private void load()
        {
            Direction = FillDirection.Vertical;
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;
            Margin = new MarginPadding { Vertical = 2 };

            Children = new Drawable[]
            {
                new SpriteText
                {
                    Text = Label,
                    Font = FontUsage.Default.With(size: 11),
                    Colour = Colour4.Black,
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                },
                options = new SettingsMultiOption<T>
                {
                    Items = Items,
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    RelativeSizeAxes = Axes.X,
                    Height = 20,
                },
            };
        }
    }

    public class SettingsMultiOption<T> : TabControl<T>
    {
        public SettingsMultiOption()
        {
            Margin = new MarginPadding { Left = -2 };
        }

        protected override Dropdown<T> CreateDropdown()
            => null;

        protected override TabItem<T> CreateTabItem(T item)
            => new SettingsMultiOptionItem<T>(item);

        private class SettingsMultiOptionItem<I> : TabItem<I>
        {
            private readonly string text;
            private Box background;

            private readonly Colour4 activeColour = Colour4.White;
            private readonly Colour4 inactiveColour = Colour4.Gray;

            public SettingsMultiOptionItem(I item) : base(item)
            {
                text = item.ToString().ToUpper();
                AutoSizeAxes = Axes.Both;
                Masking = true;
                BorderThickness = 2f;
                BorderColour = Colour4.Black;
                Margin = new MarginPadding(2);
            }

            protected override void OnActivated()
                => background.Colour = activeColour;

            protected override void OnDeactivated()
                => background.Colour = inactiveColour;

            [BackgroundDependencyLoader]
            private void load()
            {
                Children = new Drawable[]
                {
                    background = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = inactiveColour,
                    },
                    new SpriteText
                    {
                        Margin = new MarginPadding(3),
                        Text = text,
                        Font = FontUsage.Default.With(size: 9),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = Colour4.Black,
                    },
                };
            }
        }
    }
}
