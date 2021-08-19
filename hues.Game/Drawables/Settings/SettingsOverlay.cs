using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;

namespace hues.Game.Drawables.Settings
{
    public class SettingsOverlay : VisibilityContainer
    {
        protected override void PopIn()
            => this.FadeIn(200);

        protected override void PopOut()
            => this.FadeOut(200);

        public SettingsOverlay()
        {
            AutoSizeAxes = Axes.Y;
            Width = 800;
            Masking = true;
            BorderColour = Colour4.Black;
            BorderThickness = 3;
        }

        private SettingsTabControl tabControl;
        private FillFlowContainer settingsFlow;

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    Name = "Settings Background",
                    Colour = Colour4.LightGray,
                    RelativeSizeAxes = Axes.Both,
                },
                new FillFlowContainer
                {
                    Direction = FillDirection.Vertical,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        tabControl = new SettingsTabControl
                        {
                            RelativeSizeAxes = Axes.X,
                            Height = 40,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                        },
                        settingsFlow = new FillFlowContainer
                        {
                            Name = "Settings Content",
                            Direction = FillDirection.Vertical,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Padding = new MarginPadding(10),
                            LayoutDuration = 250,
                            LayoutEasing = Easing.OutCirc,
                        },
                    },
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            tabControl.Current.BindValueChanged((change) =>
            {
                settingsFlow.Clear();

                switch (change.NewValue)
                {
                    case SettingsTabOption.Info:
                        LoadComponentAsync(new SettingsInfo
                        {
                            RelativeSizeAxes = Axes.X,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                        }, settingsFlow.Add);
                        break;

                    case SettingsTabOption.Options:
                        LoadComponentAsync(new SettingsOptions
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                        }, settingsFlow.Add);
                        break;

                    default:
                        LoadComponentAsync(new SpriteText
                        {
                            Colour = Colour4.Black,
                            Text = "TODO",
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Margin = new MarginPadding(10),
                        }, settingsFlow.Add);
                        break;
                }
            }, true);
        }
    }
}
