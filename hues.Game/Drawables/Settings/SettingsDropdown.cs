using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using osuTK;

namespace hues.Game.Drawables.Settings
{
    public class SettingsDropdown<T> : Dropdown<T>
    {
        protected override DropdownHeader CreateHeader() => new SettingsDropdownHeader();

        public class SettingsDropdownHeader : DropdownHeader
        {
            private readonly SpriteText label;

            protected override LocalisableString Label
            {
                get => label.Text;
                set => label.Text = value;
            }

            public SettingsDropdownHeader()
            {
                Masking = true;
                BorderThickness = 2;
                BorderColour = Colour4.Black;

                Foreground.AutoSizeAxes = Axes.None;
                Foreground.Height = 15;
                Foreground.Padding = new MarginPadding { Horizontal = 2 };

                BackgroundColour = Colour4.DimGray;
                BackgroundColourHover = Colour4.White;

                label = new SpriteText
                {
                    AlwaysPresent = true,
                    Font = FontUsage.Default.With(size: 9),
                    Colour = Colour4.Black,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                };
            }

            [BackgroundDependencyLoader]
            private void load()
            {
                Children = new Drawable[]
                {
                    label,
                    new SpriteIcon
                    {
                        Icon = FontAwesome.Solid.SortDown,
                        Colour = Colour4.Black,
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreRight,
                        Size = new Vector2(9),
                    },
                };
            }
        }

        protected override DropdownMenu CreateMenu() => new SettingsDropdownMenu();

        public class SettingsDropdownMenu : DropdownMenu
        {
            protected override Menu CreateSubMenu() => new BasicMenu(Direction.Vertical);

            protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction)
                => new BasicScrollContainer(direction);

            protected override DrawableDropdownMenuItem CreateDrawableDropdownMenuItem(MenuItem item)
                => new SettingsDropdownMenuItem(item);

            public class SettingsDropdownMenuItem : DrawableDropdownMenuItem
            {
                public SettingsDropdownMenuItem(MenuItem item) : base(item)
                {
                    Masking = true;
                    BorderThickness = 2;
                    BorderColour = Colour4.Black;

                    Foreground.AutoSizeAxes = Axes.X;
                    Foreground.Height = 15;
                    Foreground.Padding = new MarginPadding { Horizontal = 2 };

                    BackgroundColour = Colour4.DimGray;
                    BackgroundColourHover = Colour4.White;
                    BackgroundColourSelected = Colour4.White;
                }

                protected override Drawable CreateContent() => new SpriteText
                {
                    Font = FontUsage.Default.With(size: 9),
                    Colour = Colour4.Black,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                };
            }
        }
    }
}
