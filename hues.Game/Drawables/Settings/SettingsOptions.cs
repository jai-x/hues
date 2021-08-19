using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Platform;
using hues.Game.Configuration;

namespace hues.Game.Drawables.Settings
{
    public class SettingsOptions : Container
    {
        public SettingsOptions()
        {
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;
        }

        [Resolved]
        private FrameworkConfigManager frameworkConfig { get; set; }

        [Resolved]
        private HuesConfigManager huesConfig { get; set; }

        private SettingsMultiOptionWithLabel<WindowMode> windowOptions;
        private SettingsMultiOptionWithLabel<FrameSync> fpsOptions;
        private SettingsMultiOptionWithLabel<ExecutionMode> threadOptions;

        private SettingsTextBoxWithLabel blurSigmaOptions;
        private SettingsTextBoxWithLabel blurTimeOptions;
        private SettingsDropdownWithLabel<Easing> blurEasingOptions;
        private SettingsTextBoxWithLabel blackoutTimeOptions;

        private readonly Bindable<float> blurSigmaBindable = new Bindable<float>();
        private readonly Bindable<double> blurTimeBindable = new Bindable<double>();
        private readonly Bindable<double> blackoutTimeBindable = new Bindable<double>();

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new FillFlowContainer
                {
                    Name = "Left Column",
                    Direction = FillDirection.Vertical,
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Width = 0.5f,
                    Children = new Drawable[]
                    {
                        new SpriteText
                        {
                            Text = "System",
                            Font = FontUsage.Default.With(size: 12),
                            Colour = Colour4.Black,
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                            Margin = new MarginPadding { Bottom = 5 },
                        },
                        windowOptions = new SettingsMultiOptionWithLabel<WindowMode>
                        {
                            Label = "Window Mode",
                            Items = Enum.GetValues(typeof(WindowMode)).Cast<WindowMode>().ToArray(),
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                        },
                        fpsOptions = new SettingsMultiOptionWithLabel<FrameSync>
                        {
                            Label = "FPS Limit",
                            Items = Enum.GetValues(typeof(FrameSync)).Cast<FrameSync>().ToArray(),
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                        },
                        threadOptions = new SettingsMultiOptionWithLabel<ExecutionMode>
                        {
                            Label = "Execution Mode",
                            Items = Enum.GetValues(typeof(ExecutionMode)).Cast<ExecutionMode>().ToArray(),
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                        },
                    },
                },
                new FillFlowContainer
                {
                    Name = "Right Column",
                    Direction = FillDirection.Vertical,
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Width = 0.5f,
                    Children = new Drawable[]
                    {
                        new SpriteText
                        {
                            Text = "Graphics",
                            Font = FontUsage.Default.With(size: 12),
                            Colour = Colour4.Black,
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                            Margin = new MarginPadding { Bottom = 5 },
                        },
                        blurSigmaOptions = new SettingsTextBoxWithLabel
                        {
                            Label = "Blur Amount (gaussian sigma)",
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                        },
                        blurTimeOptions = new SettingsTextBoxWithLabel
                        {
                            Label = "Blur Time (ms)",
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                        },
                        blurEasingOptions = new SettingsDropdownWithLabel<Easing>
                        {
                            Label = "Blur Easing",
                            Items = Enum.GetValues(typeof(Easing)).Cast<Easing>().ToArray(),
                            DropdownWidth = 175,
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                        },
                        blackoutTimeOptions = new SettingsTextBoxWithLabel
                        {
                            Label = "Blackout Time (ms)",
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                        },
                    },
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            frameworkConfig.BindWith(FrameworkSetting.WindowMode, windowOptions.Current);
            frameworkConfig.BindWith(FrameworkSetting.FrameSync, fpsOptions.Current);
            frameworkConfig.BindWith(FrameworkSetting.ExecutionMode, threadOptions.Current);

            huesConfig.BindWith(HuesSetting.BlurSigma, blurSigmaBindable);
            huesConfig.BindWith(HuesSetting.BlurTimeMs, blurTimeBindable);
            huesConfig.BindWith(HuesSetting.BlurEasing, blurEasingOptions.Current);
            huesConfig.BindWith(HuesSetting.BlackoutTimeMs, blackoutTimeBindable);

            blurSigmaBindable.BindValueChanged(change =>
            {
                blurSigmaOptions.TextBox.Text = change.NewValue.ToString();
            }, true);

            blurTimeBindable.BindValueChanged(change =>
            {
                blurTimeOptions.TextBox.Text = change.NewValue.ToString();
            }, true);

            blackoutTimeBindable.BindValueChanged(change =>
            {
                blackoutTimeOptions.TextBox.Text = change.NewValue.ToString();
            }, true);

            blurSigmaOptions.TextBox.TextCommitted += (newText) =>
            {
                try
                {
                    var val = float.Parse(newText);
                    huesConfig.SetValue(HuesSetting.BlurSigma, val);
                    blurSigmaOptions.TextBox.FlashConfirm();
                }
                catch
                {
                    blurSigmaOptions.FlashErrorMessage("Invalid value");
                    blurSigmaOptions.TextBox.FlashError();
                    blurSigmaOptions.TextBox.Text = blurSigmaBindable.Value.ToString();
                }
            };

            blurTimeOptions.TextBox.TextCommitted += (newText) =>
            {
                try
                {
                    var val = double.Parse(newText);
                    huesConfig.SetValue(HuesSetting.BlurTimeMs, val);
                    blurTimeOptions.TextBox.FlashConfirm();
                }
                catch
                {
                    blurTimeOptions.FlashErrorMessage("Invalid value");
                    blurTimeOptions.TextBox.FlashError();
                    blurTimeOptions.TextBox.Text = blurTimeBindable.Value.ToString();
                }
            };

            blackoutTimeOptions.TextBox.TextCommitted += (newText) =>
            {
                try
                {
                    var val = double.Parse(newText);
                    huesConfig.SetValue(HuesSetting.BlackoutTimeMs, val);
                    blackoutTimeOptions.TextBox.FlashConfirm();
                }
                catch
                {
                    blackoutTimeOptions.FlashErrorMessage("Invalid value");
                    blackoutTimeOptions.TextBox.FlashError();
                    blackoutTimeOptions.TextBox.Text = blackoutTimeBindable.Value.ToString();
                }
            };
        }
    }
}
