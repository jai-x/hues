using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;

namespace hues.Game.Drawables.Settings
{
    public class SettingsTextBoxWithLabel : FillFlowContainer
    {
        public string Label { get; init; }
        public SettingsTextBox TextBox => textbox;

        private SettingsTextBox textbox;
        private SpriteText flash;

        public void FlashErrorMessage(string message)
        {
            flash.Hide();
            flash.Text = message;
            flash.ClearTransforms();
            flash.Show();
            flash.FadeOut(2000, Easing.InExpo);
        }

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
                new FillFlowContainer
                {
                    Height = 20,
                    Direction = FillDirection.Horizontal,
                    RelativeSizeAxes = Axes.X,
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Spacing = new Vector2(10, 0),
                    Children = new Drawable[]
                    {
                        textbox = new SettingsTextBox
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Width = 100,
                            Height = 15,
                        },
                        flash = new SpriteText
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Colour = Colour4.Red,
                            Font = FontUsage.Default.With(size: 9),
                            Alpha = 0f,
                        }
                    },
                },
            };
        }
    }

    public class SettingsTextBox : TextBox
    {
        public Action<string> TextCommitted;

        private Colour4 error_colour => Colour4.Red;
        private Colour4 confirm_colour => Colour4.DimGray;
        private Colour4 active_colour => Colour4.White;
        private Colour4 inactive_colour => Colour4.White;
        private Colour4 text_colour => Colour4.Black;
        private Box background;

        [BackgroundDependencyLoader]
        private void load()
        {
            Masking = true;
            BorderThickness = 2f;
            BorderColour = Colour4.Black;

            Add(background = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = inactive_colour,
                Depth = 1,
            });
        }

        public void FlashError() => NotifyInputError();

        public void FlashConfirm() => background.FlashColour(confirm_colour, 200);

        protected override SpriteText CreatePlaceholder() => new SpriteText();

        protected override Caret CreateCaret() => new SettingsCaret();

        protected override Drawable GetDrawableCharacter(char c) => new SpriteText
        {
            Text = c.ToString(),
            Font = FontUsage.Default.With(size: 9),
            Anchor = Anchor.CentreLeft,
            Origin = Anchor.CentreLeft,
            Colour = text_colour,
        };

        protected override void NotifyInputError() => background.FlashColour(error_colour, 200);

        protected override void OnTextCommitted(bool newText)
        {
            base.OnTextCommitted(newText);
            TextCommitted?.Invoke(this.Text);
        }

        protected override void OnFocus(FocusEvent e)
        {
            base.OnFocus(e);
            background.Colour = active_colour;
        }

        protected override void OnFocusLost(FocusLostEvent e)
        {
            base.OnFocusLost(e);
            background.Colour = inactive_colour;
        }
    }

    public class SettingsCaret : Caret
    {
        private Colour4 caret_color = Colour4.Black;
        private const float caret_width = 2f;
        private const float alpha_normal = 0.7f;
        private const float alpha_select = 0.4f;

        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.Y;
            Width = caret_width;
            Height = 0.9f;
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;

            InternalChild = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = caret_color,
            };
        }

        public override void DisplayAt(Vector2 position, float? selectionWidth)
        {
            if (selectionWidth == null)
            {
                this.MoveTo(new Vector2(position.X - (caret_width / 2), position.Y));
                this.ResizeWidthTo(caret_width);
                this.Loop(c => c.FadeTo(alpha_normal).FadeTo(alpha_select, 750, Easing.InOutSine));
            }
            else
            {
                this.MoveTo(new Vector2(position.X, position.Y));
                this.ResizeWidthTo(selectionWidth.Value + (caret_width / 2));
                this.FadeTo(alpha_select);
            }
        }
    }
}
