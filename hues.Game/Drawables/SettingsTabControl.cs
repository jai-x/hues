using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;

namespace hues.Game.Drawables
{
    public enum SettingsTabOption
    {
        Info,
        Options,
        Editor,
        Resources,
    }

    public class SettingsTabControl : TabControl<SettingsTabOption>
    {
        public SettingsTabControl()
        {
            Items = Enum.GetValues(typeof(SettingsTabOption)).Cast<SettingsTabOption>().ToArray();
        }

        protected override Dropdown<SettingsTabOption> CreateDropdown()
            => null;

        protected override TabItem<SettingsTabOption> CreateTabItem(SettingsTabOption option)
            => new SettingsTabItem(option);

        private class SettingsTabItem : TabItem<SettingsTabOption>
        {
            private Box bottomLine;
            private string text;

            public SettingsTabItem(SettingsTabOption option) : base(option)
            {
                text = option.ToString().ToUpper();
                RelativeSizeAxes = Axes.Both;
                Masking = true;
            }

            [BackgroundDependencyLoader]
            private void load()
            {
                Children = new Drawable[]
                {
                    new Box
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        RelativeSizeAxes = Axes.X,
                        Height = 2,
                        Colour = Colour4.Black,
                    },
                    new Box
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        RelativeSizeAxes = Axes.Y,
                        Width = 2,
                        Colour = Colour4.Black,
                    },
                    new Box
                    {
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreRight,
                        RelativeSizeAxes = Axes.Y,
                        Width = 2,
                        Colour = Colour4.Black,
                    },
                    bottomLine = new Box
                    {
                        Anchor = Anchor.BottomCentre,
                        Origin = Anchor.BottomCentre,
                        RelativeSizeAxes = Axes.X,
                        Height = 2,
                        Colour = Colour4.Black,
                    },
                    new SpriteText
                    {
                        Text = text,
                        Font = FontUsage.Default.With(size: 14),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = Colour4.Black,
                    }
                };
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();
                // This is probably cursed lmao
                Width = 1f / (Parent as TabFillFlowContainer).Children.Count;
            }

            protected override void OnActivated()
                => bottomLine.Hide();

            protected override void OnDeactivated()
                => bottomLine.Show();
        }
    }
}
