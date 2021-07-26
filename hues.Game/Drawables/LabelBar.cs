using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace hues.Game.Drawables
{
    public class LabelBar : CompositeDrawable
    {
        public void SetText(string text)
        {
            if (labelSpriteText == null)
                return;

            if (text == null)
                labelSpriteText.Text = "[none]";
            else
                labelSpriteText.Text = text;
        }

        public int LabelTextSize = 12;

        private SpriteText labelSpriteText;

        protected override void LoadComplete()
        {
            InternalChildren = new Drawable[]
            {
                new CircularContainer
                {
                    Masking = true,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Colour4.Black,
                        },
                        labelSpriteText = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = FontUsage.Default.With(size: LabelTextSize),
                        },
                    },
                },
            };
        }
    }
}
